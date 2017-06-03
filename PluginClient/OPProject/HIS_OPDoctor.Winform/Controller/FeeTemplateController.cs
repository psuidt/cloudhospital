using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;
using HIS_OPDoctor.Winform.IView.Template;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 费用模板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmFeeTemplate")]//与系统菜单对应
    [WinformView(Name = "FrmFeeTemplate", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmFeeTemplate")]
    [WinformView(Name = "FrmFeeTemplateInfo", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.Template.FrmFeeTemplateInfo")]
    public class FeeTemplateController : WcfClientController
    {
        /// <summary>
        /// 费用模板信息
        /// </summary>
        IFrmFeeTemplateInfo iFrmFeeTemplateInfo;

        /// <summary>
        /// 费用模板
        /// </summary>
        IFrmFeeTemplate iFrmFeeTemplate;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmFeeTemplate = (IFrmFeeTemplate)iBaseView["FrmFeeTemplate"];
            iFrmFeeTemplateInfo = (IFrmFeeTemplateInfo)iBaseView["FrmFeeTemplateInfo"];
        }

        /// <summary>
        /// 获取处方模板头信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="presType">模板类型</param>
        /// <returns>处方模板头信息</returns>
        [WinformMethod]
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(presType);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "FeeTemplateController", "GetPresTemplate", requestAction);
            List<OPD_PresMouldHead> tempList = retdata.GetData<List<OPD_PresMouldHead>>(0);

            OPD_PresMouldHead head = new OPD_PresMouldHead();

            head.ModuldName = "全部模板";
            head.ModulLevel = intLevel;
            head.MouldType = 0;
            head.PresMouldHeadID = 0;
            head.PID = 99999;
            head.PresType = presType;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;

            tempList.Add(head);

            return tempList;
        }

        /// <summary>
        /// 弹出模板头信息窗体
        /// </summary>
        /// <param name="flag">1、新增；2、修改</param>
        /// <param name="head">处方模板头实体</param>
        [WinformMethod]
        public void PopInfoWindow(int flag, OPD_PresMouldHead head)
        {
            //新增
            if (flag == 1)
            {
                iFrmFeeTemplateInfo.ResNode = true;
                iFrmFeeTemplateInfo.ResTemp = false;
                iFrmFeeTemplateInfo.TempName = string.Empty;
                iFrmFeeTemplateInfo.IsNewFlag = true;
                iFrmFeeTemplateInfo.Head = head;
            }
            else
            {
                if (head.MouldType == 0)
                {
                    iFrmFeeTemplateInfo.ResNode = true;
                }
                else
                {
                    iFrmFeeTemplateInfo.ResTemp = true;
                }

                iFrmFeeTemplateInfo.TempName = head.ModuldName;
                iFrmFeeTemplateInfo.IsNewFlag = false;
                iFrmFeeTemplateInfo.Head = head;
            }

            (iBaseView["FrmFeeTemplateInfo"] as Form).ShowDialog();
        }

        /// <summary>
        /// 保存模板头信息
        /// </summary>
        /// <param name="head">保存的模板对象</param>
        [WinformMethod]
        public void SaveTempInfo(OPD_PresMouldHead head)
        {
            head.CreateDate = System.DateTime.Now;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "FeeTemplateController", "SaveTempInfo", requestAction);
            head = retdata.GetData<OPD_PresMouldHead>(0);
            int resFlag = retdata.GetData<int>(1);
            if (iFrmFeeTemplateInfo.IsNewFlag == true)
            {
                iFrmFeeTemplate.ListHead.Add(head);
                Node newNode = new Node();
                newNode.Name = head.PresMouldHeadID.ToString();
                newNode.Text = head.ModuldName;
                newNode.AccessibleDescription = head.MouldType.ToString();
                iFrmFeeTemplate.AddNode(newNode, iFrmFeeTemplate.UseTree);
            }
            else
            {
                OPD_PresMouldHead tempHead = iFrmFeeTemplate.ListHead.Find((OPD_PresMouldHead headTemp) => headTemp.PresMouldHeadID == head.PresMouldHeadID);
                tempHead.ModuldName = tempHead.ModuldName;
                iFrmFeeTemplate.EditNode(tempHead.ModuldName, iFrmFeeTemplate.UseTree);
            }

            if (resFlag > 0)
            {
                (iBaseView["FrmFeeTemplateInfo"] as Form).Close();
            }
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <param name="head">删除的模板对象</param>
        /// <returns>1删除成功</returns>
        [WinformMethod]
        public int DeleteMoudelInfo(OPD_PresMouldHead head)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "FeeTemplateController", "DeleteMoudelInfo", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父id</param>
        /// <param name="id">模板头id</param>
        /// <returns>true不存在</returns>
        [WinformMethod]
        public bool CheckName(string name, int presType, int level, long pid, int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(name);
                request.AddData(presType);
                request.AddData(level);
                request.AddData(pid);
                request.AddData(id);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "FeeTemplateController", "CheckName", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 获取用法联动showcard信息
        /// </summary>
        [WinformMethod]
        public void LoadFeeInfoCard()
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "FeeTemplateController",
            "LoadFeeInfoCard",
            (request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
            });
            var fees = retdata.GetData<System.Data.DataTable>(0);
            iFrmFeeTemplate.BindFeeInfoCard(fees);
        }

        /// <summary>
        /// 读取费用模板信息
        /// </summary>
        /// <param name="headId">模板头id</param>
        [WinformMethod]
        public void LoadFee(string headId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "FeeTemplateController",
            "LoadMouldDetail",
            (request) =>
            {
                request.AddData(Convert.ToInt32(headId));
            });
            var fees = retdata.GetData<System.Data.DataTable>(0);
            iFrmFeeTemplate.BindDgFee(fees);
        }

        /// <summary>
        /// 读取费用模板信息
        /// </summary>
        /// <param name="headId">模板头id</param>
        [WinformMethod]
        public void LoadHead(string headId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "FeeTemplateController",
            "LoadMouldHead",
            (request) =>
            {
                request.AddData(Convert.ToInt32(headId));
            });
            var head = retdata.GetData<System.Data.DataTable>(0);
            if (head != null)
            {
                if (head.Rows.Count > 0)
                {
                    iFrmFeeTemplate.StrDocName = head.Rows[0]["DoctName"].ToString();
                    iFrmFeeTemplate.StrDeptName = head.Rows[0]["DeptName"].ToString();
                    iFrmFeeTemplate.MouldType= head.Rows[0]["MouldType"].ToString();
                }
            }
        }

        /// <summary>
        /// 保存费用模板信息
        /// </summary>
        /// <param name="mouldList">模板明细列表</param>
        [WinformMethod]
        public void SaveDetail(List<OPD_PresMouldDetail> mouldList)
        {
            var retdata = InvokeWcfService(
           "OPProject.Service",
           "FeeTemplateController",
           "SaveDetail",
           (request) =>
           {
               request.AddData(mouldList);
           });
            int result = retdata.GetData<int>(0);
            MessageBoxShowSimple("保存成功");
        }

        /// <summary>
        /// 删除费用模板信息
        /// </summary>
        /// <param name="detailId">明细id</param>
        [WinformMethod]
        public void DelDetail(string detailId)
        {
            var retdata = InvokeWcfService(
           "OPProject.Service",
           "FeeTemplateController",
           "DelDetail",
           (request) =>
           {
               request.AddData(detailId);
           });
            int result = retdata.GetData<int>(0);
            MessageBoxShowSimple("删除成功");
        }
    }
}
