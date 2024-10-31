// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// namespace Mendel.Core.Persistence.Models;
//
// // Junction Entity, represents many-to-many relationship between ImageGeneSet & Gene
// public class ImageGeneSetGene
// {
// 	public int ImageGeneSetId { get; set; }
// 	public ImageGeneSet ImageGeneSet { get; set; }
//
// 	public int GeneId { get; set; }
// 	public Gene Gene { get; set; }
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class ImageGeneSetGeneConfiguration : IEntityTypeConfiguration<ImageGeneSetGene>
// {
// 	public void Configure(EntityTypeBuilder<ImageGeneSetGene> builder)
// 	{
// 		builder.ToTable("ImageGeneSetGene");
//
// 		builder.HasKey(p => new { p.ImageGeneSetId, p.GeneId });
// 		builder.HasOne(p => p.ImageGeneSet)
// 			.WithMany(p => p.Genes)
// 			.HasForeignKey(p => p.ImageGeneSetId);
//
// 		builder.HasOne(p => p.Gene)
// 			.WithMany(p => p.ImageGeneSetGenes)
// 			.HasForeignKey(p => p.GeneId);
// 	}
// }