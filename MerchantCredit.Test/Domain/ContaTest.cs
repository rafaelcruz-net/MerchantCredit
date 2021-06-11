using Credito.Domain.Agreggates.Conta;
using Credito.Exception;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MerchantCredit.Test.Domain
{
    public class ContaTest
    {
        [Fact]
        public void DeveCriarUmContaCorretamente()
        {
            Conta conta = new Conta()
            {
                ContaId = Guid.NewGuid(),
                Active = true,
                Limite = new Credito.Domain.Agreggates.Conta.ValueObject.Limite(10000M),
                Nome = "Joao do teste",
                CPF = "123456798900",
            };

            Assert.True(conta.Active);
            Assert.True(conta.Limite.Valor == 10000M);
        }

        [Fact]
        public void DeveTransacionarCorretamenteCasoTenhaLimite()
        {
            Conta conta = new Conta()
            {
                ContaId = Guid.NewGuid(),
                Active = true,
                Limite = new Credito.Domain.Agreggates.Conta.ValueObject.Limite(10000M),
                Nome = "Joao do teste",
                CPF = "123456798900",
            };

            Transacao transacao = new Transacao()
            {
                Id = Guid.NewGuid(),
                Merchant = new Credito.Domain.Agreggates.Conta.ValueObject.Merchant("BK"),
                Time = DateTime.Now,
                ValorTransacao = new Credito.Domain.Agreggates.Conta.ValueObject.ValorTransacao(5000m)
            };

            conta.AplicarTransacao(transacao);

            Assert.True(transacao.Conta != null);
            Assert.True(transacao.Conta.ContaId == conta.ContaId);
            Assert.True(conta.Transacoes.Count > 0);
            Assert.True(conta.Limite.Valor == 5000m);

        }

        [Fact]
        public void NaoDeveTransacionarCasoNaoTenhaLimiteParaCompra()
        {
            Conta conta = new Conta()
            {
                ContaId = Guid.NewGuid(),
                Active = true,
                Limite = new Credito.Domain.Agreggates.Conta.ValueObject.Limite(1000M),
                Nome = "Joao do teste",
                CPF = "123456798900",
            };

            Transacao transacao = new Transacao()
            {
                Id = Guid.NewGuid(),
                Merchant = new Credito.Domain.Agreggates.Conta.ValueObject.Merchant("BK"),
                Time = DateTime.Now,
                ValorTransacao = new Credito.Domain.Agreggates.Conta.ValueObject.ValorTransacao(5000m)
            };


            var exception = Assert.Throws<BusinessException>(() =>
            {
                conta.AplicarTransacao(transacao);
            });

            Assert.True(exception.Errors.Count == 1);

        }

        [Fact]
        public void NaoDeveTransacionarCasoSejaUmaCompraDuplicada()
        {
            Conta conta = new Conta()
            {
                ContaId = Guid.NewGuid(),
                Active = false,
                Limite = new Credito.Domain.Agreggates.Conta.ValueObject.Limite(1000M),
                Nome = "Joao do teste",
                CPF = "123456798900",
            };

            Transacao transacao = new Transacao()
            {
                Id = Guid.NewGuid(),
                Merchant = new Credito.Domain.Agreggates.Conta.ValueObject.Merchant("BK"),
                Time = DateTime.Now,
                ValorTransacao = new Credito.Domain.Agreggates.Conta.ValueObject.ValorTransacao(5000m)
            };

            conta.Transacoes.Add(transacao);

            var exception = Assert.Throws<BusinessException>(() =>
            {
                conta.AplicarTransacao(transacao);
            });

            Assert.True(exception.Errors.Count == 3);

        }
    }
}
