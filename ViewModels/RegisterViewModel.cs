using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMGDH_Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密    码")]
        public string PassWord { get; set; }
    }
}
