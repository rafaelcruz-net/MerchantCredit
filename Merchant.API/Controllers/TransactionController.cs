using Credito.Domain.Agreggates.Conta;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public static List<Conta> Contas { get; set; } = new List<Conta>();
        

        [HttpPost]
        [Route("")]
        public IActionResult Save([FromBody] Conta model)
        {
            model.ContaId = Guid.NewGuid();
            
            Contas.Add(model);

            return Created("", model);
        }

        [HttpPost]
        [Route("transaction")]
        public IActionResult CreateTransaction([FromBody] Transacao model)
        {
            var conta = Contas.FirstOrDefault();

            conta.AplicarTransacao(model);

            return Ok();
        }
    }
}
