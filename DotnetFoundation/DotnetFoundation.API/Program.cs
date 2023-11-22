using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Filters;
using DotnetFoundation.API.BLL;
using DotnetFoundation.API.BLL.Interfaces;
using DotnetFoundation.API.Models;
using DotnetFoundation.DAL.DatabaseContext;
using DotnetFoundation.DAL.Repositories;
using DotnetFoundation.DAL.Repositories.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Controller Services
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// AutoMapper Services
builder.Services.AddAutoMapper(typeof(Program));

// Swagger UI Services
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer Scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(filePath);
});

// CORS Services
string[] allowedCorsOrigin = builder.Configuration.GetSection("CorsPolicies:CorsOrigin").Value
    .Split(new char[] { ',', ';' })
    .Select(origin => origin.Trim())
    .ToArray();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(allowedCorsOrigin)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:JWT_Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = true,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
            {
                TokenValidationParameters clonedParameters = validationParameters.Clone();
                clonedParameters.LifetimeValidator = null;

                // If token expiry time is not null, then validate lifetime
                if (expires != null)
                {
                    Validators.ValidateLifetime(notBefore, expires, securityToken, clonedParameters);
                }

                return true;
            }
        };
    });

// Configure Error Response from Model Validations
builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        return ModelValidationBadRequest.ModelValidationErrorResponse(actionContext);
    };
});

// Logging service Serilogs
builder.Logging.AddSerilog();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    path: "logs/log-.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}{NewLine}",
    rollingInterval: RollingInterval.Day,
    restrictedToMinimumLevel: LogEventLevel.Information
    ).CreateLogger();

// DB connection Setup
builder.Services.AddDbContext<SqlDatabaseContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DBConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Adding HTTP Context
builder.Services.AddHttpContextAccessor();

// Adding Services Scope For BLLs - Dependency Injection
builder.Services.AddScoped<IUsersBLL, UsersBLL>();

// Adding Services Scope For Repositories - Dependency Injection
builder.Services.AddScoped<IUsersRepo, UsersRepo>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
