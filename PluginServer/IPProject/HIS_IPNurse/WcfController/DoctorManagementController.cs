using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPNurse.Dao;
using HIS_IPNurse.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.WcfController
{
    /// <summary>
    /// 护士站医嘱转抄服务端控制器
    /// </summary>
    [WCFController]
    public class DoctorManagementController : WcfServerController
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns>科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptList()
        {
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人未转抄医嘱列表
        /// </summary>
        /// <returns>病人未转抄医嘱列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatNotCopiedDocList()
        {
            int deptID = requestData.GetData<int>(0);
            string orderCategory = requestData.GetData<string>(1);
            string orderStatus = requestData.GetData<string>(2);
            string astFlag = requestData.GetData<string>(3);
            bool isTrans = requestData.GetData<bool>(4);
            DataTable patListDt = NewDao<IDoctorManagementDao>().GetDocPatList(deptID, orderCategory, orderStatus, astFlag, isTrans);
            DataTable docListDt = NewDao<IDoctorManagementDao>().GetPatNotCopiedDocList(deptID, orderCategory, orderStatus, astFlag, isTrans);
            responseData.AddData(patListDt);
            responseData.AddData(docListDt);
            return responseData;
        }

        /// <summary>
        /// 获取费用项目列表
        /// </summary>
        /// <returns>费用项目列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDocFeeList()
        {
            DataTable feeDt = NewDao<IDoctorManagementDao>().GetDocFeeItemList();
            responseData.AddData(feeDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人医嘱关联费用列表
        /// </summary>
        /// <returns>病人医嘱关联费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatDocRelationFeeList()
        {
            int patListID = requestData.GetData<int>(0);
            int groupID = requestData.GetData<int>(1);
            DataTable docFeeList = NewDao<IDoctorManagementDao>().GetPatDocRelationFeeList(patListID, groupID);
            responseData.AddData(docFeeList);
            return responseData;
        }

        /// <summary>
        /// 删除医嘱关联费用数据
        /// </summary>
        /// <returns>true删除成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelFeeItemData()
        {
            int generateID = requestData.GetData<int>(0);
            NewDao<IDoctorManagementDao>().DelFeeItemGenerate(generateID);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 保存医嘱费用数据
        /// </summary>
        /// <returns>true保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveFeeItemData()
        {
            DataTable docFeeList = requestData.GetData<DataTable>(0);
            int patListID = requestData.GetData<int>(1);
            int groupID = requestData.GetData<int>(2);
            int empId = requestData.GetData<int>(3);
            int deptID = requestData.GetData<int>(4);
            bool result = NewObject<DoctorManagement>().SaveFeeItemData(docFeeList, patListID, groupID, empId, deptID);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <returns>转抄结果（错误消息等）</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CopiedDoctocList()
        {
            List<string> arrayOrderID = requestData.GetData<List<string>>(0);
            int empId = requestData.GetData<int>(1);
            List<string> arrayMsg = new List<string>();
            NewObject<DoctorManagement>().CopiedDoctocList(arrayOrderID, empId, arrayMsg);
            responseData.AddData(arrayMsg);
            return responseData;
        }

        /// <summary>
        /// 取消转抄
        /// </summary>
        /// <returns>操作结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelTransDocOrder()
        {
            List<string> arrayOrderID = requestData.GetData<List<string>>(0);
            // 判断医嘱是否已发送
            DataTable sendDt = NewDao<IDoctorManagementDao>().IsCheckOrderSend(string.Join(",", arrayOrderID.ToArray()));

            if (sendDt.Rows.Count > 0)
            {
                responseData.AddData(-1);
                responseData.AddData(sendDt);
            }

            NewDao<IDoctorManagementDao>().CancelTransDocOrder(string.Join(",", arrayOrderID.ToArray()));
            responseData.AddData(0);
            return responseData;
        }

        /// <summary>
        /// 获取病人费用数据
        /// </summary>
        /// <returns>病人费用数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPatFeeInfo()
        {
            int patListID = requestData.GetData<int>(0);
            // 预交金总额
            DataTable sumDepositDt = NewDao<IDoctorManagementDao>().GetPatSumPay(patListID);
            // 记账金额
            DataTable patSumFeeDt = NewDao<IDoctorManagementDao>().GetPatLongOrderSumPay(patListID);
            responseData.AddData(sumDepositDt);
            responseData.AddData(patSumFeeDt);
            return responseData;
        }

        /// <summary>
        /// 获取皮试数据
        /// </summary>
        /// <returns>皮试数据</returns>
        [WCFMethod]
        public ServiceResponseData QuerySkinTestData()
        {
            int iDeptID = requestData.GetData<int>(0);
            bool bIsCheckeed = requestData.GetData<bool>(1);
            string sBDate = requestData.GetData<string>(2);
            string sEDate = requestData.GetData<string>(3);
            // 预交金总额
            DataTable dtSkinTest = NewDao<IDoctorManagementDao>().QuerySkinTestData(iDeptID, bIsCheckeed, sBDate, sEDate);

            responseData.AddData(dtSkinTest);
            return responseData;
        }

        /// <summary>
        /// 标注皮试结果
        /// </summary>
        /// <returns>true：标记成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CheckSkinTest()
        {
            int iOrderId = requestData.GetData<int>(0);
            bool bIsPassed = requestData.GetData<bool>(1);
            int iDeptId = requestData.GetData<int>(2);
            int iEmpId = requestData.GetData<int>(3);
            bool b = NewObject<DoctorManagement>().CheckSkinTest(iOrderId, bIsPassed, iDeptId, iEmpId);
            responseData.AddData(b);
            return responseData;
        }

        /// <summary>
        /// 验证医嘱是否已转抄，已转抄的医嘱不允许补录费用
        /// </summary>
        /// <returns>true：已转抄</returns>
        [WCFMethod]
        public ServiceResponseData CheckOrderStatus()
        {
            int patListID = requestData.GetData<int>(0);
            int groupID = requestData.GetData<int>(1);
            DataTable resultDt = NewDao<IDoctorManagementDao>().CheckOrderStatus(patListID, groupID);
            if (resultDt.Rows.Count <= 0)
            {
                responseData.AddData(true);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }

    }
}
