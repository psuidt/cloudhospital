using System.Collections.Generic;
using System.Data;
using HIS_Entity.IPManage;

namespace HIS_IPNurse.Winform.IView
{
    /// <summary>
    /// 医嘱发送接口
    /// </summary>
    public interface IOrderCheck
    {
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptDt">科室列表</param>
        /// <param name="deptID">默认科室ID</param>
        void bind_DeptList(DataTable deptDt, int deptID);

        /// <summary>
        /// 绑定可发送人员
        /// </summary>
        /// <param name="patListDt">病人列表</param>
        void bind_PatList(DataTable patListDt);

        /// <summary>
        /// 绑定医嘱列表
        /// </summary>
        /// <param name="docListDt">医嘱列表</param>
        void bind_OrederCheckInfo(DataTable docListDt);

        /// <summary>
        /// 绑定医嘱关联费用列表
        /// </summary>
        /// <param name="patOrderRelationFeeList">医嘱关联费用列表</param>
        void bind_PatOrderRelationFeeList(DataTable patOrderRelationFeeList);

        /// <summary>
        /// 医嘱发送返回结果
        /// </summary>
        /// <param name="sResultList">发送结果消息</param>
        void bind_OrderSendResult(List<IP_OrderCheckError> sResultList);

        /// <summary>
        /// 刷新发送数据
        /// </summary>
        void GetOrederCheckInfo();
    }
}
