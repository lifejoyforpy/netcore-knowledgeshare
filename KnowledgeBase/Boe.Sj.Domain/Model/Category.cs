using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BoeSj.KnowledgeBase.Domain.Model
{
    [Table("category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Guid { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; } = "";

        public string Departments { get; set; } = "";

        //public int IsLocked { get; set; } = 0;

        public string CreatedBy { get; set; } = "";

        public string CreatedByJobNo { get; set; } = "";

        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public string ParentGuid { get; set; }
    }
}
