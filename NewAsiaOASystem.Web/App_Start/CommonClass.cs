using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewAsiaOASystem.Web
{
    public class CommonClass
    {
        public bool ValidateImg(string imgName)
        {
            string[] imgType = new string[] { "gif", "jpg", "png", "bmp" };

            int i = 0;
            bool blean = false;
            string message = string.Empty;

            //判断是否为Image类型文件
            while (i < imgType.Length)
            {
                if (imgName.Equals(imgType[i].ToString()))
                {
                    blean = true;
                    break;
                }
                else if (i == (imgType.Length - 1))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return blean;
        }
        public string upLoadImg(HttpPostedFileBase imgFile)
        {
            try
            {
                //上传和返回(保存到数据库中)的路径
                string uppath = string.Empty;
                //保存到数据库目录
                string Sapath = string.Empty;
                if (imgFile != null)
                {
                    //创建图片新的名称
                    string nameImg = Guid.NewGuid().ToString();
                    //获得上传图片的路径
                    string strPath = imgFile.FileName;
                    //获得上传图片的类型(后缀名)
                    string type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                    if (ValidateImg(type))
                    {
                        //拼写上传图片的路径
                        uppath = HttpContext.Current.Server.MapPath("~/UploadImg/");
                        Sapath = "/UploadImg/";
                        uppath += nameImg + "." + type;
                        Sapath += nameImg + "." + type;
                        //上传图片
                        imgFile.SaveAs(uppath);
                    }
                    return Sapath;
                }

                return "";
            }

            catch
            {
                return "";
            }
            
        }

        internal int checkInput(string pageIndex, int count)
        {
            throw new NotImplementedException();
        }
    }
}