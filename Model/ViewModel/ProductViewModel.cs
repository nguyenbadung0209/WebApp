﻿    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }    
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string CateName { get; set; }
        public string CateMetaTitle { get; set; }
        public bool Status { get; set; }
    }
}
