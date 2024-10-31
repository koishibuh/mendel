// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.GeneImages.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/genes")]
// public class AddUnknownGeneToImageController : ApiControllerBase
// {
// 	[HttpPost("unknown")]
// 	public async Task<ActionResult> AddGenes
// 		([FromBody] AddUnknownGeneToImageCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddUnknownGeneToImageHandler(
// MendelDbContext Database
// ) : IRequestHandler<AddUnknownGeneToImageCommand>
// {
// 	public async Task Handle(AddUnknownGeneToImageCommand command, CancellationToken cancel)
// 	{
// 		var existingImageGeneSets = await Database.ImageGeneSets
// 			.Where(igs => command.ImageIds.Contains(igs.ImageId))
// 			.Include(igs => igs.Genes)
// 			.ToListAsync();
//
// 		foreach (var imageId in command.ImageIds)
// 		{
// 			// Check if an ImageGeneSet already exists for this image
// 			var imageGeneSet = existingImageGeneSets.FirstOrDefault(igs => igs.ImageId == imageId);
//
// 			if (imageGeneSet == null)
// 			{
// 				// If not, create a new ImageGeneSet
// 				imageGeneSet = new ImageGeneSet
// 				{
// 					ImageId = imageId,
// 					Genes = new List<ImageGeneSetGene>()  // Initialize the Genes collection
// 				};
// 				Database.ImageGeneSets.Add(imageGeneSet);
// 			}
//
// 			// Check if the gene is already associated with this ImageGeneSet
// 			if (!imageGeneSet.Genes.Any(g => g.GeneId == command.GeneId))
// 			{
// 				// If not, create the association
// 				imageGeneSet.Genes.Add(new ImageGeneSetGene { GeneId = command.GeneId });
// 			}
// 		}
//
// 		// Save changes to the database
// 		await Database.SaveChangesAsync(cancel);
// 	}
// }
//
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record AddUnknownGeneToImageCommand(
// int GeneId,
// List<int> ImageIds
// ) : IRequest;