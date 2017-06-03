using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_MaterialManage.Winform.IView.BaseData;
using HIS_MaterialManage.Winform.ViewForm;

namespace HIS_MaterialManage.Winform.Controller
{
    /// <summary>
    /// 物资科室设置控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmMaterialDept")]//与系统菜单对应                       HIS_DrugManage.Winform.ViewForm
    [WinformView(Name = "FrmMaterialDept", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmMaterialDept")]
    [WinformView(Name = "FrmDeptSetType", DllName = "HIS_MaterialManage.Winform.dll", ViewTypeName = "HIS_MaterialManage.Winform.ViewForm.FrmDeptSetType")]//物资类型选择窗口
    public class MaterialDeptController : WcfClientController
    {
        /// <summary>
        /// 物资库房接口
        /// </summary>
        IFrmMaterialDept frmMaterialDept;

        /// <summary>
        /// 物资科室类型form接口
        /// </summary>
        IFrmDeptSetType frmDeptSetType;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            frmMaterialDept = (IFrmMaterialDept)iBaseView["FrmMaterialDept"];
            frmDeptSetType = (IFrmDeptSetType)iBaseView["FrmDeptSetType"];
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        [WinformMethod]
        public void LoadDeptData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "GetDeptData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialDept.BindDep(dt);
        }

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="deptId">科室编码</param>
        /// <returns>true/false</returns>
        [WinformMethod]
        public bool ExistMaterialDept(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "ExistMaterialDept", requestAction);
            bool isexist = retdata.GetData<bool>(0);
            return isexist;
        }

        /// <summary>
        /// 添加物资科室信息
        /// </summary>
        [WinformMethod]
        public void AddDrugDept()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmMaterialDept.CurrtDeptDic);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "AddDrugDept", requestAction);
        }

        /// <summary>
        /// 获取物资科室列表数据并绑定表格
        /// </summary>
        [WinformMethod]
        public void GetDeptDicData()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "GetDeptDicData");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialDept.BindDrugDeptGrid(dt);
        }

        /// <summary>
        /// 删除物资科室记录
        /// </summary>
        /// <param name="deptDicID">物资科室记录</param>
        [WinformMethod]
        public void DeleteDeptDic(int deptDicID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptDicID);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "DeleteDeptDic", requestAction);
            int retVal = retdata.GetData<int>(0);
        }

        /// <summary>
        /// 取得科室类型弹出窗体实例
        /// </summary>
        /// <returns>界面对象</returns>
        [WinformMethod]
        public object GetFrmDeptSetType()
        {
            return frmDeptSetType;
        }

        /// <summary>
        /// 启用物资科室记录
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="deptType">科室类型</param>
        [WinformMethod]
        public void Start(int deptId, int deptType)
        {
            DialogResult dialogResult;
            var dialog = iBaseView["FrmDeptSetType"] as FrmDeptSetType;
            if (null == dialog)
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

                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "Start", requestAction);
                MessageBoxShowSimple("该物资科室已经成功启用，请立刻进行初始化月结...");
            }
        }

        /// <summary>
        /// 取得物资科室单据
        /// </summary>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void GetDrugDeptBill(int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "GetDrugDeptBill", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmMaterialDept.BindDrugDeptBillGrid(dt);
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        [WinformMethod]
        public void GetTypeDic()
        {
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "GetTypeDic");
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDeptSetType.BindGrid(dt);
        }

        /// <summary>
        /// 停用物资科室
        /// </summary>
        /// <param name="deptDicID">物资科室id</param>
        /// <param name="deptId">科室Id</param>
        [WinformMethod]
        public void StopUseDrugDept(int deptDicID, int deptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(deptDicID);
                request.AddData(deptId);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "MaterialDeptController", "StopUseDrugDept", requestAction);
        }
    }
}