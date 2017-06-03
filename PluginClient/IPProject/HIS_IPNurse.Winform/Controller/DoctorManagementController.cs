using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.Controller
{
    /// <summary>
    /// 护士站医嘱管理控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDoctorManagement")]//在菜单上显示
    [WinformView(Name = "FrmDoctorManagement", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmDoctorManagement")]
    public class DoctorManagementController : WcfClientController
    {
        /// <summary>
        /// 护士站医嘱管理接口
        /// </summary>
        IDoctorManagement mIDoctorManagement;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIDoctorManagement = (IDoctorManagement)DefaultView;
            //m_IDoctorManagement.ExecDeptDoctorID = LoginUserInfo.DeptId;
            //m_IDoctorManagement.ExecDeptName = LoginUserInfo.DeptName;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "GetDeptList");
            DataTable deptDt = retdata.GetData<DataTable>(0);
            mIDoctorManagement.bind_DeptList(deptDt, LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取未转抄医嘱列表以及病人列表
        /// </summary>
        [WinformMethod]
        public void GetDocList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDoctorManagement.DeptID);
                request.AddData(mIDoctorManagement.OrderCategory);
                request.AddData(mIDoctorManagement.OrderStatus);
                request.AddData(mIDoctorManagement.AstFlag);
                request.AddData(mIDoctorManagement.IsTrans);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "GetPatNotCopiedDocList", requestAction);
            // 病人列表
            DataTable patListDt = retdata.GetData<DataTable>(0);
            // 未转抄医嘱列表
            DataTable docListDt = retdata.GetData<DataTable>(1);
            // 数据绑定
            mIDoctorManagement.bind_PatList(patListDt);
            mIDoctorManagement.bind_DocList(docListDt);
        }

        /// <summary>
        /// 获取弹出网格费用项目列表
        /// </summary>
        //[WinformMethod]
        //public void GetDocFeeItemList()
        //{
        //    ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "GetDocFeeList");
        //    DataTable FeeItemDt = retdata.GetData<DataTable>(0);
        //    m_IDoctorManagement.bind_SimpleFeeItemData(FeeItemDt);
        //}

        /// <summary>
        /// 获取医嘱关联费用列表
        /// </summary>
        //[WinformMethod]
        //public void GetPatDocRelationFeeList()
        //{
        //    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
        //    {
        //        request.AddData(m_IDoctorManagement.PatListID);
        //        request.AddData(m_IDoctorManagement.GroupID);
        //    });
        //    ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "GetPatDocRelationFeeList", requestAction);
        //    DataTable PatDocFeeList = retdata.GetData<DataTable>(0);
        //    m_IDoctorManagement.bind_PatDocRelationFeeList(PatDocFeeList);
        //}

        /// <summary>
        /// 删除医嘱关联费用数据
        /// </summary>
        /// <param name="DelDt">医嘱关联费用数据</param>
        //[WinformMethod]
        //public void DelFeeItemData(int GenerateID)
        //{
        //    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
        //    {
        //        request.AddData(GenerateID);
        //    });
        //    ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "DelFeeItemData", requestAction);
        //    //RemoveEmpty(FeeList);
        //    //DataRow[] Deldr = FeeList.Select("CheckFlg=1");
        //    //if (Deldr.Length <= 0)
        //    //{
        //    //    MessageBoxShowSimple("请选择需要删除的费用数据！");
        //    //    return;
        //    //}
        //    //DataRow[] dr = FeeList.Select("UpdFlg=1");
        //    //bool Result = false;
        //    //if (dr.Length > 0)
        //    //{
        //    //    if (MessageBoxShowYesNo("您有未保存的费用数据，是否需要数据保存后执行删除？") == DialogResult.Yes)
        //    //    {
        //    //        Result = true;
        //    //        DataRow[] arrayDr = FeeList.Select("Amount<=0");
        //    //        if (arrayDr.Length > 0)
        //    //        {
        //    //            string msg = string.Empty;
        //    //            for (int i = 0; i < arrayDr.Length; i++)
        //    //            {
        //    //                msg += "[" + arrayDr[i]["ItemName"] + "]、";
        //    //            }
        //    //            msg = msg.Substring(0, msg.Length - 1);
        //    //            msg += "等项目数量小于或等于0，请输入正确数量！";
        //    //            MessageBoxShowSimple(msg);
        //    //            return;
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    if (MessageBoxShowYesNo("确定删除选中的医嘱费用吗？") != DialogResult.Yes)
        //    //    {
        //    //        return;
        //    //    }
        //    //}
        //    //Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
        //    //{
        //    //    //request.AddData(DelDt);
        //    //    request.AddData(FeeList);
        //    //    request.AddData(Result);
        //    //    request.AddData(m_IDoctorManagement.PatListID);
        //    //    request.AddData(m_IDoctorManagement.GroupID);
        //    //    request.AddData(LoginUserInfo.EmpId);
        //    //});
        //    //ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "DelFeeItemData", requestAction);
        //    //bool Reault = retdata.GetData<bool>(0);
        //    //if (Reault)
        //    //{
        //    //    MessageBoxShowSimple("医嘱费用删除成功！");
        //    //    GetPatDocRelationFeeList();
        //    //}
        //}

        /// <summary>
        /// 保存医嘱关联费用数据
        /// </summary>
        /// <param name="FeeDt">医嘱关联费用数据</param>
        //[WinformMethod]
        //public void SaveFeeItemData(DataTable FeeDt)
        //{
        //    // 去空白行
        //    RemoveEmpty(FeeDt);
        //    DataRow[] dr = FeeDt.Select("UpdFlg=1");
        //    if (dr.Length <= 0)
        //    {
        //        MessageBoxShowSimple("没有需要保存的费用数据！");
        //        return;
        //    }

        //    DataRow[] arrayDr = FeeDt.Select("Amount<=0");
        //    if (arrayDr.Length > 0)
        //    {
        //        string msg = string.Empty;
        //        for (int i = 0; i < arrayDr.Length; i++)
        //        {
        //            msg += "[" + arrayDr[i]["ItemName"] + "]、";
        //        }
        //        msg = msg.Substring(0, msg.Length - 1);
        //        msg += "等项目数量小于或等于0，请输入正确数量！";
        //        MessageBoxShowSimple(msg);
        //        return;
        //    }

        //    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
        //    {
        //        request.AddData(FeeDt);
        //        request.AddData(m_IDoctorManagement.PatListID);
        //        request.AddData(m_IDoctorManagement.GroupID);
        //        request.AddData(LoginUserInfo.EmpId);
        //    });
        //    ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "SaveFeeItemData", requestAction);
        //    bool Result = retdata.GetData<bool>(0);
        //    if (Result)
        //    {
        //        MessageBoxShowSimple("医嘱费用保存成功！");
        //        GetPatDocRelationFeeList();
        //    }
        //}

        /// <summary>
        /// 去除DataTable中的完全空白行
        /// </summary>
        /// <param name="dt"></param>
        //private void RemoveEmpty(DataTable dt)
        //{
        //    List<DataRow> removelist = new List<DataRow>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        bool IsNull = true;
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            if (j == 0 || j == 2)
        //            {
        //                continue;
        //            }
        //            if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
        //            {
        //                IsNull = false;
        //            }
        //        }
        //        if (IsNull)
        //        {
        //            removelist.Add(dt.Rows[i]);
        //        }
        //    }
        //    for (int i = 0; i < removelist.Count; i++)
        //    {
        //        dt.Rows.Remove(removelist[i]);
        //    }
        //    dt.AcceptChanges();
        //}

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID集合</param>
        [WinformMethod]
        public void CopiedDoctocList(List<string> arrayOrderID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(arrayOrderID);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "CopiedDoctocList", requestAction);
            List<string> arrayMsg = retdata.GetData<List<string>>(0);
            if (arrayMsg.Count > 0)
            {
                if (arrayOrderID.Count == arrayMsg.Count)
                {
                    string msg = string.Join("、", arrayMsg.ToArray()) + "等皮试医嘱没有结果，或者其结果为阳性,无法完成转抄！";
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    string msg = "医嘱转抄成功,其中" + string.Join("、", arrayMsg.ToArray()) + "等皮试医嘱没有结果，或者其结果为阳性,无法完成转抄！";
                    MessageBoxShowSimple(msg);
                }
            }
            else
            {
                MessageBoxShowSimple("医嘱转抄成功！");
            }

            GetDocList();
        }

        /// <summary>
        /// 医嘱转抄并发送
        /// </summary>
        /// <param name="arrayOrderID">医嘱ID集合</param>
        /// <param name="iArrayGroupId">组号ID集合</param>
        [WinformMethod]
        public void CopiedandSendDoctocList(List<string> arrayOrderID, List<int> iArrayGroupId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(arrayOrderID);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "CopiedDoctocList", requestAction);
            List<string> arrayMsg = retdata.GetData<List<string>>(0);

            if (arrayOrderID.Count == arrayMsg.Count)
            {
                string msg = string.Join("、", arrayMsg.ToArray()) + "等皮试医嘱没有结果，或者其结果为阳性,无法完成转抄！";
                MessageBoxShowSimple(msg);
            }
            else
            {
                if (arrayMsg.Count > 0)
                {
                    string msg = "医嘱转抄成功,其中" + string.Join("、", arrayMsg.ToArray()) + "等皮试医嘱没有结果，或者其结果为阳性,无法完成转抄！";
                    MessageBoxShowSimple(msg);
                }
                else
                {
                    MessageBoxShowSimple("医嘱转抄成功！ 正在发送！");
                }

                Action<ClientRequestData> requestAction1 = ((ClientRequestData request) =>
                {
                    request.AddData(iArrayGroupId);
                    request.AddData(DateTime.Now);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata1 = InvokeWcfService("IPProject.Service", "OrderCheckController", "SendOrderCheckList", requestAction1);
                bool b = retdata1.GetData<bool>(0);
                if (b)
                {
                    string msg = string.Empty;
                    List<IP_OrderCheckError> sendResultList = retdata1.GetData<List<IP_OrderCheckError>>(1);
                    foreach (IP_OrderCheckError ipOrderCheckError in sendResultList)
                    {
                        msg += ipOrderCheckError.ErrorMessage + "\n";
                    }

                    if (!msg.Equals(string.Empty))
                    {
                        MessageBoxShowSimple(msg);
                    }
                }
                else
                {
                    string sSendResult = retdata1.GetData<string>(1);
                    MessageBoxShowError(sSendResult);
                }
            }

            GetDocList();
        }

        /// <summary>
        /// 取消转抄
        /// </summary>
        /// <param name="arrayOrderID">待转抄医嘱ID集合</param>
        [WinformMethod]
        public void CancelTransDocOrder(List<string> arrayOrderID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(arrayOrderID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "CancelTransDocOrder", requestAction);

            int result = retdata.GetData<int>(0);
            if (result == 0)
            {
                MessageBoxShowSimple("医嘱取消转抄成功！");
            }
            else if (result == -1)
            {
                DataTable sendDt = retdata.GetData<DataTable>(1);
                if (sendDt.Rows.Count == arrayOrderID.Count)
                {
                    MessageBoxShowSimple("已发送医嘱无法执行取消转抄操作，转抄失败！");
                }
                else
                {
                    StringBuilder msgText = new StringBuilder();

                    for (int i = 0; i < sendDt.Rows.Count; i++)
                    {
                        msgText.Append(sendDt.Rows[i]["ItemName"].ToString());
                        if (i != sendDt.Rows.Count - 1)
                        {
                            msgText.Append("、");
                        }
                    }
                    MessageBoxShowSimple(string.Format("取消转抄成功，其中{0}等医嘱已发送，无法执行取消转抄！", msgText.ToString()));
                }
            }

            GetDocList();
        }

        #region 皮试界面
        /// <summary>
        /// 查询皮试数据
        /// </summary>
        /// <param name="iDeptID">科室ID</param>
        /// <param name="bIsCheckeed">是否已标注</param>
        /// <param name="sBDate">开始时间</param>
        /// <param name="sEDate">结束时间</param>
        [WinformMethod]
        public void QuerySkinTestData(int iDeptID, bool bIsCheckeed, string sBDate, string sEDate)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDeptID);
                request.AddData(bIsCheckeed);
                request.AddData(sBDate);
                request.AddData(sEDate);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "QuerySkinTestData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            mIDoctorManagement.LoadSkinTestInfo(dt);
        }

        /// <summary>
        /// 标记皮试结果
        /// </summary>
        /// <param name="iOrderID">医嘱Id</param>
        /// <param name="bIsPassed">皮试结果</param>
        [WinformMethod]
        public void CheckSkinTest(int iOrderID, bool bIsPassed)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iOrderID);
                request.AddData(bIsPassed);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorManagementController", "CheckSkinTest", requestAction);
            bool b = retdata.GetData<bool>(0);
            if (b)
            {
                MessageBoxShowSimple("皮试标注成功！");
                mIDoctorManagement.QuerySkinTestInfo();
            }
            else
            {
                MessageBoxShowError("皮试标注失败！");
            }
        }
        #endregion

        #region "共用方法"

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }

        #endregion
    }
}
