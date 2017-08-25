using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 管理员_角色
    /// </summary>
    public class Middle_Manage_Role
    {
        public int ID { get; set; }

        /// <summary>
        /// 管理员ID
        /// </summary>
        public Guid ManageID { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
    }
}
