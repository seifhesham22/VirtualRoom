using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Infrastructure.IdentityModules;
using Modules.Identity.Infrastructure.Presestance;
using Modules.Identity.Infrastructure.TokenService;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Modules.Identity.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityModule(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Database
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseNpgsql(configuration.GetConnectionString("default"));
            });

            //Identity
            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<ITokenHandler, TokenHandler>();
            return services;
        }
    }
}