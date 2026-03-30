using FinalChallengeSA.Application.Commands.Customers.CreateCustomer;
using FinalChallengeSA.Application.Interfaces;
using FinalChallengeSA.Application.Validators;
using FinalChallengeSA.Infra.Data.Context;
using FinalChallengeSA.Infra.Data.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalChallengeSA.Infra.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void AddApplicationMediatR(this IServiceCollection services)
        {
            var applicationAssembly = typeof(CreateCustomerCommand).Assembly;

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(applicationAssembly);
            });
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void SetupFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCustomerRequestValidator>();
        }
    }
}
