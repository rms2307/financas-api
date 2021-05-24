using Financas.Application.Persistence;
using Financas.Domain;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Financas.Application.Features.Users
{
    public partial class ValidarUser
    {
        public class QueryHandler
        {
            private readonly FinancasContext _context;

            public QueryHandler(FinancasContext context)
            {
                _context = context;
            }

            public User Handle(QueryUserNameAndPassword query)
            {
                var pass = ComputeHash(query.Password, new SHA256CryptoServiceProvider());
                return _context.Users.FirstOrDefault(u => (u.UserName == query.UserName) && (u.Password == pass));
            }

            public User Handle(QueryUserName query)
            {
                return _context.Users.FirstOrDefault(u => u.UserName == query.UserName);
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
