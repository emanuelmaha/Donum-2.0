using Donum.Services;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

public class ImportExportController : AuthorizeController
{
	private readonly IImporterService _importerService;

	public ImportExportController(IImporterService importerService)
	{
		_importerService = importerService;
	}

	[HttpPost]
	public void Run() => _importerService.Import();
}