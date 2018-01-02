using Infrastructure;
using Infrastructure.Entity;
using Repository.Entity;
using System.Data.Entity;

namespace Repository
{
    public class EntityContext : BaseEntityContext
    {

        #region 数据集
        /// <summary>
        /// 调试/异常记录
        /// </summary>
        public DbSet<Sys_DebugLog> Sys_DebugLog { get; set; }
        public DbSet<Sys_BizLog> Sys_BizLog { get; set; }

        /// <summary>
        /// 管理后台-菜单
        /// </summary>
        public DbSet<M_Menu> M_Menu { get; set; }
        /// <summary>
        /// 管理后台-角色
        /// </summary>
        public DbSet<M_Role> M_Role { get; set; }
        /// <summary>
        /// 管理后台-管理员
        /// </summary>
        public DbSet<M_Manage> M_Manage { get; set; }


        public DbSet<Sys_Account> Sys_Account { get; set; }
        public DbSet<Sys_ManagePower> Sys_RoleMenu { get; set; }
        public DbSet<Sys_Dept> Sys_Dept { get; set; }
        #endregion
    }
}