using CloudinaryDotNet;
using FluentValidation;
using Mendel.Core.Configurations;
using Mendel.Core.Services.Cloudinary;
using Mendel.Core.Services.FinalOutpost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
		services.AddTransient<ICloudinaryService, CloudinaryService>();

		services.AddSingleton<ICloudinary, Cloudinary>(s =>
		{
			var settings = s.GetRequiredService<IOptions<CloudinarySettings>>().Value;
			var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);

			return new Cloudinary(account);
		});

		return services;
	}
}