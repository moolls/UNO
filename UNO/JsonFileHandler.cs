using System.Text.Json;
public class JsonFileHandler<T> : FileHandler<T>
{
    public JsonFileHandler(string filePath) : base(filePath) { }

    public override void Write(T data)
    {
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(FilePath, json);
    }

    public override T Read()
    {
        // Check if the file exists and is not empty
        if (!File.Exists(FilePath) || new FileInfo(FilePath).Length == 0)
        {
            return default(T); // Return null or a default value if the file is empty
        }

        var json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<T>(json);
    }

    
}
