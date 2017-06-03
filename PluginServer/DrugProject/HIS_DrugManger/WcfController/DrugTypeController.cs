using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药品类型控制器
    /// </summary>
    [WCFController]
    public class DrugTypeController : WcfServerController
    {
        #region 药品类型
        /// <summary>
        /// 保存药品类型
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveDrugType()
        {
            DG_TypeDic drugProduct = NewObject<DG_TypeDic>();
            var p = requestData.GetData<DG_TypeDic>(0);
            this.BindDb(p);
           var flag= NewObject<DrugTypeMgr>().SaveDrugType(p);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取药品类型
        /// </summary>
        /// <returns>药品类型</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugType()
        {
            var pruduct = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugTypeMgr>().GetDrugType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 药品子类型
        /// <summary>
        /// 保存药品子类型
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveChildDrugType()
        {
            var product = requestData.GetData<DG_ChildTypeDic>(0);
            this.BindDb(product);
            var flag= NewObject<DrugTypeMgr>().SaveChildDrugType(product);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 获取药品子类型
        /// </summary>
        /// <returns>药品子类型</returns>
        [WCFMethod]
        public ServiceResponseData GetChildDrugType()
        {
            var pruduct = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<DrugTypeMgr>().GetChildDrugType(pruduct);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药品类型和子类型
        /// </summary>
        /// <returns>药品类型和子类型</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugTypeAndChild()
        {
            DataTable dt = NewObject<IDGDao>().GetDrugTypeAndChild();
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
