using System;
using System.Collections.Generic;

namespace BoeSj.KnowledgeBase.Application.DTO.OutPut
{
    public class KnowledegeShareDetail
    {
        public string Id { get; set; }
        public string Guid { get; set; }

        //public string Title { get; set; }
        public string JobNum { get; set; }

        public string Author { get; set; }

        public DateTime CreateTime { get; set; }

        public int Status { get; set; } = 0;

        public int Views { get; set; } = 0;

        public int Likes { get; set; } = 0;

        public int Locked { get; set; } = 0;

        public string Title { get; set; }
        public List<string> Tag { get; set; }

        public string Category { get; set; }

        public string Content { get; set; }

        public int Type { get; set; } = 0;

        public string PostGuid { get; set; }
    }
}