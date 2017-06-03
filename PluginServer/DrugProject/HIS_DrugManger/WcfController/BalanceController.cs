using System;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Account;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 财务管理控制器（月结）
    /// </summary>
    [WCFController]
    public class BalanceController : WcfServerController
    {
        /// <summary>
        /// 获取药品月结对象
        /// </summary>
        /// <param name="opType">类型</param>
        /// <returns>药品月结对象</returns>
        private DrugBalance GetBalance(string opType)
        {
            switch (opType)
            {
                case DGConstant.OP_DW_MONTHACCOUNT:
                    return NewObject<DWBalance>();
                case DGConstant.OP_DS_MONTHACCOUNT:
                    return NewObject<DSBalance>();
                default:
                    return NewObject<DWBalance>();
            }
        }

        /// <summary>
        /// 获取单前月结日期
        /// </summary>
        /// <returns>月结日期</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountDay()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            DrugBalance balance = GetBalance(opType);
            int day = balance.GetAccountDay(deptId);
            responseData.AddData(day);
            return responseData;
        }

        /// <summary>
        /// 查询科室的月结记录
        /// </summary>
        /// <returns>科室月结记录</returns>
        [WCFMethod]
        public ServiceResponseData GetMonthBalaceByDept()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            DrugBalance balance = GetBalance(opType);
            responseData.AddData(balance.GetMonthBalaceByDept(deptId));
            return responseData;
        }

        /// <summary>
        /// 月结日期设置
        /// </summary>
        /// <returns>设置日期</returns>
        [WCFMethod]
        public ServiceResponseData SetAccountDay()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int accountDay = requestData.GetData<int>(2);
            DrugBalance balance = GetBalance(opType);
            balance.SetAccountDay(accountDay, deptId);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 月结操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData MonthAccount()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int userId = requestData.GetData<int>(2);

            //科室 是 1药库 或 0药房
            int deptType = opType == DGConstant.OP_DW_MONTHACCOUNT ? 1 : 0;
            DrugBalance balance = GetBalance(opType);
            DGBillResult rtn = new DGBillResult();
            if (balance.IsMonthAccount(deptId))
            {
                rtn.Result = 1;
                rtn.ErrMsg = "本月已经月结过，不能再次月结";
                responseData.AddData(rtn);
                return responseData;
            }

            var depdic = NewObject<DrugDeptMgr>().GetDepdic(deptId, LoginUserInfo.WorkId);
            if (depdic == null)
            {
                rtn.Result = 1;
                rtn.ErrMsg = "本科室不是药剂科室，不能进行相关操作";
                responseData.AddData(rtn);
                return responseData;
            }

            if (deptType == 1&&depdic.DeptType!=1)
            {
                rtn.Result = 1;
                rtn.ErrMsg = "您所在科室没有单前科室的操作权限";
                responseData.AddData(rtn);
                return responseData;
            }

            if (deptType == 0 && depdic.DeptType != 0)
            {
                rtn.Result = 1;
                rtn.ErrMsg = "您所在科室没有单前科室的操作权限";
                responseData.AddData(rtn);
                return responseData;
            }

            if (!NewObject<DrugDeptMgr>().IsDeptChecked(deptId, LoginUserInfo.WorkId))
            {
                rtn.Result = 1;
                rtn.ErrMsg = "当前科室处于盘点状态或者没有设置科室的盘点状态 不能处理业务操作";
                responseData.AddData(rtn);
                return responseData;
            }

            try
            {
                NewDao<IDGDao>().SetCheckStatus(deptId, 1, deptType);//设置盘点状态
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
                NewDao<IDGDao>().SetCheckStatus(deptId, 0, deptType); //恢复盘点状态
            }

            return responseData;
        }

        /// <summary>
        /// 对账操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SystemCheckAccount()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int userId = requestData.GetData<int>(2);

            //科室 是 1药库 或 0药房
            int deptType = opType == DGConstant.OP_DW_MONTHACCOUNT ? 1 : 0;
            DrugBalance balance = GetBalance(opType);
            DGBillResult rtn = new DGBillResult();
            oleDb.BeginTransaction();
            try
            {
                DgSpResult result = balance.SystemCheckAccount(deptId, LoginUserInfo.WorkId);
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
