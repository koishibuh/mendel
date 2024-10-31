using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mendel.Core.Persistence.Models;

// /*══════════════════【 ENTITY 】══════════════════*/
// public class Creature
// {
// 	public int Id { get; set; }
// 	public string Code { get; set; } = string.Empty;
// 	public string ImageUrl { get; set; } = string.Empty;
// 	public DateTimeOffset AcquiredTimestamp { get; set; }
// 	public int Age { get; set; }
// 	public bool IsStunted { get; set; }
// 	public string Sex { get; set; } = string.Empty;
//
// 	public int ScientistId { get; set; }
// 	public Scientist Scientist { get; set; } = null!;
//
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class CreatureConfiguration : IEntityTypeConfiguration<Creature>
// {
// 	public void Configure(EntityTypeBuilder<Creature> builder)
// 	{
// 		builder.ToTable("Creatures");
//
// 		builder.HasKey(p => p.Id);
// 		builder.Property(p => p.Id);
//
// 		builder.Property(p => p.Code);
// 		builder.Property(p => p.ImageUrl);
// 		builder.Property(p => p.AcquiredTimestamp);
// 		builder.Property(p => p.Age);
// 		builder.Property(p => p.IsStunted);
// 		builder.Property(p => p.Sex);
//
// 		builder.HasOne(p => p.Scientist)
// 			.WithMany(p => p.Creatures)
// 			.HasForeignKey(p => p.ScientistId)
// 			.OnDelete(DeleteBehavior.Restrict);
// 	}
// }