namespace WindowsFormsApp1.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [StringLength(100)]
        public string productID { get; set; }

        [Required]
        public string productName { get; set; }

        public string productUnit { get; set; }

        public decimal productCost { get; set; }

        public int? productManufacturer { get; set; }

        public int? productProvider { get; set; }

        public int? productCategory { get; set; }

        public byte? productMaxDiscountAmount { get; set; }

        public byte? productActiveDiscountAmount { get; set; }

        public int productQuantityInStock { get; set; }

        public string productDescription { get; set; }

        public string productPicture { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
