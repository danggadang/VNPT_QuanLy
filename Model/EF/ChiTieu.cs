namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTieu")]
    public partial class ChiTieu
    {
        [StringLength(250)]
        public string ID { get; set; }

        public int? Nam { get; set; }

        public int? Thang { get; set; }

        public int? SoLuongDon { get; set; }

        [Column(TypeName = "money")]
        public decimal? DoanhThu { get; set; }

        public decimal? XuLy { get; set; }

        public decimal? KhenThuong { get; set; }
    }
}
