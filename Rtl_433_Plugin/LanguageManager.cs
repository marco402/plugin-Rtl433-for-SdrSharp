
//26/02/2026 Add class Changes to the languages on the TopMost button
//file LanguageManager.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.Rtl_433
{
    public static class LanguageManager
    {
        private static readonly HashSet<string> SupportedCultures = new HashSet<string>
        {
            "fr-FR","en-US","de-DE","es-ES","it-IT","pt-PT","pt-BR","nl-NL",
            "sv-SE","no-NO","da-DK","fi-FI","pl-PL","cs-CZ","sk-SK","hu-HU",
            "ro-RO","tr-TR","ja-JP","zh-CN"
        };

        private static CultureInfo _currentCulture;
        private static ResourceManager _rm;

        public static void Initialize(ResourceManager rm)
        {
            _rm = rm;
            _currentCulture = DetectUserLanguage();
        }

        private static CultureInfo DetectUserLanguage()
        {
            CultureInfo systemCulture = CultureInfo.InstalledUICulture;

            if (SupportedCultures.Contains(systemCulture.Name))
                return systemCulture;

            string shortLang = systemCulture.TwoLetterISOLanguageName;
            string match = SupportedCultures.FirstOrDefault(c => c.StartsWith(shortLang + "-"));
            if (match != null)
                return new CultureInfo(match);

            return new CultureInfo("en-US");
        }

        public static string GetString(string key)
        {
            return _rm.GetString(key, _currentCulture);
        }

        public static CultureInfo CurrentCulture => _currentCulture;
    }

}

