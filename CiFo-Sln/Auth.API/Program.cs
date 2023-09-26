using Cifo.Model;
using Cifo.Service;
using Cifo.Service.Interfaces;
using FirebaseAdmin;
using Cifo.Model.GovFolder;
using Google.Cloud.Firestore;
using Google.Apis.Auth.OAuth2;
using Cifo.Service.Extensions;

namespace Auth.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "eafit-cifo-firebase.json");
            var firestore = builder.Configuration.GetSection("firestore.auth").Get<FirestoreModel>();
            var fireBaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(builder.Configuration.GetValue<string>("FIREBASE_CONFIG"))
            }, firestore.ProjectName);
            //var fireBaseApp= FirebaseApp.Create()
            var govFolderUrl = builder.Configuration.GetSection("govCarpeta.settings").Get<GovFolderUrl>();
            var _operator = builder.Configuration.GetSection("govCarpeta.operator").Get<OperatorDto>();
            builder.Services.ConfigurationCifoApp(fireBaseApp, firestore, govFolderUrl, _operator);

            builder.Services.ConfigurationAuth(firestore);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}