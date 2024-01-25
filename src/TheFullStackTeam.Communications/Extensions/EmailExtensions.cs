
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using TheFullStackTeam.Communications.EmailTemplates;
using TheFullStackTeam.Communications.EmailTemplates.Localization;
using TheFullStackTeam.Communications.EmailTemplates.Render;
using TheFullStackTeam.Communications.Services;
using TheFullStackTeam.Communications.Services.Abstract;

namespace TheFullStackTeam.Communications.Extensions
{
    public static class EmailExtensions
    {
        public static void AddEmailService(this IServiceCollection services)
        {
            //Communications Services
            services.AddTransient<IEmailService, Office365MailService>();

            //To Render email template
            services.AddTransient<IEmailViewRenderer, EmailViewRenderer>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<LocalizationService>();

            // Add the embedded file provider
            var viewAssembly = typeof(IEmailViewRenderer).GetTypeInfo().Assembly;
            var fileProvider = new EmbeddedFileProvider(viewAssembly);
            services
                  .AddControllersWithViews()
                  .AddRazorRuntimeCompilation();
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(fileProvider);
            });
        }
    }
}
