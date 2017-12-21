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
        public ActionResult TableList(Paging Paging, Sys_Role Sys_Role)
        {
            var query = this.Entity.Sys_Role.AsNoTracking().AsQueryable();
            if (!Sys_Role.Name.IsNullOrEmpty())
            {
                query = query.Where(o => o.Name.Contains(Sys_Role.Name));
            }
            List<Sys_Role> Sys_RoleList = query.OrderBy(o => o.AddTime).Skip(Paging.Skip).Take(Paging.Rows).ToList();
            int Count = this.Entity.Sys_Role.Count();
            return this.ToTableJson(Sys_RoleList,Count);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(Sys_Role Sys_Role)
        {
            Sys_Role.AddTime = DateTime.Now;
            this.Entity.Sys_Role.Add(Sys_Role);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Sys_Role = this.Entity.Sys_Role.FirstOrDefault(o => o.ID == Id);
            if (Sys_Role == null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.Sys_Role = Sys_Role;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Sys_Role Sys_Role)
        {
            var baseSys_Role = this.Entity.Sys_Role.FirstOrDefault(o => o.ID == Sys_Role.ID);
            if (baseSys_Role == null)
            {
                return this.Error("数据不存在");
            }
            baseSys_Role = this.Request.ConvertRequestToModel<Sys_Role>(baseSys_Role, Sys_Role);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));
            var baseSys_Role = this.Entity.Sys_Role.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.Sys_Role.RemoveRange(baseSys_Role);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
