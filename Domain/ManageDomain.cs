using Domain.Base;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class ManageDomain : IAggregate
    {
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

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; private set; }

        /// <summary>
        /// 管理账户状态
        /// </summary>
        public State State { get; protected set; }

        private ManageDomain (){}

        /// <summary>
        /// 创建/添加管理员
        /// </summary>
        /// <param name="Manage"></param>
        /// <returns></returns>
        public static ManageDomain Create(string UserName, string Mobile, string Email, string Password)
        {
            //1.账户名、手机号码、邮箱必须要填一个 
            if (UserName.IsNullOrEmpty() && Mobile.IsNullOrEmpty() && Email.IsNullOrEmpty())
            {
                throw new Exception("请填写账户名、手机号码、邮箱中的一个");
            }
            var ManageDomain = new ManageDomain()
            {
                Email = Email,
                Mobile = Mobile,
                Password = Password,
                UserName = UserName,
                ID = Guid.NewGuid(),
                State = State.Normal,
            };
            return ManageDomain;
        }

        /// <summary>
        /// 加载/读取管理员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static ManageDomain Load(Guid ID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool Edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 批量设置状态
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static void SetState(Guid[] IDs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="OldPassword">旧密码</param>
        /// <param name="NewPassword">新密码</param>
        /// <returns></returns>
        public bool SetPassword(string OldPassword,string NewPassword)
        {
            throw new NotImplementedException();
        }

        //忘记密码,暂不实现
        public bool ForgetPassword()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="UserName">用户名/邮箱/手机号码</param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool Login(string UserName, string Password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
    
}
