using FluentValidation;
using Mendel.Core.Services.FinalOutpost;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Mendel.Core.Common;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddAppServices
	(this IServiceCollection services)
	{
		services.AddValidatorsFromAssembly(typeof(ApplicationServiceRegistration).Assembly);

		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(typeof(ApplicationServiceRegistration).Assembly);
			cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
		});

		services.Scan(selector => selector.FromAssemblies(typeof(ApplicationServiceRegistration).Assembly)
		.AddClasses(false)
		.UsingRegistrationStrategy(RegistrationStrategy.Skip)
		.AsMatchingInterface()
		.WithTransientLifetime());

		services.AddSingleton<IFinalOutpostService, FinalOutpostService>();

		return services;
	}
}