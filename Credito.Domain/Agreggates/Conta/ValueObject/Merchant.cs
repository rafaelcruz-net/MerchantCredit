using System;
using System.Collections.Generic;
using System.Text;

namespace Credito.Domain.Agreggates.Conta.ValueObject
{
    public class Merchant
    {
        public string Name { get; set; }

        public Merchant()
        {

        }

        public Merchant(string name)
        {
            this.Name = name;

        }
    }
}
