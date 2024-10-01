using Application.Interfaces.Contexts;
using Application.Interfaces.Repositories;
using Infrastructure.Persistance.Contexts;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CLEAN_ARCH_TEST"),
                b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)),ServiceLifetime.Singleton);


            //This is the provider of IApplicationDBContext into ApplicationDBContext.cs i.e., dependency injection.
            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());
            services.AddScoped<IMeasureRepository, MeasureRepository>();
            services.AddScoped<IJobRepository, JobRepository>();

            return services;
        }
    }
}