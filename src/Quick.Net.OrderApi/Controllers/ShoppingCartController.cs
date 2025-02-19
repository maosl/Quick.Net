using Microsoft.AspNetCore.Mvc;

namespace Quick.Net.OrderApi.Controllers
{
    public class ShoppingCartController : ControllerBase
    {
        [Route("api/order/shopping-carts")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            Console.WriteLine($"开始打印header信息");
            foreach (var item in this.Request.Headers)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            Console.WriteLine($"打印header信息完成");
            return new string[] { "洗发水", "无人机" };
        }

        [Route("api/order/shopping-carts")]
        [HttpPost]
        public string Post()
        {
            return "添加商品到购物车成功";
        }
    }
}
