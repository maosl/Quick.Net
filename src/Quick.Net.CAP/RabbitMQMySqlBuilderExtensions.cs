using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quick.Net.CAP.RabbitMQ.MySql;
using System.Runtime.CompilerServices;

namespace Quick.Net.CAP
{
    public static class RabbitMQMySqlBuilderExtensions
    {
        public static void AddRabbitMQMysql(this IServiceCollection services)
        {

            services.AddCap(x =>
            {
                 x.UseEntityFramework<AppDbContext>();
                 x.UseRabbitMQ("localhost");
                 x.UseDashboard();
                 //x.EnableConsumerPrefetch = true;
                 x.UseDispatchingPerGroup = true;
                 x.EnableSubscriberParallelExecute = true;
                 //x.FailedThresholdCallback = failed =>
                 //{
                 //    //var logger = failed.ServiceProvider.GetService<ILogger<Startup>>();
                 //    //logger.LogError($@"A message of type {failed.MessageType} failed after executing {x.FailedRetryCount} several times, 
                 //    //  requiring manual troubleshooting. Message name: {failed.Message.GetName()}");
                 //};
            });
        }
    }
}
