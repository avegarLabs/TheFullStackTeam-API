using System;
using System.Threading;

namespace TheFullStackTeam.Communications.EmailTemplates.Extensions
{
    public static class UrlExtensions
    {
        public static string AbsoluteContent(this string url)
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            var result = new Uri(url + culture + "/").ToString();

            return result;
        }
    }
}