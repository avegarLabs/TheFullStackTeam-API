using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using TheFullStackTeam.CvPdfGenerator.Resources;

namespace TheFullStackTeam.CvPdfGenerator.Localization
{
    public class LocalizationService
    {

        private readonly IStringLocalizer _localize;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(ViateTemplateResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);

            if (string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
            {
                SetCurrentCulture();
            }

            _localize = factory.Create("ViateTemplateResources", assemblyName.Name);
        }

        private static void SetCurrentCulture()
        {
            var specifiedCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = specifiedCulture;
            CultureInfo.CurrentUICulture = specifiedCulture;
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            var result = _localize[key];
            return result;
        }

        public LocalizedString GetLocalizedHtmlString(string key, string parameter)
        {
            return _localize[key, parameter];
        }
    }
}
