using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 供应商维护控制器
    /// </summary>
    [WCFController]
    public class SupplyController: WcfServerController
    {
        /// <summary>
        /// 保存生产商数据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveSupply()
        {
            var product = requestData.GetData<DG_SupportDic>(0);
            var workId = requestData.GetData<int>(1);
            DGBillResult flag = NewObject<DrugSupplyMgr>().SaveSupply(product, workId);
            responseData.AddData(flag);
            return responseData;
        }

        ///// <summary>
        ///// 获取对象
        ///// </summary>
        ///// <returns></returns>
        // [WCFMethod]
        // public ServiceResponseData GetSupply()
        // {
        //     var dic = requestData.GetData<Dictionary<string, string>>(0);
        //     DataTable dt = NewObject<DrugSupplyMgr>().GetSupplys(dic);
        //     responseData.AddData(dt);
        //     return responseData;
        // }

        /// <summary>
        /// 获取生产商分页数据
        /// </summary>
        /// <returns>生产商分页数据</returns>
        [WCFMethod]
        public ServiceResponseData GetSupply()
        {
            int pageNo = requestData.GetData<int>(0);  //页码
            int pageSize = requestData.GetData<int>(1); //每页有多少条记录
            List<Tuple<string, string, SqlOperator>> andWhere = requestData.GetData< List < Tuple < string, string, SqlOperator>>> (2);
            List<Tuple<string, string, SqlOperator>> orWhere = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(3);
            PageInfo page = null;
            DataTable dt = NewObject<DrugSupplyMgr>().GetSupplys(pageNo, pageSize, andWhere, orWhere, out page);
            responseData.AddData(dt);
            responseData.AddData(page.totalRecord);
            return responseData;
        }

        /// <summary>
        /// 删除生产商
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeleteSupply()
        {
            var p = requestData.GetData<DG_SupportDic>(0);
            this.BindDb(p);
            NewObject<DrugSupplyMgr>().UpdateSupply(p);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 使用或查询条件语句获取生产商
        /// </summary>
        /// <returns>获取生产商</returns>
        [WCFMethod]
        public ServiceResponseData GetSupplyUserOr()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugSupplyMgr>().GetSupplysUserOr(dic);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
