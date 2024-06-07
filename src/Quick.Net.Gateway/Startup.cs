using Microsoft.AspNetCore.Authentication;
using Ocelot.DependencyInjection;
using Quick.Net.Gateway.Extensions;
using System.Reflection;

namespace Quick.Net.Gateway
{
    public class Startup
    {
        /**
        *┌──────────────────────────────────────────────────────────────┐
        *│　描    述：模拟一个网关项目         
        *│　测    试：在网关swagger中查看具体的服务         
        *│　作    者：anson zhang                                             
        *└──────────────────────────────────────────────────────────────┘
        */
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwaggerSetup();
            services.AddControllers();
            services.AddCustomOcelotSetup();
        }
        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseCustomSwaggerMildd(() => Assembly.GetExecutingAssembly().GetManifestResourceStream("Quick.Net.Gateway.index.html"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomOcelot().Wait();
        }
    }
}
