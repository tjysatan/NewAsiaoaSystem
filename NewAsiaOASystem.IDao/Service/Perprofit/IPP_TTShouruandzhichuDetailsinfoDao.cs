using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewAsiaOASystem.DBModel;
using NewAsiaOASystem.ViewModel;

namespace NewAsiaOASystem.IDao
{
    public interface IPP_TTShouruandzhichuDetailsinfoDao:IBaseDao<PP_TTShouruandzhichuDetailsinfoView>
    {
        /// <summary>
        /// 根据团队ID 查找该团队当月的团体支出的明细
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="type">类型 0 收入 1支出</param>
        /// <returns></returns>
        IList<PP_TTShouruandzhichuDetailsinfoView> GetTTszMxtoMonbyteamId(string teamId, string type);

        /// <summary>
        /// 根据团队Id项目Id完成时间查找团体项目 明细
        /// </summary>
        /// <param name="teamId">团队Id</param>
        /// <param name="profuId">项目Id</param>
        /// <param name="wctime">完成时间</param>
        /// <returns></returns>
        PP_TTShouruandzhichuDetailsinfoView GetTTSZMXinfobyteamIdandprofuIdandwctime(string teamId, string profuId, string wctime);



        #region //插入保存 返回Id
        string InsertID(PP_TTShouruandzhichuDetailsinfoView modelView); 
        #endregion

        /// <summary>
        /// 团队（不固定分配）收入项分页数据
        /// </summary>
        /// <param name="srname"></param>
        /// <param name="jltime"></param>
        /// <param name="Iswcfp"></param>
        /// <param name="TeamId"></param>
        /// <returns></returns>
        PagerInfo<PP_TTShouruandzhichuDetailsinfoView> GetTTshourutwelistpage(string srname, string jltime, string Iswcfp, string TeamId);
    }
}
