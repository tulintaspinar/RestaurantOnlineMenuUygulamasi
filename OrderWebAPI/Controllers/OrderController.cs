using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OrderWebAPI.Models;

namespace OrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _orderCollection;
        public OrderController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            _orderCollection = database.GetCollection<Order>("order");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderAllList()
        {
            return await _orderCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(string id)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderId, id);
            return await _orderCollection.Find(filter).SingleOrDefaultAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            await _orderCollection.InsertOneAsync(order);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filter, order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.OrderId, id);
            await _orderCollection.DeleteOneAsync(filter);
            return Ok();
        }
    }
}
