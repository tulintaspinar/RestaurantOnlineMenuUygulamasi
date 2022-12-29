using Microsoft.AspNetCore.Mvc;
using MikroservicesUI.Models;
using Newtonsoft.Json;

namespace MikroservicesUI.Controllers
{
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Menu()
        {
            var client = _httpClientFactory.CreateClient();
            var burgerListResponse = await client.GetAsync("http://localhost:8001/api/Burger");
            var drinkListResponse = await client.GetAsync("http://localhost:8001/api/Drink");

            if (burgerListResponse.IsSuccessStatusCode && drinkListResponse.IsSuccessStatusCode)
            {
                var burgerData = await burgerListResponse.Content.ReadAsStringAsync();
                var convertBurgerData = JsonConvert.DeserializeObject<List<MenuViewModel>>(burgerData);

                var drinkData = await drinkListResponse.Content.ReadAsStringAsync();
                var convertDrinkData = JsonConvert.DeserializeObject<List<MenuViewModel>>(drinkData);

                List<MenuViewModel> listMenu = new List<MenuViewModel>();
                listMenu.AddRange(convertBurgerData);
                listMenu.AddRange(convertDrinkData);

                return View(listMenu);
            }

            return View();
        }
    }
}
