using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarReferenceGuide.Data;

public static class ConfigureServices
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarReferenceGuideDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("def_connection"));
        });
        services.AddScoped<CarReferenceGuideDbContext>();
    }
}