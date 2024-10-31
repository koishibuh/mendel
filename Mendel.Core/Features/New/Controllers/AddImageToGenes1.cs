// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Diagnostics;
// namespace Mendel.Core.Features.Species.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/species")]
// public class AddImageToGenesController : ApiControllerBase
// {
// 	[HttpPost("genes/images")]
// 	public async Task<ActionResult> AddSpecies
// 		([FromBody] AddImageToGenesCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddImageToGenesHandler(
// MendelDbContext Database
// ) : IRequestHandler<AddImageToGenesCommand>
// {
// 	public async Task Handle(AddImageToGenesCommand command, CancellationToken cancel)
// 	{
// 		var speciesDetail = await Database.Species
// 			.Include(x => x.Genotypes)
// 			.ThenInclude(x => x.Portraits)
// 			.FirstOrDefaultAsync(x => x.Id == command.SpeciesId);
//
// 		// var speciesDetail = await Database.Species
// 		// 	.Where(x => x.Id == command.SpeciesId)
// 		// 	.SelectMany(x => x.Genotypes)
// 		// 	.SelectMany(g => g.Portraits)
// 		// 	.ToListAsync(cancel);
//
// 		foreach (var id in command.Ids)
// 		{
// 			var portrait = speciesDetail.Genotypes
// 				.Where(g => id.Contains(g.Id))
// 				.SelectMany(g => g.Portraits)
// 				.GroupBy(p => p.Id)
// 				.Where(g => g.Count() == id.Count)
// 				.Select(g => g.FirstOrDefault())
// 				.FirstOrDefault();
//
// 			if (portrait is not null)
// 			{
// 				if (command.Gender == "Male")
// 				{
// 					portrait.AdultMaleUrl = command.ImageUrl;
// 				}
// 				else if (command.Gender == "Female")
// 				{
// 					portrait.AdultFemaleUrl = command.ImageUrl;
// 				}
// 				else
// 				{
// 					portrait.AdultMaleUrl = command.ImageUrl;
// 					portrait.AdultFemaleUrl = command.ImageUrl;
// 				}
// 			}
// 			else
// 			{
//
// 			}
// 		}
//
//
//
//
// 		return Task.CompletedTask;
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record AddImageToGenesCommand(
// int SpeciesId,
// List<List<int>> Ids,
// string ImageUrl,
// string Gender
// ) : IRequest;