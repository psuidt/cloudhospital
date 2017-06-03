using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.CoreFrame.Common;
using HIS_Entity.ClinicManage;
using HIS_IPDoctor.Dao;

namespace HIS_IPDoctor.ObjectModel
{
    /// <summary>
    /// 模板业务实现类
    /// </summary>
    public class OrderTempManage : AbstractObjectModel
    {
        /// <summary>
        /// 获取医嘱模板列表
        /// </summary>
        /// <param name="tempLevel">模板级别</param>
        /// <param name="deptId">所属科室</param>
        /// <param name="empId">所属用户</param>
        /// <returns>模板列表</returns>
        public List<IPD_OrderModelHead> GetOrderTempList(int tempLevel, int deptId, int empId)
        {
            List<IPD_OrderModelHead> orderTempList = NewDao<IOrderTempManageDao>().GetOrderTempList(tempLevel, deptId, empId);
            if (orderTempList == null || orderTempList.Count == 0)
            {
                IPD_OrderModelHead morderModelHead = NewObject<IPD_OrderModelHead>();
                morderModelHead.PID = 0;
                morderModelHead.ModelName = "全部类型";
                morderModelHead.ModelLevel = tempLevel;
                morderModelHead.ModelType = 0;
                morderModelHead.CreatDeptID = deptId;
                morderModelHead.CreateDocID = empId;
                morderModelHead.CreateDate = DateTime.Now;
                morderModelHead.UpdateDate = DateTime.Now;
                morderModelHead.Memo = string.Empty;
                morderModelHead.DeleteFlag = 0;
                this.BindDb(morderModelHead);
                morderModelHead.save();
                orderTempList.Add(morderModelHead);
            }

            return orderTempList;
        }

        /// <summary>
        /// 保存模板明细数据
        /// </summary>
        /// <param name="longOrderDetails">长嘱明细</param>
        /// <param name="tempOrderDetails">临嘱明细</param>
        /// <returns>保存是否成功</returns>
        public bool SaveOrderDetailsData(DataTable longOrderDetails, DataTable tempOrderDetails)
        {
            // 长期医嘱
            if (longOrderDetails != null)
            {
                // 根据组号对数据进行排序
                longOrderDetails.DefaultView.Sort = "GroupID";
                // 给同组数据设置相同的用法、频次、滴速、嘱托
                int tempGroupID = 0;
                IPD_OrderModelDetail orderModelDetails = NewObject<IPD_OrderModelDetail>();
                //
                for (int i = 0; i < longOrderDetails.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(longOrderDetails.Rows[i]["GroupID"]);
                        orderModelDetails = ConvertExtend.ToObject<IPD_OrderModelDetail>(longOrderDetails, i);
                        this.BindDb(orderModelDetails);
                        orderModelDetails.save();
                        continue;
                    }

                    if (Convert.ToInt32(longOrderDetails.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        longOrderDetails.Rows[i]["ChannelID"] = longOrderDetails.Rows[i - 1]["ChannelID"];
                        longOrderDetails.Rows[i]["ChannelName"] = longOrderDetails.Rows[i - 1]["ChannelName"];
                        longOrderDetails.Rows[i]["FrenquencyID"] = longOrderDetails.Rows[i - 1]["FrenquencyID"];
                        longOrderDetails.Rows[i]["Frenquency"] = longOrderDetails.Rows[i - 1]["Frenquency"];
                        longOrderDetails.Rows[i]["DropSpec"] = longOrderDetails.Rows[i - 1]["DropSpec"];
                        longOrderDetails.Rows[i]["Entrust"] = longOrderDetails.Rows[i - 1]["Entrust"];
                    }

                    orderModelDetails = ConvertExtend.ToObject<IPD_OrderModelDetail>(longOrderDetails, i);
                    this.BindDb(orderModelDetails);
                    orderModelDetails.save();
                    tempGroupID = Convert.ToInt32(longOrderDetails.Rows[i]["GroupID"]);
                }
            }

            // 临时医嘱
            if (tempOrderDetails != null)
            {
                // 根据组号对数据进行排序
                tempOrderDetails.DefaultView.Sort = "GroupID";

                // 给同组数据设置相同的用法、频次、滴速、嘱托
                int tempGroupID = 0;
                IPD_OrderModelDetail orderModelDetails = NewObject<IPD_OrderModelDetail>();                
                for (int i = 0; i < tempOrderDetails.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        tempGroupID = Convert.ToInt32(tempOrderDetails.Rows[i]["GroupID"]);
                        orderModelDetails = ConvertExtend.ToObject<IPD_OrderModelDetail>(tempOrderDetails, i);
                        this.BindDb(orderModelDetails);
                        orderModelDetails.save();
                        continue;
                    }

                    if (Convert.ToInt32(tempOrderDetails.Rows[i]["GroupID"]) == tempGroupID)
                    {
                        tempOrderDetails.Rows[i]["ChannelName"] = tempOrderDetails.Rows[i - 1]["ChannelName"];
                        tempOrderDetails.Rows[i]["Frenquency"] = tempOrderDetails.Rows[i - 1]["Frenquency"];
                        tempOrderDetails.Rows[i]["DropSpec"] = tempOrderDetails.Rows[i - 1]["DropSpec"];
                        tempOrderDetails.Rows[i]["Entrust"] = tempOrderDetails.Rows[i - 1]["Entrust"];
                    }

                    orderModelDetails = ConvertExtend.ToObject<IPD_OrderModelDetail>(tempOrderDetails, i);
                    this.BindDb(orderModelDetails);
                    orderModelDetails.save();
                    tempGroupID = Convert.ToInt32(tempOrderDetails.Rows[i]["GroupID"]);
                }
            }

            return true;
        }
    }
}
