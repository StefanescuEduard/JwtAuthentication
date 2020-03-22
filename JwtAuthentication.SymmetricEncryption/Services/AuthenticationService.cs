using JwtAuthentication.SymmetricEncryption.Models;

namespace JwtAuthentication.SymmetricEncryption.Services
{
    public class AuthenticationService
    {
        private readonly UserService userService;
        private readonly TokenService tokenService;

        public AuthenticationService(UserService userService, TokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public string Authenticate(UserCredentials userCredentials)
        {
            userService.ValidateCredentials(userCredentials);
            string securityToken = tokenService.GetToken();

            return securityToken;
        }
    }
}
