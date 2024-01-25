using System.Globalization;
using TheFullStackTeam.Communications.EmailTemplates.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace TheFullStackTeam.Communications.EmailTemplates.Localization
{
    public class LocalizationService
    {
        private readonly IStringLocalizer _localize;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            var type = typeof(EmailTemplateResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName ?? string.Empty);

            if (string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
            {
                SetCurrentCulture();
            }

            _localize = factory.Create("EmailTemplateResources", assemblyName.Name);
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