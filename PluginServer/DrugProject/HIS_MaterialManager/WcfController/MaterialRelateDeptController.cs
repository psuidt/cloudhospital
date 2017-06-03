using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.ObjectModel.BaseData;
using HIS_PublicManage.ObjectModel;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 往来科室维护控制器
    /// </summary>
    [WCFController]
    public class MaterialRelateDeptController : WcfServerController
    {
        /// <summary>
        /// 获取所有科室室数据
        /// </summary>
        /// <returns>所有科室室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAllDeptData()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.全部科室, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreRoomData()
        {
            int menuTypeFlag = requestData.GetData<int>(0);
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 根据科室Id获取往来科室数据
        /// </summary>
        /// <returns>往来科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetRelateDeptDataUserCon()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            var oW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(1);
            DataTable dt = NewObject<RelateDeptMgr>().GetRelateDeptData(andW, oW);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 根据科室Id获取往来科室数据
        /// </summary>
        /// <returns>往来科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetRelateDeptData()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewObject<RelateDeptMgr>().GetRelateDeptData(deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 条件获取往来科室的数据
        /// </summary>
        /// <returns>往来科室的数据</returns>
        [WCFMethod]
        public ServiceResponseData GetRelateDeptDataByCon()
        {
            var andW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(0);
            var oW = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(1);
            DataTable dt = NewObject<RelateDeptMgr>().GetRelateDeptData(andW, oW);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取全部科室树形数据
        /// </summary>
        /// <returns>全部科室树形数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadDeptTreeData()
        {
            List<BaseDeptLayer> layerlist = NewObject<BaseDeptLayer>().getlist<BaseDeptLayer>();
            List<BaseDept> deptlist = NewObject<BaseDept>().getlist<BaseDept>("DelFlag=0");

            responseData.AddData(layerlist);
            responseData.AddData(deptlist);
            return responseData;
        }

        /// <summary>
        /// 批量保存往来科室
        /// </summary>
        /// <returns>返回小于0失败</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData BatchSaveRelateDept()
        {
            DataTable dtSave = requestData.GetData<DataTable>(0);
            int empId = requestData.GetData<int>(1);
            int ret = NewObject<RelateDeptMgr>().BatchSaveRelateDept(dtSave, empId);
            responseData.AddData("true");
            return responseData;
        }

        /// <summary>
        /// 删除往来科室
        /// </summary>
        /// <returns>返回小于0 失败</returns>
        [WCFMethod]
        public ServiceResponseData DeleteRelateDept()
        {
            int drugDeptID = requestData.GetData<int>(0);
            int relationDeptID = requestData.GetData<int>(1);
            int ret = NewObject<RelateDeptMgr>().DeleteRelateDept(drugDeptID, relationDeptID);
            responseData.AddData(ret);
            return responseData;
        }
    }
}
