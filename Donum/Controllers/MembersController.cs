using Donum.Data;
using Donum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

public class MembersController : AuthorizeController
{
	private readonly DataContext _dbContext;

	public MembersController(DataContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	public List<Member> Get() => _dbContext.Members.ToList();
}