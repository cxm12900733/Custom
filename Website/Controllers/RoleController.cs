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
    public class RoleController : BaseController
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
        public ActionResult Index(Paging Paging, Sys_Role Sys_Role)
        {
            var query = this.Entity.Sys_Role.AsQueryable();
            if (!Sys_Role.Name.IsEmpty())
            {
                query.Where(o => o.Name == Sys_Role.Name);
            }
            List<Sys_Role> Sys_RoleList = query.OrderBy(o => o.AddTime).Take(Paging.Rows).Skip(Paging.Skip).ToList();
            int Count = this.Entity.Sys_Role.Count();
            return this.ToTableJson(Sys_RoleList,Count);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Sys_Role Sys_Role)
        {
            Sys_Role.AddTime = DateTime.Now;
            Sys_Role.Describe = Sys_Role.Describe.IsEmpty() ? string.Empty : Sys_Role.Describe;
            Sys_Role.MenuIDs = Sys_Role.MenuIDs.IsEmpty() ? string.Empty : Sys_Role.MenuIDs;
            this.Entity.Sys_Role.Add(Sys_Role);
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
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));  
            var baseSys_Menu = this.Entity.Sys_Menu.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.Sys_Menu.RemoveRange(baseSys_Menu);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}