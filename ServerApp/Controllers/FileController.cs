using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;
namespace WebApplication1.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{
    private IFileRepository mFileRep;

    public FileController(IFileRepository fileRep)
    {
        mFileRep = fileRep;
    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var blobName = await mFileRep.AddAsync(file);
        return Ok(new { url = blobName });
    }
    
    [HttpGet("download/{filename}")]
    public async Task<IActionResult> GetByName(string filename)
    {
        var result = await mFileRep.GetStreamAsync(filename);

        if (result is null)
            return NotFound();

        var (stream, contentType) = result.Value;

        return File(stream, contentType);
    }

    [HttpGet]
    [Route("getall")]
    public async Task<List<string>> GetAll()
    {
        var res = await mFileRep.GetAllAsync();
        return res;
    }
    
    [HttpDelete("delete/{blobName}")]
    public async Task<IActionResult> Delete(string blobName)
    {
        var deleted = await mFileRep.DeleteAsync(blobName);

        if (!deleted)
            return NotFound(new { message = "Blob not found." });

        return Ok(new { message = "Blob deleted successfully." });
    }
}