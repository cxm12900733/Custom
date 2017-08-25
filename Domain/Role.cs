using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role : IEntity
    {
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 权限
        /// </summary>
        public int[] PowerIDs { get; private set; }

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
