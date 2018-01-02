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
    /// 菜单树
    /// </summary>
    public class Base_MenuTree
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Required]
        public int ParentID { get; set; }

        /// <summary>
        /// 子ID
        /// </summary>
        [Required]
        public int SonID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }
    }
}
