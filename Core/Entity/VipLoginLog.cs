namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VipLoginLog")]
    public partial class VipLoginLog
    {
        [StringLength(40)]
        public string VipLoginLogId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        public DateTime? LoginTime { get; set; }

        [StringLength(16)]
        public string LoginIP { get; set; }
    }
}
