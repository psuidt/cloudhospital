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
    /// 药品类型控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDrugType")] //与系统菜单对应HIS_DrugManage.Winform.ViewForm
    [WinformView(Name = "FrmDrugType", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmDrugType")]
    public class DrugTypeController : WcfClientController
    {
        #region 药品类型
        /// <summary>
        /// 药品类型对象
        /// </summary>
        private IFrmDrugType frmDrugType;

        /// <summary>
        /// 初始化函数
        /// </summary>
        public override void Init()
        {
            frmDrugType = (IFrmDrugType)DefaultView;
        }

        /// <summary>
        /// 获取药品类型
        /// </summary>
        [WinformMethod]
        public void GetDrugType()
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugType.QueryConditionP);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "GetDrugType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDrugType.LoadDrugType(dt);
        }

        /// <summary>
        /// 保存药品类型
        /// </summary>
        [WinformMethod]
        public void SaveDrugType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugType.CurrentDataP);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "SaveDrugType", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                GetDrugType();
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }
        #endregion 药品类型

        #region 药品子类型
        /// <summary>
        /// 获取药品子类型
        /// </summary>
        [WinformMethod]
        public void GetChildDrugType()
        {
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugType.QueryConditionC);
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "GetChildDrugType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            frmDrugType.LoadChildDrugType(dt);
        }

        /// <summary>
        /// 保存药品子类型
        /// </summary>
        [WinformMethod]
        public void SaveChildDrugType()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(frmDrugType.CurrentDataC);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "DrugTypeController", "SaveChildDrugType", requestAction);
            bool flag = retdata.GetData<bool>(0);
            if (flag)
            {
                GetChildDrugType();
                MessageBoxShowSimple("保存成功");
            }
            else
            {
                MessageBoxShowError("存在相同记录");
            }
        }
        #endregion
    }
}
