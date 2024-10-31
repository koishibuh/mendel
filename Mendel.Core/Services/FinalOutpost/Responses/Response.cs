using System.Text.Json.Serialization;
namespace Mendel.Core.Services.FinalOutpost.Responses;

/*══════════════════【 RESPONSE 】══════════════════*/
public class Response
{
	[JsonPropertyName("error")]
	public bool Error { get; set; }

	[JsonPropertyName("errorCode")]
	// [JsonConverter(typeof(ErrorMessageConverter))]
	public string? ErrorCode { get; set; }

	[JsonPropertyName("creatures")]
	public List<CreatureDto>? Creatures { get; set; }
}