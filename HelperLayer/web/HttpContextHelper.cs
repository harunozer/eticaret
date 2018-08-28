using HelperLayer.Permissions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace HelperLayer.web
{
    public static class HttpContextHelper
    {
        public static string GetClientIP(this HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress.ToString();
        }

        public static bool CheckPermission(this HttpContext httpContext, PermissionModulEnum modul, PermissionEnum permission)
        {
            //TODO Login olan üyenin rolü işlem için uygun mu kontrolü
            return true;
        }

        /// <summary>
        /// sırasıyla QueryString, Form değerlerini verir.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetRequestItem(this HttpContext httpContext, string key)
        {
            return httpContext.QueryStringGet(key) != "" ? httpContext.QueryStringGet(key) : httpContext.FormGet(key);
        }

        #region SessionExtensions
        public static string SessionStringGet(this HttpContext httpContext, string key)
        {
            return httpContext.Session.GetString(key);
        }

        public static void SessionObjectSet(this HttpContext httpContext, string key, object value)
        {
            httpContext.Session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }

        public static T SessionObjectGet<T>(this HttpContext httpContext, string key)
        {
            var value = httpContext.SessionStringGet(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SessionAbandon(this HttpContext httpContext)
        {
            httpContext.Session.Clear();
        }

        public static void SessionSetLoginUser<T>(this HttpContext httpContext, T user)
        {
            httpContext.SessionObjectSet(Consts.SessionLoginUser, user);
        }

        public static T SessionGetLoginUser<T>(this HttpContext httpContext)
        {
            return httpContext.SessionObjectGet<T>(Consts.SessionLoginUser);
        }
        

        #endregion

        #region CookieExtensions
        public static void CookieSet(this HttpContext httpContext, string Key, string value, CookieOptions options = null)
        {
            //options verilmezse default değerler
            if (options == null)
            {
                options = new CookieOptions();
                //options.HttpOnly = true;
                options.MaxAge = TimeSpan.FromDays(30);
                //options.Secure = true;
            }

            //value TEA gibi kütüphane ile şifrelenerek basılıp okunabilir.
            httpContext.Response.Cookies.Append(Key, value, options);
        }

        public static string CookieGet(this HttpContext httpContext, string key)
        {
            if (httpContext.Request.Cookies.ContainsKey(key))
                return httpContext.Request.Cookies[key].ToString();
            return null;
        }

        public static void CookieObjectSet<T>(this HttpContext httpContext, string Key, object value, CookieOptions options = null)
        {
            httpContext.CookieSet(Key, JsonConvert.SerializeObject(value), options);
        }

        public static T CookieObjectGet<T>(this HttpContext httpContext, string key)
        {
            var value = httpContext.CookieGet(key);

            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void CookieRemove(this HttpContext httpContext, string Key)
        {
            httpContext.Response.Cookies.Delete(Key);
        }

        #endregion

        #region QueryString
        public static string QueryStringGet(this HttpContext httpContext, string key)
        {
            return httpContext.Request.Query[key].ToString();
        }
        #endregion

        #region Form
        public static string FormGet(this HttpContext httpContext, string key)
        {
            if (!httpContext.Request.HasFormContentType) return "";
            if (!httpContext.Request.Form.ContainsKey(key)) return "";
            return httpContext.Request.Form[key].ToString();
        }
        #endregion

    }
}
