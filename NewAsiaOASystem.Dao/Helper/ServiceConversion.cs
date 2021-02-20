using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Spring.Data;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.Dao
{
    public abstract class ServiceConversion<TDTO, TDATA> : DBService<TDATA>
        where TDATA : class, new()
        where TDTO : class, new()
    {
        public virtual String GetSearchHql()
        {
            return string.Format(" from {0} where 1=1 ", typeof(TDATA).Name);
        }
        public void SetPagerPageIndex(int index)
        {
            pager.CurrentPageIndex = index;
        }

        public void SetPagerPageSize(int size)
        {
            pager.PageSize = size;
        }

        public PagerInfo<TDTO> pager;

        public ServiceConversion()
        {
            this.pager = new PagerInfo<TDTO>();
        }

        /// <summary>
        /// 从DTO转换为数据操纵对象
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDATA ConvertToData(TDTO dto)
        {
            TDATA data = new TDATA();
            return (TDATA)ConvertETE(dto, data);
        }



        /// <summary>
        /// 将数据操作对象转换为TDTO对象
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDTO ConvertToDTO(TDATA data)
        {
            TDTO dto = new TDTO();
            return (TDTO)ConvertETE(data, dto);
        }

        /// <summary>
        /// 从DTO转换为数据操纵对象
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDATA ConvertToData(TDTO dto, TDATA Outdata)
        {
            if (Outdata == null)
                Outdata = new TDATA();

            return (TDATA)ConvertETE(dto, Outdata); ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TDTO ConvertToDTO(TDATA data, TDTO outDTO)
        {
            if (outDTO == null)
                outDTO = new TDTO();
            return (TDTO)ConvertETE(data, outDTO);

        }

        object ConvertETE(object objInput, object objOut)
        {
            //获取输入对象属性集合
            List<PropertyInfo> FronPro = objInput.GetType().GetProperties().ToList<PropertyInfo>();
            //获取输出对象属性集合
            List<PropertyInfo> BackPro = objOut.GetType().GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo item in FronPro)
            {
                //获取属性名称与属性类型一致的属性
                PropertyInfo temp = BackPro.Find(x => x.Name == item.Name && x.PropertyType == item.PropertyType);

                if (temp == null)
                    continue;
                temp.SetValue(objOut, item.GetValue(objInput, null), null);
            }
            return objOut;
        }

        /// <summary>
        /// 数据操纵对象转换为DTO，单个对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public virtual TDTO GetView(TDATA data)
        {
            TDTO view = ConvertToDTO(data);
            return view;
        }
        /// <summary>
        /// 从DTO 转换成数据操作对象，单个对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual TDATA GetData(TDTO data)
        {
            TDATA view = ConvertToData(data);
            return view;
        }

        /// <summary>
        /// 将数据操作对象集合转换为DTO集合，传入对象集合
        /// </summary>
        /// <param name="data">数据对象集合</param>
        /// <returns></returns>
        public virtual IList<TDTO> GetViewlist(List<TDATA> data)
        {
            if (data == null)
                return null;
            if (data.Count <= 0)
                return null;
            return data.ConvertAll<TDTO>(x => GetView(x));
        }

        /// <summary>
        /// 将tdo集合转换为数据对象集合
        /// </summary>
        /// <param name="tdo">tdo数据对象集合</param>
        /// <returns></returns>
        public IList<TDATA> GetDatalist(List<TDTO> tdo)
        {
            if (tdo == null)
                return null;
            if (tdo.Count <= 0)
                return null;
            return tdo.ConvertAll<TDATA>(x => GetData(x));
        }


        public virtual PagerInfo<TDTO> Search()
        {
            ISession session = GetSession();
            string hql = GetSearchHql();
            string tempHql = hql;
            ///得到第一个from
            int inde = tempHql.IndexOf("from");
            if (tempHql.Contains("group"))
            {
                int inde1 = tempHql.IndexOf("group");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("GROUP"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("GROUP");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("order"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("order");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else if (tempHql.Contains("ORDER"))///获得from和后边的字符
            {
                int inde1 = tempHql.IndexOf("ORDER");
                tempHql = tempHql.Substring(inde, inde1 - inde);
            }
            else
            {
                tempHql = tempHql.Substring(inde);
            }

            IQuery queryCount = session.CreateQuery(string.Format("select count(*)  {0} ", tempHql));
            //计算总记录数       
            pager.RecordCount = Convert.ToInt32(queryCount.UniqueResult());
            IQuery query = session.CreateQuery(hql);
            //计算起始的行
            int index = (pager.CurrentPageIndex - 1) * pager.PageSize;
            if (index > pager.RecordCount)
            {
                index = 0;
                pager.CurrentPageIndex = 1;
            }
            query.SetFirstResult(index);
            query.SetMaxResults(pager.PageSize);
            IList<TDATA> tempData = query.List<TDATA>();
            pager.DataList = (tempData as List<TDATA>).ConvertAll(x => GetView(x));
            pager.GetPagingDataJson = JsonConvert.SerializeObject(pager.DataList);
            return pager;
        }


 
    }
}
