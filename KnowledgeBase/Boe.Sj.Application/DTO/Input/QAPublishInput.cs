﻿using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using System.Collections.Generic;

namespace BoeSj.KnowledgeBase.Application.DTO.Input
{
    public class QAPublishInput
    {
        public string Guid { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public string QuestionCategory { get; set; }
        public string BelongDepartment { get; set; } = "";
        public string TempGuid { get; set; }

        public List<Attachment> Attachment { get; set; }
    }

}