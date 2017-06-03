using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.Controller
{
    /// <summary>
    /// 医嘱费用核对控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDocOrderExpenseCheck")]//在菜单上显示
    [WinformView(Name = "FrmDocOrderExpenseCheck", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmDocOrderExpenseCheck")]
    [WinformView(Name = "FrmFeePresDate", DllName = "HIS_IPNurse.Winform.dll", ViewTypeName = "HIS_IPNurse.Winform.ViewForm.FrmFeePresDate")]
    public class DocOrderExpenseCheckController : WcfClientController
    {
        /// <summary>
        /// 医嘱费用核对接口
        /// </summary>
        IDocOrderExpenseCheck mIDocOrderExpenseCheck;

        /// <summary>
        /// 医嘱费用记账确认接口
        /// </summary>
        IFeePresDate mIFeePresDate;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            mIDocOrderExpenseCheck = (IDocOrderExpenseCheck)DefaultView;
            mIFeePresDate = iBaseView["FrmFeePresDate"] as IFeePresDate;
        }

        /// <summary>
        /// 费用明细
        /// </summary>
        private DataTable feeItemDt = new DataTable();

        /// <summary>
        /// 数据加载
        /// </summary>
        public override void AsynInit()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetSimpleFeeItemDataDt");
            feeItemDt = retdata.GetData<DataTable>(0).Select("ItemClass NOT IN (1,4)").CopyToDataTable();
        }

        /// <summary>
        /// 将数据绑定到界面控件
        /// </summary>
        public override void AsynInitCompleted()
        {
            mIDocOrderExpenseCheck.bind_SimpleFeeItemData(feeItemDt);
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
            mIDocOrderExpenseCheck.bind_DeptList(deptDt, LoginUserInfo.DeptId);
        }

        /// <summary>
        /// 获取病人列表
        /// </summary>
        [WinformMethod]
        public void GetPatList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.DeptId);
                request.AddData(mIDocOrderExpenseCheck.PatStatus);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetPatList", requestAction);
            DataTable patListDt = retdata.GetData<DataTable>(0);
            mIDocOrderExpenseCheck.bind_PatList(patListDt);
        }

        /// <summary>
        /// 获取病人费用信息
        /// </summary>
        [WinformMethod]
        public void GetPatFeeInfo()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.PatListID);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetPatFeeInfo", requestAction);
            DataTable sumDepositDt = retdata.GetData<DataTable>(0);
            DataTable patSumFeeDt = retdata.GetData<DataTable>(1);
            // 预交金
            decimal sumDeposit = Convert.ToDecimal(sumDepositDt.Rows[0][0]);
            // 累计记账金额
            decimal patSumFee = Convert.ToDecimal(patSumFeeDt.Rows[0][0]);
            // 床位费
            decimal patBedFee = Convert.ToDecimal(patSumFeeDt.Rows[1][0]);
            // 今日费用
            decimal caySumFee = Convert.ToDecimal(patSumFeeDt.Rows[2][0]);
            mIDocOrderExpenseCheck.bind_PatFeeInfo(sumDeposit, patSumFee, patBedFee, caySumFee);
        }

        /// <summary>
        /// 获取病人医嘱列表
        /// </summary>
        [WinformMethod]
        public void GetPatOrderList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.PatListID);// 病人登记ID
                request.AddData(mIDocOrderExpenseCheck.OrderType);// 医嘱类型
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetPatOrderList", requestAction);
            DataTable orderDt = retdata.GetData<DataTable>(0);
            mIDocOrderExpenseCheck.bind_PatOrderList(orderDt);
        }

        /// <summary>
        /// 获取医嘱关联病人费用列表
        /// </summary>
        [WinformMethod]
        public void GetOrderFeeList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.PatListID);// 病人登记ID
                request.AddData(mIDocOrderExpenseCheck.OrderID);// 医嘱ID
                request.AddData(mIDocOrderExpenseCheck.GroupID);// 医嘱分组ID
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetOrderFeeList", requestAction);
            // 绑定医嘱关联费用数据
            DataTable orderFeeList = retdata.GetData<DataTable>(0);
            DataTable orderSumFeeList = retdata.GetData<DataTable>(1);
            mIDocOrderExpenseCheck.bind_OrderFeeList(orderFeeList, orderSumFeeList);
        }

        /// <summary>
        /// 冲账
        /// </summary>
        /// <param name="tempDt">冲账列表</param>
        /// <param name="isOrder">医嘱标志</param>
        /// <param name="orderID">医嘱ID</param>
        /// <param name="isUpdateExa">是否修改检查检验治疗申请表</param>
        [WinformMethod]
        public void OrderStrikeABalance(DataTable tempDt, bool isOrder, int orderID, bool isUpdateExa)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempDt);
                request.AddData(orderID);// 医嘱ID
                request.AddData(isUpdateExa);// 是否修改检查检验治疗申请表 
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "OrderStrikeABalance", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (!result)
            {
                List<string> msgList = retdata.GetData<List<string>>(1);
                //string msg = string.Join("、", MsgList.ToArray());
                //msg = msg.Substring(0, msg.Length - 1);
                MessageBoxShowSimple(msgList[0]);
            }
            else
            {
                MessageBoxShowSimple("冲账成功！");
            }

            if (isOrder)
            {
                // 获取医嘱费用列表
                GetOrderFeeList();
                // 获取病人最新费用信息
                GetPatFeeInfo();
            }
        }

        /// <summary>
        /// 取消冲账
        /// </summary>
        /// <param name="tempDt">取消冲账列表</param>
        /// <param name="isOrder">医嘱标志</param>
        /// <param name="orderID">医嘱ID</param>
        /// <param name="isUpdateExa">是否修改检查检验治疗申请表</param>
        [WinformMethod]
        public void CancelOrderStrikeABalance(DataTable tempDt, bool isOrder, int orderID, bool isUpdateExa)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempDt);
                request.AddData(orderID);// 医嘱ID
                request.AddData(isUpdateExa);// 是否修改检查检验治疗申请表 
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "CancelOrderStrikeABalance", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (!result)
            {
                List<string> msgList = retdata.GetData<List<string>>(1);
                //string msg = string.Join("、", MsgList.ToArray());
                //msg = msg.Substring(0, msg.Length - 1);
                MessageBoxShowSimple(msgList[0]);
                //MessageBoxShowSimple(msg + "等药品库存数不够，无法进行取消冲账操作！");
            }
            else
            {
                MessageBoxShowSimple("取消冲账成功！");
            }

            if (isOrder)
            {
                // 获取医嘱费用列表
                GetOrderFeeList();
                // 获取病人最新费用信息
                GetPatFeeInfo();
            }
        }

        /// <summary>
        /// 查询已记账费用列表
        /// </summary>
        /// <param name="orderType">医嘱类型</param>
        [WinformMethod]
        public void GetCostList(string orderType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.PatListID);
                request.AddData(orderType);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetCostList", requestAction);
            DataTable feeDt = retdata.GetData<DataTable>(0);
            DataTable sumFeeDt = retdata.GetData<DataTable>(1);
            // 绑定费用数据
            mIDocOrderExpenseCheck.bind_CostList(feeDt, sumFeeDt);
        }

        /// <summary>
        /// 删除费用记录
        /// </summary>
        /// <param name="feeItemDt">待删除的费用记录</param>
        /// <param name="bindDt">网格费用数据源</param>
        [WinformMethod]
        public void DelFeeLongOrderData(DataTable feeItemDt, DataTable bindDt)
        {
            // 提示是否删除数据Msg
            if (MessageBoxShowYesNo("确定要删除费用记录记录吗？") == DialogResult.Yes)
            {
                // 删除完全空白行
                RemoveEmpty(feeItemDt);
                feeItemDt.AcceptChanges();

                // 删除未保存的记录
                DataRow[] arrayDr = feeItemDt.Select("GenerateID<=0");
                if (arrayDr.Length > 0)
                {
                    foreach (DataRow dr in arrayDr)
                    {
                        feeItemDt.Rows.Remove(dr);
                    }
                }

                // 如果选中的数据全部为未保存的记录，并且已全部删除，则不需要执行服务端的删除操作，直接从源数据中删除选中行
                if (feeItemDt.Rows.Count <= 0)
                {
                    MessageBoxShowSimple("费用记录删除成功！");
                    for (int i = bindDt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (!string.IsNullOrEmpty(bindDt.Rows[i]["CheckFlg"].ToString()))
                        {
                            if (int.Parse(bindDt.Rows[i]["CheckFlg"].ToString()) == 1)
                            {
                                bindDt.Rows[i].Delete();
                            }
                        }
                    }

                    bindDt.AcceptChanges();
                    // 重新绑定数据
                    mIDocOrderExpenseCheck.bind_LongOrderData(bindDt);
                    return;
                }

                // 执行服务端删除函数，删除数据库中的费用数据
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(feeItemDt);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "DelFeeLongOrderData", requestAction);
                string result = retdata.GetData<string>(0);
                if (!string.IsNullOrEmpty(result))
                {
                    MessageBoxShowSimple(result);
                }
                else
                {
                    MessageBoxShowSimple("费用记录删除成功！");
                    for (int i = bindDt.Rows.Count - 1; i >= 0; i--)
                    {
                        if (!string.IsNullOrEmpty(bindDt.Rows[i]["CheckFlg"].ToString()))
                        {
                            if (int.Parse(bindDt.Rows[i]["CheckFlg"].ToString()) == 1)
                            {
                                bindDt.Rows[i].Delete();
                            }
                        }
                    }
                }

                bindDt.AcceptChanges();
                // 重新绑定数据
                mIDocOrderExpenseCheck.bind_LongOrderData(bindDt);
            }
        }

        /// <summary>
        /// 保存住院费用生成数据
        /// </summary>
        /// <param name="longOrderDt">住院费用生成数据</param>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void SaveLongOrderData(DataTable longOrderDt, int orderType)
        {
            DataTable feeItemDt = longOrderDt.Clone();
            if (longOrderDt != null && longOrderDt.Rows.Count > 0)
            {
                // 去空白行
                RemoveEmpty(longOrderDt);
                // 过滤出已修改的数据
                DataRow[] updArrayDr = longOrderDt.Select("IsUpdate=1");
                if (updArrayDr.Length > 0)
                {
                    foreach (DataRow dr in updArrayDr)
                    {
                        feeItemDt.Rows.Add(dr.ItemArray);
                    }
                }
                else
                {
                    MessageBoxShowSimple("没有需要保存的数据，请确认！");
                    return;
                }
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = longOrderDt.Select("Amount<=0");
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
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(feeItemDt);
                request.AddData(LoginUserInfo.EmpId);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "SaveLongOrderData", requestAction);
            string result = retdata.GetData<string>(0);
            if (string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple("保存账单成功！");
                DataTable feeDt = retdata.GetData<DataTable>(1);
                GetPatLongFeeItemGenerate(orderType);
            }
            else
            {
                MessageBoxShowSimple(result);
            }
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void FeeItemAccounting(int orderType)
        {
            mMsg = string.Empty;
            if (orderType == 3)
            {
                // 去空白行
                RemoveEmpty(mIDocOrderExpenseCheck.LongOrderList);
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = mIDocOrderExpenseCheck.LongOrderList.Select("Amount<=0");
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
                    return;
                }
            }
            else if (orderType == 2)
            {
                // 长期账单未勾选任何费用时不进行任何操作
                if (!mIFeePresDate.BedOrderChecked && !mIFeePresDate.PresDateChecked)
                {
                    return;
                }
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.LongOrderList);
                request.AddData(LoginUserInfo.EmpId);
                if (orderType == 2)
                {
                    request.AddData(mIFeePresDate.EndTime);
                    request.AddData(mIFeePresDate.BedOrderChecked);
                    request.AddData(mIFeePresDate.PresDateChecked);
                }
                else
                {
                    //request.AddData(DateTime.Now);
                    request.AddData(DateTime.Now);
                    request.AddData(false);
                    request.AddData(true);
                }

                request.AddData(mIDocOrderExpenseCheck.PatListID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "FeeItemAccounting", requestAction);
            bool result = retdata.GetData<bool>(0);
            if (result)
            {
                string msg = "费用记账成功！";
                mMsg = msg;
                if (orderType == 2)
                {
                    mIFeePresDate.CloseForm();
                }
                else
                {
                    MessageBoxShowSimple(msg);
                    GetPatLongFeeItemGenerate(orderType);
                    // 获取病人最新费用信息
                    GetPatFeeInfo();
                }
            }
            else
            {
                // 记账失败提示Msg
                List<string> msgList = retdata.GetData<List<string>>(1);
                MessageBoxShowSimple(msgList[0]);
            }
        }

        /// <summary>
        /// 消息内容
        /// </summary>
        private string mMsg = string.Empty;

        /// <summary>
        /// 长期账单记账--打开处方日期确认界面
        /// </summary>
        /// <param name="isBedFee">是否为床位费记账</param>
        [WinformMethod]
        public void ShowFrmFeePresDate(bool isBedFee)
        {
            if (isBedFee)
            {
                mIFeePresDate.SetControlEnabled(true);
            }
            else
            {
                // 去空白行
                RemoveEmpty(mIDocOrderExpenseCheck.LongOrderList);
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = mIDocOrderExpenseCheck.LongOrderList.Select("Amount<=0");
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
                    return;
                }

                mIFeePresDate.SetControlEnabled(false);
            }

            ((Form)iBaseView["FrmFeePresDate"]).ShowDialog();
            if (!string.IsNullOrEmpty(mMsg))
            {
                MessageBoxShowSimple(mMsg);
            }

            GetPatLongFeeItemGenerate(2);
            // 获取病人最新费用信息
            GetPatFeeInfo();
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <param name="stopFeeDt">要停用的账单列表</param>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void StopFeeLongOrderData(DataTable stopFeeDt, int orderType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stopFeeDt);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "StopFeeLongOrderData", requestAction);
            string result = retdata.GetData<string>(0);
            if (!string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple(result);
            }
            else
            {
                MessageBoxShowSimple("账单停用成功！");
                GetPatLongFeeItemGenerate(orderType);
            }
        }

        /// <summary>
        /// 查询病人长期账单列表
        /// </summary>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void GetPatLongFeeItemGenerate(int orderType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(mIDocOrderExpenseCheck.PatListID);
                request.AddData(orderType);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetPatFeeItemGenerate", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            // 绑定账单数据
            mIDocOrderExpenseCheck.bind_LongOrderData(dt);
        }

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        [WinformMethod]
        public void GetSimpleFeeItemData()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetSimpleFeeItemDataDt");
            DataTable dt = retdata.GetData<DataTable>(0).Select("ItemClass NOT IN (1,4)").CopyToDataTable();
            mIDocOrderExpenseCheck.bind_SimpleFeeItemData(dt);
        }

        /// <summary>
        /// 去除DataTable中的完全空白行
        /// </summary>
        /// <param name="dt">待处理的DataTable</param>
        private void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool isNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0 || j == 2)
                    {
                        continue;
                    }

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
        /// 获取病人状态
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <returns>病人信息</returns>
        [WinformMethod]
        public DataTable GetPatientStatus(int patlistid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DocOrderExpenseCheckController", "GetPatientStatus", requestAction);
            return retdata.GetData<DataTable>(0);
        }

        #region "共用方法--提示Msg(界面用)"

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
