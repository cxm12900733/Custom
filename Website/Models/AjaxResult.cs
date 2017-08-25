using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    /// <summary>
    /// Ajax返回
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 业务是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Ajax返回
    /// </summary>
    public class AjaxResult<T> : AjaxResult
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }
    }
}