using NewAsiaOASystem.ViewModel;
using NewAsiaOASystem.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace NewAsiaOASystem.Web
{
    public class GetControllerInfo
    {
        /// <summary>
        /// 从控制器中获取信息
        /// </summary>
        public List<GetControllerInfoView> GetFromControllerInfo()
        {
            var controllers =
                from t in GetAllControllerTypes()
                where typeof(Controller).IsAssignableFrom(t) && !t.IsAbstract
                orderby t.FullName
                from m in t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                where !m.IsSpecialName
                select new { ControllerName= FormatControllerName(t.FullName), ActionName = m.Name, Params = m.GetParameters() };

            List<GetControllerInfoView> list = new List<GetControllerInfoView>();
            foreach (var item in controllers.ToList())
            {
                GetControllerInfoView controllerView = new GetControllerInfoView();
                controllerView.ControllerName = item.ControllerName;
                controllerView.ActionName = item.ActionName;
                list.Add(controllerView);
            }

            return list;
            //.ForEach(c => );
            //controllers.ToList().ForEach(c => Debug.WriteLine(string.Format("Controller: {0}, Action: {1}({2})",
            //                                                                c.ControllerName, c.ActionName,
            //                                                                string.Join(", ",
            //                                                                            c.Params.Select(p => p.Name).
            //                                                                                ToArray()))));
            //Debug.WriteLine(string.Format("Controller/action count: {0}", controllers.Count()));
            //Debug.WriteLine(string.Format("Controller count: {0}", controllers.GroupBy(c => c.ControllerName).Count()));

        }

        /// <summary>
        /// 获取所有的控制器类型
        /// </summary>
        /// <returns>all types in an assembly where my controllers can be found</returns>
        private static Type[] GetAllControllerTypes()
        {
            return typeof(SysFunctionController).Assembly.GetTypes();
        }

        /// <summary>
        /// 返回控制器名称
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        private static string FormatControllerName(string typeName)
        {
            return typeName.Replace("NewAsiaOASystem.Web.", string.Empty).Replace("Controllers.", string.Empty).Replace("Controller", string.Empty);
        }
    }
}