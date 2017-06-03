using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_DrugManage.Winform.ViewForm;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 药剂科室维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDrugDept")]//在菜单上显示
    [WinformView(Name = "FrmDrugDept", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDrugDept")]//药剂科室设置
    [WinformView(Name = "FrmDeptSetType", DllName = "HIS_DrugManage.Winform.dll", ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDeptSetType")]//药品类型选择窗口
    public class DrugDeptController: WcfClientController
    {
        /// <summary>
        /// 药剂科室form接口
        /// </summary>
        IFrmDrugDept frmAccount;

        /// <summary>
        /// 药剂科室类型form接口
        /// </summary>
        IFrmDeptSetType frmDeptSetType;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmDrugDept)iBaseView["FrmDrugDept"];
            frmDeptSetType= (IFrmDeptSetType)iBaseView["FrmDeptSetType"];
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        [WinformMethod]
        public void LoadDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "GetDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDep(dt);
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="deptId">科室编码</param>
        /// <returns>科室是否存在</returns>
        [WinformMethod]
        public bool ExistDrugDept(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "ExistDrugDept", requestAction);
            bool isexist= retdata.GetData<bool>(0);
            return isexist;
        }

        /// <summary>
        /// 添加药剂科室信息
        /// </summary>
        [WinformMethod]
        public void AddDrugDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {                
                request.AddData(frmAccount.CurrtDeptDic);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "AddDrugDept", requestAction);
        }

        /// <summary>
        /// 获取药剂科室列表数据并绑定表格
        /// </summary>
        [WinformMethod]
        public void GetDeptDicData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "GetDeptDicData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDrugDeptGrid(dt);
        }

        /// <summary>
        /// 删除药剂科室记录
        /// </summary>
        /// <param name="deptDicID">科室ID</param>
        [WinformMethod]
        public void DeleteDeptDic(int deptDicID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptDicID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "DeleteDeptDic", requestAction);
            int retVal = retdata.GetData<int>(0);
        }

        /// <summary>
        /// 取得科室类型弹出窗体实例
        /// </summary>
        /// <returns>科室类型弹出窗体实例</returns>
        [WinformMethod]
        public object GetFrmDeptSetType()
        {
            return frmDeptSetType;
        }

        /// <summary>
        /// 启用药剂科室记录
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型</param>
        [WinformMethod]
        public void Start(int deptId, int deptType)
        {
            DialogResult dialogResult;
            var dialog = iBaseView["FrmDeptSetType"] as FrmDeptSetType;
            if (dialog == null)
            {
                dialogResult = DialogResult.None;
            }

            dialogResult = dialog.ShowDialog();
            if (dialog.Result == 1)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(deptId);
                        request.AddData(deptType);
                        request.AddData(dialog.DrugTypeList);
                    });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "Start", requestAction);
                MessageBoxShowSimple("该药剂科室已经成功启用，请立刻进行初始化月结...");
            }
        }

        /// <summary>
        /// 取得药剂科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetDrugDeptBill(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "GetDrugDeptBill", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmAccount.BindDrugDeptBillGrid(dt);
        }

        /// <summary>
        /// 取得药品类型典
        /// </summary>
        [WinformMethod]
        public void GetTypeDic()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "GetTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDeptSetType.BindGrid(dt);
        }            

        /// <summary>
        /// 停用药剂科室
        /// </summary>
        /// <param name="deptDicID">药剂科室id</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void StopUseDrugDept(int deptDicID,int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptDicID);
                request.AddData(deptId);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugDeptController", "StopUseDrugDept",requestAction);          
        }
    }
}
