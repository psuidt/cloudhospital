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
using HIS_MaterialManager.Dao;
using HIS_MaterialManager.ObjectModel.BaseData;

namespace HIS_MaterialManage.WcfController
{
    /// <summary>
    /// 物资分类维护控制器
    /// </summary>
    [WCFController]
    public class MaterialTypeController : WcfServerController
    {
        #region 物资类型
        /// <summary>
        /// 保存物资类型
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveMaterialType()
        {
            MW_TypeDic drugProduct = NewObject<MW_TypeDic>();
            var p = requestData.GetData<MW_TypeDic>(0);
            this.BindDb(p);
            SetWorkId(oleDb.WorkId);
            var flag = NewObject<MaterialTypeMgr>().SaveMaterialType(p);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取物资类型数据
        /// </summary>
        /// <returns>物资类型数据</returns>
        [WCFMethod]
        public ServiceResponseData GetMaterialType()
        {
            var pruduct = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<MaterialTypeMgr>().GetMaterialType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 物资子类型
        /// <summary>
        /// 物资子类型
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveChildMaterialType()
        {
            var product = requestData.GetData<MW_TypeDic>(0);
            this.BindDb(product);
            SetWorkId(oleDb.WorkId);
            var flag = NewObject<MaterialTypeMgr>().SaveChildMaterialType(product);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取物资子类型
        /// </summary>
        /// <returns>物资子类型</returns>
        [WCFMethod]
        public ServiceResponseData GetChildMaterialType()
        {
            var pruduct = requestData.GetData<Dictionary<string, string>>(0);
            //DataTable dt = NewObject<MaterialTypeMgr>().GetChildMaterialType(pruduct);
            DataTable dt = NewDao<IMWDao>().GetChildMaterialType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除物资子类型
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData DelChildMaterialType()
        {
            var product = requestData.GetData<MW_TypeDic>(0);
            this.BindDb(product);
            int result = product.delete();
            responseData.AddData(result);
            return responseData;
        }
        #endregion
    }
}