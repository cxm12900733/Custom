using Infrastructure;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Website.Common
{
    public abstract class ManageWebViewPage<TModel> : WebViewPage<TModel>
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public EntityContext Entity
        {
            get
            {
                return (EntityContext)this.ViewBag.Entity;
            }
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<Sys_Menu> MenuTree
        {
            get
            {
                return (List<Sys_Menu>)this.ViewBag.MenuTree;
            }
        }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName
        {
            get
            {
                return (string)this.ViewBag.ActionName;
            }
        }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName
        {
            get
            {
                return (string)this.ViewBag.ControllerName;
            }
        }
        
    }
}