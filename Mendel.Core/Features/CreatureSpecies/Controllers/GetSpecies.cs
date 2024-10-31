using FluentValidation;
using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.CreatureSpecies.Controllers;

/// <summary>
/// Filter by Names, Ids, or gets all
/// Returns Species Id & Name
/// </summary>
/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/species")]
public class GetSpeciesController : ApiControllerBase
{
	[HttpGet]
	public async Task<ActionResult> GetSpecies
		([FromQuery] GetSpeciesQuery query)
	{
		var result = await Mediator.Send(query);
		return Ok(result);
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record GetSpeciesHandler(
MendelDbContext Database
) : IRequestHandler<GetSpeciesQuery, List<SpeciesVm>>
{
	public async Task<List<SpeciesVm>> Handle
		(GetSpeciesQuery query, CancellationToken cancel)
	{
		if (query.GetAllSpecies())
		{
			return await Database.Species
				.Select(x => new SpeciesVm(x.Id, x.Name))
				.ToListAsync(cancel);
		}

		var result = new List<SpeciesVm>();

		if (query.GetSpeciesByNames())
		{
			var species = await Database.Species
				.Where(x => query.Names!.Contains(x.Name))
				.Select(x => new SpeciesVm(x.Id, x.Name))
				.ToListAsync(cancel);

			result.AddRange(species);
		}
		if (query.GetSpeciesByIds())
		{
			var species = await Database.Species
				.Where(x => query.Ids!.Contains(x.Id))
				.Select(x => new SpeciesVm(x.Id, x.Name))
				.ToListAsync(cancel);

			result.AddRange(species);
		}

		return result.Count > 0
			? result
			: throw new Exception("No species found");
	}
}

/*════════════════════【 QUERY 】════════════════════*/
public record GetSpeciesQuery(
List<int>? Ids = null,
List<string>? Names = null
) : IRequest<List<SpeciesVm>>
{
	public bool GetAllSpecies() => Names is null && Ids is null;
	public bool GetSpeciesByNames() => Names is not null;
	public bool GetSpeciesByIds() => Ids is not null;
}

/*═════════════════【 VIEW MODELS 】═════════════════*/
public record SpeciesVm(int Id, string Name);

/*══════════════════【 VALIDATOR 】══════════════════*/
public class GetSpeciesValidator
	: AbstractValidator<GetSpeciesQuery>
{
	private MendelDbContext Database { get; }

	public GetSpeciesValidator(MendelDbContext context)
	{
		Database = context;

		When(x => x.Ids != null, () =>
		{
			RuleFor(x => x.Ids)
				.MustAsync(async (ids, cancellation) =>
				{
					var (allIdsExist, missingIds) = await ValidateAndFindMissingIds(ids);
					return allIdsExist;
				})
				.WithMessage((_, ids) =>
				{
					var (_, missingIds) = ValidateAndFindMissingIds(ids).Result;
					return $"The following IDs do not exist in the database: {string.Join(", ", missingIds)}";
				});
		});
		When(x => x.Names != null, () =>
		{
			RuleFor(x => x.Names)
				.MustAsync(async (names, cancellation) =>
				{
					var (allNamesExist, missingNames) = await ValidateAndFindMissingNames(names);
					return allNamesExist;
				})
				.WithMessage((_, names) =>
				{
					var (_, missingNames) = ValidateAndFindMissingNames(names).Result;
					return $"The following names do not exist in the database: {string.Join(", ", missingNames)}";
				});
		});

	}

	private async Task<(bool AllIdsExist, List<int> MissingIds)> ValidateAndFindMissingIds(List<int> ids)
	{
		var existingIds = await Database.Species
			.Where(e => ids.Contains(e.Id))
			.Select(e => e.Id)
			.ToListAsync();

		var missingIds = ids.Except(existingIds).ToList();

		return (missingIds.Count == 0, missingIds);
	}

	private async Task<(bool AllNamesExist, List<string> MissingNames)> ValidateAndFindMissingNames(List<string> names)
	{
		var existingNames = await Database.Species
			.Where(e => names.Contains(e.Name))
			.Select(e => e.Name)
			.ToListAsync();

		var missingNames = names.Except(existingNames).ToList();

		return (missingNames.Count == 0, missingNames);
	}
}