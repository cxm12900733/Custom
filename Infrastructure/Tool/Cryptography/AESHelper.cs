using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tool.Cryptography
{
    public class AESHelper
    {
        public string Key { get; private set; }
        public string IV { get; private set; }

        private AesManaged Aes;

        /// <summary>
        /// 初始化AES
        /// </summary>
        /// <param name="key">加密Key</param>
        /// <param name="iv">向量</param>
        public AESHelper(string key,string iv)
        {
            this.Key = key;
            this.IV = iv;
            var KeyArr = Encoding.UTF8.GetBytes(this.Key);
            var IVArr = Encoding.UTF8.GetBytes(this.IV);
            this.Aes = new AesManaged();
            Aes.Key = KeyArr;
            Aes.IV = IVArr;
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="clearText">明文</param>
        /// <returns></returns>
        public string Encrypt(string clearText)
        {
            ICryptoTransform encryptor = Aes.CreateEncryptor();
            return encryptor.Encrypt(clearText);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <returns></returns>
        public string Decrypt(string encryptedText)
        {
            ICryptoTransform decryptor = Aes.CreateDecryptor();
            return decryptor.Decrypt(encryptedText);
        }
    }
}
