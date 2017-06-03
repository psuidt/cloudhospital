using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 门诊病历模板业务处理
    /// </summary>
    public class OMRTplManager : AbstractObjectModel
    {
        /// <summary>
        /// 获取模板树信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>模板头信息</returns>
        public List<OPD_OMRTmpHead> GetOMRTemplate(int intLevel, int workID, int deptID, int empID)
        {
            return NewDao<IOPDDao>().GetOMRTemplate(intLevel, workID, deptID, empID);
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="info">模板信息</param>
        /// <param name="resFlag">保存成功失败标志</param>
        /// <returns>模板实体</returns>
        public OPD_OMRTmpHead SaveMouldInfo(OPD_OMRTmpHead info, out int resFlag)
        {
            this.BindDb(info);
            resFlag = info.save();
            return info;
        }

        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="info">模板头信息</param>
        /// <returns>1成功</returns>
        public int DeleteMoudel(OPD_OMRTmpHead info)
        {
            this.BindDb(info);
            return info.delete();
        }

        /// <summary>
        /// 检查模板名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父节点id</param>
        /// <param name="id">模板id</param>
        /// <returns>模板名称数据</returns>
        public bool CheckName(string name, int level, int pid, int id)
        {
            DataTable dt = NewDao<IOPDDao>().CheckMoudelName(name,level, pid);
            if (id == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    if (id == Convert.ToInt32(dt.Rows[0]["OMRTmpHeadID"]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 保存模板详细信息
        /// </summary>
        /// <param name="mouldList">模板明细</param>
        /// <returns>1成功</returns>
        public int SaveDetail(OPD_OMRTmpDetail mouldList)
        {
            return 1;
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="headModel">模板头</param>
        /// <param name="detailModel">模板明细</param>
        /// <returns>true成功</returns>
        public  bool AsSaveTmp(OPD_OMRTmpHead headModel, OPD_OMRTmpDetail detailModel)
        {
            headModel.CreateDate = DateTime.Now;

            //覆盖原来的
            if (headModel.OMRTmpHeadID != 0)
            {
                List<OPD_OMRTmpDetail> detailList = NewObject<OPD_OMRTmpDetail>().getlist<OPD_OMRTmpDetail>("OMRTmpHeadID="+ headModel.OMRTmpHeadID);
                if (detailList.Count > 0)
                {
                    detailModel.OMRTmpDetailID = detailList[0].OMRTmpDetailID;
                }
            }

            this.BindDb(headModel);
            int iHeadRtn = headModel.save();
            detailModel.OMRTmpHeadID = headModel.OMRTmpHeadID;
            this.BindDb(detailModel);
            int iDetailRtn = detailModel.save();
            if (iHeadRtn > 0 && iDetailRtn > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 取得模板内容
        /// </summary>
        /// <param name="headId">模板头Id</param>
        /// <returns>模板内容</returns>
        public OPD_OMRTmpDetail GetOMRTemplateDetail(int headId)
        {
            List<OPD_OMRTmpDetail> detailList = NewObject<OPD_OMRTmpDetail>().getlist<OPD_OMRTmpDetail>("OMRTmpHeadID=" + headId);
            if (detailList.Count > 0)
            {
                return detailList[0];
            }

            return null;
        }
    }
}
