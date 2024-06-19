
using Nacos.AspNetCore.V2;

namespace Quick.Net.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddNacosAspNet(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Quick.Net.Api",new Microsoft.OpenApi.Models.OpenApiInfo() { Title= "Quick.Net.Api", Version= "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(option => {
                    option.SwaggerEndpoint("/swagger/Quick.Net.Api/swagger.json", "Quick.Net.Api v1");
                });
            }
        
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
