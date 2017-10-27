namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PointRecord")]
    public partial class PointRecord
    {
        [StringLength(40)]
        public string PointRecordId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        public int? PointChange { get; set; }

        [StringLength(32)]
        public string PointExplain { get; set; }

        [StringLength(64)]
        public string PosNo { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(40)]
        public string AddMan { get; set; }
    }
}
