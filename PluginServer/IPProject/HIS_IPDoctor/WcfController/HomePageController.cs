using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;

namespace HIS_IPDoctor.WcfController
{
    /// <summary>
    /// 病案首页服务端控制器
    /// </summary>
    [WCFController]
    public class HomePageController : WcfServerController
    {
        /// <summary>
        /// 保存和修改病案首页记录
        /// </summary>
        /// <returns>true</returns>
        [WCFMethod]
        public ServiceResponseData SaveCaseRecord()
        {
            Emr_CaseRecord caseRecord = requestData.GetData<Emr_CaseRecord>(0);
            this.BindDb(caseRecord);
            caseRecord.save();
            responseData.AddData(true);
            return responseData;
        }

        /// <summary>
        /// 获取病案首页记录
        /// </summary>
        /// <returns>病案首页记录</returns>
        [WCFMethod]
        public ServiceResponseData GetCaseRecord()
        {
            int patlistid = requestData.GetData<int>(0);
            List<Emr_CaseRecord> caseRecords = NewObject<Emr_CaseRecord>().getlist<Emr_CaseRecord>(" Patlistid=" + patlistid + " And DeleteStatus=0");
            responseData.AddData(caseRecords);
            return responseData;
        }

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <returns>病人基本信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCasePatientInfo()
        {
            int patlistid = requestData.GetData<int>(0);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = LoginUserInfo;
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("EMRHISInterface.Service", "EmrHomePageDataSourceController", "GetCasePatientInfo", requestAction);
            DataTable dtPatInfo = retdata.GetData<DataTable>(0);
            //病人基本信息
            MedicalCasePatient casePatientInfo = new MedicalCasePatient();
            if (dtPatInfo != null || dtPatInfo.Rows.Count > 0)
            {
                casePatientInfo = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MedicalCasePatient>(dtPatInfo, 0);
                string age = casePatientInfo.Age;
                if (casePatientInfo.Status != 3 && casePatientInfo.Status != 4)
                {
                    casePatientInfo.LeaveHDate = string.Empty;
                    casePatientInfo.CurrDeptName = string.Empty;
                    casePatientInfo.CurrwardName = string.Empty;
                }
                else
                {
                    casePatientInfo.LeaveHDate = dtPatInfo.Rows[0]["LeaveDate"].ToString();
                }

                //转科科室
                requestAction = ((ClientRequestData request) =>
                {
                    request.LoginRight = LoginUserInfo;
                    request.AddData(patlistid);
                });
                retdata = InvokeWcfService("EMRHISInterface.Service", "EmrHomePageDataSourceController", "GetCaseTransDeptInfo", requestAction);
                DataTable dtTransInfo = retdata.GetData<DataTable>(0);
                if (dtTransInfo != null && dtTransInfo.Rows.Count > 0)
                {
                    string strTrans = string.Empty;
                    strTrans = dtTransInfo.Rows[0]["newDeptName"].ToString();
                    for (int i = 1; i < dtTransInfo.Rows.Count; i++)
                    {
                        strTrans += "→" + dtTransInfo.Rows[i]["newDeptName"].ToString();
                    }

                    casePatientInfo.TransDeptName = strTrans;
                }

                //实际住院天数
                DateTime dt1 = casePatientInfo.Status == 4 || casePatientInfo.Status == 3 ? Convert.ToDateTime(dtPatInfo.Rows[0]["LeaveDate"].ToString()) : DateTime.Now;
                DateTime dt2 = casePatientInfo.EnterHDate;
                TimeSpan ts = dt1 - dt2;
                if (ts.Days == 0)
                {
                    casePatientInfo.InHospDays = 1;
                }
                else
                {
                    casePatientInfo.InHospDays = ts.Days - 1;
                }
            }

            responseData.AddData(casePatientInfo);
            return responseData;
        }

        /// <summary>
        /// 获取病人诊断信息
        /// </summary>
        /// <returns>病人诊断信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCaseDiagInfo()
        {
            int patlistid = requestData.GetData<int>(0);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = LoginUserInfo;
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("EMRHISInterface.Service", "EmrHomePageDataSourceController", "GetCaseDiagInfo", requestAction);
            DataTable dtDiagInfo = retdata.GetData<DataTable>(0);
            MedicalCaseDiagoInfo caseDiagInfo = new MedicalCaseDiagoInfo();
            if (dtDiagInfo.Rows.Count > 0)
            {
                int opddiagId = 0;
                int mainDiagId = 0;
                DataRow[] rows = dtDiagInfo.Select("Code='01'");
                if (rows.Length > 0)
                {
                    opddiagId = Convert.ToInt32(rows[0]["Id"]);
                    caseDiagInfo.OpdocDiagName = rows[0]["DiagnosisName"].ToString();
                    caseDiagInfo.OpdocDiagCode = rows[0]["ICDCode"].ToString();
                }

                rows = dtDiagInfo.Select("Main=1");
                if (rows.Length > 0)
                {
                    mainDiagId = Convert.ToInt32(rows[0]["Id"]);
                    caseDiagInfo.MainDiagName = rows[0]["DiagnosisName"].ToString();
                    caseDiagInfo.MainDiagNameCode = rows[0]["ICDCode"].ToString();
                    caseDiagInfo.Rybq1 = rows[0]["Effect"].ToString();
                }

                int index = 2;
                for (int i = 0; i < dtDiagInfo.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDiagInfo.Rows[i]["ID"]) != opddiagId && Convert.ToInt32(dtDiagInfo.Rows[i]["ID"]) != mainDiagId)
                    {
                        string zdname = "Cyzd" + index;
                        string zdcode = "CyzdCode" + index;
                        string rybq = "Rybq" + index;
                        SetPropertyValues(caseDiagInfo, zdname, dtDiagInfo.Rows[i]["DiagnosisName"].ToString());
                        SetPropertyValues(caseDiagInfo, zdcode, dtDiagInfo.Rows[i]["ICDCode"].ToString());
                        SetPropertyValues(caseDiagInfo, rybq, dtDiagInfo.Rows[i]["Effect"].ToString());
                        index += 1;
                    }
                }
            }

            responseData.AddData(caseDiagInfo);
            return responseData;
        }

        /// <summary>
        /// 给实体属性赋值
        /// </summary>
        /// <param name="item">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        private void SetPropertyValues(MedicalCaseDiagoInfo item, string propertyName, string propertyValue)
        {
            System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == propertyName)
                {
                    properties[i].SetValue(item, propertyValue);
                }
            }
        }

        /// <summary>
        /// 获取病案首页基础数据信息
        /// </summary>
        /// <returns>首页基础数据信息</returns>
        [WCFMethod]
        public ServiceResponseData GetEmrHomeBasicData()
        {
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = LoginUserInfo;
            });
            ServiceResponseData retdata = InvokeWcfService("EMRHISInterface.Service", "EmrHomePageDataSourceController", "GetEmrHomeBasicData", requestAction);
            DataSet dataset = retdata.GetData<DataSet>(0);
            responseData.AddData(dataset);
            return responseData;
        }

        /// <summary>
        /// 获取病案首页病人费用信息
        /// </summary>
        /// <returns>首页病人费用信息</returns>
        [WCFMethod]
        public ServiceResponseData GetCasePatFee()
        {
            int patlistid = requestData.GetData<int>(0);
            Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
            {
                request.LoginRight = LoginUserInfo;
                request.AddData(patlistid);
            });
            ServiceResponseData retdata = InvokeWcfService("EMRHISInterface.Service", "EmrHomePageDataSourceController", "GetCasePatFee", requestAction);
            DataTable dtFee = retdata.GetData<DataTable>(0);
            MedicalCaseFeeInfo feeInfo = new MedicalCaseFeeInfo();
            if (dtFee != null)
            {
                for (int i = 0; i < dtFee.Rows.Count; i++)
                {
                    SetFeePropertyValues(feeInfo, dtFee.Rows[i][0].ToString(), dtFee.Rows[i][1].ToString());
                }
            }

            DataTable dtTotalFee = retdata.GetData<DataTable>(1);
            if (dtTotalFee != null && dtTotalFee.Rows.Count > 0)
            {
                feeInfo.TotalFee = Convert.ToDecimal(Convert.ToDecimal(dtTotalFee.Rows[0][0]).ToString("0.00"));
                feeInfo.SelfFee = Convert.ToDecimal(Convert.ToDecimal(dtTotalFee.Rows[0][1]).ToString("0.00"));
            }

            feeInfo.抗菌药物费用 = retdata.GetData<decimal>(2);
            feeInfo.手术治疗费 = feeInfo.手术治疗费 + feeInfo.麻醉费 + feeInfo.手术费;
            responseData.AddData(feeInfo);
            return responseData;
        }

        /// <summary>
        /// 对象属性赋值
        /// </summary>
        /// <param name="item">对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyValue">属性值</param>
        /// <returns>返回MedicalCaseFeeInfo</returns>
        private MedicalCaseFeeInfo SetFeePropertyValues(MedicalCaseFeeInfo item, string propertyName, string propertyValue)
        {
            System.Reflection.PropertyInfo[] properties = item.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name == propertyName)
                {
                    properties[i].SetValue(item, Convert.ToDecimal(Convert.ToDecimal(propertyValue).ToString("0.00")));
                }
            }

            return item;
        }
    }
}
