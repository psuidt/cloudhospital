using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Account;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_DrugManger.ObjectModel.Store;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 报表统计控制器
    /// </summary>
    [WCFController]
    public class ReportController : WcfServerController
    {
        /// <summary>
        /// 取得药品类型典
        /// </summary>
        /// <returns>药品类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugTypeDic()
        {
            DataTable dtTypeDic = NewObject<DG_TypeDic>().gettable("TypeID<=3");
            responseData.AddData(dtTypeDic);
            return responseData;
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadDrugStorage()
        {
            var systemType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            IStore iProcess = NewObject<StoreFactory>().GetStoreProcess(systemType);
            DataTable dtRtn = iProcess.LoadDrugStorages(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptRoomData()
        {
            int menuTypeFlag = 0;
            string belongSys = requestData.GetData<string>(0);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                menuTypeFlag = 1;
            }
            else
            {
                //药房
                menuTypeFlag = 0;
            }

            DataTable dt = NewObject<RelateDeptMgr>().GetStoreRoomData(menuTypeFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药品子类型
        /// </summary>
        /// <returns>药品子类型</returns>
        [WCFMethod]
        public ServiceResponseData GetChildDrugType()
        {
            var pruduct = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugTypeMgr>().GetChildDrugType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns>供应商</returns>
        [WCFMethod]
        public ServiceResponseData GetSupportDic()
        {
            DataTable dt = NewDao<IDGDao>().GetSupplyForShowCard();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取来往科室
        /// </summary>
        /// <returns>来往科室</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptRelation()
        {
            string deptid = requestData.GetData<string>(1);
            DataTable dt = NewDao<IDGDao>().GetRelateDeptData(Convert.ToInt32(deptid));
            responseData.AddData(dt);
            return responseData;
        }

        #region 药品汇总表
        /// <summary>
        /// 获取药房月结信息
        /// </summary>
        /// <returns>药房月结信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDSBalance()
        {
            int dept = 0;
            string deptid = requestData.GetData<string>(0);
            if (int.TryParse(deptid, out dept))
            {
            }

            DataTable dt = NewDao<IDSDao>().GetMonthBalaceByDept(dept);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药库月结信息
        /// </summary>
        /// <returns>药库月结信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDWBalance()
        {
            int dept = 0;
            string deptid = requestData.GetData<string>(0);
            if (int.TryParse(deptid, out dept))
            {
            }

            DataTable dt = NewDao<IDWDao>().GetMonthBalaceByDept(dept);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <returns>入库查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStore()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmTotalAccountDS")
            {
                dt = NewDao<IDSDao>().GetInStore(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetInStore(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <returns>出库查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetOutStore()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmTotalAccountDS")
            {
                dt = NewDao<IDSDao>().GetOutStore(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetOutStore(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <returns>盘点查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCheck()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmTotalAccountDS")
            {
                dt = NewDao<IDSDao>().GetCheck(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetCheck(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <returns>调价查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetAdjPrice()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmTotalAccountDS")
            {
                dt = NewDao<IDSDao>().GetAdjPrice(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetAdjPrice(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <returns>住院发药信息</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDisp()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDSDao>().GetIPDisp(query);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取门诊发药信息
        /// </summary>
        /// <returns>门诊发药信息</returns>
        [WCFMethod]
        public ServiceResponseData GetOPDisp()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDSDao>().GetOPDisp(query);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 药品流水账
        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <returns>入库查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStores()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmFlowAccountDS")
            {
                dt = NewDao<IDSDao>().GetInStores(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetInStores(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取出库查询信息
        /// </summary>
        /// <returns>出库查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetOutStores()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmFlowAccountDS")
            {
                dt = NewDao<IDSDao>().GetOutStores(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetOutStores(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取盘点查询信息
        /// </summary>
        /// <returns>盘点查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetChecks()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmFlowAccountDS")
            {
                dt = NewDao<IDSDao>().GetChecks(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetChecks(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取调价查询信息
        /// </summary>
        /// <returns>调价查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetAdjPrices()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            DataTable dt = new DataTable();
            if (frmName == "FrmFlowAccountDS")
            {
                dt = NewDao<IDSDao>().GetAdjPrices(query);
            }
            else
            {
                dt = NewDao<IDWDao>().GetAdjPrices(query);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取住院发药信息
        /// </summary>
        /// <returns>住院发药信息</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDisps()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDSDao>().GetIPDisps(query);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取门诊发药信息
        /// </summary>
        /// <returns>门诊发药信息</returns>
        [WCFMethod]
        public ServiceResponseData GetOPDisps()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<IDSDao>().GetOPDisps(query);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 发药统计
        /// <summary>
        /// 获取发药统计
        /// </summary>
        /// <returns>发药统计</returns>
        [WCFMethod]
        public ServiceResponseData GetDispTotal()
        {
            var systemType = requestData.GetData<string>(0);
            var queryCondition = requestData.GetData<Dictionary<string, string>>(1);
            DataTable dt = new DataTable();
            if (systemType == "0")
            {
                dt = NewDao<IDSDao>().GetIPDispTotal(queryCondition);
            }
            else
            {
                dt = NewDao<IDSDao>().GetOPDispTotal(queryCondition);
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取发药统计详细
        /// </summary>
        /// <returns>发药统计详细</returns>
        [WCFMethod]
        public ServiceResponseData GetDispDetail()
        {
            var systemType = requestData.GetData<string>(0);
            var deptId = requestData.GetData<string>(1);
            var drugID = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            if (systemType == "0")
            {
                dt = NewDao<IDSDao>().GetIPDispDetatil(deptId, drugID);
            }
            else
            {
                dt = NewDao<IDSDao>().GetOPDispDetatil(deptId, drugID);
            }

            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 进销存统计
        /// <summary>
        /// 取得会计年
        /// </summary>
        /// <returns>会计年</returns>
        [WCFMethod]
        public ServiceResponseData GetAcountYears()
        {
            DataTable dtYear = new DataTable();
            string belongSys = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                dtYear = NewDao<IDWDao>().GetAcountYears(deptId);
            }
            else
            {
                dtYear = NewDao<IDSDao>().GetAcountYears(deptId);
            }

            responseData.AddData(dtYear);
            return responseData;
        }

        /// <summary>
        /// 取得会计月
        /// </summary>
        /// <returns>会计月</returns>
        [WCFMethod]
        public ServiceResponseData GetAcountMonths()
        {
            DataTable dtMonth = new DataTable();
            string belongSys = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int year = requestData.GetData<int>(2);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                dtMonth = NewDao<IDWDao>().GetAcountMonths(deptId, year);
            }
            else
            {
                dtMonth = NewDao<IDSDao>().GetAcountMonths(deptId, year);
            }

            responseData.AddData(dtMonth);
            return responseData;
        }

        /// <summary>
        /// 查询进销存帐
        /// </summary>
        /// <returns>进销存帐</returns>
        [WCFMethod]
        public ServiceResponseData InventoryStatistic()
        {
            DataTable dt = new DataTable();
            string belongSys = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int year = requestData.GetData<int>(2);
            int month = requestData.GetData<int>(3);
            int typeId = requestData.GetData<int>(4);
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                //药库
                dt = NewObject<DWAccountQuery>().CSPJReport(deptId, year, month, typeId);
            }
            else 
            {
                //药房
                dt = NewObject<DSAccountQuery>().CSPJReport(deptId, year, month, typeId);
            }
            
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 药品明细查询
        /// <summary>
        /// 获取药品信息
        /// </summary>
        /// <returns>药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            DataTable dtRtn = new DataTable();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetDrugDicForInStoreShowCard(false, 0);
            }
            else
            {
                dtRtn = NewDao<IDSDao>().GetDrugDicForInStoreShowCard(false, 0);
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得会计月开始结束日期
        /// </summary>      
        /// <returns>会计月开始结束日期</returns>
        [WCFMethod]
        public ServiceResponseData GetBalanceDate()
        {
            string belongSys = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int year = requestData.GetData<int>(2);
            int month = requestData.GetData<int>(3);
            DataTable dtRtn = new DataTable();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetBalanceDate(deptId, year, month);
            }
            else
            {
                dtRtn = NewDao<IDSDao>().GetBalanceDate(deptId, year, month);
            }

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得药品明细账数据
        /// </summary>
        /// <returns>药品明细账数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountDetail()
        {
            string belongSys = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int year = requestData.GetData<int>(2);
            int month = requestData.GetData<int>(3);
            string beginTime= requestData.GetData<string>(4);
            string endTime = requestData.GetData<string>(5);
            int drugId = requestData.GetData<int>(6);
            int queryType = requestData.GetData<int>(7);
            DataTable dtRtn = new DataTable();
            DataTable dtTotalRtn = new DataTable();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                dtRtn = NewDao<IDWDao>().GetAccountDetail(deptId,year,  month,  beginTime,  endTime,  drugId, queryType);
                dtTotalRtn=NewDao<IDWDao>().GetAccountDetailSum(deptId, year, month, beginTime, endTime, drugId, queryType);
            }
            else
            {
                dtRtn = NewDao<IDSDao>().GetAccountDetail(deptId, year, month, beginTime, endTime, drugId, queryType);
                dtTotalRtn = NewDao<IDSDao>().GetAccountDetailSum(deptId, year, month, beginTime, endTime, drugId, queryType);
            }

            if (dtRtn.Rows.Count > 0)
            {
                decimal overAmount = 0;
                decimal lendAmount = 0;
                decimal debitAmount =0;
                decimal overRetailFee = 0;
                decimal lendRetailFee = 0;
                decimal debitRetailFee = 0;
                decimal[] results = GetOverAmount(dtRtn);
                overAmount= results[0];
                overRetailFee= results[1];
                lendAmount = Convert.ToDecimal(dtTotalRtn.Rows[0]["LendAmount"]);
                debitAmount = Convert.ToDecimal(dtTotalRtn.Rows[0]["DebitAmount"]);
                lendRetailFee= Convert.ToDecimal(dtTotalRtn.Rows[0]["LendRetailFee"]);
                debitRetailFee = Convert.ToDecimal(dtTotalRtn.Rows[0]["DebitRetailFee"]);
                dtTotalRtn.Rows[0]["OverAmount"] = results[0];
                dtTotalRtn.Rows[0]["OverRetailFee"] = results[1];
                dtTotalRtn.Rows[0]["OpenAmount"]= overAmount - lendAmount + debitAmount;
                dtTotalRtn.Rows[0]["OpenRetailFee"] = overRetailFee - lendRetailFee + debitRetailFee;              
            }

            responseData.AddData(dtRtn);
            responseData.AddData(dtTotalRtn);
            return responseData;
        }

        /// <summary>
        /// 取得期初余额
        /// </summary>
        /// <param name="dtRtn">明细表</param>
        /// <returns>期初余额</returns>
        private decimal[] GetOverAmount(DataTable dtRtn)
        {
            decimal[] results = new decimal[2] { 0,0};
            DataView dataView = new DataView(dtRtn);
            DataTable dtBatch = dataView.ToTable(true, new string[] { "DrugID", "BatchNO" });
            foreach (DataRow row in dtBatch.Rows)
            {
                string batchNo = row["BatchNO"].ToString();
                DataView dv = dtRtn.DefaultView;
                dv.RowFilter = "BatchNO='" + batchNo + "'";
                dv.Sort = "AccountID desc";
                results[0] = results[0] + Convert.ToDecimal(dv[0]["OverAmount"]);
                results[1] = results[1] + Convert.ToDecimal(dv[0]["OverRetailFee"]);
            }

            return results;
        }

        /// <summary>
        /// 获取单据表头信息
        /// </summary>
        /// <returns>单据表头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetBillHeadInfo()
        {
            string belongSys = requestData.GetData<string>(0);
            string opType = requestData.GetData<string>(1);
            int deptId = requestData.GetData<int>(2);
            int detailID = requestData.GetData<int>(3);
            BillMasterShower billRtn = new BillMasterShower();
            if (belongSys == DGConstant.OP_DW_SYSTEM)
            {
                billRtn = NewObject<DWAccountQuery>().GetBillHeadInfo(opType, deptId, detailID);
            }
            else
            {
                billRtn = NewObject<DSAccountQuery>().GetBillHeadInfo(opType, deptId, detailID);
            }

            responseData.AddData(billRtn);
            return responseData;
        }
        #endregion

        #region 采购入库对比报表
        /// <summary>
        /// 获取采购入库对比数据
        /// </summary>
        /// <returns>采购入库对比数据</returns>
        [WCFMethod]
        public ServiceResponseData GetBuyComparison()
        {
            int deptId = requestData.GetData<int>(0);
            string yearMonth = requestData.GetData<string>(1);
            string drugName = requestData.GetData<string>(2);
            DataTable dt = NewDao<IDWDao>().GetBuyComparison(deptId, yearMonth, drugName);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 新药入库统计
        /// <summary>
        /// 查询新药入库统计
        /// </summary>
        /// <returns>新药入库统计</returns>
        [WCFMethod]
        public ServiceResponseData GetNewDrug()
        {
            int deptId = requestData.GetData<int>(0);
            string yearMonth = requestData.GetData<string>(1);
            string drugName = requestData.GetData<string>(2);
            DataTable dt = NewDao<IDWDao>().GetNewDrug(deptId, yearMonth, drugName);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
