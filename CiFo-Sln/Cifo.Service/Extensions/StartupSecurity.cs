using Cifo.Model;
using Cifo.Model.GovFolder;
using Cifo.Service.Interfaces;
using Google.Cloud.Firestore.V1;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types.FieldFilter.Types;
using FirebaseAdmin;
using Cifo.Service.Document;
using Cifo.Service.Messages;
using Cifo.Service.Storage;
using CIFO.Core.Infraestructure;
using Google.Api;

namespace Cifo.Service.Extensions
{
    public static class StartupSecurity
    {
        public static void ConfigurationAuth(this IServiceCollection services, FirestoreModel firestore)
        {
            IdentityModelEventSource.ShowPII = true;
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
        }
        public static void ConfigurationCifoApp(this IServiceCollection services, FirebaseApp fireBaseApp
            , FirestoreModel firestore, GovFolderUrl govFolderUrl, OperatorDto _operator)
        {
            services.AddSingleton(fireBaseApp);
            services.AddSingleton(FirestoreDb.Create(firestore.ProjectName));
            services.AddScoped<IFirebaseAuthService, FirebaseAuthService>(auth => new FirebaseAuthService(firestore.ApiKey));
            services.AddSingleton<IGovFolderService, GovFolderService>(gov => new GovFolderService(govFolderUrl, _operator));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IUpdateDocumentService, UpdateDocumentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ICloudStorageProvider, FireBStorageProvider>();
        }
    }
}
