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
    /// 账单计费控制器
    /// </summary>
    [WinformController(DefaultViewName = "FrmDoctorsOrderSccounting")]//在菜单上显示
    [WinformView(Name = "FrmDoctorsOrderSccounting", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmDoctorsOrderSccounting")]
    [WinformView(Name = "FrmFeePresDate", DllName = "HIS_IPManage.Winform.dll", ViewTypeName = "HIS_IPManage.Winform.ViewForm.FrmFeePresDate")]
    public class DoctorsOrderSccountingController : WcfClientController
    {
        /// <summary>
        /// 账单管理接口
        /// </summary>
        IDoctorsOrderSccounting iDoctorsOrderSccounting;

        /// <summary>
        /// 长期账单记账确认接口
        /// </summary>
        IFeePresDate iFeePresDate;

        /// <summary>
        /// 病人入院登记ID
        /// </summary>
        private int mPatListId = 0;

        /// <summary>
        /// 消息内容
        /// </summary>
        private string mMsg = string.Empty;

        /// <summary>
        /// 控制器初始化
        /// </summary>
        public override void Init()
        {
            iDoctorsOrderSccounting = (IDoctorsOrderSccounting)DefaultView;
            iFeePresDate = iBaseView["FrmFeePresDate"] as IFeePresDate;
        }

        /// <summary>
        /// 取得所有科室列表
        /// </summary>
        [WinformMethod]
        public void GetDeptList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(true);
            });

            // 通过WCF调用服务端控制器取得住院临床科室列表
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "AdmissionController", "GetDeptBasicData", requestAction);
            DataTable deptDt = retdata.GetData<DataTable>(0);
            iDoctorsOrderSccounting.Bind_DeptList(deptDt);
        }

        /// <summary>
        /// 获取在床病人列表
        /// </summary>
        [WinformMethod]
        public void GetPatientList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDoctorsOrderSccounting.DeptId);
                request.AddData(iDoctorsOrderSccounting.Patam);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetPatientList", requestAction);
            DataTable patDt = retdata.GetData<DataTable>(0);

            // 绑定在床病人列表
            iDoctorsOrderSccounting.Bind_PatientList(patDt);
        }

        /// <summary>
        /// 获取病人的累计交费金额和累计记账金额
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        [WinformMethod]
        public void GetPatSumPay(int patListId)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetPatSumPay", requestAction);
            decimal sumPay = retdata.GetData<decimal>(0);
            decimal longFeeOrder = retdata.GetData<decimal>(1);
            decimal tempFeeOrder = retdata.GetData<decimal>(2);
            decimal bedFee = retdata.GetData<decimal>(3);
            iDoctorsOrderSccounting.Bind_PatSumPay(sumPay, longFeeOrder, tempFeeOrder, bedFee);
        }

        /// <summary>
        /// 绑定弹出网格费用列表
        /// </summary>
        [WinformMethod]
        public void GetSimpleFeeItemData()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetSimpleFeeItemDataDt");
            DataTable dt = retdata.GetData<DataTable>(0);
            iDoctorsOrderSccounting.Bind_SimpleFeeItemData(dt);
        }

        /// <summary>
        /// 查询病人长期账单列表
        /// </summary>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void GetPatLongFeeItemGenerate(int patListID, int orderType)
        {
            mPatListId = patListID;
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patListID);
                request.AddData(orderType);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetPatFeeItemGenerate", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            iDoctorsOrderSccounting.Bind_LongOrderData(dt);
            iDoctorsOrderSccounting.SetGridColor();
        }

        /// <summary>
        /// 保存住院费用生成数据
        /// </summary>
        /// <param name="longOrderDt">账单数据</param>
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

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "SaveLongOrderData", requestAction);
            string result = retdata.GetData<string>(0);

            if (string.IsNullOrEmpty(result))
            {
                MessageBoxShowSimple("保存账单成功！");
                DataTable deeDt = retdata.GetData<DataTable>(1);
                GetPatLongFeeItemGenerate(mPatListId, orderType);
            }
            else
            {
                MessageBoxShowError(result);
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
        /// 删除费用记录
        /// </summary>
        /// <param name="feeItemDt">待删除数据列表</param>
        /// <param name="bindDt">界面已绑定数据列表</param>
        [WinformMethod]
        public void DelFeeLongOrderData(DataTable feeItemDt, DataTable bindDt)
        {
            if (MessageBoxShowYesNo("确定要删除费用记录记录吗？") == DialogResult.Yes)
            {
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
                    iDoctorsOrderSccounting.Bind_LongOrderData(bindDt);
                    iDoctorsOrderSccounting.SetGridColor();
                    return;
                }

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(feeItemDt);
                });

                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "DelFeeLongOrderData", requestAction);
                string result = retdata.GetData<string>(0);

                if (!string.IsNullOrEmpty(result))
                {
                    MessageBoxShowError(result);
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
                iDoctorsOrderSccounting.Bind_LongOrderData(bindDt);
                iDoctorsOrderSccounting.SetGridColor();
            }
        }

        /// <summary>
        /// 获取账单模板列表
        /// </summary>
        [WinformMethod]
        public void GetIPFeeItemTempList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(LoginUserInfo.WorkId);
                request.AddData(LoginUserInfo.DeptId);
                request.AddData(LoginUserInfo.EmpId);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetFeeItemTempList", requestAction);
            List<IP_FeeItemTemplateHead> feeTempList = retdata.GetData<List<IP_FeeItemTemplateHead>>(0);
            iDoctorsOrderSccounting.Bind_FeeTempList(feeTempList);
        }

        /// <summary>
        /// 根据模板ID查询模板对应的账单明细数据
        /// </summary>
        /// <param name="tempHeadID">账单模板头ID</param>
        [WinformMethod]
        public void GetFeeItemTempDetails(int tempHeadID)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempHeadID);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetFeeItemTempDetails", requestAction);
            DataTable feeDetailsDt = retdata.GetData<DataTable>(0);
            iDoctorsOrderSccounting.Bind_TempDetailLongOrderData(feeDetailsDt);
        }

        /// <summary>
        /// 账单记账
        /// </summary>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void FeeItemAccounting(int orderType)
        {
            if (orderType == 3)
            {
                // 去空白行
                RemoveEmpty(iDoctorsOrderSccounting.LongOrderList);
                // 找出数量等于小于0的账单
                DataRow[] arrayDr = iDoctorsOrderSccounting.LongOrderList.Select("Amount<=0");

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
                request.AddData(iDoctorsOrderSccounting.LongOrderList);
                request.AddData(LoginUserInfo.EmpId);
                //request.AddData(LoginUserInfo.DeptId);
                if (orderType == 2)
                {
                    request.AddData(iFeePresDate.StartTime);
                    request.AddData(iFeePresDate.EndTime);
                    request.AddData(iFeePresDate.BedOrderChecked);
                    request.AddData(iFeePresDate.PresDateChecked);
                }
                else
                {
                    request.AddData(DateTime.Now);
                    request.AddData(DateTime.Now);
                    request.AddData(false);
                    request.AddData(true);
                }
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "FeeItemAccounting", requestAction);
            bool result = retdata.GetData<bool>(0);
            List<string> msgList = retdata.GetData<List<string>>(1);

            if (result)
            {
                string msg = "费用记账成功！";

                if (msgList.Count > 0)
                {
                    msg += "其中";
                    msg += string.Join("、", msgList.ToArray());
                    msg += "等药品库存数不够！";
                }

                mMsg = msg;

                if (orderType == 2)
                {
                    iFeePresDate.CloseForm();
                }
                else
                {
                    MessageBoxShowSimple(msg);
                    GetPatLongFeeItemGenerate(mPatListId, orderType);
                    GetPatSumPay(mPatListId);
                }
            }
            else
            {
                MessageBoxShowError(msgList[0]);
            }
        }
        
        /// <summary>
        /// 长期账单记账--打开处方日期确认界面
        /// </summary>
        [WinformMethod]
        public void ShowFrmFeePresDate()
        {
            // 去空白行
            RemoveEmpty(iDoctorsOrderSccounting.LongOrderList);
            // 找出数量等于小于0的账单
            DataRow[] arrayDr = iDoctorsOrderSccounting.LongOrderList.Select("Amount<=0");

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

            ((Form)iBaseView["FrmFeePresDate"]).ShowDialog();

            if (!string.IsNullOrEmpty(mMsg))
            {
                MessageBoxShowSimple(mMsg);
            }

            // 重新加载长期账单列表
            GetPatLongFeeItemGenerate(mPatListId, 2);

            // 重新加载病人费用数据
            GetPatSumPay(mPatListId);
        }

        /// <summary>
        /// 停用账单
        /// </summary>
        /// <param name="stopFeeDt">待停用账单列表</param>
        /// <param name="orderType">账单类型</param>
        [WinformMethod]
        public void StopFeeLongOrderData(DataTable stopFeeDt, int orderType)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(stopFeeDt);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "StopFeeLongOrderData", requestAction);
            string result = retdata.GetData<string>(0);

            if (!string.IsNullOrEmpty(result))
            {
                MessageBoxShowError(result);
            }
            else
            {
                MessageBoxShowSimple("账单停用成功！");
                GetPatLongFeeItemGenerate(mPatListId, orderType);
            }
        }

        /// <summary>
        /// 查询已记账费用列表
        /// </summary>
        [WinformMethod]
        public void GetCostList()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(iDoctorsOrderSccounting.PatListID);
                request.AddData(iDoctorsOrderSccounting.OrderType);
                request.AddData(iDoctorsOrderSccounting.ItemID);
                request.AddData(iDoctorsOrderSccounting.StartTime);
                request.AddData(iDoctorsOrderSccounting.EndTime);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "GetCostList", requestAction);
            DataTable feeDt = retdata.GetData<DataTable>(0);
            // 绑定费用数据
            iDoctorsOrderSccounting.Bind_CostList(feeDt);
            // 设置费用列表网格显示颜色
            iDoctorsOrderSccounting.SetFeeListGridColor();
        }

        /// <summary>
        /// 冲账
        /// </summary>
        /// <param name="tempDt">冲账列表</param>
        [WinformMethod]
        public void StrikeABalance(DataTable tempDt)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempDt);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "StrikeABalance", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (!result)
            {
                List<string> msgList = retdata.GetData<List<string>>(1);
                string msg = string.Join("、", msgList.ToArray());
                msg = msg.Substring(0, msg.Length - 1);
                MessageBoxShowSimple(msg + "等项目已正常执行，无法进行冲账操作！");
            }

            GetCostList();
            GetPatSumPay(mPatListId);
        }

        /// <summary>
        /// 取消冲账
        /// </summary>
        /// <param name="tempDt">取消冲账列表</param>
        [WinformMethod]
        public void CancelStrikeABalance(DataTable tempDt)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(tempDt);
            });

            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "DoctorsOrderSccountingController", "CancelStrikeABalance", requestAction);
            bool result = retdata.GetData<bool>(0);

            if (!result)
            {
                List<string> msgList = retdata.GetData<List<string>>(1);
                string msg = string.Join("、", msgList.ToArray());
                msg = msg.Substring(0, msg.Length - 1);
                MessageBoxShowSimple(msg + "等药品库存数不够，无法进行取消冲账操作！");
            }

            GetCostList();
            GetPatSumPay(mPatListId);
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
