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
    /// 菜单
    /// 这里菜单和权限是混合的
    /// </summary>
    public class M_Menu : BaseEntity, ILogicDelete
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Name
        {
            get { return name; }
            set { name = value ?? string.Empty; }
        }
        private string name = string.Empty;

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
        public string Url 
        {
            get { return url; }
            set { url = value ?? string.Empty; }
        }
        private string url = string.Empty;

        /// <summary>
        /// 控制器
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Control
        {
            get { return control; }
            set { control = value ?? string.Empty; }
        }
        private string control = string.Empty;

        /// <summary>
        /// 操作
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Method
        {
            get { return method; }
            set { method = value ?? string.Empty; }
        }
        private string method = string.Empty;

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
        /// 图标
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Icon
        {
            get { return icon; }
            set { icon = value ?? string.Empty; }
        }
        private string icon = string.Empty;

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
