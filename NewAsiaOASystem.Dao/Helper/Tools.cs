using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace NewAsiaOASystem.Dao
{
   public class Tools
    {
        public static DateTime GetDateTimeForUnix(string timeStamp)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            long ticks = long.Parse(timeStamp + "0000000");
            TimeSpan span = new TimeSpan(ticks);
            return time.Add(span);
        }

        public static string GetUnixTimeForDateTimeNow()
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            string str = DateTime.Parse(DateTime.Now.ToString()).Subtract(time).Ticks.ToString();
            return str.Substring(0, str.Length - 7);
        }

        public static string GetUnixTimeForDateTimeNow(string dateTime)
        {
            DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(0x7b2, 1, 1));
            string str = DateTime.Parse(dateTime).Subtract(time).Ticks.ToString();
            return str.Substring(0, str.Length - 7);
        }

        public static string SubmitToUrl(string url, string xml, string method)
        {
            Stream responseStream;
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(xml);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            request.Method = method;
            request.ContentLength = bytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            try
            {
                responseStream = request.GetResponse().GetResponseStream();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            string str = string.Empty;
            using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
            {
                str = reader.ReadToEnd();
            }
            requestStream.Close();
            responseStream.Close();
            return str;
        }
    }
}
