﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Application.DTO.Input
{
    public class SearchInput:PageModel
    {
        public string Query { get; set; }

       
        public string Type { get; set; } = "0";

        public int Sort { get; set; } = 0;

        public string Locked { get; set; } = "2";

        public string State { get; set; } = "0";
    }

    public enum SearchSort {
        Views,
        Recent,     
        Likes
    }
}
