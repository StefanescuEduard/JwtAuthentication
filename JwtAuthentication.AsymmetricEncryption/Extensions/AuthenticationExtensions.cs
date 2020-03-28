using JwtAuthentication.AsymmetricEncryption.Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace JwtAuthentication.AsymmetricEncryption.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAsymmetricAuthentication(this IServiceCollection services)
        {
            var issuerSigningCertificate = new SigningIssuerCertificate();
            RsaSecurityKey issuerSigningKey = issuerSigningCertificate.GetIssuerSigningKey();
            services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = issuerSigningKey,
                        LifetimeValidator = LifetimeValidator
                    };
                });

            return services;
        }

        private static bool LifetimeValidator(DateTime? notBefore,
            DateTime? expires,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            return expires != null && expires > DateTime.Now;
        }
    }
}