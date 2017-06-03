using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    public interface IQueryMenber: IBaseView
    {
        /// <summary>
        /// 病人手机号、姓名、身份证号、医保卡号
        /// </summary>
        string StrPatInfo { get; set; }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="patDt">病人列表</param>
        void Bind_PatList(DataTable patDt);

        /// <summary>
        /// 获取当前会员信息数据源
        /// </summary>
        /// <returns>当前会员信息数据源</returns>
        DataTable GetPatInfoDatable();

        /// <summary>
        /// 获取当前选定的行
        /// </summary>
        int GetCurRowIndex { get; }
    }
}
