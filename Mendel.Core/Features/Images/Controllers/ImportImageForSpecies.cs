// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Species.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/species")]
// public class CreatePortraitForSpeciesController : ApiControllerBase
// {
// 	[HttpPost("portrait")]
// 	public async Task<ActionResult> CreatePortraitForSpecies
// 		([FromBody] CreatePortraitForSpeciesCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record CreatePortraitForSpeciesHandler(
// MendelDbContext Database
// ) : IRequestHandler<CreatePortraitForSpeciesCommand>
// {
// 	public async Task Handle(CreatePortraitForSpeciesCommand command, CancellationToken cancel)
// 	{
// 		var genes = await Database.Genes
// 			.Where(x => x.SpeciesId == command.speciesId && x.Trait != "Unknown")
// 			.ToListAsync(cancel);
//
// 		var groupedGenes = genes
// 			.GroupBy(g => g.Trait)
// 			.Select(g => g.Select(gene => gene.Alleles)
// 				.ToList())
// 			.ToList();
//
// 		var combinations = CartesianProduct(groupedGenes);
//
// 		var portraits = new List<Portrait>();
//
// 		foreach (var combination in combinations)
// 		{
// 			var genotype = combination
// 				.Select((alleles, index) =>
// 				{
// 					var trait = genes.GroupBy(g => g.Trait).ElementAt(index).Key; // Get the trait for this index
// 					var genotype = genes.FirstOrDefault(g => g.Trait == trait && g.Alleles == alleles); // Match by trait and allele
// 					return genotype; // Get the Id of the matching genotype
// 				})
// 				.ToList();
//
// 			var portrait = new Portrait { Genotypes = genotype };
// 			portraits.Add(portrait);
// 		}
//
// 		Database.AddRange(portraits);
// 		await Database.SaveChangesAsync(cancel);
// 	}
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
// }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record CreatePortraitForSpeciesCommand(
// int speciesId
// ) : IRequest;