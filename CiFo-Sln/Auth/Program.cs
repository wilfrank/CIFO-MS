using Auth.Model;
using Cifo.Service;
using Cifo.Service.Interfaces;
using Firebase.Auth;
using Firebase.Auth.Providers;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "eafit-cifo-firebase.json");
            builder.Services.AddSingleton(FirebaseApp.Create());
            var firestore = builder.Configuration.GetSection("firestore.auth").Get<FirestoreModel>();
            //var firebaseProjectName = "eafit-cifo";
            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = firestore.ApiKey,
                AuthDomain = $"{firestore.ProjectName}.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider(),
                    new GoogleProvider()
                }
            }));

            builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen()
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}