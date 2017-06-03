using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 费用模板管理
    /// </summary>
    public class FeeTemplateMrg : AbstractObjectModel
    {
        /// <summary>
        /// 获取模板树信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="presType">处方类型</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>模板树信息</returns>
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int workID, int presType, int deptID, int empID)
        {
            return NewDao<IOPPresTemplate>().GetPresTemplate(intLevel, workID, presType, deptID, empID);
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="info">模板头信息</param>
        /// <param name="resFlag">1成功</param>
        /// <returns>模板信息</returns>
        public OPD_PresMouldHead SaveMouldInfo(OPD_PresMouldHead info, out int resFlag)
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
        public int DeleteMoudel(OPD_PresMouldHead info)
        {
            this.BindDb(info);
            return info.delete();
        }

        /// <summary>
        /// 检查模板名称是否重复
        /// </summary>
        /// <param name="name">模板名称</param>
        /// <param name="presType">处方类型</param>
        /// <param name="level">模板级别</param>
        /// <param name="pid">父id</param>
        /// <param name="id">模板id</param>
        /// <returns>false存在</returns>
        public bool CheckName(string name, int presType, int level, int pid, int id)
        {
            DataTable dt = NewDao<IOPPresTemplate>().CheckMoudelName(name, presType, level, pid);
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
                    if (id == Convert.ToInt32(dt.Rows[0]["PresMouldHeadID"]))
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
        /// <param name="mouldList">模板明细列表</param>
        /// <returns>1成功</returns>
        public int SaveDetail(List<OPD_PresMouldDetail> mouldList)
        {
            int result = 0;
            foreach (OPD_PresMouldDetail mould in mouldList)
            {
                BindDb(mould);
                result = mould.save();
            }

            return result;
        }
    }
}
