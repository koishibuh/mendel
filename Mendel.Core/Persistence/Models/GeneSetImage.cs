using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Mendel.Core.Persistence.Models;

// Gene Group
public class GeneSetImage
{
	public int Id { get; set; }

	public int GeneSetId { get; set;}
	public GeneSet GeneSet { get; set; }

	public int ImageId { get; set; }
	public Image Image { get; set; }
}

/*═══════════════【 CONFIGURATION 】═══════════════*/
public class CreatureConfiguration : IEntityTypeConfiguration<GeneSetImage>
{
	public void Configure(EntityTypeBuilder<GeneSetImage> builder)
	{
		builder.ToTable("ImageGeneSets");

		builder.HasKey(p => p.Id);

		builder.HasOne(p => p.GeneSet)
			.WithMany(p => p.GeneSetImages)
			.HasForeignKey(p => p.GeneSetId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(p => p.Image)
			.WithMany(p => p.GeneSetImages)
			.HasForeignKey(p => p.GeneSetId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}