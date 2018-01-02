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
        public ActionResult TableList(M_Manage M_Manage)
        {
            List<M_Manage> M_ManageList = this.Entity.M_Manage.AsNoTracking().OrderByDescending(o => o.ID).ToList();
            return this.ToTableJson(M_ManageList);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var M_RoleList = this.Entity.M_Role.AsNoTracking().Where(o => o.State == State.Normal).OrderBy(o => o.Sort).ToList();
            this.ViewBag.M_RoleList = M_RoleList;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(M_Manage M_Manage)
        {
            M_Manage.AddTime = DateTime.Now;
            this.Entity.M_Manage.Add(M_Manage);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var M_Manage = this.Entity.M_Manage.FirstOrDefault(o => o.ID == Id);
            if (M_Manage == null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.M_Manage = M_Manage;

            var M_RoleList = this.Entity.M_Role.AsNoTracking().Where(o => o.State == State.Normal).OrderBy(o => o.Sort).ToList();
            this.ViewBag.M_RoleList = M_RoleList;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(M_Manage M_Manage)
        {
            var baseM_Manage = this.Entity.M_Manage.FirstOrDefault(o => o.ID == M_Manage.ID);
            if (baseM_Manage == null)
            {
                return this.Error("数据不存在");
            }
            baseM_Manage = this.Request.ConvertRequestToModel<M_Manage>(baseM_Manage, M_Manage);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));
            var baseM_Manage = this.Entity.M_Manage.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.M_Manage.RemoveRangeLogic(baseM_Manage);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
