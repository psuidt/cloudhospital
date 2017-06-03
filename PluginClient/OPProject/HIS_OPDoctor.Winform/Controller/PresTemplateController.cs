using System;
using System.Collections.Generic;
using System.Data;
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
    /// 处方模板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmPresTemplate")]//与系统菜单对应
    [WinformView(Name = "FrmPresTemplate", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmPresTemplate")]
    [WinformView(Name = "FrmPesTemplateInfo", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.Template.FrmPesTemplateInfo")]
    public class PresTemplateController : WcfClientController
    {
        /// <summary>
        /// 处方条数
        /// </summary>
        private int presCount;

        /// <summary>
        /// 处方模板信息接口
        /// </summary>
        IFrmPesTemplateInfo iFrmPesTemplateInfo;

        /// <summary>
        /// 处方模板界面接口
        /// </summary>
        IFrmPresTemplate iFrmPresTemplate;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmPresTemplate = (IFrmPresTemplate)iBaseView["FrmPresTemplate"];
            iFrmPesTemplateInfo = (IFrmPesTemplateInfo)iBaseView["FrmPesTemplateInfo"];
        }

        /// <summary>
        /// 获取处方模板头信息
        /// </summary>
        /// <param name="intLevel">权限级别</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方模板头信息</returns>
        [WinformMethod]
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel,int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {                
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(presType);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresTemplateController", "GetPresTemplate", requestAction);
            List<OPD_PresMouldHead> tempList = retdata.GetData<List<OPD_PresMouldHead>>(0);
            
            OPD_PresMouldHead head = new OPD_PresMouldHead();

                head.ModuldName = "全部模板";
                head.ModulLevel = intLevel;
                head.MouldType = 0;
                head.PresMouldHeadID =0;
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
        /// <param name="head">处方头实体</param>
        [WinformMethod]
        public void PopInfoWindow(int flag, OPD_PresMouldHead head)
        {
            //新增
            if (flag == 1) 
            {
                iFrmPesTemplateInfo.ResNode = true;
                iFrmPesTemplateInfo.ResTemp = false;
                iFrmPesTemplateInfo.TempName = string.Empty;
                iFrmPesTemplateInfo.IsNewFlag = true;
                iFrmPesTemplateInfo.Head = head; 
            }
            else
            {
                if (head.MouldType == 0)
                {
                    iFrmPesTemplateInfo.ResNode = true;
                }
                else
                {
                    iFrmPesTemplateInfo.ResTemp = true;
                }

                iFrmPesTemplateInfo.TempName = head.ModuldName;
                iFrmPesTemplateInfo.IsNewFlag = false;
                iFrmPesTemplateInfo.Head = head;                
            }
             
            (iBaseView["FrmPesTemplateInfo"] as Form).ShowDialog();
        }

        /// <summary>
        /// 保存模板头信息
        /// </summary>
        /// <param name="head">处方头实体</param>
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
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresTemplateController", "SaveTempInfo", requestAction);
            head= retdata.GetData<OPD_PresMouldHead>(0);
            int resFlag = retdata.GetData<int>(1);
            if (iFrmPesTemplateInfo.IsNewFlag==true)
            {
                iFrmPresTemplate.ListHead.Add(head);
                Node newNode = new Node();
                newNode.Name = head.PresMouldHeadID.ToString();
                newNode.Text = head.ModuldName;
                newNode.AccessibleDescription = head.MouldType.ToString();
                iFrmPresTemplate.AddNode(newNode, iFrmPresTemplate.UseTree);
            }
            else
            {
                OPD_PresMouldHead tempHead = iFrmPresTemplate.ListHead.Find((OPD_PresMouldHead headTemp) => headTemp.PresMouldHeadID ==head.PresMouldHeadID);
                tempHead.ModuldName = tempHead.ModuldName;
                //iFrmPresTemplate.selectWestDrugNode.Text = tempHead.ModuldName;
                iFrmPresTemplate.EditNode(tempHead.ModuldName,iFrmPresTemplate.UseTree);
            }  

            if  (resFlag>0)
            {
                (iBaseView["FrmPesTemplateInfo"] as Form).Close();
            }
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <param name="head">处方头实体</param>
        /// <returns>1删除成功</returns>
        [WinformMethod]
        public int DeleteMoudelInfo(OPD_PresMouldHead head)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresTemplateController", "DeleteMoudelInfo", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父id</param>
        /// <param name="id">处方头id</param>
        /// <returns>true存在</returns>
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
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresTemplateController", "CheckName", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 获取药房信息
        /// </summary>
        /// <param name="presType">处方类型</param>
        /// <returns>药房信息</returns>
        [WinformMethod]
        public DataTable GetDrugStoreRoom(int presType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(presType);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetDrugStoreRoom", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        [WinformMethod]
        public void GetSystemParameter()
        {
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "PresManageController", "GetSystemParamenter");
            int regValidDays = Convert.ToInt32(retdata.GetData<string>(0));
            int presCount = Convert.ToInt32(retdata.GetData<string>(1));
           // RegValidDays = regValidDays;
            this.presCount = presCount;
        }

        /// <summary>
        /// 获取最大处方数
        /// </summary>
        /// <returns>最大处方数</returns>
        [WinformMethod]
        public int GetPresCount()
        {
            return presCount;
        }
    }
}
