using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models.EasyUI
{
    public class DataListModel
    {
        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; } 
        /// <summary>
        /// 选中
        /// </summary>
        public bool @checked {get;set;}
    }
}