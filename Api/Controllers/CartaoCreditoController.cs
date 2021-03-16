using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Credito;

namespace Financas.Api.Controllers
{
    [Route("/creditos")]
    public class CartaoCreditoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] RecuperarGastosCartaoCredito.Query query,
            [FromServices] RecuperarGastosCartaoCredito.QueryHandler handler)
        {
            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
