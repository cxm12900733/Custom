using Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace Domain
{
    /// <summary>
    /// 账户
    /// </summary>
    public abstract class AccountDomain : IEntity
    {
        #region 属性

        public Guid ID { get; protected set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; protected set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; protected set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; protected set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; protected set; }
        
        #endregion

    }
    
}
