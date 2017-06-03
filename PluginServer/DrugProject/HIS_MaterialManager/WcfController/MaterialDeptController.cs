using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_PublicManage.ObjectModel;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资库房维护控制器
    /// </summary>
    [WCFController]
    public  class MaterialDeptController : WcfServerController
    {
        /// <summary>
        /// 获取科室数据
        /// </summary>
        /// <returns>科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptData()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.全部科室, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 是否存在物资科室
        /// </summary>
        /// <returns>true存在</returns>
        [WCFMethod]
        public ServiceResponseData ExistMaterialDept()
        {
            int deptId = requestData.GetData<int>(0);
            bool retVal = NewObject<MaterialDeptMgr>().ExistMaterialDept(deptId);
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 获取物资科室数据
        /// </summary>
        /// <returns>物资科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptDicData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetDeptDicData();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 添加物资科室
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData AddDrugDept()
        {
            MW_DeptDic deptdic = requestData.GetData<MW_DeptDic>(0);
            this.BindDb(deptdic);
            int retVal = deptdic.save();
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 删除物资科室
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteDeptDic()
        {
            int deptDicID = requestData.GetData<int>(0);
            MW_DeptDic model = (MW_DeptDic)NewObject<MW_DeptDic>().getmodel(deptDicID);

            string where = "DeptID=" + model.DeptID;
            DataTable dtStoage = ((MW_Storage)NewObject<MW_Storage>()).gettable(where);
            if (dtStoage.Rows.Count > 0)
            {
                throw new Exception("当前科室已经发生业务，无法删除，只能停用");
            }

            int retVal = NewObject<MaterialDeptMgr>().DeleteDeptDic(deptDicID);
            responseData.AddData(retVal);
            return responseData;
        }

        /// <summary>
        /// 取得单据序列
        /// </summary>
        /// <returns>单据序列数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugDeptBill()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewObject<MaterialDeptMgr>().GetDrugDeptBill(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 启用物资科室
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData Start()
        {
            int deptId = requestData.GetData<int>(0);
            int deptType = requestData.GetData<int>(1);
            List<int> drugTypeList = requestData.GetData<List<int>>(2);
            MaterialDeptMgr drugDeptMgr = NewObject<MaterialDeptMgr>();
            //1.初始化业务单据
            drugDeptMgr.InitSerialNumber(deptId, deptType);
            //2.修改启用标志
            drugDeptMgr.UpdateStartStatus(deptId);
            bool ret = drugDeptMgr.SaveDeptType(deptId, drugTypeList);
            //3.添加物资科室参数
            int iRtn = NewDao<IMWDao>().SaveParameters(deptId);
            responseData.AddData(ret);
            responseData.AddData(iRtn);
            return responseData;
        }

        /// <summary>
        /// 停用物资科室
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData StopUseDrugDept()
        {
            int deptDicID = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            MaterialDeptMgr drugDeptMgr = NewObject<MaterialDeptMgr>();
            int val = drugDeptMgr.StopUseDrugDept(deptDicID, deptId);
            responseData.AddData(val);
            return responseData;
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典数据</returns>
        [WCFMethod]
        public ServiceResponseData GetTypeDic()
        {
            MaterialDeptMgr deptMgr = NewObject<MaterialDeptMgr>();
            DataTable dt = deptMgr.GetTypeDic();
            responseData.AddData(dt);
            return responseData;
        }
    }
}
