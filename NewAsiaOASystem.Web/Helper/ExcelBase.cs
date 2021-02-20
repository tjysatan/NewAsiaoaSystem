using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
//using Microsoft.Office.Interop.Excel;
using org.in2bits.MyXls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.SS.Util;

namespace NewAsiaOASystem.Web
{
    public class ExcelBase
    {
        #region Myxls导出Excel (传入DataTable)
        /// <summary>
        /// Myxls导出Excel(传入DataTable)
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="xlsname">文件名</param>
        /// <param name="table">内存表</param>
        public void ExportExcelForPercentForWeb(string sheetName, string xlsname, System.Data.DataTable table)
        {

            XlsDocument xls = new XlsDocument();
            Worksheet sheet = xls.Workbook.Worksheets.Add(sheetName);
            try
            {

                if (table == null || table.Rows.Count == 0) { return; }



                // 数据单元格样式  
                XF dataXF = xls.NewXF(); // 为xls生成一个XF实例，XF是单元格格式对象  
                dataXF.HorizontalAlignment = HorizontalAlignments.Centered; // 设定文字居中  
                dataXF.VerticalAlignment = VerticalAlignments.Centered; // 垂直居中  
                dataXF.UseBorder = true; // 使用边框   
                dataXF.LeftLineStyle = 1; // 左边框样式  
                dataXF.LeftLineColor = Colors.Black; // 左边框颜色  
                dataXF.BottomLineStyle = 1;  // 下边框样式  
                dataXF.BottomLineColor = Colors.Black;  // 下边框颜色  
                dataXF.Font.FontName = "宋体";
                //dataXF.Font.Height = 9 * 20; // 设定字大小（字体大小是以 1/20 point 为单位的）  
                //dataXF.UseProtection = false; // 默认的就是受保护的，导出后需要启用编辑才可修改  
                //dataXF.TextWrapRight = true; // 自动换行  

                //单独给最右单元格设置边框
                XF lastRightXF = xls.NewXF();
                lastRightXF.RightLineStyle = 1;//右边样式
                lastRightXF.RightLineColor = Colors.Black;
                lastRightXF.LeftLineStyle = 1; // 左边框样式  
                lastRightXF.LeftLineColor = Colors.Black; // 左边框颜色  
                lastRightXF.BottomLineStyle = 1;  // 下边框样式  
                lastRightXF.BottomLineColor = Colors.Black;  // 下边框颜色  
                lastRightXF.Font.FontName = "宋体";

                //单独给最上边单元格设置边框
                XF lastTopXF = xls.NewXF();
                lastTopXF.HorizontalAlignment = HorizontalAlignments.Centered; // 设定文字居中  
                lastTopXF.VerticalAlignment = VerticalAlignments.Centered; // 垂直居中  
                lastTopXF.TopLineStyle = 1;//右边样式
                lastTopXF.TopLineColor = Colors.Black;
                lastTopXF.RightLineStyle = 1;//右边样式
                lastTopXF.RightLineColor = Colors.Black;
                lastTopXF.LeftLineStyle = 1; // 左边框样式  
                lastTopXF.LeftLineColor = Colors.Black; // 左边框颜色  
                lastTopXF.BottomLineStyle = 1;  // 下边框样式  
                lastTopXF.BottomLineColor = Colors.Black;  // 下边框颜色  
                lastTopXF.Font.FontName = "宋体";
                //填充表头  
                foreach (DataColumn col in table.Columns)
                {
                    sheet.Cells.Add(1, col.Ordinal + 1, col.ColumnName, lastTopXF);
                }

                //填充内容  
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j + 1 == table.Columns.Count)
                            sheet.Cells.Add(i + 2, j + 1, table.Rows[i][j].ToString(), lastRightXF);
                        else
                            sheet.Cells.Add(i + 2, j + 1, table.Rows[i][j].ToString(), dataXF);

                    }
                }

                //ws.Cells.Merge(1, 2, 2, 2);
             
              

                #region 客户端保存
                using (MemoryStream ms = new MemoryStream())
                {
                    xls.Save(ms);
                    ms.Flush();
                    ms.Position = 0;
                    sheet = null;
                    xls = null;
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();

                    response.Charset = "UTF-8";
                    response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + xlsname));
                    byte[] data = ms.ToArray();
                    System.Web.HttpContext.Current.Response.BinaryWrite(data);

                }

                #endregion
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sheet = null;
                xls = null;
            }

        }
        # endregion

        #region Myxls通过读取模板导出Excel
        /// <summary>
        /// Myxls通过读取模板导出Excel
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="xlsname">文件名</param>
        /// <param name="TemplateUrl">模板路径</param>
        /// <param name="table">内存表</param>
        public void ExportExcelTemplate(string sheetName, string xlsname, string TemplateUrl, System.Data.DataTable table)
        {
            XlsDocument xls = new XlsDocument(TemplateUrl);
            xls.FileName = System.IO.Path.GetFileName(TemplateUrl);

            Worksheet sheet = xls.Workbook.Worksheets[0];
            try
            {

                if (table == null || table.Rows.Count == 0)
                    return;

                //填充内容  
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        sheet.Cells.Add(i + 8, j + 1, table.Rows[i][j].ToString());
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    xls.Save(ms);
                    ms.Flush();
                    ms.Position = 0;
                    sheet = null;
                    xls = null;
                    HttpResponse response = System.Web.HttpContext.Current.Response;
                    response.Clear();

                    response.Charset = "UTF-8";
                    response.ContentType = "application/vnd-excel";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + xlsname));
                    byte[] data = ms.ToArray();
                    System.Web.HttpContext.Current.Response.BinaryWrite(data);

                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                sheet = null;
                xls = null;
            }

        }

        #endregion

        #region NPOI读取模板下载导出Excel
        public void ToExcel(System.Data.DataTable dt, string TemplateUrl, int SUMRS)
        {
            if (dt == null || dt.Rows.Count == 0)
                return;
            //导出文件
            string ReportFileName = System.IO.Path.GetFileName(TemplateUrl);
            FileStream file = new FileStream(TemplateUrl, FileMode.Open, FileAccess.Read);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            HSSFSheet ws = hssfworkbook.GetSheet("Sheet1") as HSSFSheet;

            ICellStyle cellstyle = hssfworkbook.CreateCellStyle();
            cellstyle.BorderBottom = BorderStyle.Thin;
            cellstyle.BorderLeft = BorderStyle.Thin;
            cellstyle.BorderRight = BorderStyle.Thin;
            cellstyle.BorderTop = BorderStyle.Thin;
            int yes_myzrs=0;//已免疫人数
            int no_myzrs = 0;//未免疫人数
            string yes_myzrsbfb;//已经免疫百分比
            string no_myzrsbfb;//未免疫百分比
            string lcbfb;//流出百分比
            string wclbfb;//未处理百分比
            //填充内容  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    IRow row = ws.GetRow(i + 5);
                    ICell cell = row.GetCell(j);
                    cell.CellStyle = cellstyle;
                    ws.GetRow(i + 5).GetCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    //sheet.Cells.Add(i + 8, j + 1, table.Rows[i][j].ToString());
                }
            }

            for (int i = 0; i < 18; i++)
            {
                yes_myzrs =yes_myzrs+Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][i+1]);
            }

            for (int i = 0; i < 8; i++)
            {
                no_myzrs = no_myzrs + Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][i + 19]);
            }

            yes_myzrsbfb = GetBFB(yes_myzrs, SUMRS);
            no_myzrsbfb = GetBFB(no_myzrs, SUMRS);
            lcbfb = GetBFB(Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][27]), SUMRS);
            wclbfb = GetBFB(Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][28]), SUMRS);


           SetCellRangeAddress(ws, dt.Rows.Count + 5, dt.Rows.Count + 5, 1, 18);//合并已免疫单元格
           SetCellRangeAddress(ws, dt.Rows.Count + 5, dt.Rows.Count + 5, 19, 26);//合并未免疫单元格
           ws.GetRow(dt.Rows.Count + 5).GetCell(0).SetCellValue("汇总");// 
           ws.GetRow(dt.Rows.Count + 5).GetCell(1).SetCellValue(yes_myzrs.ToString());//已处理总人数
           ws.GetRow(dt.Rows.Count + 5).GetCell(19).SetCellValue(no_myzrs.ToString());//已处理总人数
           ws.GetRow(dt.Rows.Count + 5).GetCell(27).SetCellValue(dt.Rows[dt.Rows.Count - 1][27].ToString());//流出人数
           ws.GetRow(dt.Rows.Count + 5).GetCell(28).SetCellValue(dt.Rows[dt.Rows.Count - 1][28].ToString());//未处理人数

           SetCellRangeAddress(ws, dt.Rows.Count + 6, dt.Rows.Count + 6, 1, 18);//合并已免疫单元格
           SetCellRangeAddress(ws, dt.Rows.Count + 6, dt.Rows.Count + 6, 19, 26);//合并未免疫单元格

           ws.GetRow(dt.Rows.Count + 6).GetCell(0).SetCellValue("百分比（%）");// 
           ws.GetRow(dt.Rows.Count + 6).GetCell(1).SetCellValue(yes_myzrsbfb.ToString());//已处理总人数百分比
           ws.GetRow(dt.Rows.Count + 6).GetCell(19).SetCellValue(no_myzrsbfb.ToString());//已处理总人数百分比
           ws.GetRow(dt.Rows.Count + 6).GetCell(27).SetCellValue(lcbfb.ToString());//流出人数百分比
           ws.GetRow(dt.Rows.Count + 6).GetCell(28).SetCellValue(wclbfb.ToString());//未处理人数百分比


            ws.ForceFormulaRecalculation = true;
            

            using (FileStream filess = File.OpenWrite(ReportFileName))
            {
                hssfworkbook.Write(filess);
            }

            //filess.Close();

            HttpResponse response = System.Web.HttpContext.Current.Response;
            System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
            response.Clear();
            response.Charset = "UTF-8";
            response.ContentEncoding = System.Text.Encoding.UTF8;
            // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
            response.AddHeader("Content-Disposition", "attachment; filename="
                + System.Web.HttpContext.Current.Server.UrlEncode(DateTime.Now.ToString("yyyyMMdd") + ".xls"));
            // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
            response.AddHeader("Content-Length", filet.Length.ToString());

            // 指定返回的是一个不能被客户端读取的流，必须被下载 
            response.ContentType = "application/ms-excel";

            // 把文件流发送到客户端 
            response.WriteFile(filet.FullName);
            // 停止页面的执行 
            response.End();
        }


        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        #region //计算百分数
        public string GetBFB(int a, int b)
        {
            if (b != 0)
            {
                Double percent = Convert.ToDouble(a) / Convert.ToDouble(b) * 100;
                string result = string.Format("{0:0.00}", percent);
                return result;
            }
            else
            {
                return "0";
            }
        }
        #endregion

        #endregion
    }
}