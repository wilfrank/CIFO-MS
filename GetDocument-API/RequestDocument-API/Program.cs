using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using CIFO.Core.Infraestructure;
using CIFO.Services.Messages;
using CIFO.Services.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<ICloudStorageProvider, FireBStorageProvider>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();
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
