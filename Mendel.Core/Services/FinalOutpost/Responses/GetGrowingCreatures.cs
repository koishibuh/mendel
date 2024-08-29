using System.Text.Json.Serialization;

namespace Mendel.Core.Services.FinalOutpost.Responses;

/*══════════════════【 RESPONSE 】══════════════════*/
public class GetGrowingCreatures
{
	public bool Error { get; set; }

	[JsonConverter(typeof(ErrorMessageConverter))]
	public string ErrorCode { get; set; }

	public List<GrowingCreature> Creatures { get; set; }
}


public class GrowingCreature
{
	public bool Error { get; set; }
	[JsonConverter(typeof(ErrorMessageConverter))]
	public string ErrorCode { get; set; }
	public string Code { get; set; }
	public string ImgSrc { get; set; }
	public DateTimeOffset Gotten { get; set; }
	public int GrowthLevel { get; set; }
	public string Name { get; set; }
	public bool IsStunted { get; set; }
	public string BreedName { get; set; }
	public string Genetics { get; set; }
	public string Gender { get; set; }
}