using Bloggie.web.Data;
using Bloggie.web.Repositories;
using Bloggie.web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<BloggieDbContext>(opt =>
{
    // opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // Adjust the version according to your MySQL version

    opt.UseMySql(connectionString, serverVersion);
});

builder.Services.AddScoped<IBlogPost, BlogPostRepository>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
