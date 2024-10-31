// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Features.Images.Controllers;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Mendel.Core.Services.Cloudinary;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Security.Cryptography;
// namespace Mendel.Core.Features.Cloudinary.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/cloudinary/genes")]
// public class AddCloudinaryImageController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> AddCloudinaryImage
// 		([FromBody] AddCloudinaryImageCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddCloudinaryImageHandler(
// MendelDbContext Database,
// IHttpClientFactory HttpClientFactory,
// ICloudinaryService CloudinaryService
// ) : IRequestHandler<AddCloudinaryImageCommand>
// {
// 	public async Task Handle(AddCloudinaryImageCommand command, CancellationToken cancel)
// 	{
//
// 		var unknownGene = await Database.Genes
// 			.Where(g => g.Species.Id == command.SpeciesId && g.Trait == "Unknown")
// 			.FirstOrDefaultAsync(cancel);
//
//
// 		if (unknownGene == null)
// 			throw new Exception($"Unknown gene not found for species {command.SpeciesId}");
//
// 		var pendingHash = new List<string>();
//
// 		foreach (var item in command.Images)
// 		{
//
// 			using var httpClient = HttpClientFactory.CreateClient("Default");
// 			var imageBytes = await httpClient.GetByteArrayAsync(item.Url, cancel);
//
// 			var hash = "";
// 			using (var sha256 = SHA256.Create())
// 			{
// 				var hashBytes = sha256.ComputeHash(imageBytes);
// 				hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
// 			}
//
// 			var imageDb = await Database.Images
// 				.FirstOrDefaultAsync(x => x.Hash == hash, cancel);
//
// 			if (imageDb is not null)
// 			{
// 				// delete image from cloudinary
// 				await CloudinaryService.DeleteImageByPublicId(item.PublicId);
// 				continue;
// 			}
//
// 			var alreadyPending = pendingHash.FirstOrDefault(x => x == hash);
// 			if (alreadyPending is not null)
// 			{
// 				await CloudinaryService.DeleteImageByPublicId(item.PublicId);
// 				continue;
// 			}
//
// 			var image = new Image
// 			{
// 				Hash = hash,
// 				Url = item.Url,
// 				Age = command.Age,
// 				Sex = command.Sex
// 			};
//
// 			var imageGeneSet = new ImageGeneSet
// 			{
// 				Image = image,
// 				Genes = [new ImageGeneSetGene { GeneId = unknownGene.Id }]
// 			};
//
// 			Database.Images.Add(image);
// 			Database.ImageGeneSets.Add(imageGeneSet);
//
// 			pendingHash.Add(hash);
// 		}
//
// 		await Database.SaveChangesAsync(cancel);
// 	}
// }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record AddCloudinaryImageCommand(
// int SpeciesId,
// List<CloudinaryFile> Images,
// string Age,
// string Sex
// ) : IRequest;