using API.Entities;
using Microsoft.EntityFrameworkCore;


namespace API.Data;

public class DataContext : DbContext
{
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=datingapp;User=root;Password=root;", new MySqlServerVersion(new Version(8, 0, 21)));
    }
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<AppUser> Users { get; set; }
}
