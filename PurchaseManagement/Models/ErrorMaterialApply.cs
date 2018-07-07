using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("ErrorMaterialApply")]
    public class ErrorMaterialApply
    {
        [Key]
        public int ErrorMaterialApplyID { get; set; }

        public int MaterialID { get; set; }//物料ID

        public DateTime? BookDate { get; set; }//标记日期

        public int BookPerson { get; set; }//标记人员

        [Required]
        [StringLength(100)]
        public string BookType { get; set; }//标记类型：属性不完整的物资编码，标记删除；

        [StringLength(200)]
        public string Remark { get; set; }//备注
    }
}