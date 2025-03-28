using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer(
            @"Server=localhost;Database=MLCDatabase;User Id=sa;Password=VerySecret1234;TrustServerCertificate=True;Encrypt=False");
        return new DataContext(optionsBuilder.Options);
    }
}