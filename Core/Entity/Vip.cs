namespace Core.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vip")]
    public partial class Vip
    {
        [StringLength(40)]
        public string VipId { get; set; }

        [StringLength(16)]
        public string VipCode { get; set; }

        [StringLength(16)]
        public string VipName { get; set; }

        public bool? VipSex { get; set; }

        public DateTime? VipBirthday { get; set; }

        [StringLength(16)]
        public string VipPhone { get; set; }

        [StringLength(16)]
        public string VipCountry { get; set; }

        [StringLength(16)]
        public string VipProvince { get; set; }

        [StringLength(16)]
        public string VipCity { get; set; }

        [StringLength(256)]
        public string VipHeadImg { get; set; }

        [StringLength(2)]
        public string Status { get; set; }

        public DateTime? AddTime { get; set; }

        [StringLength(40)]
        public string AddMan { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(40)]
        public string ModifyMan { get; set; }

        [StringLength(40)]
        public string Sign { get; set; }
    }
}
