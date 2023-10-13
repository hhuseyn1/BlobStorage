using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<string> Login(LoginDTO user)
    {
        var check = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
        if (check == null)
            return null;
        if (!PasswordHash.ConfirmPasswordHash(user.Password, check.PassHash, check.PassSalt))
            return null;

        return JWTService.GenerateSecurityToken(check);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO user)
    {
        try
        {
            var check = await _context.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
            if (check != null)
                return BadRequest("The user has already exists!");
            PasswordHash.Create(user.Password, out byte[] passHash, out byte[] passSalt);
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                Surname = user.Surname,
                PassHash = passHash,
                PassSalt = passSalt
            };

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }
}
