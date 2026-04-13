using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.People.Application.Interfaces;
using Modules.People.Infrastructure.Persistence;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Modules.People.Infrastructure.Dependancies
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPeopleModule(
            this IServiceCollection service,
            IConfiguration config)
        {
            service.AddDbContext<PeopleDbContext>(x =>
            {
                x.UseNpgsql(config.GetConnectionString("default"));
            });

            service.AddScoped<IPeopleDbContext, PeopleDbContext>();
            service.AddScoped<CurrentUser>();
            return service;
        }
    }
}