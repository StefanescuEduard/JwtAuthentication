using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace JwtAuthentication.AsymmetricEncryption.Controllers
{
    [Route("identity/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetClaims()
        {
            var userClaims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(userClaims);
        }

        [HttpGet("name")]
        public IActionResult GetName()
        {
            string name = User.FindFirstValue(ClaimTypes.Name);
            return Ok(name);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRoles()
        {
            IEnumerable<Claim> roleClaims = User.FindAll(ClaimTypes.Role);
            IEnumerable<string> roles = roleClaims.Select(r => r.Value);
            return Ok(roles);
        }
    }
}
