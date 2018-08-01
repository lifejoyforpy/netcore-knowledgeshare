using MongoDB.Bson.Serialization.Attributes;

namespace BoeSj.KnowledgeBase.Infrastructure.Mongo
{
    public interface IMongoEntity<TKey>
    {
        /// <summary>
        ///     主键
        /// </summary>
        [BsonId]
        TKey Id { get; set; }
    }
}