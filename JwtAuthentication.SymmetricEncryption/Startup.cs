using JwtAuthentication.Shared;
using JwtAuthentication.Shared.Services;
using JwtAuthentication.SymmetricEncryption.Extensions;
using JwtAuthentication.SymmetricEncryption.Models;
using JwtAuthentication.SymmetricEncryption.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;

namespace JwtAuthentication.SymmetricEncryption
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            IConfigurationSection settingsSection = configuration.GetSection("AppSettings");
            AppSettings settings = settingsSection.Get<AppSettings>();
            byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);

            services.AddAuthentication(signingKey);

            services.Configure<AppSettings>(settingsSection);
            services.AddTransient<AuthenticationService>();
            services.AddTransient<UserService>();
            services.AddTransient<TokenService>();
            services.AddTransient<UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
