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
// [Route("api/image")]
// public class UploadUrlCloudinaryController : ApiControllerBase
// {
// 	[HttpPost("cloud")]
// 	public async Task<ActionResult> UploadImageCloudinary
// 		([FromBody] UploadUrlCloudinaryCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record UploadUrlCloudinaryHandler(
// MendelDbContext Database,
// ICloudinaryService CloudinaryService,
// IHttpClientFactory HttpClientFactory
// ) : IRequestHandler<UploadUrlCloudinaryCommand>
// {
// 	public async Task Handle(UploadUrlCloudinaryCommand command, CancellationToken cancel)
// 	{
// 		using var httpClient = HttpClientFactory.CreateClient("Default");
// 		var imageBytes = await httpClient.GetByteArrayAsync(command.ImageUrl, cancel);
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
// 		if (imageDb is not null)
// 		{
// 			throw new Exception("Image already saved");
// 		}
//
// 		var imageUrl = await CloudinaryService.UploadImageRandomString
// 			(command.ImageUrl, command.SpeciesName);
//
// 		var image = command.CreateEntity(imageUrl, hash);
//
// 		Database.Update(image);
// 		await Database.SaveChangesAsync(cancel);
// 	}
// }
//
// /*═══════════════════【 COMMAND 】═══════════════════*/
// public record UploadUrlCloudinaryCommand(
// string SpeciesName,
// string ImageUrl,
// string Age,
// string Sex
// ) : IRequest
// {
// 	public Image CreateEntity(string imageUrl, string hash)
// 		=> new()
// 		{
// 			Hash = hash,
// 			Url = imageUrl,
// 			Age = Age,
// 			Sex = Sex
// 		};
// }