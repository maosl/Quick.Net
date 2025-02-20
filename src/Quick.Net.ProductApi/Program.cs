
using Nacos.AspNetCore.V2;

namespace Quick.Net.ProductApi
{
    public class Program
    {
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
            //服务注册
            builder.Services.AddNacosAspNet(builder.Configuration);
            builder.Services.AddControllers();
    
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Quick.Net.ProductApi", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Quick.Net.ProductApi", Version = "v1" });
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint("/swagger/Quick.Net.ProductApi/swagger.json", "Quick.Net.ProductApi v1");
                });
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
