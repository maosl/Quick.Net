
using Microsoft.Extensions.Configuration;
using Nacos.AspNetCore.V2;
using Nacos.V2.DependencyInjection;

namespace Quick.Net.OrderApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //配置中心
            builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
            //服务注册
            builder.Services.AddNacosAspNet(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Quick.Net.OrderApi", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Quick.Net.OrderApi", Version = "v1" });
            });
        
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(option =>
                {
                    option.SwaggerEndpoint("/swagger/Quick.Net.OrderApi/swagger.json", "Quick.Net.OrderApi v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
