// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Mendel.Core.Services.Cloudinary;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Security.Cryptography;
// namespace Mendel.Core.Features.New.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/species")]
// public class AddImageToGenesController : ApiControllerBase
// {
// 	[HttpPost("images/genes")]
// 	public async Task<ActionResult> AddImageToGenes
// 		([FromBody] AddImageToGenesCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddImageToGenesHandler(
// MendelDbContext Database,
// ICloudinaryService CloudinaryService,
// IHttpClientFactory HttpClientFactory
// ) : IRequestHandler<AddImageToGenesCommand>
// {
// 	public async Task Handle(AddImageToGenesCommand command, CancellationToken cancel)
// 	{
// 		using var httpClient = HttpClientFactory.CreateClient("Default");
// 		var imageBytes = await httpClient.GetByteArrayAsync(command.Image, cancel);
//
// 		// var imageBytes = command.ConvertToByte();
//
// 		var hash = "";
// 		using (var sha256 = SHA256.Create())
// 		{
// 			var hashBytes = sha256.ComputeHash(imageBytes);
// 			hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
// 		}
//
// 		var imageDb = await Database.Images
// 			.Include(x => x.ImageGeneSets)
// 			.ThenInclude(x => x.Genes)
// 			.FirstOrDefaultAsync(x => x.Hash == hash, cancel);
//
// 		if (imageDb is null)
// 		{
// 			var imageUrl = await CloudinaryService.UploadImageRandomString
// 				(command.Image, command.SpeciesName);
//
// 			var imageGene = command.CreateEntity(imageUrl, hash);
// 			Database.Images.Add(imageGene);
// 		}
// 		else
// 		{
// 			foreach (var geneIdSet in command.Ids)
// 			{
// 				var sortedGeneIdSet = geneIdSet.OrderBy(id => id).ToList();
// 				var setExists = imageDb.ImageGeneSets.Any(i =>
// 					i.Genes.Select(g => g.GeneId).OrderBy(id => id)
// 						.SequenceEqual(sortedGeneIdSet));
//
// 				if (setExists) continue;
//
// 				var newImageGeneSet = new ImageGeneSet
// 				{
// 					Image = imageDb,
// 					Genes = sortedGeneIdSet
// 						.Select(geneId => new ImageGeneSetGene
// 							{ GeneId = geneId })
// 						.ToList()
// 				};
//
// 				imageDb.ImageGeneSets.Add(newImageGeneSet);
// 			}
//
// 			if (imageDb.ImageGeneSets.Any(igs => igs.Id == 0))
// 			{
// 				Database.Update(imageDb);
// 			}
// 		}
//
// 		await Database.SaveChangesAsync(cancel);
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record AddImageToGenesCommand(
// string SpeciesName,
// List<List<int>> Ids,
// string Image,
// string Age,
// string Sex
// ) : IRequest
// {
// 	public byte[] ConvertToByte() => Convert.FromBase64String(Image);
//
// 	public Image CreateEntity(string imageUrl, string hash) =>
// 		new()
// 		{
// 			Hash = hash,
// 			Url = imageUrl,
// 			Age = Age,
// 			Sex = Sex,
// 			ImageGeneSets = Ids.Select(set => new ImageGeneSet
// 			{
// 				Genes = set.Select(geneId => new ImageGeneSetGene
// 				{
// 					GeneId = geneId
// 				}).ToList()
// 			}).ToList()
// 		};
// };