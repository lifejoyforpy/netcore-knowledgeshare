using BoeSj.KnowledgeBase.Infrastructure.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Domain.Model.Mongo
{
    public class Log : BaseModel
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
        public string Content { get; set; }
        public string PostGuid { get; set; }
        /// <summary>
        /// 1:发布 2：回退 3：回复 4：采纳
        /// </summary>
        public int Type { get; set; }
        public string JobNum { get; set; }
        public string Department { get; set; }
        public string Author { get; set; }
    }
}
