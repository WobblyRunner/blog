namespace Blog.Data;

public static class Global
{
	public const string ENV_UNTRUSTEDCACHE_FILEPATH = @"untrusted_random_cache";
	public static string GetRandomCachedFilePath(this IHostEnvironment environment, out string randomFileName)
	{
		randomFileName = Path.GetRandomFileName();
		return Path.Combine(environment.ContentRootPath, environment.EnvironmentName, ENV_UNTRUSTEDCACHE_FILEPATH, randomFileName);
	}
}