using Infrastructure.Tool.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<string> originals = new List<string>()
            {
                "Here is some data to encrypt!",
                "中文测试一下",
                "特设字符测试&*(!@)#($&^%$%?",
                "中文测试一下SDFWFFDFDSFDSFDSWFF255611516262622122162212FFEWFEFDSFAFEFGWFWQFQFQA",
            };
            string original = "Here is some data to encrypt!";
            string key = "r3oDgh2id9FMDjKgWC6eB5A9OmQBmrLY";
            string IV = "p52kkXLyco6oHOwP";
            var AES = new AESHelper(key, IV);
            List<string> a = new List<string>();
            foreach (var item in originals)
            {
                var encryptedText = AES.Encrypt(item);
                var clearText = AES.Decrypt(encryptedText);
                a.Add(clearText);
            }
            
            ////string original = "Here is some data to encrypt!";
            ////string key = "r3oDgh2id9FMDjKgWC6eB5A9OmQBmrLY";
            ////string IV = "p52kkXLyco6oHOwP";
            ////var KeyArr = Encoding.UTF8.GetBytes(key);
            ////var IVArr = Encoding.UTF8.GetBytes(IV);
            ////AesManaged EncryptorAes = new AesManaged();
            ////EncryptorAes.Key = KeyArr;
            ////EncryptorAes.IV = IVArr;
            //myAes.GenerateIV();
            //var Istrue = myAes.ValidKeySize(myAes.Key.Length);

            ////ICryptoTransform encryptor = EncryptorAes.CreateEncryptor();
            ////string str = Encrypt(original, encryptor);

            ////AesManaged DecryptorAes = new AesManaged();
            ////DecryptorAes.Key = KeyArr;
            ////DecryptorAes.IV = IVArr;
            ////ICryptoTransform decryptor = DecryptorAes.CreateDecryptor();
            ////string neworiginal = Decrypt(str, decryptor);
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

            //var newEncrypted = Convert.FromBase64String(str);
            //string neworiginal = string.Empty;
            //ICryptoTransform decryptor = myAes.CreateDecryptor();
            //// Create the streams used for decryption.
            //using (MemoryStream msDecrypt = new MemoryStream(newEncrypted))
            //{
            //    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            //    {
            //        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            //        {

            //            // Read the decrypted bytes from the decrypting stream
            //            // and place them in a string.
            //            neworiginal = srDecrypt.ReadToEnd();
            //        }
            //    }
            //}

            return View();
        }

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
