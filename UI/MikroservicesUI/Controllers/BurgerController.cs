using Microsoft.AspNetCore.Mvc;
using MikroservicesUI.Models;
using Newtonsoft.Json;
using System.Text;

namespace MikroservicesUI.Controllers
{
    public class BurgerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BurgerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> AllBurgerList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:8001/api/Burger");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<List<BurgerViewModel>>(data);
                return View(convertData);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddBurger()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBurger(BurgerViewModel burger)
        {
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(burger);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://localhost:8001/api/Burger", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Burger updated!";
                return RedirectToAction("AllBurgerList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteBurgerInfo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:8001/api/Burger/{id}");
            if (responseMessage.IsSuccessStatusCode)
                return RedirectToAction("AllBurgerList");
            else
            {
                TempData["Error"] = "Burger is not deleted!";
                return RedirectToAction("AllBurgerList");
            }

        }

        [HttpGet]
        public async Task<IActionResult> EditBurgerInfo(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:8001/api/Burger/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var data = await responseMessage.Content.ReadAsStringAsync();
                var convertData = JsonConvert.DeserializeObject<BurgerViewModel>(data);
                return View(convertData);
            }

            else
            {
                ViewBag.Error = "Currently unavailable! Try again a few minutes after.";
                return RedirectToAction("AllBurgerList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditBurgerInfo(BurgerViewModel burger)
        {
            var client = _httpClientFactory.CreateClient();
            var data = JsonConvert.SerializeObject(burger);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("http://localhost:8001/api/Burger", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["Success"] = "Burger updated!";
                return RedirectToAction("AllBurgerList");
            }
            else
            {
                TempData["Error"] = "Burger is not updated! Try again a few minutes after.";
                return View();
            }

        }
    }
}
