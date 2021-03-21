using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Fixo;
using System;

namespace Financas.Api.Controllers
{
    [Route("/fixos")]
    public class CustoFixoContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult RecuperarCustosFixosDoMes([FromQuery] RecuperarCustosFixosDoMes.Query query,
            [FromServices] RecuperarCustosFixosDoMes.QueryHandler handler)
        {
            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{Id}")]
        public IActionResult RecuperarUmCustoFixo([FromRoute] RecuperarUmCustoFixo.Query query,
            [FromServices] RecuperarUmCustoFixo.QueryHandler handler)
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
        public IActionResult CadastrarCustoFixo([FromBody] CadastrarCustoFixo.Command command,
            [FromServices] CadastrarCustoFixo.CommandHandler handler)
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
        public IActionResult EditarCustoFixo([FromBody] EditarCustoFixo.Command command,
            [FromServices] EditarCustoFixo.CommandHandler handler)
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

        [HttpDelete("mes_atual/{Id}")]
        public IActionResult RemoverCustoFixoDoMesAtual(
            [FromRoute] RemoverCustoFixoDoMesAtual.Command command,
            [FromServices] RemoverCustoFixoDoMesAtual.CommandHandler handler)
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

        [HttpDelete("proximos_meses/{Id}")]
        public IActionResult RemoverCustoFixoDosProximosMeses(
            [FromRoute] RemoverCustoFixoDosProximosMeses.Command command,
            [FromServices] RemoverCustoFixoDosProximosMeses.CommandHandler handler)
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

        [HttpDelete("todos_meses/{Id}")]
        public IActionResult RemoverCustoFixoTodosMeses(
            [FromRoute] RemoverCustoFixoTodosMeses.Command command,
            [FromServices] RemoverCustoFixoTodosMeses.CommandHandler handler)
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
