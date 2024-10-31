// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Mendel.Core.Persistence.Models;
//
// // Junction Entity
// /*══════════════════【 ENTITY 】══════════════════*/
// public class CreatureGene
// {
// 	public int CreatureId { get; set; }
// 	public Creature Creature { get; set; }
//
// 	public int GeneId { get; set; }
// 	public Gene Gene { get; set; }
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class CreatureGenotypeConfiguration : IEntityTypeConfiguration<CreatureGene>
// {
// 	public void Configure(EntityTypeBuilder<CreatureGene> builder)
// 	{
// 		builder.ToTable("CreatureGenes");
//
// 		builder.HasKey(p => new {p.CreatureId, p.GeneId});
//
// 		builder.HasOne(p => p.Creature)
// 			.WithMany(p => p.CreatureGenes)
// 			.HasForeignKey(p => p.CreatureId);
//
// 		builder.HasOne(p => p.Gene)
// 			.WithMany(p => p.CreatureGenes)
// 			.HasForeignKey(p => p.GeneId);
// 	}
// }