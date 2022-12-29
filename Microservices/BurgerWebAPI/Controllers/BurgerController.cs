using BurgerWebAPI.Context;
using BurgerWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BurgerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurgerController : ControllerBase
    {
        private readonly BurgerDbContext _burgerDbContext;
        public BurgerController(BurgerDbContext burgerDbContext)
        {
            _burgerDbContext = burgerDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Burger>> GetBurgerAllList()
        {
            return _burgerDbContext.Burgers;
        }
        [HttpGet("{burger_id:int}")]
        public async Task<ActionResult<Burger>> GetBurgerById(int burger_id)
        {
            var burger = await _burgerDbContext.Burgers.FindAsync(burger_id);
            return burger;
        }

        [HttpPost]
        public async Task<ActionResult<Burger>> AddBurger(Burger burger)
        {
            await _burgerDbContext.Burgers.AddAsync(burger);
            await _burgerDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Burger>> UpdateBurger(Burger burger)
        {
            _burgerDbContext.Burgers.Update(burger);
            await _burgerDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{burger_id:int}")]
        public async Task<ActionResult<Burger>> DeleteBurger(int burger_id)
        {
            var burger = await _burgerDbContext.Burgers.FindAsync(burger_id);
            _burgerDbContext.Remove(burger);
            await _burgerDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
