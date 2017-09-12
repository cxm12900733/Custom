using System;
using System.Reflection;
using System.Web;

namespace Infrastructure.Extend
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 把接收到得参数复制到实体中，包含文件的上传
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="_request"></param>
        /// <param name="_ShopMenu"></param>
        /// <returns></returns>
        public static T ConvertRequestToModel<T>(this HttpRequestBase _request, T _SaveModel, T _ShopMenu)
        {
            Type myType = _ShopMenu.GetType();

            Type saveType = _SaveModel.GetType();
            //复制Post的参数
            for (int i = 0; i < _request.Form.Count; i++)
            {
                PropertyInfo pinfo = myType.GetProperty(_request.Form.Keys[i]);
                PropertyInfo saveInfo = saveType.GetProperty(_request.Form.Keys[i]);
                if (saveInfo != null)
                {
                    object v = pinfo.GetValue(_ShopMenu, null);
                    try
                    {
                        saveInfo.SetValue(_SaveModel, v, null);
                    }
                    catch (Exception) { }
                }

            }

            //复制Get的参数
            for (int i = 0; i < _request.QueryString.Count; i++)
            {
                if (_request.QueryString.Keys[i] != null)
                {
                    PropertyInfo pinfo = myType.GetProperty(_request.QueryString.Keys[i]);
                    PropertyInfo saveInfo = saveType.GetProperty(_request.QueryString.Keys[i]);
                    if (saveInfo != null)
                    {
                        object v = pinfo.GetValue(_ShopMenu, null);
                        if (v != null)
                        {
                            saveInfo.SetValue(_SaveModel, v, null);
                        }
                    }
                }
            }
            return _SaveModel;
        }
    }
}
