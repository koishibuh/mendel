// using CloudinaryDotNet;
// using MediatR;
// using Mendel.Core.Common;
// using Microsoft.AspNetCore.Mvc;
// namespace Mendel.Core.Features.Images.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/images/cloudinary")]
// public class GetImagesFromCloudinaryController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> GetImagesFromCloudinary
// 		([FromBody] GetImagesFromCloudinaryQuery query)
// 	{
// 		var result = await Mediator.Send(query);
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record GetImagesFromCloudinaryHandler(
// ICloudinary Cloudinary
// ) : IRequestHandler<GetImagesFromCloudinaryQuery, List<CloudinaryFile>>
// {
// 	public async Task<List<CloudinaryFile>> Handle
// 		(GetImagesFromCloudinaryQuery command, CancellationToken cancel)
// 	{
// 		var results = await Cloudinary.Search()
// 			.Expression($"tags:{command.Name}")
// 			.SortBy("public_id", "desc")
// 			.MaxResults(100)
// 			.ExecuteAsync();
//
// 		return results.Resources
// 			.Select(x => new CloudinaryFile(x.Url, x.PublicId))
// 			.ToList();
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GetImagesFromCloudinaryQuery(
// string Name
// ) : IRequest<List<CloudinaryFile>>;
//
// public record CloudinaryFile(string Url, string PublicId);