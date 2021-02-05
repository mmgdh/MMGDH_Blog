using Microsoft.AspNetCore.Mvc.Rendering;
using MMGDH_Blog.Model;
using MMGDH_Blog.ViewModels.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.ViewModels
{
    public class ArticleViewModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string MarkDown { get; set; }
        public string GetHtml { get; set; }
        public string Synopsis { get; set; }
        public DateTime CreateTime { get; set; }
        public string KnowledgePoints { get; set; }
        public string BelongProject { get; set; }
        public string Author { get; set; }
        #region TryCHange展示
        public List<CodeBase> DisplayTags { get; set; }
        public List<String> DisplayTags2 { get; set; }
        public IEnumerable<SelectListItem> projects { get; set; }
        public string CoverImagePath { get; set; }
        public List<string> Tags { get; set; }
        #endregion
    }
}
