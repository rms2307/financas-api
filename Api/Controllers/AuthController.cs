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
            Console.WriteLine("Controller -> Signin");

            if (string.IsNullOrWhiteSpace(command.UserName) || string.IsNullOrWhiteSpace(command.Password))
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
            Console.WriteLine("Controller -> Refresh");

            if (string.IsNullOrWhiteSpace(command.AccessToken) || string.IsNullOrWhiteSpace(command.RefreshToken))
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

        [HttpPost("recuperar-senha")]
        public IActionResult RecuperarSenha([FromBody] RecuperarSenha.Command command,
            [FromServices] RecuperarSenha.CommandHandler handler)
        {
            Console.WriteLine("Controller -> RecuperarSenha");

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

        [HttpGet("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke([FromServices] RevokeToken.CommandHandler handler)
        {
            Console.WriteLine("Controller -> Revoke");

            var result = handler.Handle();

            return !result ? BadRequest() : Ok(result);
        }
    }
}
