using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("MaterialQualityFeedback")]
    public class MaterialQualityFeedback
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string FeedbackTitle { get; set; }//反馈单标题

        [Required]
        public int MaterialQualityTypeID { get; set; }//问题类型ID

        [Required]
        [StringLength(200)]
        public string SupplierName { get; set; }//供应商名称

        [StringLength(200)]
        public string MaterialNum { get; set; }//ERP计划预留号/物资编码

        [StringLength(200)]
        public string ErpOrderNum { get; set; }//ERP订单号

        [Required]
        [StringLength(1000)]
        public string FeedbackContent { get; set; }//问题内容

        [Required]
        public int InputPersonID { get; set; }//提交人员ID

        [Required]
        [StringLength(100)]
        public string InputPersonName { get; set; }//提交人员姓名

        [StringLength(50)]
        public string InputPersonPhone { get; set; }//提交人员联系方式固定电话

        [StringLength(50)]
        public string InputPersonMobile { get; set; }//提交人员联系方式移动

        [Required]
        public DateTime InputDateTime { get; set; } //提交时间

        [Required]
        public int InputDeptID { get; set; }//提交部门ID

        public int AcceptPersonID { get; set; }//接收人员ID

        [StringLength(100)]
        public string AcceptPersonName { get; set; }//接收人员姓名

        public DateTime? AcceptDateTime { get; set; }//接收时间，可空类型

        public int ReplyPersonID { get; set; }//回复人员ID

        [StringLength(100)]
        public string ReplyPersonName { get; set; }//回复人员姓名

        public DateTime? ReplyDateTime { get; set; }//回复时间，可空类型

        [StringLength(1000)]
        public string ReplyContent { get; set; }//回复内容

        public int CheckPersonID { get; set; }//审核人员ID

        [StringLength(100)]
        public string CheckPersonName { get; set; }//审核人员姓名

        public DateTime? CheckDateTime { get; set; }//审核时间，可空类型

        //反馈单状态信息
        //待提交、待接收、接收回退、接收完成、回复完毕、回复回退、审核通过、审核回退、考核单已生成
        [StringLength(50)]
        public string FeedBackState { get; set; }

        [StringLength(50)]
        public string AppraiseBillState { get; set; }//考核单据状态，【已生成、待生成】

        public DateTime AppraiseBillInputTime { get; set; }//考核单据生成时间

        public int AppraiseBillNum { get; set; }//考核单据编号
    }
}