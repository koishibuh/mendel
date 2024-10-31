// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Images.Controllers;
//
// /// <summary>
// /// Returns a list of images that have 0 genes assigned to them.
// /// Can filter by Juvenile, Adult, or All if age is null.
// /// </summary>
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/images")]
// public class GetOrphanedImagesController : ApiControllerBase
// {
// 	[HttpGet("orphaned")]
// 	public async Task<ActionResult> GetOrphanedImages
// 		([FromQuery] int speciesId)
// 	{
// 		var query = new GetOrphanedImagesQuery(speciesId);
// 		var result = await Mediator.Send(query);
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record GetOrphanedImagesHandler(
// MendelDbContext Database
// ) : IRequestHandler<GetOrphanedImagesQuery, List<NoGeneImageVm>>
// {
// 	public async Task<List<NoGeneImageVm>> Handle
// 		(GetOrphanedImagesQuery command, CancellationToken cancel)
// 	{
// 		var gene = await Database.Genes
// 			.Where(x => x.SpeciesId == command.SpeciesId && x.Trait == "Unknown")
// 			.FirstOrDefaultAsync(cancel);
//
// 		if (gene is null)
// 			throw new Exception("Unknown Gene for Species not found");
//
// 		return await Database.Images
// 			.Where(i => i.ImageGeneSets.Any(igs => igs.Genes.Any(g => g.GeneId == gene.Id)))
// 			.Select(i => new NoGeneImageVm (i.Id, i.Url, i.Age, i.Sex))
// 			.ToListAsync(cancel);
//
// 		// var images = await Database.Images
// 		// 		.Where(i => i.ImageGeneSets.All(igs => igs.Genes.Count == 0))
// 		// 		.Select(i => new NoGeneImageVm(i.Id, i.Url, i.Age, i.Sex))
// 		// 		.ToListAsync(cancel);
// 		//
// 		// 	return images;
//
// 		// if (command.Age is null)
// 		// {
// 		// 	var images = await Database.Images
// 		// 		.Where(i => i.ImageGeneSets.All(igs => igs.Genes.Count == 0))
// 		// 		.Select(i => new NoGeneImageVm(i.Id, i.Url, i.Age, i.Sex))
// 		// 		.ToListAsync(cancel);
// 		//
// 		// 	return images;
// 		// }
// 		// else
// 		// {
// 		// 	var images = await Database.Images
// 		// 		.Where(i => i.ImageGeneSets
// 		// 			.All(igs => igs.Genes.Count == 0)
// 		// 			&& i.Age == command.Age)
// 		// 		.Select(i => new NoGeneImageVm(i.Id, i.Url, i.Age, i.Sex))
// 		// 		.ToListAsync(cancel);
// 		//
// 		// 	return images;
// 		// }
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GetOrphanedImagesQuery(
// // string? Age
// int SpeciesId
// ) : IRequest<List<NoGeneImageVm>>;
//
// public record NoGeneImageVm(
// int Id,
// string Url,
// string Age,
// string Sex
// );