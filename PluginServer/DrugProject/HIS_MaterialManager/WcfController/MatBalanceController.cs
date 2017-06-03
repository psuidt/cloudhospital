using System;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.DrugManage;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.Account;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 财务管理控制器（月结）
    /// </summary>
    [WCFController]
    public class MatBalanceController : WcfServerController
    {
        /// <summary>
        /// 获取当前月结日期
        /// </summary>
        /// <returns>当前月结日期</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountDay()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            MaterialBalance balance = NewObject<MaterialBalance>();
            int day = balance.GetAccountDay(deptId);
            responseData.AddData(day);
            return responseData;
        }

        /// <summary>
        /// 查询科室的月结记录
        /// </summary>
        /// <returns>月结记录</returns>
        [WCFMethod]
        public ServiceResponseData GetMonthBalaceByDept()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            MaterialBalance balance = NewObject<MaterialBalance>();
            responseData.AddData(balance.GetMonthBalaceByDept(deptId));
            return responseData;
        }

        /// <summary>
        /// 月结日期设置
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SetAccountDay()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int accountDay = requestData.GetData<int>(2);
            MaterialBalance balance = NewObject<MaterialBalance>();
            balance.SetAccountDay(accountDay, deptId);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 月结操作
        /// </summary>
        /// <returns>错误信息</returns>
        [WCFMethod]
        public ServiceResponseData MonthAccount()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int userId = requestData.GetData<int>(2);
           
            //int deptType = opType == MWConstant.OP_MW_MONTHACCOUNT ? 1 : 0;
            MaterialBalance balance = NewObject<MaterialBalance>();
            MWBillResult rtn = new MWBillResult();
            if (balance.IsMonthAccount(deptId))
            {
                rtn.Result = 1;
                rtn.ErrMsg = "本月已经月结过，不能再次月结";
                responseData.AddData(rtn);
                return responseData;
            }

            var depdic = NewObject<MaterialDeptMgr>().GetDepdic(deptId, LoginUserInfo.WorkId);
            if (depdic == null)
            {
                rtn.Result = 1;
                rtn.ErrMsg = "本科室不是物资管理科室，不能进行相关操作";
                responseData.AddData(rtn);
                return responseData;
            }

            if (depdic.DeptType!=2)
            {
                rtn.Result = 1;
                rtn.ErrMsg = "您所在科室没有单前科室的操作权限";
                responseData.AddData(rtn);
                return responseData;
            }

            if (!NewObject<MaterialDeptMgr>().IsDeptChecked(deptId))
            {
                rtn.Result = 1;
                rtn.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                responseData.AddData(rtn);
                return responseData;
            }

            try
            {
                NewDao<IMWDao>().SetCheckStatus(deptId, 1);//设置盘点状态
                oleDb.BeginTransaction();
                rtn = balance.MonthAccount(userId, deptId, LoginUserInfo.WorkId);
                if (rtn.Result == 0)
                {
                    oleDb.CommitTransaction();
                }
                else
                {
                    oleDb.RollbackTransaction();
                }

                responseData.AddData(rtn);
            }
            catch (Exception error)
            {
                oleDb.RollbackTransaction();
                rtn.Result = 2;
                rtn.ErrMsg = error.Message;
                responseData.AddData(rtn);
            }
            finally
            {
                NewDao<IMWDao>().SetCheckStatus(deptId, 0); //恢复盘点状态
            }

            return responseData;
        }

        /// <summary>
        /// 对账操作
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SystemCheckAccount()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int userId = requestData.GetData<int>(2);
            //科室 是 1药库 或 0药房
            int deptType = opType == DGConstant.OP_DW_MONTHACCOUNT ? 1 : 0;

            MaterialBalance balance = NewObject<MaterialBalance>();
            DGBillResult rtn = new DGBillResult();
            oleDb.BeginTransaction();
            try
            {
                MWSpResult result = balance.SystemCheckAccount(deptId, LoginUserInfo.WorkId);
                oleDb.CommitTransaction();
                responseData.AddData(result);
                return responseData;
            }
            catch
            {
                oleDb.RollbackTransaction();
            }

            return null;
        }
    }
}
