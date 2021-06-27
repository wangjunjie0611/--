using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Community.Common.Session
{
    public class SessionInfo
    {
        public static IHttpContextAccessor _httpContextAccessor;



        public static string PId
        {
            get { return GetSession("pid"); }
            set { SetSession("pid", value); }
        }
        /// 获取session值
        /// </summary>
        /// <param name="key">session的key</param>
        /// <returns></returns>
        private static string GetSession(string key)
        {
            string value = string.Empty;
            byte[] obj = null;
            //在类中使用session通过out byte[],来返回session.
            _httpContextAccessor.HttpContext.Session.TryGetValue(key, out obj);
            return System.Text.Encoding.Default.GetString(obj);//将字节数组转换成字符串
        }
        /// <summary>
        /// 设置session
        /// </summary>
        /// <param name="key">session的key</param>
        /// <param name="obj">要调置的对像</param>
        private static void SetSession(string key, object obj)
        {
            //System.Text.Encoding.Default.GetBytes(obj.ToString()); //将字符串转换成字节
            _httpContextAccessor.HttpContext.Session.Set(key, System.Text.Encoding.Default.GetBytes(obj.ToString()));

        }


    }
}
