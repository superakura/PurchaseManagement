using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace PurchaseManagement.Controllers
{
    public class MaterialCodeController : Controller
    {
        //MaterialCode
        Models.DB db = new Models.DB();

        //物资编码查询
        public ViewResult Search()
        {
            return View();
        }

        //5497编码和ERP物资编码数据导入
        public ViewResult Input()
        {
            return View();
        }

        //已提报信息
        public ViewResult MySubmit()
        {
            return View();
        }

        //错码信息审核
        public ViewResult ErrorCode()
        {
            return View();
        }

        //重码信息提报信息审核
        public ViewResult Check()
        {
            return View();
        }

        //上传5497分类文件
        [HttpPost]
        public JsonResult Upload5497FileAjax(HttpPostedFileBase file5497Upload)
        {
            if (file5497Upload != null)
            {
                var fileExt = Path.GetExtension(file5497Upload.FileName).ToLower();
                var newName = "5497_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                var filePath = Request.MapPath("~/FileUpload");
                var fullName = Path.Combine(filePath, newName);
                try
                {
                    file5497Upload.SaveAs(fullName);
                    return Json(new { state = "ok", fileName = newName });
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json("noFile");
        }

        //上传erp编码文件
        [HttpPost]
        public JsonResult UploadErpFileAjax(HttpPostedFileBase fileErpUpload)
        {
            if (fileErpUpload != null)
            {
                var fileExt = Path.GetExtension(fileErpUpload.FileName).ToLower();
                var newName = "Erp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
                var filePath = Request.MapPath("~/FileUpload");
                var fullName = Path.Combine(filePath, newName);
                try
                {
                    fileErpUpload.SaveAs(fullName);
                    return Json(new { state = "ok", fileName = newName });
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }
            }
            return Json("noFile");
        }

        //上传后，更新物资编码数据
        [HttpPost]
        public JsonResult UpdateMaterialList()
        {
            try
            {
                var fileErp = Request.Form["fileErp"].ToString();
                var file5497 = Request.Form["file5497"].ToString();

                var sw = new Stopwatch();
                sw.Start();
                System.Data.DataTable tbErp = App_Code.Commen.ReadExcel(Server.MapPath("~/FileUpload/") + fileErp);
                for (int i = 0; i < tbErp.Rows.Count; i++)
                {
                    var insertErp = new System.Text.StringBuilder();
                    SqlParameter[] paras = {
                        new SqlParameter("@PGr",SqlDbType.NVarChar,100),
                        new SqlParameter("@MaterialNum",SqlDbType.NVarChar,100),
                        new SqlParameter("@MaterialDesc",SqlDbType.NVarChar,200),
                        new SqlParameter("@MaterialCategoryNum",SqlDbType.NVarChar,100),
                        new SqlParameter("@ChangeDate",SqlDbType.NVarChar,100),
                        new SqlParameter("@MaterialUnit",SqlDbType.NVarChar,100),
                        new SqlParameter("@ValCl",SqlDbType.NVarChar,100)
                    };
                    paras[0].Value = tbErp.Rows[i]["PGr"].ToString();
                    paras[1].Value = tbErp.Rows[i]["物料"].ToString();
                    paras[2].Value = tbErp.Rows[i]["物料描述"].ToString();
                    paras[3].Value = tbErp.Rows[i]["物料组"].ToString();
                    paras[4].Value = tbErp.Rows[i]["上次更改"].ToString();
                    paras[5].Value = tbErp.Rows[i]["计"].ToString();
                    paras[6].Value = tbErp.Rows[i]["ValCl"].ToString();

                    insertErp.Append(@"insert into MaterialList
                    (PGr,MaterialNum,MaterialDesc,MaterialCategoryNum,ChangeDate,MaterialUnit,ValCl) ");
                    insertErp.Append("values('@PGr','@MaterialNum','@MaterialDesc','@MaterialCategoryNum','@ChangeDate','@MaterialUnit','@ValCl')");
                    db.Database.ExecuteSqlCommand(insertErp.ToString());
                }

                sw.Stop();
                return Json(new { fileErp = fileErp, file5497 = file5497, sw = sw.Elapsed });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //上传后，更新物资编码数据,使用sqlBulkCopy方法
        [HttpPost]
        public JsonResult UpdateMaterialListBulkCopy()
        {
            try
            {
                var fileErp = Request.Form["fileErp"].ToString();
                var file5497 = Request.Form["file5497"].ToString();

                System.Data.DataTable DtErp = App_Code.Commen.ReadExcel(Server.MapPath("~/FileUpload/") + fileErp);
                System.Data.DataTable Dt5497 = App_Code.Commen.ReadExcel(Server.MapPath("~/FileUpload/") + file5497);

                var connStr = ConfigurationManager.ConnectionStrings["DB"].ToString();

                var swErp = new Stopwatch();
                swErp.Start();
                using (SqlBulkCopy copyErp = new SqlBulkCopy(connStr))
                {
                    copyErp.DestinationTableName = "MaterialList";
                    copyErp.ColumnMappings.Add("PGr", "PGr");
                    copyErp.ColumnMappings.Add("物料", "MaterialNum");
                    copyErp.ColumnMappings.Add("物料描述", "MaterialDesc");
                    copyErp.ColumnMappings.Add("物料组", "MaterialCategoryNum");
                    copyErp.ColumnMappings.Add("上次更改", "ChangeDate");
                    copyErp.ColumnMappings.Add("计", "MaterialUnit");
                    copyErp.ColumnMappings.Add("ValCl", "ValCl");
                    copyErp.WriteToServer(DtErp);
                }
                swErp.Stop();

                var sw5497 = new Stopwatch();
                sw5497.Start();
                using (SqlBulkCopy copy5496 = new SqlBulkCopy(connStr))
                {
                    copy5496.DestinationTableName = "CategoryList5497";
                    copy5496.ColumnMappings.Add("大类名称", "FirstCategory");
                    copy5496.ColumnMappings.Add("中类名称", "SecondCategory");
                    copy5496.ColumnMappings.Add("小类名称", "ThirdCategory");
                    copy5496.ColumnMappings.Add("品名", "Product");
                    copy5496.ColumnMappings.Add("型号规格规范", "TypeSpecification");
                    copy5496.ColumnMappings.Add("基本计量单位", "MeasurementUnit");

                    copy5496.ColumnMappings.Add("主属性-1名称", "Attribute01Name");
                    copy5496.ColumnMappings.Add("主属性1-单位", "Attribute01Unit");

                    copy5496.ColumnMappings.Add("主属性-2名称", "Attribute02Name");
                    copy5496.ColumnMappings.Add("主属性2-单位", "Attribute02Unit");

                    copy5496.ColumnMappings.Add("主属性-3名称", "Attribute03Name");
                    copy5496.ColumnMappings.Add("主属性3-单位", "Attribute03Unit");

                    copy5496.ColumnMappings.Add("主属性-4名称", "Attribute04Name");
                    copy5496.ColumnMappings.Add("主属性4-单位", "Attribute04Unit");

                    copy5496.ColumnMappings.Add("主属性-5名称", "Attribute05Name");
                    copy5496.ColumnMappings.Add("主属性5-单位", "Attribute05Unit");

                    copy5496.ColumnMappings.Add("主属性-6名称", "Attribute06Name");
                    copy5496.ColumnMappings.Add("主属性6-单位", "Attribute06Unit");

                    copy5496.ColumnMappings.Add("主属性-7名称", "Attribute07Name");
                    copy5496.ColumnMappings.Add("主属性7-单位", "Attribute07Unit");

                    copy5496.ColumnMappings.Add("主属性-8名称", "Attribute08Name");
                    copy5496.ColumnMappings.Add("主属性8-单位", "Attribute08Unit");

                    copy5496.ColumnMappings.Add("主属性-9名称", "Attribute09Name");
                    copy5496.ColumnMappings.Add("主属性9-单位", "Attribute09Unit");

                    copy5496.ColumnMappings.Add("主属性-10名称", "Attribute10Name");
                    copy5496.ColumnMappings.Add("主属性10-单位", "Attribute10Unit");

                    copy5496.WriteToServer(Dt5497);
                }
                sw5497.Stop();

                return Json(new {
                    fileErp = fileErp,
                    file5497 = file5497,
                    swErp = swErp.Elapsed.TotalSeconds,
                    sw5497 = sw5497.Elapsed.TotalSeconds
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult GetMaterialList()
        {
            try
            {
                int.TryParse(Request.Form["limit"], out var limit);
                int.TryParse(Request.Form["offset"], out var offset);
                var materialNum = Request.Form["materialNum"];//物资编码
                var materialNumChild = Request.Form["materialNumChild"];//物资组
                var materialDesc = Request.Form["materialDesc"];//物资描述
                var valCl = Request.Form["valCl"];//valCl
                var pGr = Request.Form["pGr"];//pGr

                var result = from m in db.MaterialList
                             orderby m.MaterialListID
                             select new {
                                 m.MaterialListID,
                                 m.PGr,
                                 m.Typ,
                                 m.Factory,
                                 m.MaterialUnit,
                                 m.Money,
                                 m.Currency,
                                 m.MaterialNum,
                                 m.MaterialDesc,
                                 m.MaterialCategoryNum,
                                 m.ChangeDate,
                                 m.ValCl
                             };
                if (!string.IsNullOrEmpty(materialNum))
                {
                    result = result.Where(w => w.MaterialNum.Contains(materialNum));
                }
                if (!string.IsNullOrEmpty(materialNumChild))
                {
                    result = result.Where(w => w.MaterialCategoryNum.Contains(materialNumChild));
                }
                if (!string.IsNullOrEmpty(materialDesc))
                {
                    result = result.Where(w => w.MaterialDesc.Contains(materialDesc));
                }
                if (!string.IsNullOrEmpty(valCl))
                {
                    result = result.Where(w => w.ValCl.Contains(valCl));
                }
                if (!string.IsNullOrEmpty(pGr))
                {
                    result = result.Where(w => w.PGr.Contains(pGr));
                }
                return Json(new { total = result.Count(), rows = result.Skip(offset).Take(limit).ToList() });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}