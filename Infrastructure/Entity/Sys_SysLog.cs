namespace Infrastructure.Entity
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ϵͳ��־
    /// </summary>
    public partial class Sys_SysLog
    {

        [Key]
        public int ID { get; set; }

        /// <summary>
        /// ��¼�ȼ�
        /// FATAL:��������
        /// ERROR:����
        /// WARN:����
        /// INFO:��ʾ
        /// DEBUG:����
        /// </summary>
        [Required]
        [StringLength(20)]
        public string LogLevel { get; set; }

        /// <summary>
        /// ��¼��/��Դ
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Logger { get; set; }

        /// <summary>
        /// ��Ϣ
        /// </summary>
        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        /// <summary>
        /// �쳣
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Exception { get; set; }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }

    }
}
