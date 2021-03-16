using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.RecuperarCustosDiversos;

namespace Financas.Api.Controllers
{
    [Route("/diversos")]
    public class CustoDiversoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] RecuperarCustosDiversos.Query query,
            [FromServices] RecuperarCustosDiversos.QueryHandler handler)
        {
            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
