using AppTimer.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppTimer.Data;

public class DataContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=appTimer;User Id=SA;Password=Password@1;TrustServerCertificate=True");
    }

    public DbSet<Registry> Registries { get; set; }
}