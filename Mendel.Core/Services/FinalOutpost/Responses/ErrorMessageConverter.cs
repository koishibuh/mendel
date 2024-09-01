using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mendel.Core.Services.FinalOutpost.Responses;

// public class ErrorMessage
// {
// 	public const string ErrorOne = "User does not exist";
// 	public const string ErrorTwo = "User's lab is hidden";
// 	public const string ErrorThree = "User does not have any growing creatures";
// 	public const string ErrorFour = "Creature does not exist";
// 	public const string ErrorFive = "Invalid API Call, check documentation";
// 	public const string ErrorSix = "Species does not exist";
// 	public const string ErrorSeven = "Tab does not exist";
// 	public const string ErrorEight = "Tab is hidden";
// 	public const string ErrorNine = "This species is locked and it's data is not available to view";
// }

public class ErrorMessageConverter : JsonConverter<string>
{
	public override string? Read
	(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var value = reader.GetInt32();
		return value switch
		{
			0 => "No errors",
			1 => "User does not exist",
			2 => "User's lab is hidden",
			3 => "User does not have any growing creatures",
			4 => "Creature does not exist",
			5 => "Invalid API Call, check documentation",
			6 => "Species does not exist",
			7 => "Tab does not exist",
			8 => "Tab is hidden",
			9 => "This species is locked and it's data is not available to view",
			_ => $"Unknown Error Code {value}"
		};
	}

	public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}