using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("MaterialQualityType")]
    public class MaterialQualityType
    {
        [Key]
        public int MaterialQualityTypeID { get; set; }

        [Required]
        [StringLength(100)]
        public string MaterialQualityTypeName { get; set; }//质量反馈问题类型

        [Required]
        public int InputPerson { get; set; }//数据更新人员

        [Required]
        public DateTime InputDateTime { get; set; }//数据更新时间
    }
}