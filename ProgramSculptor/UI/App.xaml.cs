using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> availableLanguages = new List<CultureInfo>();
        private static string[] languageCodes = { "en-GB", "ru-RU" };
        private const string valueParameter = "value";
        private const string LanguageFileBegining = "lang.";
        private const string LanguageFilePathFormat = "Resources/" + LanguageFileBegining + "{0}.xaml";
        private ResourceDictionary dict;

        public IReadOnlyList<CultureInfo> Languages
        {
            get
            {
                return availableLanguages.AsReadOnly();
            }
        }

        public App()
        {
            SetAvailableLanguages();

            LanguageChanged += AppLanguageChanged;
        }


        public event EventHandler LanguageChanged;

        private static void SetAvailableLanguages()
        {
            availableLanguages.Clear();
            foreach (string lang in languageCodes)
            {
                availableLanguages.Add(new CultureInfo(lang));
            }
        }

        public CultureInfo Language
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(valueParameter);
                }

                if (value == Thread.CurrentThread.CurrentUICulture)
                {
                    return;
                }

                SetLanguage(value);
            }
        }

        private void SetLanguage(CultureInfo value)
        {
            Thread.CurrentThread.CurrentUICulture = value;

            TrySetDictionaryLanguage(value.Name);

            StartUsingNewDictionary();

            LanguageChanged(this, new EventArgs());
        }

        private void StartUsingNewDictionary()
        {

            //Collection<ResourceDictionary> dictionaries = this.Resources.MergedDictionaries;


            var dictionaries = FindLanguageDictionary(this.Resources);
            ResourceDictionary oldDict = FindLanguageDictionary(dictionaries);


            StartUsingNewDictionary(dictionaries.MergedDictionaries, oldDict);
        }

        private static ResourceDictionary FindLanguageDictionary(ResourceDictionary ditionary)
        {
            return ditionary.MergedDictionaries
                .FirstOrDefault(
                    d => d.Source != null &&
                    d.Source.OriginalString.Contains(LanguageFileBegining));
        }

        private void StartUsingNewDictionary(Collection<ResourceDictionary> dictionaries, ResourceDictionary oldDict)
        {
            if (oldDict != null)
            {
                Replace(dictionaries, oldDict);
            }
            else
            {
                dictionaries.Add(dict);
            }
        }

        private void Replace(Collection<ResourceDictionary> dictionaries, ResourceDictionary oldDict)
        {
            int index = dictionaries.IndexOf(oldDict);
            dictionaries.Remove(oldDict);
            dictionaries.Insert(index, dict);
        }

        private void TrySetDictionaryLanguage(string valueName)
        {
            if (languageCodes.Contains(valueName))
            {
                SetDictionaryLanguage(valueName);
            }
            else
            {
                SetDictionaryLanguage(languageCodes[0]);
            }
        }

        private void SetDictionaryLanguage(string valueName)
        {
            dict = new ResourceDictionary();
            string fileName = string.Format(LanguageFilePathFormat, valueName);
            dict.Source = new Uri(fileName, UriKind.Relative);
        }

        private void ApplicationLoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            Language = UI.Properties.Settings.Default.CurrentLanguage;
        }

        private void AppLanguageChanged(object sender, EventArgs e)
        {
            UI.Properties.Settings.Default.CurrentLanguage = Language;
            UI.Properties.Settings.Default.Save();
        }
    }
}
