using Financas.Application.Features.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Financas.Api.Controllers
{
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("signin")]
        public IActionResult Signin([FromBody] Signin.Command command,
            [FromServices] Signin.CommandHandler handler)
        {
            if (command.UserName is null || command.Password is null)
                return BadRequest("UserName e/ou Password nulos.");

            try
            {
                var token = handler.Handle(command);
                if (token is null) return Unauthorized();
                return Ok(token);
            }
            catch (Exception e)
            {
                return Unauthorized(e.Message);
            }
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshToken.Command command,
            [FromServices] RefreshToken.CommandHandler handler)
        {
            if (command.AccessToken is null || command.RefreshToken is null)
                return BadRequest("AccessToken e RefreshToken nulos.");

            try
            {
                var token = handler.Handle(command);
                if (token is null) return BadRequest();
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
            
        [HttpGet("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke([FromServices] RevokeToken.CommandHandler handler)
        {
            RevokeToken.Command command = new()
            {
                UserName = User.Identity.Name
            };
            var result = handler.Handle(command);

            return !result ? BadRequest() : NoContent();
        }
    }
}
