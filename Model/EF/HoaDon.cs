namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string MaKH { get; set; }

        public long? SoLuong { get; set; }

        [StringLength(250)]
        public string TenKH { get; set; }

        [StringLength(250)]
        public string SDT { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTienDV { get; set; }

        [Column(TypeName = "money")]
        public decimal? GiamGia { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        [StringLength(250)]
        public string IDNhanVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayXuLy { get; set; }
    }
}
