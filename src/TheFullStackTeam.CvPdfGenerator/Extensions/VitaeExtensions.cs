using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using TheFullStackTeam.CvPdfGenerator.Localization;

namespace TheFullStackTeam.CvPdfGenerator.Extensions
{
    public static class VitaeExtensions
    {
        public static void AddVitaeServices(this IServiceCollection services)
        {
            services.AddTransient<ITemplateService, RazorViewsTemplateService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddScoped<LocalizationService>();

            // Add the embedded file provider
            var viewAssembly = typeof(ITemplateService).GetTypeInfo().Assembly;
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
