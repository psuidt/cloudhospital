using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Dao
{
    public interface IDataMatchInterface
    {

        //获取医保类型
        DataTable M_GetMIType();

        //获取医院目录
        /// <summary>
        /// 获取医院数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别</param>
        /// <returns></returns>
        DataTable M_GetHISDataInfo(int ybId, int dataType);

        //获取医院的医保目录
        /// <summary>
        /// 获取医保数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        DataTable M_GetMIDataInfo(int ybId, int dataType);
        /// <summary>
        /// 获取匹配数据
        /// </summary>
        /// <param name="dataType">字典类型1.剂型2.频次3.参保人员类别4收费等级5就诊科别</param>
        /// <param name="ybId">医保类型</param>
        /// <returns></returns>
        DataTable M_GetMatchDataInfo(int ybId, int dataType);
        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        bool M_DeleteMatchData(int id);

        /// <summary>
        /// 保存基础数据匹配
        /// </summary>
        /// <param name="iMIDataID"></param>
        /// <param name="iDataType"></param>
        /// <param name="iHospDataID"></param>
        /// <param name="iMIID"></param>
        /// <returns></returns>
        bool M_SaveMatchData(int iMIDataID, int iDataType, int iHospDataID, int iMIID);
    }
}
