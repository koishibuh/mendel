using Mendel.Core.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Mendel.Core.Persistence;

public class MendelDbContext(DbContextOptions<MendelDbContext> options) : DbContext(options)
{
	public DbSet<Gene> Genes => Set<Gene>();
	public DbSet<Set> Sets => Set<Set>();
	public DbSet<Image> Images => Set<Image>();
	public DbSet<Species> Species => Set<Species>();
	public DbSet<GeneSet> GeneSets => Set<GeneSet>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MendelDbContext).Assembly);
	}
}