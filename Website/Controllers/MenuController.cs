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
        public ActionResult TableList(Sys_Menu Sys_Menu)
        {
            List<Sys_Menu> Base_MenuList = this.Entity.Sys_Menu.AsNoTracking().OrderBy(o => o.Sort).ToList();
            return this.ToTableJson(Base_MenuList);
        }

        [HttpGet]
        public ActionResult Add(int ParentID = 1)
        {
            var Sys_Menu = new Sys_Menu();
            Sys_Menu.ParentID = ParentID;
            this.ViewBag.Sys_Menu = Sys_Menu;
            //图标文件
            string filename = this.Server.MapPath("/Icon.json");
            string jsonstr = System.IO.File.ReadAllText(filename);
            var ComboboxModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxModel>>(jsonstr);
            var CustomIconListJson = Newtonsoft.Json.JsonConvert.SerializeObject(ComboboxModelList.OrderBy(o => o.group).ToList());
            this.ViewBag.CustomIconListJson = CustomIconListJson;
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Add(Sys_Menu Sys_Menu)
        {
            Sys_Menu.AddTime = DateTime.Now;
            switch(Sys_Menu.MenuType)
            {
                case MenuType.Directory:
                    Sys_Menu.Control = string.Empty;
                    Sys_Menu.Method = string.Empty;
                    Sys_Menu.Url = string.Empty;
                    break;
                case MenuType.Powers:
                    Sys_Menu.Url = string.Empty;
                    Sys_Menu.Icon = string.Empty;
                    break; 
            }
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
            //图标文件
            string filename = this.Server.MapPath("/Icon.json");
            string jsonstr = System.IO.File.ReadAllText(filename);
            var ComboboxModelList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ComboboxModel>>(jsonstr);
            var CustomIconListJson = Newtonsoft.Json.JsonConvert.SerializeObject(ComboboxModelList.OrderBy(o => o.group).ToList());
            this.ViewBag.CustomIconListJson = CustomIconListJson;
            return View();
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
            switch (baseSys_Menu.MenuType)
            {
                case MenuType.Directory:
                    baseSys_Menu.Control = string.Empty;
                    baseSys_Menu.Method = string.Empty;
                    baseSys_Menu.Url = string.Empty;
                    break;
                case MenuType.Powers:
                    baseSys_Menu.Url = string.Empty;
                    baseSys_Menu.Icon = string.Empty;
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
            var baseSys_Menu = this.Entity.Sys_Menu.Where(o => idsArr.Contains(o.ID)).ToList();
            this.Entity.Sys_Menu.RemoveRangeLogic(baseSys_Menu);
            this.Entity.SaveChanges();
            Result.Message = "操作成功";
            return this.ToJson(Result);
        }
    }
}
