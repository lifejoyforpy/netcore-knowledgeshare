using BoeSj.KnowledgeBase.Infrastructure.ExpressionTree;
using BoeSj.KnowledgeBase.Infrastructure.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace BoeSj.KnowledgeBase.Domain.Model.Mongo
{
    public class Post : BaseModel
    {
        public string Title { get; set; }
        public List<string> Tag { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        // 0 知识分享 ,1问答
        public int Type { get; set; } = 0;

        //关联主表的guid
        public string PostGuid { get; set; }

        //点赞次数
        public int Likes { get; set; } = 0;

        public int Views { get; set; } = 0;

        /// <summary>
        /// 0 仅自己可见 1 部门可见 2 公开
        /// </summary>
        public int Locked { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        [IgnoreKey]
        public int State { get; set; }
    }

    public class Reply : BaseModel
    {
        public string JobNum { get; set; }
        public string Author { get; set; }
        public int Likes { get; set; }

        public List<Attachment> Attachment { get; set; } = new List<Mongo.Attachment>();
        public string Content { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        public int DataFlag { get; set; } = 1;

        /// <summary>
        /// 1:best
        /// </summary>
        public int IsBest { get; set; } = 0;

        //关联主表的guid
        public string PostGuid { get; set; }

        public List<object> Comments { get; set; } = new List<object>();
    }

    public class Attachment
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Suffix { get; set; }
    }
}