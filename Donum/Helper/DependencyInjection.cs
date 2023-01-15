using Donum.Services;

namespace Donum.Helper;

public static class DependencyInjection
{
	public static void RegisterDependencyInjection(this IServiceCollection services)
	{
		services.AddScoped<IImporterService, ImporterService>();
		services.AddScoped<IReportService, ReportService>();
	}
}