using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;
using HIS_Entity.OPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 门诊发药控制器
    /// </summary>
    [WCFController]
    public class OpDispController : WcfServerController
    {
        /// <summary>
        /// 获取药房数据
        /// </summary>
        /// <returns>药房数据集</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugStoreData()
        {
            DataTable dt = NewObject<DG_DeptDic>().gettable("DeptType=0 AND StopFlag=0");
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 药房公共接口
        /// </summary>     
        /// <returns>结算数据集</returns>     
        [WCFMethod]
        public ServiceResponseData QueryPatientInfo()
        {
            var code = requestData.GetData<string>(0);//发票号或会员卡号
            var execDeptId = requestData.GetData<int>(1);//执行科室Id
            var distributeFlag = requestData.GetData<int>(2);
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt =  model.QueryPatientInfo(code, execDeptId,distributeFlag);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取收费主表
        /// </summary>
        /// <returns>收费头表数据集</returns>
        [WCFMethod]
        public ServiceResponseData GetFeeItemHead()
        {
            var invoiceNO = requestData.GetData<string>(0);
            var deptId = requestData.GetData<int>(1);
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt = model.GetFeeItemHead(invoiceNO, deptId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取费用明细
        /// </summary>
        /// <returns>费用明细</returns>
        [WCFMethod]
        public ServiceResponseData GetFeeItemDetail()
        {
            var feeItemHeadId = requestData.GetData<int>(0);//费用主表Id
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt = model.GetFeeItemDetail(feeItemHeadId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取执行单信息
        /// </summary>
        /// <returns>执行单信息</returns>
        [WCFMethod]
        public ServiceResponseData GetExecuteBills()
        {
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt = model.GetExecuteBills();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药房发药数据
        /// </summary>
        /// <returns>药房发药数据</returns>
        [WCFMethod]
        public ServiceResponseData OPDisp()
        {
            List<DS_OPDispHead> dispHead = new List<DS_OPDispHead>();           
            DataTable dtFeeHead = requestData.GetData<DataTable>(0);
            int sendEmpID = requestData.GetData<int>(1);
            string sendEmpName = requestData.GetData<string>(2);
            int deptId = requestData.GetData<int>(3);
            string feeHeadIds = "(";
            foreach (DataRow row in dtFeeHead.Rows)
            {
                DS_OPDispHead modelHead = new DS_OPDispHead();
                DrugStoreManagement model = NewObject<DrugStoreManagement>();
                if (model.HasRefund(row["InvoiceNO"].ToString()))
                 {
                    throw new Exception("本张单据不能发药，存在退费记录，请进行退费操作");                    
                }
                
                modelHead.BillNO = 0;
                modelHead.BusiType = DGConstant.OP_DS_OPDISPENSE;
                modelHead.RetailFee = Convert.ToDecimal(row["TotalFee"]);
                modelHead.PatListID = Convert.ToInt32(row["PatListID"]);
                modelHead.PatName = row["PatName"].ToString();
                modelHead.PatSex = row["PatSex"].ToString();
                modelHead.Diagnose = row["DiseaseName"].ToString();
                modelHead.PatAge = row["Age"].ToString();
                modelHead.PresDocID = Convert.ToInt32(row["PresEmpID"]);
                modelHead.PresDocName = row["PresEmpName"].ToString();
                modelHead.PresDeptID = Convert.ToInt32(row["PresDeptID"]);
                modelHead.PresDeptName = row["PresDeptName"].ToString();
                modelHead.DispenserID = sendEmpID;
                modelHead.DispenserName = sendEmpName;
                modelHead.PharmacistID = sendEmpID;
                modelHead.PharmacistName = sendEmpName;
                modelHead.RefundFlag = 0;//发药0退药1
                modelHead.DispTime = System.DateTime.Now;
                modelHead.FeeNO = row["FeeNo"].ToString();
                modelHead.InvoiceNO = row["InvoiceNO"].ToString();
                modelHead.ChargeTime = Convert.ToDateTime(row["ChargeDate"]);
                modelHead.ChargeEmpID = Convert.ToInt32(row["ChargeEmpID"]);
                modelHead.ChargeEmpName = row["ChargeEmpName"].ToString();
                modelHead.FeeItemHeadID = Convert.ToInt32(row["CostHeadID"]);
                modelHead.RecipeID = Convert.ToInt32(row["FeeItemHeadID"]);
                modelHead.RecipeType = row["PresType"].ToString();
                modelHead.RecipeAmount = Convert.ToInt32(row["PresAmount"]);
                modelHead.DeptID = deptId;
                dispHead.Add(modelHead);
                feeHeadIds += row["FeeItemHeadID"].ToString() + ",";
            }

            feeHeadIds = feeHeadIds.Substring(0, feeHeadIds.Length - 1) + ")";

            //获取该结算单所有明细记录  
            List<DS_OPDispDetail> dispDetail = ConvertFeeDetailModel(feeHeadIds,deptId);
            OPDispBill iProcess = NewObject<OPDispBill>();
            DGBillResult rtn = new DGBillResult();
            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.OPDisp(dispHead, dispDetail, sendEmpID);
                if (rtn.Result == 0)
                {
                    oleDb.CommitTransaction();
                }
                else
                {
                    oleDb.RollbackTransaction();
                }

                responseData.AddData(rtn);
            }
            catch (Exception error)
            {
                oleDb.RollbackTransaction();
                rtn.ErrMsg = error.Message;
                rtn.Result = 2;
                responseData.AddData(rtn);
            }

            return responseData;
        }

        /// <summary>
        /// 将费用明细转换成实体模型
        /// </summary>
        /// <param name="feeHeadIds">过滤条件</param>
        /// <param name="deptId">药房</param>
        /// <returns>将费用明细转换成实体模型集</returns>
        private List<DS_OPDispDetail> ConvertFeeDetailModel(string feeHeadIds, int deptId)
        {
            List<DS_OPDispDetail> dispDetail = new List<DS_OPDispDetail>();
            DataTable dtFeeDetails = NewObject<DrugStoreManagement>().GetFeeItemDetail(feeHeadIds);
            foreach (DataRow row in dtFeeDetails.Rows)
            {
                DS_OPDispDetail modelDetail = new DS_OPDispDetail();
                modelDetail.DispHeadID = 0;
                modelDetail.FeeDetailID = Convert.ToInt32(row["PresDetailID"]);
                modelDetail.CTypeID = Convert.ToInt32(row["CTypeID"]);
                modelDetail.DrugID = Convert.ToInt32(row["DrugID"]);
                modelDetail.DrugSpec = row["Spec"].ToString();
                modelDetail.ChemName = row["ChemName"].ToString();
                modelDetail.ProductName = row["ProductName"].ToString();
                modelDetail.UnitID = Convert.ToInt32(row["MiniUnitID"]);
                modelDetail.UnitName = row["MiniUnit"].ToString();
                modelDetail.PackUnit = row["PackUnit"].ToString();
                modelDetail.UnitAmount = Convert.ToInt32(row["UnitNO"]);
                modelDetail.DispAmount = Convert.ToInt32(row["Amount"]);
                modelDetail.RetailPrice = Convert.ToDecimal(row["RetailPrice"]);
                modelDetail.StockPrice = Convert.ToDecimal(row["StockPrice"]);
                modelDetail.BatchNO = string.Empty;
                modelDetail.RetailFee = Convert.ToDecimal(row["TotalFee"]);               
                modelDetail.StockFee = 0;
                modelDetail.RetFlag = 0;//发药0退药1
                modelDetail.RefundFlag = 0;
                modelDetail.UseAmount = 0;
                modelDetail.UseWay = string.Empty;
                modelDetail.Frequency = string.Empty;
                modelDetail.UseUnit = string.Empty;
                modelDetail.Orders = string.Empty;
                modelDetail.DeptID = deptId;
                modelDetail.FeeItemHeadID = Convert.ToInt32(row["FeeItemHeadID"]);
                dispDetail.Add(modelDetail);
            }

            return dispDetail;
        }

        /// <summary>
        /// 将费用明细转换成实体模型
        /// </summary>
        /// <param name="dtRefund">退药明细</param>
        /// <param name="deptId">药房ID</param>
        /// <returns>将费用明细转换成实体模型集</returns>
        private List<DS_OPDispDetail> ConvertRefundDetailModel(DataTable dtRefund, int deptId)
        {
            List<DS_OPDispDetail> dispDetail = new List<DS_OPDispDetail>();
            foreach (DataRow row in dtRefund.Rows)
            {
                DS_OPDispDetail modelDetail = new DS_OPDispDetail();
                modelDetail.DispHeadID = 0;
                modelDetail.FeeDetailID = Convert.ToInt32(row["ReDetailID"]);
                modelDetail.CTypeID = Convert.ToInt32(row["CTypeID"]);
                modelDetail.DrugID = Convert.ToInt32(row["DrugID"]);
                modelDetail.DrugSpec = row["Spec"].ToString();
                modelDetail.ChemName = row["ChemName"].ToString();
                modelDetail.ProductName = row["ProductName"].ToString();
                modelDetail.UnitID = Convert.ToInt32(row["MiniUnitID"]);
                modelDetail.UnitName = row["MiniUnit"].ToString();
                modelDetail.PackUnit = row["PackUnit"].ToString();
                modelDetail.UnitAmount = Convert.ToInt32(row["UnitNO"]);
                modelDetail.DispAmount = Convert.ToInt32(row["RefundAmount"]);
                modelDetail.RetailPrice = Convert.ToDecimal(row["RetailPrice"]);
                modelDetail.StockPrice = Convert.ToDecimal(row["StockPrice"]);
                modelDetail.BatchNO = string.Empty;
                modelDetail.RetailFee = Convert.ToDecimal(row["RefundFee"]);
                decimal base_num = 0;//基本数--转换成包装数还剩多少基本数
                base_num = Convert.ToInt32(row["RefundAmount"]) % Convert.ToInt32(row["UnitNO"]);
                decimal relation_num = Convert.ToDecimal(row["UnitNO"]);
                decimal presAmount = Convert.ToDecimal(row["RefundAmount"]);
                decimal pack_num = Convert.ToDecimal((presAmount - base_num) / relation_num);                
                decimal price = Convert.ToDecimal(row["StockPrice"]);
                decimal num = Convert.ToDecimal(row["PresAmount"]);
                modelDetail.StockFee = (pack_num* price+(price / relation_num)* base_num)* num;
                modelDetail.RetFlag = 1;//发药0退药1
                modelDetail.RefundFlag = 0;
                modelDetail.UseAmount = 0;
                modelDetail.UseWay = string.Empty;
                modelDetail.Frequency = string.Empty;
                modelDetail.UseUnit = string.Empty;
                modelDetail.Orders = string.Empty;
                modelDetail.DeptID = deptId;
                modelDetail.FeeItemHeadID = Convert.ToInt32(row["FeeItemHeadID"]);
                dispDetail.Add(modelDetail);
            }

            return dispDetail;
        }

        #region 退药        
        /// <summary>
        /// 药房退药
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData OPRefund()
        {
            List<DS_OPDispHead> dispHead = new List<DS_OPDispHead>();
            DataTable dtRefund = requestData.GetData<DataTable>(0);
            int sendEmpID = requestData.GetData<int>(1);
            string sendEmpName = requestData.GetData<string>(2);
            int deptId = requestData.GetData<int>(3);

            //判断退费消息是否存在，不存在抛出异常
            foreach (DataRow row in dtRefund.Rows)
            {
                int reHeadID = Convert.ToInt32(row["ReHeadID"]);
                DataTable dtRefundRecord = NewObject<OP_FeeRefundHead>().gettable("ReHeadID=" + reHeadID.ToString());
                if (dtRefundRecord == null || dtRefundRecord.Rows.Count <= 0)
                {
                    throw new Exception("该退药单已经被删除，请刷新界面重新操作！");
                }

                int recipeId = Convert.ToInt32(row["FeeItemHeadID"]);

                //判断是否存在
                if(dispHead.FindIndex(x => x.RecipeID == recipeId)>=0)
                {
                    continue;
                }

                DS_OPDispHead modelHead = new DS_OPDispHead();
                modelHead.BillNO = 0;
                modelHead.BusiType = DGConstant.OP_DS_OPREFUND;

                //求退费总金额
                modelHead.RetailFee = Convert.ToDecimal(dtRefund.Compute("sum(RefundAmount)", "FeeItemHeadID=" + recipeId.ToString()));
                modelHead.PatListID = Convert.ToInt32(row["PatListID"]);
                modelHead.PatName = row["PatName"].ToString();
                modelHead.PatSex = row["PatSex"].ToString();
                modelHead.Diagnose = row["DiseaseName"].ToString();
                modelHead.PatAge = row["Age"].ToString();
                modelHead.PresDocID = Convert.ToInt32(row["PresEmpID"]);
                modelHead.PresDocName = row["PresEmpName"].ToString();
                modelHead.PresDeptID = Convert.ToInt32(row["PresDeptID"]);
                modelHead.PresDeptName = row["PresDeptName"].ToString();
                modelHead.DispenserID = sendEmpID;
                modelHead.DispenserName = sendEmpName;
                modelHead.PharmacistID = sendEmpID;
                modelHead.PharmacistName = sendEmpName;
                modelHead.RefundFlag = 1;//发药0退药1
                modelHead.DispTime = System.DateTime.Now;
                modelHead.FeeNO = row["FeeNo"].ToString();
                modelHead.InvoiceNO = row["InvoiceNO"].ToString();
                modelHead.ChargeTime = Convert.ToDateTime(row["ChargeDate"]);
                modelHead.ChargeEmpID = Convert.ToInt32(row["ChargeEmpID"]);
                modelHead.ChargeEmpName = row["ChargeEmpName"].ToString();
                modelHead.FeeItemHeadID = Convert.ToInt32(row["CostHeadID"]);
                modelHead.RecipeID = Convert.ToInt32(row["FeeItemHeadID"]);
                modelHead.RecipeType = row["PresType"].ToString();
                modelHead.RecipeAmount = Convert.ToInt32(row["PresAmount"]);
                modelHead.DeptID = deptId;
                dispHead.Add(modelHead);
            }

            //获取该结算单所有明细记录  
            List<DS_OPDispDetail> dispDetail = ConvertRefundDetailModel(dtRefund, deptId);
            OPDispBill iProcess = NewObject<OPDispBill>();
            DGBillResult rtn = new DGBillResult();
            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.OPRefund(dispHead, dispDetail, dtRefund);
                if (rtn.Result == 0)
                {
                    oleDb.CommitTransaction();
                }
                else
                {
                    oleDb.RollbackTransaction();
                }

                responseData.AddData(rtn);
            }
            catch (Exception error)
            {
                oleDb.RollbackTransaction();
                rtn.ErrMsg = error.Message;
                rtn.Result = 2;
                responseData.AddData(rtn);
            }

            return responseData;
        }

        /// <summary>
        /// 获取退费明细
        /// </summary>
        /// <returns>退费明细</returns>
        [WCFMethod]
        public ServiceResponseData GetRefundDetail()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt = model.GetRefundDetail(queryCondition);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取退费查询明细
        /// </summary>
        /// <returns>退费查询明细</returns>
        [WCFMethod]
        public ServiceResponseData GetRefundQueryDetail()
        {
            var queryCondition = requestData.GetData<Dictionary<string,string>>(0);
            DrugStoreManagement model = NewObject<DrugStoreManagement>();
            DataTable dt = model.GetRefundDetail(queryCondition);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
