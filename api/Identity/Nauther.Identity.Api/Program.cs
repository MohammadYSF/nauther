using Nauther.Framework.Application.Extensions;
using Nauther.Framework.Infrastructure.Extensions;
using Nauther.Identity.Api;
using Nauther.Identity.Application;
using Nauther.Identity.Infrastructure;
using Nauther.Identity.Persistence;
using Nauther.Identity.Persistence.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplicationServices()
    .AddAuthUserService()
    .AddFluentValidationService()
    .AddInquiryServices(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddSerilogLogging(builder.Configuration)
    .AddFileService(builder.Configuration)
    .AddRedisCacheService(builder.Configuration)
    .AddEasyCachingService(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddApiServices(builder.Configuration)
    .AddHttpClientHandlers();



builder.Configuration
    .AddConfiguration(builder.Environment);

builder.Services.AddHostedService<CacheSeeder>();
var app = builder.Build();

// Apply migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Users.ExecuteDelete();
    //db.UserCredentials.ExecuteDelete();
    //db.UserRoles.ExecuteDelete();
    //db.UserPermissions.ExecuteDelete();
    // db.Database.Migrate();
}

app.AddMiddlewares();

app.Run();
