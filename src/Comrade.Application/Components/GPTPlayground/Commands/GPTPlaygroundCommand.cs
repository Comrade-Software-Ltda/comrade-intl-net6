using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Comrade.Application.Components.GPTPlayground.Commands;

public class GPTPlaygroundCommand : IGPTPlaygroundCommand
{
    public async Task<dynamic> Create()
    {
        var apiKey ="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";
        HttpClient client = new();
        client.BaseAddress = new Uri("https://api.openai.com/v1/");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
            
        var payload = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = "2+2" },
                new { role = "system", content = "you are now a monkey that will stay in character all the time no matter whats said and just play along." }
            }
        };

        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("chat/completions", content);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            var completion = JsonSerializer.Deserialize<dynamic>(responseString);
            Console.WriteLine(completion);
            return completion;
        }
        else
        {
            Console.WriteLine("API key is valid!");
        }
        
        content.Dispose();

        return response;
    }



}
