using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Mendel.Core.Persistence.Models;

public class GeneSet
{
	public int Id { get; set; }
	public int GeneId { get; set; }
	public Gene Gene { get; set; }

	public int SetId { get; set; }
	public Set Set { get; set; }

	public List<GeneSetImage>? GeneSetImages { get; set; }
}

/*═══════════════【 CONFIGURATION 】═══════════════*/
public class GeneSetConfiguration : IEntityTypeConfiguration<GeneSet>
{
	public void Configure(EntityTypeBuilder<GeneSet> builder)
	{
		builder.ToTable("GeneSets");

		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.Gene)
			.WithMany(p => p.GeneSets)
			.HasForeignKey(p => p.GeneId);

		builder.HasOne(p => p.Set)
			.WithMany(p => p.GeneSets)
			.HasForeignKey(p => p.SetId);
	}
}