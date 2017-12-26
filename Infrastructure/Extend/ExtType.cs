using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    /// <summary>
    /// 基础类型扩展
    /// </summary>
    public static class ExtType
    {
        public static bool IsNullOrEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        public static bool IsNullOrEmpty(this int val)
        {
            return val == 0 ? true : false;
        }

        public static bool IsNullOrEmpty(this decimal val)
        {
            return val == decimal.Zero ? true : false;
        }

        public static bool IsNullOrEmpty(this byte val)
        {
            return val == 0 ? true : false;
        }

        public static bool IsNullOrEmpty(this Guid val)
        {
            return val == null || val == Guid.Empty ? true : false;
        }
    }
}
