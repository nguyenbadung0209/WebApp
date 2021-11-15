using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter user name")]
        [StringLength(50, ErrorMessage = "The maximum length of the use name is 50 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(32, ErrorMessage = "The maximum length of the password is 32 characters")]
        public string PassWord { get; set; }

        public bool RememberMe { get; set; }
    }
}