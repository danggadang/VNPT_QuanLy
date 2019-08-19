namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(250)]
        public string TenNV { get; set; }

        [StringLength(50)]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        public string Mail { get; set; }

        public int? IDNhom { get; set; }

        [StringLength(150)]
        public string Anh { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgaySua { get; set; }
    }
}
