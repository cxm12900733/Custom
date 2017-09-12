using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extend
{
    public static class BaseType
    {
        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrEmpty(val);
        }

        public static bool IsZero(this int val)
        {
            return val == 0 ? true : false;
        }

        public static bool IsZero(this decimal val)
        {
            return val == decimal.Zero ? true : false;
        }

        public static bool IsZero(this byte val)
        {
            return val == 0 ? true : false;
        }

        public static bool IsEmpty(this Guid val)
        {
            return val == null || val == Guid.Empty ? true : false;
        }
    }
}
