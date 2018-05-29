using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("SupplierAppraise")]
    public class SupplierAppraise
    {
        [Key]
        public int SupplierAppraiseID { get; set; }//主键ID

        [Required][StringLength(100)]
        public string SupplierName { get; set; }//供应商名称

        [Required][StringLength(100)]
        public string ContractNumber { get; set; }//合同号或报价单或其他

        [Required][StringLength(100)]
        public string AppraiseDepend { get; set; }//考核标准

        public decimal AppraiseMoney { get; set; }//考核金额

        public float AppraiseScore { get; set; }//考核分数

        [Required][StringLength(500)]
        public string AppraiseContent { get; set; }//考核内容

        public int AppraisePersonID { get; set; }//考核人ID

        public DateTime AppraiseInputTime { get; set; }//考核单生成时间

        [StringLength(10)]
        public string AppraiseTypeQuality { get; set; }//考核类型--质量

        [StringLength(10)]
        public string AppraiseTypeContract{ get; set; }//考核类型--合同

        [StringLength(10)]
        public string AppraiseTypeService { get; set; }//考核类型--服务

        [StringLength(10)]
        public string AppraiseTypePriceResponse { get; set; }//考核类型--价格响应

        [StringLength(10)]
        public string AppraiseTypeSincerity { get; set; }//考核类型--诚信

        [StringLength(10)]
        public string AppraiseTypeOther { get; set; }//考核类型--其他
    }
}