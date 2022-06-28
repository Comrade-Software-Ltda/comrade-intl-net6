using Comrade.Domain.Bases;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Comrade.Persistence.Extensions;

public static class JsonUtilities
{
    public static List<TTargetModel>? GetListFromJson<TTargetModel>(Stream? jsonStream)
        where TTargetModel : Entity
    {
        if (jsonStream != null)
        {
            var reader = new StreamReader(jsonStream);
            var jsonString = reader.ReadToEnd();

            var list = JsonConvert.DeserializeObject<List<TTargetModel>>(jsonString);

            reader.Dispose();

            return list;
        }

        return new List<TTargetModel>();
    }

    public static List<TTargetModel>? GetListFromJsonSnakeCaseNamingStrategy<TTargetModel>(
        Stream? jsonStream)
        where TTargetModel : Entity
    {
        if (jsonStream != null)
        {
            var reader = new StreamReader(jsonStream);
            var jsonString = reader.ReadToEnd();

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy {ProcessDictionaryKeys = true}
                },
                Formatting = Formatting.Indented
            };

            var list = JsonConvert.DeserializeObject<List<TTargetModel>>(jsonString, settings);

            reader.Dispose();

            return list;
        }

        return new List<TTargetModel>();
    }
}