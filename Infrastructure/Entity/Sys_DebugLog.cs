namespace Infrastructure.Entity
{
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ������־
    /// </summary>
    public partial class Sys_DebugLog
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
        /// ��Դ
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Location { get; set; }

        /// <summary>
        /// ��Ϣ
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        /// <summary>
        /// �쳣
        /// </summary>
        [Required]
        public string Exception { get; set; }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }

    }
}
