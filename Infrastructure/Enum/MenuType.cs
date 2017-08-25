using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 目录
        /// </summary>
        Directory = 1,

        /// <summary>
        /// 介面
        /// </summary>
        Interface = 2,

        /// <summary>
        /// 按钮
        /// </summary>
        Button = 3,

        /// <summary>
        /// 权限
        /// </summary>
        Powers = 4,
    }
}
