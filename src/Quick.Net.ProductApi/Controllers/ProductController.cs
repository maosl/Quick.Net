using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Net;

namespace Quick.Net.ProductApi.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [Route("api/products")]
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
            return new string[] { "我来自 Quick.Net.ProductApi：" + localIp + "的服务" };
        }

    }
}
