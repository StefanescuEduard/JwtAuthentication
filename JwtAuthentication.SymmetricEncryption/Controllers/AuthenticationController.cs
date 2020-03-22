using JwtAuthentication.SymmetricEncryption.Exceptions;
using JwtAuthentication.SymmetricEncryption.Models;
using JwtAuthentication.SymmetricEncryption.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthentication.SymmetricEncryption.Controllers
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