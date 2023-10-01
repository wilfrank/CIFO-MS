using Cifo.Model;
using Cifo.Model.GovFolder;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Cifo.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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
