using Infrastructure;
using Infrastructure.Entity;
using Infrastructure.Factory;
using Infrastructure.ITool;
using Infrastructure.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 数据库接口
        /// </summary>
        protected EntityContext Entity;

        /// <summary>
        /// 系统日志
        /// </summary>
        protected ISysLog ISysLog = new Log4Net();

        /// <summary>
        /// 菜单树
        /// </summary>
        protected List<Sys_Menu> MenuTree = new List<Sys_Menu>();

        public BaseController()
        {
            this.Entity = DBContextFactory.GetDBContext();
            MenuTree = Entity.Sys_Menu.ToList();
            this.ViewBag.ISysLog = ISysLog;
            this.ViewBag.Entity = Entity;
            this.ViewBag.MenuTree = MenuTree;
            
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.ViewBag.ActionName = filterContext.RouteData.Values.ContainsKey("action") ? this.RouteData.Values["action"] : string.Empty;
            this.ViewBag.ControllerName = filterContext.RouteData.Values.ContainsKey("controller") ? this.RouteData.Values["controller"] : string.Empty;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        { 
            var ex = filterContext.Exception;
            ISysLog.Error(ex.Message, ex);
            base.OnException(filterContext);
        }


        /// <summary>
        /// 输出easyUI的表格Json数据
        /// </summary>
        /// <param name="DataList">数据列</param>
        /// <param name="Total">总数</param>
        /// <returns></returns>
        protected JsonResult ToTableJson(object DataList, bool IsGet = false, int Total = 0)
        {
            var JsonResult = new JsonResult() { ContentType = "text/html" };
            JsonResult.JsonRequestBehavior = IsGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet;
            JsonResult.Data = new { rows = DataList, total = Total };
            return JsonResult;
        }

        /// <summary>
        /// 输出Json数据
        /// </summary>
        /// <param name="Data">数据</param>
        /// <param name="IsGet">是否允许Get</param>
        /// <returns></returns>
        protected JsonResult ToJson(object Data, bool IsGet = false)
        {
            var JsonResult = new JsonResult() { ContentType = "text/html" };
            JsonResult.JsonRequestBehavior = IsGet ? JsonRequestBehavior.AllowGet : JsonRequestBehavior.DenyGet;
            JsonResult.Data = new { data = Data };
            return JsonResult;
        }

        public ActionResult Error(string message, SortedDictionary<string, string> links = null, string title = "温馨提示")
        {
            ViewBag.message = message;
            ViewBag.title = title;
            ViewBag.links = links;
            return View("Error");
        }

        public ActionResult Succeed(string message, SortedDictionary<string, string> links = null, string title = "操作成功", bool isWindow = true)
        {
            ViewBag.message = message;
            ViewBag.title = title;
            ViewBag.links = links;
            ViewBag.isWindow = isWindow;
            return View("Succeed");
        }

    }

    
}
