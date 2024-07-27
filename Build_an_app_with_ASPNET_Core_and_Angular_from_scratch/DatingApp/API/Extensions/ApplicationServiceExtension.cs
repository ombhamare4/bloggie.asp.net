using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors();
        services.AddScoped<ITokenService, TokenService>();
        services.AddDbContext<DataContext>(opt =>
        {
            // opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            var connectionString = config.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21)); // Adjust the version according to your MySQL version

            opt.UseMySql(connectionString, serverVersion);
        });
        return services;

    }
}
