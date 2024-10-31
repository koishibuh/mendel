// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Mendel.Core.Persistence.Models;
//
// /*══════════════════【 ENTITY 】══════════════════*/
// public class Species
// {
// 	public int Id { get; set; }
// 	public string Name { get; set; }
// 	public string? CapsuleImage { get; set; }
// 	public string? Event { get; set; }
// 	public DateTimeOffset ReleaseDate { get; set; }
//
// 	public List<Gene> Genes { get; set;  } = [];
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
// {
// 	public void Configure(EntityTypeBuilder<Species> builder)
// 	{
// 		builder.ToTable("Species");
//
// 		builder.HasKey(p => p.Id);
//
// 		builder.Property(p => p.Name)
// 		.IsRequired();
//
// 		builder.Property(p => p.CapsuleImage);
// 		builder.Property(p => p.Event);
// 		builder.Property(p => p.ReleaseDate);
// 	}
// }