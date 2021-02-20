using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;

namespace NewAsiaOASystem
{
    public class QRHelper
    {
        /// <summary> 
        /// 生成二维码 
        /// </summary> 
        /// <param name="qrCodeContent">要编码的内容</param> 
        /// <returns>返回二维码位图</returns> 
        public static Bitmap QRCodeEncoderUtil(string qrCodeContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeVersion = 0;
            Bitmap img = qrCodeEncoder.Encode(qrCodeContent, Encoding.UTF8);//指定utf-8编码， 支持中文 
            return img;
        }

        /// <summary> 
        /// 解析二维码 
        /// </summary> 
        /// <param name="bitmap">要解析的二维码位图</param> 
        /// <returns>解析后的字符串</returns> 
        public static string QRCodeDecoderUtil(Bitmap bitmap)
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(bitmap), Encoding.UTF8);//指定utf-8编码， 支持中文 
            return decodedString;
        }


        /// <summary>
        ///  //获取制定月的最后一天
        /// </summary>
        /// <param name="Year">年</param>
        /// <param name="Mon">月</param>
        /// <returns></returns>
        public static string GetMonLastDAY(string Year, string Mon)
        {
            DateTime d = new DateTime(Convert.ToInt32(Year),Convert.ToInt32(Mon), 1).AddMonths(1).AddDays(-1);
            return d.ToString("yyyy-MM-dd");
        }


        #region //根据日期和当前的时间获取天数,如果是周六周天就减少一天（星期几0~6 星期日~星期）
        public static string Getdkxxdczts(string czdatetime)
        {
            DateTime nowdate = DateTime.Now;//当前时间
            DateTime czdatetimenew = Convert.ToDateTime(czdatetime);//上线时间
            int DAYSUM = DateDiff(czdatetimenew,nowdate);
            string weekdat = nowdate.DayOfWeek.ToString();
            if (weekdat == "0" || weekdat == "6")
            {
                DAYSUM = DAYSUM - 1;
            }
            return DAYSUM.ToString();
        }




            #region //俩个时间之间的天数
         public static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());
            TimeSpan sp = end.Subtract(start);
            return sp.Days;
        }
        #endregion
        #endregion

        #region //全角转半角
         /**/
        // /
        // / 转半角的函数(DBC case)
        // /
        // /任意字符串
        // /半角字符串
        // /
        // /全角空格为12288，半角空格为32
        // /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        // /
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion


    }
}