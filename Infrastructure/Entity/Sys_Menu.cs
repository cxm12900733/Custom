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
    /// 菜单
    /// 这里菜单和权限是混合的
    /// </summary>
    public class Sys_Menu
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        [Required]
        public MenuType MenuType { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Url { get;set; }

        /// <summary>
        /// UI事件
        /// </summary>
        [Required]
        [StringLength(100)]
        public string UIEvent { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Control { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Action { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Describe { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public State State { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Required]
        public int ParentID { get; set; }
    }
}
