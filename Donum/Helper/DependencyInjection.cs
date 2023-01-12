using Donum.Services;
using Donum.Services.Authorization;
using Donum.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Donum.Helper;

public static class DependencyInjection
{
	public static void RegisterDependencyInjection(this IServiceCollection services)
	{
		services.AddScoped<IJwtUtils, JwtUtils>();
		services.AddScoped<IAccountService, AccountService>();
		services.AddScoped<IEmailService, EmailService>();
	}
}