using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.IPManage;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 账单模板
    /// </summary>
    [WinformController(DefaultViewName = "FrmDoctorTempManage")]//在菜单上显示
    [WinformView(Name = "FrmDoctorTempManage", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDoctorTempManage")]
    [WinformView(Name = "FrmAddDoctorTemp", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAddDoctorTemp")]
    public class DoctorTempManageController : WcfClientController
    {
        /// <summary>
        /// 模板管理接口
        /// </summary>
        IDoctorTempManage doctorTempManage;

        /// <summary>
        /// 新增账单模板接口
        /// </summary>
        IAddDoctorTemp addDoctorTemp;

        /// <summary>
        /// 是否是新增模板操作
        /// </summary>
        private bool tempIsAdd = false;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            doctorTempManage = (IDoctorTempManage)DefaultView;
            addDoctorTemp = iBaseView["FrmAddDoctorTemp"] as IAddDoctorTemp;
        }

        /// <summary>
        /// 获取账单模板
        /// </summary>
        /// <param name="tempHeadID">账单模板头ID</param>
        [WinformMethod]
        public void GetIPFeeItemTempList(int tempHeadID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "GetIPFeeItemTempList", requestAction);
            List<IP_FeeItemTemplateHead> feeTempList = retdata.GetData<List<IP_FeeItemTemplateHead>>(0);
            doctorTempManage.Bind_FeeTempList(feeTempList, tempHeadID);
        }

        /// <summary>
        /// 获取收费项目录入选项卡数据源
        /// </summary>
        [WinformMethod]
        public void GetRegItemShowCard()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "GetRegItemShowCard");
            DataTable dt = retdata.GetData<DataTable>(0);
            doctorTempManage.Bind_RegItemShowCard(dt);
        }

        /// <summary>
        /// 显示新增模板界面
        /// </summary>
        /// <param name="isAdd">是否为新增</param>
        [WinformMethod]
        public void ShowAddDoctorTemp(bool isAdd)
        {
            tempIsAdd = isAdd;
            addDoctorTemp.FeeItemTemplateHead = doctorTempManage.FeeItemTemplateHead;
            ((Form)iBaseView["FrmAddDoctorTemp"]).ShowDialog();
        }

        /// <summary>
        /// 停用模板
        /// </summary>
        /// <param name="isDel">true：停用模板/false：启用模板</param>
        [WinformMethod]
        public void StopOrOperationFeeTemp(bool isDel)
        {
            if (isDel)
            {
                if (MessageBoxShowYesNo("确定要停用模板吗？") != DialogResult.Yes)
                {
                    return;
                }
            }
            else
            {
                if (MessageBoxShowYesNo("确定要启用用模板吗？") != DialogResult.Yes)
                {
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                doctorTempManage.FeeItemTemplateHead.UpdateTime = DateTime.Now;

                if (isDel)
                {
                    // 删除模板
                    doctorTempManage.FeeItemTemplateHead.DelFlag = 1;
                }
                else
                {
                    // 启用模板
                    doctorTempManage.FeeItemTemplateHead.DelFlag = 0;
                }

                request.AddData(doctorTempManage.FeeItemTemplateHead);
                request.AddData(doctorTempManage.FeeTempDetailDt);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "SaveFeeItemTemplateHead", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                if (isDel)
                {
                    MessageBoxShowSimple("停用模板成功！");
                }
                else
                {
                    MessageBoxShowSimple("启用模板成功！");
                }

                // 重新绑定模板列表
                GetIPFeeItemTempList(0);
            }
        }

        /// <summary>
        /// 新增、修改模板
        /// </summary>
        [WinformMethod]
        public void SaveFeeItemTemplateHead()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                // 新增模板
                if (tempIsAdd)
                {
                    addDoctorTemp.FeeItemTemplateHead.CreateDeptID = LoginUserInfo.DeptId;
                    addDoctorTemp.FeeItemTemplateHead.CreateEmpID = LoginUserInfo.EmpId;
                    addDoctorTemp.FeeItemTemplateHead.CreateDate = DateTime.Now;
                    addDoctorTemp.FeeItemTemplateHead.DelFlag = 0;
                }
                else
                {
                    // 修改模板
                    addDoctorTemp.FeeItemTemplateHead.UpdateTime = DateTime.Now;
                }

                request.AddData(addDoctorTemp.FeeItemTemplateHead);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "SaveFeeItemTemplateHead", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (result)
            {
                MessageBoxShowSimple("模板保存成功！");
                // 关闭模板名维护界面
                addDoctorTemp.ColseForm();

                // 重新绑定模板列表
                if (tempIsAdd)
                {
                    int tempHeadID = retdata.GetData<int>(1);
                    GetIPFeeItemTempList(tempHeadID);
                }
                else
                {
                    GetIPFeeItemTempList(0);
                }
            }
        }

        /// <summary>
        /// 保存模板明细数据
        /// </summary>
        /// <param name="tempHeadID">账单模板头ID</param>
        /// <param name="isDel">是否已停用</param>
        [WinformMethod]
        public void SaveFeeTempDetailData(int tempHeadID, bool isDel)
        {
            if (doctorTempManage.FeeTempDetailDt != null && doctorTempManage.FeeTempDetailDt.Rows.Count > 0)
            {
                // 去空白行
                RemoveEmpty(doctorTempManage.FeeTempDetailDt);
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = doctorTempManage.FeeTempDetailDt.Select("ItemAmount<=0");

                if (arrayDr.Length > 0)
                {
                    string msg = string.Empty;

                    for (int i = 0; i < arrayDr.Length; i++)
                    {
                        msg += "[" + arrayDr[i]["ItemName"] + "]、";
                    }

                    msg = msg.Substring(0, msg.Length - 1);
                    msg += "等项目数量小于或等于0，请输入正确数量！";
                    MessageBoxShowSimple(msg);
                    doctorTempManage.SetGrdTempListState(false);
                    return;
                }
            }
            else
            {
                return;
            }

            if (!isDel)
            {
                if (doctorTempManage.FeeTempDetailDt == null || doctorTempManage.FeeTempDetailDt.Rows.Count <= 0)
                {
                    MessageBoxShowSimple("没有需要保存的模板明细数据，请确认！");
                    doctorTempManage.SetGrdTempListState(true);
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(doctorTempManage.FeeTempDetailDt);
                request.AddData(tempHeadID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "SaveFeeTempDetailData", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (result)
            {
                MessageBoxShowSimple("保存模板明细成功！");
                doctorTempManage.SetGrdTempListState(true);
            }
            else
            {
                string msg = retdata.GetData<string>(1);
                MessageBoxShowSimple(msg);
                doctorTempManage.SetGrdTempListState(false);
            }
        }

        /// <summary>
        /// 去除DataTable中的完全空白行
        /// </summary>
        /// <param name="dt">待处理DataTable</param>
        private void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool isNull = true;

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        isNull = false;
                    }
                }

                if (isNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }

            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }

            dt.AcceptChanges();
        }

        /// <summary>
        /// 查询模板明细数据
        /// </summary>
        /// <param name="tempHeadID">模板ID</param>
        [WinformMethod]
        public void GetFeeTempDetails(int tempHeadID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempHeadID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "NurseTempManageController", "GetFeeTempDetails", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            doctorTempManage.Bind_FeeTempDetails(dt);
        }

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