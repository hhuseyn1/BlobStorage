using TestBlobStorage.Models.Abstracts;

namespace TestBlobStorage.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte[] PassHash { get; set; }
    public byte[] PassSalt { get; set; }
}
