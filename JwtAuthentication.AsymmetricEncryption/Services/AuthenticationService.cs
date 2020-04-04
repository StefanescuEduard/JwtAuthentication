using JwtAuthentication.Shared;
using JwtAuthentication.Shared.Models;
using JwtAuthentication.Shared.Services;

namespace JwtAuthentication.AsymmetricEncryption.Services
{
    public class AuthenticationService
    {
        private readonly UserService userService;
        private readonly UserRepository userRepository;

        public AuthenticationService(UserService userService, UserRepository userRepository)
        {
            this.userService = userService;
            this.userRepository = userRepository;
        }

        public string Authenticate(UserCredentials userCredentials)
        {
            userService.ValidateCredentials(userCredentials);
            User user = userRepository.GetUser(userCredentials.Username);
            var tokenService = new TokenService(user);
            string securityToken = tokenService.GetToken();

            return securityToken;
        }
    }
}
