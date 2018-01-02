using Infrastructure;
using Newtonsoft.Json;
using Repository.Entity;
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
        public ActionResult TableList(Paging Paging, M_Role M_Role)
        {
            var query = this.Entity.M_Role.AsNoTracking().AsQueryable();
            if (!M_Role.Name.IsNullOrEmpty())
            {
                query = query.Where(o => o.Name.Contains(M_Role.Name));
            }
            List<M_Role> M_RoleList = query.OrderBy(o => o.AddTime).Skip(Paging.Skip).Take(Paging.Rows).ToList();
            int Count = this.Entity.M_Role.Count();
            return this.ToTableJson(M_RoleList,Count);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(M_Role M_Role)
        {
            M_Role.AddTime = DateTime.Now;
            this.Entity.M_Role.Add(M_Role);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var M_Role = this.Entity.M_Role.FirstOrDefault(o => o.ID == Id);
            if (M_Role == null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.M_Role = M_Role;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(M_Role M_Role)
        {
            var baseM_Role = this.Entity.M_Role.FirstOrDefault(o => o.ID == M_Role.ID);
            if (baseM_Role == null)
            {
                return this.Error("数据不存在");
            }
            baseM_Role = this.Request.ConvertRequestToModel<M_Role>(baseM_Role, M_Role);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));
            var baseM_Role = this.Entity.M_Role.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.M_Role.RemoveRangeLogic(baseM_Role);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
