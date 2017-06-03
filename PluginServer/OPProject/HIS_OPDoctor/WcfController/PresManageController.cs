using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_Entity.OPManage;
using HIS_OPDoctor.Dao;
using HIS_OPDoctor.ObjectModel;
using HIS_PublicManage.ObjectModel;
using System.Linq;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 处方管理服务控制器
    /// </summary>
    [WCFController]
    public class PresManageController : WcfServerController
    {
        #region 门诊患者查询
        /// <summary>
        /// 取得医生所在科室信息
        /// </summary>
        /// <returns>医生所在科室信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDocRelateDeptInfo()
        {
            int empId = requestData.GetData<int>(0);
            DataTable dt = NewObject<OutPatient>().GetDocRelateDeptInfo(empId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <returns>病人列表数据</returns>
        [WCFMethod]
        public ServiceResponseData LoadPatientList()
        {
            int docId = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            string regBeginDate = requestData.GetData<string>(2);
            string regEndDate = requestData.GetData<string>(3);
            int visitStatus = requestData.GetData<int>(4);
            int belong = requestData.GetData<int>(5);
            DataTable dt = NewObject<OutPatient>().LoadPatientList(docId, deptId, regBeginDate, regEndDate, visitStatus, belong);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <returns>病人信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientInfo()
        {
            string id = requestData.GetData<string>(0);
            int type = requestData.GetData<int>(1);
            DataTable dt = NewObject<OutPatient>().GetPatientInfo(id, type);
            responseData.AddData(dt);
            int patListId = 0;
            if (dt.Rows.Count > 0)
            {
                patListId = Convert.ToInt32(dt.Rows[0]["PatListID"]);
            }

            DataTable dtDisease = NewObject<DiagnosisManage>().LoadDiagnosisList(patListId);
            responseData.AddData(dtDisease);

            //诊断信息暂不加
            return responseData;
        }

        /// <summary>
        /// 取得病人科室信息
        /// </summary>
        /// <returns>病人科室信息</returns>
        [WCFMethod]
        public ServiceResponseData LoadPatientInfo()
        {
            int id = requestData.GetData<int>(0);
            DataTable dt = NewObject<IOPDDao>().LoadPatientInfo(id);
            responseData.AddData(dt);
            int patListId = 0;
            if (dt.Rows.Count > 0)
            {
                patListId = Convert.ToInt32(dt.Rows[0]["PatListID"]);
            }

            DataTable dtDisease = NewObject<DiagnosisManage>().LoadDiagnosisList(patListId);
            responseData.AddData(dtDisease);

            //诊断信息暂不加
            return responseData;
        }
        #endregion

        #region 修改病人信息
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <returns>会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetMemberInfo()
        {
            int memberID = requestData.GetData<int>(0);
            DataTable dt = NewObject<OutPatient>().GetMemberInfo(memberID);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion

        #region 下诊断
        /// <summary>
        /// 获取诊断数据
        /// </summary>
        /// <returns>诊断数据</returns>
        [WCFMethod]
        public ServiceResponseData GetDisease()
        {
            DataTable dt = NewObject<BasicDataManagement>().GetDisease();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 加载诊断记录
        /// </summary>
        /// <returns>诊断记录</returns>
        [WCFMethod]
        public ServiceResponseData LoadDiagnosisList()
        {
            int patListID = requestData.GetData<int>(0);
            DataTable dt = NewObject<DiagnosisManage>().LoadDiagnosisList(patListID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 添加诊断记录
        /// </summary>
        /// <returns>诊断记录</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AddDiagnosis()
        {
            OPD_DiagnosisRecord model = requestData.GetData<OPD_DiagnosisRecord>(0);
            bool bRtn = NewObject<DiagnosisManage>().AddDiagnosis(model);
            bRtn = NewObject<DiagnosisManage>().AddCommonDiagnosis(model);//常用诊断
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 删除诊断记录
        /// </summary>
        /// <returns>bool删除成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteDiagnosis()
        {
            int diagnosisId = requestData.GetData<int>(0);
            int patListID = requestData.GetData<int>(1);
            bool bRtn = NewObject<DiagnosisManage>().DeleteDiagnosis(diagnosisId, patListID);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData SortDiagnosis()
        {
            DataTable dtDiagnosis = requestData.GetData<DataTable>(0);
            NewObject<DiagnosisManage>().SortDiagnosis(dtDiagnosis);
            responseData.AddData(true);
            return responseData;
        }
        #endregion

        #region 处方控件数据源
        /// <summary>
        /// 获取皮试对应的药品ID
        /// </summary>
        /// <returns>皮试对应的药品ID</returns>
        [WCFMethod]
        public ServiceResponseData GetActDrugId()
        {
            string actDrugId = NewObject<SysConfigManagement>().GetSystemConfigValue("ActPresDrugID");
            responseData.AddData(actDrugId);
            return responseData;
        }

        /// <summary>
        /// 取得药品执行药房
        /// </summary>
        /// <returns>药房信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugStoreRoom()
        {
            int presType = requestData.GetData<int>(0);
            DataTable dt = NewObject<PrescriptionProcess>().GetDrugStoreRoom(presType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取用法
        /// </summary>
        /// <returns>用法数据</returns>
        [WCFMethod]
        public ServiceResponseData GetChannelData()
        {
            List<Basic_Channel> list = NewObject<Basic_Channel>().getlist<Basic_Channel>(" DelFlag=0 and OutUsed=1").OrderBy(p=>p.SortOrder).ToList();
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取频次
        /// </summary>
        /// <returns>频次数据</returns>
        [WCFMethod]
        public ServiceResponseData GetFrequencyData()
        {
            List<Basic_Frequency> list = NewObject<Basic_Frequency>().getlist<Basic_Frequency>(" DelFlag=0").OrderBy(p=>p.SortOrder).ToList();          
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取嘱托
        /// </summary>
        /// <returns>嘱托数据</returns>
        [WCFMethod]
        public ServiceResponseData GetEntrustData()
        {
            List<Basic_Entrust> list = NewObject<Basic_Entrust>().getlist<Basic_Entrust>(" DelFlag=0");
            responseData.AddData(list);
            return responseData;
        }

        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns>单位数据</returns>
        [WCFMethod]
        public ServiceResponseData GetUnitData()
        {
            DataTable dt = new DataTable();
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取药品项目数据
        /// </summary>
        /// <returns>药品项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetShowCardData()
        {
            int itemclass = requestData.GetData<int>(0);
            int statID = requestData.GetData<int>(1);
            int deptId = requestData.GetData<int>(2);
            DataTable dtItems = NewObject<IOPDDao>().GetFeeItemShowCardDatas();
            responseData.AddData(dtItems);
            return responseData;
        }

        /// <summary>
        /// 获取处方模板药品项目数据
        /// </summary>
        /// <returns>处方模板药品项目数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPresTplShowCardData()
        {
            int itemclass = requestData.GetData<int>(0);
            int statID = requestData.GetData<int>(1);
            int deptId = requestData.GetData<int>(2);
            DataTable dtItems = NewObject<IOPDDao>().GetFeeItemShowCardData(itemclass, statID, deptId);
            responseData.AddData(dtItems);
            return responseData;
        }

        /// <summary>
        /// 取得药品信息
        /// </summary>
        /// <returns>药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugItem()
        {
            int itemId = requestData.GetData<int>(0);
            int deptId = requestData.GetData<int>(1);
            DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(itemId, deptId);
            responseData.AddData(dtItems);
            return responseData;
        }

        /// <summary>
        /// 取得药品信息
        /// </summary>
        /// <returns>药品信息</returns>
        [WCFMethod]
        public ServiceResponseData GetDrugItemTpl()
        {
            int itemId = requestData.GetData<int>(0);
            DataTable dtItems = NewObject<IOPDDao>().GetDrugItem(itemId, -1);
            responseData.AddData(dtItems);
            return responseData;
        }

        /// <summary>
        /// 删除处方明细
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeletePrescriptionData()
        {
            int presDetailId = requestData.GetData<int>(0);
            bool bRtn = NewObject<IOPDDao>().DeletePrescriptionData(presDetailId);
            bool bRtnLink = NewObject<PrescriptionProcess>().DeleteLinkFeeItems(presDetailId);
            responseData.AddData(bRtn);
            responseData.AddData(bRtnLink);
            return responseData;
        }

        /// <summary>
        /// 删除一组处方
        /// </summary>
        /// <returns>true删除成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DeleteGroupPrescriptionData()
        {
            int patListId = requestData.GetData<int>(0);
            int presType = requestData.GetData<int>(1);
            int presNo = requestData.GetData<int>(2);
            bool bRtn = NewObject<IOPDDao>().DeletePrescriptionData(patListId, presType, presNo);
            bool bRtnLink = NewObject<PrescriptionProcess>().DeleteLinkFeeItems(patListId, presType, presNo);
            responseData.AddData(bRtn);
            responseData.AddData(bRtnLink);
            return responseData;
        }

        /// <summary>
        /// 获取处方
        /// </summary>
        /// <returns>处方数据</returns>
        [WCFMethod]
        public ServiceResponseData GetPrescriptionData()
        {
            int patListId = requestData.GetData<int>(0);
            int presType = requestData.GetData<int>(1);

            DataTable dt = NewObject<IOPDDao>().GetPrescriptionData(patListId, presType);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 是否结算处方
        /// </summary>
        /// <returns>true是收费</returns>
        [WCFMethod]
        public ServiceResponseData IsCostPres()
        {
            string ids = requestData.GetData<string>(0);
            bool bRtn = NewObject<IOPDDao>().IsCostPres(ids);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <returns>true有库存</returns>
        [WCFMethod]
        public ServiceResponseData IsDrugStore()
        {
            int deptId = requestData.GetData<int>(0);
            int itemId = requestData.GetData<int>(1);
            decimal qty = requestData.GetData<decimal>(2);
            bool bRtn = NewObject<IOPDDao>().IsDrugStore(deptId, itemId, qty);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 判断所有库存
        /// </summary>
        /// <returns>库存药品信息</returns>
        [WCFMethod]
        public ServiceResponseData IsDrugAllStore()
        {
            string ids = requestData.GetData<string>(0);
            DataTable dt = NewObject<IOPDDao>().IsDrugStore(ids);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 更新自备标志
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePresSelfDrugFlag()
        {
            int presDetailId = requestData.GetData<int>(0);
            int flag = requestData.GetData<int>(1);
            bool bRtn = NewObject<IOPDDao>().UpdatePresSelfDrugFlag(presDetailId, flag);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 更新医保报销标志
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePresReimbursementFlag()
        {
            List<OPD_PresDetail> list = requestData.GetData<List<OPD_PresDetail>>(0);
            int flag = requestData.GetData<int>(1);
            bool bRtn = NewObject<IOPDDao>().UpdatePresReimbursementFlag(list, flag);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 更新本院注射次数
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePresInjectTimes()
        {
            int presDetailId = requestData.GetData<int>(0);
            string menuText = requestData.GetData<string>(1);
            int execTimes = requestData.GetData<int>(2);
            bool bRtn = NewObject<PrescriptionProcess>().UpdatePresInjectTimes(presDetailId, menuText, execTimes);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 更新处方号和组Id
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData UpdatePresNoAndGroupId()
        {
            List<OPD_PresDetail> list = requestData.GetData<List<OPD_PresDetail>>(0);
            bool bRtn = NewObject<IOPDDao>().UpdatePresNoAndGroupId(list);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SavePrescriptionData()
        {
            int patListId = requestData.GetData<int>(0);
            int selectedMemberID = requestData.GetData<int>(1);
            int presType = requestData.GetData<int>(2);
            List<OPD_PresDetail> list = requestData.GetData<List<OPD_PresDetail>>(3);
            //更新当前医生ID
            if (list.Count > 0)
            {
                NewObject<PrescriptionProcess>().UpdatePatCurrentDoctorID(patListId, list[0].PresDoctorID, list[0].PresDeptID);
            }

            bool bRtn = NewObject<IOPDDao>().SavePrescriptionData(patListId, selectedMemberID, presType, list);
            //生成关联费用
            bool bRtnLink = NewObject<PrescriptionProcess>().SaveLinkFeeItems(patListId, presType, list);
            responseData.AddData(bRtn);
            responseData.AddData(bRtnLink);
            return responseData;
        }

        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns>系统参数信息</returns>
        [WCFMethod]
        public ServiceResponseData GetSystemParamenter()
        {
            SysConfigManagement basic = NewObject<SysConfigManagement>();
            string regValidDays = basic.GetSystemConfigValue("RegValidPeriod");
            string presCount = basic.GetSystemConfigValue("PresCount");
            string drugRepeatWarn = basic.GetSystemConfigValue("DrugRepeatWarn");
            string dayGreater30 = basic.GetSystemConfigValue("DayGreater30");
            string canPrintChargedPres = basic.GetSystemConfigValue("CanPrintChargedPres");
            responseData.AddData(regValidDays);
            responseData.AddData(presCount);
            responseData.AddData(drugRepeatWarn);
            responseData.AddData(dayGreater30);
            responseData.AddData(canPrintChargedPres);
            return responseData;
        }

        /// <summary>
        /// 获取处方打印的信息
        /// </summary>
        /// <returns>处方打印信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPrintPresData()
        {
            int preHeadId = requestData.GetData<int>(0);
            int preNo = requestData.GetData<int>(1);
            DataTable presData = NewDao<IOPDDao>().GetPrintPresData(preHeadId, preNo);
            responseData.AddData(presData);
            return responseData;
        }

        /// <summary>
        /// 通过处方模板Id获取处方模板信息
        /// </summary>
        /// <returns>处方模板信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPresTemplate()
        {
            int tplId = requestData.GetData<int>(0);
            DataTable dt =  NewObject<IOPDDao>().GetPresTemplate(tplId);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 获取处方模板行
        /// </summary>
        /// <returns>处方模板行</returns>
        [WCFMethod]
        public ServiceResponseData GetPresTemplateRow()
        {
            int[] tpldetailIds = requestData.GetData<int[]>(0);
            DataTable dt = NewObject<IOPDDao>().GetPresTemplateRow(tpldetailIds);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 处方模板另存为
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData AsSavePresTemplate()
        {
            OPD_PresMouldHead newMould = requestData.GetData<OPD_PresMouldHead>(0);
            List<OPD_PresDetail> presList = requestData.GetData<List<OPD_PresDetail>>(1);
            SetWorkId(oleDb.WorkId);
            bool result = NewObject<PrescriptionProcess>().AsSavePresTemplate(newMould, presList);
            responseData.AddData(result);
            return responseData;
        }
        #endregion

        #region 结束就诊
        /// <summary>
        /// 结束就诊
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData CompleteDiagonsis()
        {
            int patListId = requestData.GetData<int>(0);
            OP_PatList pat = (OP_PatList)NewObject<OP_PatList>().getmodel(patListId);
            pat.VisitStatus = 2;
            this.BindDb(pat);
            int iRtn = pat.save();
            if (iRtn > 0)
            {
                responseData.AddData(true);
            }
            else
            {
                responseData.AddData(false);
            }

            return responseData;
        }
        #endregion

        #region 开住院证
        /// <summary>
        /// 获取病人以及会员信息
        /// </summary>
        /// <returns>病人以及会员信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientData()
        {
            int patListId = requestData.GetData<int>(0);
            DataTable dtPatList = NewDao<IOPDDao>().GetPatientData(patListId);
            responseData.AddData(dtPatList);
            DataTable dtDisease = NewObject<DiagnosisManage>().LoadDiagnosisList(patListId);
            responseData.AddData(dtDisease);
            return responseData;
        }

        /// <summary>
        /// 获取病人住院证信息
        /// </summary>
        /// <returns>病人住院证信息</returns>
        [WCFMethod]
        public ServiceResponseData GetInpatientReg()
        {
            int patListId = requestData.GetData<int>(0);
            OPD_InpatientReg inpatientReg = NewDao<IOPDDao>().GetInpatientReg(patListId);
            responseData.AddData(inpatientReg);
            return responseData;
        }

        /// <summary>
        /// 保存病人住院登记信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        public ServiceResponseData SaveInpatientReg()
        {
            OPD_InpatientReg inpatientReg = requestData.GetData<OPD_InpatientReg>(0);
            int workId = requestData.GetData<int>(1);
            SetWorkId(workId);
            BindDb(inpatientReg);
            int result = inpatientReg.save();
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 获取床位信息
        /// </summary>
        /// <returns>床位信息</returns>
        [WCFMethod]
        public ServiceResponseData GetBedInfo()
        {
            int workId = requestData.GetData<int>(0);
            DataTable dtBedInfo = NewDao<IOPDDao>().GetBedInfo(workId);
            responseData.AddData(dtBedInfo);
            return responseData;
        }
        #endregion

        #region 常用诊断
        /// <summary>
        /// 加载常用诊断
        /// </summary>
        /// <returns>常用诊断</returns>
        [WCFMethod]
        public ServiceResponseData LoadCommonDianosis()
        {
            int doctorID = requestData.GetData<int>(0);
            DataTable dt = NewDao<IOPDDao>().LoadCommonDianosis(doctorID);
            responseData.AddData(dt);
            return responseData;
        }

        /// <summary>
        /// 删除常用诊断
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        public ServiceResponseData DeleteCommonDianosis()
        {
            int commonDiagnosisID = requestData.GetData<int>(0);
            bool bRtn = NewObject<DiagnosisManage>().DeleteCommonDianosis(commonDiagnosisID);
            responseData.AddData(bRtn);
            return responseData;
        }
        #endregion

        #region 复制历史就诊记录
        /// <summary>
        /// 复制历史就诊记录
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData OneCopy()
        {
            bool bDiseaseHis = requestData.GetData<bool>(0);
            bool bWest = requestData.GetData<bool>(1);
            bool bChinese = requestData.GetData<bool>(2);
            bool bFee = requestData.GetData<bool>(3);
            int currentPatId = requestData.GetData<int>(4);
            int hisPatListId = requestData.GetData<int>(5);
            int presDoctorID = requestData.GetData<int>(6);
            int presDeptID = requestData.GetData<int>(7);
            bool bRtn = NewObject<PrescriptionProcess>().OneCopy(bDiseaseHis, bWest, bChinese, bFee, currentPatId, hisPatListId,presDoctorID, presDeptID);
            responseData.AddData(bRtn);
            return responseData;
        }
        #endregion

        #region 门诊病历
        /// <summary>
        /// 查询病历信息
        /// </summary>
        /// <returns>病历信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientOMRData()
        {
            int patListId = requestData.GetData<int>(0);
            OPD_MedicalRecord modelOMR = NewObject<OMRManager>().GetPatientOMRData(patListId);
            responseData.AddData(modelOMR);
            return responseData;
        }

        /// <summary>
        /// 保存病历信息
        /// </summary>
        /// <returns>true成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveOMRData()
        {
            OPD_MedicalRecord omrModel = requestData.GetData<OPD_MedicalRecord>(0);
            //更新当前医生ID
            NewObject<PrescriptionProcess>().UpdatePatCurrentDoctorID(omrModel.PatListID, omrModel.PresDoctorID, omrModel.PresDeptID);
        
            bool bRtn = NewObject<OMRManager>().SaveOMRData(omrModel);
            responseData.AddData(bRtn);
            return responseData;
        }

        /// <summary>
        /// 取得特殊字符
        /// </summary>
        /// <returns>特殊字符</returns>
        [WCFMethod]
        public ServiceResponseData GetSymbolData()
        {
            DataTable dtSymbolType = NewDao<IOPDDao>().GetSymbolType();
            DataTable dtSymbolContent = NewDao<IOPDDao>().GetSymbolContent();
            responseData.AddData(dtSymbolType);
            responseData.AddData(dtSymbolContent);
            return responseData;
        }

        /// <summary>
        /// 取得病历打印数据
        /// </summary>
        /// <returns>病历打印数据</returns>
        [WCFMethod]
        public ServiceResponseData GetOMRPrintPresData()
        {
            int patListId = requestData.GetData<int>(0);
            DataTable dt = NewObject<IOPDDao>().GetOMRPrintPresData(patListId);
            responseData.AddData(dt);
            return responseData;
        }
        #endregion
    }
}
