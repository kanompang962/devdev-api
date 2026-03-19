using devdev_api.Extensions;

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
