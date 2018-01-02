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
        public ActionResult TableList(M_Menu M_Menu)
        {
            List<M_Menu> Base_MenuList = this.Entity.M_Menu.AsNoTracking().OrderBy(o => o.Sort).ToList();
            return this.ToTableJson(Base_MenuList);
        }

        [HttpGet]
        public ActionResult Add(int ParentID = 1)
        {
            var M_Menu = new M_Menu();
            M_Menu.ParentID = ParentID;
            this.ViewBag.M_Menu = M_Menu;
            //图标文件
            string filename = this.Server.MapPath("/Icon.json");
            string jsonstr = System.IO.File.ReadAllText(filename);
            var ComboboxModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxModel>>(jsonstr);
            var CustomIconListJson = Newtonsoft.Json.JsonConvert.SerializeObject(ComboboxModelList.OrderBy(o => o.group).ToList());
            this.ViewBag.CustomIconListJson = CustomIconListJson;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(M_Menu M_Menu)
        {
            M_Menu.AddTime = DateTime.Now;
            switch(M_Menu.MenuType)
            {
                case MenuType.Directory:
                    M_Menu.Control = string.Empty;
                    M_Menu.Method = string.Empty;
                    M_Menu.Url = string.Empty;
                    break;
                case MenuType.Powers:
                    M_Menu.Url = string.Empty;
                    M_Menu.Icon = string.Empty;
                    break; 
            }
            this.Entity.M_Menu.Add(M_Menu);
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var M_Menu = this.Entity.M_Menu.FirstOrDefault(o => o.ID == Id);
            if (M_Menu==null)
            {
                return this.Error("数据不存在");
            }
            this.ViewBag.M_Menu = M_Menu;
            //图标文件
            string filename = this.Server.MapPath("/Icon.json");
            string jsonstr = System.IO.File.ReadAllText(filename);
            var ComboboxModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxModel>>(jsonstr);
            var CustomIconListJson = Newtonsoft.Json.JsonConvert.SerializeObject(ComboboxModelList.OrderBy(o => o.group).ToList());
            this.ViewBag.CustomIconListJson = CustomIconListJson;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(M_Menu M_Menu)
        {
            var baseM_Menu = this.Entity.M_Menu.FirstOrDefault(o => o.ID == M_Menu.ID);
            if (baseM_Menu == null)
            {
                return this.Error("数据不存在");
            }
            baseM_Menu = this.Request.ConvertRequestToModel<M_Menu>(baseM_Menu,M_Menu);
            switch (baseM_Menu.MenuType)
            {
                case MenuType.Directory:
                    baseM_Menu.Control = string.Empty;
                    baseM_Menu.Method = string.Empty;
                    baseM_Menu.Url = string.Empty;
                    break;
                case MenuType.Powers:
                    baseM_Menu.Url = string.Empty;
                    baseM_Menu.Icon = string.Empty;
                    break;
            }
            this.Entity.SaveChanges();
            return this.Succeed("操作成功");
        }

        [HttpPost]
        public JsonResult Del(string ids)
        {
            var Result = new AjaxResult();
            var idsStrArr = ids.Split(',');
            int[] idsArr = Array.ConvertAll<string, int>(idsStrArr, s => int.Parse(s));  
            var baseM_Menu = this.Entity.M_Menu.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.M_Menu.RemoveRangeLogic(baseM_Menu);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
