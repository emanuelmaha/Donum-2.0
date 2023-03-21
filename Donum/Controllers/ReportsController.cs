using Donum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

public class ReportsController : AuthorizeController
{
	private readonly IReportService _reportService;

	public ReportsController(IReportService reportService)
	{
		_reportService = reportService;
	}

	[HttpGet("allMembers")]
	public string RunAllMembers(DateTime date) => _reportService.AllMembersReport(date);
}