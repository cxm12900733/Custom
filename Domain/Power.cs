using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Power : IEntity
    {
        public int ID { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; private set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; private set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Control { get; private set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; private set; }

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
