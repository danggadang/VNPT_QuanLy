namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhomDV")]
    public partial class NhomDV
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string TenNhom { get; set; }
    }
}
