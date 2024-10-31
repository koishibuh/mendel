using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Mendel.Core.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.Genes.Controllers;

/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/genes")]
public class CreateGeneSetsController : ApiControllerBase
{
	[HttpPost("sets")]
	public async Task<ActionResult> CreateGeneSets
		([FromQuery] CreateGeneSetsCommand command)
	{
		await Mediator.Send(command);
		return Ok();
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record CreateGeneSetsHandler(
MendelDbContext Database
) : IRequestHandler<CreateGeneSetsCommand>
{
	public async Task Handle(CreateGeneSetsCommand command, CancellationToken cancel)
	{

		var genesDb = await Database.Genes
			.Where(x => x.SpeciesId == command.SpeciesId)
			.GroupBy(x => x.Trait)
			.Select(x => new
			{
				Trait = x.Key,
				GeneIds = x.Select(x => x.Id).ToList()
			})
			.ToListAsync(cancel);

		var filteredGenesDb = genesDb
			.Where(x => x.Trait != "Unknown")
			.ToList();

		var geneSets = filteredGenesDb
			.Select(x => x.GeneIds)
			.CartesianProduct()
			.Select(x => x.ToList())
			.ToList();

		var unknownGene = genesDb
			.FirstOrDefault(x => x.Trait == "Unknown");

		geneSets.Add(unknownGene.GeneIds);

		foreach (var geneSet in geneSets)
		{
			var set = new Set();

			Database.Sets.Add(set);

			foreach (var geneId in geneSet)
			{
				var geneSetDb = new GeneSet
				{
					Set = set,
					GeneId = geneId
				};

				Database.GeneSets.Add(geneSetDb);
			}
		}

		await Database.SaveChangesAsync(cancel);


		// // var query = await Database.Genes
		// // 	.Where(g => g.Species.Name == command.Name)
		// // 	.GroupBy(g => g.Trait)
		// // 	.Select(g => new
		// // 	{
		// // 		Trait = g.Key,
		// // 		GeneIds = g.Select(x => x.Id).ToList()
		// // 	})
		// // 	.ToListAsync(cancel);
		// //
		// // var filteredQuery = query.Where(g => g.Trait.ToLower() != "unknown").ToList();
		// //
		// // var combinations = filteredQuery
		// // 	.Select(g => g.GeneIds)
		// // 	.CartesianProduct()
		// // 	.Select(combo => combo.ToList())
		// // 	.ToList();
		//
		// var query = await Database.Genes
		// 	.Where(g => g.Species.Name == command.Name && g.Trait != "Unknown")
		// 	.GroupBy(g => g.Trait)
		// 	.Select(g => new
		// 	{
		// 		Trait = g.Key,
		// 		GeneIds = g.Select(x => x.Id).ToList()
		// 	})
		// 	.ToListAsync(cancel);
		//
		// var combinations = query
		// 	.Select(g => g.GeneIds)
		// 	.CartesianProduct()
		// 	.Select(combo => combo.ToList())
		// 	.ToList();
		//
		// foreach (var combo in combinations)
		// {
		// 	// var image = new Image
		// 	// {
		// 	//
		// 	// };
		//
		//
		// 	var imageGeneSet = new ImageGeneSet
		// 	{
		// 		ImageId = 1
		// 	};
		//
		// 	// Database.Update(image);
		// 	Database.Update(imageGeneSet);
		// 	await Database.SaveChangesAsync(cancel);
		//
		// 	var imageGeneSetGenes = combo.Select(geneId => new ImageGeneSetGene
		// 	{
		// 		ImageGeneSetId = imageGeneSet.Id,
		// 		GeneId = geneId
		// 	}).ToList();
		//
		// 	Database.ImageGeneSetGenes.AddRange(imageGeneSetGenes);
		// 	await Database.SaveChangesAsync(cancel);
	}
}

/*════════════════════【 COMMAND 】════════════════════*/
public record CreateGeneSetsCommand(
int SpeciesId
) : IRequest;

// public record GeneSetVm(
// List<GeneVm> Genes,
// List<GeneSetDto> GeneSets
// );

// public record GeneSetDto(
// int Id,
// List<int> GeneIdSet);

public static class EnumerableExtensions
{
	public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
	{
		IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
		return sequences.Aggregate(
			emptyProduct,
			(accumulator, sequence) =>
				from accSeq in accumulator
				from item in sequence
				select accSeq.Concat(new[] { item }));
	}
}