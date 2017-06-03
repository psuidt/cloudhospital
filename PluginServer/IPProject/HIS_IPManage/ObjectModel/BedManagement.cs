using System;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 床位
    /// </summary>
    public class BedManagement : AbstractObjectModel
    {
        /// <summary>
        /// 保存床位以及床位费用数据
        /// </summary>
        /// <param name="ipBedInfo">床位信息</param>
        /// <param name="bedFreeDt">床位费</param>
        /// <param name="isAddBed">是否为加床</param>
        /// <returns>错误消息</returns>
        public string SaveBedInfo(IP_BedInfo ipBedInfo, DataTable bedFreeDt, bool isAddBed)
        {
            bool isExistence = true; // 是否存在相同床位号标识
            if (!isAddBed)
            {
                // 查询同房间号下是否已存在相同的床位
                isExistence = NewDao<IIPManageDao>().IsExistenceCheck(ipBedInfo.WardID, ipBedInfo.RoomNO, ipBedInfo.BedNO);
            }
            // 不存在相同床位，保存新的床位数据
            if (isExistence)
            {
                // 保存床位信息
                this.BindDb(ipBedInfo);
                ipBedInfo.save();

                // 修改床位信息时，先删除数据库中现有的床位费用数据
                if (isAddBed)
                {
                    NewDao<IIPManageDao>().DeleteBedFreeList(ipBedInfo.BedID);
                }
                // 保存床位费用数据
                if (bedFreeDt.Rows.Count > 0)
                {
                    for (int i = 0; i < bedFreeDt.Rows.Count; i++)
                    {
                        IP_BedFee ipbedFree = ConvertExtend.ToObject<IP_BedFee>(bedFreeDt, i);
                        ipbedFree.ID = 0;
                        ipbedFree.BedID = ipBedInfo.BedID;
                        // 保存床位信息
                        this.BindDb(ipbedFree);
                        ipbedFree.save();
                    }
                }
            }
            else
            {
                // 存在相同的床位，提示Msg
                return "该床位号已存在，请重新输入！";
            }

            return string.Empty;
        }

        /// <summary>
        /// 停用或启用病床
        /// </summary>
        /// <param name="isStoped">状态</param>
        /// <param name="bedId">病床ID</param>
        /// <returns>错误消息</returns>
        public string StoppedOrEnabledBed(int isStoped, int bedId)
        {
            // 停用病床
            if (isStoped == 1)
            {
                // 检查病床是否已分配病人
                bool isExistence = NewDao<IIPManageDao>().BedIsUsed(bedId);
                if (isExistence)
                {
                    return "当前床位已分配病人，不能被停用！";
                }
            }
            // 停用或启用病床
            NewDao<IIPManageDao>().StoppedOrEnabledBed(isStoped, bedId);
            return string.Empty;
        }

        /// <summary>
        /// 保存床位分配数据
        /// </summary>
        /// <param name="tempDt">病人入院登记信息</param>
        /// <param name="bedId">床位ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <param name="empId">操作员ID</param>
        /// <returns>错误消息</returns>
        public string SaveBedAllocation(DataTable tempDt, int bedId, int wardId, string bedNo, int empId)
        {
            // 验证床位是否已被占用
            bool result = NewObject<IIPManageDao>().IsBedOccupy(bedId, 0, string.Empty);
            if (result)
            {
                int patStatus = Convert.ToInt32(tempDt.Rows[0]["Status"]);
                // 出院召回病人修改病人出院医嘱状态
                if (patStatus == 3)
                {
                    NewDao<IIPManageDao>().UpdatePatOrder(int.Parse(tempDt.Rows[0]["PatListID"].ToString()));
                }
                else if (patStatus == -1)
                {
                    // 转科病人，修改转科表数据
                    IPD_TransDept transDept = (IPD_TransDept)NewObject<IPD_TransDept>().getmodel(tempDt.Rows[0]["ID"]);
                    transDept.OperDate = DateTime.Now;
                    transDept.Operator = empId;
                    transDept.FinishFlag = 1;
                    this.BindDb(transDept);
                    transDept.save();
                }

                // 修改病人入院登记数据
                NewDao<IIPManageDao>().UpdatePatList(
                    bedNo,
                    int.Parse(tempDt.Rows[0]["CurrDoctorID"].ToString()),
                    int.Parse(tempDt.Rows[0]["CurrNurseID"].ToString()),
                    int.Parse(tempDt.Rows[0]["PatListID"].ToString()));

                // 修改床位数据
                NewDao<IIPManageDao>().UpdatePatBedInfo(
                    int.Parse(tempDt.Rows[0]["PatListID"].ToString()),
                    tempDt.Rows[0]["PatName"].ToString(),
                    tempDt.Rows[0]["Sex"].ToString(),
                    int.Parse(tempDt.Rows[0]["CurrDeptID"].ToString()),
                    int.Parse(tempDt.Rows[0]["CurrDoctorID"].ToString()),
                    int.Parse(tempDt.Rows[0]["CurrNurseID"].ToString()), 
                    bedId);

                // 新增床位分配日志数据
                IP_BedLog bedLog = new IP_BedLog();
                bedLog.BedID = bedId;  // 床位ID
                bedLog.BedNo = bedNo;
                bedLog.WardID = wardId;
                bedLog.PatListID = int.Parse(tempDt.Rows[0]["PatListID"].ToString());
                bedLog.PatName = tempDt.Rows[0]["PatName"].ToString();
                bedLog.PatSex = tempDt.Rows[0]["Sex"].ToString();
                bedLog.PatDeptID = int.Parse(tempDt.Rows[0]["CurrDeptID"].ToString());
                bedLog.PatDoctorID = int.Parse(tempDt.Rows[0]["CurrDoctorID"].ToString());
                bedLog.PatNurseID = int.Parse(tempDt.Rows[0]["CurrNurseID"].ToString());
                bedLog.AssignDate = DateTime.Now;
                bedLog.AssignEmpID = empId;
                this.BindDb(bedLog);
                bedLog.save();

                // 生成床位费用
                SaveBedFeeData(bedId, bedLog.PatListID, false, empId);
            }
            else
            {
                return "当前床位已分配病人或被停用，请重新选择床位！";
            }

            return string.Empty;
        }

        /// <summary>
        /// 生成病人床位费
        /// </summary>
        /// <param name="bedId">病床ID</param>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="isPackBed">是否为包床费用</param>
        /// <param name="empId">操作员ID</param>
        private void SaveBedFeeData(int bedId, int patListId, bool isPackBed, int empId)
        {
            // 获取床位费用
            DataTable tempDt = null;
            if (isPackBed)
            {
                // 包床获取包床床位费用
                tempDt = NewDao<IIPManageDao>().GetBedFeeItemList(bedId, 1);
            }
            else
            {
                // 不是包床，获取病床包床费用之外的床位费
                tempDt = NewDao<IIPManageDao>().GetBedFeeItemList(bedId, 0);
            }

            if (tempDt != null && tempDt.Rows.Count > 0)
            {
                // 查询病人登记信息
                DataTable patDt = NewDao<IIPManageDao>().GetPatListInfo(patListId);
                IP_PatList pat = ConvertExtend.ToObject<IP_PatList>(patDt, 0);
                for (int i = 0; i < tempDt.Rows.Count; i++)
                {
                    IP_FeeItemGenerate feeItem = NewObject<IP_FeeItemGenerate>();
                    feeItem.PatListID = pat.PatListID;
                    feeItem.PatName = pat.PatName;
                    feeItem.PatDeptID = pat.CurrDeptID;
                    feeItem.PatDoctorID = pat.CurrDoctorID;
                    feeItem.PatNurseID = pat.CurrNurseID;
                    feeItem.BabyID = 0;
                    if (DBNull.Value == tempDt.Rows[i]["ItemID"])
                    {
                        continue;
                    }
                    feeItem.ItemID = Convert.ToInt32(tempDt.Rows[i]["ItemID"]);
                    feeItem.ItemName = tempDt.Rows[i]["ItemName"].ToString();
                    feeItem.FeeClass = Convert.ToInt32(tempDt.Rows[i]["ItemClass"]);
                    feeItem.StatID = Convert.ToInt32(tempDt.Rows[i]["StatID"]);
                    feeItem.Spec = tempDt.Rows[i]["Standard"].ToString();
                    feeItem.PackAmount = Convert.ToInt32(tempDt.Rows[i]["MiniConvertNum"]);
                    feeItem.PackUnit = tempDt.Rows[i]["UnPickUnit"].ToString();
                    feeItem.InPrice = Convert.ToDecimal(tempDt.Rows[i]["InPrice"]);
                    feeItem.SellPrice = Convert.ToDecimal(tempDt.Rows[i]["SellPrice"]);
                    feeItem.Amount = Convert.ToInt32(tempDt.Rows[i]["ItemAmount"]);
                    feeItem.Unit = tempDt.Rows[i]["MiniUnitName"].ToString();
                    feeItem.DoseAmount = 0;
                    feeItem.TotalFee = Math.Round(feeItem.Amount * Convert.ToDecimal(tempDt.Rows[i]["UnitPrice"]), 2);
                    feeItem.PresDeptID = pat.CurrDeptID;
                    feeItem.PresDoctorID = pat.CurrDoctorID;
                    feeItem.ExecDeptDoctorID = pat.CurrDeptID;
                    feeItem.PresDate = DateTime.Now;
                    feeItem.MarkDate = DateTime.Now;
                    feeItem.MarkEmpID = empId;
                    feeItem.SortOrder = 0;
                    feeItem.OrderID = 0;
                    feeItem.GroupID = 0;
                    feeItem.OrderType = 2;
                    feeItem.FrequencyID = 0;
                    feeItem.FrequencyName = string.Empty;
                    feeItem.ChannelID = 0;
                    feeItem.ChannelName = string.Empty;
                    feeItem.IsStop = 0;
                    feeItem.FeeSource = 4;
                    feeItem.CalCostMode = 0;
                    feeItem.BedID = bedId;
                    this.BindDb(feeItem);
                    feeItem.save();
                }
            }
        }

        /// <summary>
        /// 取消分床
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>取消成功或失败</returns>
        public bool CancelTheBed(int patListId, int wardId, string bedNo)
        {
            // 需要验证床位是否已产生费用
            bool result = NewDao<IIPManageDao>().CheckPatIsCostIncurred(patListId);
            if (!result)
            {
                // 取消分床修改床位信息
                NewDao<IIPManageDao>().CancelBedUpdIpBedInfo(wardId, bedNo);
                // 取消分床修改病人登记信息
                NewDao<IIPManageDao>().CancelBedUpdIpPatList(patListId);
                DataTable bedDt = NewDao<IIPManageDao>().GetBedInfoId(wardId, bedNo);
                // 取消分床时停用账单
                NewDao<IIPManageDao>().StopBedFee(patListId, Convert.ToInt32(bedDt.Rows[0]["BedID"]));
                return true;
            }

            return false;
        }

        /// <summary>
        /// 取消包床
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">床位号</param>
        /// <returns>错误消息</returns>
        public string CancelPackBed(int patListId, int wardId, string bedNo)
        {
            // 验证床位是否为包床
            DataTable packDt = NewDao<IIPManageDao>().CheckBedIsPack(patListId, wardId, bedNo);
            if (packDt != null && packDt.Rows.Count > 0)
            {
                // 包床床位,可以取消
                if (Convert.ToInt32(packDt.Rows[0]["IsPack"]) == 1)
                {
                    NewDao<IIPManageDao>().CancelBedUpdIpBedInfo(wardId, bedNo);
                    DataTable bedDt = NewDao<IIPManageDao>().GetBedInfoId(wardId, bedNo);
                    // 取消包床时停用账单
                    NewDao<IIPManageDao>().StopBedFee(patListId, Convert.ToInt32(bedDt.Rows[0]["BedID"]));
                }
                else
                {
                    // 否则不能取消
                    return "取消包床失败，当前病床为病人入院分配病床！";
                }

                return string.Empty;
            }
            else
            {
                return "当前床位不存在或床位未分配病人！";
            }
        }

        /// <summary>
        /// 保存换床数据
        /// </summary>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardID">病区ID</param>
        /// <param name="oldBedNo">旧床号</param>
        /// <param name="empId">操作人ID</param>
        /// <param name="workID">机构ID</param>
        /// <returns>错误消息</returns>
        public string SaveBedChanging(string newBedNo, int patListId, int wardID, string oldBedNo, int empId, int workID)
        {
            // 验证新床位是否被占用
            bool result = NewObject<IIPManageDao>().IsBedOccupy(0, wardID, newBedNo);
            if (result)
            {
                // 修改新分配病床信息
                NewDao<IIPManageDao>().UpdateNewBed(wardID, newBedNo, oldBedNo, 0);
                
                // 记录床位修改日志
                NewDao<IIPManageDao>().SaveNewBedLogInfo(wardID, oldBedNo, newBedNo, empId, workID);
                
                // 修改旧床位信息
                NewDao<IIPManageDao>().CancelBedUpdIpBedInfo(wardID, oldBedNo);
                
                // 修改病人登记信息
                NewDao<IIPManageDao>().PatBedChanging(patListId, newBedNo);

                DataTable oldBedDt = NewDao<IIPManageDao>().GetBedInfoId(wardID, oldBedNo);
                // 停用旧床位的费用数据
                NewDao<IIPManageDao>().StopBedFee(patListId, Convert.ToInt32(oldBedDt.Rows[0]["BedID"]));

                DataTable NewBedDt = NewDao<IIPManageDao>().GetBedInfoId(wardID, newBedNo);
                // 生成新床位的费用数据
                if (NewBedDt != null && NewBedDt.Rows.Count > 0)
                {
                    SaveBedFeeData(Convert.ToInt32(NewBedDt.Rows[0]["BedID"]), patListId, false, empId);
                }
            }
            else
            {
                return "床位上已分配了病人，请选择新的床位！";
            }

            return string.Empty;
        }

        /// <summary>
        /// 修改医生或护士--保存新的医生护士数据
        /// </summary>
        /// <param name="patListId">病人登记ID</param>
        /// <param name="wardId">病区ID</param>
        /// <param name="bedNo">病床号</param>
        /// <param name="doctorid">医生ID</param>
        /// <param name="nurseid">护士ID</param>
        /// <returns>true:修改成功</returns>
        public bool SaveUpdatePatient(int patListId, int wardId, string bedNo, int doctorid, int nurseid)
        {
            // 保存病人登记数据
            bool result = NewDao<IIPManageDao>().UpdatePatListDoctor(patListId, doctorid, nurseid);
            // 判断病人是否有包床
            DataTable patPackBedListDt = NewDao<IIPManageDao>().GetPatPackBedList(patListId);
            // 修改病人所有床位数据
            if (patPackBedListDt != null && patPackBedListDt.Rows.Count > 0)
            {
                for (int i = 0; i < patPackBedListDt.Rows.Count; i++)
                {
                    NewDao<IIPManageDao>().UpdateBedDoctor(doctorid, nurseid, int.Parse(patPackBedListDt.Rows[i]["BedID"].ToString()));
                }
            }

            return result;
        }

        /// <summary>
        /// 保存包床数据
        /// </summary>
        /// <param name="newBedNo">新床位号</param>
        /// <param name="wardID">病区ID</param>
        /// <param name="oldBedNo">旧床号</param>
        /// <param name="empId">操作人ID</param>
        /// <param name="workID">机构ID</param>
        /// <returns>true：保存成功</returns>
        public string SavePackBedData(string newBedNo, int wardID, string oldBedNo, int empId, int workID)
        {
            // 验证新床位是否被占用
            bool result = NewObject<IIPManageDao>().IsBedOccupy(0, wardID, newBedNo);
            if (result)
            {
                // 修改新分配病床信息
                NewDao<IIPManageDao>().UpdateNewBed(wardID, newBedNo, oldBedNo, 1);
                // 记录床位修改日志
                NewDao<IIPManageDao>().SaveNewBedLogInfo(wardID, oldBedNo, newBedNo, empId, workID);

                // 生成新床位的费用数据
                DataTable bedDt = NewDao<IIPManageDao>().GetBedInfoId(wardID, newBedNo);
                if (bedDt != null && bedDt.Rows.Count > 0)
                {
                    int bedId = Convert.ToInt32(bedDt.Rows[0]["BedID"]);
                    SaveBedFeeData(bedId, Convert.ToInt32(bedDt.Rows[0]["PatListID"]), true, empId);
                }
            }
            else
            {
                return "床位上已分配了病人，请选择新的床位！";
            }

            return string.Empty;
        }

        /// <summary>
        /// 定义病人出院
        /// </summary>
        /// <param name="wardId">病区ID</param>
        /// <param name="patListID">病人登记ID</param>
        /// <param name="checkFlg">是否检查通过</param>
        /// <param name="deptID">转科新科室Id</param>
        /// <returns>出院报表数据</returns>
        public DataTable PatientOutHospital(int wardId, int patListID, out bool checkFlg, int deptID)
        {
            bool isNotStopOrder = true;
            DataTable resultDt = new DataTable();
            // 检查所有医嘱是否已停
            DataTable notExecOrder = NewDao<IIPManageDao>().GetNotExecOrder(patListID);
            if (notExecOrder.Rows.Count > 0)
            {
                resultDt.Merge(notExecOrder);
                isNotStopOrder = false;
            }

            // 检查所有账单有没有停用
            DataTable notStopOrderDt = NewDao<IIPManageDao>().GetNotStopOrder(patListID);
            if (notStopOrderDt.Rows.Count > 0)
            {
                resultDt.Merge(notStopOrderDt);
                isNotStopOrder = false;
            }

            // 检查所有药品是否已统领
            DataTable notGuideOrderDt = NewDao<IIPManageDao>().GetNotGuideOrder(patListID);
            if (notGuideOrderDt.Rows.Count > 0)
            {
                resultDt.Merge(notGuideOrderDt);
                isNotStopOrder = false;
            }

            // 取系统参数验证是否允许没发完药可以出院
            string result = NewObject<SysConfigManagement>().GetSystemConfigValue("IsMedicine");
            if (result == "1")
            {
                // 检查所有统领药品是否已发药 （根据参数配置是否需要验证）
                DataTable notDispDrugDt = NewDao<IIPManageDao>().GetNotDispDrugList(patListID);
                if (notDispDrugDt.Rows.Count > 0)
                {
                    resultDt.Merge(notDispDrugDt);
                    isNotStopOrder = false;
                }
            }

            if (!isNotStopOrder)
            {
                checkFlg = false;
                return resultDt;
            }

            // 清空床位信息
            NewDao<IIPManageDao>().PatOutHospitalUpdateBedData(wardId, patListID);
            if (deptID == 0)
            {
                // 修改病人状态、出院时间
                NewDao<IIPManageDao>().PatOutHospitalUpdatePatListData(patListID);
                // 获取出院通知单基本数据
                DataTable patListDt = NewDao<IIPManageDao>().GetPatOutHospitalData(patListID);
                resultDt.Merge(patListDt);
            }
            else
            {
                // 修改病人当前科室
                IPD_TransDept transDept = (IPD_TransDept)NewObject<IPD_TransDept>().getmodel(patListID);
                NewDao<IIPManageDao>().UpdatepatCurrDept(patListID, deptID);
            }

            checkFlg = true;
            return resultDt;
        }
    }
}
