namespace Comrade.UnitTests.JsonConverters;

public class SaveJsonObjectToFile<TEntity>
{
    public void Excute(List<TEntity> result, string name)
    {
        var oto = JsonSerializer.Serialize(result);

        var path = @"c:\temp\" + name + ".json";

        // This text is added only once to the file.
        if (!File.Exists(path))
        {
            // Create a file to write to.
            File.WriteAllText(path, oto);
        }
    }
}