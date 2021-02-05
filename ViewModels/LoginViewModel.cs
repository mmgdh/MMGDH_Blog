using System.ComponentModel.DataAnnotations;

namespace MMGDH_Blog.ViewModels
{
    public class LoginViewModel
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
