using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public static Localizer instance;

    Dictionary<string, LanguageData> data;
    private Language currentLanguage;
    public Language defaultLanguage;

    public TextAsset dataSheet;
    
    public delegate void LanguageChangeDelegate();
    public static LanguageChangeDelegate OnLanguageChangeDelegate;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        currentLanguage = defaultLanguage;
        LoadLanguageSheet();
    }

    public static string GetText(string textKey)
    {
        return instance.data[textKey].GetText(instance.currentLanguage);
    }

    public static void SetLanguage(Language language)
    {
        instance.currentLanguage = language;
        OnLanguageChangeDelegate?.Invoke();
    }

    void LoadLanguageSheet()
    {
        string[] lines = dataSheet.text.Split(new char[]{ '\n'});

        for (int i = 1; i < lines.Length; i++)
        {
            if (lines.Length > 1) AddNewDataEntry(lines[i]);
        }
    }

    void AddNewDataEntry(string str)
    {
        string[] entry = str.Split(new char[] { ';' });

        var languageData = new LanguageData(entry);

        if (data == null) data = new Dictionary<string, LanguageData>();
        data.Add(entry[0], languageData);
    }
}