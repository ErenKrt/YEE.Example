using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEE.Identity.Application.Helpers;
using YEE.Identity.Application.Services.Impl;
using YEE.Identity.Application.Services.Interfaces;
using YEE.Identity.Core.Helpers.Impl;
using YEE.Identity.Core.Helpers.Interfaces;
using YEE.Identity.DataAccess.EntityFramework;
using YEE.Identity.DataAccess.EntityFramework.Impl;
using YEE.Identity.DataAccess.EntityFramework.Interfaces;

namespace YEE.Identity.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration
                .GetSection("YEE")
                .GetSection("Database")
                .GetSection("ConnectionString")
                .Value;

            services.AddDbContext<DatabaseContext>(ops =>
            {
                ops.UseNpgsql(connectionString);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<IHasher, Hasher>();
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();
            config.Apply(new DtoMapper());

            var customMapper = new CustomMapper(config);
            services.Add(
                new ServiceDescriptor(
                    typeof(ICustomMapper),
                    sp => customMapper,
                    ServiceLifetime.Transient
                )
            );

            return services;
        }
    }
}
