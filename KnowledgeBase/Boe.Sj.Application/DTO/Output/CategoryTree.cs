﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Application.DTO.Output
{
    public class CategoryTree
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public bool Children { get; set; } = true;

        public State State { get; set; }

        //public 
    }

    public class State
    {
        public string CategoryDescription { get; set; }

        public string Departments { get; set; }

        //public int IsLocked { get; set; }

        public int Id { get; set; }

        public bool opened { get; set; } = true; 
    }

    public class CategoryTree2
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public List<CategoryTree2> Children { get; set; } = new List<CategoryTree2>();

        public State State { get; set; }

        //public 
    }
}
