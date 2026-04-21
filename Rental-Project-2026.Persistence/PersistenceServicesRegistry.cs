using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Persistence.Repositories;
using Rental_Project_2026.Persistence.UnitOfWorks;

namespace Rental_Project_2026.Persistence
{
    public static class PersistenceServicesRegistry
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer("name=RentalProjectConnectionString");
            });

            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IBranchesRepository, BranchesRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
