using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPDoctor.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 处方处理对象
    /// </summary>
    [Serializable]
    public class PrescriptionProcess : AbstractObjectModel
    {
        /// <summary>
        /// 取得药品执行药房
        /// </summary>
        /// <param name="presType">0西药1中草药</param>
        /// <returns>药房数据</returns>
        public DataTable GetDrugStoreRoom(int presType)
        {
            return NewDao<IOPDDao>().GetDrugStoreRoom(presType);
        }

        /// <summary>
        /// 删除处方关联费用项目
        /// </summary>
        /// <param name="presDetailId">处方明细id</param>
        /// <returns>true成功</returns>
        public bool DeleteLinkFeeItems(int presDetailId)
        {
            bool bRtn = true;
            List<OPD_PresDetail> detailList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("LinkPresDetailID=" + presDetailId.ToString() );
            if (detailList.Count > 0)
            {
                foreach (OPD_PresDetail detail in detailList)
                {
                    if (detail.IsCharged == 0)
                    {
                        detail.delete();
                    }
                }
            }

            return bRtn;
        }

        /// <summary>
        /// 删除整张处方联动费用
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        public bool DeleteLinkFeeItems(int patListId, int presType, int presNo)
        {
            bool bRtn = true;
            OPD_PresHead presHead = NewObject<OPD_PresHead>().getlist<OPD_PresHead>("PatListID=" + patListId + " AND PresType=" + presType)[0];
            int presHeadId = presHead.PresHeadID;
            if (presHead.PresType == 1 || presHead.PresType == 2)
            {
                List<OPD_PresDetail> detailList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("PresHeadID=" + presHeadId.ToString() + " and PresNO=" + presNo);
                if (detailList.Count > 0)
                {
                    foreach (OPD_PresDetail pres in detailList)
                    {
                        bRtn = DeleteLinkFeeItems(pres.PresDetailID);
                    }
                }
            }

            return bRtn;
        }

        /// <summary>
        /// 保存联动费用项目
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="list">处方明细列表</param>
        /// <returns>true成功</returns>
        public bool SaveLinkFeeItems(int patListId, int presType, List<OPD_PresDetail> list)
        {
            if (presType == 1 || presType == 2)
            {
                //生成处方头
                int headId = NewDao<IOPDDao>().GetFeeHeadId(patListId);
                OPD_PresHead head = new OPD_PresHead();
                if (headId == 0)
                {
                }
                else
                {
                    head.PresHeadID = headId;
                }

                OP_PatList op = (OP_PatList)NewObject<OP_PatList>().getmodel(patListId);

                head.MemberID = op.MemberID;
                head.PatListID = patListId;
                head.PresType = 3;
                this.BindDb(head);
                head.save();
                int lastPresNo = -1;
                int lastPresGroupNo = -1;
                int lastChannelID = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    int presDetailId = list[i].PresDetailID;
                    int presNo = list[i].PresNO;
                    int presGroupNo = list[i].GroupID;
                    int channelID = list[i].ChannelID;
                    if (presType == 1)
                    {
                        if (presNo == lastPresNo && presGroupNo == lastPresGroupNo)
                        {
                            continue;
                        }
                    }
                    else if (presType == 2)
                    {
                        if (presNo == lastPresNo && channelID == lastChannelID)
                        {
                            continue;
                        }
                    }

                    InsertFeePresDetail(presType, head.PresHeadID, list[i]);
                    lastPresNo = presNo;
                    lastPresGroupNo = presGroupNo;
                    lastChannelID = channelID;
                }
            }

            return true;
        }

        /// <summary>
        /// 插入费用明细
        /// </summary>
        /// <param name="presType">处方类型</param>
        /// <param name="presHeadID">处方头ID</param>
        /// <param name="presDetail">处方明细实体</param>
        private void InsertFeePresDetail(int presType, int presHeadID, OPD_PresDetail presDetail)
        {
            Basic_Frequency frequency = (Basic_Frequency)NewObject<Basic_Frequency>().getmodel(presDetail.FrequencyID);
            int execNum, cycleDay;
            Calculate(frequency.ExecuteCode, out execNum, out cycleDay);
            //根据用法获取关联费用
            DataTable dtFeeItems = NewDao<IOPDDao>().GetChannelFees(presDetail.ChannelID);
            foreach (DataRow row in dtFeeItems.Rows)
            {
                int calCostMode = Convert.ToInt32(row["CalCostMode"]);
                int totalAmount = 0;
                if (presType == 2)
                {
                    totalAmount = presDetail.DoseNum;
                }
                else
                {
                    if (presDetail.ExecNum > 0)
                    {
                        totalAmount = presDetail.ExecNum * Convert.ToInt32(row["ItemAmount"]);
                    }
                    else
                    {
                        if (calCostMode == 0)
                        {
                            totalAmount = execNum * presDetail.Days * Convert.ToInt32(row["ItemAmount"]);
                        }
                        else
                        {
                            totalAmount = presDetail.Days * Convert.ToInt32(row["ItemAmount"]);
                        }
                    }
                }

                OPD_PresDetail modelDetail = new OPD_PresDetail();
                modelDetail.Price = Convert.ToDecimal(row["UnitPrice"].ToString());

                modelDetail.PresHeadID = presHeadID;
                modelDetail.ItemID = Convert.ToInt32(row["ItemID"]);
                modelDetail.ItemName = row["ItemName"].ToString();
                modelDetail.PresNO = presDetail.PresNO;
                modelDetail.GroupID = presDetail.GroupID;
                modelDetail.GroupSortNO = 1;
                modelDetail.StatID = Convert.ToInt32(row["StatID"]);
                modelDetail.Spec = row["spec"].ToString();
                modelDetail.Dosage = totalAmount;
                modelDetail.DosageUnit = row["UnPickUnit"].ToString();
                modelDetail.Factor = 1;
                modelDetail.FrequencyID = 0;
                modelDetail.ChannelID = 0;
                modelDetail.Entrust = string.Empty;
                modelDetail.DoseNum = 0;
                modelDetail.ChargeAmount = totalAmount;
                modelDetail.ChargeUnit = row["UnPickUnit"].ToString();
                modelDetail.Days = 1;
                modelDetail.DropSpec = string.Empty;
                modelDetail.IsAst = 0;
                modelDetail.AstResult = string.Empty;
                modelDetail.IsTake = 0;
                modelDetail.IsCharged = 0;
                modelDetail.IsCancel = 0;
                modelDetail.PresAmount = totalAmount;
                modelDetail.PresAmountUnit = row["UnPickUnit"].ToString();
                modelDetail.PresFactor = 1;
                modelDetail.ExecNum = Convert.ToInt32(row["ItemAmount"]);
                modelDetail.Memo = "关联费用";
                if (Convert.ToInt32(row["ExecDeptId"]) == 0)
                {
                    modelDetail.ExecDeptID = presDetail.PresDeptID;
                }
                else
                {
                    modelDetail.ExecDeptID = Convert.ToInt32(row["ExecDeptId"]);
                }

                modelDetail.IsEmergency = 0;
                modelDetail.IsLunacyPosion = 0;
                modelDetail.PresDate = presDetail.PresDate;
                modelDetail.PresDeptID = presDetail.PresDeptID;
                modelDetail.PresDoctorID = presDetail.PresDoctorID;
                modelDetail.LinkPresDetailID = presDetail.PresDetailID;
                BindDb(modelDetail);
                modelDetail.save();
            }
        }

        /// <summary>
        /// 更改处方注射次数
        /// </summary>
        /// <param name="presDetailId">处方明细Id</param>
        /// <param name="menuText">菜单显示名称</param>
        /// <param name="execTimes">执行次数</param>
        /// <returns>true成功</returns>
        public bool UpdatePresInjectTimes(int presDetailId, string menuText, int execTimes)
        {
            bool bRtn = true;
            OPD_PresDetail model = (OPD_PresDetail)NewObject<OPD_PresDetail>().getmodel(presDetailId);
            int presHeadId = model.PresHeadID;
            int presNo = model.PresNO;
            int presGroupNo = model.GroupID;
            List<OPD_PresDetail> presList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("PresHeadID=" + presHeadId + " and PresNO=" + presNo + " and GroupID=" + presGroupNo);
            foreach (OPD_PresDetail m in presList)
            {
                if (m.IsCharged == 0)
                {
                    m.Memo = menuText;
                    m.ExecNum = execTimes;
                    this.BindDb(model);
                    int iRtn = model.save();
                    //修改关联费用数量
                    List<OPD_PresDetail> feeList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("LinkPresDetailID=" + m.PresDetailID);
                    foreach (OPD_PresDetail feeItem in feeList)
                    {
                        feeItem.Dosage = m.ExecNum * feeItem.ExecNum;
                        feeItem.ChargeAmount = m.ExecNum * feeItem.ExecNum;
                        feeItem.PresAmount = m.ExecNum * feeItem.ExecNum;
                        this.BindDb(feeItem);
                        feeItem.save();
                    }
                }
            }

            return bRtn;
        }

        /// <summary>
        /// 计算频次
        /// </summary>
        /// <param name="caption">表达式</param>
        /// <param name="execNum">执行次数</param>
        /// <param name="cycleDay">周期天数</param>
        public void Calculate(string caption, out int execNum, out int cycleDay)
        {
            string[] freq_s = caption.Split(new char[] { '@' });
            if (freq_s[0] == "D")
            {
                //隔几天
                if (freq_s.Length == 2)
                {
                    execNum = 1;
                    cycleDay = Convert.ToInt32(freq_s[1]);
                }
                else if (freq_s.Length == 4)
                {
                    //几天几次
                    execNum = Convert.ToInt32(freq_s[2]);
                    cycleDay = Convert.ToInt32(freq_s[1]);
                }
                else
                {
                    execNum = 1;//执行次数
                    cycleDay = 1;//执行周期
                }
            }
            else
            {
                execNum = 1;//执行次数
                cycleDay = 1;//执行周期
            }
        }

        /// <summary>
        /// 处方模板另存为
        /// </summary>
        /// <param name="newMould">处方模板头信息</param>
        /// <param name="presList">处方明细信息</param>
        /// <returns>true成功</returns>
        public bool AsSavePresTemplate(OPD_PresMouldHead newMould, List<OPD_PresDetail> presList)
        {
            BindDb(newMould);
            int mouId = newMould.save();
            foreach (OPD_PresDetail pres in presList)
            {
                OPD_PresMouldDetail newDetail = new OPD_PresMouldDetail();
                OPD_PresDetail presDetail = NewDao<IOPDDao>().GetPresDetail(pres.PresDetailID);
                newDetail.PresMouldHeadID = mouId;
                newDetail.PresNO = 1;
                newDetail.Price = presDetail.Price;
                newDetail.ChargeAmount = presDetail.ChargeAmount;
                newDetail.ChannelID = presDetail.ChannelID;
                newDetail.Days = presDetail.Days;
                newDetail.Dosage = presDetail.Dosage;
                newDetail.DosageUnit = presDetail.DosageUnit;
                newDetail.DoseNum = presDetail.DoseNum;
                newDetail.Entrust = presDetail.Entrust;
                newDetail.ExecDeptID = presDetail.ExecDeptID;
                newDetail.Factor = presDetail.Factor;
                newDetail.FrequencyID = presDetail.FrequencyID;
                newDetail.GroupID = presDetail.GroupID;
                newDetail.GroupSortNO = presDetail.GroupSortNO;
                newDetail.ItemID = presDetail.ItemID;
                newDetail.ItemName = presDetail.ItemName;
                newDetail.PresAmount = presDetail.PresAmount;
                newDetail.PresAmountUnit = presDetail.PresAmountUnit;
                newDetail.PresFactor = presDetail.PresFactor;
                newDetail.Spec = presDetail.Spec;
                newDetail.StatID = presDetail.StatID;
                BindDb(newDetail);
                newDetail.save();
            }

            return true;
        }

        /// <summary>
        /// 一键复制就诊记录
        /// </summary>
        /// <param name="bDiseaseHis">病历</param>
        /// <param name="bWest">西药</param>
        /// <param name="bChinese">中草药</param>
        /// <param name="bFee">费用</param>
        /// <param name="currentPatId">当前病人Id</param>
        /// <param name="hisPatListId">历史病人Id</param>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室</param>
        /// <returns>true成功</returns>
        public bool OneCopy(bool bDiseaseHis, bool bWest, bool bChinese, bool bFee, int currentPatId, int hisPatListId, int presDoctorID, int presDeptID)
        {
            SysConfigManagement basic = NewObject<SysConfigManagement>();
            string regValidDays = basic.GetSystemConfigValue("RegValidPeriod");
            OP_PatList pat = NewObject<OP_PatList>().getmodel(currentPatId) as OP_PatList;
            DateTime regDate = pat.RegDate;
            DateTime tempDate = regDate.AddDays(Convert.ToInt32(regValidDays) - 1);
            if (tempDate < DateTime.Now)
            {
                throw new Exception("该病人超过挂号有效期不能复制");
            }

            //复制病历 
            if (bDiseaseHis)
            {
                CopyOMR(currentPatId, hisPatListId, presDoctorID, presDeptID);
            }

            if (bWest)
            {
                CopyPres(1, currentPatId, hisPatListId, presDoctorID, presDeptID);
            }

            if (bChinese)
            {
                CopyPres(2, currentPatId, hisPatListId, presDoctorID, presDeptID);
            }

            if (bFee)
            {
                CopyPres(3, currentPatId, hisPatListId, presDoctorID, presDeptID);
            }

            return true;
        }

        /// <summary>
        /// 拷贝病历
        /// </summary>
        /// <param name="currentPatId">当前病人id</param>
        /// <param name="hisPatListId">历史病人id</param>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室Id</param>
        private void CopyOMR(int currentPatId, int hisPatListId, int presDoctorID, int presDeptID)
        {
            //获取历史病人的病历
            List<OPD_MedicalRecord> omrList = NewObject<OPD_MedicalRecord>().getlist<OPD_MedicalRecord>("PatListID=" + hisPatListId);
            //存在病历记录再复制
            if (omrList.Count > 0)
            {
                //当前存在病历
                List<OPD_MedicalRecord> omrCurrList = NewObject<OPD_MedicalRecord>().getlist<OPD_MedicalRecord>("PatListID=" + currentPatId);
                OPD_MedicalRecord copyModel = NewObject<OPD_MedicalRecord>();
                
                //存在病历记录 更新
                if (omrCurrList.Count > 0)
                {
                    copyModel = omrCurrList[0];
                    copyModel.Symptoms = omrList[0].Symptoms;
                    copyModel.SicknessHistory = omrList[0].SicknessHistory;
                    copyModel.PhysicalExam = omrList[0].PhysicalExam;
                }                
                else 
                {
                    //不存在那么 创建
                    OP_PatList patModel = NewObject<OP_PatList>().getmodel(currentPatId) as OP_PatList;
                    copyModel.Symptoms = omrList[0].Symptoms;
                    copyModel.SicknessHistory = omrList[0].SicknessHistory;
                    copyModel.PhysicalExam = omrList[0].PhysicalExam;
                    copyModel.PatListID = currentPatId;
                    copyModel.PresDeptID = presDeptID;
                    copyModel.PresDoctorID = presDoctorID;
                    copyModel.MemberID = patModel.MemberID;
                }

                this.BindDb(copyModel);
                copyModel.save();
            }            
        }

        /// <summary>
        /// 拷贝处方
        /// </summary>
        /// <param name="presType">处方类型</param>
        /// <param name="currentPatId">当前病人id</param>
        /// <param name="hisPatListId">历史病人id</param>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室Id</param>
        /// <returns>true成功</returns>
        private bool CopyPres(int presType, int currentPatId, int hisPatListId, int presDoctorID, int presDeptID)
        {
            //检查历史是否有处方
            DataTable dtHisRecord = NewDao<IOPDDao>().GetPresInfo(presType, hisPatListId);
            if (dtHisRecord.Rows.Count > 0)
            {
                //检查当前是否有处方记录
                DataTable dtCurrentRecord = NewDao<IOPDDao>().GetPresInfo(presType, currentPatId);
                int currHeadId = 0;
                int hisHeadId = Convert.ToInt32(dtHisRecord.Rows[0]["PresHeadID"]);
                int currPresNo = 0;

                //判断当前病人是否有记录
                if (dtCurrentRecord.Rows.Count > 0)
                {
                    currHeadId = Convert.ToInt32(dtCurrentRecord.Rows[0]["PresHeadID"]);
                    currPresNo = Convert.ToInt32(dtCurrentRecord.Rows[dtCurrentRecord.Rows.Count - 1]["PresNO"]);
                }
                else
                {
                    //生成表头
                    OPD_PresHead presHisHead = NewObject<OPD_PresHead>().getmodel(Convert.ToInt32(dtHisRecord.Rows[0]["PresHeadID"])) as OPD_PresHead;
                    OPD_PresHead presCurrHead = new OPD_PresHead();
                    presCurrHead.MemberID = presHisHead.MemberID;
                    presCurrHead.PatListID = currentPatId;
                    presCurrHead.PresType = presType;
                    this.BindDb(presCurrHead);
                    presCurrHead.save();
                    currHeadId = presCurrHead.PresHeadID;
                    currPresNo = 0;
                }

                List<OPD_PresDetail> detailHisList = null;

                //费用
                if (presType == 3)
                {
                    detailHisList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("PresHeadID=" + hisHeadId + " and LinkPresDetailID=0");
                }
                else
                {
                    detailHisList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("PresHeadID=" + hisHeadId );
                }

                int i = 1;
                int diffVal = 0;

                //取得费用的最大处方号
                DataTable dtCurrentFeeRecord = NewDao<IOPDDao>().GetPresInfo(3, currentPatId);
                int maxFeePresNo = 0;
                if (presType == 1 || presType == 2)
                {
                    if (dtCurrentRecord.Rows.Count > 0)
                    {
                        maxFeePresNo = Convert.ToInt32(dtCurrentRecord.Rows[dtCurrentRecord.Rows.Count - 1]["PresNO"]);
                    }
                }

                foreach (OPD_PresDetail m in detailHisList)
                {
                    if (i == 1)
                    {
                        diffVal = m.PresNO - 1;
                    }

                    decimal unitPrice = 0;
                    if (presType == 1 || presType == 2)
                    {
                        //检查药品是否停用，和库存不足情况--直接过滤掉
                        DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(m.ItemID, m.ExecDeptID);
                        if (dtItems.Rows.Count <= 0)
                        {
                            continue;
                        }

                        bool bRtn = NewObject<IOPDDao>().IsDrugStore(m.ExecDeptID, m.ItemID, m.ChargeAmount);
                        if (bRtn == false)
                        {
                            continue;
                        }

                        unitPrice = Convert.ToDecimal(dtItems.Rows[0]["UnitPrice"]);
                    }
                    else
                    {
                        DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(m.ItemID, m.ExecDeptID);
                        if (dtItems.Rows.Count <= 0)
                        {
                            continue;
                        }

                        unitPrice = Convert.ToDecimal(dtItems.Rows[0]["UnitPrice"]);
                    }

                    OPD_PresDetail currDetailModel = new OPD_PresDetail();
                    this.BindDb(currDetailModel);
                    //生成处方明细
                    CreatePresDetail(presDoctorID, presDeptID, currHeadId, currPresNo, diffVal, m, unitPrice, currDetailModel, presType);
                    int newPresDetailId = currDetailModel.PresDetailID;
                    if (presType == 1 || presType == 2)
                    {
                        List<OPD_PresDetail> feeList = detailHisList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("LinkPresDetailID=" + m.PresDetailID);
                        if (feeList.Count > 0)
                        {
                            //生成关联动费用
                            CopyPresLinkFee(currentPatId, hisPatListId, presDoctorID, presDeptID, m.PresDetailID, newPresDetailId, maxFeePresNo);
                        }
                    }

                    i = i + 1;
                }
            }

            return true;
        }

        /// <summary>
        /// 生成明细
        /// </summary>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室Id</param>
        /// <param name="currHeadId">处方头</param>
        /// <param name="currPresNo">处方号</param>
        /// <param name="diffVal">处方号差值</param>
        /// <param name="m">模板</param>
        /// <param name="unitPrice">单位价格</param>
        /// <param name="currDetailModel">当前明细实体</param>
        /// <param name="presType">处方类型</param>
        private static void CreatePresDetail(int presDoctorID, int presDeptID, int currHeadId, int currPresNo, int diffVal, OPD_PresDetail m, decimal unitPrice, OPD_PresDetail currDetailModel,int presType)
        {
            currDetailModel.PresHeadID = currHeadId;
            currDetailModel.PresNO = currPresNo + m.PresNO - diffVal;
            currDetailModel.GroupID = m.GroupID;
            currDetailModel.GroupSortNO = m.GroupSortNO;
            currDetailModel.ItemID = m.ItemID;
            currDetailModel.ItemName = m.ItemName;
            currDetailModel.StatID = m.StatID;
            currDetailModel.Spec = m.Spec;
            currDetailModel.Dosage = m.Dosage;
            currDetailModel.DosageUnit = m.DosageUnit;
            currDetailModel.Factor = m.Factor;
            currDetailModel.ChannelID = m.ChannelID;
            currDetailModel.FrequencyID = m.FrequencyID;
            currDetailModel.Entrust = m.Entrust;
            currDetailModel.DoseNum = m.DoseNum;
            currDetailModel.ChargeAmount = m.ChargeAmount;
            currDetailModel.ChargeUnit = m.ChargeUnit;
            currDetailModel.Price = unitPrice;
            currDetailModel.Days = m.Days;
            currDetailModel.DropSpec = m.DropSpec;
            currDetailModel.IsAst = m.IsAst;
            currDetailModel.AstResult = string.Empty;
            currDetailModel.IsTake = 0;
            currDetailModel.IsCharged = 0;
            currDetailModel.IsCancel = 0;
            currDetailModel.PresAmount = m.PresAmount;
            currDetailModel.PresAmountUnit = m.PresAmountUnit;
            currDetailModel.PresFactor = m.PresFactor;
            currDetailModel.ExecNum = 0;
            currDetailModel.Memo = string.Empty;
            currDetailModel.PresDoctorID = presDoctorID;
            currDetailModel.PresDeptID = presDeptID;
            if (presType == 3)
            {
                currDetailModel.ExecDeptID = presDeptID;
            }
            else
            {
                currDetailModel.ExecDeptID = m.ExecDeptID;
            }

            currDetailModel.PresDate = DateTime.Now;
            currDetailModel.IsEmergency = m.IsEmergency;
            currDetailModel.IsLunacyPosion = m.IsLunacyPosion;
            currDetailModel.save();
        }

        /// <summary>
        /// 拷贝联动费用
        /// </summary>
        /// <param name="currentPatId">当前就诊病人Id</param>
        /// <param name="hisPatListId">历史病人Id</param>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室Id</param>
        /// <param name="oldLinkPresDetailId">老联动处方明细Id</param>
        /// <param name="newLinkPresDetailId">新联动处方明细Id</param>
        /// <param name="maxFeePresNo">最大的费用方号</param>
        /// <returns>true成功</returns>
        private bool CopyPresLinkFee(int currentPatId, int hisPatListId, int presDoctorID, int presDeptID,int oldLinkPresDetailId,int newLinkPresDetailId,int maxFeePresNo)
        {
            //检查历史是否有处方
            DataTable dtHisRecord = NewDao<IOPDDao>().GetPresInfo(3, hisPatListId);
            if (dtHisRecord.Rows.Count > 0)
            {
                //检查当前是否有处方记录
                DataTable dtCurrentRecord = NewDao<IOPDDao>().GetPresInfo(3, currentPatId);
                int currHeadId = 0;
                int hisHeadId = Convert.ToInt32(dtHisRecord.Rows[0]["PresHeadID"]);
                int currPresNo = 0;

                //存在取得原值
                if (dtCurrentRecord.Rows.Count > 0)
                {
                    currHeadId = Convert.ToInt32(dtCurrentRecord.Rows[0]["PresHeadID"]);
                    currPresNo = Convert.ToInt32(dtCurrentRecord.Rows[dtCurrentRecord.Rows.Count - 1]["PresNO"]);
                }
                else
                {
                    //不存在创建新值
                    OPD_PresHead presHisHead = NewObject<OPD_PresHead>().getmodel(Convert.ToInt32(dtHisRecord.Rows[0]["PresHeadID"])) as OPD_PresHead;
                    OPD_PresHead presCurrHead = new OPD_PresHead();
                    presCurrHead.MemberID = presHisHead.MemberID;
                    presCurrHead.PatListID = currentPatId;
                    presCurrHead.PresType = 3;
                    this.BindDb(presCurrHead);
                    presCurrHead.save();
                    currHeadId = presCurrHead.PresHeadID;
                    currPresNo = 0;
                }

                //保存明细
                SaveLinkPresDetail(presDoctorID, presDeptID, oldLinkPresDetailId, newLinkPresDetailId, maxFeePresNo, currHeadId, hisHeadId);
            }

            return true;
        }

        /// <summary>
        /// 保存联动明细
        /// </summary>
        /// <param name="presDoctorID">处方医生Id</param>
        /// <param name="presDeptID">处方科室Id</param>
        /// <param name="oldLinkPresDetailId">原处方明细Id</param>
        /// <param name="newLinkPresDetailId">新处方明细Id</param>
        /// <param name="maxFeePresNo">最大费用处方号</param>
        /// <param name="currHeadId">当前头Id</param>
        /// <param name="hisHeadId">历史头Id</param>
        private void SaveLinkPresDetail(int presDoctorID, int presDeptID, int oldLinkPresDetailId, int newLinkPresDetailId, int maxFeePresNo, int currHeadId, int hisHeadId)
        {
            List<OPD_PresDetail> detailHisList = detailHisList = NewObject<OPD_PresDetail>().getlist<OPD_PresDetail>("PresHeadID=" + hisHeadId + " and LinkPresDetailID=" + oldLinkPresDetailId);
            foreach (OPD_PresDetail m in detailHisList)
            {
                decimal unitPrice = 0;
                //DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(m.ItemID,m.ExecDeptID);
                //不能根据原有执行科室条件查询
                DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(m.ItemID, -1);
                if (dtItems.Rows.Count <= 0)
                {
                    continue;
                }

                unitPrice = Convert.ToDecimal(dtItems.Rows[0]["UnitPrice"]);
                OPD_PresDetail currDetailModel = new OPD_PresDetail();
                this.BindDb(currDetailModel);
                currDetailModel.PresHeadID = currHeadId;
                currDetailModel.PresNO = m.PresNO + maxFeePresNo;
                currDetailModel.GroupID = m.GroupID;
                currDetailModel.GroupSortNO = m.GroupSortNO;
                currDetailModel.ItemID = m.ItemID;
                currDetailModel.ItemName = m.ItemName;
                currDetailModel.StatID = m.StatID;
                currDetailModel.Spec = m.Spec;
                currDetailModel.Dosage = m.Dosage;
                currDetailModel.DosageUnit = m.DosageUnit;
                currDetailModel.Factor = m.Factor;
                currDetailModel.ChannelID = m.ChannelID;
                currDetailModel.FrequencyID = m.FrequencyID;
                currDetailModel.Entrust = m.Entrust;
                currDetailModel.DoseNum = m.DoseNum;
                currDetailModel.ChargeAmount = m.ChargeAmount;
                currDetailModel.ChargeUnit = m.ChargeUnit;
                currDetailModel.Price = unitPrice;
                currDetailModel.Days = m.Days;
                currDetailModel.DropSpec = m.DropSpec;
                currDetailModel.IsAst = m.IsAst;
                currDetailModel.AstResult = string.Empty;
                currDetailModel.IsTake = 0;
                currDetailModel.IsCharged = 0;
                currDetailModel.IsCancel = 0;
                currDetailModel.PresAmount = m.PresAmount;
                currDetailModel.PresAmountUnit = m.PresAmountUnit;
                currDetailModel.PresFactor = m.PresFactor;
                currDetailModel.ExecNum = 0;
                currDetailModel.Memo = string.Empty;
                currDetailModel.PresDoctorID = presDoctorID;
                currDetailModel.PresDeptID = presDeptID;
                currDetailModel.ExecDeptID = presDeptID;
                currDetailModel.PresDate = DateTime.Now;
                currDetailModel.IsEmergency = m.IsEmergency;
                currDetailModel.IsLunacyPosion = m.IsLunacyPosion;
                currDetailModel.LinkPresDetailID = newLinkPresDetailId;
                currDetailModel.save();
            }
        }

        /// <summary>
        /// 更新就诊医生
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="currentDocId">当前接诊医生Id</param>
        /// <param name="currentDocDeptId">当前接诊科室Id</param>
        /// <returns>true更新成功</returns>
        public bool UpdatePatCurrentDoctorID(int patListId, int currentDocId,int currentDocDeptId)
        {
            bool bRtn = true;

            //查询该病人是否生成处方，
            List<OPD_PresHead> presHeadList = NewObject<OPD_PresHead>().getlist<OPD_PresHead>("PatListID="+ patListId);
            
            //没有生成任何处方
            if (presHeadList.Count == 0)
            {
                OP_PatList pat = NewObject<OP_PatList>().getmodel(patListId) as OP_PatList;
                
                //更新挂号表当前病人的就诊医生Id
                if (pat.CureEmpID != currentDocId)
                {
                    pat.CureEmpID = currentDocId;
                    this.BindDb(pat);
                   int iRtn =  pat.save();
                    bRtn =  iRtn > 0 ? true : false;
                }
            }

            if (bRtn == true)
            {
                //更改病人信息表中的当前就诊科室
                OP_PatList patList = NewObject<OP_PatList>().getmodel(patListId) as OP_PatList;
              
                //更新挂号表当前病人的就诊医生Id
                if (patList.CureDeptID != currentDocDeptId)
                {
                    patList.CureDeptID = currentDocDeptId;
                    this.BindDb(patList);
                    int iRtn = patList.save();
                    bRtn = iRtn > 0 ? true : false;
                }
            }

            return bRtn;
        }
    }
}
