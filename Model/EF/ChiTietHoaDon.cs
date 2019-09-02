namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? MaKH { get; set; }

        [StringLength(250)]
        public string TenKH { get; set; }

        [StringLength(100)]
        public string SDT { get; set; }

        [StringLength(250)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string Mail { get; set; }

        [StringLength(250)]
        public string TenDV { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public int? IDNhanVien { get; set; }

        public DateTime? NgayTao { get; set; }
    }
}
