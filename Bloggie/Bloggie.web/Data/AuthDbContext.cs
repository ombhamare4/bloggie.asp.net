using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=bloggieAuthDb;User=root;Password=root;", new MySqlServerVersion(new Version(8, 0, 21)));
        }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var superAdminRoleId = "8d9248dd-6db7-4ccb-8f7a-15a3909b2521";
            var adminRoleId = "1338ecff-8e6f-4f0f-9007-beacb6d64ba1";
            var userRoleID = "135e42d2-fe2f-427a-97d7-a9c08f1e4d79";
            //Seed Roles(User,Superadmin,admin)
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id=userRoleID,
                    ConcurrencyStamp = userRoleID
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
            //Seed SuperAdmin
            var superAdminId = "34e1d293-baf7-4ccf-80ae-e16c566f9e4b";
            var superAdminUser = new IdentityUser()
            {
                Id = superAdminId,
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                PasswordHash = null,
                NormalizedEmail= "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper()
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser,"superadmin123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add all roles to Superadmin
 
            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleID,
                    UserId = superAdminId
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }

}
