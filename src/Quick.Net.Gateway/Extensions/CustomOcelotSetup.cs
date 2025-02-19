using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Nacos;
using Ocelot.Provider.Polly;
using System;
using System.Threading.Tasks;

namespace Quick.Net.Gateway.Extensions
{
    public static class CustomOcelotSetup
    {
        /// <summary>
        /// 注入Ocelot
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddCustomOcelotSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddOcelot()
                .AddNacosDiscovery()
                .AddPolly();
        }
        /// <summary>
        /// 使用Ocelot
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> UseCustomOcelot(this IApplicationBuilder app)
        {
            await app.UseOcelot();
            return app;
        }

    }
}
