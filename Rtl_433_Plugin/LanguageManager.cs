
//26/02/2026 Add class Changes to the languages on the TopMost button
//file LanguageManager.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDRSharp.Rtl_433
{
    public static class LanguageManager
    {
        private static readonly HashSet<string> SupportedCultures = new HashSet<string>
        {
            "fr-FR","en-US","de-DE","es-ES","it-IT","pt-PT","pt-BR","nl-NL",
            "sv-SE","nb-NO","da-DK","fi-FI","pl-PL","cs-CZ","sk-SK","hu-HU",
            "ro-RO","tr-TR","ja-JP","zh-CN"
        };
        private static CultureInfo _defaultCulture;
        private static CultureInfo _currentCulture;
        private static ResourceManager _rm;

        public static void Initialize(ResourceManager rm)
        {
            _rm = rm;
            _currentCulture = DetectUserLanguage();
            _defaultCulture = new CultureInfo("en-US");
        }

        private static CultureInfo DetectUserLanguage()
        {
            CultureInfo systemCulture = CultureInfo.InstalledUICulture;
#if DEBUG
            //Key;;en-US;;fr-FR;;de-DE;;es-ES;;it-IT;;pt-PT;;pt-BR;;nl-NL;;sv-SE;;nb-NO;;da-DK;;fi-FI;;pl-PL;;cs-CZ;;sk-SK;;hu-HU;;ro-RO;;tr-TR;;ja-JP;;zh-CN;;se-FI
            return new CultureInfo("se-FI"); //it-IT  se-FI pour test longueurs
#endif
            if (SupportedCultures.Contains(systemCulture.Name))
                return systemCulture;

            string shortLang = systemCulture.TwoLetterISOLanguageName;
            string match = SupportedCultures.FirstOrDefault(c => c.StartsWith(shortLang + "-"));
            if (match != null)
                return new CultureInfo(match);

            return _defaultCulture;  // new CultureInfo("en-US");
        }

        public static string GetString(string key)
        {
           string result=_rm.GetString(key, _currentCulture);
            if(result!="")
                return result;
            else
                return _rm.GetString(key, _defaultCulture);
        }

        public static string GetString(string key, params object[] args)
        {
            string value = GetString(key);  // _rm.GetString(key, _currentCulture); 
            if (args == null || args.Length == 0)
                return value;

            return string.Format(value, args);
        }

        //public static CultureInfo CurrentCulture => _currentCulture;
        //private static CultureInfo myCurrentCulture;

        public static CultureInfo CurrentCulture
        {
            get { return _currentCulture; }
            set { _currentCulture = value; }
        }


    }
//#if DEBUG
//public static class LocalizationBootstrap
//{
//        public static readonly bool Initialized = true;
//        static LocalizationBootstrap()
//    {
//        string culture = "it-IT";
//        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
//        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
//    }
//}
//#endif

}

