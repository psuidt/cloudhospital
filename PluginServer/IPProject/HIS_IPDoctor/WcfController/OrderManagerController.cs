using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_Entity.DrugManage;
using HIS_IPDoctor.Dao;
using HIS_IPDoctor.ObjectModel;
using HIS_PublicManage.ObjectModel;
using System.Linq;

namespace HIS_IPDoctor.WcfController
{
    /// <summary>
    /// 医嘱管理后面控制器类
    /// </summary>
    [WCFController]
    public class OrderManagerController : WcfServerController
    {
        #region 病人信息获取
        /// <summary>
        /// 获取病人列表
        /// </summary>
        /// <returns>病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientInfo()
        {
            int doctorID = requestData.GetData<int>(0);
            int deptID = requestData.GetData<int>(1);
            DateTime bdate = requestData.GetData<DateTime>(2);
            DateTime edate = requestData.GetData<DateTime>(3);           
            bool isOut = requestData.GetData<bool>(4);
            bool isMy = requestData.GetData<bool>(5);
            string queryContent = requestData.GetData<string>(6);
            DataTable dtPatient = NewDao<IIPDOrderDao>().GetPatientInfo(doctorID, deptID, bdate, edate, isOut, isMy, queryContent,0);           
            responseData.AddData(dtPatient);
            return responseData;
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns>科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptList()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            int doctorID = requestData.GetData<int>(0);
            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.住院临床科室, false);
            responseData.AddData(dtDept);
            return responseData;
        }

        /// <summary>
        /// 获取病人详细信息
        /// </summary>
        /// <returns>病人详细信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientInfoByPatListID()
        {
            int patListid = requestData.GetData<int>(0);
            DataTable dtPat = NewDao<IIPDOrderDao>().GetPatientInfo(0, 0, DateTime.Now, DateTime.Now, true, true, string.Empty, patListid);
            responseData.AddData(dtPat);
            DataTable dtPatFee = NewDao<IIPDOrderDao>().GetPatDepositFee(patListid);
            responseData.AddData(dtPatFee);
            //判断病人是否存在未完成的转科医嘱
            bool hasNotFinishTrans = false;
            List<IPD_TransDept> trans = NewObject<IPD_TransDept>().getlist<IPD_TransDept>(" Patlistid=" + patListid + " and CancelFlag=0 and FinishFlag=0");
            if (trans!=null && trans.Count > 0)
            {
                hasNotFinishTrans = true;
            }

            responseData.AddData(hasNotFinishTrans);
            return responseData;
        }

        /// <summary>
        /// 获取病人费用数据
        /// </summary>
        /// <returns>费用数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientFeeInfo()
        {
            int patListid = requestData.GetData<int>(0);
            DataTable dtPatFee = NewDao<IIPDOrderDao>().GetPatDepositFee(patListid);
            responseData.AddData(dtPatFee);
            //判断病人是否存在未完成的转科医嘱
            bool hasNotFinishTrans = false;
            List<IPD_TransDept> trans = NewObject<IPD_TransDept>().getlist<IPD_TransDept>(" Patlistid="+patListid+ " and CancelFlag=0 and FinishFlag=0");
            if (trans!=null && trans.Count > 0)
            {
                hasNotFinishTrans = true;
            }

            responseData.AddData(hasNotFinishTrans);
            return responseData;
        }
        #endregion

        /// <summary>
        /// 界面始始化获取数据
        /// </summary>
        /// <returns>数据集</returns>
        [WCFMethod]
        public ServiceResponseData OrderDataInit()
        {
            int orderCategory = requestData.GetData<int>(0);
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dtDrugItem = feeitem.GetFeeItemDataDt(FeeBusinessType.医嘱业务);
            responseData.AddData(dtDrugItem);

            List<Basic_Channel> list = NewObject<Basic_Channel>().getlist<Basic_Channel>(" DelFlag=0 and InUsed=1");
            responseData.AddData(list);

            List<Basic_Frequency> listFre = NewObject<Basic_Frequency>().getlist<Basic_Frequency>(" DelFlag=0");
            responseData.AddData(listFre);

            List<Basic_Entrust> listEntrust = NewObject<Basic_Entrust>().getlist<Basic_Entrust>(" DelFlag=0");
            responseData.AddData(listEntrust);
            return responseData;
        }

        #region 医嘱控件数据获取
        /// <summary>
        /// 获取药品项目数据
        /// </summary>
        /// <returns>药品项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetShowCardData()
        {
            int orderCategory = requestData.GetData<int>(0);
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dtDrugItem = feeitem.GetFeeItemDataDt(FeeBusinessType.医嘱业务);
            responseData.AddData(dtDrugItem);
            return responseData;
        }

        /// <summary>
        /// 获取用法 
        /// </summary>
        /// <returns>用法数据</returns>
        [WCFMethod]
        public ServiceResponseData GetChannelData()
        {
            List<Basic_Channel> list= NewObject<Basic_Channel>().getlist<Basic_Channel>(" DelFlag=0 and InUsed=1").OrderBy(p=>p.SortOrder).ToList();
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取频次
        /// </summary>
        /// <returns>频次数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFrequencyData()
        {
            List<Basic_Frequency> list = NewObject<Basic_Frequency>().getlist<Basic_Frequency>(" DelFlag=0").OrderBy(p => p.SortOrder).ToList();
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取嘱托
        /// </summary>
        /// <returns>嘱托数据</returns>        
        [WCFMethod]
        public ServiceResponseData GetEntrustData()
        {
            List<Basic_Entrust> list = NewObject<Basic_Entrust>().getlist<Basic_Entrust>(" DelFlag=0");
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns>单位</returns>
        [WCFMethod]
        public ServiceResponseData GetUnitData()
        {
            DataTable dt = new DataTable();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取医嘱
        /// </summary>
        /// <returns>医嘱数据</returns>
        [WCFMethod]
        public ServiceResponseData GetOrders()
        {
            int orderCategory = requestData.GetData<int>(0);
            int patlistid = requestData.GetData<int>(1);
            int orderStatus = requestData.GetData<int>(2);
            int deptID = requestData.GetData<int>(3);
            DataTable dt = NewObject<IIPDOrderDao>().GetOrders(orderCategory, patlistid, orderStatus, deptID);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 医嘱操作
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <returns>组号</returns>
        [WCFMethod]
        public ServiceResponseData GetGroupMax()
        {
            SerialNumberSource serialNumberSource = NewObject<SerialNumberSource>();
            string groupID = serialNumberSource.GetSerialNumber(SnType.医嘱组号);
            responseData.AddData(groupID);
            return responseData;
        }

        /// <summary>
        /// 药品库存判断
        /// </summary>
        /// <returns>库存不足药品</returns>
        [WCFMethod]
        public ServiceResponseData IsDrugStore()
        {
            List<IPD_OrderRecord> records = requestData.GetData<List<IPD_OrderRecord>>(0);
            List<IPD_OrderRecord> errRecords = new List<IPD_OrderRecord>();
            DrugStoreManagement drugStore = NewObject<DrugStoreManagement>();
            foreach (IPD_OrderRecord record in records)
            {
                if (record.ItemType == 1)
                {
                    if (drugStore.GetStorage(record.ItemID, record.ExecDeptID) < record.Amount * record.UnitNO)
                    {
                        errRecords.Add(record);
                    }
                }
            }  
                 
            responseData.AddData(errRecords);
            return responseData;
        }

        /// <summary>
        /// 医嘱删除，医嘱发送，医嘱停嘱，医嘱取消停嘱
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData OperatorOrder()
        {
            List<IPD_OrderRecord> records = requestData.GetData<List<IPD_OrderRecord>>(0);
            int operatorType = requestData.GetData<int>(1);//1医嘱删除 2医嘱发送 3医嘱停嘱 4医嘱取消停
            int empid = requestData.GetData<int>(2);
            NewObject<OrderProcess>().OperatorOrder(records, operatorType, empid);
            #region "保存业务消息数据 --Add By ZhangZhong"
            // 保存业务消息数据
            if (operatorType == 2)
            {
                Dictionary<string, string> msgDic = new Dictionary<string, string>();
                int workId = requestData.GetData<int>(3);
                int userId = requestData.GetData<int>(4);
                int deptId = requestData.GetData<int>(5);
                msgDic.Add("PatListID", records[0].PatListID.ToString()); // 病人登记ID
                msgDic.Add("WorkID", workId.ToString()); // 消息机构ID
                msgDic.Add("SendUserId", userId.ToString()); // 消息生成人ID
                msgDic.Add("SendDeptId", deptId.ToString()); // 消息生成科室ID
                NewObject<BusinessMessage>().GenerateBizMessage(MessageType.住院新医嘱, msgDic);
            }
            #endregion
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        /// <returns>true</returns>        
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData Save()
        {
            List<IPD_OrderRecord> records = requestData.GetData<List<IPD_OrderRecord>>(0);
            NewObject<OrderProcess>().SaveRecords(records);
            responseData.AddData(true);
            return responseData;
        }
        #endregion

        /// <summary>
        /// 获取所有在用药房
        /// </summary>
        /// <returns>在用药房</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugStore()
        {
            DataTable dt = NewObject<DG_DeptDic>().gettable(" DeptType = 0 AND StopFlag = 0");
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取皮试对应的药品ID
        /// </summary>
        /// <returns>对应的药品ID</returns>
        [WCFMethod]
        public ServiceResponseData GetActDrugId()
        {
            string actDrugId = NewObject<SysConfigManagement>().GetSystemConfigValue("ActOrderDrugID");
            responseData.AddData(actDrugId);
            return responseData;
        }

        /// <summary>
        /// 通过科室ID获取科室对应可开药品的药房ID
        /// </summary>
        /// <returns>药房ID</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugStoreByDeptID()
        {
            int deptid = requestData.GetData<int>(0);
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("DeptIDs", Type.GetType("System.String"));
            try
            {
                string drugDeptIds = string.Empty;
                string[] drugDepts = NewObject<SysConfigManagement>().GetSystemConfigValue("IPDDeptDrugStore").Split(',');
                foreach (string drugDept in drugDepts)
                {
                    string[] values = drugDept.Split('-');
                    drugDeptIds += (Convert.ToInt32(values[0]) == deptid ? (values[1] + ",") : string.Empty);
                }

                if (drugDeptIds.Trim() != string.Empty)
                {
                    drugDeptIds = drugDeptIds.Substring(0, drugDeptIds.Length - 1);
                    dt.Rows.Add("默认药房", drugDeptIds);
                }
            }
            catch
            {
            }

            dt.Rows.Add("全部药房", -1);

            DataTable yf = NewObject<DG_DeptDic>().gettable(" DeptType = 0 AND StopFlag = 0");
            if (yf != null)
            {
                foreach (DataRow row in yf.Rows)
                {
                    dt.Rows.Add(row["DeptName"].ToString().Trim(), row["DeptId"]);
                }
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取开嘱和停嘱的默认执行次数
        /// </summary>
        /// <returns>默认执行次数</returns>
        [WCFMethod]
        public ServiceResponseData GetExecCount()
        {
            int type = requestData.GetData<int>(0);
            int frequencyID = requestData.GetData<int>(1);
            DateTime date = requestData.GetData<DateTime>(2);
            DateTime bdate = new DateTime();
            DateTime edate = new DateTime();
            if (type == 0)
            {
                //首次默认
                bdate = date;
                edate = Convert.ToDateTime(date.ToString("yyyy-MM-dd 23:59:59"));
            }
            else if (type == 1)
            {
                //末次默认
                bdate = Convert.ToDateTime(date.ToString("yyyy-MM-dd 00:00:00"));
                edate = date;
            }

            FrequencyManagement frequencyManager = NewObject<FrequencyManagement>();
            int count = 0;
            try
            {
                count=frequencyManager.GetExecCount(frequencyID, bdate, edate);
            }
            catch(Exception )
            {
                count = 0;
            }

            responseData.AddData(count);
            return responseData;
        }

        /// <summary>
        /// 获取未执行医嘱
        /// </summary>
        /// <returns>未执行医嘱</returns>
        [WCFMethod]
        public ServiceResponseData getNotExsistOrders()
        {
            int patlistid = requestData.GetData<int>(0);
            DataTable dt = NewObject<IPD_OrderRecord>().gettable(" patlistid="+patlistid+" and  DeleteFlag=0 and ((orderStatus<2 and orderCategory=0) or (orderCategory=1 and orderStatus<1))");
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取住院临床科室
        /// </summary>
        /// <returns>住院临床科室</returns>
        [WCFMethod]
        public ServiceResponseData getIpDept()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();            
            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.住院临床科室, false);
            responseData.AddData(dtDept);
            return responseData;
        }

        /// <summary>
        /// 转科医嘱
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData TransDeptOrder()
        {
            int patlistid = requestData.GetData<int>(0);
            List<IPD_OrderRecord> list = requestData.GetData<List<IPD_OrderRecord>>(1);
            DateTime transDate = requestData.GetData<DateTime>(2);
            int transDeptID = requestData.GetData<int>(3);
            int oprator = requestData.GetData<int>(4);
            IPD_OrderRecord spciRecord = requestData.GetData<IPD_OrderRecord>(5);
            NewObject<OrderProcess>().TransDeptOrder(patlistid, list, transDate, transDeptID, oprator, spciRecord);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <returns>诊断数据</returns>
        [WCFMethod]
        public ServiceResponseData getDisease()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            DataTable dtDisease = basicmanagement.GetDisease();
            responseData.AddData(dtDisease);
            return responseData;
        }

        /// <summary>
        /// 获取出院情况
        /// </summary>
        /// <returns>出院情况数据</returns>
        [WCFMethod]
        public ServiceResponseData getOutSituation()
        {           
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.入院情况, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 出院医嘱
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData OutHospOrder()
        {
            int patlistid = requestData.GetData<int>(0);
            List<IPD_OrderRecord> list = requestData.GetData<List<IPD_OrderRecord>>(1);
            DateTime outDate = requestData.GetData<DateTime>(2);
            string outDiseaseName = requestData.GetData<string>(3);
            string outDiseaseCode = requestData.GetData<string>(4);
            string outSituation = requestData.GetData<string>(5);          
            int oprator = requestData.GetData<int>(6);
            IPD_OrderRecord spciRecord = requestData.GetData<IPD_OrderRecord>(7);
            NewObject<OrderProcess>().OutHospOrder(patlistid, list, outDate, outDiseaseName, outDiseaseCode, outSituation, oprator, spciRecord);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取血糖数据
        /// </summary>
        /// <returns>血糖数据</returns>
        [WCFMethod]
        public ServiceResponseData GetBloodGluRecord()
        {
            int patlistid = requestData.GetData<int>(0);
            DataTable dtBloodRecord = NewDao<IIPDOrderDao>().GetBloodGluRecord(patlistid);
            for (int i = 0; i < dtBloodRecord.Rows.Count; i++)
            {
                if (dtBloodRecord.Rows[i]["QueryName"].ToString().Trim() == "血糖")
                {
                    for (int j = 3; j < dtBloodRecord.Columns.Count; j++)
                    {
                        if (dtBloodRecord.Rows[i][j].ToString().Trim() == "0.00")
                        {
                            dtBloodRecord.Rows[i][j] = DBNull.Value;
                        }
                    }
                }
            }

            responseData.AddData(dtBloodRecord);
            return responseData;
        }
    }
}
