using Mendel.Core.Services.FinalOutpost.Responses;
using System.Text.Json;
namespace Mendel.XTest;

public class TFOTests
{
	[Fact]
	public void GetUserCreaturesBySpecies()
	{
		var text = File.ReadAllText("../../../ApiResponses/userSpeciesData.jsonl");
		var result = JsonSerializer.Deserialize<Response>(text);

		Assert.NotNull(result);
		Assert.True(result.Creatures.Count == 10);
		Assert.True(result.Creatures[0].Code == "3dj5d");
		Assert.True(result.Creatures[0].Genetics.Count == 3);
	}
}