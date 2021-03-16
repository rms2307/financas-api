using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Fixo;

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

        [HttpGet("{Id}")]
        public IActionResult RecuperarUmCustoFixo([FromRoute] RecuperarUmCustoFixo.Query query,
            [FromServices] RecuperarUmCustoFixo.QueryHandler handler)
        {
            return handler
                .Handle(query)
                .Match<IActionResult>(
                    Some: cd => Ok(cd),
                    None: () => NotFound()
                );
        }
    }
}
