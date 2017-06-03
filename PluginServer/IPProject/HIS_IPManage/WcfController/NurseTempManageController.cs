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
using HIS_IPManage.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPManage.WcfController
{
    /// <summary>
    /// 护士账单模板控制器
    /// </summary>
    [WCFController]
    public class NurseTempManageController : WcfServerController
    {
        /// <summary>
        /// 查询医嘱账单模板列表
        /// </summary>
        /// <returns>医嘱账单模板列表</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData GetIPFeeItemTempList()
        {
            int workID = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            int empId = requestData.GetData<int>(2);
            List<IP_FeeItemTemplateHead> feeTempList = NewDao<IIPManageDao>().GetIPFeeItemTempList(workID, deptId, empId);
            // 不存在模板数据时，生成根节点数据
            if (feeTempList == null || feeTempList.Count <= 0)
            {
                IP_FeeItemTemplateHead feeItemTempHead = NewObject<IP_FeeItemTemplateHead>();
                feeItemTempHead.TempHeadID = 0;
                feeItemTempHead.PTempHeadID = 0;
                feeItemTempHead.TempName = "全院模板";
                feeItemTempHead.TempLevel = 0;
                feeItemTempHead.TempClass = 0;
                feeItemTempHead.CreateDeptID = deptId;
                feeItemTempHead.CreateEmpID = empId;
                feeItemTempHead.CreateDate = DateTime.Now;
                feeItemTempHead.UpdateTime = DateTime.Now;
                feeItemTempHead.DelFlag = 0;
                this.BindDb(feeItemTempHead);
                feeItemTempHead.save();
                feeItemTempHead.TempHeadID = 0;
                feeItemTempHead.TempName = "科室模板";
                feeItemTempHead.TempLevel = 1;
                feeItemTempHead.TempClass = 0;
                this.BindDb(feeItemTempHead);
                feeItemTempHead.save();
                feeItemTempHead.TempHeadID = 0;
                feeItemTempHead.TempName = "个人模板";
                feeItemTempHead.TempLevel = 2;
                feeItemTempHead.TempClass = 0;
                this.BindDb(feeItemTempHead);
                feeItemTempHead.save();
                feeTempList = NewDao<IIPManageDao>().GetIPFeeItemTempList(workID, deptId, empId);
            }

            responseData.AddData(feeTempList);
            return responseData;
        }

        /// <summary>
        /// 保存模板数据
        /// </summary>
        /// <returns>true：保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveFeeItemTemplateHead()
        {
            IP_FeeItemTemplateHead feeItemTemp = requestData.GetData<IP_FeeItemTemplateHead>(0);
            this.BindDb(feeItemTemp);
            feeItemTemp.save();
            responseData.AddData(true);
            responseData.AddData(feeItemTemp.TempHeadID);
            return responseData;
        }

        /// <summary>
        /// 获取项目选项卡数据
        /// </summary>
        /// <returns>项目选项卡数据</returns>
        [WCFMethod]
        public ServiceResponseData GetRegItemShowCard()
        {
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dt = feeitem.GetSimpleFeeItemDataDt(FeeBusinessType.记账业务);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存模板明细数据
        /// </summary>
        /// <returns>true：保存成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveFeeTempDetailData()
        {
            try
            {
                DataTable feeTempDetailDt = requestData.GetData<DataTable>(0);
                int tempHeadID = requestData.GetData<int>(1);
                // 删除现存的模板明细数据
                NewDao<IIPManageDao>().DelFeeTempDetail(tempHeadID);
                if (feeTempDetailDt.Rows.Count > 0)
                {
                    IP_FeeItemTemplateDetail feeItemTempDetail;
                    for (int i = 0; i < feeTempDetailDt.Rows.Count; i++)
                    {
                        feeItemTempDetail = new IP_FeeItemTemplateDetail();
                        feeItemTempDetail = ConvertExtend.ToObject<IP_FeeItemTemplateDetail>(feeTempDetailDt, i);
                        feeItemTempDetail.TempDetailID = 0;
                        this.BindDb(feeItemTempDetail);
                        feeItemTempDetail.save();
                    }
                }

                responseData.AddData(true);
            }
            catch (Exception ex)
            {
                responseData.AddData(false);
                responseData.AddData(ex.Message);
                return responseData;
            }

            return responseData;
        }

        /// <summary>
        /// 查询模板明细数据
        /// </summary>
        /// <returns>模板明细数据列表</returns>
        [WCFMethod]
        public ServiceResponseData GetFeeTempDetails()
        {
            int tempHeadID = requestData.GetData<int>(0);
            DataTable feeTempDetails = NewDao<IIPManageDao>().GetFeeTempDetails(tempHeadID);
            responseData.AddData(feeTempDetails);
            return responseData;
        }
    }
}
