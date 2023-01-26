using Microsoft.AspNetCore.Mvc;

namespace Frbar.AzurePoc.BackendApi.Controllers;

[ApiController]
[Route("/api/hello-world")]
public class HelloWorldController : ControllerBase
{
    private readonly ILogger<HelloWorldController> _logger;

    public HelloWorldController(ILogger<HelloWorldController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        var response = string.Empty;
        
        foreach(var header in Request.Headers.ToList())
        {
            response += $"{header.Key} = {header.Value.First()}{Environment.NewLine}";
        }           

        return response;
    }
}
