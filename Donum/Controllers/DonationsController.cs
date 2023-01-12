using Donum.Data;
using Donum.Models;
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
	public List<Donation> Get(string memberName)
	{
		if (!string.IsNullOrEmpty(memberName)) {
			return _dbContext.Donations.Include(d => d.Member).Where(d =>
				d.Member.FirstName.Contains(memberName) || d.Member.LastName.Contains(memberName) ||
				(d.Member.FirstName + d.Member.LastName).Contains(memberName)).ToList();
		}

		return _dbContext.Donations.Include(d => d.Member).ToList();
	}
}