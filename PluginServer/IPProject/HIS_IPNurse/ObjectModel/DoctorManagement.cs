using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_IPNurse.Dao;

namespace HIS_IPNurse.ObjectModel
{
    /// <summary>
    /// 医嘱转抄
    /// </summary>
    public class DoctorManagement : AbstractObjectModel
    {
        /// <summary>
        /// 保存医嘱费用数据
        /// </summary>
        /// <param name="saveDocFeeItemDt">待保存医嘱费用数据</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="groupID">组号ID</param>
        /// <param name="empId">操作员ID</param>
        /// <param name="deptID">科室ID</param>
        /// <returns>true：保存成功</returns>
        public bool SaveFeeItemData(DataTable saveDocFeeItemDt, int patListID, int groupID, int empId,int deptID)
        {
            if (saveDocFeeItemDt.Rows.Count > 0)
            {
                IP_FeeItemGenerate mIPFeeItemGenerate = NewObject<IP_FeeItemGenerate>();
                // 获取病人信息
                IP_PatList mPatlist = NewObject<IP_PatList>();
                DataTable patDt = NewDao<IDoctorManagementDao>().GetPatientInfo(Convert.ToInt32(saveDocFeeItemDt.Rows[0]["PatListID"]));
                mPatlist = ConvertExtend.ToObject<IP_PatList>(patDt, 0);
                // 获取医嘱信息
                IPD_OrderRecord mIPDOrderRecord = NewObject<IPD_OrderRecord>();
                DataTable docDt = NewDao<IDoctorManagementDao>().GetOrderRecord(patListID, groupID);
                mIPDOrderRecord = ConvertExtend.ToObject<IPD_OrderRecord>(docDt, 0);
                for (int i = 0; i < saveDocFeeItemDt.Rows.Count; i++)
                {
                    mIPFeeItemGenerate = ConvertExtend.ToObject<IP_FeeItemGenerate>(saveDocFeeItemDt, i);
                    if (mIPFeeItemGenerate.GenerateID == 0)
                    {
                        // 病人登记信息
                        mIPFeeItemGenerate.PatName = mPatlist.PatName;
                        mIPFeeItemGenerate.PatDeptID = mPatlist.CurrDeptID;
                        mIPFeeItemGenerate.PatDoctorID = mPatlist.CurrDoctorID;
                        mIPFeeItemGenerate.PatNurseID = mPatlist.CurrNurseID;
                        mIPFeeItemGenerate.BabyID = 0;
                        // 医嘱信息
                        mIPFeeItemGenerate.OrderID = mIPDOrderRecord.OrderID;
                        mIPFeeItemGenerate.GroupID = mIPDOrderRecord.GroupID;
                        mIPFeeItemGenerate.OrderType = mIPDOrderRecord.OrderCategory;// == 0 ? 0 : 3;
                        mIPFeeItemGenerate.FrequencyID = mIPDOrderRecord.FrenquencyID;
                        mIPFeeItemGenerate.FrequencyName = mIPDOrderRecord.Frequency;
                        mIPFeeItemGenerate.ChannelID = mIPDOrderRecord.ChannelID;
                        mIPFeeItemGenerate.ChannelName = mIPDOrderRecord.ChannelName;
                        mIPFeeItemGenerate.PresDoctorID = empId;
                        mIPFeeItemGenerate.PresDeptID = deptID;
                        mIPFeeItemGenerate.MarkEmpID = empId;
                    }
                    // 保存费用数据
                    this.BindDb(mIPFeeItemGenerate);
                    mIPFeeItemGenerate.save();
                }
            }

            return true;
        }

        /// <summary>
        /// 医嘱转抄
        /// </summary>
        /// <param name="arrayOrderID">待转抄医嘱列表</param>
        /// <param name="empId">转抄护士ID</param>
        /// <param name="arrayMsg">错误消息</param>
        public void CopiedDoctocList(List<string> arrayOrderID, int empId, List<string> arrayMsg)
        {
            StringBuilder strMsg = new StringBuilder();
            // 判断皮试医嘱是否已标记结果并且为阴性
            DataTable astFlagDt = NewDao<IDoctorManagementDao>().GetOrderRecordAstFlag(string.Join(",", arrayOrderID.ToArray()));
            if (astFlagDt != null && astFlagDt.Rows.Count > 0)
            {
                for (int i = 0; i < astFlagDt.Rows.Count; i++)
                {
                    // 如果皮试医嘱未标记结果或者结果为阳性，则同组医嘱都不能转抄
                    DataTable orderList = NewDao<IDoctorManagementDao>().GetOrderIDList(
                        Convert.ToInt32(astFlagDt.Rows[i]["PatListID"]), 
                        Convert.ToInt32(astFlagDt.Rows[i]["GroupID"]));
                    if (orderList != null && orderList.Rows.Count > 1)
                    {
                        for (int j = 0; j < orderList.Rows.Count; j++)
                        {
                            arrayOrderID.Remove(orderList.Rows[j]["OrderID"].ToString());
                        }
                    }
                    else
                    {
                        arrayOrderID.Remove(astFlagDt.Rows[i]["OrderID"].ToString());
                    }

                    arrayMsg.Add(astFlagDt.Rows[i]["ItemName"].ToString());
                }
            }

            if (arrayOrderID.Count > 0)
            {
                // 执行医嘱转抄
                NewDao<IDoctorManagementDao>().UpdateDocOrder(string.Join(",", arrayOrderID.ToArray()), empId);
            }
        }

        /// <summary>
        /// 标注皮试
        /// </summary>
        /// <param name="iOrder">医嘱ID</param>
        /// <param name="bIsPassed">是否通过（阴性）</param>
        /// <param name="iDeptId">科室ID</param>
        /// <param name="iEmpId">操作员ID</param>
        /// <returns>true：标记成功</returns>
        public bool CheckSkinTest(int iOrder, bool bIsPassed, int iDeptId, int iEmpId)
        {
            try
            {
                int iAstFlag= bIsPassed ? 1 : 2;
                IPD_OrderRecord orderRecord = NewObject<IPD_OrderRecord>().getmodel(iOrder) as IPD_OrderRecord;
                if (orderRecord != null)
                {
                    //1.获取父医嘱  判断时候已经转抄，已转抄的不允许再修改
                    IPD_OrderRecord orderRecordF = NewObject<IPD_OrderRecord>().getmodel(orderRecord.AstOrderID) as IPD_OrderRecord;
                    if (orderRecordF!=null && orderRecordF.OrderStatus < 2)
                    {
                        //2.修改皮试医嘱的皮试结果
                        orderRecord.AstFlag = iAstFlag;
                        orderRecord.ExecDate = System.DateTime.Now;
                        this.BindDb(orderRecord);
                        orderRecord.save();

                        //3.修改皮试医嘱对应的父医嘱的皮试结果                    
                        if (orderRecordF != null)
                        {
                            orderRecordF.AstFlag = iAstFlag;

                            this.BindDb(orderRecordF);
                            orderRecordF.save();
                        }
                        //List<IPD_OrderRecord> orderRecordFList = NewObject<IPD_OrderRecord>().getlist<IPD_OrderRecord>(" AstOrderID=" + iOrder + " and CancelFlag = 0 and DeleteFlag = 0");
                        //foreach (IPD_OrderRecord orderRecordF in orderRecordFList)
                        //{
                        //    orderRecordF.AstFlag = iAstFlag;

                        //    this.BindDb(orderRecordF);
                        //    orderRecordF.save();
                        //}

                        //4.修改或者增加皮试医嘱结果表信息
                        List<IPN_OrderAstResult> orderAstResultList = NewObject<IPN_OrderAstResult>().getlist<IPN_OrderAstResult>("OrderID =" + iOrder);
                        if (orderAstResultList.Count > 0)
                        {
                            foreach (IPN_OrderAstResult orderAstResult in orderAstResultList)
                            {
                                orderAstResult.AstResult = iAstFlag;
                                orderAstResult.ExecDeptID = iDeptId;
                                orderAstResult.ExecEmpID = iEmpId;
                                orderAstResult.ExecDate = System.DateTime.Now;

                                this.BindDb(orderAstResult);
                                orderAstResult.save();
                            }
                        }
                        else
                        {
                            IPN_OrderAstResult orderAstResult = NewObject<IPN_OrderAstResult>();
                            orderAstResult.OrderID = iOrder;
                            orderAstResult.AstResult = iAstFlag;
                            orderAstResult.ExecDeptID = iDeptId;
                            orderAstResult.ExecEmpID = iEmpId;
                            orderAstResult.ExecDate = System.DateTime.Now;
                            this.BindDb(orderAstResult);
                            orderAstResult.save();
                        }

                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}