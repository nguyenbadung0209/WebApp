using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }


        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 5)]
        public string UserName { set; get; }

        
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Password length is at least 6 characters.")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { set; get; }

        
        [Compare("Password", ErrorMessage = "Password was wrong!")]
        public string ConfirmPassword { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }

        
        public string Address { set; get; }

        [StringLength(50)]
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter the correct address")]
        public string Email { set; get; }

        
        public string Phone { set; get; }

        
        public string ProvinceID { set; get; }


        
        public string DistrictID { set; get; }
    }
}