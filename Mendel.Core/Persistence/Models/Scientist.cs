using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

public class Scientist
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;

	/*══════════════════【 NAVIGATION 】══════════════════*/
	public List<Creature> Creatures { get; set; } = [];
}

public class ScientistConfiguration : IEntityTypeConfiguration<Scientist>
{
	public void Configure(EntityTypeBuilder<Scientist> builder)
	{
		builder.ToTable("Scientists");

		builder.HasKey(p => p.Id);
		builder.Property(p => p.Id);

		builder.Property(p => p.Name);

		builder.HasMany(p => p.Creatures)
		.WithOne(p => p.Scientist)
		.HasForeignKey(p => p.ScientistId)
		.IsRequired();
	}
}