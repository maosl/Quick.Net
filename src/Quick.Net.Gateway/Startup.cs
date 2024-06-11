using Microsoft.AspNetCore.Authentication;
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
            //services.AddCustomSwaggerSetup();
            services.AddControllers();
            services.AddCustomOcelotSetup();
            //services.AddMCodeOcelotSwagger(new OcelotSwaggerOptions()
            //{
            //    new SwaggerEndPoints()  {
            //         new SwaggerEndPoint(){ Name="ap1",Url="http://localhost:5000"},
            //         new SwaggerEndPoint(){ Name="ap2",Url="http://localhost:5000"},
            //    }
            //});
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
            app.UseMCodeOcelotSwagger();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseCustomOcelot().Wait();
        }
    }
}
