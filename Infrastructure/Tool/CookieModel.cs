using Infrastructure.Tool.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Tool
{
    /// <summary>
    /// CookieModel上下文
    /// </summary>
    public class CookieModelContext<T> where T : CookieModel, new()
    {
        protected string Key = string.Empty;
        protected string IV = string.Empty;
        protected string CookieName = string.Empty;
        protected T CookieModel { get; set; }

        public CookieModelContext(string Key, string IV, string CookieName)
        {
            //校验初始化
            if (Key.IsNullOrEmpty() || IV.IsNullOrEmpty() || CookieName.IsNullOrEmpty())
            {
                throw new ArgumentException("参数未配置");
            }

            this.Key = Key;
            this.IV = IV;
            this.CookieName = CookieName;
        }

        /// <summary>
        /// 取得CookieModel
        /// </summary>
        public T GetCookieModel()
        {
            if (CookieModel == null)
            {
                
                var AES = new AESHelper(Key, IV);
                var cookie = HttpContext.Current.Request.Cookies[CookieName];
                string DecryptText = AES.Decrypt(cookie.Value);
                var Values = HttpUtility.ParseQueryString(DecryptText);
                var GetCookieModel = new T();
                System.Reflection.PropertyInfo[] properties = GetCookieModel.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                foreach (System.Reflection.PropertyInfo item in properties)
                {
                    var itemValue = Values[item.Name];
                    item.SetValue(GetCookieModel, itemValue == null ? "" : itemValue.ToString());
                }
                return GetCookieModel;
            }
            else
            {
                return this.CookieModel;
            }
            
        }

        /// <summary>
        /// 保存CookieModel
        /// </summary>
        /// <param name="CookieModel"></param>
        /// <param name="ExpiresDateTime"></param>
        public void SetCookieModel(T CookieModel, DateTime ExpiresDateTime)
        {
            var AES = new AESHelper(Key, IV);
            var cookie = HttpContext.Current.Response.Cookies[CookieName];
            cookie.Expires = ExpiresDateTime;

            System.Reflection.PropertyInfo[] properties = CookieModel.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                object value = item.GetValue(CookieModel, null);
                cookie.Values.Add(item.Name, value == null ? "" : value.ToString());
            }
            //加密保存cookie
            var EncryptText = AES.Encrypt(cookie.Value);
            cookie.Value = EncryptText;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 清除Cookie
        /// </summary>
        public void ClearCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies[CookieName];
            cookie.Expires = new DateTime(1983, 7, 21);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
    }

    /// <summary>
    /// Cookie的基础数据模型，扩展请继承
    /// </summary>
    public class CookieModel
    {
        public string ID { get; set; }
    }
}
