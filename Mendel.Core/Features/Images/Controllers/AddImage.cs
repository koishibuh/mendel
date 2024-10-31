// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Mendel.Core.Services.Cloudinary;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Security.Cryptography;
// using System.Text;
// namespace Mendel.Core.Features.Images.Controllers;
//
// /// <summary>
// /// Converts image into hash, checks if already in Database.
// /// Uploads image to Cloudinary and saves to Database
// /// </summary>
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/images")]
// public class AddImageController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> AddImage
// 		([FromBody] AddImageCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddImageHandler(
// MendelDbContext Database,
// ICloudinaryService CloudinaryService
// ) : IRequestHandler<AddImageCommand>
// {
// 	public async Task Handle
// 		(AddImageCommand command, CancellationToken cancel)
// 	{
//
// 		// 1. Get the gene ID for the "Unknown" trait for the specified species
// 		var unknownGene = await Database.Genes
// 			.Where(g => g.Species.Name == command.SpeciesName && g.Trait == "Unknown")
// 			.FirstOrDefaultAsync(cancel);
//
//
// 		if (unknownGene == null)
// 			throw new Exception($"Unknown gene not found for species {command.SpeciesName}");
//
// 		foreach (var base64Image in command.Images)
// 		{
// 			var imageHash = ComputeHash(base64Image);
//
// 			var existingImage = await Database.Images
// 				.FirstOrDefaultAsync(i => i.Hash == imageHash, cancel);
//
// 			if (existingImage != null)
// 				continue;
//
//
// 			var imageUrl = await CloudinaryService.UploadImageRandomString(base64Image, command.SpeciesName);
//
// 			var newImage = new Image
// 			{
// 				Hash = imageHash,
// 				Url = imageUrl,
// 				Age = command.Age,
// 				Sex = command.Sex
// 			};
//
// 			Database.Images.Add(newImage);
// 			await Database.SaveChangesAsync(cancel);
//
// 			var imageGeneSet = new ImageGeneSet
// 			{
// 				ImageId = newImage.Id,
// 				Genes = [new ImageGeneSetGene { GeneId = unknownGene.Id }]
// 			};
//
// 			Database.ImageGeneSets.Add(imageGeneSet);
// 			await Database.SaveChangesAsync(cancel);
// 		}
//
// 		//
// 		//
// 		//
// 		// var unknownGeneId = await Database.Genes
// 		// 	.Where(g => g.Species.Name == command.SpeciesName && g.Trait == "Unknown")
// 		// 	.Select(g => g.Id)
// 		// 	.FirstOrDefaultAsync(cancel);
// 		//
// 		// foreach (var item in command.Images)
// 		// {
// 		// 	var clean = "";
// 		// 	if (item.Contains(","))
// 		// 	{
// 		// 		clean = item.Substring(item.IndexOf(",") + 1);
// 		// 	}
// 		// 	var imageBytes = Convert.FromBase64String(clean);
// 		//
// 		// 	var hash = "";
// 		// 	using (var sha256 = SHA256.Create())
// 		// 	{
// 		// 		var hashBytes = sha256.ComputeHash(imageBytes);
// 		// 		hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
// 		// 	}
// 		//
// 		// 	var imageDb = await Database.Images
// 		// 		.Include(x => x.ImageGeneSets)
// 		// 		.ThenInclude(x => x.Genes)
// 		// 		.FirstOrDefaultAsync(x => x.Hash == hash, cancel);
// 		//
// 		// 	if (imageDb is not null)
// 		// 	{
// 		// 		// throw new Exception("Image already saved");
// 		// 		// TODO: Send message that image wasn't saved?
// 		// 		continue;
// 		// 	}
// 		//
// 		// 	var imageUrl = await CloudinaryService.UploadImageRandomString
// 		// 		(item, command.SpeciesName);
// 		//
// 		// 	var image = command.CreateEntity(imageUrl, hash);
// 		//
// 		// 	Database.Update(image);
// 		//
// 		// 	var imageGeneSet = new ImageGeneSet
// 		// 	{
// 		// 		ImageId = image.Id,
// 		// 		Genes = [new() { GeneId = unknownGeneId }]
// 		// 	};
// 		//
// 		// 	Database.ImageGeneSets.Add(imageGeneSet);
// 		// }
// 		// await Database.SaveChangesAsync(cancel);
// 	}
//
// 	private string ComputeHash(string base64Image)
// 	{
// 		var clean = "";
// 		if (base64Image.Contains(","))
// 		{
// 			clean = base64Image.Substring(base64Image.IndexOf(",") + 1);
// 		}
// 		var imageBytes = Convert.FromBase64String(clean);
//
// 		using var sha256 = SHA256.Create();
// 		var hashBytes = sha256.ComputeHash(imageBytes);
// 		return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
// 	}
// }
//
// /*═══════════════════【 COMMAND 】═══════════════════*/
// public record AddImageCommand(
// string SpeciesName,
// List<string> Images,
// string Age,
// string Sex
// ) : IRequest
// {
//
// 	public Image CreateEntity(string imageUrl, string hash)
// 		=> new()
// 		{
// 			Hash = hash,
// 			Url = imageUrl,
// 			Age = Age,
// 			Sex = Sex
// 		};
// }