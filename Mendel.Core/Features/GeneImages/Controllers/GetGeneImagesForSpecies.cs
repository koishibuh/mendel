using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.GeneImages.Controllers;

/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/images")]
public class GetGeneImagesForSpeciesController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult> GetGeneImagesForSpecies
		([FromQuery] GetGeneImagesForSpeciesQuery query)
	{
		await Mediator.Send(query);
		return Ok();
	}
}

public record GeneSetDto(int Id, List<int> GeneIds);

/*═══════════════════【 HANDLER 】═══════════════════*/
public record GetImagesForSpeciesHandler(
MendelDbContext Database
) : IRequestHandler<GetGeneImagesForSpeciesQuery>
{
	public async Task Handle
		(GetGeneImagesForSpeciesQuery query, CancellationToken cancel)
	{
		var species = await Database.Species
			.FirstOrDefaultAsync(x => x.Id == query.SpeciesId);

		var unisex = species.JuvenileUnisex && species.AdultUnisex;

		// get geneset for species
		var geneSets = await Database.GeneSets
			.Where(gs => gs.Gene.SpeciesId == query.SpeciesId)
			.GroupBy(gs => gs.SetId)
			.Select(group => new GeneSetDto
				(group.Key, group.Select(gs => gs.GeneId).ToList()))
			.ToListAsync(cancel);

		var list = new List<NewGeneSetImageVm>();

		// create objects
		foreach (var item in geneSets)
		{
			if (unisex)
			{
				if (item.GeneIds.Count > 1)
				{
					var femaleVm = new NewGeneSetImageVm
					{
						Sex = "Female",
						JuvenileImageId = null,
						JuvenileImageUrl = null,
						AdultImageId = null,
						AdultImageUrl = null
					};

					var maleVm = new NewGeneSetImageVm
					{
						Sex = "Male",
						JuvenileImageId = null,
						JuvenileImageUrl = null,
						AdultImageId = null,
						AdultImageUrl = null
					};

					list.Add(femaleVm);
					list.Add(maleVm);
				}
				else
				{
					var vm = new NewGeneSetImageVm
					{
						Sex = "Unisex",
						JuvenileImageId = null,
						JuvenileImageUrl = null,
						AdultImageId = null,
						AdultImageUrl = null
					};

					list.Add(vm);
				}
			}
			else
			{
				var unknownVm = new NewGeneSetImageVm
				{
					Sex = "Unknown",
					JuvenileImageId = null,
					JuvenileImageUrl = null,
					AdultImageId = null,
					AdultImageUrl = null
				};

				list.Add(unknownVm);
			}
		}


	}

	// var test = list;



//
// 		// Get the species group and its genes
// 		var speciesGenes = await Database.Species
// 			.Where(sg => sg.Id == query.SpeciesId)
// 			.Select(sg => new
// 			{
// 				SpeciesName = sg.Name,
// 				Genes = sg.Genes.Select(g => new GeneVm(
// 					g.Id,
// 					g.Trait,
// 					g.Alleles,
// 					g.Description
// 					)).ToList()
// 			})
// 			.FirstOrDefaultAsync(cancel);
//
// 		if (speciesGenes is null)
// 			throw new Exception("Species not found");
//
// 		var geneSets = await Database.GeneSets
// 			.Where(gs => gs.Gene.SpeciesId == query.SpeciesId)
// 			.ToListAsync(cancel);
//
// 		var imagesWithGenes = await Database.Images
// 			.Where(i => i.GeneSetImages.Any(igs => igs.GeneSet.Gene.SpeciesId == query.SpeciesId))
// 			.Select(i => new GeneSetImageDto
// 			{
// 				ImageId = i.Id,
// 				Sex = i.Sex,
// 				Age = i.Age,
// 				Url = i.Url,
// 				GeneIdSets = i.GeneSetImages
// 					.Where(gsi => gsi.GeneSet.Gene.SpeciesId == query.SpeciesId)
// 					.Select(gsi => gsi.GeneSet.Gene.Id)
// 					.Distinct()
// 					.ToList()
// 			})
// 			.ToListAsync(cancel);
//
// 		// Group geneSets by SetId
// 		var geneSetGroups = geneSets.GroupBy(gs => gs.SetId)
// 			.Select(g => new { SetId = g.Key, GeneIds = g.Select(x => x.GeneId).ToList() })
// 			.ToList();
//
// // Find missing gene sets
// 		var missingGeneSets = geneSetGroups
// 			.Where(gs => !imagesWithGenes.Any(iwg => iwg.GeneIdSets.Select(gis => gis).Contains(gs.SetId)))
// 			.ToList();
//
// // Add missing gene sets to imagesWithGenes
// 		foreach (var missingSet in missingGeneSets)
// 		{
// 			imagesWithGenes.Add(new GeneSetImageDto
// 			{
// 				ImageId = null,
// 				Sex = null,
// 				Age = null,
// 				Url = null,
// 				GeneIdSets = missingSet.GeneIds.ToList()
// 			});
// 		}
//
// 		var test = missingGeneSets;
//
// 		// var combinedImages = imagesWithGenes.GroupBy(i => new
// 		// {
// 		// 	i.Sex,
// 		// 	GeneIdSets = string.Join(";", i.GeneIdSets.Select(set => string.Join(",", set)))
// 		// }).Select(g => new ImageWithGenesVM
// 		// {
// 		// 	Sex = g.Key.Sex,
// 		// 	JuvenileImageId = g.FirstOrDefault(i => i.Age == "Juvenile")?.ImageId,
// 		// 	JuvenileUrl = g.FirstOrDefault(i => i.Age == "Juvenile")?.Url,
// 		// 	AdultImageId = g.FirstOrDefault(i => i.Age == "Adult")?.ImageId,
// 		// 	AdultUrl = g.FirstOrDefault(i => i.Age == "Adult")?.Url,
// 		// 	GeneIdSets = g.First().GeneIdSets // All items in the group have the same GeneIdSets
// 		// })
// 		// .ToList();
//
// 		// var combinedImages = imagesWithGenes
// 		// 	.GroupBy(i => new
// 		// 	{
// 		// 		i.Sex,
// 		// 		GeneIdSets = string.Join(";", i.GeneIdSets.Select(set => string.Join(",", set)))
// 		// 	})
// 		// 	.Select(g => new ImageWithGenesVM
// 		// 	{
// 		// 		Sex = g.Key.Sex,
// 		// 		JuvenileImageId = g.FirstOrDefault(i => i.Age == "Juvenile")?.ImageId,
// 		// 		JuvenileUrl = g.FirstOrDefault(i => i.Age == "Juvenile")?.Url,
// 		// 		AdultImageId = g.FirstOrDefault(i => i.Age == "Adult")?.ImageId,
// 		// 		AdultUrl = g.FirstOrDefault(i => i.Age == "Adult")?.Url,
// 		// 		GeneIdSets = g.First().GeneIdSets // All items in the group have the same GeneIdSets
// 		// 	})
// 		// 	.ToList();
// 		//
// 		// return new SpeciesImageVm(
// 		// 	query.SpeciesId,
// 		// 	speciesGenes.SpeciesName,
// 		// 	speciesGenes.Genes,
// 		// 	combinedImages
// 		// 	);

List<List<int>> GetAllPossibleGeneSets(List<GeneVm> genes)
{
	var result = new List<List<int>> { new List<int>() };
	foreach (var gene in genes)
	{
		var newSets = new List<List<int>>();
		foreach (var set in result)
		{
			newSets.Add(new List<int>(set));
			newSets.Add(new List<int>(set) { gene.Id });
		}
		result = newSets;
	}
	return result;
}
}

/*════════════════════【 QUERY 】════════════════════*/
public record GetGeneImagesForSpeciesQuery(
int SpeciesId
) : IRequest;

public class GeneSetImageDto
{
	public int? ImageId { get; set; }
	public string? Sex { get; set; }
	public string? Age { get; set; }
	public string? Url { get; set; }
	public List<int> GeneIdSets { get; set; }
}

public class NewGeneSetImageVm
{
	public string Sex { get; set; }
	public int? JuvenileImageId { get; set; }
	public string? JuvenileImageUrl { get; set; }
	public int? AdultImageId { get; set; }
	public string? AdultImageUrl { get; set; }
	public List<int> GeneIdSet { get; set; }
}

public class ImageGeneVm
{
	public int ImageId { get; set; }
	public List<int> GeneIds { get; set; }
	public string Sex { get; set; }
	public string? JuvenileUrl { get; set; }
	public string? AdultUrl { get; set; }
}

public record ImageDto(
int Id,
int GeneId,
string Url,
string Age,
string Sex
);