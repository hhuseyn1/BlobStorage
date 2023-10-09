using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestBlobStorage.Services;

namespace TestBlobStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IStorageManager _storageManager;

    public FileController(IStorageManager storageManager)
    {
        _storageManager = storageManager;
    }

    [HttpGet("getUrl")]
    public IActionResult GetUrl(string fileName)
    {
        try
        {
            var result = _storageManager.GetSignedUrl(fileName);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }

    [HttpGet("getUrlAsync")]
    public async Task<IActionResult> GetUrlAsync(string fileName)
    {
        try
        {
            var result = await _storageManager.GetSignedUrlAsync(fileName);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }

    [HttpDelete("deleteFile")]
    public IActionResult DeleteFile(string fileName)
    {
        try
        {
            var result = _storageManager.DeleteFile(fileName);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }


    [HttpDelete("deleteFileAsync")]
    public async Task<IActionResult> DeleteFileAsync(string fileName)
    {
        try
        {
            var result = await _storageManager.DeleteFileAsync(fileName);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }

    [HttpPost("uploadFile")]
    public IActionResult UploadFile(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            {
                var fileName = file.FileName;
                var contentType = file.ContentType;
                var result = _storageManager.UploadFile(stream, fileName, contentType);
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost("uploadFileAsync")]
    public async Task<IActionResult> UploadFileAsync(IFormFile file)
    {
        try
        {
            using (var stream = file.OpenReadStream())
            {
                var fileName = file.FileName;
                var contentType = file.ContentType;
                var result = await _storageManager.UploadFileAsync(stream, fileName, contentType);
                return Ok(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

}
