using CommunicationService.Application.CommandService;
using CommunicationService.Application.QueryService;
using CommunicationService.Domain.Repositories;
using CommunicationService.Domain.Services.Notification;
using CommunicationService.Domain.Services.TypesNotification;
using CommunicationService.Infrastructure.Persistence.EFC.Repositories;
using CommunicationService.Infrastructure.Popularity.TypesNotification;
using CommunicationService.Shared.Infrastructure.Interfaces.ASP.Configuration;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Configuration;
using CommunicationService.Shared.Infrastructure.Persistence.EFC.Repositories;
using IamService.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Database Configuration
// Add Database Connection
var connectionString = builder.Configuration.GetConnectionString("CommunicationContext");

builder.Services.AddTransient<IDbConnection>(db => new MySqlConnection(connectionString));

var connectionStringFromEnvironment = Environment.GetEnvironmentVariable("CommunicationContextDbConnection");

if (connectionStringFromEnvironment != null)
{
    connectionString = connectionStringFromEnvironment;
}

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<CommunicationContext>(
    options =>
    {
        if (connectionString != null)
            if (builder.Environment.IsDevelopment())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            else if (builder.Environment.IsProduction())
                options.UseMySQL(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Error)
                    .EnableDetailedErrors();
    });

#endregion

#region OPENAPI Configuration
// Configure Lowercase URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Communication MicroService",
                Version = "v1",
                Description = "Communication MicroService",
                TermsOfService = new Uri("https://acme-learning.com/tos"),
                Contact = new OpenApiContact
                {
                    Name = "Sweet Manager Studios",
                    Email = "contact@swm.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Apache 2.0",
                    Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                }
            });
        c.EnableAnnotations();
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer", Type = ReferenceType.SecurityScheme
                    }
                },
                Array.Empty<string>()
            }
        });
    });

#endregion

builder.Services.AddHttpContextAccessor();

#region Dependency Injection

// SUPPLY MANAGEMENT BOUNDED CONTEXT
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

builder.Services.AddScoped<ITypesNotificationCommandService, TypesNotificationCommandService>();
builder.Services.AddScoped<ITypesNotificationQueryService, TypesNotificationQueryService>();
builder.Services.AddScoped<ITypesNotificationRepository, TypesNotificationRepository>();

builder.Services.AddScoped<TypesNotificationsInitializer>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

var app = builder.Build();

#region Ensure Database Created (COMPILE AppDbContext)
// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CommunicationContext>();
    context.Database.EnsureCreated();
}
#endregion

#region Run DatabaseInitializer
using (var scope = app.Services.CreateScope())
{
    var notificationInitializer = scope.ServiceProvider.GetRequiredService<TypesNotificationsInitializer>();

    notificationInitializer.InitializeAsync().Wait();
}
#endregion

// Configuration cors
app.UseCors(
    b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();