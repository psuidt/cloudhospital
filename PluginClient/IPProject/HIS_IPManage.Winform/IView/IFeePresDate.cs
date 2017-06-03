using System;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 处方日期确认接口
    /// </summary>
    public interface IFeePresDate
    {
        /// <summary>
        /// 床位费
        /// </summary>
        bool BedOrderChecked { get;  }

        /// <summary>
        /// 长期账单
        /// </summary>
        bool PresDateChecked { get;  }

        /// <summary>
        /// 记账开始时间
        /// </summary>
        DateTime StartTime { get;  }

        /// <summary>
        /// 记账结束时间
        /// </summary>
        DateTime EndTime { get;  }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        void CloseForm();
    }
}