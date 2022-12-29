using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OrderWebAPI.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId, BsonElement("_OrderId"), BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
    }
}
