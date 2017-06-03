using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 库存上下限控制器
    /// </summary>
    [WCFController]
    public class SetLimitController : WcfServerController
    {
        /// <summary>
        /// 获取药品显示卡数据
        /// </summary>
        /// <returns>药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugShowCardInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            DataTable dtRtn = null;
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetDrugDicForInStoreShowCard(false,0);
            }
            else
            {
                dtRtn = NewDao<IDWDao>().GetDrugDicForInStoreShowCard(false,0);
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreRoomData()
        {
            int menuTypeFlag = 0;
            string belongSys = requestData.GetData<string>(0);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                menuTypeFlag = 1;//药库
            }
            else
            {
                menuTypeFlag = 0;//药房
            }

            DataTable dt = NewObject<RelateDeptMgr>().GetStoreRoomData(menuTypeFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 查询库存
        /// </summary>
        /// <returns>库存数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadStoreLimitData()
        {
            DataTable dtRtn = new DataTable();
            string belongSys = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetLoadStoreLimitData(queryCondition);//药库
            }
            else 
            {
                dtRtn = NewDao<IDSDao>().GetLoadStoreLimitData(queryCondition);//药房
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 保存上下限
        /// </summary>
        /// <returns>保存结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveStoreLimit()
        {
            string belongSys = requestData.GetData<string>(0);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                List<DW_Storage> details = requestData.GetData<List<DW_Storage>>(1);               
                try
                {
                    NewDao<IDWDao>().SaveStoreLimit(details);
                    responseData.AddData(true);
                }
                catch (Exception error)
                {                    
                    responseData.AddData(false);
                    responseData.AddData(error.Message);
                }
            }
            else
            {
                //药房
                List<DS_Storage> details = requestData.GetData<List<DS_Storage>>(1);               
                try
                {
                    NewDao<IDSDao>().SaveStoreLimit(details);
                    responseData.AddData(true);
                }
                catch (Exception error)
                {
                    responseData.AddData(false);
                    responseData.AddData(error.Message);
                }
            }

            return responseData;
        }
    }
}
