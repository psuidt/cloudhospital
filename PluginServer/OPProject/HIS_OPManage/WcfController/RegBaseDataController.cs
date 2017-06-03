using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.OPManage;
using HIS_OPManage.Dao;
using HIS_OPManage.ObjectModel;
using HIS_PublicManage.ObjectModel;

namespace HIS_OPManage.WcfController
{
    /// <summary>
    /// 挂号基础维护控制器类
    /// </summary>
    [WCFController]
    public class RegBaseDataController : WcfServerController
    {
        /// <summary>
        /// 获得挂号类型
        /// </summary>
        /// <returns>返回挂号类型</returns>
        [WCFMethod]
        public ServiceResponseData GetRegTypes()
        {
            DataTable dt = NewObject<OP_RegType>().gettable();
            DataColumn col = new DataColumn();
            col.ColumnName = "TotalRegFee";
            col.DataType = typeof(decimal);
            dt.Columns.Add(col);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["TotalRegFee"] = NewObject<RegisterProcess>().GetRegTotalFee(Convert.ToInt32(dt.Rows[i]["RegTypeID"]));//挂号总金额
            }

            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 增加和修改挂号类型
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData SaveRegtype()
        {
            OP_RegType regtype = requestData.GetData<OP_RegType>(0);
            List<OP_RegType> regTypeOld = NewObject<OP_RegType>().getlist<OP_RegType>(" regtypecode='" + regtype.RegTypeCode + "' and regtypeid!=" + regtype.RegTypeID + string.Empty);
            if (regTypeOld != null && regTypeOld.Count > 0)
            {
                throw new Exception("已经存在相同挂号类型代码,请重新输入挂号类型代码");
            }

            this.BindDb(regtype);
            regtype.save();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取挂号类别对应的明细收费项目
        /// </summary>
        /// <returns>明细收费项目</returns>
        [WCFMethod]
        public ServiceResponseData GetRegItems()
        {
            int regid = requestData.GetData<int>(0);
            DataTable dt = NewDao<IOPManageDao>().GetRegItemFees(regid);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存挂号对应挂号明细
        /// </summary>
        /// <returns>返回保存后的明细数据</returns>
        [WCFMethod]
        public ServiceResponseData SaveRegItemFees()
        {
            OP_RegType curregtype = requestData.GetData<OP_RegType>(0);
            List<OP_RegItemFee> regItems = requestData.GetData<List<OP_RegItemFee>>(1);
            for (int index = 0; index < regItems.Count; index++)
            {
                regItems[index].RegTypeID = curregtype.RegTypeID;
                regItems[index].Flag = 0;
                this.BindDb(regItems[index]);
                regItems[index].save();
            }

            responseData.AddData(regItems);
            return responseData;
        }

        /// <summary>
        /// 获取项目选项卡数据
        /// </summary>
        /// <returns>返回选项卡数据</returns>
        [WCFMethod]
        public ServiceResponseData GetRegItemShowCard()
        {
            FeeItemDataSource feeitem = NewObject<FeeItemDataSource>();
            DataTable dt = feeitem.GetSimpleFeeItemDataDt(FeeBusinessType.记账业务);// EFWCoreLib.CoreFrame.Common.ConvertExtend.ToDataTable(feeitem.GetSimpleFeeItemData(FeeBusinessType.记账业务));
            string regStatID = NewObject<CommonMethod>().GetOpConfig(OpConfigConstant.RegFeeStatID);
            DataView dv = dt.DefaultView;
            if (!string.IsNullOrEmpty(regStatID))
            {
                dv.RowFilter = " statid in (" + regStatID + ")";
            }

            responseData.AddData(dv.Table);
            return responseData;
        }

        /// <summary>
        /// 删除费用明细
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData DeleteRegItemFees()
        {
            try
            {
                int id = requestData.GetData<int>(0);
                NewObject<OP_RegItemFee>().delete(id);
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
