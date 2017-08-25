namespace Infrastructure.Entity
{
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 管理员业务日志
    /// </summary>
    public partial class Sys_ManageBizLog
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 账户ID
        /// </summary>
        [Required]
        public Guid Role_ManageID { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Controller { get; set; }

        /// <summary>
        /// 活动
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Action { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }
    }
}
