namespace Infrastructure.Entity
{
    using Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ����Աҵ����־
    /// </summary>
    public partial class Sys_ManageBizLog
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// �˻�ID
        /// </summary>
        [Required]
        public Guid Role_ManageID { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Controller { get; set; }

        /// <summary>
        /// �
        /// </summary>
        [Required]
        [StringLength(40)]
        public string Action { get; set; }

        /// <summary>
        /// ��Ϣ
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        [Required]
        public DateTime AddTime { get; set; }
    }
}
