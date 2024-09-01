using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

public class CreatureGenotype
{
	public int Id { get; set; }
	public int CreatureId { get; set; }
	public int GenotypeId { get; set; }
}


public class CreatureGenotypeConfiguration : IEntityTypeConfiguration<CreatureGenotype>
{
	public void Configure(EntityTypeBuilder<CreatureGenotype> builder)
	{
		builder.ToTable("CreatureGenotypes");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id);

		builder.Property(p => p.CreatureId);
		builder.Property(p => p.GenotypeId);
	}
}