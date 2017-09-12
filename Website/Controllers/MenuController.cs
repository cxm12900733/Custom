﻿using Infrastructure;
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
        public JsonResult Add(Sys_Menu Sys_Menu)
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
            var AjaxResult = new AjaxResult()
            {
                IsSuccess = true,
                Message = "操作成功",
            };
            return new JsonResult() { Data = AjaxResult };
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Sys_Menu = this.Entity.Sys_Menu.FirstOrDefault(o=>o.ID == id);
            if (Sys_Menu==null)
            {
                
            }
            this.ViewBag.Sys_Menu = Sys_Menu;
            return View("Add");
        }

        [HttpPost]
        public JsonResult Edit(Sys_Menu Sys_Menu)
        {
            var AjaxResult = new AjaxResult()
            {
                IsSuccess = true,
                Message = "操作成功",
            };
            var baseSys_Menu = this.Entity.Sys_Menu.FirstOrDefault(o => o.ID == Sys_Menu.ID);
            if (baseSys_Menu == null)
            {
                AjaxResult.IsSuccess = false;
                AjaxResult.Message = "数据不存在";
                return new JsonResult() { Data = AjaxResult };
            }
            baseSys_Menu = Sys_Menu.MapTo(baseSys_Menu);
            this.Entity.SaveChanges();
            return new JsonResult() { Data = AjaxResult };
        }

        [HttpPost]
        public ActionResult Del()
        {
            return View();
        }
    }
}