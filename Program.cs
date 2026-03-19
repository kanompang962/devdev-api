using devdev_api.Domain.Entities.Users;
using devdev_api.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Infrastructure
builder.Services
    .AddDatabase(builder.Configuration)
    .AddAutoDependencies()
    // .AddMappers()
    .AddValidation()
    .AddJwtAuthentication(builder.Configuration)
    .AddApiServices()
    .AddSwaggerDocumentation()
    .AddCorsPolicy();

var app = builder.Build();

app.UseCorsPolicy();

app.UseAppMiddleware();

app.Run();
