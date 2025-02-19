using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;

namespace Quick.Net.OrderApi.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ILogger _logger;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        [Route("api/orders")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string localIp = string.Empty;
            // 获取本地主机名
            string hostName = Dns.GetHostName();
            // 获取本地主机的 IP 地址列表
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIp = ip.ToString();
                    break;
                }
            }

            return new string[] { "我来自 Quick.Net.OrderApi：" + localIp+ "的服务" };
        }
    }
}
