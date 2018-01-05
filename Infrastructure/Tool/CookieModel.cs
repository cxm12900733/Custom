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
    public abstract class CookieModelContext<T> where T : CookieModel, new()
    {
        private string key = "r3oDgh2id9FMDjKgWC6eB5A9OmQBmrLY";
        private string IV = "p52kkXLyco6oHOwP";
        private string CookieName;

        public T GetModel()
        {
            return new T();
        }

        public void SetModel(T Model, DateTime ExpiresDateTime)
        {
            var AES = new AESHelper(key, IV);


            var cookie = HttpContext.Current.Response.Cookies[CookieName];
            cookie.Expires = ExpiresDateTime;
            System.Reflection.PropertyInfo[] properties = Model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                object value = item.GetValue(Model, null);
                cookie.Values.Add(item.Name, value.ToString());
            }
            cookie.Value
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void ClearCookie()
        {

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
