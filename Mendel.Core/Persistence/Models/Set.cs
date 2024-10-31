using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Mendel.Core.Persistence.Models;

public class Set
{
	public int Id { get; set; }
	public List<GeneSet> GeneSets { get; set; }
}

/*═══════════════【 CONFIGURATION 】═══════════════*/
public class SetConfiguration : IEntityTypeConfiguration<Set>
{
	public void Configure(EntityTypeBuilder<Set> builder)
	{
		builder.ToTable("Sets");

		builder.HasKey(p => p.Id);
	}
}