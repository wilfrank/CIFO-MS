using Cifo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Cifo.Service.Extensions
{
    public static class StartupSecurity
    {
        public static void ConfigurationAuth(this IServiceCollection services, FirestoreModel firestore)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = $"https://securetoken.google.com/{firestore.ProjectName}";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = $"https://securetoken.google.com/{firestore.ProjectName}",
                        ValidateAudience = true,
                        ValidAudience = firestore.ProjectName,
                        ValidateLifetime = true
                    };
                });
            // Add services to the container.
            services.AddAuthorization();
        }
    }
}
