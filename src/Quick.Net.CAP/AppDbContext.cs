using Microsoft.EntityFrameworkCore;

namespace Quick.Net.CAP.RabbitMQ.MySql
{


    public class AppDbContext : DbContext
    {
        public const string ConnectionString = "charset=utf8;server=127.0.0.1;port=3306;uid=saas_dev;pwd=9aHmc7je@gDd#IAR;Database=zm_dev03_saas_db;sslmode=none;";



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        }
    }
}
