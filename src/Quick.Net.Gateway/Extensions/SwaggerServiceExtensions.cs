using Microsoft.OpenApi.Models;

namespace Quick.Net.Gateway.Extensions
{
    /// <summary>
    /// Swagger 服务扩展
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// 添加 Swagger 服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="swaggerOptions"></param>
        /// <returns></returns>
        public static IServiceCollection AddMCodeSwagger(this IServiceCollection services, SwaggerOptions swaggerOptions)
        {
            services.AddSingleton(swaggerOptions);

            SwaggerGenServiceCollectionExtensions.AddSwaggerGen(services, c =>
            {
                c.SwaggerDoc(swaggerOptions.ServiceName, swaggerOptions.ApiInfo);

                if (swaggerOptions.XmlCommentFiles != null)
                {
                    foreach (string xmlCommentFile in swaggerOptions.XmlCommentFiles)
                    {
                        string str = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
                        if (File.Exists(str)) c.IncludeXmlComments(str, true);
                    }
                }

                SwaggerGenOptionsExtensions.CustomSchemaIds(c, x => x.FullName);

                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = "请输入 bearer 认证"
                });


                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                                              {
                                                  {
                                                      new OpenApiSecurityScheme
                                                      {
                                                          Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                                                      },
                                                      new string[] {}
                                                  }
                                              });
            });

            return services;
        }

        /// <summary>
        /// 使用 Swagger UI
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMCodeSwagger(this IApplicationBuilder app)
        {
            string serviceName = app.ApplicationServices.GetRequiredService<SwaggerOptions>().ServiceName;

            SwaggerUIBuilderExtensions.UseSwaggerUI(SwaggerBuilderExtensions.UseSwagger(app), c =>
            {
                c.SwaggerEndpoint("/swagger/" + serviceName + "/swagger.json", serviceName);
            });
            return app;
        }
        public static IServiceCollection AddMCodeOcelotSwagger(this IServiceCollection services, OcelotSwaggerOptions ocelotSwaggerOptions)
        {
            services.AddSingleton(ocelotSwaggerOptions);
            SwaggerGenServiceCollectionExtensions.AddSwaggerGen(services);
            return services;
        }

        public static IApplicationBuilder UseMCodeOcelotSwagger(this IApplicationBuilder app)
        {
            OcelotSwaggerOptions ocelotSwaggerOptions = app.ApplicationServices.GetService<OcelotSwaggerOptions>();

            if (ocelotSwaggerOptions == null || ocelotSwaggerOptions.SwaggerEndPoints == null)
            {
                return app;
            }

            SwaggerUIBuilderExtensions.UseSwaggerUI(SwaggerBuilderExtensions.UseSwagger(app), c =>
            {
                foreach (SwaggerEndPoint swaggerEndPoint in ocelotSwaggerOptions.SwaggerEndPoints)
                {
                    c.SwaggerEndpoint(swaggerEndPoint.Url, swaggerEndPoint.Name);
                }
            });
            return app;
        }
    }
    /// <summary>
    /// Swagger配置
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// API信息
        /// </summary>
        public OpenApiInfo ApiInfo { get; set; }

        /// <summary>
        /// Xml注释文件
        /// </summary>
        public string[] XmlCommentFiles { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="apiInfo">API信息</param>
        /// <param name="xmlCommentFiles">Xml注释文件</param>
        public SwaggerOptions(string serviceName, OpenApiInfo apiInfo, string[] xmlCommentFiles = null)
        {
            ServiceName = !string.IsNullOrWhiteSpace(serviceName) ? serviceName : throw new ArgumentException("serviceName parameter not config.");
            ApiInfo = apiInfo;
            XmlCommentFiles = xmlCommentFiles;
        }
    }
    /// <summary>
    /// 网关Swagger配置
    /// </summary>
    public class OcelotSwaggerOptions
    {
        public List<SwaggerEndPoint> SwaggerEndPoints { get; set; }
    }
    /// <summary>
    /// Swagger终端
    /// </summary>
    public class SwaggerEndPoint
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
    }
}
