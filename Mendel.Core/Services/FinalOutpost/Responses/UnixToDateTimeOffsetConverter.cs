using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mendel.Core.Services.FinalOutpost.Responses;

public class UnixToDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
{
	public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetInt64();
		return DateTimeOffset.FromUnixTimeSeconds(value);
	}

	public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}