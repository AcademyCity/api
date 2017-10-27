namespace Core.Entity.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public class CouponDto
    {
        [StringLength(40)]
        public string CouponId { get; set; }

        [StringLength(40)]
        public string CouponConfigId { get; set; }
        
        [StringLength(32)]
        public string CouponName { get; set; }

        [StringLength(64)]
        public string CouponImg { get; set; }

        public int? CouponPoint { get; set; }

        [StringLength(2048)]
        public string CouponExplain { get; set; }

        public int CouponNum { get; set; }

        [StringLength(16)]
        public string CouponCode { get; set; }

        [StringLength(64)]
        public string PosKey { get; set; }

        [StringLength(2)]
        public string ValidityMode { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? EffectDate { get; set; }

        public int? ValidDate { get; set; }

        public bool? IsUse { get; set; }

    }
}
