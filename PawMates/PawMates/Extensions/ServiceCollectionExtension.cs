﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Services.AttendanceService;
using PawMates.Core.Services.EventService;
using PawMates.Core.Services.PetService;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection service)
        {
            service.AddLogging();

            service.AddScoped<IEventService, EventService>();
            service.AddScoped<IAttendanceService, AttendanceService>();
            service.AddScoped<IPetService, PetService>();

            return service;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository, Repository>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }

        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;

            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
            
            return services;
        }


    }
}
