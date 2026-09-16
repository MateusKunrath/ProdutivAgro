using ProdutivAgro.Api.Extensions;
using ProdutivAgro.Api.Authentication;
using ProdutivAgro.Application;
using ProdutivAgro.Application.Abstractions.Authentication;
using ProdutivAgro.Infrastructure;
using ProdutivAgro.Infrastructure.Extensions;
using ProdutivAgro.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddApiExtensions();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddScoped<AuthenticationCookieService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendDev");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (!builder.Configuration.IsTestEnvironment())
{
    await MigrateDatabase();
}

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DatabaseMigration.MigrateDatabase(scope.ServiceProvider);
}

public partial class Program { }
