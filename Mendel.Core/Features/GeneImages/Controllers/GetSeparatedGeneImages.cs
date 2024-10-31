// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Features.Images.Controllers;
// using Mendel.Core.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.GeneImages.Controllers;
//
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/genes/new/test")]
// public class GetSeparatedGeneImagesController : ApiControllerBase
// {
// 	[HttpGet]
// 	public async Task<ActionResult> GetSeparatedGeneImages
// 		([FromQuery] GetSeparatedGeneImagesQuery query)
// 	{
// 		var result = await Mediator.Send(query);
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record GetSeparatedGeneImagesHandler(
// MendelDbContext Database
// ) : IRequestHandler<GetSeparatedGeneImagesQuery, GeneImageVm>
// {
// 	public async Task<GeneImageVm> Handle
// 		(GetSeparatedGeneImagesQuery query, CancellationToken cancel)
// 	{
// 		var gene = await Database.Genes
// 			.Where(x => x.SpeciesId == query.SpeciesId && x.Trait == "Unknown")
// 			.FirstOrDefaultAsync(cancel);
//
// 		if (gene is null)
// 			throw new Exception("Unknown Gene for Species not found");
//
// 		// Get the species group and its genes
// 		var speciesGenes = await Database.Species
// 			.Where(sg => sg.Id == query.SpeciesId)
// 			.Select(sg => new
// 			{
// 				SpeciesName = sg.Name,
// 				Genes = sg.Genes.Select(g => new GeneVm(
// 					g.Id,
// 					g.Trait,
// 					g.Alleles,
// 					g.Description
// 					)).ToList()
// 			})
// 			.FirstOrDefaultAsync(cancel);
//
// 		if (speciesGenes is null)
// 			throw new Exception("Species not found");
//
// 		var imagesWithGenes = await Database.Images
// 			.Where(i => i.ImageGeneSets
// 				.Any(igs => igs.Genes
// 					.Any(g => g.Gene.SpeciesId == query.SpeciesId)))
// 			.Select(i => new ImageDto(i.Id, gene.Id, i.Url, i.Age, i.Sex))
// 			.ToListAsync(cancel);
//
// 		return new GeneImageVm(
// 			query.SpeciesId,
// 			speciesGenes.SpeciesName,
// 			speciesGenes.Genes,
// 			imagesWithGenes
// 			);
// 	}
//
// 	List<List<int>> GetAllPossibleGeneSets(List<GeneVm> genes)
// 	{
// 		var result = new List<List<int>> { new List<int>() };
// 		foreach (var gene in genes)
// 		{
// 			var newSets = new List<List<int>>();
// 			foreach (var set in result)
// 			{
// 				newSets.Add(new List<int>(set));
// 				newSets.Add(new List<int>(set) { gene.Id });
// 			}
// 			result = newSets;
// 		}
// 		return result;
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GetSeparatedGeneImagesQuery(
// int SpeciesId
// ) : IRequest<GeneImageVm>;
//
// public record GeneImageVm(
// int SpeciesId,
// string SpeciesName,
// List<GeneVm> Genes,
// List<ImageDto> Images
// );