using DotnetFoundation.Api;
using DotnetFoundation.Application;
using DotnetFoundation.Infrastructure;
using DotnetFoundation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Templates;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string root = Directory.GetCurrentDirectory();
string dotenv = Path.GetFullPath(Path.Combine(root, "..", "..", ".env"));
DotEnv.Load(dotenv);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
})
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// AutoMapper Services
builder.Services.AddAutoMapper(typeof(Program));

// Swagger UI Services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetFoundation API", Version = "v1" });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Standard Authorization header using the Bearer Scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();

    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(filePath);
});

// logging using serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
        formatter: new ExpressionTemplate(
            "{ {Timestamp: @t, Level: @l, TracebackId: @tr, RequestMethod, RequestPath, StatusCode, SourceContext, Data:{EventId: @i, RequestId}, Message: @m, Exception: @x} }\n\n"
        ),
        path: "logs/log-.ndjson",
        rollingInterval: RollingInterval.Day)
    .WriteTo.Console(
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss zzz} [{Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}\n")
    .CreateLogger();

Log.Logger.Information("Logging has started");
builder.Host.UseSerilog();

// Adding HTTP Context
builder.Services.AddHttpContextAccessor();

// Modify builder for different layers
builder.Services.AddApplication();
builder.Services.AddServices();
builder.Services.AddInfrastructure(builder.Configuration);

// Building application
WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
