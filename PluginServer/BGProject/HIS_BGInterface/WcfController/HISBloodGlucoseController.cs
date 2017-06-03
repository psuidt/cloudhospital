using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using System.Data;
using HIS_BGInterface.Dao;
using HIS_Entity.IPManage;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;

namespace HIS_BGInterface.WcfController
{
    [WCFController]
    public class HISBloodGlucoseController: WcfServerController
    {
        [WCFMethod]
        public ServiceResponseData GetHello()
        {
            try
            {
                responseData.AddData("hello");
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 1.获取在院患者主索引基本信息 todo:仅在床
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetInPatientBaseInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtInPatientBaseInfo = NewDao<IHISBloodGlucoseDao>().GetInPatientBaseInfo(iWorkId);
                responseData.AddData(dtInPatientBaseInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 2.获取在院患者住院信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetInPatientHospitalizationInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtInPatientHospitalizationInfo = NewDao<IHISBloodGlucoseDao>().GetInPatientHospitalizationInfo(iWorkId);
                responseData.AddData(dtInPatientHospitalizationInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 3.获取在用科室信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetWorkDeptInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtDeptInfo = NewDao<IHISBloodGlucoseDao>().GetWorkDeptInfo(iWorkId);
                responseData.AddData(dtDeptInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 4.获取在用病区信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetWorkAreaInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtAreaInfo = NewDao<IHISBloodGlucoseDao>().GetWorkAreaInfo(iWorkId);
                responseData.AddData(dtAreaInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 5.获取在用床位信息
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetWorkBedInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtBedInfo = NewDao<IHISBloodGlucoseDao>().GetWorkBedInfo(iWorkId);
                responseData.AddData(dtBedInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }

        }

        /// <summary>
        /// 6.获取在用用户信息 
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData GetWorkUserInfo()
        {
            try
            {
                int iWorkId = requestData.GetData<int>(0);
                DataTable dtUserInfo = NewDao<IHISBloodGlucoseDao>().GetWorkUserInfo(iWorkId);
                responseData.AddData(dtUserInfo);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("err");
            }
        }

        /// <summary>
        /// 7,血糖数据上传到HIS
        /// </summary>
        /// <returns></returns>
        [WCFMethod]
        public ServiceResponseData InsertPluRecord()
        {
            try
            {
                int workid = requestData.GetData<int>(0);
                IP_PluRecord pluRecord = requestData.GetData<IP_PluRecord>(1);
                if (pluRecord == null)
                {
                    throw new Exception("没有测量数据");
                }
                if (pluRecord.PatlistID == 0)
                {
                    throw new Exception("病人ID不能为0");
                }
                SetWorkId(workid);//设置WorkID

                int ID = NewDao<IHISBloodGlucoseDao>().GetPluRecordID(pluRecord);
                pluRecord.ID = ID;
                this.BindDb(pluRecord);
                pluRecord.save();
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("血糖数据错误：" + e.Message);
            }
        }


        /// <summary>
        /// 7,血糖数据上传到HIS
        /// </summary>
        /// <returns></returns>
        [AOP(typeof(AopTransaction))]
        [WCFMethod]        
        public ServiceResponseData InsertPluRecordList()
        {
            try
            {
                int workid = requestData.GetData<int>(0);
                List<IP_PluRecord> pluRecordList = requestData.GetData<List< IP_PluRecord>>(1);
                if (pluRecordList == null || pluRecordList.Count==0)
                {
                    throw new Exception("没有测量数据");
                }
                foreach (IP_PluRecord pluRecord in pluRecordList)
                {
                    if (pluRecord.PatlistID == 0)
                    {
                        throw new Exception("病人ID不能为0");
                    }
                    SetWorkId(workid);//设置WorkID
                    int ID = NewDao<IHISBloodGlucoseDao>().GetPluRecordID(pluRecord);
                    pluRecord.ID = ID;
                    this.BindDb(pluRecord);
                    pluRecord.save();
                }
                responseData.AddData(true);
                return responseData;
            }
            catch (Exception e)
            {
                throw new Exception("血糖数据错误：" + e.Message);
            }
        }

      
    }
}
