using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.SqlPagination;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_Entity.SqlAly;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资供应商控制器
    /// </summary>
    [WCFController]
    public class MaterialSupplyController : WcfServerController
    {
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveSupply()
        {
            var product = requestData.GetData<MW_SupportDic>(0);
            var workId = requestData.GetData<int>(1);
            bool flag = NewObject<MaterialSupplyMgr>().SaveSupply(product, workId);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <returns>供应商</returns>
        [WCFMethod]
        public ServiceResponseData GetSupply()
        {
            int pageNo = requestData.GetData<int>(0);  //页码
            int pageSize = requestData.GetData<int>(1); //每页有多少条记录
            List<Tuple<string, string, SqlOperator>> andWhere = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(2);
            List<Tuple<string, string, SqlOperator>> orWhere = requestData.GetData<List<Tuple<string, string, SqlOperator>>>(3);
            PageInfo page = null;
            DataTable dt = NewObject<MaterialSupplyMgr>().GetSupplys(pageNo, pageSize, andWhere, orWhere, out page);
            responseData.AddData(dt);
            responseData.AddData(page.totalRecord);
            return responseData;
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteSupply()
        {
            var p = requestData.GetData<MW_SupportDic>(0);
            this.BindDb(p);
            NewObject<MaterialSupplyMgr>().UpdateSupply(p);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 使用或语句获取对象
        /// </summary>
        /// <returns>获取对象</returns>
        [WCFMethod]
        public ServiceResponseData GetSupplyUserOr()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<MaterialSupplyMgr>().GetSupplysUserOr(dic);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
