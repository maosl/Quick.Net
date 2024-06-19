using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Quick.Net.Gateway.Extensions;
using System.Reflection;

namespace Quick.Net.Gateway
{
    public class Startup
    {
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Quick.Net.Gateway", Version = "v1" });
            });
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
            app.UseSwagger();
            //1、第一步，这里不需要以 /swagger 开头
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/Quick.Net.Api/swagger.json", "Quick.Net.Api");
                o.SwaggerEndpoint("/Quick.Net.Gateway/swagger.json", "Quick.Net.Gateway");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomOcelot().Wait();
        }
    }
}
