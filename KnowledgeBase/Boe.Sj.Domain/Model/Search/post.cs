﻿using Nest;
using System;
using System.Collections.Generic;

namespace BoeSj.KnowledgeBase.Domain.Model.Search
{

    public class post
    {

        public string Title { get; set; }

        public int Views{ get; set; }

        public int Likes{ get; set; }

     

        //[Text(Analyzer = "ik_max_word")]
        public string Category { get; set; }

        public string Content { get; set; }

        public DateTime CreateTime { get; set; }

        // 0 知识分享 ,1问答
        public int Type { get; set; } = 0;
        /// <summary>
        /// 0 仅自己可见 1 部门可见 2 公开
        /// </summary>
        public int Locked { get; set; } = 0;

        public int State { get; set; } = 0;

        //关联主表的guid
        public string PostGuid { get; set; }

        public List<string> Tag { get; set; }

        public CompletionField suggest { get; set; }
    }
}
