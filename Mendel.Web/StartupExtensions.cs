using Mendel.Core.Configurations;
using Mendel.Core.Exceptions;
using Mendel.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Mendel.Web;

public static class StartupExtensions
{
	public static WebApplication ConfigureServices
	(this WebApplicationBuilder builder)
	{
		var debugMode = builder.Environment.IsDevelopment();

		builder.Configuration.AddEnvironmentVariables();

		var settings = builder.Configuration.GetSection("Settings");
		builder.Services.Configure<Settings>(settings);

		builder.Services.AddHttpClient("TheFinalOutpost", httpClient =>
		httpClient.BaseAddress = new Uri("https://finaloutpost.net/api/v1/"));

		builder.Services.AddSerilog();
		builder.Services.AddPersistenceService(builder.Configuration);

		builder.Services.AddControllers();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

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

		app.UseExceptionHandler();

		app.UseHttpsRedirection();
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