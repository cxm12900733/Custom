using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ExtConverter
    {
        /// <summary>
        /// byte[]转16进制字符串
        /// </summary>
        public static string ToHexString (this byte[] bytes)
        {
            return BitConverter.ToString(bytes, 0).Replace("-", string.Empty);
        }

        /// <summary>
        /// 16进制字符串转byte[]
        /// </summary>
        public static byte[] ToByte(this string HexString)
        {
            var inputByteArray = new byte[HexString.Length / 2];
            for (var x = 0; x < inputByteArray.Length; x++)
            {
                var i = Convert.ToInt32(HexString.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            return inputByteArray;
        }
        
    }
}
