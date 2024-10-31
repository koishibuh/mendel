// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Enums;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Checklist.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/species")]
// public class GetEventSpeciesController : ApiControllerBase
// {
// 	[HttpGet("event")]
// 	public async Task<ActionResult> GetEventSpecies()
// 	{
// 		var result = await Mediator.Send(new GetEventSpeciesQuery());
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record GetEventSpeciesHandler(
// MendelDbContext Database
// ) : IRequestHandler<GetEventSpeciesQuery, List<SpeciesVm2>>
// {
// 	public async Task<List<SpeciesVm2>> Handle(GetEventSpeciesQuery query, CancellationToken cancel)
// 	{
// 		var species = await Database.Species
// 			.Where(x => x.Name == "Arba Brakumo")
// 			// .Where(x =>
// 			// 	x.Event == EventType.Halloween &&
// 			// 	x.Event == EventType.WinterSolstice)
// 			.Select(x => new SpeciesVm2(
// 					x.Name,
// 					x.Event,
// 					x.CapsuleImage,
// 					x.Genotypes.Select(x => new GenotypeVm(x.Id, x.Trait, x.Alleles, x.Description)).ToList(),
// 					x.Genotypes
// 						.SelectMany(x => x.Portraits)
// 						.Select(
// 							z => new GenotypeVm2(
// 								z.Genotypes.Select(y => y.Id).ToList(),
// 								z.JuvenileUrl,
// 								z.AdultUrl,
// 								new List<CreatureVm>()))
// 						.ToList()
// 					)
// 				).ToListAsync(cancel);
//
// 		// var species = await Database.Species
// 		// 	.Where(x => x.Name == "Arba Brakumo")
// 		// 	// .Where(x =>
// 		// 	// 	x.Event == EventType.Halloween &&
// 		// 	// 	x.Event == EventType.WinterSolstice)
// 		// 	.Select(x => new SpeciesVm2(
// 		// 			x.Name,
// 		// 			x.Event,
// 		// 			x.CapsuleImage,
// 		// 			x.Genotypes
// 		// 				.SelectMany(x => x.Portraits)
// 		// 				.Select(
// 		// 					z => new GenotypeVm2(
// 		// 						z.Genotypes.Select(y => new GenotypeVm(y.Id, y.Trait, y.Alleles, y.Description)).ToList(),
// 		// 						z.JuvenileUrl,
// 		// 						z.AdultUrl,
// 		// 						new List<CreatureVm>()))
// 		// 				.ToList()
// 		// 			)
// 		// 		).ToListAsync(cancel);
//
// 		// add creatures where the Id matches to lists
//
//
// 		var creatureList = new List<CreatureVm2>();
// 		creatureList.Add(new CreatureVm2("ABCDE", "Unknown", 3, false, "female", [21, 24, 27]));
// 		creatureList.Add(new CreatureVm2("HIJKL", "Unknown", 3, false, "male", [21, 24, 29]));
// 		// creatures.Add(new CreatureVm2("MNOPQ", "Unknown", 2, false, "male", [1]));
//
// 		// foreach (var item in species[0].Genes)
// 		// {
// 		//
// 		// }
//
// 		foreach (var creature in creatureList)
// 		{
// 			foreach (var item in species[0].Genes)
// 			{
// 				var ids = item.Genes.Select(x => x.Id).ToList();
// 				if (!creature.GeneIds.Except(ids).Any())
// 				{
// 					item.Creatures.Add(new CreatureVm(creature.Code, creature.Name, creature.GrowthLevel, creature.Gender, creature.Stunted));
// 				}
// 			}
// 		}
//
//
//
// 		return species;
//
// 		// var species = await Database.Species
// 		// 	.Where(x => x.Name == "Arba Brakumo")
// 		// 	// .Where(x =>
// 		// 	// 	x.Event == EventType.Halloween &&
// 		// 	// 	x.Event == EventType.WinterSolstice)
// 		// 	.Select(x => new SpeciesVm(
// 		// 		x.Name,
// 		// 		x.Event,
// 		// 		x.CapsuleImage,
// 		// 		x.Genotypes
// 		// 			.Select(y =>
// 		// 				new GenotypeVm(y.Id, y.Trait, y.Alleles, y.Description))
// 		// 			.ToList(),
// 		// 		x.Genotypes
// 		// 			.SelectMany(x => x.Portraits)
// 		// 			.Select(
// 		// 			z => new PortraitVm(
// 		// 				z.JuvenileUrl,
// 		// 				z.AdultUrl,
// 		// 				z.Sex,
// 		// 				z.Genotypes
// 		// 					.Select(x => x.Id)
// 		// 					.ToList()))
// 		// 			.ToList()))
// 		// 	.ToListAsync();
// 		//
// 		// return species;
//
//
// 		// Get list of species from Database
// 		// var species = await Database.Species
// 		// 	.Include(x => x.Genotypes)
// 		// 	.Select(x => new SpeciesVm(x.Name, x.Event, new GenotypeVm.ToList(), )
// 		// 		).ToList();
//
// 		// var genes = await Database.Genotypes.Include(x => x.Species).Where(x => x.Species.Name == query.)
//
// 		// var species = await Database.Species
// 		// 	.Include(x => x.Genotypes)
// 		// 	.ThenInclude(x => x.Portraits)
// 		// 	.Where(x =>
// 		// 		x.Event == EventType.Halloween &&
// 		// 		x.Event == EventType.WinterSolstice)
// 		// 	.ToListAsync(cancel);
// 		//
// 		// foreach (var specie in species)
// 		// {
// 		// 	var genotypes = specie.Genotypes
// 		// 		.Select(x => new GenotypeVm(x.Id, x.Trait, x.Alleles, x.Description))
// 		// 		.ToList();
// 		//
// 		// 	var portraits = specie.Genotypes.Select(x => x.Portraits);
// 		//
// 		//
// 		// 	var test = new SpeciesVm(
// 		// 		specie.Name, specie.Event, specie.CapsuleImage, new List<GenotypeVm>(x => new GenotypeVm)
// 		// }
//
// 		// Create VM
//
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GetEventSpeciesQuery : IRequest<List<SpeciesVm2>>;
//
// public record CreatureVm2(
// string Code,
// string Name,
// int GrowthLevel,
// bool Stunted,
// string Gender,
// List<int> GeneIds
// );
//
// public record SpeciesVm(
// string SpeciesName,
// string Event,
// string CapsuleUrl,
// List<GenotypeVm> Genes,
// List<PortraitVm> Portraits
// );
//
// public record PortraitVm(
// string Juvenile,
// string Adult,
// string Gender,
// List<int> GeneIds
// );
//
// public record GenotypeVm(
// int Id,
// string Trait,
// string Alleles,
// string Description
// );
//
// public record SpeciesVm2(
// string SpeciesName,
// string Event,
// string CapsuleUrl,
// List<GenotypeVm> Genes,
// List<GenotypeVm2> GeneCreatures
// );
//
// public record GenotypeVm2(
// List<int> GeneIds,
// string Juvenile,
// string AdultMale,
// string AdultFemale,
// List<CreatureVm> Creatures
// );
//
// public record CreatureVm(
// string Code,
// string Name,
// int GrowthLevel,
// string Gender,
// bool Stunted
// );