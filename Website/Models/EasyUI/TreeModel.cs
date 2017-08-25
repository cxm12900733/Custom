using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models.EasyUI
{
    public class TreeModel
    {
        public int ID { get; set; }
        public string text { get; set; }
        public int ParentID { get; set; }
        /// <summary>
        /// 打开状态 默认打开,关闭closed,打开open
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 图标样式
        /// </summary>
        public string iconCls { get; set; } 
    }

    public static class TreeModelList
    {
        /// <summary>
        /// 全打开
        /// </summary>
        public static List<TreeModel> Open(this List<TreeModel> TreeModelList)
        {
            TreeModelList.ForEach(o =>
            {
                o.state = "open";
            });
            return TreeModelList;
        }

        /// <summary>
        /// 全关闭
        /// </summary>
        public static List<TreeModel> Closed(this List<TreeModel> TreeModelList)
        {
            TreeModelList.ForEach(o =>
            {
                o.state = "closed";
            });
            return TreeModelList;
        }
    }
}