using Microsoft.AspNetCore.Mvc;
using MikroservicesUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace MikroservicesUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:8001/api/Order");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<List<OrderViewModel>>(data);
                return View(convertData);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(MenuViewModel menu)
        {
            OrderViewModel order = new OrderViewModel();
            order.OrderId = "";
            order.Name = menu.Name;
            order.Price = menu.Price;
            order.Description = menu.Description;
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(order);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:8001/api/Order", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Menu","Menu");
        }

        public async Task<IActionResult> DeleteOrderInfo(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:8001/api/Order/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("Index");
            else
            {
                TempData["Error"] = "Order is not deleted!";
                return RedirectToAction("Index");
            }

        }
    }
}
