using Domain.Base;
using Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : IEntity
    {
        public int ID { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; private set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType MenuType { get; private set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Tier { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int PowerID { get; private set; }

        //添加
        public Role Add()
        {
            throw new NotImplementedException();
        }

        //修改
        public Role Edit()
        {
            throw new NotImplementedException();
        }

        //删除
        public Role Delete()
        {
            throw new NotImplementedException();
        }


    }
}
