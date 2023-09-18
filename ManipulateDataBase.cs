using Newtonsoft.Json;

namespace ManipulateJSON;

public class ManipulateDataBase
{
    public void AddJSON<TObject>(string filepath, string key, TObject value)
    {
        Dictionary<string, TObject>? _instance = JsonConvert.DeserializeObject<Dictionary<string, TObject>>(File.ReadAllText(filepath));

        int lastObject = _instance != null ? int.Parse(_instance.Last().Key[key.Length..]) + 1 : 0;
        _instance ??= new Dictionary<string, TObject>();
        _instance.Add($"{key}{lastObject}", value);

        File.WriteAllText(filepath, JsonConvert.SerializeObject(_instance, Formatting.Indented, new JsonSerializerSettings()));
    }
    public TObject? ReadJSON<TObject>(string filepath, string key)
    {
        string rawStringJsonInput = File.ReadAllText(filepath);
        Dictionary<string, TObject>? _instance = JsonConvert.DeserializeObject<Dictionary<string, TObject>>(rawStringJsonInput);
        TObject? value;
        if (_instance != null)
            _ = _instance.TryGetValue(key, out value);
        else
            value = default;
        return value;
    }
    public Dictionary<string, TObject>? ReadAllJSON<TObject>(string filepath)
    {
        string rawStringJsonInput = File.ReadAllText(filepath);
        Dictionary<string, TObject>? _instance = JsonConvert.DeserializeObject<Dictionary<string, TObject>>(rawStringJsonInput);
        return _instance;
    }
    public void RemoveJSON<TObject>(string filepath, string key)
    {
        Dictionary<string, TObject>? _instance = JsonConvert.DeserializeObject<Dictionary<string, TObject>>(File.ReadAllText(filepath));
        _instance?.Remove(key);
        File.WriteAllText(filepath, JsonConvert.SerializeObject(_instance, Formatting.Indented, new JsonSerializerSettings()));
    }
}
