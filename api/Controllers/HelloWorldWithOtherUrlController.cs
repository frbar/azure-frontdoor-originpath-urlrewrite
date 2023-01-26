using Microsoft.AspNetCore.Mvc;

namespace Frbar.AzurePoc.BackendApi.Controllers;

[ApiController]
[Route("/something-else/api/hello-world")]
public class HelloWorldWithOtherUrlController : ControllerBase
{
    private readonly ILogger<HelloWorldWithOtherUrlController> _logger;

    public HelloWorldWithOtherUrlController(ILogger<HelloWorldWithOtherUrlController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        var response = "Hello from HelloWorldWithOtherUrlController!" + Environment.NewLine;
        
        foreach(var header in Request.Headers.ToList())
        {
            response += $"{header.Key} = {header.Value.First()}{Environment.NewLine}";
        }           

        return response;
    }
}
