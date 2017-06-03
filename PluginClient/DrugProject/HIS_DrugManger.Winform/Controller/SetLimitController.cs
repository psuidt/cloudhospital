using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 库存上下限控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmSetLimit")]//在菜单上显示
    [WinformView(Name = "FrmSetLimit", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmSetLimit")]//库存上下线-药房
    [WinformView(Name = "FrmSetLimitDW", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmSetLimit")]//库存上下线-药库
    public class SetLimitController: WcfClientController
    {
        /// <summary>
        /// 药房库存上下限设置窗口
        /// </summary>
        IFrmSetLimit iFrmSetLimitDS;

        /// <summary>
        /// 药库库存上下限设置窗口
        /// </summary>
        IFrmSetLimit iFrmSetLimitDW;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            iFrmSetLimitDS = (IFrmSetLimit)iBaseView["FrmSetLimit"];
            iFrmSetLimitDW = (IFrmSetLimit)iBaseView["FrmSetLimitDW"];
        }
        
        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetStoreRoomData(string frmName)
        {
            if (frmName == "FrmSetLimit")
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "GetStoreRoomData", requestAction);
                iFrmSetLimitDS.BindStoreRoomCombox(retdata.GetData<DataTable>(0),LoginUserInfo.DeptId);
            }
            else
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "GetStoreRoomData", requestAction);
                iFrmSetLimitDW.BindStoreRoomCombox(retdata.GetData<DataTable>(0),LoginUserInfo.DeptId);
            }
        }

        /// <summary>
        /// 获取药品数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetDrugShowCardInfo(string frmName)
        {
            if (frmName == "FrmSetLimit")
            {
                //药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "GetDrugShowCardInfo", requestAction);
                iFrmSetLimitDS.BindDrugSelectCard(retdata.GetData<DataTable>(0));
            }
            else
            {
                //药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "GetDrugShowCardInfo", requestAction);
                iFrmSetLimitDW.BindDrugSelectCard(retdata.GetData<DataTable>(0));
            }
        }

        /// <summary>
        /// 查询库存数据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetStoreLimitData(string frmName)
        {
            if (frmName == "FrmSetLimit")
            {
                //如果是药房
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData(iFrmSetLimitDS.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "LoadStoreLimitData", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmSetLimitDS.BindStoreLimitGrid(dtRtn);
            }
            else
            {
                //如果是药库
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData(iFrmSetLimitDW.GetQueryCondition());
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "LoadStoreLimitData", requestAction);
                DataTable dtRtn = retdata.GetData<DataTable>(0);
                iFrmSetLimitDW.BindStoreLimitGrid(dtRtn);
            }
        }

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="dtStoreLimit">库存上下限数据</param>
        [WinformMethod]
        public void SaveStoreLimit(string frmName,DataTable dtStoreLimit)
        {
            if (frmName == "FrmSetLimit")
            {
                //如果是药房
                List<DS_Storage> lstDetails = new List<DS_Storage>();
                for (int index = 0; index < dtStoreLimit.Rows.Count; index++)
                {
                    DS_Storage detail = ConvertExtend.ToObject<DS_Storage>(dtStoreLimit, index);
                    lstDetails.Add(detail);
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_SYSTEM);
                    request.AddData<List<DS_Storage>>(lstDetails);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "SaveStoreLimit", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("设置库存上下限成功");
                }
                else
                {
                    string rtnMsg = retdata.GetData<string>(1);
                    MessageBoxShowSimple("单据保存失败:" + rtnMsg);
                }
            }
            else
            {
                //如果是药库
                List<DW_Storage> lstDetails = new List<DW_Storage>();
                for (int index = 0; index < dtStoreLimit.Rows.Count; index++)
                {
                    DW_Storage detail = ConvertExtend.ToObject<DW_Storage>(dtStoreLimit, index);
                    lstDetails.Add(detail);
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DW_SYSTEM);
                    request.AddData<List<DW_Storage>>(lstDetails);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "SetLimitController", "SaveStoreLimit", requestAction);
                bool result = retdata.GetData<bool>(0);
                if (result)
                {
                    MessageBoxShowSimple("设置库存上下限成功");
                }
                else
                {
                    string rtnMsg = retdata.GetData<string>(1);
                    MessageBoxShowSimple("单据保存失败:" + rtnMsg);
                }             
            }
        }
    }
}
