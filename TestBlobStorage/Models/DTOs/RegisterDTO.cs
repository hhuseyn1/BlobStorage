namespace TestBlobStorage.Models.DTOs;

public class RegisterDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ushort Age { get; set; }
    public string Password { get; set; }
    public IFormFile Image { get; set; }
}
