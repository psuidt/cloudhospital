using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 生产商维护控制器
    /// </summary>
    [WCFController]
    public class ProductController : WcfServerController
    {
        /// <summary>
        /// 保存生产商
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveProduct()
        {
            DrugProductMgr drugProduct = NewObject<DrugProductMgr>();
            var product = requestData.GetData<DG_ProductDic>(0);
            var workId = requestData.GetData<int>(1);
            this.BindDb(product);
            SetWorkId(workId);
            responseData.AddData(NewObject<DrugProductMgr>().SaveProduct(product, workId));
            return responseData;
        }

        /// <summary>
        /// 获取生产商数据
        /// </summary>
        /// <returns>生产商数据</returns>
        [WCFMethod]
        public ServiceResponseData GetProduct()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugProductMgr>().GetProducts(dic);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除生产商
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeleteProduct()
        {
            DrugProductMgr drugProduct = NewObject<DrugProductMgr>();
            var pruduct = requestData.GetData<DG_ProductDic>(0);
            this.BindDb(pruduct);
            drugProduct.UpdateProduct(pruduct);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 通过查询条件获取生产商数据
        /// </summary>
        /// <returns>生产商数据</returns>
        [WCFMethod]
        public ServiceResponseData GetProductUserOr()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugProductMgr>().GetProductsUserOr(dic);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
