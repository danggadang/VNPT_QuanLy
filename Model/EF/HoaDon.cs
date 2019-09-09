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

        public int IDHoaDon { get; set; }

        public int? IDKhachHang { get; set; }

        public int? IDDichVu { get; set; }

        public int? SoLuong { get; set; }

        public int? TongTien { get; set; }

        public int? IDNhanVien { get; set; }
    }
}
