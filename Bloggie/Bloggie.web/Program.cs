using Bloggie.web.Data;
using Bloggie.web.Repositories;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//AddController
builder.Services.AddControllers();

builder.Services.AddDbContext<BloggieDbContext>(opt =>
{
    // opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // Adjust the version according to your MySQL version

    opt.UseMySql(connectionString, serverVersion);
});

builder.Services.AddDbContext<AuthDbContext>(opt =>
{
    // opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // Adjust the version according to your MySQL version

    opt.UseMySql(connectionString, serverVersion);
});

builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();
//Default password setting
builder.Services.Configure<IdentityOptions>(options=>{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

});

builder.Services.ConfigureApplicationCookie(options => 
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

//Repository and Interface Repository Injection
builder.Services.AddScoped<IBlogPost, BlogPostRepository>();
builder.Services.AddScoped<IImage, ImageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
