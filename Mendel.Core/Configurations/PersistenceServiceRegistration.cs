using Mendel.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mendel.Core.Configurations;

public static class PersistenceServiceRegistration
{
	public static IServiceCollection AddPersistenceService
	(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString("MendelConnectionString");
		var serverVersion = new MySqlServerVersion(new Version(10, 3, 39));

		services.AddDbContext<MendelDbContext>(options =>
		options.UseMySql(connectionString, serverVersion));

		return services;
	}
}