using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.IPManage;
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.ObjectModel
{
    /// <summary>
    /// 药品统领单处理
    /// </summary>
    public class DrugbillManagement : AbstractObjectModel
    {
        /// <summary>
        /// 发送药品统领单
        /// </summary>
        /// <param name="commandList">药品统领列表</param>
        /// <param name="makeEmpID">统领人ID</param>
        /// <param name="makeEmpName">统领人名</param>
        /// <param name="commandStatus">true：发药/false：退药</param>
        /// <param name="workID">机构ID</param>
        /// <returns>发送成功或失败</returns>
        public bool SendCommandList(DataTable commandList, int makeEmpID, string makeEmpName, bool commandStatus, int workID)
        {
            // 退药统领
            if (!commandStatus)
            {
                // 取得统领单类型
                DataTable drugBillBackThere = NewDao<IIPManageDao>().GetDrugBillTypeList(true);
                // 获取是否按病人统领标志
                int isPatCommand = Convert.ToInt32(drugBillBackThere.Rows[0]["IsPatCommand"]);
                // 按病人统领
                SavePatDrugbillData(isPatCommand, commandList, makeEmpID, makeEmpName, Convert.ToInt32(drugBillBackThere.Rows[0]["BillTypeID"]), workID);
                return true;
            }

            // 发药统领
            // 取得统领单类型列表
            DataTable drugBillTypeList = NewDao<IIPManageDao>().GetDrugBillTypeList(false);

            // 保存统领数据
            if (drugBillTypeList != null && drugBillTypeList.Rows.Count > 0)
            {
                //DataTable NewCommandList = CommandList;
                // 根据统领类型顺序做成统领单数据
                for (int i = 0; i < drugBillTypeList.Rows.Count; i++)
                {
                    // 
                    if (!string.IsNullOrEmpty(Convert.ToString(drugBillTypeList.Rows[i]["BillRule"])))
                    {
                        DataTable temp = commandList.Clone();
                        // 过滤出对应类型的统领数据
                        DataView view = new DataView(commandList);
                        string where = Convert.ToString(drugBillTypeList.Rows[i]["BillRule"]) + "=1";
                        // 当前统领类型为统领单或者退药单是暂时不统计数据
                        if (where.Contains("IsCommandSheet") || where.Contains("IsBackThere"))
                        {
                            continue;
                        }

                        view.RowFilter = where;
                        temp = view.ToTable();
                        // 将统领数据保存到数据库
                        // 没有查到对应数据
                        if (temp == null || temp.Rows.Count <= 0)
                        {
                            continue;
                        }

                        // 获取是否按病人统领标志
                        int isPatCommand = Convert.ToInt32(drugBillTypeList.Rows[i]["IsPatCommand"]);
                        // 按病人统领
                        SavePatDrugbillData(isPatCommand, temp, makeEmpID, makeEmpName, Convert.ToInt32(drugBillTypeList.Rows[i]["BillTypeID"]), workID);
                        // 过滤未保存的数据
                        DataView newView = new DataView(commandList);
                        where = Convert.ToString(drugBillTypeList.Rows[i]["BillRule"]) + "<>1";
                        view.RowFilter = where;
                        commandList = view.ToTable();
                        if (commandList.Rows.Count <= 0)
                        {
                            return true;
                        }
                    }
                }

                // 分类统领后，剩下的数据全部分类成统领单
                if (commandList.Rows.Count > 0)
                {
                    // 取得统领单类型ID
                    drugBillTypeList.TableName = "DrugBillTypeList";
                    DataView view = new DataView(drugBillTypeList);
                    view.RowFilter = "BillRule='IsCommandSheet'";
                    DataTable tempDt = view.ToTable();
                    if (tempDt != null && tempDt.Rows.Count > 0)
                    {
                        // 获取是否按病人统领标志
                        int isPatCommand = Convert.ToInt32(tempDt.Rows[0]["IsPatCommand"]);
                        // 按病人统领
                        SavePatDrugbillData(isPatCommand, commandList, makeEmpID, makeEmpName, Convert.ToInt32(tempDt.Rows[0]["BillTypeID"]), workID);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 按病人统领数据
        /// </summary>
        /// <param name="isPatCommand">是否按病人统领</param>
        /// <param name="billData">统领数据</param>
        /// <param name="makeEmpID">统领人ID</param>
        /// <param name="makeEmpName">统领人名</param>
        /// <param name="billTypeID">统领类型ID</param>
        /// <param name="workID">机构ID</param>
        private void SavePatDrugbillData(int isPatCommand, DataTable billData, int makeEmpID, string makeEmpName, int billTypeID, int workID)
        {
            // IsPatCommand = 1按病人生成统领单
            if (isPatCommand == 1)
            {
                // 取出统领数据中的病人ID列表
                int[] patListIDArray = billData.AsEnumerable().Select(d => d.Field<int>("PatListID")).ToArray();
                // 去掉重复的病人ID
                string strPatListID = string.Join(",", patListIDArray.ToArray());
                strPatListID = string.Join(",", strPatListID.Split(',').Distinct().ToArray());
                string[] strpatListIdArray = strPatListID.Split(',').ToArray();
                // 循环根据病人生成统领数据
                for (int j = 0; j < strpatListIdArray.Length; j++)
                {
                    DataTable commandDt = billData.Select("PatListID=" + strpatListIdArray[j]).CopyToDataTable();
                    // 保存统领单数据
                    SaveDrugbillData(commandDt, billTypeID, makeEmpID, makeEmpName, true, workID);
                }
            }
            else
            {
                // 不按病人统领
                // 保存统领单数据
                SaveDrugbillData(billData, billTypeID, makeEmpID, makeEmpName, false, workID);
            }
        }

        /// <summary>
        /// 保存统领单数据
        /// </summary>
        /// <param name="drugbillData">药品统领数据</param>
        /// <param name="billTypeID">统领单类型</param>
        /// <param name="makeEmpID">统领人ID</param>
        /// <param name="makeEmpName">统领人名</param>
        /// <param name="isPatCommand">是否按病人统领true:按病人统领/false:不按病人统领</param>
        /// <param name="workID">机构ID</param>
        private void SaveDrugbillData(DataTable drugbillData, int billTypeID, int makeEmpID, string makeEmpName, bool isPatCommand, int workID)
        {
            // 保存统领单头数据
            IP_DrugBillHead drugBillHead = NewObject<IP_DrugBillHead>();
            for (int j = 0; j < drugbillData.Rows.Count; j++)
            {
                if (j == 0)
                {
                    drugBillHead.BillClass = 0; // 单据分类
                    drugBillHead.BillTypeID = billTypeID;  // 统领单类型
                    drugBillHead.PresDeptID = Convert.ToInt32(drugbillData.Rows[j]["PatDeptID"].ToString()); // 开方科室ID
                    drugBillHead.ExecDeptID = Convert.ToInt32(drugbillData.Rows[j]["ExecDeptID"].ToString());  // 执行科室
                    drugBillHead.MakeDate = DateTime.Now; // 发送时间
                    drugBillHead.MakeEmpID = makeEmpID; // 发送人
                    drugBillHead.MakeEmpName = makeEmpName; // 发送人姓名
                    if (isPatCommand)
                    {
                        drugBillHead.PatName = drugbillData.Rows[j]["PatName"].ToString();
                    }

                    this.BindDb(drugBillHead);
                    drugBillHead.save();
                    #region "保存业务消息数据 --Add By ZhangZhong"
                    // 保存业务消息数据
                    Dictionary<string, string> msgDic = new Dictionary<string, string>();
                    msgDic.Add("WorkID", workID.ToString()); // 消息机构ID
                    msgDic.Add("SendUserId", makeEmpID.ToString()); // 消息生成人ID
                    msgDic.Add("SendDeptId", drugBillHead.PresDeptID.ToString()); // 消息生成科室ID
                    msgDic.Add("BillHeadID", drugBillHead.BillHeadID.ToString()); // 统领明细ID
                    NewObject<BusinessMessage>().GenerateBizMessage(MessageType.药品统领, msgDic);
                    #endregion
                }
                // 保存统领单明细数据
                IP_DrugBillDetail drugBillDetail = ConvertExtend.ToObject<IP_DrugBillDetail>(drugbillData, j);
                decimal retailFee = Math.Round((drugBillDetail.InPrice * drugBillDetail.Amount) / drugBillDetail.PackAmount, 2);
                drugBillDetail.BillHeadID = drugBillHead.BillHeadID;
                drugBillDetail.RETAILFEE = retailFee;// 批发金额
                drugBillDetail.SellFee = Convert.ToDecimal(drugbillData.Rows[j]["TotalFee"]);   // 零售金额
                drugBillDetail.CostEmpID = Convert.ToInt32(drugbillData.Rows[j]["PresDoctorID"]); // 记账员ID
                drugBillDetail.CostDate = Convert.ToDateTime(drugbillData.Rows[j]["ChargeDate"]); // 记账时间
                drugBillDetail.OrderGroupID = Convert.ToInt32(drugbillData.Rows[j]["GroupID"]);// 医嘱组号 
                if (drugBillDetail.Amount <= 0)
                {
                    drugBillDetail.DispDrugFlag = 1;// 发药标识
                }
                else
                {
                    drugBillDetail.DispDrugFlag = 0;// 发药标识
                }

                drugBillDetail.NoDrugFlag = 0;// 缺药标识 0
                drugBillDetail.SpecDicID = Convert.ToInt32(drugbillData.Rows[j]["CenteDrugID"]);// 药品规格典ID 
                this.BindDb(drugBillDetail);
                drugBillDetail.save();
            }
        }

        /// <summary>
        /// 取消发送统领单
        /// </summary>
        /// <param name="billHeadID">统领单头表ID</param>
        /// <returns>错误消息</returns>
        public string CancelSendOrder(int billHeadID)
        {
            // 根据统领单头表ID查询明细是否存在已发药的数据
            bool result = NewDao<IIPManageDao>().CheckOrderDetailDispDrugFlag(billHeadID);
            // 如果已发药，提示Msg取消发送失败。
            if (result)
            {
                return "当前统领单部分药品已发药，不允许取消！";
            }
            else
            {
                // 否则删除头表和明细表数据
                // 删除统领单头表数据
                NewDao<IIPManageDao>().DelDrugBillHeadData(billHeadID);
                // 删除统领单明细数据
                NewDao<IIPManageDao>().DelDrugBillDetailData(billHeadID);
            }

            return string.Empty;
        }
    }
}
