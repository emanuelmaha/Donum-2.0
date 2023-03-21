using Donum.Data;
using Donum.Models;
using Donum.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donum.Controllers;

public class DonationsController : AuthorizeController
{
	private readonly DataContext _dbContext;

	public DonationsController(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public List<Donation> Get(string? memberName = null)
	{
		if (!string.IsNullOrEmpty(memberName)) {
			return _dbContext.Donations.Include(d => d.Member).ToList().Where(d =>
					d.Member.FirstName.IndexOf(memberName, StringComparison.OrdinalIgnoreCase) >= 0 ||
					d.Member.LastName.IndexOf(memberName, StringComparison.OrdinalIgnoreCase) >= 0 ||
					(d.Member.FirstName + d.Member.LastName).IndexOf(memberName, StringComparison.OrdinalIgnoreCase) >=
					0)
				.OrderByDescending(d => d.DateOfReceived).ToList();
		}

		return _dbContext.Donations.Include(d => d.Member).OrderByDescending(d => d.DateOfReceived).Take(100).ToList();
	}
}