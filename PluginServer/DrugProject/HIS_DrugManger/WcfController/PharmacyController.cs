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
    /// 药理分类维护控制器
    /// </summary>
    [WCFController]
    public class PharmacyController : WcfServerController
    {
        /// <summary>
        /// 获取药理分类
        /// </summary>
        /// <returns>药理分类</returns>
        [WCFMethod]
        public ServiceResponseData GetAllPharmacy()
        {
            DataTable dt = NewObject<PharmacyMgr>().GetAllPharmacy();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 通过父级ID获取药理分类
        /// </summary>
        /// <returns>药理分类</returns>
        [WCFMethod]
        public ServiceResponseData GetGridByParentId()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<PharmacyMgr>().GetGridByParentId(dic);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除药理分类
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeletePharmacy()
        {
            var p = requestData.GetData<DG_Pharmacology>(0);
            this.BindDb(p);
            NewObject<PharmacyMgr>().UpdatePharmacy(p);
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 保存药理分类
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SavePharmacy()
        {
            var p = requestData.GetData<DG_Pharmacology>(0);
            this.BindDb(p);
            var flag = NewObject<PharmacyMgr>().SavePharmacy(p);
            responseData.AddData(p.PharmID);
            responseData.AddData(flag);
            return responseData;
        }
    }
}
