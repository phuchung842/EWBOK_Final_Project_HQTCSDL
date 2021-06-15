namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public long ID { get; set; }

        public long? UserID { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }

        [StringLength(1000)]
        public string Exception { get; set; }
    }
}
