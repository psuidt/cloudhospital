using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.Bill;
using HIS_Entity.DrugManage;
using HIS_Entity.IPManage;
using HIS_PublicManage.ObjectModel;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 住院统领发药控制器
    /// </summary>
    [WCFController]
    public class IpDispController : WcfServerController
    {
        /// <summary>
        /// 获取药品信息数据源
        /// </summary>
        /// <returns>药品信息数据源</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugShowCard()
        {
            int deptId = requestData.GetData<int>(0);
            DataTable dt = NewDao<IDGDao>().GetDrugStoreShowCardData(deptId);
            responseData.AddData(dt);//药品项目选项卡数据
            return responseData;
        }

        /// <summary>
        /// 获取临床科室数据
        /// </summary>
        /// <returns>临床科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetClinicalDeptData()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 缺药
        /// </summary>
        /// <returns>true成功false失败</returns>
        [WCFMethod]
        public ServiceResponseData ShortageDrug()
        {
            int detailId = Convert.ToInt32(requestData.GetData<string>(0));
            IP_DrugBillDetail model = (IP_DrugBillDetail)NewObject<IP_DrugBillDetail>().getmodel(detailId);
            model.NoDrugFlag = 1;
            int iRtn = model.save();
            bool bRtn = false;
            if (iRtn > 0)
            {
                bRtn = true;
                #region "保存业务消息数据 --Add By ZhangZhong"

                // 保存业务消息数据
                Dictionary<string, string> msgDic = new Dictionary<string, string>();
                int workId = requestData.GetData<int>(1);
                int userId = requestData.GetData<int>(2);
                int deptId = requestData.GetData<int>(3);
                msgDic.Add("WorkID", workId.ToString()); // 消息机构ID
                msgDic.Add("SendUserId", userId.ToString()); // 消息生成人ID
                msgDic.Add("SendDeptId", deptId.ToString()); // 消息生成科室ID
                msgDic.Add("BillDetailID", model.BillDetailID.ToString()); // 统领明细ID
                NewObject<BusinessMessage>().GenerateBizMessage(MessageType.药房缺药, msgDic);
                #endregion
            }
            else
            {
                bRtn = false;
            }

            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 获取统领单类型
        /// </summary>
        /// <returns>统领单类型</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDrugBillType()
        {
            DataTable dt = NewDao<IDSDao>().GetIPDrugBillType();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得统领单头表
        /// </summary>
        /// <returns>统领头表</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDrugBillHead()
        {
            Dictionary<string, string> condition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<DrugStoreManagement>().GetIPDrugBillHead(condition);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 取得统领单明细表
        /// </summary>
        /// <returns>统领明细表</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDrugBillDetail()
        {
            Dictionary<string, string> condition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<DrugStoreManagement>().GetIPDrugBillDetail(condition);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取发药头表数据
        /// </summary>
        /// <returns>发药头表数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDispIPBillHead()
        {
            Dictionary<string, string> condition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewDao<DrugStoreManagement>().GetDispIPBillHead(condition);
            responseData.AddData(dt);
            return responseData;
        }

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
        /// 抽取某些列去掉重复行
        /// </summary>
        /// <param name="dtHead">数据表</param>
        /// <param name="filedNames">字段数组</param>
        /// <returns>去掉重复行</returns>
        public DataTable DistinctTableInfo(DataTable dtHead, string[] filedNames)
        {
            DataView dv = dtHead.DefaultView;
            DataTable distTable = dv.ToTable("dept", true, filedNames);
            return distTable;
        }

        /// <summary>
        /// 获取住院发药数据
        /// </summary>
        /// <returns>住院发药数据</returns>
        [WCFMethod]
        public ServiceResponseData IPDisp()
        {
            //每次发药产生一个发药表头
            DS_IPDispHead dispHead = new DS_IPDispHead();
            DataTable dtDispDetail = requestData.GetData<DataTable>(0);
            int sendEmpID = requestData.GetData<int>(1);
            string sendEmpName = requestData.GetData<string>(2);
            int deptId = requestData.GetData<int>(3);
            dispHead.DispHeadID = 0;
            dispHead.BillNO = 0;
            dispHead.RetailFee = Convert.ToDecimal(dtDispDetail.Compute("sum(SellFee)", "chk=1"));
            dispHead.DispenserID = sendEmpID;
            dispHead.PharmacistID = sendEmpID;
            dispHead.DispTime = DateTime.Now;
            dispHead.RefFlag = 0;
            dispHead.BusiType = DGConstant.OP_DS_IPDISPENSE;
            dispHead.DeptID = Convert.ToInt32(dtDispDetail.Rows[0]["PresDeptID"]);//科室
            dispHead.ExecDeptID = deptId;
            dispHead.BillTypeID = Convert.ToInt32(dtDispDetail.Rows[0]["BillTypeID"]);

            //检查统领单是否存在，不存在抛出异常
            DataTable dtCheck = DistinctTableInfo(dtDispDetail, new string[] { "chk", "BillHeadID", "BillDetailID", "ChemName", "PatName" });
            foreach (DataRow r in dtCheck.Rows)
            {
                if (r["chk"].ToString() == "1")
                {
                    IP_DrugBillDetail m = (IP_DrugBillDetail)NewObject<IP_DrugBillDetail>().getmodel(r["BillDetailID"].ToString());
                    if (m == null)
                    {
                        throw new Exception("患者【" + r["PatName"].ToString() + "】的药品【" + r["ChemName"].ToString() + "】明细单据不存在，请刷新数据重发");
                    }
                }
            }

            //获取该结算单所有明细记录  
            List<DS_IPDispDetail> dispDetail = ConvertFeeDetailModel(dtDispDetail, deptId);
            IPDispBill iProcess = NewObject<IPDispBill>();
            DGBillResult rtn = new DGBillResult();
            try
            {
                oleDb.BeginTransaction();
                rtn = iProcess.IPDisp(dispHead, dispDetail);
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
        /// 转换住院发药费用明细模型
        /// </summary>
        /// <param name="dtDetail">住院发药费用明细数据源</param>
        /// <param name="deptId">科室ID</param>
        /// <returns>住院发药费用明细数据集</returns>
        private List<DS_IPDispDetail> ConvertFeeDetailModel(DataTable dtDetail, int deptId)
        {
            List<DS_IPDispDetail> dispDetail = new List<DS_IPDispDetail>();
            foreach (DataRow row in dtDetail.Rows)
            {
                if (row["chk"].ToString() == "1")
                {
                    DS_IPDispDetail modelDetail = new DS_IPDispDetail();
                    modelDetail.DispDetailID = 0;
                    modelDetail.DispHeadID = 0;
                    modelDetail.CTypeID = Convert.ToInt32(row["CTypeID"]);
                    modelDetail.DrugID = Convert.ToInt32(row["DrugID"]);
                    modelDetail.DrugSpecID = Convert.ToInt32(row["DrugSpecID"]);
                    modelDetail.ChemName = row["ChemName"].ToString();
                    modelDetail.DrugSpec = row["DrugSpec"].ToString();
                    modelDetail.ProductName = row["ProductName"].ToString();
                    modelDetail.UnitID = Convert.ToInt32(row["UnitID"]);
                    modelDetail.UnitName = row["UnitName"].ToString();
                    modelDetail.UnitAmount = Convert.ToInt32(row["UnitAmount"]);
                    modelDetail.DispAmount = Convert.ToInt32(row["DispAmount"]);
                    modelDetail.Dosage = Convert.ToInt32(row["Dosage"]);
                    modelDetail.RetailPrice = Convert.ToDecimal(row["SellPrice"]);
                    modelDetail.StockPrice = Convert.ToDecimal(row["InPrice"]);
                    modelDetail.BatchNO = string.Empty;
                    modelDetail.RetailFee = Convert.ToDecimal(row["SellFee"]);
                    modelDetail.StockFee = Convert.ToDecimal(row["StockFee"]);
                    modelDetail.FeelID = Convert.ToInt32(row["FeeRecordID"]);
                    modelDetail.ChargeTime = Convert.ToDateTime(row["CostDate"]);
                    modelDetail.ChargerID = Convert.ToInt32(row["CostEmpID"]);
                    modelDetail.MsgHeadID = Convert.ToInt32(row["BillHeadID"]);
                    modelDetail.MsgDetaillID = Convert.ToInt32(row["BillDetailID"]);
                    modelDetail.GroupNO = Convert.ToInt32(row["GroupNO"]);
                    modelDetail.InpatientNO = row["InpatientNO"].ToString();
                    modelDetail.InPatientID = Convert.ToInt32(row["PatListID"]);
                    modelDetail.PatName = row["PatName"].ToString();
                    modelDetail.BedNo = row["BedNO"].ToString();
                    modelDetail.PresDocID = Convert.ToInt32(row["PatDoctorID"]);
                    modelDetail.PresDocName = row["PresDocName"].ToString();
                    modelDetail.UseAmount = Convert.ToInt32(row["UseAmount"]);
                    modelDetail.UseWay = row["UseWay"].ToString();
                    modelDetail.Frequency = row["Frequency"].ToString();
                    modelDetail.UseUnit = row["UseUnit"].ToString();
                    modelDetail.PackUnit = row["PackUnit"].ToString();
                    modelDetail.Orders = row["Orders"].ToString();
                    modelDetail.DeptID = deptId;
                    dispDetail.Add(modelDetail);
                }
            }

            return dispDetail;
        }

        /// <summary>
        /// 取得统领单明细表-用于打印
        /// </summary>
        /// <returns>统领明细表</returns>
        [WCFMethod]
        public ServiceResponseData GetIPDrugBillDetailPrint()
        {
            int iDispHeadID = requestData.GetData<int>(0);
            DataTable dt = NewDao<DrugStoreManagement>().GetIPDrugBillDetailPrint(iDispHeadID);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
