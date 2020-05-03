using JwtAuthentication.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace JwtAuthentication.Shared
{
    public class UserRepository
    {
        private readonly IEnumerable<User> users;

        public UserRepository()
        {
            users = new List<User>
            {
                new User
                {
                    Username = "john.doe",
                    Password = "john.password",
                    Roles = new []{"User"}
                },
                new User
                {
                    Username = "jane.doe",
                    Password = "jane.password",
                    Roles = new []{"User", "Admin"}
                }
            };
        }

        public User GetUser(string username)
        {
            return users.SingleOrDefault(u => u.Username.Equals(username));
        }
    }
}
