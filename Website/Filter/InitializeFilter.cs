using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Tool;
using Website.Models;

namespace Website.Filter
{
    /// <summary>
    /// 初始化过滤器
    /// </summary>
    public class InitializeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 认证
            bool isAnoy = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);

            var ClientHook = new ClientHook<Website.Controllers.ManageClientDataModel>();
            var ManageClientDataModel = ClientHook.GetModel();
            if (!isAnoy && ManageClientDataModel!=null)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var result = new AjaxResult()
                    {
                        IsSuccess = false,
                        Message = "请重新登录",
                    };

                    filterContext.Result = new JsonResult
                    {
                        Data = result,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        ContentType = "text/html"
                    };
                }
                else
                {
                    new ActionResult()
                    {
                        
                    }
                }
            }
            #endregion

            #region 授权
            string Controller = filterContext.Controller.ControllerContext.Controller.ToString();
            string ActionName = filterContext.ActionDescriptor.ActionName;
            filterContext.Controller.ViewBag.ActionName = Controller;
            filterContext.Controller.ViewBag.ControllerName = ActionName;
            #endregion

            #region 初始化配置

            #endregion
        }
    }
}