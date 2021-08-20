#region

using Comrade.Domain.Bases;
using System.Text.Json;
using System.Text.Json.Serialization;

#endregion

namespace Comrade.UnitTests.JsonConverters;

public class DeserializeJsonObject<TEntity> where TEntity : Entity
{
    public TEntity? Excute(string inputJson)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        options.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<TEntity>(inputJson, options);
    }
}
