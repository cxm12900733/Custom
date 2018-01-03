using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace Website.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            string original = "Here is some data to encrypt!";
            string key = "AES.Custom.Web.Manage.Login.ABCD";
            var keyarr = Encoding.ASCII.GetBytes(key);
            AesManaged myAes = new AesManaged();
            //myAes.Key = keyarr;
            //myAes.GenerateIV();
            //var Istrue = myAes.ValidKeySize(myAes.Key.Length);

            ICryptoTransform encryptor = myAes.CreateEncryptor();
            byte[] encrypted;
            string str = Encrypt(original, encryptor);
            //using (MemoryStream msEncrypt = new MemoryStream())
            //{
            //    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            //    {
            //        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            //        {

            //            //Write all data to the stream.
            //            swEncrypt.Write(original);
            //        }

            //        encrypted = msEncrypt.ToArray();
            //    }
            //}
            //string str = Encoding.Default.GetString(encrypted);

            var newEncrypted = Convert.FromBase64String(str);
            string neworiginal = string.Empty;
            ICryptoTransform decryptor = myAes.CreateDecryptor();
            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(newEncrypted))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        neworiginal = srDecrypt.ReadToEnd();
                    }
                }
            }

            return View();
        }

        // 加密算法
        private string Encrypt(string clearText,ICryptoTransform encryptor)
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
            return encryptedText;
        }

        // 解密算法
        //private string Decrypt(string encryptedText)
        //{
        //    byte[] encryptedBuffer = Convert.FromBase64String(encryptedText);
        //    Stream encryptedStream = new MemoryStream(encryptedBuffer);

        //    MemoryStream clearStream = new MemoryStream();
        //    CryptoStream cryptoStream =
        //        new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read);

        //    int bytesRead = 0;
        //    byte[] buffer = new byte[BufferSize];

        //    do
        //    {
        //        bytesRead = cryptoStream.Read(buffer, 0, BufferSize);
        //        clearStream.Write(buffer, 0, bytesRead);
        //    } while (bytesRead > 0);

        //    buffer = clearStream.GetBuffer();
        //    string clearText =
        //        Encoding.UTF8.GetString(buffer, 0, (int)clearStream.Length);

        //    return clearText;
        //}

        //public void test()
        //{
        //    string IconsRelativePath = "/Content/css/Custom/customIcons/";
        //    var IconsPath = HttpContext.Server.MapPath(IconsRelativePath);
        //    var icons = System.IO.Directory.GetFiles(IconsPath);
        //    var Directories = System.IO.Directory.GetDirectories(IconsPath);
        //    foreach (var item in Directories)
        //    {
        //        var a = System.IO.Path.GetFileName(item);
        //    }
            
        //    //System.IO.Directory.GetDirectories(IconsPath);
            
        //    foreach (var item in icons)
        //    {
        //        string fileName = System.IO.Path.GetFileName(item);
        //        string pureFileName = fileName.Remove(fileName.LastIndexOf('.'));
        //    }
        //}

        /// <summary>
        /// 按文件生成自定义图标样式、Json文件
        /// </summary>
        /// <returns></returns>
        public bool RebuildCustomIcons()
        {
            string CssRelativePath = "/Content/css/Custom/CustomIcons.css";
            string DirectorieRelativePath = "/Content/css/Custom/Icons/";
            string JsonRelativePath = "/Icon.json";
            var CssPath = HttpContext.Server.MapPath(CssRelativePath);
            var DirectoriePath = HttpContext.Server.MapPath(DirectorieRelativePath);
            var JsonPath = HttpContext.Server.MapPath(JsonRelativePath);
            var ImgFile = "Icons/";
            /* 流程
             * 1.查询确认保存路径,并清空内容
             * 2.查询图标路径下的所有图标
             * 3.
             */
            //创建文件，取得通道
            var CssPathStrema = System.IO.File.Create(CssPath);
            var JsonPathStrema = System.IO.File.Create(JsonPath);

            //创建写入通道
            var CssStreamWriter = new System.IO.StreamWriter(CssPathStrema);
            var JsonStreamWriter = new System.IO.StreamWriter(JsonPathStrema);

            try
            {
                //取得图标文件列表，并组成样式
                var Dirs = System.IO.Directory.GetDirectories(DirectoriePath);
                string CssSaveString = string.Empty;
                string JsonSaveString = string.Empty;
                foreach (var Dir in Dirs)
                {
                    var DirName = System.IO.Path.GetFileName(Dir);
                    var icons = System.IO.Directory.GetFiles(Dir);
                    foreach (var item in icons)
                    {
                        string fileName = System.IO.Path.GetFileName(item);
                        string pureFileName = fileName.Remove(fileName.LastIndexOf('.'));
                        //大括号转义->{{
                        CssSaveString += string.Format(".icon-{0} {{ background:url('{1}') no-repeat center center; }} \r\n", pureFileName, ImgFile + DirName + "/" + fileName);
                        JsonSaveString += string.Format("{{\"text\": \"{0}\",\"value\": \"icon-{1}\",\"iconCls\": \"icon-{2}\",\"group\": \"{3}\"}}, \r\n"
                            , pureFileName, pureFileName, pureFileName, DirName);
                    }
                }
                JsonSaveString = "[\r\n" + JsonSaveString + "]\r\n";

                //写入
                CssStreamWriter.Write(CssSaveString);
                JsonStreamWriter.Write(JsonSaveString);
            }
            finally
            {
                //关闭
                CssStreamWriter.Close();
                CssPathStrema.Close();

                JsonStreamWriter.Close();
                JsonPathStrema.Close();
            }
            
            return true;
            
        }

        //public bool RebuildCustomIconsList()
        //{
        //    string SaveRelativePath = "/Common/CustomIcons.cs";
        //    string IconsRelativePath = "/Content/css/customIcons/";
        //    var SavePath = HttpContext.Server.MapPath(SaveRelativePath);
        //    var IconsPath = HttpContext.Server.MapPath(IconsRelativePath);
        //    //创建文件，取得通道
        //    var SavePathStrema = System.IO.File.Create(SavePath);
        //    //创建写入通道
        //    var streamWriter = new System.IO.StreamWriter(SavePathStrema);
        //    //取得图标文件列表，并组成样式
        //    var icons = System.IO.Directory.EnumerateFiles(IconsPath);
        //    string saveString = string.Empty;
        //    streamWriter.Write("using System.Collections.Generic;\r\n namespace Website.Common\r\n {\r\n public static class CustomIcons{\r\n public static Dictionary<string,string> CustomIconList = new Dictionary<string,string>(){\r\n ");
        //    foreach (var item in icons)
        //    {
        //        string fileName = System.IO.Path.GetFileName(item);
        //        string pureFileName = fileName.Remove(fileName.LastIndexOf('.'));
        //        //大括号转义->{{
        //        saveString += string.Format("{{\"{0}\",\"icon-{0}\"}},\r\n", pureFileName); ;
        //    }
        //    //写入，差关闭
        //    streamWriter.Write(saveString);
        //    streamWriter.Write("};\r\n }\r\n }");
        //    streamWriter.Close();
        //    SavePathStrema.Close();
        //    return true;
        //}

    }
}
