namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrangThaiHopDong")]
    public partial class TrangThaiHopDong
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Ten { get; set; }

        [StringLength(250)]
        public string Mau { get; set; }
    }
}
