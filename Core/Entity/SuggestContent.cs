namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuggestContent")]
    public class SuggestContent
    {
        [StringLength(40)]
        public string SuggestContentId { get; set; }

        [Required]
        [StringLength(40)]
        public string SuggestId { get; set; }

        [StringLength(8)]
        public string Reply1 { get; set; }

        [StringLength(8)]
        public string Reply2 { get; set; }

        [StringLength(8)]
        public string Reply3 { get; set; }

        [StringLength(8)]
        public string Reply4 { get; set; }

        [StringLength(8)]
        public string Reply5 { get; set; }

        [StringLength(8)]
        public string Reply6 { get; set; }

        [StringLength(8)]
        public string Reply7 { get; set; }

        [StringLength(8)]
        public string Reply8 { get; set; }

        public DateTime? AddTime { get; set; }
    }
}
