using JwtAuthentication.Shared.Exceptions;
using JwtAuthentication.Shared.Models;

namespace JwtAuthentication.Shared.Services
{
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void ValidateCredentials(UserCredentials userCredentials)
        {
            User user = userRepository.GetUser(userCredentials.Username);
            bool isValid = user.Username == userCredentials.Username &&
                           user.Password == userCredentials.Password;

            if (!isValid)
            {
                throw new InvalidCredentialsException();
            }
        }
    }
}