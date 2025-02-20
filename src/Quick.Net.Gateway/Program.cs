
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Nacos;
using Ocelot.Provider.Polly;
using Ocelot.Middleware;

namespace Quick.Net.Gateway
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //配置中心
            builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
            // 从环境变量获取宿主机 IP 和映射端口
            var hostIp = System.Environment.GetEnvironmentVariable("HOST_IP");
            var hostPort = System.Environment.GetEnvironmentVariable("HOST_PORT");
            if (!string.IsNullOrEmpty(hostIp) && !string.IsNullOrEmpty(hostPort))
            {
                // 更新 Nacos 服务注册配置
                builder.Configuration["nacos:Ip"] = hostIp; //获取宿主机IP
                builder.Configuration["nacos:Port"] = hostPort;
            }
            builder.Services.AddOcelot()
                .AddNacosDiscovery()
                .AddPolly();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "Quick.Net.Gateway", Version = "v1" });
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/Quick.Net.OrderApi/swagger.json", "Quick.Net.OrderApi");
                o.SwaggerEndpoint("/Quick.Net.Gateway/swagger.json", "Quick.Net.Gateway");
            });
            app.UseOcelot().Wait();
            app.Run();

            //new WebHostBuilder()
            //.UseKestrel()
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.ConfigureAppConfiguration((hostingContext, config) =>
            //{
            //    config
            //        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
            //        .AddJsonFile("appsettings.json", true, true)
            //        //.AddJsonFile("ocelot.json")
            //        .AddEnvironmentVariables();
            //    //配置中心
            //    config.AddNacosV2Configuration(config.Build().GetSection("NacosConfig"));
            //})
            
            //.ConfigureServices(s =>
            //{
            //    s.AddOcelot()
            //        .AddNacosDiscovery()
            //        .AddPolly();
            //})
            //.ConfigureLogging((hostingContext, logging) =>
            //{

            //})
            ////.UseIISIntegration()
            //.Configure(app =>
            //{
            //    app.UseOcelot().Wait();
            //})
            //.Build()
            //.Run();
        }
    }
}
