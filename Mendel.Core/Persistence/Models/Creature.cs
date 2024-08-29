using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

public class Creature
{
	public int Id { get; set; }
	public string Code { get; set; } = string.Empty;
	public string ImageUrl { get; set; } = string.Empty;
	public DateTimeOffset AcquiredTimestamp { get; set; }
	public int GrowthStage { get; set; }
	public bool IsStunted { get; set; }
	public string Gender { get; set; } = string.Empty;

	public int ScientistId { get; set; }

	/*══════════════════【 NAVIGATION 】══════════════════*/

	public Scientist Scientist { get; set; } = null!;
	public List<Genotype> Genotypes { get; set; } = [];
}

public class CreatureConfiguration : IEntityTypeConfiguration<Creature>
{
	public void Configure(EntityTypeBuilder<Creature> builder)
	{
		builder.ToTable("Creatures");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id);

		builder.Property(p => p.Code);
		builder.Property(p => p.ImageUrl);
		builder.Property(p => p.AcquiredTimestamp);
		builder.Property(p => p.GrowthStage);
		builder.Property(p => p.IsStunted);
		builder.Property(p => p.Gender);

		builder.HasMany(p => p.Genotypes)
		.WithMany(p => p.Creatures)
		.UsingEntity<CreatureGenotype>();
	}
}