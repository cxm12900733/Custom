using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 重新生成自定义图标
        /// </summary>
        /// <returns></returns>
        public bool RebuildCustomIcons()
        {
            string SaveRelativePath = "/Content/css/customIcons.css";
            string IconsRelativePath = "/Content/css/customIcons/";
            var SavePath = HttpContext.Server.MapPath(SaveRelativePath);
            var IconsPath = HttpContext.Server.MapPath(IconsRelativePath);
            var cssFile = "customIcons/";
            /* 流程
             * 1.查询确认保存路径,并清空内容
             * 2.查询图标路径下的所有图标
             * 3.
             */
            //创建文件，取得通道
            var SavePathStrema = System.IO.File.Create(SavePath);
            //创建写入通道
            var streamWriter = new System.IO.StreamWriter(SavePathStrema);
            //取得图标文件列表，并组成样式
            var icons = System.IO.Directory.EnumerateFiles(IconsPath);
            string saveString = string.Empty;
            foreach (var item in icons)
            {
                string fileName = System.IO.Path.GetFileName(item);
                string pureFileName = fileName.Remove(fileName.LastIndexOf('.'));
                //大括号转义->{{
                saveString += string.Format(".icon-{0} {{ background:url('{1}') no-repeat center center; }} \r\n", pureFileName, cssFile + fileName);;
            }
            //写入，差关闭
            streamWriter.Write(saveString);
            streamWriter.Close();
            SavePathStrema.Close();
            return true;
            
        }

        public bool RebuildCustomIconsList()
        {
            string SaveRelativePath = "/Common/CustomIcons.cs";
            string IconsRelativePath = "/Content/css/customIcons/";
            var SavePath = HttpContext.Server.MapPath(SaveRelativePath);
            var IconsPath = HttpContext.Server.MapPath(IconsRelativePath);
            //创建文件，取得通道
            var SavePathStrema = System.IO.File.Create(SavePath);
            //创建写入通道
            var streamWriter = new System.IO.StreamWriter(SavePathStrema);
            //取得图标文件列表，并组成样式
            var icons = System.IO.Directory.EnumerateFiles(IconsPath);
            string saveString = string.Empty;
            streamWriter.Write("using System.Collections.Generic;\r\n namespace Website.Common\r\n {\r\n public static class CustomIcons{\r\n public static Dictionary<string,string> CustomIconList = new Dictionary<string,string>(){\r\n ");
            foreach (var item in icons)
            {
                string fileName = System.IO.Path.GetFileName(item);
                string pureFileName = fileName.Remove(fileName.LastIndexOf('.'));
                //大括号转义->{{
                saveString += string.Format("{{\"{0}\",\"icon-{0}\"}},\r\n", pureFileName); ;
            }
            //写入，差关闭
            streamWriter.Write(saveString);
            streamWriter.Write("};\r\n }\r\n }");
            streamWriter.Close();
            SavePathStrema.Close();
            return true;
        }

    }
}
