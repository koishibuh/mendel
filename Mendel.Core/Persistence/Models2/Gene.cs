// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Mendel.Core.Persistence.Models;
//
// /*══════════════════【 ENTITY 】══════════════════*/
// public class Gene
// {
// 	public int Id { get; set; }
// 	public string Trait { get; set; } = null!;
// 	public string Alleles { get; set; } = null!;
// 	public string Description { get; set; } = string.Empty;
//
// 	public int SpeciesId { get; set; }
// 	public Species Species { get; set; } = null!;
//
// 	public List<ImageGeneSetGene> ImageGeneSetGenes { get; set; }
// 	public List<CreatureGene> CreatureGenes { get; set; }
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class GenotypeConfiguration : IEntityTypeConfiguration<Gene>
// {
// 	public void Configure(EntityTypeBuilder<Gene> builder)
// 	{
// 		builder.ToTable("Genes");
//
// 		builder.HasKey(p => p.Id);
//
// 		builder.Property(p => p.SpeciesId);
// 		builder.Property(p => p.Trait);
// 		builder.Property(p => p.Alleles);
// 		builder.Property(p => p.Description);
//
// 		builder.HasOne(p => p.Species)
// 			.WithMany(p => p.Genes)
// 			.HasForeignKey(p => p.SpeciesId)
// 			.OnDelete(DeleteBehavior.Cascade);
// 	}
// }