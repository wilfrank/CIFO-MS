using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using CIFO.Core.Infraestructure;
using CIFO.Services.Messages;
using CIFO.Services.GovCarpeta;
using CIFO.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ICloudStorageProvider, FireBStorageProvider>();
builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
