using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Diverso;
using System.Net;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Financas.Api.Controllers
{
    [Authorize("Bearer")]
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
            try
            {
                return Ok(handler.Handle(query));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CadastrarCustoDiverso([FromBody] CadastrarCustoDiverso.Command command,
            [FromServices] CadastrarCustoDiverso.CommandHandler handler)
        {
            try
            {
                return Ok(handler.Handle(command));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult EditarCustoDiverso([FromBody] EditarCustoDiverso.Command command,
            [FromServices] EditarCustoDiverso.CommandHandler handler)
        {
            try
            {
                return Ok(handler.Handle(command));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{Id}")]
        public IActionResult RemoverUmCustoDiverso([FromRoute] RemoverUmCustoDiverso.Command command,
            [FromServices] RemoverUmCustoDiverso.CommandHandler handler)
        {
            try
            {
                handler.Handle(command);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
