using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("RepectMaterialApply")]
    public class RepectMaterialApply
    {
        [Key]
        public int RepectMaterialApplyID { get; set; }

        public int MaterialID { get; set; }//物料ID

        public int RepectMaterialID { get; set; }//重复物料ID

        public DateTime? BookDate { get; set; }//标记日期

        public int BookPerson { get; set; }//标记人员

        [Required]
        [StringLength(100)]
        public string BookType { get; set; }//标记类型，1、重复；2、作废删除；

        [StringLength(200)]
        public string ApplyDesc { get; set; }//申请描述
    }
}