namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string TenKH { get; set; }

        [StringLength(250)]
        public string DiaChi { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(250)]
        public string SDT { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NamSinh { get; set; }
    }
}
