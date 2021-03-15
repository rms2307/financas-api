using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.RecuperarCustosFixos;

namespace Financas.Api.Controllers
{
    [Route("/fixos")]
    public class CustoFixoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] RecuperarCustosFixos.Query query,
            [FromServices] RecuperarCustosFixos.QueryHandler handler)
        {
            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
