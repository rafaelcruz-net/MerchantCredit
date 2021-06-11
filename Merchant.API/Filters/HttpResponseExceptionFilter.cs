using Credito.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Merchant.API.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is BusinessException ex)
            {

                var result = new ObjectResult(new
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                    Title = "Aconteceu um ou mais problema ao a operação",
                    Detalhes = ex.Errors, 
                    Info = "Entre em contato com nossa central"
                });

                result.StatusCode = (int)HttpStatusCode.UnprocessableEntity;

                context.Result = result;

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
