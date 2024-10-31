using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Mendel.Core.Persistence.Models;

// // Intermediate Entity, Set of Genes Associated with Image
// public class ImageGeneSet
// {
// 	public int Id { get; set; }
// 	public int ImageId { get; set; }
// 	public Image Image { get; set; }
//
// 	public List<ImageGeneSetGene> Genes { get; set; }
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class ImageGeneSetConfiguration : IEntityTypeConfiguration<ImageGeneSet>
// {
// 	public void Configure(EntityTypeBuilder<ImageGeneSet> builder)
// 	{
// 		builder.ToTable("ImageGeneSets");
//
// 		builder.HasKey(p => p.Id);
//
// 		builder.HasOne(p => p.Image)
// 			.WithMany(p => p.ImageGeneSets)
// 			.HasForeignKey(p => p.ImageId)
// 			.OnDelete(DeleteBehavior.Cascade);
// 	}
// }