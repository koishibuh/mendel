using FluentValidation;
using MediatR;
using Mendel.Core.Common;
using Mendel.Core.Persistence;
using Mendel.Core.Persistence.Models;
using Mendel.Core.Services.Cloudinary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Mendel.Core.Features.CreatureSpecies.Controllers;

/*══════════════════【 CONTROLLER 】══════════════════*/
[Route("api/species")]
public class AddSpeciesController : ApiControllerBase
{
	[HttpPost]
	public async Task<ActionResult> AddSpecies
		([FromBody] AddSpeciesCommand command)
	{
		var result = await Mediator.Send(command);
		return Ok(result);
	}
}

/*═══════════════════【 HANDLER 】═══════════════════*/
public record AddSpeciesHandler(
MendelDbContext Database,
ICloudinaryService CloudinaryService,
IHttpClientFactory HttpClientFactory
) : IRequestHandler<AddSpeciesCommand, int>
{
	public async Task<int> Handle(AddSpeciesCommand command, CancellationToken cancel)
	{
		if (command.CapsuleImage is not null)
		{
			// using var httpClient = HttpClientFactory.CreateClient("Default");
			// var imageBytes = await httpClient.GetByteArrayAsync(command.CapsuleImage, cancel);

			command.CapsuleImage = await CloudinaryService.UploadImageRandomString
				(command.CapsuleImage, command.Name);
		}

		var species = command.CreateEntity();

		var gene = new Gene
		{
			Trait = "Unknown",
			Alleles = "Unknown",
			Description = "Unknown",
			Species = species
		};

		Database.Update(species);
		Database.Update(gene);
		await Database.SaveChangesAsync(cancel);

		return species.Id;
	}
}

/*════════════════════【 QUERY 】════════════════════*/
public record AddSpeciesCommand(
string Name,
string? CapsuleImage,
string? Event,
DateTimeOffset? ReleaseDate
) : IRequest<int>
{
	public string CapsuleImage { get; set; } = CapsuleImage;

	public byte[] ConvertToByte() => Convert.FromBase64String(CapsuleImage);

	public Species CreateEntity() =>
		new()
		{
			Name = Name,
			CapsuleImage = CapsuleImage,
			Event = Event,
			ReleaseDate = ReleaseDate
				?? new DateTimeOffset(1990, 1, 1, 0, 0, 0, TimeSpan.Zero),
		};

	public async Task<bool> IsSpeciesNameUnique(MendelDbContext database)
	{
		var result = await database.Species
			.FirstOrDefaultAsync(x => x.Name == Name);

		return result is null;
	}
}

/*══════════════════【 VALIDATOR 】══════════════════*/
public class AddSpeciesValidator
	: AbstractValidator<AddSpeciesCommand>
{
	private MendelDbContext Database { get; }

	public AddSpeciesValidator(MendelDbContext context)
	{
		Database = context;

		RuleFor(x => x.Name)
			.NotEmpty();

		RuleFor(p => p)
			.MustAsync(SpeciesNameUnique)
			.WithMessage("Species name already exists");

	}

	private async Task<bool> SpeciesNameUnique
		(AddSpeciesCommand command, CancellationToken cancel)
		=> await command.IsSpeciesNameUnique(Database);
}