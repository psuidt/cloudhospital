using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.StoreMgr;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.Controller
{
    /// <summary>
    /// 领药申请控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmApply")] //在菜单上显示
    [WinformView(Name = "FrmApply", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmApply")] //控制器关联的界面
    [WinformView(Name = "FrmApplyDetail", DllName = "HIS_DrugManage.Winform.dll",
        ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmApplyDetail")]

    [WinformView(Name = "FrmOutApply", DllName = "HIS_DrugManage.Winform.dll",
ViewTypeName = "HIS_DrugManage.Winform.ViewForm.FrmOutApply")]

    public class ApplyStoreController : WcfClientController
    {
        /// <summary>
        /// 领药对象
        /// </summary>
        IFrmApply frmAccount;

        /// <summary>
        /// 领药明细对象
        /// </summary>
        private IFrmApplyDetails frmdeApplyDetails;

        /// <summary>
        /// 领药转出库对象
        /// </summary>
        IFrmApply frmOutApply;

        /// <summary>
        /// 当前选中科室
        /// </summary>
        private int selectedDeptID = 0;

        #region 领药申请
        /// <summary>
        /// 初始化领药申请
        /// </summary>
        public override void Init()
        {
            frmAccount = (IFrmApply)iBaseView["FrmApply"];
            frmOutApply = (IFrmApply)iBaseView["FrmOutApply"];
            frmdeApplyDetails = (IFrmApplyDetails)iBaseView["FrmApplyDetail"];
        }

        /// <summary>
        /// 绑定药库科室
        /// </summary>
        [WinformMethod]
        public void GetWareHourse()
        {
            List<Tuple<string, string, SqlOperator>> andWhere = new List<Tuple<string, string, SqlOperator>>();
            andWhere.Add(Tuple.Create("DeptType", "1", SqlOperator.Equal)); //查药库类型

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(andWhere);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "GetWareHourse", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmdeApplyDetails.BindWareHourse(dtRtn);
        }

        /// <summary>
        /// 获取药剂科室列表
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void GetDrugDept(string frmName)
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
        ///  领药表头
        /// </summary>
        /// <param name="remark">备注</param>
        /// <param name="todeptId">科室ID</param>
        /// <param name="todeptName">科室名称</param>
        /// <param name="id">表头ID</param>
        /// <returns>返回</returns>
        [WinformMethod]
        public bool InitBillHead(string remark, string todeptId, string todeptName, int id)
        {
            if (LoginUserInfo.DeptId == 0)
            {
                MessageBoxShowSimple("当前用户没有选择领药科室");
                return false;
            }

            if (id == 0)
            {
                DS_ApplyHead head = new DS_ApplyHead();
                head.ApplyHeadID = id;
                head.RegEmpID = LoginUserInfo.EmpId;
                head.UpdateTime = DateTime.Now;
                head.RegEmpName = LoginUserInfo.EmpName;
                head.Remark = remark;
                head.RegTime = DateTime.Now;
                head.ApplyDeptID = selectedDeptID;
                head.ApplyDeptName = LoginUserInfo.DeptName.ToString();
                head.ToDeptID = Convert.ToInt32(todeptId);
                head.ToDeptName = todeptName;
                frmdeApplyDetails.BindInHeadInfo(head);
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(id);
                });
                ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "GetEditBillHead", requestAction);
                DS_ApplyHead storeHead = retdata.GetData<DS_ApplyHead>(0);
                if (storeHead.AuditFlag == 1)
                {
                    MessageBoxShowSimple("当前单据已经审核，不能修改");
                    return false;
                }

                storeHead.ApplyDeptID = selectedDeptID;
                frmdeApplyDetails.CurrentApplyHead = storeHead;
                frmdeApplyDetails.BindInHeadInfo(storeHead);
            }

            frmdeApplyDetails.InitControStatus(frmdeApplyDetails.CurrentApplyHead);
            return true;
        }

        /// <summary>
        /// 出库单 可以添加的药品
        /// </summary>
        /// <param name="toDeptId">科室ID</param>
        [WinformMethod]
        public void GetOutStoreDrugInfo(string toDeptId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DW_SYSTEM);
                request.AddData(Convert.ToInt32(toDeptId));
                request.AddData(DGConstant.OP_DS_APPLYPLAN); //药房申请
                request.AddData(selectedDeptID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "GetDeptOutDrug", requestAction);
            frmdeApplyDetails.BindDrugInfoCard(retdata.GetData<DataTable>(0));
        }

        /// <summary>
        /// 查询单据表头
        /// </summary>
        [WinformMethod]
        public void LoadBillHead()
        {
            var dic =
                frmAccount.GetQueryCondition();
            dic.Add("ApplyDeptID", selectedDeptID.ToString());

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DS_APPLYPLAN);
                request.AddData(dic);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmAccount.BindApplyHead(dtRtn);
        }

        /// <summary>
        /// 获取当前选中的药剂科室ID
        /// </summary>
        /// <param name="deptID">药剂科室ID</param>
        [WinformMethod]
        public void SetSelectedDept(int deptID)
        {
            selectedDeptID = deptID;
        }

        /// <summary>
        /// 查询单据明细
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void LoadBillDetails(string frmName)
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;

            switch (frmName)
            {
                case "FrmApply":
                    headInfo = frmAccount.GetCurrentHeadID();
                    if (headInfo != null)
                    {
                        requestAction = ((ClientRequestData request) =>
                        {
                            request.AddData(DGConstant.OP_DS_APPLYPLAN);
                            request.AddData(headInfo);
                        });

                        retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "LoadBillDetial", requestAction);
                        DataTable dt = retdata.GetData<DataTable>(0);
                        frmAccount.BindApplyDetail(dt);
                    }
                    else
                        frmAccount.BindApplyDetail(null);
                    break;
                case "FrmApplyDetail":
                    headInfo = new Dictionary<string, string>();
                    headInfo.Add("ApplyHeadID", frmdeApplyDetails.CurrentApplyHead.ApplyHeadID.ToString());

                    requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(DGConstant.OP_DS_APPLYPLAN);
                        request.AddData(headInfo);
                    });
                    retdata = InvokeWcfService("DrugProject.Service", "OutStoreController", "LoadBillDetails", requestAction);
                    DataTable dttail = retdata.GetData<DataTable>(0);
                    frmdeApplyDetails.CurrentDetail = dttail;
                    frmdeApplyDetails.BindApplyDetail(dttail);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="billID">单据ID</param>
        [WinformMethod]
        public void DeleteBill(int billID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(billID);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "DeleteBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("单据已经成功删除");
                LoadBillHead();
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("单据删除失败:" + rtnMsg);
            }
        }

        /// <summary>
        /// 从界面信息更新单据头
        /// </summary>
        /// <param name="frmName">窗体名称</param>
        [WinformMethod]
        public void RefreshHead(string frmName)
        {
            DS_ApplyHead editHead = frmdeApplyDetails.GetHeadInfo();
            if (frmdeApplyDetails.CurrentApplyHead != null)
            {
                frmdeApplyDetails.CurrentApplyHead.ToDeptID = editHead.ToDeptID;
                frmdeApplyDetails.CurrentApplyHead.ToDeptName = editHead.ToDeptName;
                frmdeApplyDetails.CurrentApplyHead.Remark = editHead.Remark;
                frmdeApplyDetails.CurrentApplyHead.RegTime = editHead.RegTime;
            }
        }

        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="frmName">窗体入口</param>
        [WinformMethod]
        public void SaveBill(string frmName)
        {
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            List<DS_ApplyDetail> lstDetails = new List<DS_ApplyDetail>();
            RefreshHead(frmName);
            for (int index = 0; index < frmdeApplyDetails.CurrentDetail.Rows.Count; index++)
            {
                DS_ApplyDetail detail = ConvertExtend.ToObject<DS_ApplyDetail>(frmdeApplyDetails.CurrentDetail, index);
                lstDetails.Add(detail);
            }

            requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DS_APPLYPLAN);
                request.AddData(frmdeApplyDetails.CurrentApplyHead);
                request.AddData<List<DS_ApplyDetail>>(lstDetails);
                request.AddData<List<int>>(frmdeApplyDetails.GetDeleteDetails());
            });
            retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "SaveBill", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                MessageBoxShowSimple("领药申请单已经保存成功");
                if (frmdeApplyDetails.CurrentApplyHead.ApplyHeadID == 0)
                {
                    frmdeApplyDetails.NewBillClear();
                }
                else
                {
                    frmdeApplyDetails.CloseCurrentWindow();
                }
            }
            else
            {
                string rtnMsg = retdata.GetData<string>(1);
                MessageBoxShowSimple("领药申请单保存失败:" + rtnMsg);
            }
        }
        #endregion

        #region 领药转出库
        /// <summary>
        /// 领药申请的科室ID
        /// </summary>
        private int applyDeptId = 0;

        /// <summary>
        /// 查询单据表头
        /// </summary>
        [WinformMethod]
        public void LoadApplyHead()
        {
            var dic =
                frmOutApply.GetQueryCondition();
            dic.Add("toDeptId", applyDeptId.ToString());
            dic.Add("AuditFlag", "0");

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(DGConstant.OP_DS_APPLYPLAN);
                request.AddData(dic);
            });
            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "LoadBillHead", requestAction);
            DataTable dtRtn = retdata.GetData<DataTable>(0);
            frmOutApply.BindApplyHead(dtRtn);
        }

        /// <summary>
        /// 查询单据明细
        /// </summary>
        [WinformMethod]
        public void LoadApplyDetails()
        {
            Dictionary<string, string> headInfo = null;
            Action<ClientRequestData> requestAction = null;
            ServiceResponseData retdata = null;
            headInfo = frmOutApply.GetCurrentHeadID();
            if (headInfo != null)
            {
                requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(DGConstant.OP_DS_APPLYPLAN);
                    request.AddData(headInfo);
                });
                retdata = InvokeWcfService("DrugProject.Service", "ApplyStoreController", "LoadBillDetial", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                frmOutApply.BindApplyDetail(dt);
            }
            else
            {
                frmOutApply.BindApplyDetail(null);
            }
        }

        /// <summary>
        /// 药房领药申请
        /// </summary>
        /// <param name="head">申请头表数据集</param>
        [WinformMethod]
        public void ConvertDsApply(DataRow head)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head["ApplyHeadID"].ToString());
            });

            ServiceResponseData retdata = InvokeWcfService("DrugProject.Service","OutStoreController","ConvertDwOutFromApply",requestAction);

            DataTable dt = retdata.GetData<DataTable>(0);

            InvokeController("DrugProject.UI", "OutStoreController", "BindDataTable", head, dt);
            var form = iBaseView["FrmOutApply"] as Form;
            if (form != null)
            {
                form.Close();
            }
        }

        /// <summary>
        /// 显示出库申请
        /// </summary>
        /// <param name="selectedDeptId">选择的科室ID</param>
        [WinformMethod]
        public void Show(int selectedDeptId)
        {
            this.applyDeptId = selectedDeptId;
            ((Form)iBaseView["FrmOutApply"]).ShowDialog();
        }
        #endregion
    }
}