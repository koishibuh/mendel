using FluentValidation;
using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Mendel.Core.Persistence.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.Genes.Controllers;

// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/genes")]
// public class AddGenesController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> AddGenes
// 		([FromBody] AddGenesCommand command)
// 	{
// 		var result = await Mediator.Send(command);
// 		return Ok(result);
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// public record AddGenesHandler(
// MendelDbContext Database
// ) : IRequestHandler<AddGenesCommand, SpeciesGeneVm>
// {
// 	public async Task<SpeciesGeneVm> Handle(AddGenesCommand command, CancellationToken cancel)
// 	{
// 		var genes = command.CreateEntities();
// 		Database.UpdateRange(genes);
// 		await Database.SaveChangesAsync(cancel);
//
// 		var geneVms = genes.Select(x => new GeneVm(x.Id, x.Trait, x.Alleles, x.Description)).ToList();
// 		return new SpeciesGeneVm { SpeciesId = command.SpeciesId, SpeciesName = null, Genes = geneVms };
// 	}
// }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record AddGenesCommand(
// int SpeciesId,
// List<GeneDto> Genes
// ) : IRequest<SpeciesGeneVm>
// {
// 	public List<Gene> CreateEntities() =>
// 		Genes.Select(x => new Gene
// 		{
// 			Trait = x.Trait,
// 			Alleles = x.Alleles,
// 			Description = x.Description,
// 			SpeciesId = SpeciesId
// 		}).ToList();
//
//
// 	public async Task<bool> SpeciesIdExists(MendelDbContext database)
// 	{
// 		var result = await database.Species
// 			.FirstOrDefaultAsync(x => x.Id == SpeciesId);
//
// 		return result is not null;
// 	}
// }
//
// /*══════════════════【 VALIDATOR 】══════════════════*/
// public class AddGenesValidator
// 	: AbstractValidator<AddGenesCommand>
// {
// 	private MendelDbContext Database { get; }
//
// 	public AddGenesValidator(MendelDbContext context)
// 	{
// 		Database = context;
//
// 		RuleFor(x => x.SpeciesId)
// 			.NotEmpty();
//
// 		RuleFor(p => p)
// 			.MustAsync(SpeciesExists)
// 			.WithMessage("Id for species not found");
//
// 	}
//
// 	private async Task<bool> SpeciesExists
// 		(AddGenesCommand command, CancellationToken cancel)
// 		=> await command.SpeciesIdExists(Database);
// }