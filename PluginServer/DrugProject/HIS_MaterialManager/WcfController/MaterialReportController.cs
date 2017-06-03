using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.DrugManage;
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.Account;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 报表统计控制器
    /// </summary>
    [WCFController]
    public class MaterialReportController : WcfServerController
    {
        #region 进销存统计
        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialTypeDic()
        {
            DataTable dtTypeDic = NewObject<DG_TypeDic>().gettable("TypeID>3");
            responseData.AddData(dtTypeDic);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetStoreRoomData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得会计年
        /// </summary>
        /// <returns>会计年数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAcountYears()
        {
            DataTable dtYear = new DataTable();
            int deptId = requestData.GetData<int>(0);
            dtYear = NewDao<IMWDao>().GetAcountYears(deptId);
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
            int deptId = requestData.GetData<int>(0);
            int year = requestData.GetData<int>(1);

            dtMonth = NewDao<IMWDao>().GetAcountMonths(deptId, year);

            responseData.AddData(dtMonth);
            return responseData;
        }

        /// <summary>
        /// 进销存统计
        /// </summary>
        /// <returns>统计数据</returns>
        [WCFMethod]
        public ServiceResponseData InventoryStatistic()
        {
            DataTable dt = new DataTable();
            int deptId = requestData.GetData<int>(0);
            int year = requestData.GetData<int>(1);
            int month = requestData.GetData<int>(2);
            int typeId = requestData.GetData<int>(3);
            dt = NewObject<MWAccountQuery>().CSPJReport(deptId, year, month, typeId);

            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 物资明细查询
        /// <summary>
        /// 获取物资信息
        /// </summary>
        /// <returns>物资信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialInfo()
        {
            DataTable dtRtn = new DataTable();
            dtRtn = NewDao<IMWDao>().GetMaterialDicShowCard(false, 0);

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
            int deptId = requestData.GetData<int>(0);
            int year = requestData.GetData<int>(1);
            int month = requestData.GetData<int>(2);
            DataTable dtRtn = new DataTable();
            dtRtn = NewDao<IMWDao>().GetBalanceDate(deptId, year, month);

            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得物资明细账数据
        /// </summary>
        /// <returns>物资明细账数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAccountDetail()
        {
            int deptId = requestData.GetData<int>(0);
            int year = requestData.GetData<int>(1);
            int month = requestData.GetData<int>(2);
            string beginTime = requestData.GetData<string>(3);
            string endTime = requestData.GetData<string>(4);
            int drugId = requestData.GetData<int>(5);
            int queryType = requestData.GetData<int>(6);
            DataTable dtRtn = new DataTable();
            DataTable dtTotalRtn = new DataTable();

            dtRtn = NewDao<IMWDao>().GetAccountDetail(deptId, year, month, beginTime, endTime, drugId, queryType);
            dtTotalRtn = NewDao<IMWDao>().GetAccountDetailSum(deptId, year, month, beginTime, endTime, drugId, queryType);
            if (dtRtn.Rows.Count > 0)
            {
                decimal overAmount = 0;
                decimal lendAmount = 0;
                decimal debitAmount = 0;
                decimal overRetailFee = 0;
                decimal lendRetailFee = 0;
                decimal debitRetailFee = 0;
                decimal[] results = GetOverAmount(dtRtn);
                overAmount = results[0];
                overRetailFee = results[1];
                lendAmount = Convert.ToDecimal(dtTotalRtn.Rows[0]["LendAmount"]);
                debitAmount = Convert.ToDecimal(dtTotalRtn.Rows[0]["DebitAmount"]);
                lendRetailFee = Convert.ToDecimal(dtTotalRtn.Rows[0]["LendRetailFee"]);
                debitRetailFee = Convert.ToDecimal(dtTotalRtn.Rows[0]["DebitRetailFee"]);
                dtTotalRtn.Rows[0]["OverAmount"] = results[0];
                dtTotalRtn.Rows[0]["OverRetailFee"] = results[1];
                dtTotalRtn.Rows[0]["OpenAmount"] = overAmount - lendAmount + debitAmount;
                dtTotalRtn.Rows[0]["OpenRetailFee"] = overRetailFee - lendRetailFee + debitAmount;
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
            decimal[] results = new decimal[2] { 0, 0 };
            DataView dataView = new DataView(dtRtn);
            DataTable dtBatch = dataView.ToTable(true, new string[] { "MaterialID", "BatchNO" });
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
        /// 获取单据头信息
        /// </summary>
        /// <returns>单据头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetBillHeadInfo()
        {
            string opType = requestData.GetData<string>(0);
            int deptId = requestData.GetData<int>(1);
            int detailID = requestData.GetData<int>(2);
            BillMasterShower billRtn = new BillMasterShower();
            billRtn = NewObject<MWAccountQuery>().GetBillHeadInfo(opType, deptId, detailID);
            responseData.AddData(billRtn);
            return responseData;
        }
        #endregion

        #region 查询物资库存信息
        /// <summary>
        /// 查询物资库存信息
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadMaterialStorage()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn = NewDao<IMWDao>().LoadMaterialStorage(queryCondition, string.Empty); 
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 查询物资库存信息（带批次）
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadMaterialStorages()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            var typeId= requestData.GetData<string>(1);
            DataTable dtRtn = NewDao<IMWDao>().LoadMaterialStorages(queryCondition, typeId); 
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 取得物资类型典
        /// </summary>
        /// <returns>物资类型典</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialTypeDics()
        {
            DataTable dtTypeDic = NewObject<IMWDao>().GetMaterialTypeShowCard();
            responseData.AddData(dtTypeDic);
            return responseData;
        }
        #endregion

        #region 物资汇总
        /// <summary>
        /// 获取物资月结信息
        /// </summary>
        /// <returns>月结信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMWBalance()
        {
            int dept = 0;
            string deptid = requestData.GetData<string>(0);
            if (int.TryParse(deptid, out dept))
            {
            }

            DataTable dt = NewDao<IMWDao>().GetMonthBalaceByDept(dept);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得库房数据
        /// </summary>
        /// <returns>库房数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptRoomData()
        {
            DataTable dt = NewObject<MaterialDeptMgr>().GetMaterialDept();
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
            DataTable dt = NewDao<IMWDao>().GetSupplyForShowCard();
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
            DataTable dt = NewDao<IMWDao>().GetRelateDeptData(Convert.ToInt32(deptid));
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetInStore(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetOutStore(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetCheck(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetAdjPrice(query, typeId);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 物资流水账
        /// <summary>
        /// 获取入库查询信息
        /// </summary>
        /// <returns>入库查询信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInStores()
        {
            var query = requestData.GetData<Dictionary<string, string>>(0);
            var frmName = requestData.GetData<string>(1);
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetInStores(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetOutStores(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetChecks(query, typeId);
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
            var typeId = requestData.GetData<string>(2);
            DataTable dt = new DataTable();
            dt = NewDao<IMWDao>().GetAdjPrices(query, typeId);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
