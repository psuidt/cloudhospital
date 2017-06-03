using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 剂型维护控制器 
    /// </summary>
    [WinformController(DefaultViewName = "FrmDosage")]//在菜单上显示
    [WinformView(Name = "FrmDosage", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDosage")]//控制器关联的界面
    public class DosageController : WcfClientController
    {
        /// <summary>
        /// 剂型维护对象
        /// </summary>
        IFrmDosageManage frmDosage;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmDosage = (IFrmDosageManage)iBaseView["FrmDosage"];
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        [WinformMethod]
        public void GetDosageData()
        {
            //wcf服务调用
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDosage.QueryCondition);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DosageController", "GetDosageData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDosage.LoadData(dt);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        [WinformMethod]
        public void GetDosageDataCount()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(new Dictionary<string, string>());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DosageController", "GetDosageData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 保存药剂类型
        /// </summary>
        [WinformMethod]
        public void SaveDosageData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDosage.CurrentData);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DosageController", "SaveDosagData", requestAction);
            var flag = retdata.GetData<bool>(0);
            if (flag)
            {
                GetDosageData();
                MessageBoxShowSimple("药剂类型保存成功");
            }
            else
            {
                MessageBoxShowError("已经存在同名记录");
            }
        }

        /// <summary>
        /// 获取药剂类型
        /// </summary>
        [WinformMethod]
        public void GetDrugType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(new Dictionary<string, string>());
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "GetDrugType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDosage.BindComBox(dt);
            frmDosage.BindComboBoxQuery(dt);
        }

        /// <summary>
        /// 删除剂型
        /// </summary>
        [WinformMethod]
        public void DeleteDosage()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDosage.CurrentData);
            });
            InvokeWcfService("DrugProject.Service", "DosageController", "DeleteDosage", requestAction);
            GetDosageData();
            MessageBoxShowSimple("药剂类型删除成功");
        }
    }
}
