public class TextFileHandler : FileHandler<List<string>>
{
    public TextFileHandler(string filePath) : base(filePath) { }

    public override void Write(List<string> data)
    {
        File.WriteAllLines(FilePath, data);
    }

  public override List<string> Read()
    {
        if (File.Exists(FilePath))
        {
            return File.ReadAllLines(FilePath).ToList();
        }
        throw new FileNotFoundException($"Filen {FilePath} kunde inte hittas.");
    }
}