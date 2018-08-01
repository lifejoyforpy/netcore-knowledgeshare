using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BoeSj.KnowledgeBase.Infrastructure.Mongo
{
    [BsonIgnoreExtraElements(Inherited = true)]
    public class BaseModel : IMongoEntity<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}