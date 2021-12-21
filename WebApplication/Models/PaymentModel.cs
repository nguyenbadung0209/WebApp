using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class PaymentModel
    {
        [Key]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50)]
        public string ShipName { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Mobile is required")]
        [StringLength(50)]
        public string ShipMobile { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string ShipAddress { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Please enter the correct address")]
        public string ShipEmail { get; set; }
    }
}