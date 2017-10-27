namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Point")]
    public partial class Point
    {
        [StringLength(40)]
        public string PointId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        public int? VipPoint { get; set; }
    }
}
