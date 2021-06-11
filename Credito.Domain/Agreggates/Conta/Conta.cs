using Credito.Domain.Agreggates.Conta.ValueObject;
using Credito.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Credito.Domain.Agreggates.Conta
{
    public class Conta
    {
        public Guid ContaId { get; set; } 
        public bool Active { get; set; }
        public String Nome { get; set; }
        public String CPF { get; set; }
        public Limite Limite { get; set; }
        public List<Transacao> Transacoes { get; set; }

        private readonly BusinessException errors;

        public Conta()
        {
            this.Transacoes = new List<Transacao>();
            this.errors = new BusinessException(); 
        }


        public void AplicarTransacao(Transacao transacao)
        {
            ContaAtiva();

            TemLimiteDisponivel(transacao);

            ValidarTransacao(transacao);

            this.errors.ValidateAndThrow();

            AtivarTransacao(transacao);

        }

        private void AtivarTransacao(Transacao transacao)
        {
            this.Limite.Subtrair(transacao.ValorTransacao);

            transacao.Conta = this;

            this.Transacoes.Add(transacao);
        }

        private void ValidarTransacao(Transacao transacao)
        {
            var transacoes = this.Transacoes.Where(x => x.Time >= transacao.Time.AddMinutes(-5) && x.Time.ToLocalTime() <= transacao.Time.ToLocalTime());
            
            if (transacoes.Count() >= 3)
            {
                this.errors.AddError(new BusinessValidationFailure()
                {
                    ErrorMessage = "Não é possível efetuar mais que 3 compras em periodo menor que 5 minutos"
                });
            }

            if (transacoes.Any(x => x.Merchant.Name == transacao.Merchant.Name && x.ValorTransacao.Valor == transacao.ValorTransacao.Valor))
            {
                this.errors.AddError(new BusinessValidationFailure()
                {
                    ErrorMessage = "Você já efetuou a mesma transação com o valor igual no mesmo estabelecimento"
                });
            }
        }

        private void TemLimiteDisponivel(Transacao transacao)
        {
            if (transacao.ValorTransacao.Valor > this.Limite.Valor)
            {
                this.errors.AddError(new BusinessValidationFailure()
                {
                    ErrorMessage = "Saldo insuficiente para a transação"
                });
            }
        }

        private void ContaAtiva()
        {
            if (this.Active == false)
            {
                this.errors.AddError(new BusinessValidationFailure()
                {
                    ErrorMessage = "Conta não está ativa"
                });
            }
        }
    }
}
