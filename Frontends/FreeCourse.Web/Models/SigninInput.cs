using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Models
{
    public class SigninInput
    {
        [Required(ErrorMessage = "Lütfen email adresinizi doldurunuz.")]
        [Display(Name = "Email adresiniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [Display(Name = "Şifreniz")]
        public string Password { get; set; }
        [Display(Name = "Beni hatırla")]
        public bool IsRemember { get; set; }
    }
}
