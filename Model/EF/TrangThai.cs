namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThai")]
    public partial class TrangThai
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string TenTrangThai { get; set; }
    }
}
