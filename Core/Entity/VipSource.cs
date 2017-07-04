namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VipSource")]
    public partial class VipSource
    {
        [StringLength(40)]
        public string VipSourceId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        [StringLength(8)]
        public string SourceName { get; set; }

        [StringLength(40)]
        public string SourceId { get; set; }
    }
}
