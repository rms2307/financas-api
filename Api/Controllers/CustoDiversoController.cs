using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Diverso;

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

        [HttpGet("{Id}")]
        public IActionResult RetornarUmCustoDiverso([FromRoute] RecuperarUmCustoDiverso.Query query,
            [FromServices] RecuperarUmCustoDiverso.QueryHandler handler)
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
