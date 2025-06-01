using MongoDB.Bson.Serialization.Attributes;

namespace URLShortener.Models;

public class ShortURL
{
    [BsonId]
    public string Code { get; set; } = default!;
    public string OriginalURL { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
