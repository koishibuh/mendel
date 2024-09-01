using Mendel.Core.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Mendel.Core.Persistence;

public class MendelDbContext : DbContext
{
	public MendelDbContext(DbContextOptions<MendelDbContext> options) : base(options){}

	public DbSet<Creature> Creatures => Set<Creature>();
	public DbSet<Genotype> Genotypes => Set<Genotype>();
	public DbSet<Scientist> Scientists => Set<Scientist>();
	public DbSet<Species> Species => Set<Species>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MendelDbContext).Assembly);
	}
}