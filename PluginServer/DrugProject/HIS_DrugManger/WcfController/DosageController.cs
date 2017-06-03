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
    /// 剂型维护控制器
    /// </summary>
    [WCFController]
    public class DosageController: WcfServerController
    {
        /// <summary>
        /// 获取药品剂型数据
        /// </summary>
        /// <returns>药品剂型数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDosageData()
        {
            var dic = requestData.GetData<Dictionary<string, string>>(0);
            DataTable dt = NewObject<IDGDao>().GetDosageUserOr(dic);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData SaveDosagData()
        { 
            var p = requestData.GetData<DG_DosageDic>(0);
           bool flag=NewObject<DosageMgr>().SaveDosage(p);
            responseData.AddData(flag);
            return responseData;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <returns>处理结果</returns>
        [WCFMethod]
        public ServiceResponseData DeleteDosage()
        {
            var pruduct = requestData.GetData<DG_DosageDic>(0);
            this.BindDb(pruduct);
            NewObject<DosageMgr>().UpdateDosage(pruduct);
            responseData.AddData(true);
            return responseData;
        }
    }
}
