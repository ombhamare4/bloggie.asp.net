using Bloggie.web.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class BloggieDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=bloggiedb;User=root;Password=root;", new MySqlServerVersion(new Version(8, 0, 21)));
        }
        public BloggieDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> BlogPosts {  get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
