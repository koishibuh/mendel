using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Services.FinalOutpost;
using Mendel.Core.Services.FinalOutpost.Responses;
using Microsoft.AspNetCore.Mvc;
namespace Mendel.Core.Features.Checklist.Controllers;

/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/checklist")]
public class GetCreatureListBySpeciesController : ApiControllerBase
{
	[HttpPost]
	public async Task<ActionResult> ImportGrowingCreatures
		([FromBody] GetCreatureListBySpecies query)
	{
		var result = await Mediator.Send(query);
		return Ok(result);
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record GetUserCreaturesBySpeciesHandler(
IFinalOutpostService FinalOutpostService
) : IRequestHandler<GetCreatureListBySpecies, List<ChecklistCreatureVm>>
{
	public async Task<List<ChecklistCreatureVm>> Handle(GetCreatureListBySpecies query, CancellationToken cancel)
	{
		// query api for species
		var dto = query.CreateDto();
		var result = await FinalOutpostService.GetCreatureListBySpecies(dto);

		var vm = result.CreateVm();
		return vm;
	}
}

/*════════════════════【 QUERY 】════════════════════*/
public record GetCreatureListBySpecies(
string username,
string species
) : IRequest<List<ChecklistCreatureVm>>
{
	public UserSpeciesDto CreateDto() => new(username, species.Replace(" ", "+"));
}

public record UserSpeciesDto(string username, string species);

public record ChecklistCreatureVm(
string Code,
string Name,
int GrowthLevel,
bool Stunted,
string SpeciesName,
List<GenotypeDto> Genetics,
string Gender
);

public record GenotypeDto(string Trait, string Gene);