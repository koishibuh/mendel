// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace Mendel.Core.Persistence.Models;
//
// /*══════════════════【 ENTITY 】══════════════════*/
// public class Scientist
// {
// 	public int Id { get; set; }
// 	public string Name { get; set; } = null!;
//
// 	public List<Creature> Creatures { get; set; } = [];
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class ScientistConfiguration : IEntityTypeConfiguration<Scientist>
// {
// 	public void Configure(EntityTypeBuilder<Scientist> builder)
// 	{
// 		builder.ToTable("Scientists");
//
// 		builder.HasKey(p => p.Id);
// 		builder.Property(p => p.Id);
//
// 		builder.Property(p => p.Name);
// 	}
// }