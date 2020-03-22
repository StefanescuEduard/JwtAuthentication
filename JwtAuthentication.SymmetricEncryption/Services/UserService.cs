using JwtAuthentication.SymmetricEncryption.Exceptions;
using JwtAuthentication.SymmetricEncryption.Models;
using System.Collections.Generic;
using System.Linq;

namespace JwtAuthentication.SymmetricEncryption.Services
{
    public class UserService
    {
        private readonly IEnumerable<UserCredentials> users;

        public UserService()
        {
            users = new List<UserCredentials>
            {
                new UserCredentials
                {
                    Username = "john.doe",
                    Password = "john.password"
                }
            };
        }

        public void ValidateCredentials(UserCredentials userCredentials)
        {
            bool isValid =
                users.Any(u =>
                    u.Username == userCredentials.Username &&
                    u.Password == userCredentials.Password);

            if (!isValid)
            {
                throw new InvalidCredentialsException();
            }
        }
    }
}