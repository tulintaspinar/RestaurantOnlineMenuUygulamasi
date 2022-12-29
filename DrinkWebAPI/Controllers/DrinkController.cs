using DrinkWebAPI.Context;
using DrinkWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace DrinkWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : ControllerBase
    {
        private readonly DrinkDbContext _drinkDbContext;
        public DrinkController(DrinkDbContext context)
        {
            _drinkDbContext = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Drink>> GetDrinkAllList()
        {
            return _drinkDbContext.Drinks;
        }
        [HttpGet("{drink_id:int}")]
        public async Task<ActionResult<Drink>> GetDrinkById(int drink_id)
        {
            var burger = await _drinkDbContext.Drinks.FindAsync(drink_id);
            return burger;
        }

        [HttpPost]

        public async Task<ActionResult<Drink>> AddDrink(Drink burger)
        {
            await _drinkDbContext.Drinks.AddAsync(burger);
            await _drinkDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Drink>> UpdateDrink(Drink burger)
        {
            _drinkDbContext.Drinks.Update(burger);
            await _drinkDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{drink_id:int}")]
        public async Task<ActionResult<Drink>> DeleteDrink(int drink_id)
        {
            var burger = await _drinkDbContext.Drinks.FindAsync(drink_id);
            _drinkDbContext.Remove(burger);
            await _drinkDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
