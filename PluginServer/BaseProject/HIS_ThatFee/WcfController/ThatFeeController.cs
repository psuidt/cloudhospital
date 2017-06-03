using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_PublicManage.ObjectModel;
using HIS_ThatFee.Dao;
using HIS_ThatFee.ObjectModel;

namespace HIS_ThatFee.WcfController
{
    /// <summary>
    /// 医技确费
    /// </summary>
    [WCFController]
    public class ThatFeeController : WcfServerController
    {
        #region 医技确费
        /// <summary>
        /// 获取确费网格信息
        /// </summary>
        /// <returns>确费网格信息</returns>
        [WCFMethod]
        public ServiceResponseData GetThatFee()
        {
            string deptId = requestData.GetData<string>(0);//获取信息查询条件
            int systemtype = requestData.GetData<int>(1);
            string clincDeptId = requestData.GetData<string>(2);
            bool isCheck = requestData.GetData<bool>(3);
            bool isTest = requestData.GetData<bool>(4);
            bool isTreat = requestData.GetData<bool>(5);
            bool isNotThatFee = requestData.GetData<bool>(6);
            bool isThatFee = requestData.GetData<bool>(7);
            string strNO = requestData.GetData<string>(8);
            StringBuilder strBuilder = NewObject<ThatFee>().GetQueryWhere(deptId, systemtype, clincDeptId, isCheck, isTest, isTreat, isNotThatFee, isThatFee, strNO);
            DataTable dt = NewDao<IThatFeeDao>().GetThatFee(strBuilder, systemtype);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取执行科室
        /// </summary>
        /// <returns>执行科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDept()
        {
            DataTable deptDt = NewDao<IThatFeeDao>().GetDept();
            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 获取开方科室
        /// </summary>
        /// <returns>开方科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetClincDept()
        {
            var systemType = requestData.GetData<int>(0);
            DataTable deptDt = new DataTable();
            if (systemType == 0)
            {
                deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.门诊临床科室, false);
            }
            else
            {
                deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            }

            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 获取门诊费用明细
        /// </summary>
        /// <returns>门诊费用明细</returns>
        [WCFMethod]
        public ServiceResponseData GetOPFee()
        {
            var presId = requestData.GetData<int>(0);
            DataTable dtOPFee = NewDao<IThatFeeDao>().GetOPFee(presId);
            responseData.AddData(dtOPFee);
            return responseData;
        }

        /// <summary>
        /// 获取住院费用明细
        /// </summary>
        /// <returns>住院费用明细</returns>
        [WCFMethod]
        public ServiceResponseData GetIPFee()
        {
            var presId = requestData.GetData<int>(0);
            DataTable dtIPFee = NewDao<IThatFeeDao>().GetIPFee(presId);
            responseData.AddData(dtIPFee);
            return responseData;
        }

        /// <summary>
        /// 医技确费
        /// </summary>
        /// <returns>确费成功异常字符串</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData ThatFee()
        {
            var ids = requestData.GetData<string>(0);
            var empId = requestData.GetData<int>(1);
            var empName = requestData.GetData<string>(2);
            var systemtype = requestData.GetData<int>(3);
            SetWorkId(oleDb.WorkId);
            string result = NewObject<ThatFee>().ThatFees(ids, empId, empName, systemtype);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 取消医技确费
        /// </summary>
        /// <returns>取消确费成功失败提示字符串</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData CancelThatFee()
        {
            var ids = requestData.GetData<string>(0);
            var empId = requestData.GetData<int>(1);
            var empName = requestData.GetData<string>(2);
            var systemtype = requestData.GetData<int>(3);
            SetWorkId(oleDb.WorkId);
            string result = NewObject<ThatFee>().CancelThatFees(ids, empId, empName, systemtype);
            responseData.AddData(result);
            return responseData;
        }
        #endregion

        #region 医技确费工作量统计
        /// <summary>
        /// 获取医技确费工作量统计
        /// </summary>
        /// <returns>医技确费工作量统计数据</returns>
        [WCFMethod]
        public ServiceResponseData GetThatFeeCount()
        {
            var confirDeptID = requestData.GetData<string>(0);//获取医技科室
            var beginDate = requestData.GetData<string>(1);//获取开始时间
            var endDate = requestData.GetData<string>(2);//获取结束时间
            DataTable dt = NewObject<ThatFee>().GetThatFeeCount(confirDeptID, beginDate, endDate);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 医技明细查询
        /// <summary>
        /// 根据执行科室ID获取医技项目
        /// </summary>
        /// <returns>医技项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetExamItem()
        {
            string deptId = requestData.GetData<string>(0);//获取医技科室
            DataTable dt = NewDao<IThatFeeDao>().GetExamItem(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        ///医技确费网格信息
        /// </summary>
        /// <returns>确费网格信息</returns>
        [WCFMethod]
        public ServiceResponseData GetThatFeeDetail()
        {
            string deptId = requestData.GetData<string>(0);
            int systemtype = requestData.GetData<int>(1);
            string beginDate = requestData.GetData<string>(2);
            string endDate = requestData.GetData<string>(3);
            string itemIDs = requestData.GetData<string>(4);
            StringBuilder strBuilder = NewObject<ThatFee>().GetDetailQueryWhere(deptId, systemtype, beginDate, endDate, itemIDs);
            DataTable dt = NewDao<IThatFeeDao>().GetThatFeeDetail(strBuilder, systemtype);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
