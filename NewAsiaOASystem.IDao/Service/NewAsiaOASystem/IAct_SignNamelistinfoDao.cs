using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IAct_SignNamelistinfoDao:IBaseDao<Act_SignNamelistinfoView>
    {
        /// <summary>
        /// 根据公司名称查询
        /// </summary>
        /// <param name="company">公司名称</param>
        /// <returns></returns>
        Act_SignNamelistinfoView Gethuiyimingdanmodelbycompany(string company);

          //根据与会者查询
        /// <summary>
        /// 根据与会者查询会与人名单
        /// </summary>
        /// <param name="name">与会者</param>
        /// <returns></returns>
        Act_SignNamelistinfoView GethuiyimingdanbyName(string name);

        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="dm">编码</param>
        /// <returns></returns>
        Act_SignNamelistinfoView Gethuiyimingdanbydm(string dm);

         //根据签到时间排序查询出已经签到没有在头部显示过的名单
        Act_SignNamelistinfoView Getyijingqiandaomeiyoupiaoguo();

        //根据签到时间的排序查处已经签到已经飘过的名单
        IList<Act_SignNamelistinfoView> Getyijingqiandaoyijingpiaoguo();
    }
}
