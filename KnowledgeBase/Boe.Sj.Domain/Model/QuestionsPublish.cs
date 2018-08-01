﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoeSj.KnowledgeBase.Domain.Model
{
    [Table("questionspublish")]
    public class QuestionsPublish
    {
        [Key]
        public int Id { get; set; }

        public string JobNum { get; set; }
        public DateTime CreateTime { get; set; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string BelongDepartment { get; set; } = "";

        /// <summary>
        /// 问题分类
        /// </summary>
        public string QuestionCategory { get; set; }

        /// <summary>
        /// '状态 0：待发布 1：发布中 2：回答中 3：已结问 4:回退'
        /// </summary>
        public int State { get; set; }

        public int Flag { get; set; } = 1;
        public DateTime PostTime { get; set; }
    }
}