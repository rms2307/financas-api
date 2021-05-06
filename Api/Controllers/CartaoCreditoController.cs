using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Credito;
using System;

namespace Financas.Api.Controllers
{
    [Route("/creditos")]
    public class CartaoCreditoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult RecuperarFaturaDoMes([FromQuery] RecuperarFaturaCartaoCreditoDoMes.Query query,
            [FromServices] RecuperarFaturaCartaoCreditoDoMes.QueryHandler handler)
        {
            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{Id}")]
        public IActionResult RecuperarUmGasto([FromRoute] RecuperarUmGastoCartaoCredito.Query query,
            [FromServices] RecuperarUmGastoCartaoCredito.QueryHandler handler)
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
        public IActionResult CadastrarGasto([FromBody] CadastrarGastoCartaoCredito.Command command,
            [FromServices] CadastrarGastoCartaoCredito.CommandHandler handler)
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

        [HttpPost("cartao")]
        public IActionResult CadastrarCartaoCredito([FromBody] CadastrarCartaoCredito.Command command,
            [FromServices] CadastrarCartaoCredito.CommandHandler handler)
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
        public IActionResult EditarGasto([FromBody] EditarGastoCartaoCredito.Command command,
            [FromServices] EditarGastoCartaoCredito.CommandHandler handler)
        {
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
        public IActionResult EditarGasto([FromBody] EditarCartaoCredito.Command command,
            [FromServices] EditarCartaoCredito.CommandHandler handler)
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
        public IActionResult RemoverUmGasto([FromRoute] RemoverGastoCartaoCredito.Command command,
            [FromServices] RemoverGastoCartaoCredito.CommandHandler handler)
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
