using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using Donum.Models;

namespace Donum.Data;

public class DataContext : ApiAuthorizationDbContext<ApplicationUser>
{
	public DbSet<Member> Members { get; set; }

	public DbSet<Donation> Donations { get; set; }
	
	public DataContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
		: base(options, operationalStoreOptions) { }
}