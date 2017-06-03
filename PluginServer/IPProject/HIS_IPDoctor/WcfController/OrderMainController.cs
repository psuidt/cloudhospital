using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.IPManage;
using HIS_IPDoctor.Dao;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPDoctor.WcfController
{
    /// <summary>
    /// 住院医生站主界面后台控制器类
    /// </summary>
    [WCFController]
    public class OrderMainController : WcfServerController
    {
        /// <summary>
        /// 获取床位病人列表
        /// </summary>
        /// <returns>床位病人列表</returns>
        [WCFMethod]
        public ServiceResponseData GetBedPatient()
        {
            int doctorID = requestData.GetData<int>(0);
            int deptID = requestData.GetData<int>(1);
            DataTable dtMyPatient = NewDao<IIPDOrderDao>().GetInBedPatient(deptID, doctorID, true);
            DataTable dtDeptPatient = NewDao<IIPDOrderDao>().GetInBedPatient(deptID, doctorID, false);
            responseData.AddData(dtMyPatient);
            responseData.AddData(dtDeptPatient);
            return responseData;
        }

        /// <summary>
        /// 获取住院临床科室
        /// </summary>
        /// <returns>住院临床科室数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDeptList()
        {
            BasicDataManagement basicmanagement = NewObject<BasicDataManagement>();
            int doctorID = requestData.GetData<int>(0);
            DataTable dtDept = basicmanagement.GetBasicData(HIS_PublicManage.ObjectModel.DeptDataSourceType.住院临床科室, false);
            responseData.AddData(dtDept);
            return responseData;
        }

        /// <summary>
        /// 获取护理级别
        /// </summary>
        /// <returns>护理级别数据</returns>
        [WCFMethod]
        public ServiceResponseData getNusingLevel()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.护理级别, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取饮食种类
        /// </summary>
        /// <returns>饮食种类</returns>
        [WCFMethod]
        public ServiceResponseData getDietType()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.饮食种类, false);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 修改病人护理级别
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData UpdatePatNursing()
        {
            int patlistid = requestData.GetData<int>(0);
            string nusrsingName = requestData.GetData<string>(1);
            IP_PatList ipPatlist = NewObject<IP_PatList>().getmodel(patlistid) as IP_PatList;
            ipPatlist.NursingLever = nusrsingName;
            this.BindDb(ipPatlist);
            ipPatlist.save();

            #region "保存业务消息数据 --Add By ZhangZhong"
            // 保存业务消息数据
            Dictionary<string, string> msgDic = new Dictionary<string, string>();
            int workId = requestData.GetData<int>(2);
            int userId = requestData.GetData<int>(3);
            int deptId = requestData.GetData<int>(4);
            msgDic.Add("PatListID", ipPatlist.PatListID.ToString()); // 病人登记ID
            msgDic.Add("WorkID", workId.ToString()); // 消息机构ID
            msgDic.Add("SendUserId", userId.ToString()); // 消息生成人ID
            msgDic.Add("SendDeptId", deptId.ToString()); // 消息生成科室ID
            NewObject<BusinessMessage>().GenerateBizMessage(MessageType.护理级别, msgDic);
            #endregion

            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 修改病人饮食情况
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePatDietType()
        {
            int patlistid = requestData.GetData<int>(0);
            string dietType = requestData.GetData<string>(1);
            IP_PatList ipPatlist = NewObject<IP_PatList>().getmodel(patlistid) as IP_PatList;
            ipPatlist.DietType = dietType;
            this.BindDb(ipPatlist);
            ipPatlist.save();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 修改病人情况
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePatSituation()
        {
            int patlistid = requestData.GetData<int>(0);
            string situationCode = requestData.GetData<string>(1);
            IP_PatList ipPatlist = NewObject<IP_PatList>().getmodel(patlistid) as IP_PatList;
            ipPatlist.OutSituation = situationCode;
            this.BindDb(ipPatlist);
            ipPatlist.save();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 通过参数设置获取血糖报表Url
        /// </summary>
        /// <returns>返回血糖报表Url</returns>
        [WCFMethod]
        public ServiceResponseData GetBloodUrl()
        {
            string bloodUrl = NewObject<SysConfigManagement>().GetSystemConfigValue("BloodPluUrl").ToString();
            responseData.AddData(bloodUrl);
            return responseData;
        }
    }
}
