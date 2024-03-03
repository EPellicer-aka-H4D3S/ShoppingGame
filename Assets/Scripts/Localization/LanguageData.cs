using System.Collections.Generic;

public enum Language
{
    English = 1,
    Catalan,
    Spanish
}

public class LanguageData 
{
    public Dictionary<Language, string> data;

    public LanguageData(string[] rawData)
    {
        data = new Dictionary<Language, string>();

        for (int i = 1; i < rawData.Length; i++)
        {
            data.Add((Language)i, rawData[i]);
        }
    }

    public string GetText(Language language)
    {
        return data[language];
    }
}