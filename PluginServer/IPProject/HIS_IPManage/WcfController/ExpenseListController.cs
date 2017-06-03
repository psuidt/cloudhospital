using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 费用清单控制器
    /// </summary>
    [WCFController]
    public class ExpenseListController : WcfServerController
    {
        /// <summary>
        /// 取得病区列表
        /// </summary>
        /// <returns>病区列表</returns>
        [WCFMethod]
        public ServiceResponseData GetWardDept()
        {
            DataTable basicDataDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, true);
            responseData.AddData(basicDataDt);
            return responseData;
        }

        /// <summary>
        /// 获取科室病人费用信息
        /// </summary>
        /// <returns>科室病人费用信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptPatientInfoList()
        {
            string sDeptCode = requestData.GetData<string>(0);
            string sDTInBegin = requestData.GetData<string>(1);
            string sDTInEnd = requestData.GetData<string>(2);
            int iPatientState = requestData.GetData<int>(3);
            string sPatient = requestData.GetData<string>(4);
            int iJsId = requestData.GetData<int>(5);
            DataTable dt = NewDao<IExpenseListDao>().GetDeptPatientInfoList(sDeptCode, sDTInBegin, sDTInEnd, iPatientState, sPatient, iJsId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取病人费用清单数据
        /// </summary>
        /// <returns>费用清单数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientFeeInfo()
        {
            int iWorkId = requestData.GetData<int>(0);
            int iPatientId = requestData.GetData<int>(1);
            int iListType = requestData.GetData<int>(2);
            string sBDate = requestData.GetData<string>(3);
            string sEDate = requestData.GetData<string>(4);
            int iJsState = requestData.GetData<int>(5);
            int iDateType = requestData.GetData<int>(6);
            int iCostHeadId = requestData.GetData<int>(7);

            DataTable dtPatientFeeSum = NewDao<IExpenseListDao>().GetPatientFeeSumByPatientId(iWorkId,iPatientId, iCostHeadId);
            DataTable dtPatientFeeInfo = NewDao<IExpenseListDao>().GetPatientFeeInfo(iWorkId, iPatientId, iListType, sBDate, sEDate, iJsState, iDateType, iCostHeadId);
            responseData.AddData(dtPatientFeeSum);
            responseData.AddData(dtPatientFeeInfo);
            return responseData;
        }
    }
}
