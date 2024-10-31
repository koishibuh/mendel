// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Images.Controllers;
//
// // TODO: What should be returned after genes are added?
// // TODO: Check if gene already has both ages set
//
// /// <summary>
// /// Sets the Gene Ids for each Image Id
// /// </summary>
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/images")]
// public class AddGenesToImagesController : ApiControllerBase
// {
// 	[HttpPost("genes")]
// 	public async Task<ActionResult> AddGenesToImages
// 		([FromBody] AddGenesToImagesCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddGenesHandler(
// MendelDbContext Database
// ) : IRequestHandler<AddGenesToImagesCommand>
// {
// 	public async Task Handle
// 		(AddGenesToImagesCommand command, CancellationToken cancel)
// 	{
// 		foreach (var dto in command.AddGenesDtos)
// 		{
// 			var imageGeneSet = new ImageGeneSet { ImageId = dto.ImageId };
//
// 			Database.ImageGeneSets.Add(imageGeneSet);
// 			await Database.SaveChangesAsync(cancel);
//
//
// 			var imageGeneSetGenes = dto.GeneIds.Select(geneId => new ImageGeneSetGene
// 			{
// 				ImageGeneSetId = imageGeneSet.Id,
// 				GeneId = geneId,
// 			}).ToList();
//
// 			foreach (var gene in imageGeneSetGenes)
// 			{
// 				var exists = await Database.ImageGeneSetGenes
// 					.AnyAsync(igg => igg.ImageGeneSetId == gene.ImageGeneSetId
// 						&& igg.GeneId == gene.GeneId, cancel);
//
// 				if (!exists)
// 				{
// 					Database.ImageGeneSetGenes.Add(gene);
// 				}
// 			}
// 		}
// 		await Database.SaveChangesAsync(cancel);
//
// 	}
// }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record AddGenesToImagesCommand(
// List<AddGenesDto> AddGenesDtos
// ) : IRequest;
//
// public record AddGenesDto(List<int> GeneIds, int ImageId);