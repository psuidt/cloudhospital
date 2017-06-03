using System;
using System.Collections.Generic;
using System.Data;
using EfwControls.HISControl.Orders.Controls;
using EfwControls.HISControl.Orders.Controls.Entity;
using EFWCoreLib.WcfFrame.ClientController;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.Winform.Controller
{
    /// <summary>
    /// 医嘱控件与医嘱管理接口实现类
    /// </summary>
    public class IPDOrderDbHelper : WcfClientController, IOrderDbHelper
    {
        /// <summary>
        /// 数据集
        /// </summary>
        private DataSet dataset;

        /// <summary>
        /// 药品项目数据
        /// </summary>
        private DataTable alldtItemDrug;

        /// <summary>
        /// 医嘱控件数据集获取
        /// </summary>
        /// <param name="orderCategory">医嘱类别0长嘱1临嘱</param>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>DataSet usagedic用法数据源 frequencydic频次数据源 entrustdic嘱托数据源 itemdrug药品项目数据源</returns>
        public DataSet OrderDataInit(int orderCategory, int pageNo, int pageSize, string filter)
        {
            //药品项目选项卡数据源
            if (dataset == null || dataset.Tables.Count == 0)
            {
                dataset = new DataSet();

                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(orderCategory);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "OrderDataInit", requestAction);
                 alldtItemDrug = retdata.GetData<DataTable>(0);

                //用法数据源
                List<Basic_Channel> list = retdata.GetData<List<Basic_Channel>>(1);
                List<CardDataSourceUsage> lists = new List<CardDataSourceUsage>();
                foreach (Basic_Channel entrust in list)
                {
                    CardDataSourceUsage myEnturst = new CardDataSourceUsage();
                    myEnturst.UsageName = entrust.ChannelName;
                    myEnturst.UsageId = entrust.ID;
                    myEnturst.Pym = entrust.PYCode;
                    myEnturst.Wbm = entrust.WBCode;
                    lists.Add(myEnturst);
                }

                DataTable dtUsage = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(lists);
                dtUsage.TableName = "usagedic";
                dataset.Tables.Add(dtUsage);
                //频次数据源
                List<Basic_Frequency> listF= retdata.GetData<List<Basic_Frequency>>(2);
                List<CardDataSourceFrequency> listFs = new List<CardDataSourceFrequency>();
                foreach (Basic_Frequency entrust in listF)
                {
                    CardDataSourceFrequency myEnturst = new CardDataSourceFrequency();
                    myEnturst.Name = entrust.FrequencyName;
                    myEnturst.FrequencyId = entrust.FrequencyID;
                    myEnturst.Pym = entrust.PYCode;
                    myEnturst.Wbm = entrust.WBCode;
                    myEnturst.ExecNum = 2;
                    listFs.Add(myEnturst);
                }

                DataTable dtFrequency = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(listFs);
                dtFrequency.TableName = "frequencydic";
                dataset.Tables.Add(dtFrequency);
                //嘱托数据源
                List<Basic_Entrust> listE = retdata.GetData<List<Basic_Entrust>>(3);
                List<CardDataSourceEntrust> listEs = new List<CardDataSourceEntrust>();
                foreach (Basic_Entrust entrust in listE)
                {
                    CardDataSourceEntrust myEnturst = new CardDataSourceEntrust();
                    myEnturst.Name = entrust.EntrustName;
                    myEnturst.Id = entrust.EntrustID;
                    myEnturst.Pym = entrust.PYCode;
                    myEnturst.Wbm = entrust.WBCode;
                    listEs.Add(myEnturst);
                }

                DataTable dtEntrust = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(listEs);
                dtEntrust.TableName = "entrustdic";
                dataset.Tables.Add(dtEntrust);           
            }

            if (orderCategory == 0)
            {
                //长嘱不能开中草药和材料
                DataTable dtCopy = alldtItemDrug.Clone();
                dtCopy.Clear();
                DataRow[] rows = alldtItemDrug.Select(" statid<>102 and itemClass<>2");
                foreach (DataRow dr in rows)
                {
                    dtCopy.Rows.Add(dr.ItemArray);
                }

                dtCopy.TableName = "itemdrug";
                if (dataset.Tables.Contains("itemdrug"))
                {
                    dataset.Tables.Remove("itemdrug");
                }

                dataset.Tables.Add(dtCopy);
                return dataset;
            }
            else
            {
                alldtItemDrug.TableName = "itemdrug";
                if (dataset.Tables.Contains("itemdrug"))
                {
                    dataset.Tables.Remove("itemdrug");
                }

                dataset.Tables.Add(alldtItemDrug);
                return dataset;
            }
        }

        /// <summary>
        /// 获取医嘱选项卡数据
        /// </summary>
        /// <param name="orderCategory">0长嘱1临嘱</param>
        /// <param name="pageNo">页号</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="filter">过滤条件</param>
        /// <returns>DataTable</returns>
        public DataTable GetDrugItem(int orderCategory, int pageNo, int pageSize, string filter)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
           {
               request.AddData(orderCategory);
           });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetShowCardData", requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            if (orderCategory == 0)
            {
                //长嘱不能开中草药和材料
                DataTable dtCopy = dt.Clone();
                dtCopy.Clear();
                DataRow[] rows = dt.Select(" statid<>102 and itemClass<>2");
                foreach (DataRow dr in rows)
                {
                    dtCopy.Rows.Add(dr.ItemArray);
                }

                dtCopy.TableName = "itemdrug";
                return dtCopy;
            }
            else
            {
                return dt;
            }
        }

        /// <summary>
        /// 刷新重新获取嘱托
        /// </summary>
        /// <returns>嘱托信息</returns>
        public DataTable GetRefreshEntrust()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetEntrustData");
            List<Basic_Entrust> listE = retdata.GetData<List<Basic_Entrust>>(0);
            List<CardDataSourceEntrust> listEs = new List<CardDataSourceEntrust>();
            foreach (Basic_Entrust entrust in listE)
            {
                CardDataSourceEntrust myEnturst = new CardDataSourceEntrust();
                myEnturst.Name = entrust.EntrustName;
                myEnturst.Id = entrust.EntrustID;
                myEnturst.Pym = entrust.PYCode;
                myEnturst.Wbm = entrust.WBCode;
                listEs.Add(myEnturst);
            }

            DataTable dtEntrust = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(listEs);
            dtEntrust.TableName = "entrustdic";
            return dtEntrust;
        }

        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="orderCategory">医嘱类别</param>
        /// <returns>组号</returns>
        public int GetGroupMax(int patlistid, int orderCategory)
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetGroupMax");
            int  groupID = retdata.GetData<int>(0);
            return groupID;
        }

        /// <summary>
        /// 获得医嘱信息
        /// </summary>
        /// <param name="orderCategory">医嘱类别</param>
        /// <param name="status">医嘱状态</param>
        /// <param name="patlistid">病人ID</param>
        /// <param name="deptid">科室ID</param>
        /// <returns>医嘱信息</returns>
        public DataTable GetOrders(int orderCategory, int status, int patlistid, int deptid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(orderCategory);
                request.AddData(patlistid);
                request.AddData(status);
                request.AddData(deptid);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetOrders",requestAction);
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 检查药品库存是否足够
        /// </summary>
        /// <param name="list">医嘱数据</param>
        /// <param name="errlist">库存不足医嘱数据</param>
        /// <returns>bool</returns>
        public bool IsDrugStore(List<OrderRecord> list, List<OrderRecord> errlist)
        {
            List<IPD_OrderRecord> records = new List<IPD_OrderRecord>();
            foreach (OrderRecord listrecord in list)
            {
                IPD_OrderRecord ipdRecord = new IPD_OrderRecord();
                ipdRecord.ItemID = listrecord.ItemID;
                ipdRecord.Amount = listrecord.Amount;
                ipdRecord.UnitNO = listrecord.UnitNO;
                ipdRecord.Spec = listrecord.Spec;
                ipdRecord.Dosage = listrecord.Dosage;
                ipdRecord.DosageUnit = listrecord.DosageUnit;
                ipdRecord.ExecDeptID = listrecord.ExecDeptID;
                ipdRecord.ItemName = listrecord.ItemName;
                records.Add(ipdRecord);
            }

            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(records);            
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "IsDrugStore", requestAction);
            List<IPD_OrderRecord>  errRecords= retdata.GetData<List<IPD_OrderRecord>>(0);
            if (errRecords.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (IPD_OrderRecord ipdRecord in errRecords)
                {
                    OrderRecord errRecord = new OrderRecord();
                    errRecord.ItemID = ipdRecord.ItemID;
                    errRecord.Amount = ipdRecord.Amount;
                    errRecord.UnitNO = ipdRecord.UnitNO;
                    errRecord.Spec = ipdRecord.Spec;
                    errRecord.Dosage = ipdRecord.Dosage;
                    errRecord.DosageUnit = ipdRecord.DosageUnit;
                    errRecord.ExecDeptID = ipdRecord.ExecDeptID;
                    errRecord.ItemName = ipdRecord.ItemName;
                    errlist.Add(errRecord);
                }

                return false;
            }
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        /// <param name="orderRecords">医嘱数据</param>
        /// <returns>bool</returns>
        public bool Save(List<OrderRecord> orderRecords)
        {
            try
            {
                DataTable dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(orderRecords);
                List<IPD_OrderRecord> ipdRecords = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ipdRecords);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "Save", requestAction);
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);               
            }
         }

        /// <summary>
        /// 医嘱操作
        /// </summary>
        /// <param name="orderRecords">医嘱数据</param>
        /// <param name="operatorType">1医嘱删除 2医嘱发送 3医嘱停嘱 4医嘱取消停</param>
        /// <param name="operatorEmpId">操作员ID</param>
        /// <returns>操作是否成功</returns>
        public bool OperatorOrder(List<OrderRecord> orderRecords,int operatorType, int operatorEmpId)
        {
            try
            {
                DataTable dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(orderRecords);
                List<IPD_OrderRecord> ipdRecords = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt);
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(ipdRecords);
                    request.AddData(operatorType);
                    request.AddData(operatorEmpId);
                    request.AddData(LoginUserInfo.WorkId);
                    request.AddData(LoginUserInfo.EmpId);
                    request.AddData(LoginUserInfo.DeptId);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "OperatorOrder", requestAction);
                return true;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        /// <summary>
        /// 获取皮试用药药品ID
        /// </summary>
        /// <returns>皮试用药药品ID</returns>
        public int GetActDrugID()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetActDrugId");
            string drugid = retdata.GetData<string>(0);
            if (string.IsNullOrEmpty(drugid))
            {
                return 0;
            }

            return Convert.ToInt32(drugid);
        }

        /// <summary>
        /// 根据频次默认次数
        /// </summary>
        /// <param name="type">0首次默认1末次默认</param>
        /// <param name="frequencyId">频次ID</param>
        /// <param name="date">日adwe</param>
        /// <returns>默认次数</returns>
        public int GetExecCount(int type, int frequencyId,DateTime date)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(type);
                request.AddData(frequencyId);
                request.AddData(date);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetExecCount", requestAction);
            int  count = retdata.GetData<int>(0);
            return count;
        }

        /// <summary>
        /// 转科，出院，死亡判断是否存在未执行医嘱
        /// </summary>
        /// <param name="patlistid">病人Id</param>
        /// <returns>未执行医嘱</returns>
        public DataTable getNotExsistOrders(int patlistid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);               
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "getNotExsistOrders", requestAction);
            DataTable  dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 获取所有住院临床科室
        /// </summary>
        /// <returns>住院临床科室</returns>
        public DataTable getIpDept()
        {          
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "getIpDept");
            DataTable dt = retdata.GetData<DataTable>(0);
            return dt;
        }

        /// <summary>
        /// 转科医嘱保存
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="list">医嘱数据</param>
        /// <param name="transDate">转科日期</param>
        /// <param name="transDeptId">转入科室</param>
        /// <param name="operatorid">操作人</param>
        /// <param name="spciRecord">转科医嘱对象</param>
        /// <returns>bool</returns>
        public bool TransDeptOrder(int patlistid, List<OrderRecord> list, DateTime transDate, int transDeptId,int operatorid,OrderRecord spciRecord)
        {
            try
            {
                DataTable dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(list);
                List<IPD_OrderRecord> ipdRecords = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt);
                List<OrderRecord> listSpci = new List<OrderRecord>();
                listSpci.Add(spciRecord);
                dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(listSpci);
                IPD_OrderRecord ipdRecord = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt)[0];
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patlistid);
                    request.AddData(ipdRecords);
                    request.AddData(transDate);
                    request.AddData(transDeptId);
                    request.AddData(operatorid);
                    request.AddData(ipdRecord);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "TransDeptOrder", requestAction);                
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取诊断
        /// </summary>
        /// <returns>诊断内容</returns>   
        public DataTable getDisease()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "getDisease");
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 获取出院情况
        /// </summary>
        /// <returns>出院情况</returns>
        public DataTable getOutSituation()
        {
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "getOutSituation");
            return retdata.GetData<DataTable>(0);
        }

        /// <summary>
        /// 出院医嘱，死亡医嘱保存
        /// </summary>
        /// <param name="patlistid">病人Id</param>
        /// <param name="list">停嘱医嘱内容</param>
        /// <param name="outDate">出院日期</param>
        /// <param name="outDiseaseName">出院诊断名称</param>
        /// <param name="outDiseaseCode">出院诊断Code</param>
        /// <param name="outSituation">出院情况</param>
        /// <param name="presDoctorId">操作医生</param>
        /// <param name="spciOrderRecord">出院医嘱对象</param>
        /// <returns>bool</returns>
        public bool OutHospOrder(int patlistid, List<OrderRecord> list, DateTime outDate, string outDiseaseName, string outDiseaseCode, string outSituation, int presDoctorId, OrderRecord spciOrderRecord)
        {
            try
            {
                DataTable dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(list);
                List<IPD_OrderRecord> ipdRecords = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt);
                List<OrderRecord> listSpci = new List<OrderRecord>();
                listSpci.Add(spciOrderRecord);
                dt = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(listSpci);
                IPD_OrderRecord ipdRecord = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<IPD_OrderRecord>(dt)[0];
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.AddData(patlistid);
                    request.AddData(ipdRecords);
                    request.AddData(outDate);
                    request.AddData(outDiseaseName);
                    request.AddData(outDiseaseCode);
                    request.AddData(outSituation);
                    request.AddData(presDoctorId);
                    request.AddData(ipdRecord);
                });
                ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "OutHospOrder", requestAction);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public DataTable GetPatInfo(int patlistid)
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("IPProject.Service", "OrderManagerController", "GetPatientInfoByPatListID", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            return dtPatInfo;
        }
    }
}
