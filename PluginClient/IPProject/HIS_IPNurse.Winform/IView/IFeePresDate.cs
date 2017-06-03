using System;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 处方日期确认接口
    /// </summary>
    public interface IFeePresDate
    {
        /// <summary>
        /// 是否勾选了床位费
        /// </summary>
        bool BedOrderChecked { get;  }

        /// <summary>
        /// 是否勾选了长期账单
        /// </summary>
        bool PresDateChecked { get;  }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get;  }

        /// <summary>
        /// 关闭当前窗体
        /// </summary>
        void CloseForm();

        /// <summary>
        /// 设置界面控件可用
        /// </summary>
        /// <param name="isBedFee">是否可用标志</param>
        void SetControlEnabled(bool isBedFee);
    }
}
