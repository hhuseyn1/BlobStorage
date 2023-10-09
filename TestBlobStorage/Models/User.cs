namespace TestBlobStorage.Models;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public byte[] PassHash { get; set; }
    public byte[] PassSalt { get; set; }
}
