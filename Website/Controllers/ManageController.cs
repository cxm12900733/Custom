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
    public class ManageController : BaseController
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
        public ActionResult TableList(Sys_Manage Sys_Manage)
        {
            List<Sys_Manage> Sys_ManageList = this.Entity.Sys_Manage.AsNoTracking().OrderByDescending(o => o.ID).ToList();
            return this.ToTableJson(Sys_ManageList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var Sys_RoleList = this.Entity.Sys_Role.AsNoTracking().Where(o => o.State == State.Normal).OrderBy(o => o.Sort).ToList();
            this.ViewBag.Sys_RoleList = Sys_RoleList;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(Sys_Manage Sys_Manage)
        {
            Sys_Manage.AddTime = DateTime.Now;
            this.Entity.Sys_Manage.Add(Sys_Manage);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Sys_Manage = this.Entity.Sys_Manage.FirstOrDefault(o => o.ID == Id);
            if (Sys_Manage == null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.Sys_Manage = Sys_Manage;

            var Sys_RoleList = this.Entity.Sys_Role.AsNoTracking().Where(o => o.State == State.Normal).OrderBy(o => o.Sort).ToList();
            this.ViewBag.Sys_RoleList = Sys_RoleList;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Sys_Manage Sys_Manage)
        {
            var baseSys_Manage = this.Entity.Sys_Manage.FirstOrDefault(o => o.ID == Sys_Manage.ID);
            if (baseSys_Manage == null)
            {
                return this.Error("数据不存在");
            }
            baseSys_Manage = this.Request.ConvertRequestToModel<Sys_Manage>(baseSys_Manage, Sys_Manage);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));
            var baseSys_Manage = this.Entity.Sys_Manage.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.Sys_Manage.RemoveRange(baseSys_Manage);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
