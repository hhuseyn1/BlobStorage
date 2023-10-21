using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestBlobStorage.Data;
using TestBlobStorage.Models;
using TestBlobStorage.Models.DTOs;
using TestBlobStorage.Services;

namespace TestBlobStorage.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly AppDbContext _context;
    private readonly IStorageManager _storageManager;
    public UserController(AppDbContext context, IStorageManager storageManager)
    {
        _context = context;
        _storageManager = storageManager;
    }

    [HttpGet]
    public IActionResult GetProfilebyName(string name)
    {
        var user = _context.Users.FirstOrDefault(x => x.Name == name);
        return user is null ? NotFound() : Ok(user);
    }


    [HttpPost]
    public IActionResult UpdateProfile(RegisterDTO user)
    {
        var check = _context.Users.FirstOrDefault(u => u.Name == user.Name);
        if (check is null)
            return NotFound();

        var newUser = new User
        {
            Name = user.Name,
            Surname = user.Surname,
            Age = user.Age
        };
        return Ok(newUser);
    }


    [HttpDelete("deleteFile/{Id}")]
    public IActionResult DeleteFile(Guid id)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user is null)
                return NotFound();
            var fileName = user.Image.FileName;
            var result = _storageManager.DeleteFile(fileName);
            return Ok(result);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.NotFound);
        }
    }


    [HttpPost("uploadImage")]
    public IActionResult UploadImage(Guid id, IFormFile file)
    {
        try
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (file is null || user is null) return NotFound();
            using (var stream = file.OpenReadStream())
            {
                var fileName = Guid.NewGuid().ToString();
                var contentType = file.ContentType;
                var result = _storageManager.UploadFile(stream, fileName, contentType);
                user.Image = file;
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
