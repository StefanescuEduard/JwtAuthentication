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
            bool isValid = user != null && AreValidCredentials(userCredentials, user);

            if (!isValid)
            {
                throw new InvalidCredentialsException();
            }
        }

        private static bool AreValidCredentials(UserCredentials userCredentials, User user)
        {
            return user.Username == userCredentials.Username &&
                   user.Password == userCredentials.Password;
        }
    }
}