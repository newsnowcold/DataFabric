using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataFabric.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using DataFabric.Domain.Interfaces;
using DataFabric.Infrastructure.Repositories;

namespace DataFabric.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped(typeof(IPersistenceRepository<>), typeof(PersistenceRepository<>));

            return services;
        }
    }
}
