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
    /// </summary>
    public class Base_Menu
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
        /// 描述
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Describe { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Css { get; set; }

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
        /// 权限ID
        /// </summary>
        public int PowerID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Required]
        public int ParentID { get; set; }
    }
}
