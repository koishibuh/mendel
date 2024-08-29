using System.Net.Http.Headers;
using System.Text.Json;
using Mendel.Core.Configurations;
using Mendel.Core.Services.FinalOutpost.Responses;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mendel.Core.Services.FinalOutpost;

public record FinalOutpostService(
IOptions<Settings> Settings,
IHttpClientFactory HttpClientFactory,
ILogger<FinalOutpostService> Log
) : IFinalOutpostService
{
	private HttpClient CreateClient()
	{
		var key = Settings.Value.TfoApiKey;

		var httpClient = HttpClientFactory.CreateClient("TheFinalOutpost");
		httpClient.DefaultRequestHeaders.Add("apiKey", key);

		return httpClient;
	}

	public async Task<string> GetGrowingCreatures(string scientistName)
	{
		var httpClient = CreateClient();
		var endPoint = $"lab/{scientistName}";
		var uriBuilder = new UriBuilder(httpClient.BaseAddress + endPoint);

		var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
		using var response = await httpClient.SendAsync(request);

		var content = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			Log.LogError("This is bork");
		}

		var result = JsonSerializer.Deserialize<GetGrowingCreatures>(content);
		return "test";
	}
}

/*═══════════════════【 INTERFACE 】═══════════════════*/
public interface IFinalOutpostService
{
	Task<string> GetGrowingCreatures(string scientistName);
}