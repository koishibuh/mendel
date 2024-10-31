// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Genes.Controllers;
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/genes")]
// public class UpdateGenesController : ApiControllerBase
// {
// 	[HttpPatch]
// 	public async Task<ActionResult> UpdateGenes
// 		([FromBody] UpdateGenesCommand command)
// 	{
// 		await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record UpdateGenesHandler(
// MendelDbContext Database
// ) : IRequestHandler<UpdateGenesCommand>
// {
// 	public async Task Handle(UpdateGenesCommand command, CancellationToken cancel)
// 	{
// 		var genesDb = await Database.Genes
// 			.Where(x => x.SpeciesId == command.SpeciesId)
// 			.Where(x => command.Genes.Select(y => y.Id).Contains(x.Id))
// 			.ToListAsync(cancel);
//
// 		foreach (var gene in command.Genes)
// 		{
// 			var result = genesDb.First(x => x.Id == gene.Id);
// 			if (gene.Trait is not null)
// 				result.Trait = gene.Trait;
//
// 			if (gene.Alleles is not null)
// 				result.Alleles = gene.Alleles;
//
// 			if (gene.Description is not null)
// 				result.Description = gene.Description;
// 		}
//
// 		Database.UpdateRange(genesDb);
// 		await Database.SaveChangesAsync(cancel);
// 	}
// }
//
// /*════════════════════【 QUERY 】════════════════════*/
// public record GeneDto(
// string? Trait = null,
// string? Alleles = null,
// string? Description = null
// );
//
// public record UpdateGenesCommand(
// int SpeciesId,
// string SpeciesName,
// List<GeneVm> Genes
// ) : IRequest;
//
// /*══════════════════【 VALIDATOR 】══════════════════*/
// // TODO: Validate Gene Ids