namespace PurchaseManagement.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DB : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“DB”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“PurchaseManagement.Models.DB”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“DB”
        //连接字符串。
        public DB()
            : base("name=DB")
        {
        }

        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<DeptInfo> DeptInfo { get; set; }//部门信息表
        public virtual DbSet<UserInfo> UserInfo { get; set; }//用户信息表
        public virtual DbSet<AuthorityInfo> AuthorityInfo { get; set; }//权限信息表
        public virtual DbSet<RoleAuthority> RoleAuthority { get; set; }//角色权限关系表
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }//角色信息表
        public virtual DbSet<UserRole> UserRole { get; set; }//用户角色关系表
        public virtual DbSet<NoticeInfo> NoticeInfo { get; set; }//通知公告信息表
        public virtual DbSet<UserDept> UserDept { get; set; }//用户部门关系表
        public virtual DbSet<Log> Log { get; set; }//日志信息表

        public virtual DbSet<MaterialQualityFeedback> MaterialQualityFeedback { get; set; }//物质质量反馈表
        public virtual DbSet<MaterialQualityType> MaterialQualityType { get; set; }//物质质量类型表
        public virtual DbSet<SupplierAppraise> SupplierAppraise { get; set; }//供应商日常考核记录单--暂时不用

        public virtual DbSet<Crud> Crud { get; set; }//Crud测试表

        public virtual DbSet<CategoryList5497> CategoryList5497 { get; set; }//5497分类表
        public virtual DbSet<MaterialList> MaterialList { get; set; }//物资编码信息表
        //public virtual DbSet<RepectMaterialApply> RepectMaterialApply { get; set; }//重复编码申请审批表
        //public virtual DbSet<ErrorMaterialApply> ErrorMaterialApply { get; set; }//缺失编码审批表
    }
}