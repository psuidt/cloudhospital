using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.Controller
{
    [WinformController(DefaultViewName = "FrmRefundMessage")]//与系统菜单对应
    [WinformView(Name = "FrmRefundMessage", DllName = "HIS_OPManage.Winform.dll", ViewTypeName = "HIS_OPManage.Winform.ViewForm.FrmRefundMessage")]

    /// <summary>
    /// 退费消息控制器类
    /// </summary>
    public class RefundController : WcfClientController
    {
        /// <summary>
        /// 退费消息界面接口
        /// </summary>
        IFrmRefundMessage ifrmRefundMessage;   
         
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            ifrmRefundMessage = (IFrmRefundMessage)iBaseView["FrmRefundMessage"];
        }
               
        /// <summary>
        /// 通过票据号查找门诊收费处方
        /// </summary>
        /// <param name="invoiceNO">门诊票据号</param>
        [WinformMethod]
        public void QueryPresByInvoiceNO(string invoiceNO)
        {
            try
            {
                ifrmRefundMessage.SetPatInfo(null);
                ifrmRefundMessage.DtRefundPresc = new DataTable();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(invoiceNO);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RefundController", "QueryPresByInvoiceNO", requestAction);
                DataTable dtPresc = retdata.GetData<DataTable>(0); //卡类型
                if (dtPresc.Rows.Count == 0)
                {
                    MessageBoxShowError("找不到该票号对应的收费处方信息");
                    return;
                }

                ifrmRefundMessage.SetPatInfo(dtPresc.Rows[0]);

                // 过滤数据
                DataTable drugDetails = dtPresc.Clone();
                DataTable medialDetails = dtPresc.Clone();
                drugDetails.TableName = "DrugDetails";

                // 过滤药品明细数据
                DataView longView = new DataView(dtPresc);
                string longSqlWhere = " ExamItemID = 0";
                longView.RowFilter = longSqlWhere;
                drugDetails.Merge(longView.ToTable());

                // 过滤医技申请明细数据
                DataView tempView = new DataView(dtPresc);
                string tempSqlWhere = " ExamItemID > 0";
                tempView.RowFilter = tempSqlWhere;
                medialDetails.Merge(tempView.ToTable());
                DataColumn col = new DataColumn();
                col.ColumnName = "Sel";
                col.DataType =typeof( int);              
                medialDetails.Columns.Add(col);
                if (medialDetails.Rows.Count > 0)
                {
                    DataTable dt = medialDetails.Copy();
                    dt.Clear();
                    dt.Rows.Add(medialDetails.Rows[0].ItemArray);
                    for (int i = 1; i < medialDetails.Rows.Count; i++) 
                    {
                        if (Convert.ToInt32(medialDetails.Rows[i]["FeeItemHeadID"]) != Convert.ToInt32(medialDetails.Rows[i - 1]["FeeItemHeadID"]) ||Convert.ToInt32(medialDetails.Rows[i]["ExamItemID"]) != Convert.ToInt32(medialDetails.Rows[i - 1]["ExamItemID"]))
                        {
                            dt.Rows.Add(medialDetails.Rows[i].ItemArray);
                        }
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["DistributeFlag"]) == 0)
                        {
                            dt.Rows[i]["Sel"] = 1;
                        }
                        else
                        {
                            dt.Rows[i]["Sel"] = 0;
                        }
                    }

                    ifrmRefundMessage.DtRefundMedical = dt;
                }
                else
                {
                    ifrmRefundMessage.DtRefundMedical = medialDetails;
                } 

                ifrmRefundMessage.DtRefundPresc = CaculateDataTable(drugDetails);         
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }

        /// <summary>
        /// 转换显示
        /// </summary>
        /// <param name="dtPresc">处方数据</param>
        /// <returns>转换后的处方数据</returns>
        private DataTable CaculateDataTable(DataTable dtPresc)
        {
            if (dtPresc.Rows.Count == 0)
            {
                return new DataTable();
            }

            int lastfeeitemheadid = Convert.ToInt32(dtPresc.Rows[0]["FeeItemHeadID"]);
            int presno = 1;
            decimal oldTotalFee = Convert.ToDecimal(dtPresc.Rows[0]["totalFee"]);
            dtPresc.Rows[0]["presNO"] = presno;
            for (int i = 1; i < dtPresc.Rows.Count; i++)
            {               
                if (Convert.ToInt32(dtPresc.Rows[i]["FeeItemHeadID"]) == lastfeeitemheadid)
                {
                    dtPresc.Rows[i]["presNO"] = DBNull.Value;
                }
                else
                {
                    presno += 1;
                    dtPresc.Rows[i]["presNO"] = presno;
                    oldTotalFee += Convert.ToDecimal(dtPresc.Rows[i]["totalFee"]);
                    lastfeeitemheadid = Convert.ToInt32(dtPresc.Rows[i]["FeeItemHeadID"]);
                }
            }

            ifrmRefundMessage.OldtotalFee = oldTotalFee.ToString("0.00");
            for (int i = 0; i < dtPresc.Rows.Count; i++)
            {
                if (dtPresc.Rows[i]["statid"].ToString() == "102" && Convert.ToInt32(dtPresc.Rows[i]["DistributeFlag"]) == 1)
                {
                    //中草药已经发药的不能退
                    dtPresc.Rows[i]["RefundPackNum"] = 0;
                    dtPresc.Rows[i]["RefundMiniNum"] = 0;
                    dtPresc.Rows[i]["refundpresamount"] = 0;
                    dtPresc.Rows[i]["RefundFee"] = 0;
                }
                else
                {
                    if (Convert.ToInt32(dtPresc.Rows[i]["ItemType"]) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(dtPresc.Rows[i]["ItemType"]) == (int)OP_Enum.ItemType.组合项目)
                    {
                        if (Convert.ToInt32(dtPresc.Rows[i]["DistributeFlag"]) == 1)
                        {
                            //项目已经确费的不能退费
                            dtPresc.Rows[i]["RefundPackNum"] = 0;
                            dtPresc.Rows[i]["RefundMiniNum"] = 0;
                            dtPresc.Rows[i]["refundpresamount"] = 1;
                            dtPresc.Rows[i]["RefundFee"] = 0;
                        }

                        dtPresc.Rows[i]["oldMiniNum"] = dtPresc.Rows[i]["Amount"];
                        dtPresc.Rows[i]["oldPackNum"] = (Convert.ToDecimal(dtPresc.Rows[i]["Amount"]) - Convert.ToDecimal(dtPresc.Rows[i]["oldMiniNum"])) / Convert.ToDecimal(dtPresc.Rows[i]["UnitNO"]);
                    }
                    else
                    {
                        dtPresc.Rows[i]["oldMiniNum"] = Convert.ToDecimal(dtPresc.Rows[i]["Amount"]) % Convert.ToDecimal(dtPresc.Rows[i]["UnitNO"]);
                        dtPresc.Rows[i]["oldPackNum"] = (Convert.ToDecimal(dtPresc.Rows[i]["Amount"]) - Convert.ToDecimal(dtPresc.Rows[i]["oldMiniNum"])) / Convert.ToDecimal(dtPresc.Rows[i]["UnitNO"]);
                    }

                    dtPresc.Rows[i]["RefundPackNum"] = dtPresc.Rows[i]["oldPackNum"];
                    dtPresc.Rows[i]["RefundMiniNum"] = dtPresc.Rows[i]["oldMiniNum"];
                }
            }

            DataTable dtCopy = dtPresc.Clone();
            dtCopy.Clear();
            dtCopy.Rows.Add(dtPresc.Rows[0].ItemArray);
            for (int i = 1; i < dtPresc.Rows.Count; i++)
            {
                if (dtPresc.Rows[i]["presNO"] != DBNull.Value)
                {                   
                    DataRow row = dtCopy.NewRow();
                    row["ItemName"] = "小   计";
                    row["oldItemFee"] = dtPresc.Compute("sum(oldItemFee)", "FeeItemHeadID=" + dtPresc.Rows[i - 1]["FeeItemHeadID"]);
                    row["RefundFee"] = dtPresc.Compute("sum(RefundFee)", "FeeItemHeadID=" + dtPresc.Rows[i - 1]["FeeItemHeadID"]);
                    if (Convert.ToInt32(dtPresc.Rows[i - 1]["ExamItemID"]) > 0)
                    {
                        row["Spec"] = dtPresc.Rows[i - 1]["Memo"].ToString().Trim();
                    }

                    dtCopy.Rows.Add(row.ItemArray);
                }

                dtCopy.Rows.Add(dtPresc.Rows[i].ItemArray);
                if (i == dtPresc.Rows.Count - 1)
                {
                    DataRow row = dtCopy.NewRow();
                    row["ItemName"] = "小   计";
                    row["oldItemFee"] = dtPresc.Compute("sum(oldItemFee)", "FeeItemHeadID=" + dtPresc.Rows[i - 1]["FeeItemHeadID"]);
                    row["RefundFee"] = dtPresc.Compute("sum(RefundFee)", "FeeItemHeadID=" + dtPresc.Rows[i - 1]["FeeItemHeadID"]);
                     if (Convert.ToInt32(dtPresc.Rows[i - 1]["ExamItemID"]) > 0)
                    {
                        row["Spec"] = dtPresc.Rows[i - 1]["Memo"].ToString().Trim();
                    }

                    dtCopy.Rows.Add(row);
                }
            }

            return dtCopy;
        }
        #region 网格操作
        /// <summary>
        /// 只读判断
        /// </summary>
        /// <param name="rowindex">行号</param>
        [WinformMethod]
        public void SetReadOnly(int rowindex)
        {
            DataTable dtPresc = ifrmRefundMessage.DtRefundPresc;
            if (dtPresc.Rows[rowindex]["ItemName"].ToString() == "小   计")
            {
                ifrmRefundMessage.SetReadOnly(2);
                return;
            }

            if (dtPresc.Rows[rowindex]["StatID"].ToString() == "102")
            {
                //中药 只能全退
                if (Convert.ToInt32(dtPresc.Rows[rowindex]["DistributeFlag"]) == 1)
                {
                    //中药已经发药的不能退
                    ifrmRefundMessage.SetReadOnly(2);
                }

                if (Convert.ToInt32(dtPresc.Rows[rowindex]["DistributeFlag"]) == 0)
                {
                    //中药可退付数
                    ifrmRefundMessage.SetReadOnly(3);
                }
            }
            else
            {
                if (Convert.ToInt32(dtPresc.Rows[rowindex]["itemtype"]) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(dtPresc.Rows[rowindex]["itemtype"]) == (int)OP_Enum.ItemType.组合项目)
                {
                    if (Convert.ToInt32(dtPresc.Rows[rowindex]["DistributeFlag"]) == 1)
                    {
                        //项目已经确费的不能退
                        //全部只读
                        ifrmRefundMessage.SetReadOnly(2);
                    }
                    else
                    {
                        if (Convert.ToDecimal(dtPresc.Rows[rowindex]["oldMiniNum"]) == 0)
                        {
                            ifrmRefundMessage.SetReadOnly(0);
                        }

                        if (Convert.ToDecimal(dtPresc.Rows[rowindex]["oldPackNum"]) == 0)
                        {
                            ifrmRefundMessage.SetReadOnly(1);
                        }
                    }
                }
                else
                {
                    if (Convert.ToInt32(dtPresc.Rows[rowindex]["ResolveFlag"]) == 1)
                    {
                        ifrmRefundMessage.SetReadOnly(4);
                    }
                    else
                    {
                        if (Convert.ToDecimal(dtPresc.Rows[rowindex]["oldMiniNum"]) == 0)
                        {
                            ifrmRefundMessage.SetReadOnly(0);
                        }

                        if (Convert.ToDecimal(dtPresc.Rows[rowindex]["oldPackNum"]) == 0)
                        {
                            ifrmRefundMessage.SetReadOnly(1);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算总金额
        /// </summary>
        /// <param name="rowindex">行号</param>
        [WinformMethod]
        public void CalculateRefundTotalFee(int rowindex)
        {
            DataTable dtPresc = ifrmRefundMessage.DtRefundPresc;
            decimal oldminimun = Convert.ToDecimal(dtPresc.Rows[rowindex]["oldMiniNum"]);
            decimal oldpacknum = Convert.ToDecimal(dtPresc.Rows[rowindex]["oldPackNum"]);
            decimal oldamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["Amount"])*Convert.ToDecimal(dtPresc.Rows[rowindex]["presamount"]); 
            decimal refundminimun = Convert.ToDecimal(dtPresc.Rows[rowindex]["RefundMiniNum"]);
            decimal refundpacknum = Convert.ToDecimal(dtPresc.Rows[rowindex]["RefundPackNum"]);
            decimal presamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["presamount"]);
            decimal refundpresamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["refundpresamount"]);
            decimal unitNO = Convert.ToDecimal(dtPresc.Rows[rowindex]["UnitNO"]);
            decimal refundamount = (refundpacknum * unitNO + refundminimun) * refundpresamount;
            if (refundamount > oldamount)
            {
                MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】退数量大于原数量");
                return;
            }

            decimal sellprice = Convert.ToDecimal(dtPresc.Rows[rowindex]["RetailPrice"]);
            
            //计算行合计金额
            decimal rowTotal = (sellprice / unitNO) * ((unitNO * refundpacknum) + refundminimun) * refundpresamount;
           
            //保留两位小数   
            dtPresc.Rows[rowindex]["RefundFee"] = decimal.Round(rowTotal, 2);
            decimal sumFee = 0;
            for (int i = 0; i < dtPresc.Rows.Count; i++)
            {
                if (dtPresc.Rows[i]["ItemName"].ToString().Trim() == "小   计")
                {
                    dtPresc.Rows[i]["RefundFee"] = sumFee;
                    sumFee = 0;
                }
                else
                {
                    sumFee += Convert.ToDecimal(dtPresc.Rows[i]["RefundFee"]);
                }
            }        
        }

        /// <summary>
        /// 中药付数变化修改
        /// </summary>
        /// <param name="rowindex">行号</param>
        [WinformMethod]
        public void SetRefundPresNums(int rowindex)
        {
            DataTable dtPresc = ifrmRefundMessage.DtRefundPresc;
            int feeitemheadid = Convert.ToInt32(dtPresc.Rows[rowindex]["feeitemheadid"]);
            decimal refundpresamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["refundpresamount"]);
            for (int i = 0; i < dtPresc.Rows.Count; i++)
            {
                if (dtPresc.Rows[i]["ItemName"].ToString().Trim() == "小   计")
                {
                    continue;
                }

                if (Convert.ToInt32(dtPresc.Rows[i]["feeitemheadid"]) == feeitemheadid)
                {
                    dtPresc.Rows[i]["refundpresamount"] = refundpresamount;
                    CalculateRefundTotalFee(i);
                }
            }
        }
        #endregion

        #region 保存退费消息
        /// <summary>
        /// 保存退费消息
        /// </summary>
        [WinformMethod]
        public void SaveReundMessage()
        {
            if (SaveCheck())
            {
                try
                {
                    DataTable dtPresc = ifrmRefundMessage.DtRefundPresc;
                    DataTable dtRefundMedical = ifrmRefundMessage.DtRefundMedical;
                    if (dtPresc.Rows.Count == 0 && (dtRefundMedical == null || dtRefundMedical.Rows.Count == 0))
                    {
                        MessageBoxShowSimple("没有需要退费的处方和医技项目");
                        return;
                    }

                    if (dtRefundMedical != null && dtRefundMedical.Rows.Count > 0)
                    {
                        DataTable dtCopy = dtRefundMedical.Clone();
                        dtCopy.Clear();
                        DataView tempView = new DataView(dtRefundMedical);
                        string tempSqlWhere = " Sel > 0";
                        tempView.RowFilter = tempSqlWhere;
                        dtCopy.Merge(tempView.ToTable());
                        if (dtPresc.Rows.Count == 0 && dtCopy.Rows.Count == 0)
                        {
                            MessageBoxShowSimple("没有需要退费的处方和医技项目");
                            return;
                        }
                        //for (int i = 0; i < dtCopy.Rows.Count; i++)
                        //{
                        //    if (Convert.ToInt32(dtCopy.Rows[i]["PresEmpID"]) != LoginUserInfo.EmpId)
                        //    {
                        //        MessageBoxShowError("【" + dtCopy.Rows[i]["ItemName"] + "】不是您开的处方，不能由您退费");
                        //        return ;
                        //    }
                        //}
                    }

                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(ifrmRefundMessage.DtRefundPresc);
                        request.AddData(dtRefundMedical);
                        request.AddData(LoginUserInfo.EmpId);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RefundController", "SaveRefundMessage", requestAction);
                    ifrmRefundMessage.SetPatInfo(null);
                    ifrmRefundMessage.DtRefundPresc = new DataTable();
                    ifrmRefundMessage.DtRefundMedical = new DataTable();
                    MessageBoxShowSimple("保存退费消息成功");
                }
                catch (Exception err)
                {
                    MessageBoxShowError(err.Message);
                }
            }
        }

        /// <summary>
        /// 保存退费消息判断
        /// </summary>
        /// <returns>bool</returns>
        private bool SaveCheck()
        {            
            DataTable dtPresc = ifrmRefundMessage.DtRefundPresc;
            if (dtPresc.Rows.Count == 0)
            {
                return true;
            }

            decimal  allRefundAmount = 0;
            for (int rowindex = 0; rowindex < dtPresc.Rows.Count; rowindex++)
            {
                if (dtPresc.Rows[rowindex]["ItemName"].ToString().Trim() == "小   计")
                {
                    continue;
                }

                decimal oldminimun = Convert.ToDecimal(dtPresc.Rows[rowindex]["oldMiniNum"]);
                decimal oldpacknum = Convert.ToDecimal(dtPresc.Rows[rowindex]["oldPackNum"]);
                decimal oldamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["Amount"]) * Convert.ToDecimal(dtPresc.Rows[rowindex]["presamount"]); ;
                decimal refundminimun = Convert.ToDecimal(dtPresc.Rows[rowindex]["RefundMiniNum"]);
                if (refundminimun < 0)
                {
                    MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】退基本数量小于0");
                    return false;
                }

                decimal refundpacknum = Convert.ToDecimal(dtPresc.Rows[rowindex]["RefundPackNum"]);
                if (refundpacknum < 0)
                {
                    MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】退包装数量小于0");
                    return false;
                }

                decimal presamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["presamount"]);
                decimal refundpresamount = Convert.ToDecimal(dtPresc.Rows[rowindex]["refundpresamount"]);
                if (refundpresamount < 0)
                {
                    MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】退付数数量小于0");
                    return false;
                }

                decimal unitNO = Convert.ToDecimal(dtPresc.Rows[rowindex]["UnitNO"]);
                decimal refundamount = (refundpacknum * unitNO + refundminimun) * refundpresamount;
                allRefundAmount += refundamount;
                //if (refundamount > 0 && Convert.ToInt32(dtPresc.Rows[rowindex]["PresEmpID"]) != LoginUserInfo.EmpId)
                //{
                //    MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】不是您开的处方，不能由您退费");
                //    return false;
                //}
                if (refundamount > oldamount)
                {
                    MessageBoxShowError("【" + dtPresc.Rows[rowindex]["ItemName"] + "】退数量大于原数量");
                    return false;
                }                      
            }

            if (allRefundAmount == 0)
            {
                //所有退药数量为0，相当于没有退药，不允许保存
                MessageBoxShowError("所有退数量等于0，不允许保存");
                return false;
            }

            return true;
        }
        #endregion

        #region 退费消息查询
        /// <summary>
        /// 查询退费消息
        /// </summary>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <param name="querycondition">查询条件</param>
        [WinformMethod]
        public void QueryRefundMessage(DateTime bdate, DateTime edate, string querycondition)
        {
            try
            {
                ifrmRefundMessage.dtQueryRefundPresc = new DataTable();
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(bdate);
                    request.AddData(edate);
                    request.AddData(querycondition);
                    request.AddData(LoginUserInfo.EmpId);
                });
                ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RefundController", "QueryRefundMessage", requestAction);
                DataTable dt = retdata.GetData<DataTable>(0);
                ifrmRefundMessage.dtQueryRefundPresc = CaculateQueryTable(dt);
            }
            catch (Exception err)
            {
                MessageBoxShowError(err.Message);
            }
        }

        /// <summary>
        /// 计算退费金额
        /// </summary>
        /// <param name="dtPresc">网格数据</param>
        /// <returns>DataTable</returns>
        private DataTable CaculateQueryTable(DataTable dtPresc)
        {
            if (dtPresc.Rows.Count == 0)
            {
                return dtPresc;
            }

            for (int i = 1; i < dtPresc.Rows.Count; i++)
            {
                if (dtPresc.Rows[i]["invoiceNum"].ToString() == dtPresc.Rows[i - 1]["invoiceNum"].ToString())
                {
                    dtPresc.Rows[i]["DisPlayinvoiceNO"] = DBNull.Value;
                }
                else
                {
                    dtPresc.Rows[i]["DisPlayinvoiceNO"] = dtPresc.Rows[i]["invoiceNum"];
                }
            }

            for (int i = 0; i < dtPresc.Rows.Count; i++)
            {
                string packunit = dtPresc.Rows[i]["packunit"].ToString();
                string miniunit = dtPresc.Rows[i]["miniunit"].ToString();
                decimal unitNo = Convert.ToDecimal(dtPresc.Rows[i]["unitNO"]);
                if (dtPresc.Rows[i]["statid"].ToString() == "102" && Convert.ToInt32(dtPresc.Rows[i]["DistributeFlag"]) == 1)
                {
                    //中草药已经发药的不能退
                    dtPresc.Rows[i]["oldpackAmount"] = dtPresc.Rows[i]["presamount"] + "付";
                    dtPresc.Rows[i]["refundpackAmount"] = dtPresc.Rows[i]["refundpresamount"] + "付";
                }
                else
                {
                    if (Convert.ToInt32(dtPresc.Rows[i]["ItemType"]) == (int)OP_Enum.ItemType.收费项目 || Convert.ToInt32(dtPresc.Rows[i]["ItemType"]) == (int)OP_Enum.ItemType.组合项目)
                    {
                        dtPresc.Rows[i]["oldpackAmount"] = dtPresc.Rows[i]["Amount"] + miniunit;
                        dtPresc.Rows[i]["refundpackAmount"] = dtPresc.Rows[i]["refundAmount"] + miniunit;
                    }
                    else
                    {
                        decimal oldminiamount = Convert.ToDecimal(dtPresc.Rows[i]["Amount"]) % unitNo;
                        decimal oldpackamount = (Convert.ToDecimal(dtPresc.Rows[i]["Amount"]) - oldminiamount) / unitNo;
                        string stroldpackamount = string.Empty;
                        if (oldpackamount > 0)
                        {
                            stroldpackamount += oldpackamount + packunit;
                        }

                        if (oldminiamount > 0)
                        {
                            stroldpackamount += oldminiamount + miniunit;
                        }

                        if (stroldpackamount == string.Empty)
                        {
                            stroldpackamount = 0 + packunit;
                        }

                        dtPresc.Rows[i]["oldpackAmount"] = stroldpackamount;
                        decimal refundminiamount = Convert.ToDecimal(dtPresc.Rows[i]["refundAmount"]) % unitNo;
                        decimal refundpackamount = (Convert.ToDecimal(dtPresc.Rows[i]["refundAmount"]) - refundminiamount) / unitNo;
                        string strrefundpackamount = string.Empty;
                        if (refundpackamount > 0)
                        {
                            strrefundpackamount += refundpackamount + packunit;
                        }

                        if (refundminiamount > 0)
                        {
                            strrefundpackamount += refundminiamount + miniunit;
                        }

                        if (stroldpackamount == string.Empty)
                        {
                            strrefundpackamount = 0 + packunit;
                        }

                        if(strrefundpackamount==string.Empty)
                        {
                            strrefundpackamount = 0 + packunit;
                        }

                        dtPresc.Rows[i]["refundpackAmount"] = strrefundpackamount;
                    }
                }
            }

            DataTable dtCopy = dtPresc.Clone();
            dtCopy.Clear();
            dtCopy.Rows.Add(dtPresc.Rows[0].ItemArray);
            for (int i = 1; i < dtPresc.Rows.Count; i++)
            {
                if (Convert.ToInt32( dtPresc.Rows[i]["feeitemheadid"]) !=Convert.ToInt32( dtPresc.Rows[i - 1]["feeitemheadid"]))
                {
                    DataRow row = dtCopy.NewRow();
                    row["refundpackAmount"] = "小   计";                  
                    row["refundFee"] = dtPresc.Compute("sum(refundFee)", "feeitemheadid=" + dtPresc.Rows[i - 1]["feeitemheadid"]);
                    dtCopy.Rows.Add(row);
                }

                dtCopy.Rows.Add(dtPresc.Rows[i].ItemArray);
                if (i == dtPresc.Rows.Count - 1)
                {
                    DataRow row = dtCopy.NewRow();
                    row["refundpackAmount"] = "小   计";
                    row["refundFee"] = dtPresc.Compute("sum(refundFee)", "feeitemheadid=" + dtPresc.Rows[i]["feeitemheadid"]);
                    dtCopy.Rows.Add(row);
                }
            }  
                     
            return dtCopy;
        }

        /// <summary>
        /// 删除退费消息
        /// </summary>
        /// <param name="rowindex">行号</param>
        /// <returns>删除是否成功</returns>
        [WinformMethod]
        public bool DeleteRefundMessage(int rowindex)
        {
            DataTable dtPresc = ifrmRefundMessage.dtQueryRefundPresc;
            string invoiceNum = dtPresc.Rows[rowindex]["invoiceNum"].ToString();
            if (Convert.ToInt32(dtPresc.Rows[rowindex]["RefundPayFlag"]) == 1)
            {
                MessageBoxShowError("该票据号处方已经退费完成，不能删除");
                return false;
            }

            if (Convert.ToInt32(dtPresc.Rows[rowindex]["RefundDocID"]) !=LoginUserInfo.EmpId)
            {
                MessageBoxShowError("不能删除由其他医生发起的退费");
                return false;
            }

            if (MessageBoxShowYesNo("确实要删除票号为【" + invoiceNum + "】的退费消息吗") == DialogResult.Yes)
            {
                try
                {
                    Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                    {
                        request.AddData(invoiceNum);
                        request.AddData(LoginUserInfo.EmpId);
                    });
                    ServiceResponseData retdata = InvokeWcfService("OPProject.Service", "RefundController", "DeleteRefundMessage", requestAction);
                    MessageBoxShowSimple("删除成功");
                    return true;
                }
                catch (Exception err)
                {
                    MessageBoxShowError(err.Message);
                    return false;
                }                
            }

            return false;
        }
        #endregion

        /// <summary>
        /// 消息提示
        /// </summary>
        /// <param name="message">需要提示的消息</param>
        [WinformMethod]
        public void ShowMess(string message)
        {
            MessageBoxShowSimple(message);
        }

        /// <summary>
        /// 门诊医生站调用退费消息界面退费
        /// </summary>
        /// <param name="invoiceNO">退费票据号</param>
        [WinformMethod]
        public void ShowRefundMessage(string  invoiceNO)
        {
            ifrmRefundMessage = (IFrmRefundMessage)iBaseView["FrmRefundMessage"];          
            Form form = ifrmRefundMessage as Form;
            string tabName = form.Text;
            string tabId = "view" + form.GetHashCode();
            InvokeController("MainFrame.UI", "wcfclientLoginController", "ShowForm", form, tabName, tabId);
            QueryPresByInvoiceNO(invoiceNO);
        }
    }
}
