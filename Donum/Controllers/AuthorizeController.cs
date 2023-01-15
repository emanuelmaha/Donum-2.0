using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Donum.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthorizeController : ControllerBase
{
}