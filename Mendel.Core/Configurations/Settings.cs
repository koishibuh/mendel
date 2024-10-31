namespace Mendel.Core.Configurations;

public class Settings
{
	public string TfoApiKey { get; set; } = string.Empty;
}

public class CloudinarySettings
{
	public string CloudName { get; set; } = string.Empty;
	public string ApiKey { get; set; } = string.Empty;
	public string ApiSecret { get; set; } = string.Empty;
	public string ApiEnv { get; set; } = string.Empty;
}