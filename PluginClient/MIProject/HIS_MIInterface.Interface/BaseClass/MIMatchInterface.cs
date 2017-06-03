using HIS_Entity.MIManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS_MIInterface.Interface.BaseClass
{
    /// <summary>
    /// 目录匹配接口处理
    /// </summary>
    public interface MIMatchInterface
    {
        /// <summary>
        /// 下载中心药品目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_DownLoadDrugContent(InputClass inputClass); //int ybId, int workId);
        /// <summary>
        /// 下载中心项目目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_DownLoadItemContent(InputClass inputClass); //int ybId, int workId);

        /// <summary>
        /// 下载中心材料目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_DownLoadMaterialsContent(InputClass inputClass); //int ybId, int workId);

        /// <summary>
        /// 判断单条目录是否已匹配
        /// </summary>
        /// <returns></returns>
        ResultClass M_JudgeSingleContentMatch(InputClass inputClass); //);
        /// <summary>
        /// 上传匹配的目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_UploadMatchContent(InputClass inputClass); //);
        /// <summary>
        /// 重置匹配目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_ResetMatchContent(InputClass inputClass); //);
        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        ResultClass M_DeleteMatchContent(InputClass inputClass); //);
        /// <summary>
        /// 下载医院审核项目信息
        /// </summary>
        /// <returns></returns>
        ResultClass M_DownLoadHospContent(InputClass inputClass); //);

        /// <summary>
        /// 上传科室
        /// </summary>
        ResultClass M_UpLoadDept(InputClass inputClass); //);

        /// <summary>
        /// 上传人员信息
        /// </summary>
        ResultClass M_UpLoadStaff(InputClass inputClass); //);
    }
}
