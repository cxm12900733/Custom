using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tool.Cryptography
{
    public static class ICryptoTransformExtensions
    {
        /// <summary>
        /// 输出密文
        /// </summary>
        /// <param name="clearText">明文</param>
        /// <param name="encryptor">加密转换器</param>
        /// <returns></returns>
        public static string Encrypt(this ICryptoTransform encryptor, string clearText)
        {
            // 创建明文流
            byte[] clearBuffer = Encoding.UTF8.GetBytes(clearText);
            MemoryStream clearStream = new MemoryStream(clearBuffer);

            // 创建空的密文流
            MemoryStream encryptedStream = new MemoryStream();

            CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, encryptor, CryptoStreamMode.Write);

            // 将明文流写入到buffer中
            // 将buffer中的数据写入到cryptoStream中
            int bytesRead = 0;
            byte[] buffer = new byte[1024];
            do
            {
                bytesRead = clearStream.Read(buffer, 0, 1024);
                cryptoStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            cryptoStream.FlushFinalBlock();

            // 获取加密后的文本
            buffer = encryptedStream.ToArray();
            string encryptedText = Convert.ToBase64String(buffer);

            //关闭
            encryptedStream.Close();
            clearStream.Close();
            cryptoStream.Close();

            return encryptedText;
        }

        /// <summary>
        /// 解密密文
        /// </summary>
        /// <param name="encryptedText">密文</param>
        /// <param name="decryptor">解密转换器</param>
        /// <returns></returns>
        public static string Decrypt(this ICryptoTransform decryptor,string encryptedText)
        {
            byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
            Stream encryptedStream = new MemoryStream(encryptedBuffer);

            MemoryStream clearStream = new MemoryStream();
            CryptoStream cryptoStream =
                new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);

            int bytesRead = 0;
            byte[] buffer = new byte[1024];

            do
            {
                bytesRead = cryptoStream.Read(buffer, 0, 1024);
                clearStream.Write(buffer, 0, bytesRead);
            } while (bytesRead > 0);

            buffer = clearStream.GetBuffer();
            string clearText =
                Encoding.UTF8.GetString(buffer, 0, (int)clearStream.Length);

            //关闭
            encryptedStream.Close();
            clearStream.Close();
            cryptoStream.Close();

            return clearText;
        }
    }
}
