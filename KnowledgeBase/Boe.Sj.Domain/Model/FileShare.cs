using BoeSj.KnowledgeBase.Infrastructure.ExpressionTree;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoeSj.KnowledgeBase.Domain.Model
{
    [Table("fileshare")]
    public class FileShare
    {
        [IgnoreKey]
        public int Id { get; set; }
        [Key]
        public string Guid { get; set; }

        public string Title { get; set; }
        public string JobNum { get; set; }

        public string Author { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 0 草稿 1 已发布
        /// </summary>
        public int Status { get; set; } = 0;
        public int Flag { get; set; } = 0;
        //public int Views { get; set; } = 0;

        //public int Likes { get; set; } = 0;
    }
}