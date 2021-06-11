using Credito.Domain.Agreggates.Conta.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace Credito.Domain.Agreggates.Conta
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public Conta Conta { get; set; }
        public ValorTransacao ValorTransacao { get; set; }
        public Merchant Merchant { get; set; }
    }
}
