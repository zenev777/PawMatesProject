using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Core.Contracts.AttendanceInterface;
using PawMates.Core.Contracts.EventInterface;
using PawMates.Core.Contracts.PetInterface;
using PawMates.Core.Contracts.PostInterface;
using PawMates.Core.Services.AttendanceService;
using PawMates.Core.Services.EventService;
using PawMates.Core.Services.PetService;
using PawMates.Core.Services.PostService;
using PawMates.Infrastructure.Data;
using PawMates.Infrastructure.Data.Common;
using PawMates.Infrastructure.Data.IdentityModels;

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
            service.AddScoped<IPostService, PostService>();

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
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
            
            return services;
        }


    }
}
