using Financas.Application.Persistence;
using System;
using System.Linq;
using LanguageExt;
using System.Text;
using System.Security.Cryptography;

namespace Financas.Application.Features.Autenticacao
{
    public partial class RecuperarSenha
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
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

                    EnviarEmail(passRandom);

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

            private void EnviarEmail(string pass)
            {
                Console.WriteLine(pass);
            }

        }
    }
}
