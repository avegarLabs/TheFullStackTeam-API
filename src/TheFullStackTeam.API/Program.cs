using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Suited.ConnectionString.Resolver.Services.Contracts;
using System.Globalization;
using System.Reflection;
using TheFullStackTeam.API;
using TheFullStackTeam.API.AuthPolicies;
using TheFullStackTeam.API.Configurations;
using TheFullStackTeam.API.Converters;
using TheFullStackTeam.API.Middleware;
using TheFullStackTeam.Application;
using TheFullStackTeam.Application.Services.Extensions;
using TheFullStackTeam.Application.Validators;
using TheFullStackTeam.Communications.Extensions;
using TheFullStackTeam.CvPdfGenerator.Extensions;
using TheFullStackTeam.Persistence.App;
using TheFullStackTeam.RolesMemoryCache;

var builder = WebApplication.CreateBuilder(args);

// Configure CORS policies:
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy => policy
        .WithOrigins(
            // LOCAL DEVELOPMENT:
            "https://localhost:4300",
            "https://localhost:4200",

            // DEV ENVIRONMENT
            "https://tfst-dev-client-portal.azurewebsites.net",
            "https://tfst-dev-management-portal.azurewebsites.net",

            // PROD ENVIRONMENT
            "https://tfst.azurewebsites.net",
            "https://tfst-manamegement.azurewebsites.net",
            "https://app.thefullstackteam.com",
            "https://admin.thefullstackteam.com"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
IdentityModelEventSource.ShowPII = true;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options => builder.Configuration.Bind("AzureAdB2C", options),
        options => builder.Configuration.Bind("AzureAdB2C", options));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsAdmin", policy => policy.Requirements.Add(new IsAdminRequirement()));
    options.AddPolicy("IsAdminOrCustomerService", policy => policy.Requirements.Add(new IsAdminOrCustomerServiceRequirement()));
});
builder.Services.AddTransient<IAuthorizationHandler, IsAdminHandler>();
builder.Services.AddTransient<IAuthorizationHandler, IsAdminOrCustomerServiceHandler>();

builder.Services.AddDbContext<TheFullStackTeamDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "The Full Stack Team API Documentation",
        Description = "A Web API for managing The Full Stack Team",
        Contact = new OpenApiContact
        {
            Name = "Juan García Carmona",
            Url = new Uri("https://www.jgcarmona.com/")
        }
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".XML";
    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
    options.IncludeXmlComments(commentsFile);
});
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));

builder.Services.AddMediatR(typeof(AppResult<>).GetTypeInfo().Assembly);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(AppResult<>).GetTypeInfo().Assembly);
builder.Services.AddTransient<IRolesMemoryCache, RolesMemoryCache>();
//builder.Services.AddSingleton<RolesMemoryCache>();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddApplicationServices();

builder.Services.AddTransient<ISessionService, SessionService>();

builder.Services.AddEmailService();
builder.Services.AddVitaeServices();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("es"),
        new CultureInfo("ca")
    };

    // This will be used if no specific culture can be determined for a given request.
    options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddApplicationInsightsTelemetry();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<TheFullStackTeamDbContext>();
    context.Database.Migrate();
}

app.UseSwagger(); 
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Full Stack Team API - v1");
    c.RoutePrefix = string.Empty; // Set the Swagger UI to be served from the root URL
});

// Localisation
app.Use((context, next) =>
{
    var userLangs = context.Request.Headers["Accept-Language"].ToString();
    var lang = userLangs.Split(',')[0];

    //logger.LogDebug("USER LANG HEADER CONTENT IS " + userLangs);
    //logger.LogDebug("USER FIRST LANGS IS " + lang);

    //If no language header was provided, then default to english.
    if (string.IsNullOrEmpty(lang))
    {
        lang = "en";
    }

    //You could set the environment culture based on the language.
    var ci = new CultureInfo(lang);
    Thread.CurrentThread.CurrentCulture = ci;
    Thread.CurrentThread.CurrentUICulture = ci;

    return next();
});

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();