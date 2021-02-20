using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.IDao;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;
using System.Data;

namespace NewAsiaOASystem.Dao
{
    public class SysLog_historyDao :ServiceConversion<SysLog_historyView,SysLog_history> ,ISysLog_historyDao
    {
         /// <summary>
        /// 覆写查询的的HQL 语句
        /// </summary>
        /// <returns></returns>
        public override String GetSearchHql()
        {
            return " from SysLog_history  where 1=1  order by LogonTime desc";
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Ninsert(SysLog_historyView model)
        {
            SysLog_history listmodel = GetData(model);
            return base.insert(listmodel);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NUpdate(SysLog_historyView model)
        {
            SysLog_history listmodel = GetData(model);
            return base.Update(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(SysLog_historyView model)
        {
            SysLog_history listmodel = GetData(model);
            return base.Delete(listmodel);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool NDelete(List<SysLog_historyView> model)
        {
            IList<SysLog_history> listmodel = GetDatalist(model);
            return base.NDelete(listmodel);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public IList<SysLog_historyView> NGetList()
        {
            List<SysLog_history> listdata = base.GetList() as List<SysLog_history>;
            IList<SysLog_historyView> listmodel = GetViewlist(listdata);
            return listmodel;
        } 
        /// <summary>
        /// 根据多个ID  查询多条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<SysLog_historyView> NGetList_id(string id)
        {
            List<SysLog_history> list = base.GetList_id(id) as List<SysLog_history>;
            IList<SysLog_historyView> listmodel = GetViewlist(list);
            return listmodel;
        }

        /// <summary>
        /// 根据ID 查询一条记录的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysLog_historyView NGetModelById(string id)
        {
            SysLog_historyView listmodel = GetView(base.GetModelById(id));
            return listmodel;
        }


        /// <summary>
        /// datatable转换成 json 格式的方法
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public  string CreateJsonParameters(DataTable dt)
        {

            StringBuilder JsonString = new StringBuilder();
            //Exception Handling        
            if (dt != null && dt.Rows.Count > 0)
            {
                JsonString.Append("[ ");
                //  JsonString.Append("\"T_blog\":[ ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JsonString.Append("{ ");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JsonString.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    /**/
                    /**/
                    /**/
                    /*end Of String*/
                    if (i == dt.Rows.Count - 1)
                    {
                        JsonString.Append("} ");
                    }
                    else
                    {
                        JsonString.Append("}, ");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
