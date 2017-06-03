using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药品拆零控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmResolve")]//在菜单上显示
    [WinformView(Name = "FrmResolve", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmResolve")]//控制器关联的界面
    public class ResolveController : WcfClientController
    {
        /// <summary>
        /// 药品拆零对象
        /// </summary>
        IFrmResolve frmAccount;

        /// <summary>
        /// 药品类型
        /// </summary>
        private DataTable dtDrugTypeForTb;

        /// <summary>
        /// 药品子类型
        /// </summary>
        private DataTable dtDrugCType;

        /// <summary>
        /// 药品剂型
        /// </summary>
        private DataTable dtDosage;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmResolve)iBaseView["FrmResolve"];
        }

        /// <summary>
        /// 异步加载函数
        /// </summary>
        public override void AsynInit()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ResolveController", "GetAllCenterTable");
            dtDrugTypeForTb = retdata.GetData<DataTable>(0);

            //药品子类型
            dtDrugCType = retdata.GetData<DataTable>(1);

            dtDosage = retdata.GetData<DataTable>(2);
        }

        /// <summary>
        /// 异步加载完成函数
        /// </summary>
        public override void AsynInitCompleted()
        {
            frmAccount.LoadDrugTypeForTb(dtDrugTypeForTb);
            frmAccount.LoadDrugCType(dtDrugCType);
            frmAccount.LoadDosage(dtDosage);
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        [WinformMethod]
        public void LoadDrugStorage()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmAccount.GetQueryCondition());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ResolveController", "LoadDrugStorage", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmAccount.BindStoreGrid(dtRtn);
        }

        /// <summary>
        /// 操作科室
        /// </summary>
        [WinformMethod]
        public void GetDrugDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(0);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "InStoreController", "GetDrugDeptList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDrugDept(dt);
        }

        /// <summary>
        /// 拆零操作
        /// </summary>
        /// <param name="ids">ID集</param>
        /// <returns>结果</returns>
        [WinformMethod]
        public string UpdateStorage(string ids)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "ResolveController",
             "UpdateStorage",
              (request) =>
              {
                  request.AddData(ids);
              });
            var ret = retdata.GetData<string>(0);
            return ret;
        }

        /// <summary>
        /// 取消拆零操作
        /// </summary>
        /// <param name="ids">ID集</param>
        /// <returns>是否成功</returns>
        [WinformMethod]
        public string UpdateBackStorage(string ids)
        {
            var retdata = InvokeWcfService(
            "DrugProject.Service",
            "ResolveController",
             "UpdateBackStorage",
              (request) =>
              {
                  request.AddData(ids);
              });
            var ret = retdata.GetData<string>(0);
            return ret;
        }

        /// <summary>
        /// 打印拆零单
        /// </summary>
        /// <param name="dgStore">库存数据</param>
        [WinformMethod]
        public void PrintDrugStore(DataTable dgStore)
        {
            if (dgStore.Rows.Count > 0)
            {
                Dictionary<string, object> myDictionary = new Dictionary<string, object>();
                myDictionary.Add("HospitalName", LoginUserInfo.WorkName);
                myDictionary.Add("Printer", LoginUserInfo.EmpName);
                myDictionary.Add("PrintTime", DateTime.Now);
                EfwControls.CustomControl.ReportTool.GetReport(LoginUserInfo.WorkId, 4014, 0, myDictionary, dgStore).PrintPreview(true);
            }
        }
    }
}
