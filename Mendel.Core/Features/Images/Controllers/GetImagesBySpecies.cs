// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Images.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/images/test")]
// public class GetImagesBySpeciesController : ApiControllerBase
// {
// 	[HttpGet]
// 	public async Task<ActionResult> GetImagesBySpecies
// 		([FromQuery] GetImagesQuery query)
// 	{
// 		var result = await Mediator.Send(query);
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record UpdateSpeciesHandler(
// MendelDbContext Database
// ) : IRequestHandler<GetImagesQuery, IDto>
// {
// 	public async Task<IDto> Handle(GetImagesQuery command, CancellationToken cancel)
// 	{
// 		var images = await Database.Images
// 			.Where(i => i.ImageGeneSets
// 					.Any(igs => igs.Genes
// 						.Any(g => g.Gene.SpeciesId == command.SpeciesId))
// 				&& i.Age == command.Age)
// 			.Select(i => new ImageUrlVm
// 				(i.Id, i.Url))
// 			// (i.Id, i.Url, i.ImageGeneSets.Any(igs => igs.Genes.Any())))
// 			.ToListAsync(cancel);
//
// 		return new IDto(command.SpeciesId, images);
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GetImagesQuery(
// int SpeciesId,
// string Age
// ) : IRequest<IDto>;
//
// public record IDto(
// int SpeciesId,
// List<ImageUrlVm> Images
// );
//
// public record ImageUrlVm(
// int ImageId,
// string Url
// );
//
public record SpeciesImageVm(
int SpeciesId,
string SpeciesName,
List<GeneVm> Genes,
List<ImageWithGenesVM> Images
);

public record GeneVm(
int Id,
string Trait,
string Alleles,
string Description
);

public class ImageWithGenesVM
{
	public string Sex { get; set; }
	public int? JuvenileImageId { get; set; }
	public string JuvenileUrl { get; set; }
	public int? AdultImageId { get; set; }
	public string AdultUrl { get; set; }
	public List<List<int>> GeneIdSets { get; set; }
}