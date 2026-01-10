using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class HelpPageManager : MonoBehaviour
{
    public void ChangeLanguage(int localeIndex)
    {
        StartCoroutine(SetLocale(localeIndex));
    }

    IEnumerator SetLocale(int localeIndex)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
    }
}
