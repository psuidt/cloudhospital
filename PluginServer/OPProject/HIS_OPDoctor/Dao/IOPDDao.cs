using System;
using System.Collections.Generic;
using System.Data;
using HIS_Entity.ClinicManage;

namespace HIS_OPDoctor.Dao
{
    /// <summary>
    /// 门诊医生站数据库访问接口
    /// </summary>
    public interface IOPDDao
    {
        #region 门诊患者查询
        /// <summary>
        /// 取得医生所在科室信息
        /// </summary>
        /// <param name="empId">医生Id</param>
        /// <returns>医生所在科室数据</returns>
        DataTable GetDocRelateDeptInfo(int empId);

        /// <summary>
        /// 加载病人列表
        /// </summary>
        /// <param name="docId">医生Id</param>
        /// <param name="deptId">科室Id</param>
        /// <param name="regBeginDate">挂号开始日期</param>
        /// <param name="regEndDate">挂号结束日期</param>
        /// <param name="visitStatus">就诊状态</param>
        /// <param name="belong">病人所属</param>
        /// <returns>病人列表</returns>
        DataTable LoadPatientList(int docId, int deptId, string regBeginDate, string regEndDate, int visitStatus, int belong);

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patid">病人Id</param>
        /// <returns>病人信息</returns>
        DataTable LoadPatientInfo(int patid);

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <param name="type">0卡号1就诊号2病人Id</param>
        /// <returns>病人信息</returns>
        DataTable GetPatientInfo(string id, int type);
        #endregion

        #region 修改患者信息
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="memberID">会员Id</param>
        /// <returns>会员信息</returns>
        DataTable GetMemberInfo(int memberID);
        #endregion

        #region 就诊历史查询
        /// <summary>
        /// 根据会员号获取就诊记录
        /// </summary>
        /// <param name="memId">会员id</param>
        /// <param name="queryWhere">查询条件</param>
        /// <returns>就诊记录</returns>
        DataTable GetHisRecord(int memId, Dictionary<string, string> queryWhere);

        /// <summary>
        /// 获取科室下的医生
        /// </summary>
        /// <param name="deptId">科室ID</param>
        /// <returns>医生数据</returns>
        DataTable GetDoctor(int deptId);
        #endregion

        #region 医技申请单
        /// <summary>
        /// 获取医技申请单科室
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="examclass">检查类型</param>
        /// <returns>医技申请单科室</returns>
        DataTable GetExecDept(int workId, int examclass);

        /// <summary>
        /// 根据科室获取项目分类
        /// </summary>
        /// <param name="deptId">科室id</param>
        /// <param name="examclass">项目类型</param>
        /// <returns>项目分类</returns>
        DataTable GetExamType(int deptId, int examclass);

        /// <summary>
        /// 根据项目分类获取项目信息
        /// </summary>
        /// <param name="typeId">类型id</param>
        /// <returns>项目信息</returns>
        DataTable GetExamItem(int typeId);

        /// <summary>
        /// 获取检验样本信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>检验样本信息</returns>
        DataTable GetSample(int workId);

        /// <summary>
        /// 获取费用项目数据
        /// </summary>
        /// <param name="itemId">项目id</param>
        /// <returns>费用项目数据</returns>
        DataTable GetFeeItemData(int itemId);

        /// <summary>
        ///  根据申请表头ID获取收费状态
        /// </summary>
        /// <param name="headId">申请表头ID</param>
        /// <returns>收费状态</returns>
        DataTable GetApplyStatus(int headId);

        /// <summary>
        /// 获取申请表头信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <param name="systemType">系统类型</param>
        /// <param name="patId">病人id</param>
        /// <returns>申请表头信息</returns>
        DataTable GetApplyHead(int workId, int systemType, int patId);

        /// <summary>
        /// 移除申请表头信息
        /// </summary>
        /// <param name="applyheadId">申请头信息</param>
        /// <param name="systemType">系统类型</param>
        /// <returns>1成功</returns>
        int DelApplyHead(int applyheadId, int systemType);

        /// <summary>
        /// 获取表头和明细
        /// </summary>
        /// <param name="applyheadId">申请头id</param>
        /// <returns>申请信息</returns>
        DataTable GetHeadDetail(int applyheadId);

        /// <summary>
        /// 通过卡号就诊号查询病人信息
        /// </summary>
        /// <param name="id">查询信息</param>
        /// <returns>病人信息</returns>
        DataTable GetIPPatientInfo(string id);

        /// <summary>
        /// 获取大项目ID
        /// </summary>
        /// <param name="examId">查询项目id</param>
        /// <returns>大项目数据</returns>
        DataTable GetStatID(string examId);

        /// <summary>
        /// 获取床位病人信息
        /// </summary>
        /// <param name="id">病人id</param>
        /// <returns>床位病人信息</returns>
        DataTable GetInBedPatient(int id);

        /// <summary>
        /// 获取处方表最大处方数
        /// </summary>
        /// <returns>处方表最大处方数</returns>
        DataTable GetPresNO();

        /// <summary>
        /// 获取组合项目明细
        /// </summary>
        /// <param name="examItemId">组合项目ID</param>
        /// <returns>返回组合项目明细</returns>
        DataTable GetExamItemDetail(int examItemId);
        #endregion

        #region 下诊断
        /// <summary>
        /// 加载诊断记录
        /// </summary>
        /// <param name="patListID">病人Id</param>
        /// <returns>诊断记录</returns>
        DataTable LoadDiagnosisList(int patListID);

        /// <summary>
        /// 添加诊断记录
        /// </summary>
        /// <param name="model">诊断记录实体</param>
        /// <returns>true成功</returns>
        bool AddDiagnosis(OPD_DiagnosisRecord model);

        /// <summary>
        /// 删除诊断记录
        /// </summary>
        /// <param name="diagnosisId">诊断记录Id</param>
        /// <param name="patListID">病人Id</param>
        /// <returns>true成功</returns>
        bool DeleteDiagnosis(int diagnosisId, int patListID);

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="dtDiagnosis">诊断记录表</param>
        void SortDiagnosis(DataTable dtDiagnosis);
        #endregion

        #region 处方控件数据源
        /// <summary>
        /// 取得药品执行药房
        /// </summary>
        /// <param name="presType">0西药1中草药</param>
        /// <returns>药房数据</returns>
        DataTable GetDrugStoreRoom(int presType);

        /// <summary>
        /// 获取项目ShowCard数据
        /// itemclass全部=0,药品=1,物资=2,收费项目=3,组合项目=4,说明性医嘱=5
        /// </summary>
        /// <param name="itemclass">项目类型</param>
        /// <param name="statID">统计大项目Id</param>
        /// <param name="execDeptId">执行科室</param>
        /// <returns>项目ShowCard数据</returns>
        DataTable GetFeeItemShowCardData(int itemclass, int statID, int execDeptId);

        /// <summary>
        /// 取出全部数据，前端做缓存
        /// </summary>
        /// <returns>全部数据项目数据</returns>
        DataTable GetFeeItemShowCardDatas();

        /// <summary>
        /// 取得药品信息
        /// </summary>
        /// <param name="itemId">项目ID</param>
        /// <param name="deptId">科室id</param>
        /// <returns>药品信息</returns>
        DataTable GetDrugItem(int itemId,int deptId);

        /// <summary>
        /// 删除处方明细
        /// </summary>
        /// <param name="presDetailId">处方明细Id</param>
        /// <returns>true成功</returns>
        bool DeletePrescriptionData(int presDetailId);

        /// <summary>
        /// 删除处方
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="presNo">处方号</param>
        /// <returns>true成功</returns>
        bool DeletePrescriptionData(int patListId, int presType, int presNo);

        /// <summary>
        /// 获取处方
        /// </summary>
        /// <param name="patListId">病人id</param>
        /// <param name="presType">处方类型</param>
        /// <returns>处方数据</returns>
        DataTable GetPrescriptionData(int patListId, int presType);

        /// <summary>
        /// 是否结算
        /// </summary>
        /// <param name="ids">处方明细id字符串</param>
        /// <returns>1是结算</returns>
        bool IsCostPres(string ids);

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <param name="deptId">科室Id</param>
        /// <param name="itemId">项目Id</param>
        /// <param name="qty">数量</param>
        /// <returns>true有库存</returns>
        bool IsDrugStore(int deptId, int itemId, decimal qty);

        /// <summary>
        /// 判断库存
        /// </summary>
        /// <param name="ids">项目Id字符串</param>
        /// <returns>库存药品</returns>
        DataTable IsDrugStore(string ids);

        /// <summary>
        /// 更新自备
        /// </summary>
        /// <param name="presDetailId">处方明细Id</param>
        /// <param name="flag">标志</param>
        /// <returns>true成功</returns>
        bool UpdatePresSelfDrugFlag(int presDetailId, int flag);

        /// <summary>
        /// 更新医保报销标识
        /// </summary>
        /// <param name="list">处方明细列表</param>
        /// <param name="flag">0不报销1报销</param>
        /// <returns>true更新成功</returns>
        bool UpdatePresReimbursementFlag(List<OPD_PresDetail> list, int flag);

        /// <summary>
        /// 更新处方号和组号
        /// </summary>
        /// <param name="list">处方列表</param>
        /// <returns>true成功</returns>
        bool UpdatePresNoAndGroupId(List<OPD_PresDetail> list);

        /// <summary>
        /// 保存处方
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <param name="selectedMemberID">会员id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="list">处方列表</param>
        /// <returns>true成功</returns>
        bool SavePrescriptionData(int patListId, int selectedMemberID, int presType, List<OPD_PresDetail> list);

        /// <summary>
        /// 获取处方打印的信息
        /// </summary>
        /// <param name="preHeadId">处方明细头ID</param>
        /// <param name="preNo"> 处方号</param>
        /// <returns>处方数据</returns>
        DataTable GetPrintPresData(int preHeadId, int preNo);

        /// <summary>
        /// 根据用法ID获取用法联动费用
        /// </summary>
        /// <param name="channelId">用法id</param>
        /// <returns>用法数据</returns>
        DataTable GetChannelFees(int channelId);

        /// <summary>
        /// 取得费用处方头Id
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <returns>费用处方头Id</returns>
        int GetFeeHeadId(int patListId);

        /// <summary>
        /// 获取处方模板
        /// </summary>
        /// <param name="tplId">模板ID</param>
        /// <returns>处方模板数据</returns>
        DataTable GetPresTemplate(int tplId);

        /// <summary>
        /// 获取处方模板行
        /// </summary>
        /// <param name="tpldetailIds">处方明细ID数组</param>
        /// <returns>处方模板明细数据</returns>
        DataTable GetPresTemplateRow(int[] tpldetailIds);

        /// <summary>
        /// 根据处方明细ID获取处方明细信息
        /// </summary>
        /// <param name="detailId">处方明细id</param>
        /// <returns>处方明细信息</returns>
        OPD_PresDetail GetPresDetail(int detailId);
        #endregion

        #region 开住院证
        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="patListID">病人id</param>
        /// <returns>病人数据</returns>
        DataTable GetPatientData(int patListID);

        /// <summary>
        /// 获取床位信息
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>床位信息</returns>
        DataTable GetBedInfo(int workId);

        /// <summary>
        /// 获取病人住院证信息
        /// </summary>
        /// <param name="patListID">病人id</param>
        /// <returns>病人住院证信息</returns>
        OPD_InpatientReg GetInpatientReg(int patListID);
        #endregion

        #region 常用诊断
        /// <summary>
        /// 加载常用诊断信息
        /// </summary>
        /// <param name="doctorID">医生Id</param>
        /// <returns>常用诊断数据</returns>
        DataTable LoadCommonDianosis(int doctorID);

        /// <summary>
        /// 判断常用诊断里是否存在
        /// </summary>
        /// <param name="doctorID">医生Id</param>
        /// <param name="name">诊断名称</param>
        /// <returns>true存在</returns>
        bool ExistCommonDianosis(int doctorID, string name);

        /// <summary>
        /// 修改常用诊断频次
        /// </summary>
        /// <param name="presDoctorID">处方医生id</param>
        /// <param name="diagnosisName">诊断名称</param>
        /// <returns>true成功</returns>
        bool UpdateCommonDiagnosis(int presDoctorID, string diagnosisName);
        #endregion

        #region 拷贝历史
        /// <summary>
        /// 获取处方信息
        /// </summary>
        /// <param name="presType">处方类型</param>
        /// <param name="patListId">病人Id</param>
        /// <returns>处方信息</returns>
        DataTable GetPresInfo(int presType, int patListId);
        #endregion

        #region 费用模板
        /// <summary>
        /// 获取费用模板头信息
        /// </summary>
        /// <param name="headId">模板头id</param>
        /// <returns>费用模板头信息</returns>
        DataTable LoadMouldHead(int headId);

        /// <summary>
        /// 获取材料showcard
        /// </summary>
        /// <param name="workId">机构id</param>
        /// <returns>材料showcard数据</returns>
        DataTable LoadFeeInfoCard(int workId);

        /// <summary>
        /// 获取费用模板信息
        /// </summary>
        /// <param name="headId">费用模板头id</param>
        /// <returns>费用模板信息</returns>
        DataTable LoadMouldDetail(int headId);

        /// <summary>
        /// 删除费用模板信息
        /// </summary>
        /// <param name="detailId">明细id</param>
        /// <returns>1成功</returns>
        int DelDetail(int detailId);
        #endregion

        #region 门诊病历
        /// <summary>
        /// 符号类型
        /// </summary>
        /// <returns>符号类型数据</returns>
        DataTable GetSymbolType();

        /// <summary>
        /// 符号内容
        /// </summary>
        /// <returns>符号内容数据</returns>
        DataTable GetSymbolContent();
        #endregion

        #region 病历模板
        /// <summary>
        /// 获取病历模板
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>模板头列表</returns>
        List<OPD_OMRTmpHead> GetOMRTemplate(int intLevel, int workID, int deptID, int empID);

        /// <summary>
        /// 检验同级别下名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="level">级别</param>
        /// <param name="pid">父id</param>
        /// <returns>模板名称数据</returns>
        DataTable CheckMoudelName(string name, int level, int pid);

        /// <summary>
        /// 获取模板头
        /// </summary>
        /// <param name="headId">病历模板头id</param>
        /// <returns>模板头数据</returns>
        DataTable LoadOMRMouldHead(int headId);

        /// <summary>
        /// 取得门诊病历打印处方数据
        /// </summary>
        /// <param name="patListId">病人Id</param>
        /// <returns>诊病历打印处方数据</returns>
        DataTable GetOMRPrintPresData(int patListId);
        #endregion

        #region 医生个人工作量统计
        /// <summary>
        /// 门诊医生个人工作量统计
        /// </summary>
        /// <param name="doctorId">医生Id</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>门诊医生个人工作量统计数据</returns>
        DataTable GetOPDoctorWorkLoad(int doctorId, DateTime bdate, DateTime edate);

        /// <summary>
        /// 住院医生个人工作量统计
        /// </summary>
        /// <param name="doctorId">医生Id</param>
        /// <param name="bdate">开始日期</param>
        /// <param name="edate">结束日期</param>
        /// <returns>住院医生个人工作量统计数据</returns>
        DataTable GetIPDoctorWorkLoad(int doctorId, DateTime bdate, DateTime edate);       
        #endregion
    }
}
