using Donum.Models.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
	protected Account Account => (Account)HttpContext.Items["Account"];
}