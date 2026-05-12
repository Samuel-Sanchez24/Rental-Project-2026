using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rental_Project_2026.Application.Contracts.Repositories;
using Rental_Project_2026.Persistence.Entities;
using Rental_Project_2026.Persistence.Repositories;
using Rental_Project_2026.Persistence.Seeding;
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
            services.AddScoped<IVehiclesRepository, VehiclesRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.AddTransient<SeedDb>();

            //INFRACSTRUCTURE SERVICES
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            }).AddIdentityCookies();

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.SignIn.RequireConfirmedEmail = false; //TODO  
                options.User.RequireUniqueEmail = true;
            }).AddSignInManager<SignInManager<ApplicationUser>>()
              .AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(15);
            });

            return services;
        }
    }
}
