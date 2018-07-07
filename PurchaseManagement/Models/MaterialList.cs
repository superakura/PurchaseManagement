using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("MaterialList")]
    public class MaterialList
    {
        [Key]
        public int MaterialListID { get; set; }

        [StringLength(100)]
        public string PGr { get; set; }//W50

        [StringLength(100)]
        public string Typ { get; set; }//PD

        [StringLength(100)]
        public string Factory { get; set; }//7100

        [Required]
        [StringLength(100)]
        public string MaterialNum { get; set; }//物料编码

        [Required]
        [StringLength(200)]
        public string MaterialDesc { get; set; }//物料描述

        [StringLength(100)]
        public string ChangeDate { get; set; }//上次更改日期

        [StringLength(100)]
        public string MaterialType { get; set; }//物料类型

        [Required]
        [StringLength(100)]
        public string MaterialCategoryNum { get; set; }//物料组，【物料分类编码】

        public string MaterialUnit { get; set; }//物料计量单位

        [StringLength(100)]
        public string ValCl { get; set; }

        [StringLength(100)]
        public string Pr { get; set; }

        public decimal Money { get; set; }

        [StringLength(20)]
        public string Currency { get; set; }//CNY

        //状态码
        //0、正常
        //1、标记重复--已审批
        //2、标记重复--待审核
        //3、删除
        public int? StateCode { get; set; }

        public DateTime? BookDate { get; set; }//标记日期

        public int? BookPerson { get; set; }//标记人员

        public DateTime? AuditDate { get; set; }//审核日期

        public int? AuditPerson { get; set; }//审核人员

        [StringLength(200)]
        public string Remark { get; set; }//备注
    }
}