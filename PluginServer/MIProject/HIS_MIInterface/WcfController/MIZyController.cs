using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using System.Data;
using HIS_MIInterface.Dao;
using HIS_PublicManage.ObjectModel;
using System;

namespace HIS_MIInterface.WcfController
{
    [WCFController]
    public class MIZyController : WcfServerController
    {
        [WCFMethod]
        public ServiceResponseData M_GetMIDept()
        {
            // 调用共通接口查询科室列表
            DataTable deptDt = NewObject<BasicDataManagement>().GetBasicData(DeptDataSourceType.住院临床科室, false);
            responseData.AddData(deptDt);
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData Zy_GetMIPatient()
        {
            int iMiType = requestData.GetData<int>(0);
            int iDeptCode = requestData.GetData<int>(1);

            DataTable dt = NewDao<IZYInterface>().Zy_GetMIPatient(iMiType, iDeptCode);
            responseData.AddData(dt);
            return responseData;
        }
         
        [WCFMethod]
        public ServiceResponseData Zy_GetPatientInfo()
        {
            int PatientId = requestData.GetData<int>(0);
            int iFeeType = requestData.GetData<int>(1);

            DataTable dt = NewDao<IZYInterface>().Zy_GetPatientInfo(PatientId, iFeeType);
            responseData.AddData(dt);
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData Zy_UploadzyPatFee()
        {
            int PatientId = requestData.GetData<int>(0);
            int iFlag = requestData.GetData<int>(1);

            bool b = NewDao<IZYInterface>().Zy_UploadzyPatFee(PatientId, iFlag);
            responseData.AddData(b);
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData Zy_ReSetzyPatFee()
        {
            int feeRecordID = requestData.GetData<int>(0);
            bool b = NewDao<IZYInterface>().Zy_ReSetzyPatFee(feeRecordID);
            responseData.AddData(b);
            return responseData;
        }

        /// <summary>
        /// 获取医保类型
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData M_GetMIType()
        {
            DataTable dtMz = NewDao<IZYInterface>().M_GetMIType("-1");
            DataTable dtZy = NewDao<IZYInterface>().M_GetMIType("0");
            responseData.AddData(dtMz);
            responseData.AddData(dtZy);
            return responseData;
        }

        [WCFMethod]
        public ServiceResponseData Mz_GetOutPatientFee()
        {
            int iPatType = requestData.GetData<int>(0);
            DateTime dDate = requestData.GetData<DateTime>(1);
            DataTable dt = NewDao<IZYInterface>().Mz_GetOutPatientFee(iPatType, dDate);
            responseData.AddData(dt);
            return responseData;
        }
    }
}
