using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortLinksService.Models.MongoEntities;

public class MongoEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("Url")]
    public string Url { get; set; }
    public string Password { get; set; }
    public string ShortUrl { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}