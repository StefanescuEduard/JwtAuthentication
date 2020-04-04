using JwtAuthentication.AsymmetricEncryption.Certificates;
using JwtAuthentication.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtAuthentication.AsymmetricEncryption.Services
{
    public class TokenService
    {
        private readonly User user;
        private readonly SigningAudienceCertificate signingAudienceCertificate;

        public TokenService(User user)
        {
            this.user = user;
            signingAudienceCertificate = new SigningAudienceCertificate();
        }

        public string GetToken()
        {
            SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor();
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        private SecurityTokenDescriptor GetTokenDescriptor()
        {
            const int expiringDays = 7;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(user.Claims()),
                Expires = DateTime.UtcNow.AddDays(expiringDays),
                SigningCredentials = signingAudienceCertificate.GetAudienceSigningKey()
            };

            return tokenDescriptor;
        }
    }
}