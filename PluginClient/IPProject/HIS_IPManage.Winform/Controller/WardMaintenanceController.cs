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
    /// 病床、病房维护控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmWardMaintenance")]//在菜单上显示
    [WinformView(Name = "FrmWardMaintenance", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmWardMaintenance")]
    [WinformView(Name = "FrmAddWard", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAddWard")]
    [WinformView(Name = "FrmAddBed", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmAddBed")]
    [WinformView(Name = "FrmUpdateBed", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmUpdateBed")]
    public class WardMaintenanceController : WcfClientController
    {
        /// <summary>
        /// 床位管理
        /// </summary>
        IWardMaintenance wardMaintenance;

        /// <summary>
        /// 新增病房
        /// </summary>
        IAddWard addWard;

        /// <summary>
        /// 新增病床
        /// </summary>
        IAddBed addBed;

        /// <summary>
        /// 下拉列表数据集
        /// </summary>
        private DataSet ds = new DataSet();

        /// <summary>
        /// 修改病床
        /// </summary>
        IUpdateBed updateBed;

        /// <summary>
        /// 病区ID
        /// </summary>
        private bool isAddBed = false;

        /// <summary>
        /// 病房号
        /// </summary>
        private string roomNo = string.Empty;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            wardMaintenance = (IWardMaintenance)DefaultView;
            addWard = iBaseView["FrmAddWard"] as IAddWard;
            addBed = iBaseView["FrmAddBed"] as IAddBed;
            updateBed = iBaseView["FrmUpdateBed"] as IUpdateBed;
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        public override void AsynInit()
        {
            GetEmpDataSourceType(1);  // 获取医生列表
            GetEmpDataSourceType(2);  // 获取护士列表
            GetSimpleFeeItemData();   // 获取床位费用列表
        }

        /// <summary>
        /// 将数据绑定到界面控件
        /// </summary>
        public override void AsynInitCompleted()
        {
            updateBed.Bind_txtCurrDoctor(ds.Tables["DoctorDt"]);  // 医生列表
            updateBed.Bind_txtCurrNurse(ds.Tables["NurseDt"]); // 护士列表
            updateBed.Bind_FeeItemData(ds.Tables["SimpleFeeItemDt"]);// 绑定床位费弹出网格列表
            wardMaintenance.SetControlEnabled();  // 后台数据没加载完时不允许进行床位维护操作
        }

        #region "病床维护"

        /// <summary>
        /// 取得病区列表
        /// </summary>
        [WinformMethod]
        public void GetWardDept()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetWardDept");
            DataTable dt = retdata.GetData<DataTable>(0);

            if (dt != null && dt.Rows.Count > 0)
            {
                wardMaintenance.Bind_WardDeptList(dt);
            }
        }

        /// <summary>
        /// 打开新增单个床位界面
        /// </summary>
        /// <param name="isAdd">true:修改床位信息/false:新增床位</param>
        [WinformMethod]
        public void ShowUpdateBed(bool isAdd)
        {
            // 修改床位信息
            if (isAdd)
            {
                isAddBed = isAdd;
                updateBed.IPBedInfo = wardMaintenance.IPBedInfo;

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(wardMaintenance.IPBedInfo.BedID);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetBedFreeList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                updateBed.Bind_BedFreeList(dt, isAdd);
                updateBed.IsAddBed = false;
            }
            else
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(0);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetBedFreeList", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                updateBed.Bind_BedFreeList(dt, isAdd);
                updateBed.IPBedInfo = new IP_BedInfo();
                updateBed.IsAddBed = true;
            }

            ((Form)iBaseView["FrmUpdateBed"]).ShowDialog();
        }

        /// <summary>
        /// 根据病区ID查询床位列表
        /// </summary>
        /// <param name="wardID">病区ID</param>
        [WinformMethod]
        public void GetBedList(int wardID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(wardID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetBedList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            // 绑定床位列表网格数据
            wardMaintenance.Bind_BedList(dt);
        }

        /// <summary>
        /// 加载床位费用列表
        /// </summary>
        /// <param name="bedID">床位ID</param>
        [WinformMethod]
        public void GetBedFreeList(int bedID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(bedID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetBedFreeList", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            wardMaintenance.Bind_BedFreeList(dt);
        }

        /// <summary>
        /// 停用或启用病床
        /// </summary>
        /// <param name="isStoped">0启用/1停用</param>
        /// <param name="bedId">床位ID</param>
        /// <param name="bedNo">床位号</param>
        [WinformMethod]
        public void StoppedOrEnabledBed(int isStoped, int bedId, string bedNo)
        {
            string msg = string.Empty;

            // 停用病床
            if (isStoped == 1)
            {
                msg = string.Format("确定要停用【{0}】号病床吗？", bedNo);
            }
            else
            {
                msg = string.Format("确定要启用用【{0}】号病床吗？", bedNo);
            }

            if (MessageBoxShowYesNo(msg) == DialogResult.Yes)
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(isStoped);
                    request.AddData(bedId);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "StoppedOrEnabledBed", requestAction);
                string strMsg = retdata.GetData<string>(0);

                if (!string.IsNullOrEmpty(strMsg))
                {
                    MessageBoxShowSimple(strMsg);
                }
                else
                {
                    if (isStoped == 1)
                    {
                        MessageBoxShowSimple("停用床位成功！");
                    }
                    else
                    {
                        MessageBoxShowSimple("启用床位成功！");
                    }

                    GetBedList(wardMaintenance.WardID);
                }
            }
        }

        #endregion

        #region "新增病床"

        /// <summary>
        /// 获取医生或者护士列表
        /// </summary>
        /// <param name="empType">1医生/2护士</param>
        public void GetEmpDataSourceType(int empType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(empType);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetEmpDataSourceType", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);

            if (empType == 1)
            {
                // 医生列表
                dt.TableName = "DoctorDt";
                ds.Tables.Add(dt);
            }
            else if (empType == 2)
            {
                // 护士列表
                dt.TableName = "NurseDt";
                ds.Tables.Add(dt);
            }
        }

        /// <summary>
        /// 获取床位费用列表
        /// </summary>
        public void GetSimpleFeeItemData()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "GetSimpleFeeItemData");
            DataTable dt = retdata.GetData<DataTable>(0);
            dt.TableName = "SimpleFeeItemDt";
            ds.Tables.Add(dt);
        }

        /// <summary>
        /// 保存床位以及床位费用
        /// </summary>
        /// <param name="bedFreeDT">床位费用列表</param>
        [WinformMethod]
        public void SaveBedInfo(DataTable bedFreeDT)
        {
            // 验证是否有数量为0的数据
            if (bedFreeDT != null && bedFreeDT.Rows.Count > 0)
            {
                // 去空白行
                RemoveEmpty(bedFreeDT);
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = bedFreeDT.Select("ItemAmount<=0");

                if (arrayDr.Length > 0)
                {
                    string msg = string.Empty;

                    for (int i = 0; i < arrayDr.Length; i++)
                    {
                        msg += "[" + arrayDr[i]["ItemName"] + "]、";
                    }

                    msg = msg.Substring(0, msg.Length - 1);
                    msg += "等费用项目数量小于或等于0，请输入正确数量！";
                    MessageBoxShowSimple(msg);
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                if (!isAddBed)
                {
                    updateBed.IPBedInfo.WardID = wardMaintenance.WardID; // 病区ID
                }

                request.AddData(updateBed.IPBedInfo);
                request.AddData(bedFreeDT);
                request.AddData(isAddBed);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "WardMaintenanceController", "SaveBedInfo", requestAction);
            string result = retdata.GetData<string>(0);

            if (string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple("保存床位成功！");
                // 关闭新增床位窗体
                updateBed.FormClose();
                GetBedList(wardMaintenance.WardID);
            }
            else
            {
                MessageBoxShowSimple(result);
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
