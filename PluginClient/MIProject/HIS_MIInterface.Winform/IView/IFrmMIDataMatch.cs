using EfwControls.HISControl.Prescription.Controls.CommonMvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HIS_MIInterface.Winform.IView
{
    public interface IFrmMIDataMatch : IBaseView
    {
        /// <summary>
        /// 加载HIS数据字典
        /// </summary>
        /// <param name="dt"></param>
        void LoadHISDataInfo(DataTable dt);
        /// <summary>
        /// 加载医保数据字典
        /// </summary>
        /// <param name="dt"></param>
        void LoadMIDataInfo(DataTable dt);
        /// <summary>
        /// 加载以匹配数据字典
        /// </summary>
        /// <param name="dt"></param>
        void LoadMatchDataInfo(DataTable dt);
        /// <summary>
        /// 加载医保类型
        /// </summary>
        /// <param name="dt"></param>
        void LoadMIType(DataTable dt);
        /// <summary>
        /// 刷新界面
        /// </summary>
        void ReFresh();
    }
}
