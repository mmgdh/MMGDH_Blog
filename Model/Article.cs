using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.Model
{
    public class Article
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string MarkDown { get; set; }
        public string GetHtml { get; set; }
        public string Synopsis { get; set; }
        public DateTime CreateTime { get; set; }
        public string KnowledgePoints { get; set; }
        public int ProjectId { get; set; }
        public string Author { get; set; }
        public string CoverImagePath { get; set; }

    }
}
