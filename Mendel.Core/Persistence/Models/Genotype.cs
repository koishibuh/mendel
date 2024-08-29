using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

public class Genotype
{
	public int Id { get; set; }
	public int SpeciesId { get; set; }
	public string Trait { get; set; } = null!;
	public string Alleles { get; set; } = null!;
	public string Description { get; set; } = string.Empty;

	/*══════════════════【 NAVIGATION 】══════════════════*/

	public Species Species { get; set; } = null!;
	public List<Creature> Creatures { get; set; } = [];
}

public class GenotypeConfiguration : IEntityTypeConfiguration<Genotype>
{
	public void Configure(EntityTypeBuilder<Genotype> builder)
	{
		builder.ToTable("Genotypes");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id);

		builder.Property(p => p.SpeciesId);
		builder.Property(p => p.Trait);
		builder.Property(p => p.Alleles);
		builder.Property(p => p.Description);
	}
}