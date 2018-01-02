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
    /// 管理员
    /// </summary>
    public class M_Manage : BaseEntity, ILogicDelete
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [StringLength(30)]
        public string Password { get; set; }

        /// <summary>
        /// 移动电话号码
        /// </summary>
        [StringLength(30)]
        public string MobileNo { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [Required]
        [StringLength(20)]
        public string TrueName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public State State { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 角色ID字符串
        /// </summary>
        [StringLength(200)]
        public string RoleIDs { get; set; }

    }
}
