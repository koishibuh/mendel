using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

public class Species
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string CapsuleImage { get; set; }

	/*══════════════════【 NAVIGATION 】══════════════════*/
	public List<Genotype> Genotypes { get; } = [];
}

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
	public void Configure(EntityTypeBuilder<Species> builder)
	{
		builder.ToTable("Species");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id);

		builder.Property(p => p.Name)
		.IsRequired();

		builder.Property(p => p.CapsuleImage);

		builder.HasMany(p => p.Genotypes)
		.WithOne(p => p.Species)
		.HasForeignKey(p => p.SpeciesId)
		.IsRequired();
	}
}