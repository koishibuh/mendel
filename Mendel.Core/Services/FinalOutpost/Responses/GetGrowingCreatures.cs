using System.Text.Json.Serialization;

namespace Mendel.Core.Services.FinalOutpost.Responses;

/*══════════════════【 RESPONSE 】══════════════════*/
public class GetGrowingCreatures
{
	[JsonPropertyName("error")]
	public bool Error { get; set; }

	[JsonPropertyName("errorCode")]
	[JsonConverter(typeof(ErrorMessageConverter))]
	public string? ErrorCode { get; set; }

	[JsonPropertyName("creatures")]
	public List<GrowingCreature>? Creatures { get; set; }
}


public class GrowingCreature
{
	[JsonPropertyName("error")]
	public bool Error { get; set; }

	[JsonConverter(typeof(ErrorMessageConverter))]
	[JsonPropertyName("errorCode")]
	public string? ErrorCode { get; set; }

	[JsonPropertyName("code")]
	public string? Code { get; set; }

	[JsonPropertyName("imgsrc")]
	public string? Portrait { get; set; }

	[JsonConverter(typeof(UnixToDateTimeOffsetConverter))]
	[JsonPropertyName("gotten")]
	public DateTimeOffset AcquiredTimestamp { get; set; }

	[JsonPropertyName("growthLevel")]
	public int GrowthLevel { get; set; }

	[JsonPropertyName("name")]
	public string? Name { get; set; }

	[JsonPropertyName("isStunted")]
	public bool Stunted { get; set; }

	[JsonPropertyName("breedName")]
	public string? BreedName { get; set; }

	[JsonPropertyName("genetics")]
	public string? Genetics { get; set; }

	[JsonPropertyName("gender")]
	public string? Gender { get; set; }
}