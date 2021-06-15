namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RentDetail")]
    public partial class RentDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long RentProductID { get; set; }

        public DateTime? DateExpire { get; set; }

        public DateTime? DateReturn { get; set; }

        public decimal? RentMoney { get; set; }

        public decimal? Penalty { get; set; }

        public decimal? Deposit { get; set; }

        public bool? Status { get; set; }

        public virtual Rent Rent { get; set; }

        public virtual RentProduct RentProduct { get; set; }
    }
}
