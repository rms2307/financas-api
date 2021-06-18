using Financas.Application.Features.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Financas.Api.Controllers
{
    [Route("/users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult RetornarUmUser([FromServices] RecuperarUmUser.QueryHandler handler)
        {
            Console.WriteLine("Controller -> RetornarUmUser");
            try
            {
                return Ok(handler.Handle());
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Signup([FromBody] CadastrarUser.Command command,
            [FromServices] CadastrarUser.CommandHandler handler)
        {
            Console.WriteLine("Controller -> Signup");

            try
            {
                return Ok(handler.Handle(command));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
