using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    /// <summary>
    /// 账户
    /// </summary>
    public class Base_Account
    {
        [Key]
        public Guid ID { get; set; }

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
        [Required]
        [StringLength(30)]
        public string MobileNo { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
    }
}
