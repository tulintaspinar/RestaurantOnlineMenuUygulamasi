using Microsoft.AspNetCore.Mvc;
using MikroservicesUI.Models;
using Newtonsoft.Json;

namespace MikroservicesUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var burgerResponse = await client.GetAsync("http://localhost:8001/api/Burger");
            var convertBurgerData = JsonConvert.DeserializeObject<List<BurgerViewModel>>(await burgerResponse.Content.ReadAsStringAsync());
            ViewBag.BurgerCount = convertBurgerData.ToList().Count();

            var drinkResponse = await client.GetAsync("http://localhost:8001/api/Drink");
            var convertDrinkData = JsonConvert.DeserializeObject<List<DrinkViewModel>>(await drinkResponse.Content.ReadAsStringAsync());
            ViewBag.DrinkCount = convertDrinkData.ToList().Count();

            ViewBag.AllMenuCount = (convertBurgerData.ToList().Count() + convertDrinkData.ToList().Count());

            var orderResponse = await client.GetAsync("http://localhost:8001/api/Order");
			var convertOrderData = JsonConvert.DeserializeObject<List<OrderViewModel>>(await orderResponse.Content.ReadAsStringAsync());
            ViewBag.OrderTotalGains = convertOrderData.Select(x => x.Price).Sum();
			return View();
        }
    }
}
