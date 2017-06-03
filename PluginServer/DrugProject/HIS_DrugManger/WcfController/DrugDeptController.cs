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
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药剂科室维护控制器
    /// </summary>
    [WCFController]
    public class DrugDeptController : WcfServerController
    {
        /// <summary>
        /// 获取药剂科室数据
        /// </summary>
        /// <returns>药剂科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptData()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.全部科室, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 是否存在药剂科室
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData ExistDrugDept()
        {
            int deptId = requestData.GetData<int>(0);
            bool retVal = NewObject<DrugDeptMgr>().ExistDrugDept(deptId);
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 获取药剂科室数据
        /// </summary>
        /// <returns>药剂科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptDicData()
        {
            DataTable dt = NewObject< DrugDeptMgr>().GetDeptDicData();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 添加药剂科室
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData AddDrugDept()
        {
            DG_DeptDic deptdic = requestData.GetData<DG_DeptDic>(0);
            this.BindDb(deptdic);
            int retVal = deptdic.save();
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 删除药剂科室
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeleteDeptDic()
        {
            int deptDicID = requestData.GetData<int>(0);
            DG_DeptDic model = (DG_DeptDic)NewObject<DG_DeptDic>().getmodel(deptDicID);
            if (model.DeptType == 0)
            {
                //药房
                string where = "DeptID=" + model.DeptID;
                DataTable dtStoage = ((DS_Storage)NewObject<DS_Storage>()).gettable(where);
                if (dtStoage.Rows.Count > 0)
                {
                    throw new Exception("当前科室已经发生业务，无法删除，只能停用");
                }
            }
            else
            {
                //药库
                string where = "DeptID=" + model.DeptID;
                DataTable dtStoage = ((DW_Storage)NewObject<DW_Storage>()).gettable(where);
                if (dtStoage.Rows.Count > 0)
                {
                    throw new Exception("当前科室已经发生业务，无法删除，只能停用");
                }
            }

            int retVal = NewObject<DrugDeptMgr>().DeleteDeptDic(deptDicID);
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 取得单据序列
        /// </summary>
        /// <returns>单据序列</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptBill()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewObject<DrugDeptMgr>().GetDrugDeptBill(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 启用药剂科室
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]      
        public ServiceResponseData Start()
        {
            int deptId =requestData.GetData<int>(0);
            int deptType= requestData.GetData<int>(1);
            List<int> drugTypeList= requestData.GetData<List<int>>(2);
            DrugDeptMgr drugDeptMgr = NewObject<DrugDeptMgr>();

            //1.初始化业务单据
            drugDeptMgr.InitSerialNumber(deptId, deptType);

            //2.修改启用标志
            drugDeptMgr.UpdateStartStatus(deptId);
            bool ret= drugDeptMgr.SaveDeptType(deptId, drugTypeList);

            //3.初始化药品参数表
            int iRtn = NewDao<IDGDao>().SaveDrugParameters(deptId);
            responseData.AddData(ret);
            responseData.AddData(iRtn);
            return responseData;
        }

        /// <summary>
        /// 停用药剂科室
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StopUseDrugDept()
        {
            int deptDicID = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            DrugDeptMgr drugDeptMgr = NewObject<DrugDeptMgr>();
            int val =drugDeptMgr.StopUseDrugDept(deptDicID, deptId);
            responseData.AddData(val);
            return responseData;
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetTypeDic()
        {
            DrugDeptMgr drugDeptMgr = NewObject<DrugDeptMgr>();
            DataTable dt = drugDeptMgr.GetTypeDic();
            responseData.AddData(dt);
            return responseData;
        }
    }
}
