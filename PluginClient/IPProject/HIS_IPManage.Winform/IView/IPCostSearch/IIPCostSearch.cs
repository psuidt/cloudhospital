using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;

namespace HIS_IPManage.Winform.IView.IPCostSearch
{
    /// <summary>
    /// 住院病人费用查询接口
    /// </summary>
    public interface IIPCostSearch : IBaseView
    {
        /// <summary>
        /// 绑定收费员列表
        /// </summary>
        /// <param name="chareEmpDt">收费员列表</param>
        void Bind_ChareEmpList(DataTable chareEmpDt);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        void Bind_DeptList(DataTable deptDt);

        /// <summary>
        /// 绑定病人类型列表
        /// </summary>
        /// <param name="patTypeDt">病人类型列表</param>
        void Bind_PatTypeList(DataTable patTypeDt);

        /// <summary>
        /// 绑定医生列表
        /// </summary>
        /// <param name="doctorDt">医生列表</param>
        void Bind_DoctorList(DataTable doctorDt);

        /// <summary>
        /// 绑定结算数据
        /// </summary>
        /// <param name="payTypeDt">支付方式列表</param>
        /// <param name="itemTypeDt">项目类型列表</param>
        void Bind_CostData(DataTable payTypeDt, DataTable itemTypeDt);

        /// <summary>
        /// 检索条件
        /// </summary>
        Dictionary<string, object> QueryDictionary { get; set; }
    }
}
