using JwtAuthentication.AsymmetricEncryption.Services;
using JwtAuthentication.Shared.Exceptions;
using JwtAuthentication.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.AsymmetricEncryption.Controllers
{
    [Route("identity/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials userCredentials)
        {
            try
            {
                string token = authenticationService.Authenticate(userCredentials);
                return Ok(token);
            }
            catch (InvalidCredentialsException)
            {
                return Unauthorized();
            }
        }
    }
}