using FluentValidation;
using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Mendel.Core.Services.Cloudinary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.CreatureSpecies.Controllers;

/// <summary>
/// Updates species info by Name or Id
/// </summary>
/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/species")]
public class UpdateSpeciesController : ApiControllerBase
{
	[HttpPatch]
	public async Task<ActionResult> UpdateSpeciesBy
		([FromQuery] int? id, [FromQuery] string? name, [FromBody] SpeciesDto dto)
	{
		if (id.HasValue)
		{
			var command = new UpdateSpeciesCommand(dto, Id: id.Value);
			await Mediator.Send(command);
			return Ok();
		}

		if (!string.IsNullOrEmpty(name))
		{
			var command = new UpdateSpeciesCommand(dto, Name: name);
			await Mediator.Send(command);
			return Ok();
		}

		return BadRequest("Either id or name must be provided.");
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record UpdateSpeciesHandler(
MendelDbContext Database,
ICloudinaryService CloudinaryService,
IHttpClientFactory HttpClientFactory
) : IRequestHandler<UpdateSpeciesCommand>
{
	public async Task Handle(UpdateSpeciesCommand command, CancellationToken cancel)
	{
		var speciesDb = command.Id is not null
			? await Database.Species.FirstOrDefaultAsync(x => x.Id == command.Id)
			: await Database.Species.FirstOrDefaultAsync(x => x.Name == command.Name);

		if (speciesDb is null)
			throw new Exception("Species is not found");

		if (command.Dto.Name is not null)
			speciesDb.Name = command.Dto.Name;

		if (command.Dto.CapsuleImage is not null)
		{
			if (speciesDb.CapsuleImage is not null)
				await CloudinaryService.DeleteImage(speciesDb.CapsuleImage);

			speciesDb.CapsuleImage = await CloudinaryService.UploadImageRandomString
				(command.Dto.CapsuleImage, speciesDb.Name);
		}

		if (command.Dto.Event is not null)
			speciesDb.Event = command.Dto.Event;

		if (command.Dto.ReleaseDate is not null)
			speciesDb.ReleaseDate = command.Dto.ReleaseDate.Value;

		Database.Update(speciesDb);
		await Database.SaveChangesAsync(cancel);
	}
}

/*════════════════════【 QUERY 】════════════════════*/
public record SpeciesDto(
string? Name = null,
string? CapsuleImage = null,
string? Event = null,
DateTimeOffset? ReleaseDate = null
);

public record UpdateSpeciesCommand(
SpeciesDto Dto,
int? Id = null,
string? Name = null
) : IRequest
{
	public async Task<bool> IsSpeciesNameUnique(MendelDbContext database)
	{
		var result = await database.Species
			.FirstOrDefaultAsync(x => x.Name == Name);

		return result is null;
	}
}

/*══════════════════【 VALIDATOR 】══════════════════*/
public class UpdateSpeciesValidator
	: AbstractValidator<UpdateSpeciesCommand>
{
	private MendelDbContext Database { get; }

	public UpdateSpeciesValidator(MendelDbContext context)
	{
		Database = context;

		When(x => x.Dto.Name != null, () =>
		{
			RuleFor(x => x)
				.MustAsync(SpeciesNameUnique)
				.WithMessage("Species name already exists");
		});

	}

	private async Task<bool> SpeciesNameUnique
		(UpdateSpeciesCommand command, CancellationToken cancel)
		=> await command.IsSpeciesNameUnique(Database);
}