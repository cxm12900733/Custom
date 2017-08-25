using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 角色&菜单
    /// </summary>
    public class Sys_ManagePower
    {
        public int ID { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuID { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public int ManageID { get; set; }

        /// <summary>
        /// 授权
        /// 1.
        /// 2.
        /// </summary>
        public int Authorize { get; set; }
    }
}
