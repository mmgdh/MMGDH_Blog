using MMGDH_Blog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.ViewModels
{
    public class BaseInfoModel
    {
        #region 项目
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        #endregion
        #region CodeBase
        public string Remark { get; set; }
        public string Type { get; set; }
        #endregion
        #region 展示
        public  List<CodeBase> DisplayTags{ get; set; }
        #endregion
    }
}
