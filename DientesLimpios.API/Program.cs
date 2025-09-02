using DientesLimpios.API.Middleware;
using DientesLimpios.Application;
using DientesLimpios.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseManejadorExceptiones();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
