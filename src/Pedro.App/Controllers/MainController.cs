using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pedro.App.Controllers;

[Route("api/[controller]")]
[ApiController]
public abstract class MainController : ControllerBase
{
}
