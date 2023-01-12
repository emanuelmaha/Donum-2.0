using Donum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

public class ImportController : AuthorizeController
{
	private readonly IImporterService _importerService;

	public ImportController(IImporterService importerService)
	{
		_importerService = importerService;
	}

	[HttpPost]
	public void Run() => _importerService.Import();
}