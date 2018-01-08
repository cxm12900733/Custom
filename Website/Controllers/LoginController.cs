using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using System.Security.Cryptography;
using Website.Models;

namespace Website.Controllers
{
    public class LoginController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(M_Manage M_Manage)
        {
            var AjaxResult =new AjaxResult();
            if(M_Manage.UserName.IsNullOrEmpty() || M_Manage.Password.IsNullOrEmpty())
            {
                AjaxResult.IsSuccess = false;
                AjaxResult.Message = "请输入账号或密码";
                return ToJson(AjaxResult);
            }

            #region 认证
            //加密密码
            var SHA256Managed = new SHA256Managed();
            byte[] clearBuffer = System.Text.Encoding.UTF8.GetBytes(M_Manage.Password);
            var EncryptTextHash = SHA256Managed.ComputeHash(clearBuffer);
            var EncryptText = EncryptTextHash.ToHexString();

            //查询
            var baseM_Manage = this.Entity.M_Manage.Where(o => o.UserName == M_Manage.UserName && o.Password == EncryptText).FirstOrDefault();
            if (baseM_Manage == null)
            {
                AjaxResult.IsSuccess = false;
                AjaxResult.Message = "账号或密码错误,请重新输入";
                return ToJson(AjaxResult);
            }
            if (baseM_Manage.State != State.Normal)
            {
                AjaxResult.IsSuccess = false;
                AjaxResult.Message = "账号被锁定,请联系管理员";
                return ToJson(AjaxResult);
            }
            #endregion

            #region 保存认证
            var ManageCookieModel = new ManageCookieModel()
            {
                ID = baseM_Manage.ID.ToString(),
            };
            this.CookieModelContext.SetCookieModel(ManageCookieModel,DateTime.Now.AddDays(1));
            #endregion

            return ToJson(AjaxResult);
        }
    }
}
