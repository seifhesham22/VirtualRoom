using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Application.Features.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Identity.Application.ApplicationDI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
        {
            services.AddScoped<LoginService>();
            return services;
        }
    }
}