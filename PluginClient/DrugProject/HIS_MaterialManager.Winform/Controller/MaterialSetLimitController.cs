using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.StoreMgr;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 库存上下限控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmSetLimits")]//在菜单上显示
    [WinformView(Name = "FrmSetLimits", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmSetLimits")]//
    public class MaterialSetLimitController : WcfClientController
    {
        /// <summary>
        /// 库存上下限接口
        /// </summary>
        IFrmSetLimits frmSetLimit;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmSetLimit = (IFrmSetLimits)iBaseView["FrmSetLimits"];
        }

        /// <summary>
        /// 获取库房信息
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetStoreRoomData(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSetLimitController", "GetStoreRoomData");
            frmSetLimit.BindStoreRoomCombox(retdata.GetData<DataTable>(0), LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取物资数据
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void GetMaterialShowCardInfo(string frmName)
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSetLimitController", "GetMaterialDicShowCard");
            frmSetLimit.BindMaterialSelectCard(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 查询库存数据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetStoreLimitData(string frmName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmSetLimit.GetQueryCondition());
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSetLimitController", "LoadStoreLimitData", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmSetLimit.BindStoreLimitGrid(dtRtn);
        }

        /// <summary>
        /// 保存库存上下限
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        /// <param name="dtStoreLimit">库存上下限数据</param>
        [WinformMethod]
        public void SaveStoreLimit(string frmName, DataTable dtStoreLimit)
        {
            List<MW_Storage> lstDetails = new List<MW_Storage>();
            for (int index = 0; index < dtStoreLimit.Rows.Count; index++)
            {
                MW_Storage detail = ConvertExtend.ToObject<MW_Storage>(dtStoreLimit, index);
                lstDetails.Add(detail);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData<List<MW_Storage>>(lstDetails);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialSetLimitController", "SaveStoreLimit", requestAction);
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