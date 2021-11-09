namespace Comrade.UnitTests.JsonConverters;

public class SaveJsonObjectToFile<TEntity>
{
    public void Excute(List<TEntity> result, string name)
    {
        var oto = JsonSerializer.Serialize(result);
        var path = @"c:\temp\" + name + ".json";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, oto);
        }
    }
}