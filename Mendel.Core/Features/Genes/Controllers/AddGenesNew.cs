// using FluentValidation;
// using MediatR;
// using Mendel.Core.Common;
// using Mendel.Core.Persistence;
// using Mendel.Core.Persistence.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// namespace Mendel.Core.Features.Genes.Controllers;
//
//
// /*══════════════════【 CONTROLLER 】══════════════════*/
// [Route("api/genes/speciestest")]
// public class AddGenesController : ApiControllerBase
// {
// 	[HttpPost]
// 	public async Task<ActionResult> AddGenes
// 		([FromBody] AddGenesCommand dto)
// 	{
//
// 		var test = dto;
// 		// using var reader = new StreamReader(dto.GeneJson);
// 		// var jsonText = await reader.ReadToEndAsync();
// 		//
// 		// var command = new AddGenesCommand(dto.SpeciesId, jsonText);
// 		// var result = await Mediator.Send(command);
// 		return Ok();
// 	}
// }
//
// /*═══════════════════【 HANDLER 】═══════════════════*/
// // public record AddGenesHandler(
// // MendelDbContext Database
// // ) : IRequestHandler<AddGenesCommand, SpeciesGeneVm>
// // {
// // 	public async Task<SpeciesGeneVm> Handle(AddGenesCommand command, CancellationToken cancel)
// // 	{
// //
// // 	}
// // }
//
// /*════════════════════【 COMMAND 】════════════════════*/
// public record NewGeneDto(string Trait, List<DetailsDto> Details);
//
// public record DetailsDto(string Alleles, string Description);
//
// public record AddGenesCommand(
// int SpeciesId,
// List<NewGeneDto> GeneJson
// ) : IRequest<SpeciesGeneVm>
// {
//
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
// // /*══════════════════【 VALIDATOR 】══════════════════*/
// // public class AddGenesValidator
// // 	: AbstractValidator<AddGenesCommand>
// // {
// // 	private MendelDbContext Database { get; }
// //
// // 	public AddGenesValidator(MendelDbContext context)
// // 	{
// // 		Database = context;
// //
// // 		RuleFor(x => x.SpeciesId)
// // 			.NotEmpty();
// //
// // 		RuleFor(p => p)
// // 			.MustAsync(SpeciesExists)
// // 			.WithMessage("Id for species not found");
// //
// // 	}
// //
// // 	private async Task<bool> SpeciesExists
// // 		(AddGenesCommand command, CancellationToken cancel)
// // 		=> await command.SpeciesIdExists(Database);
// // }