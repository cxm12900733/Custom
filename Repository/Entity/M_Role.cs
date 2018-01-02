using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    /// <summary>
    /// 角色
    /// </summary>
    public class M_Role : BaseEntity, ILogicDelete
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name
        {
            get { return name; }
            set { name = value ?? string.Empty; }
        }
        private string name = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Describe
        {
            get { return describe; }
            set { describe = value ?? string.Empty; }
        }
        private string describe = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 菜单ID字符串
        /// </summary>
        [Required]
        public string MenuIDs
        {
            get { return menuids; }
            set { menuids = value ?? string.Empty; }
        }
        private string menuids = string.Empty;

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }
    }
}
