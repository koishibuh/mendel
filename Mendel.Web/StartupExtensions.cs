using Mendel.Core.Common;
using Mendel.Core.Configurations;
using Mendel.Core.Exceptions;
using Mendel.Core.Persistence;
using Mendel.Core.Services.Cloudinary;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Mendel.Web;

public static class StartupExtensions
{
	public static WebApplication ConfigureServices
	(this WebApplicationBuilder builder)
	{
		var debugMode = builder.Environment.IsDevelopment();
		if (debugMode)
		{
			builder.Configuration.AddUserSecrets<Program>();
		}
		else
		{
			builder.Configuration.AddEnvironmentVariables();
		}

		var settings = builder.Configuration.GetSection("Settings");
		var cloudinarySettings = builder.Configuration.GetSection("CloudinarySettings");
		builder.Services.Configure<Settings>(settings);
		builder.Services.Configure<CloudinarySettings>(cloudinarySettings);

		builder.Services.AddHttpClient("TheFinalOutpost", httpClient =>
		httpClient.BaseAddress = new Uri("https://finaloutpost.net/api/v1/"));

		builder.Services.AddHttpClient("Default");

		builder.Services.AddSerilog((services, lc) => lc
		.ReadFrom.Configuration(builder.Configuration)
		.ReadFrom.Services(services)
		.WriteTo.Console());

		builder.Services.AddAppServices();
		builder.Services.AddPersistenceService(builder.Configuration);

		builder.Services.AddControllers();

		builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();

		return builder.Build();
	}

	public static WebApplication ConfigurePipeline(this WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		var forwardedHeaderOptions = new ForwardedHeadersOptions
		{
			ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
			RequireHeaderSymmetry = false
		};

		forwardedHeaderOptions.KnownNetworks.Clear();
		forwardedHeaderOptions.KnownProxies.Clear();
		app.UseForwardedHeaders(forwardedHeaderOptions);

		app.UseExceptionHandler();

		app.UseDefaultFiles();
		app.UseStaticFiles();

		app.UseRouting();

		app.MapControllers();
		app.MapFallbackToFile("index.html");

		return app;
	}

	public static async Task MigrateDatabase(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		try
		{
			var context = scope.ServiceProvider.GetService<MendelDbContext>();
			if (context is not null)
			{
				await context.Database.MigrateAsync();
			}
		}
		catch (Exception e)
		{
			// TODO: Log
		}
	}
}