namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Price")]
    public partial class Price
    {
        public long ID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [StringLength(120)]
        public string Name { get; set; }

        public decimal? PriceValue { get; set; }

        public decimal? PromotionPrice { get; set; }

        public long? ProductID { get; set; }

        public bool? Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
