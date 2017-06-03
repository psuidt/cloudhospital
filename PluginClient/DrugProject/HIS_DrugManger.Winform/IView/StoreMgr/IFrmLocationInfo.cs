using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.IView.StoreMgr
{
    /// <summary>
    /// 库位详情
    /// </summary>
    interface IFrmLocationInfo : IBaseView
    {
       /// <summary>
       /// 根据库位获取库位详情
       /// </summary>
       /// <param name="location">库位对象</param>
        void GetLocationInfo(DG_Location location);

        /// <summary>
        /// 保存成功执行的函数
        /// </summary>
        /// <param name="statu">状态</param>
        void SaveComplete(bool statu);
    }
}
