using System;
using System.Collections.Generic;
using System.Text;

namespace Credito.Domain.Agreggates.Conta.ValueObject
{
    public class Limite
    {
        public decimal Valor { get; set; }

        public Limite()
        {

        }

        public Limite(decimal limite)
        {
            this.Valor = limite;
        }

        public void Subtrair(ValorTransacao valor)
        {
            this.Valor -= valor.Valor;
        }
    }

}
