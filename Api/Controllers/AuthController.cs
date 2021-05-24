using Financas.Application.Features.Autenticacao;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Financas.Api.Controllers
{
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("signin")]
        public IActionResult Signin([FromBody] Login.Command command,
            [FromServices] Login.CommandHandler handler)
        {
            if (command == null) return BadRequest();

            try
            {
                var token = handler.Handle(command);

                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
