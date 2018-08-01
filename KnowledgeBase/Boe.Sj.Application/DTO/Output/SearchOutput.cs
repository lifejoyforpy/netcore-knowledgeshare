using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Application.DTO.Output
{
   public  class SearchOutput
    {
        public string suggesttag { get; set; }

        public int Views { get; set; }


        public string Title { get; set; }

        public int Likes { get; set; }

        public DateTime CreatTime { get; set; }
        public string PostGuid { get; set; }
    }
}
