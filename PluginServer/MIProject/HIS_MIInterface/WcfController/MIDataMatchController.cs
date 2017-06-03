using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_MIInterface.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.WcfController
{
    [WCFController]
    public class MIDataMatchController : WcfServerController
    {
        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMIType()
        {
            DataTable dt = NewDao<IDataMatchInterface>().M_GetMIType();
            responseData.AddData(dt);
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData M_GetHISDataInfo()
        {
            int ybid = requestData.GetData<int>(0);
            int dataType = requestData.GetData<int>(1);

            DataTable dt = NewDao<IDataMatchInterface>().M_GetHISDataInfo(ybid, dataType);
            responseData.AddData(dt);
            return responseData;
        }
        /// <summary>
        /// 获取医保目录
        /// </summary>
        [WCFMethod]
        public ServiceResponseData M_GetMIDataInfo()
        {
            int ybid = requestData.GetData<int>(0);
            int dataType = requestData.GetData<int>(1);

            DataTable dt = NewDao<IDataMatchInterface>().M_GetMIDataInfo(ybid, dataType);
            responseData.AddData(dt);
            return responseData;
        }
        /// <summary>
        /// 获取匹配目录
        /// </summary>
        [WCFMethod]
        public ServiceResponseData M_GetMatchDataInfo()
        {
            int ybid = requestData.GetData<int>(0);
            int dataType = requestData.GetData<int>(1);

            DataTable dt = NewDao<IDataMatchInterface>().M_GetMatchDataInfo(ybid, dataType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_DeleteMatchData()
        {
            int id = requestData.GetData<int>(0);

            bool bResult = NewDao<IDataMatchInterface>().M_DeleteMatchData(id);
            responseData.AddData(bResult);
            return responseData;
        }

        /// <summary>
        /// 保存基础数据匹配
        /// </summary>
        /// <param name="iMIDataID"></param>
        /// <param name="iDataType"></param>
        /// <param name="iHospDataID"></param>
        /// <param name="iMIID"></param>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_SaveMatchData()
        {
            int iMIDataID = requestData.GetData<int>(0);
            int iDataType = requestData.GetData<int>(1);
            int iHospDataID = requestData.GetData<int>(2);
            int iMIID = requestData.GetData<int>(3);

            bool bResult = NewDao<IDataMatchInterface>().M_SaveMatchData(iMIDataID, iDataType, iHospDataID, iMIID);
            responseData.AddData(bResult);
            return responseData;
        }
    }
}
