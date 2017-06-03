using System;
using System.Collections.Generic;
using System.Data;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Dao;

namespace HIS_OPDoctor.ObjectModel
{
    /// <summary>
    /// 处方模板管理
    /// </summary>
    public class PresTemplateMrg : AbstractObjectModel
    {
        /// <summary>
        /// 获取模板树信息
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="workID">机构id</param>
        /// <param name="presType">处方类型id</param>
        /// <param name="deptID">科室id</param>
        /// <param name="empID">人员id</param>
        /// <returns>模板头列表</returns>
        public List<OPD_PresMouldHead> GetPresTemplate(int intLevel, int workID, int presType, int deptID, int empID)
        {
            return NewDao<IOPPresTemplate>().GetPresTemplate(intLevel, workID, presType, deptID, empID);
        }

        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="info">模板实体</param>
        /// <param name="resFlag">是否成功，>0成功</param>
        /// <returns>模板头信息</returns>
        public OPD_PresMouldHead SaveMouldInfo(OPD_PresMouldHead info,out int resFlag)
        {
            this.BindDb(info);
            resFlag=info.save();
            return info;
        }

        /// <summary>
        /// 删除模板信息
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
        /// <param name="id">id</param>
        /// <returns>false存在</returns>
        public bool CheckName(string name, int presType, int level, int pid,int id)
        {
            DataTable dt = NewDao<IOPPresTemplate>().CheckMoudelName(name,presType,level,pid);
            //新增
            if (id==0) 
            {
                if (dt.Rows.Count>0)
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
                if (dt.Rows.Count>0)
                {
                    if (id==Convert.ToInt32(dt.Rows[0]["PresMouldHeadID"]))
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
        /// 保存模板明细数据
        /// </summary>
        /// <param name="list">模板明细内容</param>
        /// <returns>1成功</returns>
        public int SavePresTemplateData(List<OPD_PresMouldDetail> list)
        {
            int res = 0;
            foreach (OPD_PresMouldDetail detaiModel in list)
            {                 
                this.BindDb(detaiModel);
                res=detaiModel.save();
                if (res<=0)
                {
                    break;
                }
            }

            return res;
        }
    }
}
