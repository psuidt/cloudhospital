using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.BasicData;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 统计报表查询控制器类
    /// </summary>
    [WCFController]
    public class QueryController : WcfServerController
    {
        #region 处方查询
        /// <summary>
        /// 门诊处方查询界面数据获取
        /// </summary>
        /// <returns>ResponseData</returns>
        [WCFMethod]
        public ServiceResponseData RecipterInit()
        {           
            //获取收费员列表
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();//收费员
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员, false);
            responseData.AddData(dt);

            DataTable dtPattype = basicmanagement.GetPatType(false);
            responseData.AddData(dtPattype);//病人类型

            DataTable dtPayMent= NewObject<Basic_Payment>().gettable(" delflag=0");//支付方式
            responseData.AddData(dtPayMent);
            return responseData;
        }

        /// <summary>
        /// 处方查询
        /// </summary>
        /// <returns>返回处方信息</returns>
        [WCFMethod]
        public ServiceResponseData ReciptQuery()
        {
            try
            {
                Dictionary<string, object> queryDictionary = requestData.GetData<Dictionary<string, object>>(0);
                DataTable dtData = NewDao<IOPManageDao>().ReciptQuery(queryDictionary);               
                responseData.AddData(dtData);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 处方明细查询
        /// </summary>
        /// <returns>返回处方明细</returns>
        [WCFMethod]
        public ServiceResponseData QueryReciptDetail()
        {
            string feeItemHeadIDs = requestData.GetData<string>(0);
            DataTable dtDetail = NewObject<OP_FeeItemDetail>().gettable(" feeItemHeadID in (+" + feeItemHeadIDs + ")");
            DataColumn col = new DataColumn();
            col.ColumnName = "PackNum";
            dtDetail.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "MiniNum";
            dtDetail.Columns.Add(col);
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                decimal unitno = Convert.ToDecimal(dtDetail.Rows[i]["UnitNO"]);
                decimal amount = Convert.ToDecimal(dtDetail.Rows[i]["Amount"]);
                int itemtype = Convert.ToInt32(dtDetail.Rows[i]["ItemType"]);
                if (itemtype == (int)OP_Enum.ItemType.药品)
                {
                    dtDetail.Rows[i]["MiniNum"] = amount % unitno;
                    dtDetail.Rows[i]["PackNum"] = (amount - Convert.ToInt32(dtDetail.Rows[i]["MiniNum"])) / unitno;
                }
                else
                {
                    dtDetail.Rows[i]["MiniNum"] = amount;
                    dtDetail.Rows[i]["PackNum"] = (amount - Convert.ToInt32(dtDetail.Rows[i]["MiniNum"])) / unitno;
                }
            }

            responseData.AddData(dtDetail);
            return responseData;
        }
        #endregion

        #region 门诊病人费用查询
        /// <summary>
        /// 获取界面基础数据
        /// </summary>
        /// <returns>ResponseData</returns>
        [WCFMethod]
        public ServiceResponseData OpCostSearchDataInit()
        {
            //获取收费员列表
            DataTable dt = new DataTable();
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();//收费员
            dt = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.收费员,true);
            responseData.AddData(dt);

            DataTable dtPattype = basicmanagement.GetPatType(false);
            responseData.AddData(dtPattype);//病人类型

            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.门诊临床科室, true);
            responseData.AddData(dtDept);//科室
            DataTable dtDoctor = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.EmpDataSourceType.医生, true);
            responseData.AddData(dtDoctor);//医生
            return responseData;
        }

        /// <summary>
        /// 门诊病人费用查询
        /// </summary>
        /// <returns>返回支付信息和项目分类信息</returns>
        [WCFMethod]
        public ServiceResponseData OpCostSearchQuery()
        {
            Dictionary<string, object> queryDictionary = requestData.GetData<Dictionary<string, object>>(0);
            DataTable dtPayMentData = NewDao<IOPManageDao>().GetOPCostPayMentQuery(queryDictionary);
            DataTable dtPayMent = NewObject<Basic_Payment>().gettable(" delflag=0");
            DataTable dt = DtTrans(dtPayMentData,"CostHeadID",18, dtPayMent, "PayName", "payMentName", "PayMentMoney");            
            responseData.AddData(dt);//支付明细

            DataTable dtItemData = NewDao<IOPManageDao>().GetOPCostFpItemQuery(queryDictionary);
            DataTable dtItems = NewObject<Basic_StatItemSubclass>().gettable(" subtype=5 and delflag=0");  
            DataTable dtItem= DtTrans(dtItemData, "CostHeadID", 18, dtItems, "SubName", "FpItemName", "ItemFee");          
            responseData.AddData(dtItem);//项目明细
            return responseData;
        }        

        /// <summary>
        /// dataTable行转列
        /// </summary>
        /// <param name="dtOld">原来DataTable</param>
        /// <param name="pxzd">第一行需比较的字段</param>
        /// <param name="notChangeColumns">不需要改变的字段数</param>
        /// <param name="dtAddColumns">需增加的列数据datatble</param>
        /// <param name="dtaddColumnName">取增的列数据原的列名</param>
        /// <param name="addColumnName">需比较的列名</param>
        /// <param name="addColumnValue">赋值的列名的值</param>
        /// <returns>DataTable</returns>
        private DataTable DtTrans(DataTable dtOld,string pxzd,int notChangeColumns,DataTable dtAddColumns,string dtaddColumnName,string addColumnName,string addColumnValue)
        {
            DataTable dtNew = dtOld.Clone();
            dtNew.Rows.Clear();
            dtNew.Columns.RemoveAt(19);
            dtNew.Columns.RemoveAt(18);            
            for (int i = 0; i < dtAddColumns.Rows.Count; i++)
            {
                DataColumn col = new DataColumn();
                col.ColumnName = dtAddColumns.Rows[i][dtaddColumnName].ToString();
                col.DataType = typeof(decimal);
                dtNew.Columns.Add(col);
            }

            int j;          
            if (dtOld.Rows.Count > 0)
            {
                for (int i = 0; i < dtOld.Rows.Count; i++)
                {
                    DataRow row = dtNew.NewRow();

                    for (int col = 0; col < notChangeColumns; col++)
                    {
                        row[col] = dtOld.Rows[i][col];
                    }

                    for (int k = notChangeColumns; k < dtNew.Columns.Count; k++)
                    {
                        if (dtOld.Rows[i][addColumnName].ToString().Trim() == dtNew.Columns[k].ColumnName.ToString().Trim())
                        {
                            row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(Convert.ToDecimal(dtOld.Rows[i][addColumnValue]).ToString("0.00"));
                            break;
                        }
                    }

                    for (j = i + 1; j < dtOld.Rows.Count; j++)
                    {
                        if (dtOld.Rows[j][pxzd].ToString().Trim() == dtOld.Rows[i][pxzd].ToString().Trim())
                        {
                            for (int k = notChangeColumns; k < dtNew.Columns.Count; k++)
                            {
                                if (dtOld.Rows[j][addColumnName].ToString().Trim() == dtNew.Columns[k].ColumnName.ToString().Trim())
                                {
                                    // row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(dtOld.Rows[j][addColumnValue]);
                                    row[dtNew.Columns[k].ColumnName] = Convert.ToDecimal(row[dtNew.Columns[k].ColumnName] == DBNull.Value ? 0 : row[dtNew.Columns[k].ColumnName]) + Convert.ToDecimal(dtOld.Rows[j][addColumnValue]);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    i = j - 1;
                    dtNew.Rows.Add(row);
                }

                if (dtNew.Rows.Count > 0)
                {
                    DataRow dr = dtNew.NewRow();
                    dr[1] = "合计";
                    for (int col = 16; col < dtNew.Columns.Count; col++)
                    {
                        dr[col] = dtNew.Compute("sum(" + dtNew.Columns[col].ColumnName + ")", string.Empty);
                    }

                    dtNew.Rows.Add(dr);                   
                    int count = dtNew.Columns.Count - 1;
                    for (int col = count; col >= 18; col--)
                    {
                        DataRow[] rows = dtNew.Select(string.Empty + dtNew.Columns[col].ColumnName + ">"+0+ " or " + dtNew.Columns[col].ColumnName + "<" + 0 + string.Empty);
                        if (rows.Length ==0)
                        {
                            dtNew.Columns.RemoveAt(col);
                        }
                    }
                }
            }

            return dtNew;
        }

        /// <summary>
        /// 根据costheadid获取费用明细
        /// </summary>
        /// <returns>返回费用明细信息</returns>
        [WCFMethod]
        public ServiceResponseData CostSearchDetail()
        {
            int costHeadid = requestData.GetData<int>(0);
            DataTable dtDetail = NewDao<IOPManageDao>().GetCostDetail(costHeadid);  
            DataColumn col = new DataColumn();
            col.ColumnName = "PackNum";
            dtDetail.Columns.Add(col);
            col = new DataColumn();
            col.ColumnName = "MiniNum";
            dtDetail.Columns.Add(col);
            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                decimal unitno = Convert.ToDecimal(dtDetail.Rows[i]["UnitNO"]);
                decimal amount = Convert.ToDecimal(dtDetail.Rows[i]["Amount"]);
                int itemtype = Convert.ToInt32(dtDetail.Rows[i]["ItemType"]);
                if (itemtype == (int)OP_Enum.ItemType.药品)
                {
                    dtDetail.Rows[i]["MiniNum"] = amount % unitno;
                    dtDetail.Rows[i]["PackNum"] = (amount - Convert.ToInt32(dtDetail.Rows[i]["MiniNum"])) / unitno;
                }
                else
                {
                    dtDetail.Rows[i]["MiniNum"] = amount;
                    dtDetail.Rows[i]["PackNum"] = (amount - Convert.ToInt32(dtDetail.Rows[i]["MiniNum"])) / unitno;
                }
            }

            responseData.AddData(dtDetail);
            return responseData;
        }
        #endregion

        /// <summary>
        /// 发票补打
        /// </summary>
        /// <returns>返回票据补打信息</returns>
        [WCFMethod]
        public ServiceResponseData PrintInvoiceAgain()
        {
            int costHeadID = requestData.GetData<int>(0);
            string perfChar = requestData.GetData<string>(1);
            string invoice = requestData.GetData<string>(2);
            string invoiceNO = perfChar + invoice;
            List<OP_FeeItemHead> feeItems = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" invoiceNO='" + invoiceNO + "'");
            if (feeItems != null && feeItems.Count > 0)
            {
                throw new Exception("您输入的票据号已经被使用过，请重新输入");
            }

            OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costHeadID) as OP_CostHead;
            if (costHead.RecipeFlag == 1)
            {
                throw new Exception("此结算记录票据已经补打过，不能再次补打");
            }

            //获取发票打印信息
            DataTable dtInvoice = NewDao<IOPManageDao>().GetBalancePrintInvoiceDt(costHeadID);
            DataTable dtInvoiceDetail = NewDao<IOPManageDao>().GetBalancePrintDetailDt(costHeadID);
            DataTable dtInvoiceStatDetail = NewDao<IOPManageDao>().GetBalancePrintStatDt(costHeadID);
            ChargeInfo chargeInfo = new ChargeInfo();
            List<OP_CostPayMentInfo> payInfoList = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>("costHeadId="+costHeadID+string.Empty);
            chargeInfo.PayInfoList =payInfoList;                     
            responseData.AddData(dtInvoice);
            responseData.AddData(dtInvoiceDetail);
            responseData.AddData(dtInvoiceStatDetail);
            responseData.AddData(chargeInfo);
            return responseData;
        }

        /// <summary>
        /// 获取发票打印信息
        /// </summary>
        /// <returns>返回发票打印信息</returns>
        [WCFMethod]
        public ServiceResponseData PrintPrescAgain()
        {
            int costHeadID = requestData.GetData<int>(0);  
                   
            //获取发票打印信息
            DataTable dtInvoice = NewDao<IOPManageDao>().GetBalancePrintInvoiceDt(costHeadID);
            DataTable dtInvoiceDetail = NewDao<IOPManageDao>().GetBalancePrintDetailDt(costHeadID);
            DataTable dtInvoiceStatDetail = NewDao<IOPManageDao>().GetBalancePrintStatDt(costHeadID);
            ChargeInfo chargeInfo = new ChargeInfo();
            List<OP_CostPayMentInfo> payInfoList = NewObject<OP_CostPayMentInfo>().getlist<OP_CostPayMentInfo>("costHeadId=" + costHeadID + string.Empty);
            chargeInfo.PayInfoList = payInfoList;
            responseData.AddData(dtInvoice);
            responseData.AddData(dtInvoiceDetail);
            responseData.AddData(dtInvoiceStatDetail);
            responseData.AddData(chargeInfo);
            return responseData;
        }

        /// <summary>
        /// 补打门诊票据保存
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData PrintInvoiceAgainSave()
        {
            try
            {
                int costHeadID = requestData.GetData<int>(0);
                string newInvoiceNO = requestData.GetData<string>(1);
                int operatoreid = requestData.GetData<int>(2);         
                List<OP_FeeItemHead> feeItems = NewObject<OP_FeeItemHead>().getlist<OP_FeeItemHead>(" costHeadID="+costHeadID+string.Empty);
                DateTime printData = DateTime.Now;
                foreach (OP_FeeItemHead feeItemHead in feeItems)
                {
                    OP_PrintInvoiceInfo printInvoice = new OP_PrintInvoiceInfo();
                    printInvoice.FeeItemHeadID = feeItemHead.FeeItemHeadID;
                    printInvoice.OldInvoiceNumber = feeItemHead.InvoiceNO;
                    printInvoice.NewInvoiceNumber = newInvoiceNO;
                    printInvoice.PrintDate = printData;
                    printInvoice.PrintEmpID = operatoreid;
                    printInvoice.PrintType = 0;
                    printInvoice.InvoiceFee = feeItemHead.TotalFee;
                    printInvoice.PatListID = feeItemHead.PatListID;
                    printInvoice.PatName = feeItemHead.PatName;
                    printInvoice.CostHeadID = feeItemHead.CostHeadID;
                    this.BindDb(printInvoice);
                    printInvoice.save();

                    feeItemHead.InvoiceNO = newInvoiceNO;
                    feeItemHead.ReciptNO = newInvoiceNO;
                    this.BindDb(feeItemHead);
                    feeItemHead.save();
                }

                OP_CostHead costHead = NewObject<OP_CostHead>().getmodel(costHeadID) as OP_CostHead;
                costHead.RecipeFlag = 1;
                this.BindDb(costHead);
                costHead.save();               
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}