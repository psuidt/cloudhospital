using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_Entity.MIManage.Common;
using HIS_ThatFee.Dao;

namespace HIS_ThatFee.ObjectModel
{
    /// <summary>
    /// 确费管理
    /// </summary>
    public class ThatFee : AbstractObjectModel
    {
        #region 医技确费
        /// <summary>
        /// 确费操作
        /// </summary>
        /// <param name="ids">需要确费处方明细ID</param>
        /// <param name="empId">确费医生ID</param>
        /// <param name="empName">确费医生名称</param>
        /// <param name="systemType">0门诊 1住院</param>
        /// <returns>返回操作异常或成功消息</returns>
        public string ThatFees(string ids, int empId, string empName, int systemType)
        {
            StringBuilder result = new StringBuilder();
            DataTable dtInfo = NewDao<IThatFeeDao>().ConfigInfo(ids);//获取医技项目详细信息
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                decimal totalFee = Convert.ToDecimal(dtInfo.Rows[i]["TotalFee"]);
                int presId = Convert.ToInt32(dtInfo.Rows[i]["PresDetailID"]);
                if (totalFee <= 0)
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：金额为0不能确费\n");
                    continue;
                }

                if (systemType == 0 && !GetPayFlag(presId))
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：该项目已申请退费\n");
                    continue;
                }

                if (Convert.ToString(dtInfo.Rows[i]["IsReturns"]) != "0")
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：该项目已退款\n");
                    continue;
                }

                if (Convert.ToString(dtInfo.Rows[i]["ApplyStatus"]) != "1")
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：不能重复确费\n");
                    continue;
                }

                EXA_MedicalConfir medical = NewDao<IThatFeeDao>().GetConfir(presId);
                if (medical != null && medical.CancelFlag == 0)
                {
                    medical.IsCancel = 1;
                    BindDb(medical);
                    medical.save();
                }

                int meid = 0;
                NewDao<IThatFeeDao>().UpdateStatus(presId, 2, 1, systemType);//将费用表以及医技表改为确费状态
                EXA_MedicalConfir medicalconfir = GetMedical(presId, systemType, empId, empName);
                meid = SaveFeeHead(medicalconfir, dtInfo.Rows[i], 1);//插入确费记录
                DataTable dtfee = GetFeeDt(systemType, presId);
                for (int j = 0; j < dtfee.Rows.Count; j++)
                {
                    EXA_MedicalConfirDetail medicaldetail = new EXA_MedicalConfirDetail();
                    medicaldetail.ConfirID = meid;
                    medicaldetail.ExamItemID = medicalconfir.ItemID;
                    medicaldetail.MarkFlag = systemType;
                    SaveFeeDetail(medicaldetail, dtfee.Rows[j], 0, systemType);//插入确费费用明细记录
                }

                result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：确费成功\n");
            }

            return result.ToString();
        }

        /// <summary>
        /// 取消确费操作
        /// </summary>
        /// <param name="ids">需要确费处方明细ID</param>
        /// <param name="empId">确费医生ID</param>
        /// <param name="empName">确费医生名称</param>
        /// <param name="systemType">0门诊 1住院</param>
        /// <returns>返回取消确费成功或异常消息</returns>
        public string CancelThatFees(string ids, int empId, string empName, int systemType)
        {
            StringBuilder result = new StringBuilder();
            DataTable dtInfo = NewDao<IThatFeeDao>().ConfigInfo(ids);//获取医技项目详细信息
            for (int i = 0; i < dtInfo.Rows.Count; i++)
            {
                if (Convert.ToString(dtInfo.Rows[i]["ApplyStatus"]) != "2")
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：取消确费失败，原因：该项目还未确费\n");
                    continue;
                }

                int presId = Convert.ToInt32(dtInfo.Rows[i]["PresDetailID"]);
                EXA_MedicalConfir medical = NewDao<IThatFeeDao>().GetConfir(presId);
                if (medical.CancelFlag != 1)
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：取消确费失败，原因：不能重复取消确费\n");
                    continue;
                }

                if (medical.ConfirDoctorID != empId)
                {
                    result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：取消确费失败，原因：不能取消其他医生的确费记录\n");
                    continue;
                }

                if (medical != null)
                {
                    medical.IsCancel = 1;
                    BindDb(medical);
                    medical.save();
                }

                int meid = 0;
                //将费用表以及医技表改为取消确费状态
                NewDao<IThatFeeDao>().UpdateStatus(presId, 1, 0, systemType);
                EXA_MedicalConfir medicalconfir = GetMedical(presId, systemType, empId, empName);
                meid = SaveFeeHead(medicalconfir, dtInfo.Rows[i], 0);
                List<EXA_MedicalConfirDetail> detailList = NewDao<IThatFeeDao>().GetConfirDetailList(medical.ConfirID);//获取确费明细记录并插入数量和金额为负的记录
                foreach (EXA_MedicalConfirDetail medicaldetail in detailList)
                {
                    medicaldetail.ConfirID = meid;
                    medicaldetail.ExamItemID = medicalconfir.ItemID;
                    medicaldetail.MarkFlag = systemType;
                    SaveFeeDetail(medicaldetail, null, 1, systemType);
                }

                result.Append("项目：" + Convert.ToString(dtInfo.Rows[i]["ItemName"]) + "，结果：取消确费成功\n");
            }

            return result.ToString();
        }

        /// <summary>
        /// 检查该记录是否已经确费
        /// </summary>
        /// <param name="presId">处方明细ID</param>
        /// <returns>返回true存在</returns>
        private bool IsExist(int presId)
        {
            EXA_MedicalConfir medical = NewDao<IThatFeeDao>().GetConfir(presId);
            if (medical != null)
            {
                if (medical.CancelFlag == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 生成费用明细
        /// </summary>
        /// <param name="medicaldetail">确费明细实体</param>
        /// <param name="dr">数据行</param>
        /// <param name="type">0新增1修改</param>
        /// <param name="systemType">0门诊 1住院</param>
        private void SaveFeeDetail(EXA_MedicalConfirDetail medicaldetail, DataRow dr, int type, int systemType)
        {
            if (type == 0)
            {
                if (systemType == 0)
                {
                    medicaldetail.FeeDetailID = Convert.ToInt32(dr["PresDetailID"]);
                    medicaldetail.ItemPrice = Convert.ToDecimal(dr["RetailPrice"]);
                    medicaldetail.Unit = Convert.ToString(dr["MiniUnit"]);
                }
                else
                {
                    medicaldetail.FeeDetailID = Convert.ToInt32(dr["OrderID"]);
                    medicaldetail.ItemPrice = Convert.ToDecimal(dr["SellPrice"]);
                    medicaldetail.Unit = Convert.ToString(dr["Unit"]);
                }

                medicaldetail.ItemID = Convert.ToInt32(dr["ItemID"]);
                medicaldetail.ItemName = Convert.ToString(dr["ItemName"]);
                medicaldetail.TotalFee = Convert.ToDecimal(dr["TotalFee"]);
                medicaldetail.Amount = Convert.ToInt32(dr["Amount"]);
            }
            else
            {
                medicaldetail.ConfirDetailID = 0;
                medicaldetail.TotalFee = -medicaldetail.TotalFee;
                medicaldetail.Amount = -medicaldetail.Amount;
            }

            BindDb(medicaldetail);
            medicaldetail.save();
        }

        /// <summary>
        /// 生成费用头表
        /// </summary>
        /// <param name="medicalconfir">确费实体</param>
        /// <param name="dr">申请数据行数据</param>
        /// <param name="type">取消确费标识0取消1确费</param>
        /// <returns>确费id</returns>
        private int SaveFeeHead(EXA_MedicalConfir medicalconfir, DataRow dr, int type)
        {
            medicalconfir.ApplyType = Convert.ToInt32(dr["ApplyType"]);
            medicalconfir.CancelFlag = type;
            medicalconfir.IsCancel = 0;
            medicalconfir.ConfirDate = DateTime.Now;
            medicalconfir.ExamTypeID = Convert.ToInt32(dr["ExamTypeID"]);
            medicalconfir.Fee = Convert.ToInt32(dr["TotalFee"]);
            medicalconfir.ItemID = Convert.ToInt32(dr["ItemID"]);
            medicalconfir.ItemName = Convert.ToString(dr["ItemName"]);
            medicalconfir.MemberID = Convert.ToInt32(dr["MemberID"]);
            medicalconfir.PatListID = Convert.ToInt32(dr["PatListID"]);
            medicalconfir.ConfirDeptID = Convert.ToInt32(dr["ExecuteDeptID"]);
            medicalconfir.ConfirDeptName = Convert.ToString(dr["DeptName"]);
            BindDb(medicalconfir);
            medicalconfir.save();
            return medicalconfir.ConfirID;
        }

        /// <summary>
        /// 获取是否申请退款
        /// </summary>
        /// <param name="presId">处方ID</param>
        /// <returns>是否退款true是</returns>
        private bool GetPayFlag(int presId)
        {
            DataTable dt = NewDao<IThatFeeDao>().GetPayFlag(presId);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0] == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (dt.Rows[0][0].ToString() == string.Empty)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 获取医技费用明细
        /// </summary>
        /// <param name="systemType">0门诊 1住院</param>
        /// <param name="presId">明细id</param>
        /// <returns>医技费用明细数据</returns>
        private DataTable GetFeeDt(int systemType, int presId)
        {
            DataTable dtfee = new DataTable();
            if (systemType == 0)
            {
                dtfee = NewDao<IThatFeeDao>().GetOPFee(presId);//获取门诊项目的费用明细记录
            }
            else
            {
                dtfee = NewDao<IThatFeeDao>().GetIPFee(presId);//获取住院项目的费用明细记录
            }

            return dtfee;
        }

        /// <summary>
        /// 获取要保存确费对象
        /// </summary>
        /// <param name="presId">明细ID</param>
        /// <param name="systemType">类型 0门诊 1住院</param>
        /// <param name="empId">医生ID</param>
        /// <param name="empName">医生名称</param>
        /// <returns>确费实体对象</returns>
        private EXA_MedicalConfir GetMedical(int presId, int systemType, int empId, string empName)
        {
            EXA_MedicalConfir medicalconfir = new EXA_MedicalConfir();
            medicalconfir.PresDetailID = presId;
            medicalconfir.MarkFlag = systemType;
            medicalconfir.ConfirDoctorID = empId;
            medicalconfir.ConfirDoctorName = empName;
            return medicalconfir;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="systemtype">0门诊1住院</param>
        /// <param name="clincDeptId">临床科室id</param>
        /// <param name="isCheck">是否检查项目</param>
        /// <param name="isTest">是否化验项目</param>
        /// <param name="isTreat">是否治疗项目</param>
        /// <param name="isNotThatFee">未收费</param>
        /// <param name="isThatFee">已收费</param>
        /// <param name="strNO">单据号</param>
        /// <returns>查询字符串</returns>
        public StringBuilder GetQueryWhere(string deptId, int systemtype, string clincDeptId, bool isCheck, bool isTest, bool isTreat, bool isNotThatFee, bool isThatFee, string strNO)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(string.Format(" AND b.SystemType={0}", systemtype));
            if (!string.IsNullOrEmpty(deptId))
            {
                strBuilder.Append(string.Format(" AND b.ExecuteDeptID={0}", deptId));
            }

            if (!string.IsNullOrEmpty(clincDeptId))
            {
                strBuilder.Append(string.Format(" AND b.ApplyDeptID={0}", clincDeptId));
            }

            string typeStr = string.Empty;
            if (isCheck)
            {
                typeStr += "(b.ApplyType=0";
            }

            if (isTest)
            {
                typeStr += typeStr == string.Empty ? "(b.ApplyType=1" : " OR b.ApplyType=1";
            }

            if (isTreat)
            {
                typeStr += typeStr == string.Empty ? "(b.ApplyType=2" : " OR b.ApplyType=2";
            }

            if (!string.IsNullOrEmpty(typeStr))
            {
                typeStr += ")";
                strBuilder.Append(" AND " + typeStr);
            }

            string applyStr = string.Empty;
            if (isNotThatFee)
            {
                applyStr = "((a.IsReturns=0 AND a.ApplyStatus=1)";
            }

            if (isThatFee)
            {
                applyStr += applyStr == string.Empty ? "(a.ApplyStatus=2" : " OR a.ApplyStatus=2";
            }

            if (!string.IsNullOrEmpty(applyStr))
            {
                applyStr += ")";
                strBuilder.Append(" AND " + applyStr);
            }

            if (!string.IsNullOrEmpty(strNO))
            {
                strBuilder.Append(string.Format(" AND b.ApplyHeadID={0}", strNO));
            }

            return strBuilder;
        }
        #endregion

        #region 医技工作量统计
        /// <summary>
        /// 获取医技工作量统计
        /// </summary>
        /// <param name="confirDeptID">确费科室id</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>医技工作量统计数据</returns>
        public DataTable GetThatFeeCount(string confirDeptID, string beginDate, string endDate)
        {
            int iPCount = 0;
            decimal iPFee = 0;
            int oPCount = 0;
            decimal oPFee = 0;
            DataTable newdt = new DataTable();
            newdt.Columns.Add("ItemName");
            newdt.Columns.Add("OPCount");
            newdt.Columns.Add("OPFee");
            newdt.Columns.Add("IPCount");
            newdt.Columns.Add("IPFee");
            DataTable dtOP = NewDao<IThatFeeDao>().GetThatFeeCount(confirDeptID, beginDate, endDate, 0); //获取门诊记录
            DataTable dtIP = NewDao<IThatFeeDao>().GetThatFeeCount(confirDeptID, beginDate, endDate, 1); //获取住院记录
            for (int i = 0; i < dtOP.Rows.Count; i++)
            {
                DataRow dr = newdt.NewRow();
                dr["ItemName"] = dtOP.Rows[i]["ItemName"];
                string total = NewDao<IThatFeeDao>().GetThatFeeTotal(dtOP.Rows[i]["ItemID"].ToString(), 0);
                dr["OPCount"] = dtOP.Rows[i]["Count"];
                dr["OPFee"] = total;
                oPCount += Convert.ToInt32(dtOP.Rows[i]["Count"]);
                decimal totalfee = 0;
                oPFee += Tools.ToDecimal(total, totalfee);
                DataRow drIP = dtIP.Select("ItemID=" + dtOP.Rows[i]["ItemID"].ToString() ).FirstOrDefault();
                if (drIP != null)
                {
                    total = NewDao<IThatFeeDao>().GetThatFeeTotal(drIP["ItemID"].ToString(), 1);
                    dr["IPCount"] = drIP["Count"];
                    dr["IPFee"] = total;
                    iPCount += Convert.ToInt32(drIP["Count"]);
                    totalfee = 0;
                    iPFee += Tools.ToDecimal(total, totalfee);
                    dtIP.Rows.Remove(drIP);
                }
                else
                {
                    dr["IPCount"] = "0";
                    dr["IPFee"] = "0.00";
                }

                newdt.Rows.Add(dr);
            }

            for (int i = 0; i < dtIP.Rows.Count; i++)
            {
                DataRow dr = newdt.NewRow();
                dr["ItemName"] = dtIP.Rows[i]["ItemName"];
                dr["OPCount"] = "0";
                dr["OPFee"] = "0.00";
                string total = NewDao<IThatFeeDao>().GetThatFeeTotal(dtIP.Rows[i]["ItemID"].ToString(), 1);
                dr["IPCount"] = dtIP.Rows[i]["Count"];
                dr["IPFee"] = total;
                iPCount += Convert.ToInt32(dtIP.Rows[i]["Count"]);
                decimal totalfee = 0;
                iPFee += Tools.ToDecimal(total, totalfee);
                newdt.Rows.Add(dr);
            }

            DataRow newdr = newdt.NewRow();
            newdr["ItemName"] = "合计";
            newdr["OPCount"] = oPCount;
            newdr["OPFee"] = oPFee.ToString("0.00");
            newdr["IPCount"] = iPCount;
            newdr["IPFee"] = iPFee.ToString("0.00");
            newdt.Rows.Add(newdr);
            return newdt;
        }
        #endregion

        #region 医技明细
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="systemtype">0门诊1住院</param>
        /// <param name="beginDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="itemIDs">组合项目id以逗号分割的字符串</param>
        /// <returns>返回查询条件字符串</returns>
        public StringBuilder GetDetailQueryWhere(string deptId, int systemtype, string beginDate, string endDate, string itemIDs)
        {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append(string.Format(" AND b.SystemType={0}", systemtype));
            if (!string.IsNullOrEmpty(deptId))
            {
                strBuilder.Append(string.Format(" AND b.ExecuteDeptID={0}", deptId));
            }

            strBuilder.AppendFormat(" AND e.ConfirDate between '{0}' and '{1}'", beginDate, endDate);
            if (!string.IsNullOrEmpty(itemIDs))
            {
                strBuilder.AppendFormat(" AND a.ItemID in ({0})", itemIDs);
            }

            return strBuilder;
        }
        #endregion
    }
}