using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Credito;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Financas.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("/creditos")]
    public class CartaoCreditoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult RecuperarFaturaDoMes([FromQuery] RecuperarFaturaCartaoCreditoDoMes.Query query,
            [FromServices] RecuperarFaturaCartaoCreditoDoMes.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarFaturaDoMes");

            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("cartao")]
        public IActionResult RecuperarListaDeCartoesCreditos(
            [FromServices] RecuperarListaDeCartoesCreditos.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarListaDeCartoesCreditos");

            var result = handler.Handle();

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{Id}")]
        public IActionResult RecuperarUmGasto([FromRoute] RecuperarUmGastoCartaoCredito.Query query,
            [FromServices] RecuperarUmGastoCartaoCredito.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarUmGasto");

            try
            {
                return Ok(handler.Handle(query));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("cartao/{Id}")]
        public IActionResult RecuperarUmCartaoCredito([FromRoute] RecuperarUmCartaoCredito.Query query,
            [FromServices] RecuperarUmCartaoCredito.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarUmCartaoCredito");

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
        public IActionResult CadastrarGasto([FromBody] CadastrarGastoCartaoCredito.Command command,
            [FromServices] CadastrarGastoCartaoCredito.CommandHandler handler)
        {
            Console.WriteLine("Controller -> CadastrarGasto");

            try
            {
                return Ok(handler.Handle(command));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("cartao")]
        public IActionResult CadastrarCartaoCredito([FromBody] CadastrarCartaoCredito.Command command,
            [FromServices] CadastrarCartaoCredito.CommandHandler handler)
        {
            Console.WriteLine("Controller -> CadastrarCartaoCredito");

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
        public IActionResult EditarGasto([FromBody] EditarGastoCartaoCredito.Command command,
            [FromServices] EditarGastoCartaoCredito.CommandHandler handler)
        {
            Console.WriteLine("Controller -> EditarGasto");

            try
            {
                handler.Handle(command);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("cartao")]
        public IActionResult EditarCartaoCredito([FromBody] EditarCartaoCredito.Command command,
            [FromServices] EditarCartaoCredito.CommandHandler handler)
        {
            Console.WriteLine("Controller -> EditarCartaoCredito");

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
        public IActionResult RemoverUmGasto([FromRoute] RemoverGastoCartaoCredito.Command command,
            [FromServices] RemoverGastoCartaoCredito.CommandHandler handler)
        {
            Console.WriteLine("Controller -> RemoverUmGasto");

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
