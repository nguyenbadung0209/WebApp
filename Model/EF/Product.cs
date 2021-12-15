namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }
        [Required(ErrorMessage ="Name is requied")]
        [Display(Name = "Product Name")]
        [StringLength(250)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code is requied")]
        [StringLength(10)]
        public string Code { get; set; }
        [Required(ErrorMessage = "Title is requied")]
        [Display(Name = "Title")]
        [StringLength(250)]
        public string MetaTitle { get; set; }
        [Required(ErrorMessage = "Description is requied")]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }
        [Required(ErrorMessage = "Price is requied")]
        public decimal Price { get; set; }

        [Display(Name = "Promotion Price")]
        public decimal? PromotionPrice { get; set; }
        [Display(Name = "VAT")]
        public bool? IncludedVAT { get; set; }
        [Required(ErrorMessage = "Quantity is requied")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Product Category is requied")]
        [Display(Name = "Product Category")]
        public long CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }
    }
}
