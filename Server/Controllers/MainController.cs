using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MainController : ControllerBase
{
    [HttpGet(Name = "GetMain")]
    public string Get()
    {
        return "Hello, World!";
    }
}
