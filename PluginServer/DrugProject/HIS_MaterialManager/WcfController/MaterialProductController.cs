using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.MaterialManage;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资生厂商维护控制器
    /// </summary>
    [WCFController]
    public class MaterialProductController : WcfServerController
    {
        /// <summary>
        /// 保存厂家
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveProduct()
        {
            MaterialProductMgr drugProduct = NewObject<MaterialProductMgr>();

            var product = requestData.GetData<MW_ProductDic>(0);
            var workId = requestData.GetData<int>(1);
            bool flag = NewObject<MaterialProductMgr>().SaveProduct(product, workId);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取厂家数据
        /// </summary>
        /// <returns>厂家数据</returns>
        [WCFMethod]
        public ServiceResponseData GetProduct()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<MaterialProductMgr>().GetProducts(dic);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除厂家数据
        /// </summary>
        /// <returns>1删除成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteProduct()
        {
            MaterialProductMgr drugProduct = NewObject<MaterialProductMgr>();
            var pruduct = requestData.GetData<MW_ProductDic>(0);
            this.BindDb(pruduct);
            drugProduct.UpdateProduct(pruduct);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取厂家数据
        /// </summary>
        /// <returns>厂家数据</returns>
        [WCFMethod]
        public ServiceResponseData GetProductUserOr()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<MaterialProductMgr>().GetProductsUserOr(dic);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
