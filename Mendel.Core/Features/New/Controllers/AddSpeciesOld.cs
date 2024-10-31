// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Mendel.Core.Services.Cloudinary;
// using Mendel.Core.Services.FinalOutpost;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using System.Security.Cryptography;
// namespace Mendel.Core.Features.Species.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/species")]
// public class AddSpeciesController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> AddSpecies
// 		([FromBody] AddSpeciesQuery query)
// 	{
// 		await Mediator.Send(query);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record ImportGrowingCreatureHandler(
// IFinalOutpostService FinalOutpostService,
// MendelDbContext Database,
// ICloudinaryService CloudinaryService,
// ILogger<ImportGrowingCreatureHandler> Log,
// IHttpClientFactory HttpClientFactory
// ) : IRequestHandler<AddSpeciesQuery>
// {
// 	public async Task Handle(AddSpeciesQuery query, CancellationToken cancel)
// 	{
// 		// adult
// 		// code & species
// 		// get genes for species
// 		var species = await Database.Species
// 			.Where(x => x.Name == query.Species)
// 			.FirstOrDefaultAsync(cancel);
//
// 		var genes = await Database.Genes.Where(x => x.SpeciesId == species.Id && x.Trait != "Unknown").ToListAsync(cancel);
//
// 		var groupedGenes = genes.GroupBy(g => g.Trait).Select(g => g.Select((gene => gene.Alleles)).ToList()).ToList();
//
// 		var combinations = CartesianProduct(groupedGenes);
//
// 		var portraits = new List<Image>();
// 		using var httpClient = HttpClientFactory.CreateClient("Default");
//
// 		foreach (var combination in combinations)
// 		{
// 			var url = 	$"https://finaloutpost.net/ln?s={query.Code}&c=" +
// 				string.Join(",", combination.Select((alleles, index) =>
// 					$"{genes.GroupBy(g => g.Trait).ElementAt(index).Key}:{alleles}")) + "&g=male";
//
// 			var imageBytes = await httpClient.GetByteArrayAsync(url);
// 			var base64String = Convert.ToBase64String(imageBytes);
//
// 			var hash = "";
// 			using (var sha256 = SHA256.Create())
// 			{
// 				var hashBytes = sha256.ComputeHash(imageBytes);
// 				hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
// 			}
//
//
// 			var genotype = combination
// 				.Select((alleles, index) =>
// 				{
// 					var trait = genes.GroupBy(g => g.Trait).ElementAt(index).Key; // Get the trait for this index
// 					var genotype = genes.FirstOrDefault(g => g.Trait == trait && g.Alleles == alleles); // Match by trait and allele
// 					return genotype; // Get the Id of the matching genotype
// 				})
// 				.ToList();
//
// 			// var geneMap = new Dictionary<string, string>
// 			// {
// 			// 	{"AA", "1"}, {"BB", "1"}, {"CC", "1"},
// 			// 	{"Aa", "2"}, {"Bb", "2"}, {"Cc", "2"},
// 			// 	{"aa", "3"}, {"bb", "3"}, {"cc", "3"},
// 			// };
// 			//
// 			// // Parse the URL
// 			// var uri = new Uri(url);
// 			// var queryParams = System.Web.HttpUtility.ParseQueryString(uri.Query);
// 			//
// 			// // Extract species code and gender
// 			// var speciesCode = queryParams["s"];
// 			// var gender = queryParams["g"];
// 			//
// 			// // Extract the gene combinations from the 'c' parameter
// 			// var comb = queryParams["c"].Split(',');
// 			//
// 			// var formattedTraits = comb
// 			// 	.Select(comb =>
// 			// 	{
// 			// 		var parts = comb.Split(':');
// 			// 		var trait = parts[0]; // E.g., "Body", "Fins", "Head"
// 			// 		var alleles = parts[1]; // E.g., "AABbCc"
// 			//
// 			// 		// Convert the alleles using the geneMap
// 			// 		var mappedAlleles = string.Concat(
// 			// 			alleles.Batch(2)  // Split alleles into pairs
// 			// 				.Select(pair => geneMap.TryGetValue(pair, out var value) ? value : "")
// 			// 			);
// 			//
// 			// 		return $"{trait}-{mappedAlleles}";
// 			// 	})
// 			// 	.ToList();
//
// 			// Construct the final result
// 			var speciesNameClean = query.Species.Replace(" ", "");
// 			// var filename = $"{speciesNameClean}_{string.Join("-", formattedTraits)}_{char.ToUpper(gender[0])}";
//
// 			var cloudinaryUrl = await CloudinaryService.UploadImageRandomString(url, speciesNameClean);
//
// 			// var portrait = new Portrait
// 			// {
// 			// 	AdultFemaleUrl = cloudinaryUrl,
// 			// 	Genotypes = genotype,
// 			// 	// Sex = gender
// 			// };
//
// 			var newPortrait = new Image
// 			{
// 				Hash = hash,
// 				Genes = genotype,
// 				Sex = "Male",
// 				Age = "Adult",
// 				Url = cloudinaryUrl
// 			};
//
// 			portraits.Add(newPortrait);
//
// 			await Task.Delay(TimeSpan.FromSeconds(1));
// 		}
//
// 		Database.Images.AddRange(portraits);
// 		await Database.SaveChangesAsync();
//
// 	}
//
//
//
// 	// get image
// 	// save to cloudinary
// 	// save to database
//
// 	public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(IEnumerable<IEnumerable<T>> sequences)
// 	{
// 		IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
// 		foreach (var sequence in sequences)
// 		{
// 			result = from seq in result
// 				from item in sequence
// 				select seq.Concat(new[] { item });
// 		}
// 		return result;
// 	}
//
//
// }
//
// public static class Extensions
// {
// 	public static IEnumerable<string> Batch(this string str, int size)
// 	{
// 		for (int i = 0; i < str.Length; i += size)
// 		{
// 			yield return str.Substring(i, Math.Min(size, str.Length - i));
// 		}
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record AddSpeciesQuery(
// string Species,
// string Code
// ) : IRequest;