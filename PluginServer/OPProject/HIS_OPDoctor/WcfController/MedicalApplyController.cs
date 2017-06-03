using System.Data;
using EFWCoreLib.CoreFrame.Business.AttributeInfo;
using EFWCoreLib.CoreFrame.DbProvider.Transaction;
using EFWCoreLib.CoreFrame.EntLib.Aop;
using EFWCoreLib.WcfFrame.DataSerialize;
using EFWCoreLib.WcfFrame.ServerController;
using HIS_Entity.ClinicManage;
using HIS_Entity.IPManage;
using HIS_OPDoctor.Dao;
using HIS_OPDoctor.ObjectModel;

namespace HIS_OPDoctor.WcfController
{
    /// <summary>
    /// 医技申请服务控制器
    /// </summary>
    [WCFController]
    public class MedicalApplyController : WcfServerController
    {
        /// <summary>
        /// 获取医技申请科室
        /// </summary>
        /// <returns>医技申请科室</returns>
        [WCFMethod]
        public ServiceResponseData GetExecDept()
        {
            var workerId = requestData.GetData<int>(0);
            var examclass = requestData.GetData<int>(1);
            var dept = NewDao<IOPDDao>()
                .GetExecDept(workerId, examclass);
            responseData.AddData(dept);
            return responseData;
        }

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <returns>项目分类</returns>
        [WCFMethod]
        public ServiceResponseData GetExamType()
        {
            var deptId = requestData.GetData<int>(0);
            var examclass = requestData.GetData<int>(1);
            var type = NewDao<IOPDDao>()
                .GetExamType(deptId, examclass);
            responseData.AddData(type);
            return responseData;
        }

        /// <summary>
        /// 根据项目分类获取项目信息
        /// </summary>
        /// <returns>项目分类获取项目信息</returns>
        [WCFMethod]
        public ServiceResponseData GetExamItem()
        {
            var typeId = requestData.GetData<int>(0);
            var item = NewDao<IOPDDao>()
                 .GetExamItem(typeId);
            responseData.AddData(item);
            return responseData;
        }

        /// <summary>
        /// 获取检验样本信息
        /// </summary>
        /// <returns>检验样本信息</returns>
        [WCFMethod]
        public ServiceResponseData GetSample()
        {
            var workerId = requestData.GetData<int>(0);
            var sample = NewDao<IOPDDao>()
                 .GetSample(workerId);
            responseData.AddData(sample);
            return responseData;
        }

        /// <summary>
        ///  保存申请单信息
        /// </summary>
        /// <returns>申请单头id</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData SaveExam()
        {
            var head = requestData.GetData<EXA_MedicalApplyHead>(0);
            var workerId = requestData.GetData<int>(1);
            var itemData = requestData.GetData<DataTable>(2);
            var dt = requestData.GetData<DataTable>(3);
            SetWorkId(workerId);
            if (head.SystemType == 1)
            {
                // 判断病人是否已定义出院或已开出院医嘱
                IP_PatList ipPatlist = NewObject<IP_PatList>().getmodel(head.PatListID) as IP_PatList;
                head.ApplyDeptID = ipPatlist.CurrDeptID;
                if (ipPatlist.Status != 2 || ipPatlist.IsLeaveHosOrder == 1)
                {
                    responseData.AddData(-1);
                    responseData.AddData("保存失败，病人已经出院！");
                    return responseData;
                }
            }

            var result = NewObject<MedicalApply>().SaveMedicalApply(head, itemData, dt); 
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 获取申请表头信息
        /// </summary>
        /// <returns>申请状态</returns>
        [WCFMethod]
        public ServiceResponseData GetApplyStatus()
        {
            var headId = requestData.GetData<int>(0);
            var applyhead = NewDao<IOPDDao>().GetApplyStatus(headId);
            responseData.AddData(applyhead);
            return responseData;
        }

        /// <summary>
        /// 获取申请表头信息
        /// </summary>
        /// <returns>申请表头信息</returns>
        [WCFMethod]
        public ServiceResponseData GetApplyHead()
        {
            var workerId = requestData.GetData<int>(0);
            var systemType = requestData.GetData<int>(1);
            var patId = requestData.GetData<int>(2);
            var applyhead = NewDao<IOPDDao>().GetApplyHead(workerId, systemType, patId);
            responseData.AddData(applyhead);
            return responseData;
        }

        /// <summary>
        /// 移除申请表头信息
        /// </summary>
        /// <returns>1成功</returns>
        [WCFMethod]
        [AOP(typeof(AopTransaction))]
        public ServiceResponseData DelApplyHead()
        {
            var applyheadId = requestData.GetData<int>(0);
            var systemType = requestData.GetData<int>(1);
            var result = NewDao<IOPDDao>().DelApplyHead(applyheadId, systemType);
            responseData.AddData(result);
            return responseData;
        }

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <returns>申请数据</returns>
        [WCFMethod]
        public ServiceResponseData GetHeadDetail()
        {
            var applyheadId = requestData.GetData<int>(0);
            var headdetail = NewDao<IOPDDao>().GetHeadDetail(applyheadId);
            responseData.AddData(headdetail);
            return responseData;
        }

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <returns>病人信息</returns>
        [WCFMethod]
        public ServiceResponseData GetPatientList()
        {
            var patlistid = requestData.GetData<string>(0);
            var systemType = requestData.GetData<int>(1);
            DataTable patlist = new DataTable();
            if (systemType == 0)
            {
                patlist = NewDao<IOPDDao>().GetPatientInfo(patlistid, 2);
            }
            else
            {
                patlist = NewDao<IOPDDao>().GetIPPatientInfo(patlistid);
            }

            responseData.AddData(patlist);
            return responseData;
        }

        /// <summary>
        /// 获取组合项目明细
        /// </summary>
        /// <returns>返回组合项目明细</returns>
        [WCFMethod]
        public ServiceResponseData GetExamItemDetailDt()
        {
            int examItemID = requestData.GetData<int>(0);
            DataTable dtDetailItem = NewDao<IOPDDao>().GetExamItemDetail(examItemID);
            responseData.AddData(dtDetailItem);//药品项目选项卡数据          
            return responseData;
        }
    }
}
