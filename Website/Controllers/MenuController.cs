using Infrastructure;
using Infrastructure.Entity;
using Infrastructure.Tool;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using Website.Models.EasyUI;

namespace Website.Controllers
{
    public class MenuController : BaseController
    {
        /// <summary>
        /// 介面
        /// </summary>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 列表
        /// </summary>
        [HttpPost]
        public ActionResult Index(Sys_Menu Sys_Menu)
        {
            List<Sys_Menu> Base_MenuList = this.Entity.Sys_Menu.OrderBy(o => o.Sort).ToList();
            return this.ToTableJson(Base_MenuList);
        }

        [HttpGet]
        public ActionResult Add(int ParentID)
        {
            var Sys_Menu = new Sys_Menu();
            Sys_Menu.ParentID = ParentID;
            this.ViewBag.Sys_Menu = Sys_Menu;
            return View();
        }

        [HttpPost]
        public ActionResult Add(Sys_Menu Sys_Menu)
        {
            Sys_Menu.AddTime = DateTime.Now;
            switch (Sys_Menu.MenuType)
            {
                case MenuType.Directory:
                    Sys_Menu.Url = string.Empty;
                    Sys_Menu.Control = string.Empty;
                    Sys_Menu.Action = string.Empty;
                    break;
                case MenuType.Powers:
                    Sys_Menu.Url = string.Empty;
                    break;
            }
            Sys_Menu.Describe = Sys_Menu.Describe.IsEmpty() ? string.Empty : Sys_Menu.Describe;
            Sys_Menu.Icon = Sys_Menu.Icon.IsEmpty() ? string.Empty : Sys_Menu.Icon;
            Sys_Menu.UIEvent = Sys_Menu.UIEvent.IsEmpty() ? string.Empty : Sys_Menu.UIEvent;
            this.Entity.Sys_Menu.Add(Sys_Menu);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Sys_Menu = this.Entity.Sys_Menu.FirstOrDefault(o => o.ID == Id);
            if (Sys_Menu==null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.Sys_Menu = Sys_Menu;
            return View("Add");
        }

        [HttpPost]
        public ActionResult Edit(Sys_Menu Sys_Menu)
        {
            var baseSys_Menu = this.Entity.Sys_Menu.FirstOrDefault(o => o.ID == Sys_Menu.ID);
            if (baseSys_Menu == null)
            {
                return this.Error("数据不存在");
            }
            baseSys_Menu = this.Request.ConvertRequestToModel<Sys_Menu>(baseSys_Menu,Sys_Menu);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public ActionResult Del(int Id)
        {
            var baseSys_Menu = this.Entity.Sys_Menu.FirstOrDefault(o => o.ID == Id);
            this.Entity.Sys_Menu.Remove(baseSys_Menu);
            return View();
        }
    }
}
