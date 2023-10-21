using Microsoft.EntityFrameworkCore;
using TestBlobStorage.Models;

namespace TestBlobStorage.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string endpointUri = "YourCosmosDBEndpoint";
        string primaryKey = "YourPrimaryKey";
        string databaseName = "YourDatabaseName";

        optionsBuilder.UseCosmos(endpointUri, primaryKey, databaseName);
    }

    public DbSet<User> Users { get; set; } 
}
