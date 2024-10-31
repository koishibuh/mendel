using Mendel.Core.Features.Checklist.Controllers;
using System.Text.Json.Serialization;
namespace Mendel.Core.Services.FinalOutpost.Responses;

/*══════════════════【 RESPONSE 】══════════════════*/
public class GetUserCreaturesBySpecies
{
	[JsonPropertyName("error")]
	public bool Error { get; set; }

	[JsonPropertyName("errorCode")]
	[JsonConverter(typeof(ErrorMessageConverter))]
	public string? ErrorCode { get; set; }

	[JsonPropertyName("creatures")]
	public List<CreatureDto>? Creatures { get; set; }
}

public class CreatureDto
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
	public string? SpeciesName { get; set; }

	[JsonPropertyName("genetics")]
	[JsonConverter(typeof(GeneticsConverter))]
	public List<GenotypeDto>? Genetics { get; set; }

	[JsonPropertyName("gender")]
	public string? Gender { get; set; }
}

public static class UserCreatureExtensions
{
	public static List<ChecklistCreatureVm> CreateVm(this List<CreatureDto> creatures) =>
		creatures
			.Select(x => new ChecklistCreatureVm(
				x.Code, x.Name, x.GrowthLevel, x.Stunted,
				x.SpeciesName, x.Genetics, x.Gender))
			.ToList();
}

public class Genetics
{
	public string Name { get; set; }
	public string Allele { get; set; }
}