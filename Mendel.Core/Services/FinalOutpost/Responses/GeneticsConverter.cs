using Mendel.Core.Features.Checklist.Controllers;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Mendel.Core.Services.FinalOutpost.Responses;


public class GeneticsConverter : JsonConverter<List<GenotypeDto>>
{
	public override List<GenotypeDto> Read
		(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetString();
		return value.Split(',')
			.Select(x => x.Split(":"))
			.Select(y => new GenotypeDto(y[0], y[1]))
			.ToList();
	}

	public override void Write(Utf8JsonWriter writer, List<GenotypeDto> value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}