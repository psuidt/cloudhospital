using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_DrugManger.Dao;
using HIS_DrugManger.ObjectModel.BaseData;

namespace HIS_DrugManger.WcfController
{
    /// <summary>
    /// 药品拆零控制器
    /// </summary>
    [WCFController]
    public class ResolveController : WcfServerController
    {
        /// <summary>
        /// 获取界面初始化下拉菜单数据
        /// </summary>
        /// <returns>界面初始化下拉菜单数据</returns>
        [WCFMethod]
        public ServiceResponseData GetAllCenterTable()
        {
            DataTable dtDrugType = NewObject<DrugTypeMgr>().GetDrugType(null);
            responseData.AddData(dtDrugType);

            //药品子类型
            DataTable dtDrugCType = NewObject<DrugTypeMgr>().GetChildDrugType(null);
            responseData.AddData(dtDrugCType);

            DataTable dt = NewObject<IDGDao>().GetDosageUserOr(null);
            responseData.AddData(dt);

            return responseData;
        }

        /// <summary>
        /// 查询药品库存信息
        /// </summary>
        /// <returns>库存信息表</returns>
        [WCFMethod]
        public ServiceResponseData LoadDrugStorage()
        {
            var queryCondition = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dtRtn =NewDao<IDSDao>().LoadResolve(queryCondition);
            responseData.AddData(dtRtn);
            return responseData;
        }

        /// <summary>
        /// 拆零操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData UpdateStorage()
        {
            var ids = requestData.GetData<string>(0);
            var location = NewDao<IDGDao>()
                .UpdateStorages(ids, 0);
            responseData.AddData(location);
            return responseData;
        }

        /// <summary>
        /// 取消拆零操作
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData UpdateBackStorage()
        {
            var ids = requestData.GetData<string>(0);
            var location = NewDao<IDGDao>()
                .UpdateStorages(ids, 1);
            responseData.AddData(location);
            return responseData;
        }
    }
}
