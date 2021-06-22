using Financas.Application.Persistence;
using System;
using System.Linq;
using LanguageExt;
using System.Text;
using System.Security.Cryptography;
using Application.Infrastructure.Email;
using Financas.Domain;
using System.Threading.Tasks;

namespace Financas.Application.Features.Autenticacao
{
    public partial class RecuperarSenha
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;
            private readonly IEmailService _emailService;

            public CommandHandler(FinancasContext context, IEmailService emailService)
            {
                _context = context;
                _emailService = emailService;
            }

            public void Handle(Command command)
            {
                if (!string.IsNullOrWhiteSpace(command.Email))
                {
                    var user = _context.Users.FirstOrDefault(u => u.Email == command.Email);
                    if (user.IsNull()) return;

                    var passRandom = RandomString();
                    var passwordCrypto = ComputeHash(passRandom, new SHA256CryptoServiceProvider());

                    user.Password = passwordCrypto;
                    user.RefreshToken = null;

                    EnviarEmail(user, passRandom).GetAwaiter().GetResult(); ;

                    _context.SaveChanges();
                    return;
                }

                throw new Exception("E-mail é obrigatorio.");
            }

            private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
            {
                Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
                return BitConverter.ToString(hashedBytes);
            }

            private string RandomString()
            {
                string chars = "abcdefghjkmnpqrstuvwxyz023456789";
                string pass = "";
                Random random = new();
                for (int f = 0; f < 6; f++)
                {
                    pass += chars.Substring(random.Next(0, chars.Length - 1), 1);
                }
                return pass;
            }

            private async Task EnviarEmail(User user, string pass)
            {
                var emailBody = string.Format("<p>Olá {0}. </p>" +
                    "<p>Como solicitado, segue nova senha para acesso aos nossos serviços.</p>" +
                    "<p>Nova Senha: <b>{1}</b></p>" +
                    "<p>Por favor, trocar senha no primeiro acesso.</p>" +
                    "<p>Obrigado!!!</p>",
                    user.NomeCompleto,
                    pass);

                await _emailService.SendEmail(user.Email, "Nova Senha App Finanças", emailBody);                
            }

        }
    }
}
