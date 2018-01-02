using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Base_Power
    {
        [Key]
        public int ID { get; set; }

        // <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Control { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
