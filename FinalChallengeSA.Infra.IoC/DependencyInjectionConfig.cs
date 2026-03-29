using FinalChallengeSA.Application.Commands.Customers.CreateCustomer;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalChallengeSA.Infra.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplicationMediatR(this IServiceCollection services)
        {
            var applicationAssembly = typeof(CreateCustomerCommand).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(applicationAssembly);
            });

            return services;
        }

        //public static IServiceCollection AddRepositories(this IServiceCollection services)
        //{
        //    services.AddScoped<ICartRepository, CartRepository>();
        //    services.AddScoped<IProduc, ProductRepository>();
        //    services.AddScoped<IOrderRepository, OrderRepository>();
        //    services.AddScoped<ICategoryRepository, CategoryRepository>();

        //    return services;
        //}

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        //public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(MappingProfile));
        //    return services;
        //}
    }
}
