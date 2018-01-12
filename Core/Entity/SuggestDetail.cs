namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuggestDetail")]
    public class SuggestDetail
    {
        [StringLength(40)]
        public string SuggestDetailId { get; set; }

        [StringLength(40)]
        public string SuggestId { get; set; }

        [StringLength(8)]
        public string Reply { get; set; }

        [StringLength(512)]
        public string ReplyDetail { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
