using Infrastructure.Tool.Cryptography;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Tool
{
    /// <summary>
    /// 持久化操作员上下文(*登录认证Cookie实现)
    /// </summary>
    public class OperatorContext<T> where T : IOperatorModel, new()
    {
        protected string Key = string.Empty;
        protected string IV = string.Empty;
        protected string CookieName = string.Empty;

        public OperatorContext(string Key, string IV, string CookieName)
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
        /// 取得OperatorModel
        /// </summary>
        public T GetOperatorModel()
        {
            var cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                var CookieValue = cookie.Values[CookieName];

                var AES = new AESHelper(Key, IV);
                string DecryptText = AES.Decrypt(CookieValue);
                T OperatorModel = JsonConvert.DeserializeObject<T>(DecryptText);
                return OperatorModel;
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 保存OperatorModel
        /// </summary>
        /// <param name="CookieModel"></param>
        /// <param name="ExpiresDateTime"></param>
        public void SetOperatorModel(T OperatorModel, DateTime ExpiresDateTime)
        {
            var cookie = HttpContext.Current.Response.Cookies[CookieName];
            cookie.Expires = ExpiresDateTime;

            var AES = new AESHelper(Key, IV);
            string Json = JsonConvert.SerializeObject(OperatorModel);
            var EncryptText = AES.Encrypt(Json);
            cookie.Values.Add(CookieName, EncryptText);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        /// <summary>
        /// 清除OperatorModel
        /// </summary>
        public void ClearOperatorModel()
        {
            var cookie = HttpContext.Current.Request.Cookies[CookieName];
            cookie.Expires = new DateTime(1983, 7, 21);
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
    }

    /// <summary>
    /// 持久化操作员数据模型接口
    /// </summary>
    public interface IOperatorModel
    {
        string ID { get; set; }
    }
}
