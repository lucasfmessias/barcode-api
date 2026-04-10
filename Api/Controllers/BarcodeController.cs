using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("barcode")]
public class BarcodeController : ControllerBase
{
    [HttpPost("generate")]
    public IActionResult Generate([FromBody] BarcodeInput input)
    {
        var service = new BarcodeService();
        var result = service.Generate(input);
        return Ok(result);
    }
}