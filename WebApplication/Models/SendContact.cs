﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class SendContact
    {
        [Key]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter the correct address")]
        public string Email { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [Display(Name = "Message")]
        public string Content { get; set; }

    }
}