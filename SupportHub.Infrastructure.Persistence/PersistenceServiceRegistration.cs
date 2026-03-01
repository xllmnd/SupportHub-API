using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Infrastructure.Persistence.Context;
using SupportHub.Infrastructure.Persistence.Repositories;

namespace SupportHub.Infrastructure.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SupportHubDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("SupportHub.Infrastructure.Persistence")
                ));

            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
