using System.Text.Json;
using Mendel.Core.Configurations;
using Mendel.Core.Features.Checklist.Controllers;
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
		// var authenticationHeader = new AuthenticationHeaderValue("apiKey", key);
		// httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;
		httpClient.DefaultRequestHeaders.Add("apiKey", key);

		return httpClient;
	}

	public async Task<string> GetGrowingCreatures(string username)
	{
		var httpClient = CreateClient();
		var endPoint = $"lab/{username}";
		var uriBuilder = new UriBuilder(httpClient.BaseAddress + endPoint);

		var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
		using var response = await httpClient.SendAsync(request);

		var content = await response.Content.ReadAsStringAsync();
		if (!response.IsSuccessStatusCode)
		{
			Log.LogError("This is bork");
		}

		var result = JsonSerializer.Deserialize<Response>(content);
		return "test";
	}

	public async Task<List<CreatureDto>?> GetCreatureListBySpecies(UserSpeciesDto dto)
	{
		var httpClient = CreateClient();
		var endPoint = $"creatures/species/{dto.username}/{dto.species}";
		var uriBuilder = new UriBuilder(httpClient.BaseAddress + endPoint);

		var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
		using var response = await httpClient.SendAsync(request);
		if (!response.IsSuccessStatusCode)
		{
			Log.LogError("This is bork");
		}

		var content = await response.Content.ReadAsStringAsync();
		var result = JsonSerializer.Deserialize<Response>(content);

		if (result.Error)
		{
			Log.LogError(result.ErrorCode);
			throw new Exception("Error");
		}

		return result.Creatures;
	}
}

/*═══════════════════【 INTERFACE 】═══════════════════*/
public interface IFinalOutpostService
{
	Task<string> GetGrowingCreatures(string username);
	Task<List<CreatureDto>?> GetCreatureListBySpecies(UserSpeciesDto dto);
}