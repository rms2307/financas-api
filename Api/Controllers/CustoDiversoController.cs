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
        public IActionResult RecuperarCustosDiversos([FromQuery] RecuperarCustosDiversos.Query query,
            [FromServices] RecuperarCustosDiversos.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarCustosDiversos");

            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{Id}")]
        public IActionResult RetornarUmCustoDiverso([FromRoute] RecuperarUmCustoDiverso.Query query,
            [FromServices] RecuperarUmCustoDiverso.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RetornarUmCustoDiverso");

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
            Console.WriteLine("Controller -> CadastrarCustoDiverso");

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
            Console.WriteLine("Controller -> EditarCustoDiverso");

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
            Console.WriteLine("Controller -> RemoverUmCustoDiverso");

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
