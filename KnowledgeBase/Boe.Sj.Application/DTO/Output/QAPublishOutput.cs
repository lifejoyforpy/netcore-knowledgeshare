using System;
using System.Collections.Generic;

namespace BoeSj.KnowledgeBase.Application.DTO.Output
{
    public class QAInfoOutput
    {
        
        public string Guid { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string QuestionCategory { get; set; }
        public string BelongDepartment { get; set; } = "";
        public List<FileInfo> Files { get; set; } = new List<FileInfo>();
        public int State { get; set; }
        public string Author { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime PostTime { get; set; }
        public BackInfo BackInfo { get; set; }
    }

    public class FileInfo
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
    public class BackInfo
    {
        public string Content { get; set; }
        public string JobNum { get; set; }
        public string Department { get; set; }
        public string Author { get; set; }
    }
}