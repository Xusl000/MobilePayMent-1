﻿using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PayCore.Utils
{
    /// <summary>
    /// 支付的相关操作
    /// </summary>
    public static class Utility
    {
        #region 方法

        /// <summary>
        /// 获得字符串的MD5值，MD5值为大写
        /// </summary>
        /// <param name="text">字符串</param>
        public static string GetMD5(string text)
        {
            return GetMD5(text, Encoding.UTF8);
        }


        /// <summary>
        /// 获得字符串的MD5值，MD5值为大写
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="textEncoding">字符串编码</param>
        /// <returns></returns>
        public static string GetMD5(string text, Encoding textEncoding)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(textEncoding.GetBytes(text));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// 读取网页，返回网页内容
        /// </summary>
        /// <param name="pageUrl">网页URL</param>
        /// <returns></returns>
        public static string ReadPage(string pageUrl)
        {
            return ReadPage(pageUrl, Encoding.UTF8);
        }


        /// <summary>
        /// 读取网页，返回网页内容
        /// </summary>
        /// <param name="pageUrl">网页URL</param>
        /// <param name="pageEncoding">网页编码</param>
        /// <returns></returns>
        public static string ReadPage(string pageUrl, Encoding pageEncoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pageUrl);
            request.Method = "GET";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), pageEncoding))
                    {
                        if (reader != null)
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                request.Abort();
            }

            return string.Empty;
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string TimeStamp(this DateTime date)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        public static string GetClientIP()
        {
            string ip = HttpUtil.Current.Request.Headers["X-Real-IP"];
            if (string.IsNullOrEmpty(ip) || ip.Length == 0)
            {
                ip = HttpUtil.Current.Request.Headers["X-Forwarded-For"];
            }
            if (string.IsNullOrEmpty(ip) || ip.Length == 0)
            {
                ip = HttpUtil.Current.Request.Headers["Proxy-Client-IP"];
            }
            if (string.IsNullOrEmpty(ip) || ip.Length == 0)
            {
                ip = HttpUtil.Current.Request.Headers["WL-Proxy-Client-IP"];
            }
            if (string.IsNullOrEmpty(ip) || ip.Length == 0)
            {
#if NETSTANDARD2_0
                ip = HttpUtil.Current.Request.Host.Host;
#elif NET461
                ip = HttpUtil.Current.Request.UserHostAddress;
#endif
            }
            if (!string.IsNullOrEmpty(ip))
            {
                var ips = ip.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (ips.Length > 1)
                {
                    ip = ips[0].Trim();
                }
                var ipport = ip.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (ipport.Length > 1)
                {
                    ip = ipport[0].Trim();
                }
            }
            return ip;
        }

        #endregion
    }
}
