using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paging<T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public T Condition { get; set; }

        public int Take
        { 
            get{
                return (this.Page - 1) * this.Rows;
            } 
        }
    }
}