// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// namespace Mendel.Core.Persistence.Models;
//
// /*══════════════════【 ENTITY 】══════════════════*/
// public class Image
// {
// 	public int Id { get; set; }
// 	public string? Hash { get; set; }
// 	public string? Url { get; set; }
// 	public string? Age { get; set; }
// 	public string? Sex { get; set; }
//
// 	public List<ImageGeneSet> ImageGeneSets { get; set; } = [];
// }
//
// /*═══════════════【 CONFIGURATION 】═══════════════*/
// public class ImageConfiguration : IEntityTypeConfiguration<Image>
// {
// 	public void Configure(EntityTypeBuilder<Image> builder)
// 	{
// 		builder.ToTable("Images");
//
// 		builder.HasKey(p => p.Id);
//
// 		builder.Property(p => p.Hash);
// 		builder.Property(p => p.Url);
// 		builder.Property(p => p.Age);
// 		builder.Property(p => p.Sex);
// 	}
// }