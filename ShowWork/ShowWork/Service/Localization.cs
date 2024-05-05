using System.Text.Json;

namespace ShowWork.Service;

public class Localization
{
    public static Dictionary<string, Localization> Languages = new Dictionary<string, Localization>(){
        {"ru" , new Localization("ru")},
        {"en" , new Localization("en")}
    };

    private Dictionary<string, string> labels = new();

    public Localization(string language)
    {
        using(StreamReader r = new StreamReader("./Localizations/Labels-" + language + ".json")) {
            string json = r.ReadToEnd();
            labels = JsonSerializer.Deserialize<Dictionary<string,string>>(json) ?? new  Dictionary<string, string>();
        }
    }

    public string GetLabel(string label, string def = "")
    {
        if (labels.ContainsKey(label))
            return labels[label]; 
        return def;
    }
}

