using TestBlobStorage.Models.Abstracts;

namespace TestBlobStorage.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ushort Age { get; set; }
    public IFormFile Image { get; set; }
    public byte[] PassHash { get; set; }
    public byte[] PassSalt { get; set; }
}
