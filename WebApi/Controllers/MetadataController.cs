using Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi.Controllers;

[Route("api/admin/[controller]")]
[ApiController]
public class MetadataController : ControllerBase
{
    public MetadataController()
    {
        
    }
    
    [HttpGet]
    [Route("entity/{entityType}")]
    public async Task<IActionResult> GetJsonMetadata(EntityType entityType)
    {
        String filePath = Path.Combine(Directory.GetCurrentDirectory(), "Metadata", $"{entityType}_FormMetadata.json");
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound(new { message = "File not found"  + filePath});
        }

        try
        {
            var jsonData = await System.IO.File.ReadAllTextAsync(filePath);
            var parsedData = JsonSerializer.Deserialize<object>(jsonData); // Deserialize if needed
            return Ok(parsedData);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Error reading file", error = ex.Message });
        }
    }
}