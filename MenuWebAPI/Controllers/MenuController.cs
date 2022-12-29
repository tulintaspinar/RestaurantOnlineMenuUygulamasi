using MenuWebAPI.Context;
using MenuWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MenuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuDbContext _menuDbContext;
        public MenuController(MenuDbContext menuDbContext)
        {
            _menuDbContext = menuDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetMenuAllList()
        {
            return _menuDbContext.Menus;
        }
        [HttpGet("{menu_id:int}")]
        public async Task<ActionResult<Menu>> GetMenuById(int food_id)
        {
            var food = await _menuDbContext.Menus.FindAsync(food_id);
            return food;
        }
    }
}
