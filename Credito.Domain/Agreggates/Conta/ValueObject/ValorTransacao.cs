using System;
using System.Collections.Generic;
using System.Text;

namespace Credito.Domain.Agreggates.Conta.ValueObject
{
    public class ValorTransacao
    {
        public Decimal Valor { get; set; }

        public ValorTransacao(Decimal valor)
        {
            this.Valor = valor;
        }

        public ValorTransacao()
        {

        }
    }
}
