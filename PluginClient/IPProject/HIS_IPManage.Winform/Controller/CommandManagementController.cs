using System;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPManage.Winform.IView;

namespace HIS_IPManage.Winform.Controller
{
    /// <summary>
    /// 药品统领控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmCommandManagement")]//在菜单上显示
    [WinformView(Name = "FrmCommandManagement", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmCommandManagement")]
    public class CommandManagementController : WcfClientController
    {
        /// <summary>
        /// 药品统领接口
        /// </summary>
        private ICommandManagement mICommandManagement;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mICommandManagement = (ICommandManagement)DefaultView;
        }

        /// <summary>
        /// 获取并绑定执行科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptDataSourceList()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "GetDeptDataSourceList");
            DataTable execDeptDt = retdata.GetData<DataTable>(0);

            if (execDeptDt != null)
            {
                // 绑定执行科室列表
                mICommandManagement.Bind_ExecDeptList(execDeptDt);
            }
        }

        /// <summary>
        /// 获取并绑定入院科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(false);
            });
            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            DataTable deptDt = retdata.GetData<DataTable>(0);

            // 病人列表界面
            if (deptDt != null)
            {
                mICommandManagement.Bind_DeptList(deptDt);
            }
        }

        /// <summary>
        /// 取得统领列表
        /// </summary>
        [WinformMethod]
        public void GetCommandSheetList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mICommandManagement.PatDeptID);
                request.AddData(mICommandManagement.ExecDeptID);
                request.AddData(mICommandManagement.CommandStatus);
            });

            // 取得所有药品统领数据
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "GetCommandSheetList", requestAction);
            DataTable commandList = retdata.GetData<DataTable>(0);
            mICommandManagement.IsMedicineCommand = mICommandManagement.CommandStatus;
            // 过滤出病人列表
            // Table过滤字段集
            string[] strArray = new string[3];
            strArray[0] = "PatListID";
            strArray[1] = "PatName";
            strArray[2] = "PatDeptID";

            DataTable patListDt = new DataTable();
            DataTable commandTempDt = commandList.Clone();

            if (commandList != null && commandList.Rows.Count > 0)
            {
                // 全部药品
                if (mICommandManagement.Whole)
                {
                    mICommandManagement.Bind_CommandList(commandList);
                    // 从统领数据中过滤病人列表
                    patListDt = DistinctSomeColumn(commandList, strArray);
                    // 绑定病人一览列表
                    mICommandManagement.Bind_PatList(patListDt);
                }
                else
                {
                    // 根据选中的药品类型显示统领数据
                    commandTempDt = SetCommandListByDrugsType(commandList);
                    mICommandManagement.Bind_CommandList(commandTempDt);
                    if (commandTempDt.Rows.Count > 0)
                    {
                        // 从统领数据中过滤病人列表
                        patListDt = DistinctSomeColumn(commandTempDt, strArray);
                    }
                    // 绑定病人一览列表
                    mICommandManagement.Bind_PatList(patListDt);
                }
            }
            else
            {
                mICommandManagement.Bind_PatList(patListDt);  // 绑定病人一览列表
                mICommandManagement.Bind_CommandList(commandList);  // 绑定统领数据列表
            }
        }

        /// <summary>
        /// 根据药品列表过滤药品统领数据
        /// </summary>
        /// <param name="commandList">全部统领列表</param>
        /// <returns>药品统领列表</returns>
        public DataTable SetCommandListByDrugsType(DataTable commandList)
        {
            commandList.TableName = "CommandList";
            DataView view = new DataView(commandList);
            DataTable tempDt = commandList.Clone();
            string sqlWhere = string.Empty;
            // 口服药
            if (mICommandManagement.IsOralMedicine)
            {
                sqlWhere = "IsOralMedicine = 1 ";
            }

            // 针剂
            if (mICommandManagement.IsInjection)
            {
                if (string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = " IsInjection = 1 ";
                }
                else
                {
                    sqlWhere += " OR IsInjection = 1 ";
                }
            }

            // 大输液
            if (mICommandManagement.IsLargeInfusion)
            {
                if (string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = " IsLargeInfusion = 1 ";
                }
                else
                {
                    sqlWhere += " OR IsLargeInfusion = 1 ";
                }
            }

            // 中草药
            if (mICommandManagement.IsChineseHerbalMedicine)
            {
                if (string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere = " IsChineseHerbalMedicine = 1 ";
                }
                else
                {
                    sqlWhere += " OR IsChineseHerbalMedicine = 1 ";
                }
            }

            // 没有选择任何药品类别时，默认绑定所有统领药品
            if (string.IsNullOrEmpty(sqlWhere))
            {
                return commandList;
            }

            view.RowFilter = sqlWhere;
            view.Sort = "PresDate ASC";
            tempDt.Merge(view.ToTable());
            tempDt.AcceptChanges();
            return tempDt;
        }

        /// <summary>
        /// 根据病人登记信息过滤出病人列表
        /// </summary>
        /// <param name="sourceTable">传入DataTable</param>
        /// <param name="fieldName">过滤的字段集合</param>
        /// <returns>病人列表</returns>
        private DataTable DistinctSomeColumn(DataTable sourceTable, params string[] fieldName)
        {
            return sourceTable.AsEnumerable().GroupBy(row =>
            {
                StringBuilder s = new StringBuilder();
                fieldName.ToList().ForEach(p => s.Append(row[p].ToString()));
                return s.ToString();
            }).Select(p => p.First()).CopyToDataTable<DataRow>();
        }

        /// <summary>
        /// 发送药品统领数据
        /// </summary>
        /// <param name="commandList">待发送药品统领数据</param>
        [WinformMethod]
        public void SendCommandList(DataTable commandList)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(commandList);
                request.AddData(LoginUserInfo.EmpId);
                request.AddData(LoginUserInfo.EmpName);
                request.AddData(mICommandManagement.IsMedicineCommand);
                request.AddData(LoginUserInfo.WorkId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "SendCommandList", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (result)
            {
                MessageBoxShowSimple("药品统领发送成功！");
                GetCommandSheetList();
            }
        }

        /// <summary>
        /// 绑定病人列表
        /// </summary>
        [WinformMethod]
        public void GetHasBeenSentDrugbillPatList()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "GetHasBeenSentDrugbillPatList");
            DataTable resultDt = retdata.GetData<DataTable>(0);

            if (resultDt != null && resultDt.Rows.Count > 0)
            {
                mICommandManagement.Bind_HasBeenSentDrugbillPatList(resultDt);
            }
        }

        /// <summary>
        /// 获取统领单列表
        /// </summary>
        [WinformMethod]
        public void GetDrugbillOrderList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mICommandManagement.OrderDeptId);
                request.AddData(mICommandManagement.OrderStatus);
                request.AddData(mICommandManagement.PatListId);
                request.AddData(mICommandManagement.StartDate);
                request.AddData(mICommandManagement.EndDate);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "GetDrugbillOrderList", requestAction);
            DataTable resultDt = retdata.GetData<DataTable>(0);
            mICommandManagement.Bind_OrderList(resultDt);

            // 重新加载统领明细和汇总数据
            GetDrugBillData();
        }

        /// <summary>
        /// 获取统领单明细和汇总数据
        /// </summary>
        [WinformMethod]
        public void GetDrugBillData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mICommandManagement.BillHeadID);
                request.AddData(mICommandManagement.DispDrugFlag);
                request.AddData(mICommandManagement.PatListId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "GetDrugBillData", requestAction);
            DataTable summaryDt = retdata.GetData<DataTable>(0);
            DataTable detailDt = retdata.GetData<DataTable>(1);
            mICommandManagement.Bind_DurgBillSummaryList(summaryDt);
            mICommandManagement.Bind_DurgBillDetailList(detailDt);
        }

        /// <summary>
        /// 重新发送统领单
        /// </summary>
        /// <param name="orderName">统领单名</param>
        [WinformMethod]
        public void AgainSendOrder(string orderName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mICommandManagement.BillHeadID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "AgainSendOrder", requestAction);
            MessageBoxShowSimple("统领单【" + orderName + "】重新发送成功！");
            GetDrugbillOrderList();
            GetDrugBillData();
        }

        /// <summary>
        /// 取消发送统领单
        /// </summary>
        /// <param name="orderName">统领单名</param>
        [WinformMethod]
        public void CancelSendOrder(string orderName)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mICommandManagement.BillHeadID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "CommandManagementController", "CancelSendOrder", requestAction);
            string result = retdata.GetData<string>(0);

            if (string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple("统领单【" + orderName + "】取消发送成功！");
                GetDrugbillOrderList();
                GetDrugBillData();
            }
            else
            {
                MessageBoxShowSimple(result);
            }
        }

        /// <summary>
        /// 提示消息
        /// </summary>
        /// <param name="msg">消息内容</param>
        [WinformMethod]
        public void MessageShow(string msg)
        {
            MessageBoxShowSimple(msg);
        }
    }
}
