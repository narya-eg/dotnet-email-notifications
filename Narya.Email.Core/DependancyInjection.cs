using Microsoft.Extensions.DependencyInjection;
using Narya.Email.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Narya.Email.Core
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddEmailProvider(this IServiceCollection services, Func<IServiceProvider, IEmailProvider> config)
        {
            services.AddSingleton<IEmailProvider>(config);
            return services;
        }
    }
}
