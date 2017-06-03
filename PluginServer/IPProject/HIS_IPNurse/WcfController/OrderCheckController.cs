using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.Common;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_Entity.MIManage;
using HIS_IPNurse.Dao;
using HIS_IPNurse.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPNurse.WcfController
{
    /// <summary>
    /// 护士站医嘱发送服务端控制器
    /// </summary>
    [WCFController]
    public class OrderCheckController : WcfServerController
    {
        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns>科室列表</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptList()
        {
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(deptDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人可发送医嘱列表
        /// </summary>
        /// <returns>病人可发送医嘱列表</returns>
        [WCFMethod]
        public ServiceResponseData GetOrederCheckInfo()
        {
            int iDeptID = requestData.GetData<int>(0);
            string sOrderCategory = requestData.GetData<string>(1);
            string sOrderStatus = requestData.GetData<string>(2);
            DataTable patListDt = NewDao<IOrderCheckDao>().GetOrderCheckPatList(iDeptID, sOrderCategory, sOrderStatus);
            DataTable orderListDt = NewDao<IOrderCheckDao>().GetOrederCheckInfo(iDeptID, sOrderCategory, sOrderStatus);
            responseData.AddData(patListDt);
            responseData.AddData(orderListDt);
            return responseData;
        }

        /// <summary>
        /// 获取病人医嘱关联费用列表
        /// </summary>
        /// <returns>病人医嘱关联费用列表</returns>
        [WCFMethod]
        public ServiceResponseData GetOrderRelationFeeList()
        {
            int patListID = requestData.GetData<int>(0);
            int groupID = requestData.GetData<int>(1);
            DataTable docFeeList = NewDao<IOrderCheckDao>().GetOrderRelationFeeList(patListID, groupID);
            responseData.AddData(docFeeList);
            return responseData;
        }

        /// <summary>
        /// 医嘱发送
        /// </summary>
        /// <returns>发送结果</returns>
        [WCFMethod]
        public ServiceResponseData SendOrderCheckList()
        {
            List<int> iGroupIDList = requestData.GetData<List<int>>(0);
            DateTime endTime = requestData.GetData<DateTime>(1);
            int iEmpId = requestData.GetData<int>(2);
            ResultClass resultClass = NewDao<OrderCheckProcess>().AdviceExecute_Entrance(iGroupIDList, endTime, iEmpId);
            responseData.AddData(resultClass.bSucess);
            if (resultClass.bSucess)
            {
                responseData.AddData(resultClass.oResult);
            }
            else
            {
                responseData.AddData(resultClass.sRemarks);
            }

            return responseData;
        }

        #region 定时发送接口

        /// <summary>
        /// 医嘱定时发送接口  日期直接取当天24点  直接获取可发送的GroupId
        /// </summary>
        /// <returns>医嘱发送结果</returns>
        [WCFMethod]
        public ServiceResponseData AutoSendOrderCheckList()
        {
            try
            {
                int[] iWorkIDList = requestData.GetData<int[]>(0);
                foreach (int iWorkId in iWorkIDList)
                {
                    SetWorkId(iWorkId);
                    DateTime endTime = DateTime.Now;
                    List<int> iGroupIDList = new List<int>();
                    // 口服药，如果配置了发送截止时间，需要已截止时间为准
                    string sKfEndtime = NewObject<SysConfigManagement>().GetSystemConfigValue("ZyKFSendTime").Trim();
                    if (sKfEndtime != string.Empty && sKfEndtime.Length == 8)
                    {
                        // 在系统参数的基础上加1秒
                        endTime = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd " + sKfEndtime)).AddSeconds(1);
                    }
                    else
                    {
                        // 在当前日期12点的基础上加1秒
                        endTime = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd " + "12:00:01"));
                    }

                    DataTable dtOrderCheckGroupId = NewDao<IOrderCheckDao>().GetIPOrderCheckGroupList(endTime);
                    foreach (DataRow dr in dtOrderCheckGroupId.Rows)
                    {
                        int iGroupId = Convert.ToInt32(dr["GroupID"]);
                        iGroupIDList.Add(iGroupId);
                    }

                    if (iGroupIDList.Count > 0)
                    {
                        ResultClass resultClass = NewDao<OrderCheckProcess>().AdviceExecute_Entrance(iGroupIDList, endTime, 0);
                        if (resultClass.bSucess)
                        {
                            //将缺药等信息记录到日志
                            List<IP_OrderCheckError> orderCheckErrorList = (List<IP_OrderCheckError>)resultClass.oResult;
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Blue, DateTime.Now.ToString("yyyy-MM-dd") + " 医嘱自动发送成功！");
                            if (orderCheckErrorList.Count > 0)
                            {
                                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "医嘱自动发送错误信息：");
                                foreach (IP_OrderCheckError orderCheckError in orderCheckErrorList)
                                {
                                    MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  床号：" + orderCheckError.BedNo + " 组号：" + orderCheckError.GroupID + " 医嘱：" + orderCheckError.OrderName + " 原因：" + orderCheckError.ErrorMessage);
                                }
                            }

                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 医嘱自动发送失败！");
                            //失败的话 记录错误日志
                            string sError = resultClass.sRemarks;
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "医嘱自动发送错误信息：");
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + sError);
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        }
                    }
                }

                return responseData;
            }
            catch (Exception e)
            {
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 医嘱自动发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "医嘱自动发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                return null;
            }
        }

        /// <summary>
        /// 账单定时发送接口  日期直接取当天24点  直接获取可发送的GroupId
        /// </summary>
        /// <returns>账单发送结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AutoSendAccountCheckList()
        {
            try
            {
                int[] iWorkIDList = requestData.GetData<int[]>(0);
                foreach (int iWorkId in iWorkIDList)
                {
                    SetWorkId(iWorkId);
                    DateTime endTime = DateTime.Now;
                    int iPatientID = -1;
                    int empID = 0;

                    #region 1.账单记账
                    DataTable dtFeeAccountID = NewDao<IOrderCheckDao>().GetFeeAccount(iPatientID, endTime);
                    if (dtFeeAccountID != null && dtFeeAccountID.Rows.Count > 0)
                    {
                        List<int> feeAccountIDList = new List<int>();
                        foreach (DataRow dr in dtFeeAccountID.Rows)
                        {
                            feeAccountIDList.Add(Convert.ToInt32(dr["GenerateID"]));
                        }

                        bool b = NewObject<DocOrderExpenseCheck>().FeeItemAccounting(feeAccountIDList, endTime, empID);
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Blue, DateTime.Now.ToString("yyyy-MM-dd") + (b ? " 账单自动发送成功！" : " 账单自动发送失败！"));
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    }
                    #endregion
                }

                return responseData;
            }
            catch (Exception e)
            {
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 账单自动发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "账单自动发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                return null;
            }
        }

        /// <summary>
        /// 床位费定时发送接口  日期直接取当天24点  直接获取可发送的GroupId
        /// </summary>
        /// <returns>床位费发送结果</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AutoSendBedCheckList()
        {
            try
            {
                int[] iWorkIDList = requestData.GetData<int[]>(0);

                foreach (int iWorkId in iWorkIDList)
                {
                    SetWorkId(iWorkId);
                    DateTime endTime = DateTime.Now;
                    int iPatientID = -1;
                    int empID = 0;
                    #region 1.床位费记账
                    DataTable bedPatientIDList = NewDao<IOrderCheckDao>().GetBedPatientIdList(iPatientID);
                    if (bedPatientIDList != null)
                    {
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        foreach (DataRow dr in bedPatientIDList.Rows)
                        {
                            bool b = NewObject<DocOrderExpenseCheck>().BedFeeAccounting(Convert.ToInt32(dr["PatListID"]), endTime, empID);
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, (b ? System.Drawing.Color.Blue : System.Drawing.Color.Red), dr["DeptName"] + "-" + dr["PatName"] + "-" + DateTime.Now.ToString("yyyy-MM-dd") + (b ? " 床位费自动发送成功！" : " 床位费自动发送失败！"));
                        }

                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    }
                    #endregion
                }

                return responseData;
            }
            catch (Exception e)
            {
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 账单自动发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "账单自动发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                return null;
            }
        }
        #endregion

        #region 医嘱批量发送医嘱接口
        /// <summary>
        /// 医嘱批量发送医嘱接口  按patid获取可发送的GroupId
        /// </summary>
        /// <returns>医嘱发送结果</returns>
        [WCFMethod]
        public ServiceResponseData DeptSendOrderCheckList()
        {
            try
            {
                int iPatientID = requestData.GetData<int>(0);
                int iOrderCategory = requestData.GetData<int>(1);
                DateTime endTime = requestData.GetData<DateTime>(2);
                int iEmpID = requestData.GetData<int>(3);
                endTime = Convert.ToDateTime(endTime.ToString("yyyy-MM-dd 23:59:59"));
                //异常信息
                List<string> sOrderList = new List<string>();
                #region 1.发送医嘱
                //1.获取病人组号List
                List<int> iGroupIDList = new List<int>();
                DataTable dtOrderCheckGroupId = NewDao<IOrderCheckDao>().GetIPOrderCheckGroupListByPatListId(iPatientID, iOrderCategory, endTime);
                foreach (DataRow dr in dtOrderCheckGroupId.Rows)
                {
                    int iGroupId = Convert.ToInt32(dr["GroupID"]);
                    iGroupIDList.Add(iGroupId);
                }

                ResultClass resultClass = new ResultClass();
                //2.发送医嘱
                if (iGroupIDList.Count > 0)
                {
                    resultClass = NewDao<OrderCheckProcess>().AdviceExecute_Entrance(iGroupIDList, endTime, iEmpID);
                }
                else
                {
                    resultClass.bSucess = true;
                    resultClass.oResult = new List<IP_OrderCheckError>();
                }

                //3.转换返回结果集
                if (resultClass.bSucess)
                {
                    List<IP_OrderCheckError> orderCheckErrorList = (List<IP_OrderCheckError>)resultClass.oResult;
                    foreach (IP_OrderCheckError orderCheckError in orderCheckErrorList)
                    {
                        string s = "床号：" + orderCheckError.BedNo + " 组号：" + orderCheckError.GroupID + " 医嘱：" + orderCheckError.OrderName + " 信息：" + orderCheckError.ErrorMessage;
                        sOrderList.Add(s);
                    }
                }
                else
                {
                    sOrderList.Add(resultClass.sRemarks);
                }
                #endregion

                responseData.AddData(resultClass.bSucess);
                responseData.AddData(sOrderList);
                return responseData;
            }
            catch (Exception e)
            {
                responseData.AddData(e.Message);
                return responseData;
            }
        }

        /// <summary>
        /// 批量发送账单接口  按patid获取可发送的GroupId
        /// </summary>
        /// <returns>账单发送结果（包含错误消息）</returns>
        [WCFMethod]
        public ServiceResponseData DeptSendAccountCheckList()
        {
            //异常信息
            List<string> sAccountList = new List<string>();
            try
            {
                int iPatientID = requestData.GetData<int>(0);
                DateTime endTime = requestData.GetData<DateTime>(1);
                int empID = requestData.GetData<int>(2);

                //获取账单
                #region 1.账单记账
                bool b = false;
                DataTable dtFeeAccountID = NewDao<IOrderCheckDao>().GetFeeAccount(iPatientID, endTime);
                if (dtFeeAccountID != null && dtFeeAccountID.Rows.Count > 0)
                {
                    List<int> feeAccountIDList = new List<int>();
                    foreach (DataRow dr in dtFeeAccountID.Rows)
                    {
                        feeAccountIDList.Add(Convert.ToInt32(dr["GenerateID"]));
                    }

                    b = NewObject<DocOrderExpenseCheck>().FeeItemAccounting(feeAccountIDList, endTime, empID);
                }
                #endregion

                responseData.AddData(b);
                responseData.AddData(sAccountList);
                return responseData;
            }
            catch (Exception e)
            {
                sAccountList.Add("账单发送异常！" + e.Message);
                responseData.AddData(false);
                responseData.AddData(sAccountList);
                return responseData;
            }
        }

        /// <summary>
        /// 批量发送床位费接口  按patid获取可发送的GroupId
        /// </summary>
        /// <returns>批量发送结果</returns>
        [WCFMethod]
        public ServiceResponseData DeptSendBedCheckList()
        {
            //异常信息
            List<string> sAccountList = new List<string>();
            try
            {
                int iPatientID = requestData.GetData<int>(0);
                DateTime endTime = requestData.GetData<DateTime>(1);
                int empID = requestData.GetData<int>(2);
                bool b = false;
                #region 1.床位费记账
                DataTable bedPatientIDList = NewDao<IOrderCheckDao>().GetBedPatientIdList(iPatientID);
                if (bedPatientIDList != null)
                {
                    foreach (DataRow dr in bedPatientIDList.Rows)
                    {
                        b = NewObject<DocOrderExpenseCheck>().BedFeeAccounting(Convert.ToInt32(dr["PatListID"]), endTime, empID);
                    }
                }
                #endregion

                responseData.AddData(b);
                responseData.AddData(sAccountList);
                return responseData;
            }
            catch (Exception e)
            {
                sAccountList.Add("床位费发送异常！" + e.Message);
                responseData.AddData(false);
                responseData.AddData(sAccountList);
                return responseData;
            }
        }

        #endregion

        #region 按科室发送接口

        /// <summary>
        /// 科室医嘱发送接口  日期直接取当天24点  直接获取可发送的GroupId
        /// </summary>
        /// <returns>医嘱发送结果</returns>
        [WCFMethod]
        public ServiceResponseData AutoDeptSendOrderCheckList()
        {
            string sResult = "Falg:0";
            try
            {
                int iDeptID = requestData.GetData<int>(0);
                SetWorkId(1);
                DateTime endTime = DateTime.Now;
                List<int> iGroupIDList = new List<int>();
                DataTable dtOrderCheckGroupId = NewDao<IOrderCheckDao>().GetDeptIPOrderCheckGroupList(iDeptID, endTime);
                foreach (DataRow dr in dtOrderCheckGroupId.Rows)
                {
                    int iGroupId = Convert.ToInt32(dr["GroupID"]);
                    iGroupIDList.Add(iGroupId);
                }

                if (iGroupIDList.Count > 0)
                {
                    ResultClass resultClass = NewDao<OrderCheckProcess>().AdviceExecute_Entrance(iGroupIDList, endTime, 0);
                    if (resultClass.bSucess)
                    {
                        sResult = "Falg:0";
                        //将缺药等信息记录到日志
                        List<IP_OrderCheckError> orderCheckErrorList = (List<IP_OrderCheckError>)resultClass.oResult;
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Blue, DateTime.Now.ToString("yyyy-MM-dd") + " 科室医嘱发送成功！");
                        if (orderCheckErrorList.Count > 0)
                        {
                            MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "科室医嘱发送错误信息：");
                            foreach (IP_OrderCheckError orderCheckError in orderCheckErrorList)
                            {
                                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  床号：" + orderCheckError.BedNo + " 组号：" + orderCheckError.GroupID + " 医嘱：" + orderCheckError.OrderName + " 原因：" + orderCheckError.ErrorMessage);
                            }
                        }

                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    }
                    else
                    {
                        sResult = "Falg:1";
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 科室医嘱发送失败！");
                        //失败的话 记录错误日志
                        string sError = resultClass.sRemarks;
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "科室医嘱发送错误信息：");
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + sError);
                        MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                    }
                }

                responseData.AddData(sResult);
                return responseData;
            }
            catch (Exception e)
            {
                sResult = "Falg:1";
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, DateTime.Now.ToString("yyyy-MM-dd") + " 科室医嘱发送异常！");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "科室医嘱发送错误信息：");
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "  " + e.Message);
                MiddlewareLogHelper.WriterLog(LogType.TimingTaskLog, true, System.Drawing.Color.Red, "------------------------------------------------------------------------------------");
                responseData.AddData(sResult);
                return responseData;
            }
        }
        #endregion
    }
}
