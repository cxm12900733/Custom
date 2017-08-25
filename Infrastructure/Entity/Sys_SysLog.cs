namespace Infrastructure.Entity
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 系统日志
    /// </summary>
    public partial class Sys_SysLog
    {

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 记录等级
        /// FATAL:至命错误
        /// ERROR:错误
        /// WARN:警告
        /// INFO:提示
        /// DEBUG:调试
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LogLevel { get; set; }

        /// <summary>
        /// 记录器/来源
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Logger { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Exception { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }

    }
}
