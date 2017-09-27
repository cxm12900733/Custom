using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    /// <summary>
    /// 分页模型
    /// </summary>
    public class Paging 
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }

        public int Skip
        { 
            get{
                return (this.Page - 1) * this.Rows;
            } 
        }
    }
}