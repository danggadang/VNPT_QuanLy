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
        public int ID { get; set; }

        public int IDHoaDon { get; set; }

        [StringLength(50)]
        public string IDDichVu { get; set; }

        public int? SoLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }
    }
}
