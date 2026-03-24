using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Rental_Project_2026.Application.UseCases.Branches.Commands;
using Rental_Project_2026.Application.Utilities.Mediator;

namespace Rental_Project_2026.Application
{
    public static class ApplicationServicesRegistry
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, SimpleMediator>();

            services.AddScoped<IRequestHandler<CreateBranchCommand, Guid>, CreateBranchUseCase>();

            return services;
        }
    }
}
