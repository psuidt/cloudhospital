using System.Collections.Generic;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 床位分配接口
    /// </summary>
    public interface IDeptOrderCheck : IBaseView
    {
        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitWindow();

        /// <summary>
        /// 进度
        /// </summary>
        int Progress { set; }

        /// <summary>
        /// 错误信息框
        /// </summary>
        List<string> RTxtError { set; }

        /// <summary>
        /// 需要发送的病人ID集
        /// </summary>
        List<int> iPatientList { get; set; }

        /// <summary>
        /// 发送之后刷新界面
        /// </summary>
        /// <param name="iType">0- 发送完成无错误；1-发送完成，有错误 2-发送失败</param>
        void FreshSendState(int iType);
    }
}
