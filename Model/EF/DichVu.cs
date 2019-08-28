namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DichVu")]
    public partial class DichVu
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string TenDV { get; set; }

        [StringLength(250)]
        public string IDNhomDV { get; set; }

        public bool? TrangThai { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgaySua { get; set; }
    }
}
