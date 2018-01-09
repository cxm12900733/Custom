using Infrastructure;
using Infrastructure.Tool;
using Repository;
using Repository.Entity;
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
        /// 文件日志
        /// </summary>
        protected log4net.ILog DebugFileLog = Log4NetExt.DebugFileLog();

        /// <summary>
        /// SQL日志
        /// </summary>
        protected log4net.ILog DebugSQLLog = Log4NetExt.DebugSQLLog();

        /// <summary>
        /// 菜单树
        /// </summary>
        protected List<M_Menu> MenuTree = new List<M_Menu>();

        /// <summary>
        /// 当前管理员
        /// </summary>
        protected M_Manage CurrentManage;

        protected CookieModelContext<ManageCookieModel> CookieModelContext = new CookieModelContext<ManageCookieModel>("r3oDgh2id9FMDjKgWC6eB5A9OmQBmrLY", "p52kkXLyco6oHOwP", "Web.Manage.Login");

        public BaseController()
        {
            this.Entity = DBContextFactory.GetDBContext();

            //取得当前管理员
            var ManageCookieModel = CookieModelContext.GetCookieModel();
            if (!ManageCookieModel.ID.IsNullOrEmpty())
            {
                int ManageID = int.Parse(ManageCookieModel.ID);
                this.CurrentManage = this.Entity.M_Manage.Where(o => o.ID == ManageID).FirstOrDefault();
            }

            //授权
            if (this.CurrentManage != null)
            {
                if (!CurrentManage.RoleIDs.IsNullOrEmpty())
                {
                    var RoleIDsStr = CurrentManage.RoleIDs.Split(',');
                    int[] RoleIDs = Array.ConvertAll<string, int>(RoleIDsStr, delegate(string s) { return int.Parse(s); });
                    if (RoleIDs.Length > 0)
                    {
                        var M_RoleList = Entity.M_Role.Where(o => RoleIDs.Contains(o.ID)).ToList();
                        if (M_RoleList.Count > 0)
                        {
                            var MenuIDsList = M_RoleList.Select(o => o.MenuIDs).ToList();
                            var MenuIDsListStr = string.Join(",", MenuIDsList);
                            var MenuIDsStr = MenuIDsListStr.Split(',');
                            int[] MenuIDs = Array.ConvertAll<string, int>(MenuIDsStr, delegate(string s) { return int.Parse(s); });
                            MenuTree = Entity.M_Menu.Where(o => MenuIDs.Contains(o.ID)).ToList();
                        }
                        
                    }
                   
                }
            }
            MenuTree = Entity.M_Menu.ToList();
            this.ViewBag.Entity = Entity;
            this.ViewBag.MenuTree = MenuTree;
            
        }

        /// <summary>
        /// 在调用操作方法前调用。
        /// </summary>
        /// <param name="filterContext"></param>
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
            DebugSQLLog.Error("异常", ex);
            //filterContext.ExceptionHandled = true;
            //base.OnException(filterContext);
        }

        #region UI相关

        #region easyUI
        /// <summary>
        /// 输出easyUI的表格Json数据
        /// </summary>
        /// <param name="DataList">数据列</param>
        /// <param name="Total">总数</param>
        /// <returns></returns>
        protected JsonResult ToTableJson(object DataList, int Total = 0, bool IsGet = false)
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
        #endregion

        #region 通用介面
        /// <summary>
        /// 错误信息介面
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="links">链接列表</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        protected ActionResult Error(string message, SortedDictionary<string, string> links = null, string title = "温馨提示")
        {
            ViewBag.message = message;
            ViewBag.title = title;
            ViewBag.links = links;
            return View("Error");
        }
        /// <summary>
        /// 成功介面
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="links">链接列表</param>
        /// <param name="title">标题</param>
        /// <param name="isCloseWindow">是否有关闭窗口链接</param>
        /// <returns></returns>
        protected ActionResult Succeed(string message, SortedDictionary<string, string> links = null, string title = "操作成功", bool isCloseWindow = true)
        {
            ViewBag.message = message;
            ViewBag.title = title;
            ViewBag.links = links;
            ViewBag.isCloseWindow = isCloseWindow;
            return View("Succeed");
        }
        #endregion

        #endregion
    }

    public class ManageCookieModel : ICookieModel
    {
        /// <summary>
        /// 登录状态ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 记住账号
        /// </summary>
        public string UserName { get; set; }
    }
}
