namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Suggest")]
    public class Suggest
    {
        [StringLength(40)]
        public string SuggestId { get; set; }

        [StringLength(40)]
        public string VipId { get; set; }

        [StringLength(16)]
        public string StoreNo { get; set; }

        [StringLength(64)]
        public string PosNo { get; set; }

        public DateTime? EatDate { get; set; }

        [StringLength(16)]
        public string EatTime { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
