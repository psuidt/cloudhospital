using System.Collections.Generic;
using System.Data;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 医嘱管理界面接口类
    /// </summary>
    interface IFrmOrderManager
    {
        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtpatInfo">病人信息</param>
        void BindPatInfo(DataTable dtpatInfo);

        /// <summary>
        /// 显示病人明细信息
        /// </summary>
        /// <param name="drPatInfo">病人信息</param>
        /// <param name="dtPatFee">费用信息</param>
        void ShowPatDetailInfo(DataRow drPatInfo,DataTable dtPatFee);

        /// <summary>
        /// 当前科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 当前病人Id
        /// </summary>
        int CurPatListId { get; set; }

        /// <summary>
        /// 当前科室名称
        /// </summary>
        string presDeptName { get; }
      
        /// <summary>
        /// 显示病人医嘱界面
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="isLeaveHosOrder">是否开出院医嘱</param>
        /// <param name="presdeptid">开方科室ID</param>
        /// <param name="presDeptName">开方科室名称</param>
        /// <param name="presdocId">开方医生ID</param>
        /// <param name="presDocName">开方医生名称</param>
        /// <param name="patDeptID">病人科室ID</param>
        /// <param name="hasNotFinishTrans">是否有未完成转科医嘱</param>
        void LoadPatData(int patlistid, int isLeaveHosOrder, int presdeptid, string presDeptName, int presdocId, string presDocName,int patDeptID,bool hasNotFinishTrans);

        /// <summary>
        /// 药房选择控件是否可见
        /// </summary>
        bool DrugStoreVisible { get; set; }

        /// <summary>
        /// 默认药房绑定
        /// </summary>
        /// <param name="dtDrugStore">药房数据</param>
        void DrugStoreDataBind(DataTable dtDrugStore);

        /// <summary>
        /// 获取选择的药房ID
        /// </summary>
        int GetDrugStoreID { get; }

        /// <summary>
        /// 默认药房ID
        /// </summary>
        string  DefaultDrugStore { get; set; }

        /// <summary>
        /// 病人科室ID
        /// </summary>
        int patDeptID { get; set; }

        /// <summary>
        /// 诊断录入状态 0新增 1编辑
        /// </summary>
        int DiagFlag { get; set; }

        /// <summary>
        /// 绑定模板树
        /// </summary>
        /// <param name="orderTempList">模板列表</param>
        /// <param name="tempHeadID">模板ID</param>
        void bind_FeeTempList(List<IPD_OrderModelHead> orderTempList,int tempHeadID);

        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="longOrderDt">长期医嘱列表</param>
        /// <param name="tempOrderDt">临时医嘱列表</param>
        void Bind_OrderDetails(DataTable longOrderDt, DataTable tempOrderDt);

        /// <summary>
        /// 绑定申请头表数据
        /// </summary>
        /// <param name="dt">申请头表数据</param>
        void BindApplyHead(DataTable dt);

        /// <summary>
        /// 绑定诊断页面
        /// </summary>
        /// <param name="dtDiag">诊断</param>
        /// <param name="dtClass">诊断类型</param>
        /// <param name="dtDiagCode">诊断明细</param>
        void LoadDiagInfo(DataTable dtDiag, DataTable dtClass, DataTable dtDiagCode);

        /// <summary>
        /// 控件数据异步加载
        /// </summary>
        void BindControlData();

        /// <summary>
        /// 异步加载完成事件
        /// </summary>
        void BindControlDataComplete();
    }
}
