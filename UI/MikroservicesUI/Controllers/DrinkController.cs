using Microsoft.AspNetCore.Mvc;
using MikroservicesUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace MikroservicesUI.Controllers
{
    public class DrinkController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DrinkController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> AllDrinkList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:8001/api/Drink");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<List<DrinkViewModel>>(data);
                return View(convertData);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddDrink()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddDrink(DrinkViewModel drink)
        {
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(drink);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:8001/api/Drink", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Drink updated!";
                return RedirectToAction("AllDrinkList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteDrinkInfo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:8001/api/Drink/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("AllDrinkList");
            else
            {
                TempData["Error"] = "Drink is not deleted!";
                return RedirectToAction("AllDrinkList");
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditDrinkInfo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:8001/api/Drink/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<DrinkViewModel>(data);
                return View(convertData);
            }

            else
            {
                ViewBag.Error = "Currently unavailable! Try again a few minutes after.";
                return RedirectToAction("AllDrinkList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditDrinkInfo(DrinkViewModel drink)
        {
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(drink);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:8001/api/Drink", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Drink updated!";
                return RedirectToAction("AllDrinkList");
            }
            else
            {
                TempData["Error"] = "Drink is not updated! Try again a few minutes after.";
                return View();
            }

        }
    }
}
