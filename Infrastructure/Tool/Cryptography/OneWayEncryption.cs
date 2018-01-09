using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tool.Cryptography
{
    /// <summary>
    /// 单向加密
    /// </summary>
    public static class OneWayEncryption
    {
        /// <summary>
        /// SHA256加密,返回一个64位密文
        /// </summary>
        public static string SHA256(string str)
        {
            var SHA256Managed = new SHA256Managed();
            byte[] clearBuffer = System.Text.Encoding.UTF8.GetBytes(str);
            var EncryptTextHash = SHA256Managed.ComputeHash(clearBuffer);
            var EncryptText = EncryptTextHash.ToHexString();
            return EncryptText;
        }
    }
}
