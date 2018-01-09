using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Infrastructure;
using Website.Models;
using Infrastructure.Tool.Cryptography;

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
            string EncryptText = OneWayEncryption.SHA256(M_Manage.Password);
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
