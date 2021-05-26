using System;
using System.Linq;
using System.Text;
using Financas.Application.Persistence;
using Financas.Domain;
using System.Security.Cryptography;

namespace Financas.Application.Features.Users
{
    public partial class CadastrarUser
    {
        public class CommandHandler
        {
            private readonly FinancasContext _context;

            public CommandHandler(FinancasContext context)
            {
                _context = context;
            }

            public User Handle(Command command)
            {
                if (command == null || command.UserName == null || command.UserName.Trim() == "" ||
                        command.Password == null || command.Password.Trim() == "" ||
                        command.ConfirmPassword == null || command.ConfirmPassword.Trim() == "" ||
                        command.NomeCompleto == null || command.NomeCompleto.Trim() == "")
                    throw new Exception("Informações faltantes.");

                var user = _context.Users.SingleOrDefault(u => u.UserName == command.UserName);
                if (user != null) throw new Exception("Nome de usuário já cadastrado.");

                if (command.Password != command.ConfirmPassword) throw new Exception("Senhas não conferem.");

                var passwordCrypto = ComputeHash(command.Password, new SHA256CryptoServiceProvider());

                var newUser = new User
                {
                    UserName = command.UserName,
                    Password = passwordCrypto,
                    NomeCompleto = command.NomeCompleto
                };
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return newUser;
            }

            private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
            {
                Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
                return BitConverter.ToString(hashedBytes);
            }
        }
    }
}

