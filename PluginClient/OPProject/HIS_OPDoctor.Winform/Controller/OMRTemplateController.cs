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
using HIS_OPDoctor.Winform.ViewForm;

namespace HIS_OPDoctor.Winform.Controller
{
    /// <summary>
    /// 病历模板控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmOMRTemplate")]//与系统菜单对应
    [WinformView(Name = "FrmOMRTemplate", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmOMRTemplate")]
    [WinformView(Name = "FrmORMTemplateInfo", DllName = "HIS_OPDoctor.Winform.dll", ViewTypeName = "HIS_OPDoctor.Winform.ViewForm.FrmORMTemplateInfo")]
    public class OMRTemplateController : WcfClientController
    {
        /// <summary>
        /// 保存模板标志，默认为2取消保存
        /// </summary>
        int isSucess = 2;

        /// <summary>
        /// 当前的模板实体
        /// </summary>
        OPD_OMRTmpDetail currentTmpDetail = new OPD_OMRTmpDetail();

        /// <summary>
        /// 模板信息界面接口
        /// </summary>
        IFrmORMTemplateInfo iFrmORMTemplateInfo;

        /// <summary>
        /// 模板界面接口
        /// </summary>
        IFrmOMRTemplate iFrmOMRTemplate;

        /// <summary>
        /// 初始化菜单
        /// </summary>
        public override void Init()
        {
            iFrmOMRTemplate = (IFrmOMRTemplate)iBaseView["FrmOMRTemplate"];
            iFrmORMTemplateInfo = (IFrmORMTemplateInfo)iBaseView["FrmORMTemplateInfo"];
        }

        /// <summary>
        /// 获取处方模板头信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <returns>病历模板头列表</returns>
        [WinformMethod]
        public List<OPD_OMRTmpHead> GetOMRTemplate(int intLevel)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(intLevel);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "GetOMRTemplate", requestAction);
            List<OPD_OMRTmpHead> tempList = retdata.GetData<List<OPD_OMRTmpHead>>(0);
            //添加全部模板节点
            OPD_OMRTmpHead head = new OPD_OMRTmpHead();
            head.ModuldName = "全部模板";
            head.ModulLevel = intLevel;
            head.MouldType = 0;
            head.OMRTmpHeadID = 0;
            head.PID = -1;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;
            tempList.Add(head);
            return tempList;
        }

        /// <summary>
        /// 弹出模板头信息窗体
        /// </summary>
        /// <param name="flag">1、新增；2、修改</param>
        /// <param name="head">病历模板头实体</param>
        [WinformMethod]
        public void PopInfoWindow(int flag, OPD_OMRTmpHead head)
        {
            //新增
            if (flag == 1) 
            {
                iFrmORMTemplateInfo.ResNode = true;
                iFrmORMTemplateInfo.ResTemp = false;
                iFrmORMTemplateInfo.TempName = string.Empty;
                iFrmORMTemplateInfo.IsNewFlag = true;
                iFrmORMTemplateInfo.Head = head;
            }
            else
            {
                if (head.MouldType == 0)
                {
                    iFrmORMTemplateInfo.ResNode = true;
                }
                else
                {
                    iFrmORMTemplateInfo.ResTemp = true;
                }

                iFrmORMTemplateInfo.TempName = head.ModuldName;
                iFrmORMTemplateInfo.IsNewFlag = false;
                iFrmORMTemplateInfo.Head = head;
            }

            (iBaseView["FrmORMTemplateInfo"] as Form).ShowDialog();
        }

        /// <summary>
        /// 保存模板头信息
        /// </summary>
        /// <param name="head">保存的模板对象</param>
        [WinformMethod]
        public void SaveTempInfo(OPD_OMRTmpHead head)
        {
            head.CreateDate = System.DateTime.Now;
            head.CreateDeptID = LoginUserInfo.DeptId;
            head.CreateEmpID = LoginUserInfo.EmpId;

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "SaveTempInfo", requestAction);
            head = retdata.GetData<OPD_OMRTmpHead>(0);
            int resFlag = retdata.GetData<int>(1);
            if (iFrmORMTemplateInfo.IsNewFlag == true)
            {
                iFrmOMRTemplate.ListHead.Add(head);
                Node newNode = new Node();
                newNode.Name = head.OMRTmpHeadID.ToString();
                newNode.Text = head.ModuldName;
                newNode.AccessibleDescription = head.MouldType.ToString();
                iFrmOMRTemplate.AddNode(newNode, iFrmOMRTemplate.UseTree);
            }
            else
            {
                OPD_OMRTmpHead tempHead = iFrmOMRTemplate.ListHead.Find((OPD_OMRTmpHead headTemp) => headTemp.OMRTmpHeadID == head.OMRTmpHeadID);
                tempHead.ModuldName = tempHead.ModuldName;
                iFrmOMRTemplate.EditNode(tempHead.ModuldName, iFrmOMRTemplate.UseTree);
            }

            if (resFlag > 0)
            {
                (iBaseView["FrmORMTemplateInfo"] as Form).Close();
            }
        }

        /// <summary>
        /// 删除模板信息
        /// </summary>
        /// <param name="head">删除的模板对象</param>
        /// <returns>1成功</returns>
        [WinformMethod]
        public int DeleteMoudelInfo(OPD_OMRTmpHead head)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(head);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "DeleteMoudelInfo", requestAction);
            return retdata.GetData<int>(0);
        }

        /// <summary>
        /// 检验名称是否重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="level">权限级别</param>
        /// <param name="pid">父节点id</param>
        /// <param name="id">模板id</param>
        /// <returns>true存在</returns>
        [WinformMethod]
        public bool CheckName(string name, int level, long pid, int id)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(name);
                request.AddData(level);
                request.AddData(pid);
                request.AddData(id);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "CheckName", requestAction);
            return retdata.GetData<bool>(0);
        }

        /// <summary>
        /// 读取病历模板头信息
        /// </summary>
        /// <param name="headId">模板头id</param>
        [WinformMethod]
        public void LoadHead(string headId)
        {
            var retdata = InvokeWcfService(
            "OPProject.Service",
            "OMRController",
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
                    iFrmOMRTemplate.StrDocName = head.Rows[0]["DoctName"].ToString();
                    iFrmOMRTemplate.StrDeptName = head.Rows[0]["DeptName"].ToString();
                    iFrmOMRTemplate.mouldType = head.Rows[0]["MouldType"].ToString();
                }
            }
        }
        
        /// <summary>
        /// 显示存为模板窗体
        /// </summary>
        /// <param name="detailModel">门诊病历信息模板</param>
        /// <returns>成功失败标识</returns>
        [WinformMethod]
        public int ShowOMRTplDialog(OPD_OMRTmpDetail detailModel)
        {
            isSucess = 2;
            currentTmpDetail = detailModel;
            FrmOMRTemplate frm = (iFrmOMRTemplate as FrmOMRTemplate);
            frm.ShowDialog();
            return isSucess;
        }

        /// <summary>
        /// 保存病历模板
        /// </summary>
        /// <param name="tempHead">模板头表模型</param>
        [WinformMethod]
        public void AsSaveTmp(OPD_OMRTmpHead tempHead)
        {
            tempHead.CreateEmpID = LoginUserInfo.EmpId;
            tempHead.CreateDeptID = LoginUserInfo.DeptId;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempHead);
                request.AddData(currentTmpDetail);
            });
            ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "OMRController", "AsSaveTmp", requestAction);
            bool bRtn = retdata.GetData<bool>(0);
            FrmOMRTemplate frm = (iFrmOMRTemplate as FrmOMRTemplate);
            frm.Close();
            if (bRtn == true)
            {
                isSucess = 1;
            }
            else
            {
                isSucess = 0;
            }
        }

        /// <summary>
        /// 设置成功标志为取消操作标志
        /// </summary>
        [WinformMethod]
        public void SetSuccessFlag()
        {
            isSucess = 2;
        }
    }
}
