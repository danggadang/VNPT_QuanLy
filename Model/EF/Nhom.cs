namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nhom")]
    public partial class Nhom
    {
        [StringLength(250)]
        public string ID { get; set; }

        [StringLength(250)]
        public string Ten { get; set; }
    }
}
