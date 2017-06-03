using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView
{
    /// <summary>
    /// 费用清单接口
    /// </summary>
    public interface IExpenseList : IBaseView
    {
        /// <summary>
        /// 加载入院科室列表
        /// </summary>
        /// <param name="deptList">入院科室列表</param>
        void LoadDeptList(DataTable deptList);

        /// <summary>
        /// 加载病人信息
        /// </summary>
        /// <param name="dtPatientInfo">病人信息</param>
        void LoadDeptPatientInfoList(DataTable dtPatientInfo);

        /// <summary>
        /// 获取病人费用清单数据
        /// </summary>
        /// <param name="dtPatientInfo">病人信息</param>
        /// <param name="dtPatientFeeInfo">病人费用信息</param>
        void LoadPatientFeeInfo(DataTable dtPatientInfo, DataTable dtPatientFeeInfo);
    }
}
