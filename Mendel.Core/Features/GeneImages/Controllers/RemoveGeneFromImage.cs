// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.GeneImages.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/image-genes")]
// public class RemoveGeneFromImageController : ApiControllerBase
// {
// 	[HttpDelete]
// 	public async Task<ActionResult> RemoveGeneFromImage
// 		([FromBody] RemoveGeneFromImageCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record RemoveGeneFromImageHandler(
// MendelDbContext Database
// ) : IRequestHandler<RemoveGeneFromImageCommand>
// {
// 	public async Task Handle(RemoveGeneFromImageCommand command, CancellationToken cancel)
// 	{
// 		// Find the ImageGeneSet for the given image
// 		var imageGeneSet = await Database.ImageGeneSets
// 			.Include(igs => igs.Genes)
// 			.FirstOrDefaultAsync(igs => igs.ImageId == command.ImageId);
//
// 		if (imageGeneSet == null)
// 		{
// 			throw new Exception($"No gene associations found for image with ID {command.ImageId}.");
// 		}
//
// 		var removedCount = 0;
//
// 		foreach (var geneIdSet in command.GeneIds)
// 		{
// 			// Find all gene associations for this set
// 			var geneAssociations = imageGeneSet.Genes
// 				.Where(g => geneIdSet.Contains(g.GeneId))
// 				.ToList();
//
// 			foreach (var association in geneAssociations)
// 			{
// 				// Remove the gene association
// 				imageGeneSet.Genes.Remove(association);
// 				removedCount++;
// 			}
// 		}
//
// 		// If all genes were removed, you might want to remove the ImageGeneSet entirely
// 		if (!imageGeneSet.Genes.Any())
// 		{
// 			Database.ImageGeneSets.Remove(imageGeneSet);
// 		}
//
// 		// Save changes to the database
// 		await Database.SaveChangesAsync();
// 	}
// }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record RemoveGeneFromImageCommand(
// int ImageId,
// List<List<int>> GeneIds
// ) : IRequest;