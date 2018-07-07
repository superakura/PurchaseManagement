using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PurchaseManagement.Models
{
    [Table("CategoryList5497")]
    public class CategoryList5497
    {
        [Key]
        public int CategoryListID { get; set; }

        [StringLength(100)]
        public string FirstCategory { get; set; }//大类

        [StringLength(100)]
        public string FirstCategoryNum { get; set; }//大类编码

        [StringLength(100)]
        public string FirstCategoryName { get; set; }//大类名称

        [StringLength(100)]
        public string SecondCategory { get; set; }//中类

        [StringLength(100)]
        public string SecondCategoryNum { get; set; }//中类编码

        [StringLength(100)]
        public string SecondCategoryName { get; set; }//中类名称

        [StringLength(100)]
        public string ThirdCategory { get; set; }//小类

        [StringLength(100)]
        public string ThirdCategoryNum { get; set; }//小类编码

        [StringLength(100)]
        public string ThirdCategoryName { get; set; }//小类名称

        [StringLength(100)]
        public string Product { get; set; }//品名

        [StringLength(100)]
        public string ProductNum { get; set; }//品名编码

        [StringLength(200)]
        public string ProductName { get; set; }//品名名称

        [StringLength(200)]
        public string TypeSpecification { get; set; }//型号规格规范

        [StringLength(100)]
        public string MeasurementUnit { get; set; }//基本计量单位

        [StringLength(100)]
        public string Attribute01Name { get; set; }//主属性1-名称

        [StringLength(100)]
        public string Attribute01Unit { get; set; }//主属性1-单位

        [StringLength(100)]
        public string Attribute02Name { get; set; }//主属性2-名称

        [StringLength(100)]
        public string Attribute02Unit { get; set; }//主属性2-单位

        [StringLength(100)]
        public string Attribute03Name { get; set; }//主属性3-名称

        [StringLength(100)]
        public string Attribute03Unit { get; set; }//主属性3-单位

        [StringLength(100)]
        public string Attribute04Name { get; set; }//主属性4-名称

        [StringLength(100)]
        public string Attribute04Unit { get; set; }//主属性4-单位

        [StringLength(100)]
        public string Attribute05Name { get; set; }//主属性5-名称

        [StringLength(100)]
        public string Attribute05Unit { get; set; }//主属性5-单位

        [StringLength(100)]
        public string Attribute06Name { get; set; }//主属性6-名称

        [StringLength(100)]
        public string Attribute06Unit { get; set; }//主属性6-单位

        [StringLength(100)]
        public string Attribute07Name { get; set; }//主属性7-名称

        [StringLength(100)]
        public string Attribute07Unit { get; set; }//主属性7-单位

        [StringLength(100)]
        public string Attribute08Name { get; set; }//主属性8-名称

        [StringLength(100)]
        public string Attribute08Unit { get; set; }//主属性8-单位

        [StringLength(100)]
        public string Attribute09Name { get; set; }//主属性9-名称

        [StringLength(100)]
        public string Attribute09Unit { get; set; }//主属性9-单位

        [StringLength(100)]
        public string Attribute10Name { get; set; }//主属性10-名称

        [StringLength(100)]
        public string Attribute10Unit { get; set; }//主属性10-单位
    }
}