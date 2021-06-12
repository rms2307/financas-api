using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Financas.Application.Features.Receitas;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Financas.Api.Controllers
{
    [Authorize("Bearer")]
    [Route("/receitas")]
    public class ReceitaContoller : ControllerBase
    {
        [HttpGet]
        public IActionResult RecuperarReceitaDoMes([FromQuery] RecuperarReceitasDoMes.Query query,
            [FromServices] RecuperarReceitasDoMes.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarReceitaDoMes");

            var result = handler.Handle(query);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("{Id}")]
        public IActionResult RecuperarUmaReceita([FromRoute] RecuperarUmaReceita.Query query,
            [FromServices] RecuperarUmaReceita.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarUmaReceita");

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
        public IActionResult CadastrarReceita([FromBody] CadastrarReceita.Command command,
            [FromServices] CadastrarReceita.CommandHandler handler)
        {
            Console.WriteLine("Controller -> CadastrarReceita");

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
        public IActionResult EditarReceita([FromBody] EditarReceita.Command command,
            [FromServices] EditarReceita.CommandHandler handler)
        {
            Console.WriteLine("Controller -> EditarReceita");

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

        [HttpDelete("{Id}")]
        public IActionResult RemoverUmaReceita([FromRoute] RemoverUmaReceita.Command command,
            [FromServices] RemoverUmaReceita.CommandHandler handler)
        {
            Console.WriteLine("Controller -> RemoverUmaReceita");

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
