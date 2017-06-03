using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品库存查询控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmStoreQuery")]//在菜单上显示
    [WinformView(Name = "FrmStoreQuery", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmStoreQuery")]//库存查询-药房
    [WinformView(Name = "FrmStoreQueryDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmStoreQuery")]//库存查询-药库
    public class QueryStoreController : WcfClientController
    {
        /// <summary>
        /// 药房库存查询对象
        /// </summary>
        IFrmStoreQuery iFrmStoreQueryDS;

        /// <summary>
        /// 药库库存查询对象
        /// </summary>
        IFrmStoreQuery iFrmStoreQueryDW;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            iFrmStoreQueryDS = (IFrmStoreQuery)iBaseView["FrmStoreQuery"];
            iFrmStoreQueryDW = (IFrmStoreQuery)iBaseView["FrmStoreQueryDW"];
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetStoreRoomData(string frmName)
        {
            if (frmName == "FrmStoreQuery")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetStoreRoomData", requestAction);
                iFrmStoreQueryDS.BindStoreRoomCombox(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetStoreRoomData", requestAction);
                iFrmStoreQueryDW.BindStoreRoomCombox(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
            }
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugTypeDic(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetDrugTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmStoreQuery")
            {
                iFrmStoreQueryDS.BindTypeCombox(dt);
            }
            else
            {
                iFrmStoreQueryDW.BindTypeCombox(dt);
            }
        }

        /// <summary>
        /// 取得药品剂型典，根据药品类型过滤
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        /// <param name="typeId">药品类型Id</param>
        [WinformMethod]
        public void GetDosageDic(string frmName, int typeId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(typeId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "GetDosageDic", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (frmName == "FrmStoreQuery")
            {
                iFrmStoreQueryDS.BindDosageShowCard(dt);
            }
            else
            {
                iFrmStoreQueryDW.BindDosageShowCard(dt);
            }
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadDrugStorage(string frmName)
        {
            if (frmName == "FrmStoreQuery")
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(iFrmStoreQueryDS.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmStoreQueryDS.BindStoreGrid(dtRtn);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(iFrmStoreQueryDW.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "LoadDrugStorage", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmStoreQueryDW.BindStoreGrid(dtRtn);
            }
        }

        /// <summary>
        /// 查询药品批次信息
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadDrugBatch(string frmName)
        {
            if (frmName == "FrmStoreQuery")
            {
                int storageID = iFrmStoreQueryDS.GetStorageID();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
               {
                   request.AddData(DGConstant.OP_DS_SYSTEM);
                   request.AddData(storageID);
               });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "LoadDrugBatch", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmStoreQueryDS.BindStoreBatchGrid(dtRtn);
            }
            else
            {
                int storageID = iFrmStoreQueryDW.GetStorageID();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(storageID);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "LoadDrugBatch", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmStoreQueryDW.BindStoreBatchGrid(dtRtn);
            }
        }

        /// <summary>
        /// 取得登录人姓名
        /// </summary>
        [WinformMethod]
        public void GetLoginName()
        {
            iFrmStoreQueryDS.GetLoginName(LoginUserInfo.EmpName, LoginUserInfo.WorkName);
        }

        /// <summary>
        /// 打印库存单
        /// </summary>
        /// <param name="deptName">部门名称</param>
        /// <param name="dgStore">库存数据</param>
        [WinformMethod]
        public void PrintDrugStore(string deptName, DataTable dgStore)
        {
            if (dgStore.Rows.Count > 0)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                myDictionary.Add("DeptName", deptName);
                myDictionary.Add("Printer", LoginUserInfo.EmpName);
                myDictionary.Add("PrintTime", DateTime.Now);
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4001, 0, myDictionary, dgStore).PrintPreview(true);
            }
        }


        /// <summary>
        /// 库存是否启用和停用
        /// </summary>
        /// <param name="DelFlag"></param>
        /// <param name="storageID"></param>
        [WinformMethod]
        public void UpdateFlag(int DelFlag,int storageID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DelFlag);
                request.AddData(storageID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "UpdateFlag", requestAction);
            bool Flag = retdata.GetData<bool>(0);
            if (Flag == true)
            {
                MessageBoxShowSimple(DelFlag == 0 ? "停用成功" : "启用成功");
            }
            else
            {
                MessageBoxShowSimple(DelFlag == 0 ? "停用失败" : "启用失败");
            }
        }

        /// <summary>
        /// 修改有效库存
        /// </summary>
        /// <param name="DrugID"></param>
        [WinformMethod]
        public void UpdateValidateStore(int DrugID, int DeptID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DrugID);
                request.AddData(DeptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "QueryStoreController", "UpdateValidateStore", requestAction);
            bool Flag = retdata.GetData<bool>(0);
            if (Flag == true)
            {
                MessageBoxShowSimple("有效库存已调成和实际库存一致！");
            }
            else
            {
                var result = retdata.GetData<string>(1);
                MessageBoxShowSimple(result);
            }
        }

    }
}
