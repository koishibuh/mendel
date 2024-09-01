using Mendel.Web;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var app = builder
.ConfigureServices()
.ConfigurePipeline();

app.UseSerilogRequestLogging();

await app.MigrateDatabase();

app.Run();