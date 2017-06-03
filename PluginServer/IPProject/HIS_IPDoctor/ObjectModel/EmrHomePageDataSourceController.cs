using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_PublicManage.ObjectModel;

namespace HIS_IPDoctor.ObjectModel
{
    /// <summary>
    /// 病案首页数据获取
    /// </summary>
    [WCFController]
    public class EmrHomePageDataSourceController : WcfServerController
    {
        /// <summary>
        /// 获取病案首页病人信息数据
        /// </summary>
        /// <returns>病人信息数据</returns>
        [WCFMethod]
        public ServiceResponseData GetCasePatientInfo()
        {
            EmrHomePageDataSource emrCasemanagement = NewObject<EmrHomePageDataSource>();
            int patlistID = requestData.GetData<int>(0);
            DataTable dtPatInfo = emrCasemanagement.GetCasePatientInfo(patlistID);
            responseData.AddData(dtPatInfo);
            return responseData;
        }

        /// <summary>
        /// 获取病案首页病人转科数据
        /// </summary>
        /// <returns>病人转科数据</returns>
        [WCFMethod]
        public ServiceResponseData GetCaseTransDeptInfo()
        {
            EmrHomePageDataSource emrCasemanagement = NewObject<EmrHomePageDataSource>();
            int patlistID = requestData.GetData<int>(0);
            DataTable dtTransInfo = emrCasemanagement.GetCaseTransDeptInfo(patlistID);
            responseData.AddData(dtTransInfo);
            return responseData;
        }

        /// <summary>
        /// 获取病案首页病人诊断数据
        /// </summary>
        /// <returns>病人诊断数据</returns>
        [WCFMethod]
        public ServiceResponseData GetCaseDiagInfo()
        {
            EmrHomePageDataSource emrCasemanagement = NewObject<EmrHomePageDataSource>();
            int patlistID = requestData.GetData<int>(0);
            DataTable dtTransInfo = emrCasemanagement.GetCasediagInfo(patlistID);
            responseData.AddData(dtTransInfo);
            return responseData;
        }

        /// <summary>
        /// 获取首页基础数据信息
        /// </summary>
        /// <returns>首页基础数据信息</returns>
        [WCFMethod]
        public ServiceResponseData GetEmrHomeBasicData()
        {
            DataSet dataset = new DataSet();
            //疾病
            //DataTable dtDisease = NewObject<BasicDataManagement>().GetDisease();
            //dtDisease.TableName = "dtDisease";
            //dataset.Tables.Add(dtDisease.Copy());

            //医生
            DataTable dtDoctor = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.医生, false);
            dtDoctor.TableName = "dtDoctor";
            dataset.Tables.Add(dtDoctor.Copy());

            //护士
            DataTable dtNurse = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.护士, false);
            dtNurse.TableName = "dtNurse";
            dataset.Tables.Add(dtNurse.Copy());

            //所有人员
            DataTable dtUser = NewObject<BasicDataManagement>().GetBasicData(EmpDataSourceType.全部用户, false);
            dtUser.TableName = "dtUser";
            dataset.Tables.Add(dtUser.Copy());

            //手术级别
            DataTable dtOperLever = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.手术级别, false);
            dtOperLever.TableName = "dtOperLever";
            dataset.Tables.Add(dtOperLever.Copy());

            //切口愈合等级
            DataTable dtOperCutLever = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.切口愈合等级, false);
            dtOperCutLever.TableName = "dtOperCutLever";
            dataset.Tables.Add(dtOperCutLever.Copy());

            //麻配方式
            DataTable dtAnesisType = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.麻醉方式, false);
            dtAnesisType.TableName = "dtAnesisType";
            dataset.Tables.Add(dtAnesisType.Copy());

            //损伤中毒外部原因
            DataTable dtSsZd = NewObject<BasicDataManagement>().GetBasicData(PatientInfoDataSourceType.损伤中毒外部原因, false);
            dtSsZd.TableName = "dtSsZd";
            dataset.Tables.Add(dtSsZd.Copy());

            //手术
            DataTable dtOperation = NewObject<BasicDataManagement>().GetOperation();
            dtOperation.TableName = "dtOperation";
            dataset.Tables.Add(dtOperation.Copy());
            responseData.AddData(dataset);
            return responseData;
        }

        /// <summary>
        /// 获取首页病人费用信息
        /// </summary>
        /// <returns>病人费用信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCasePatFee()
        {
            EmrHomePageDataSource emrCasemanagement = NewObject<EmrHomePageDataSource>();
            int patlistID = requestData.GetData<int>(0);
            DataTable dtPatFee = emrCasemanagement.GetCasePatFee(patlistID);
            DataTable dtPatTotalFee = new DataTable();
            decimal antFee = 0;
            if (dtPatFee.Rows.Count > 0)
            {
                dtPatTotalFee = emrCasemanagement.GetCasePatTotalFee(patlistID);
                if (dtPatTotalFee.Rows.Count > 0)
                {
                    antFee = emrCasemanagement.GetAntFee(patlistID);
                }
            }
            else
            {
                dtPatFee = emrCasemanagement.GetCasePatInHospFee(patlistID);
                if (dtPatFee.Rows.Count > 0)
                {
                    dtPatTotalFee = emrCasemanagement.GetCasePatInHospTotalFee(patlistID);
                    if (dtPatTotalFee.Rows.Count > 0)
                    {
                        antFee = emrCasemanagement.GetAntFee(patlistID);
                    }
                }
            }

            responseData.AddData(dtPatFee);
            responseData.AddData(dtPatTotalFee);
            responseData.AddData(antFee);
            return responseData;
        }
    }
}
