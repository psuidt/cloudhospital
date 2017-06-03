using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MI_MIInterface.ObjectModel.BaseClass
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
        bool M_DownLoadDrugContent(int ybId, int workId);
        /// <summary>
        /// 下载中心项目目录
        /// </summary>
        /// <returns></returns>
        bool M_DownLoadItemContent(int ybId, int workId);

        /// <summary>
        /// 下载中心材料目录
        /// </summary>
        /// <returns></returns>
        bool M_DownLoadMaterialsContent(int ybId, int workId);

        /// <summary>
        /// 判断单条目录是否已匹配
        /// </summary>
        /// <returns></returns>
        bool M_JudgeSingleContentMatch();
        /// <summary>
        /// 上传匹配的目录
        /// </summary>
        /// <returns></returns>
        bool M_UploadMatchContent();
        /// <summary>
        /// 重置匹配目录
        /// </summary>
        /// <returns></returns>
        bool M_ResetMatchContent();
        /// <summary>
        /// 删除匹配目录
        /// </summary>
        /// <returns></returns>
        bool M_DeleteMatchContent();
        /// <summary>
        /// 下载医院审核项目信息
        /// </summary>
        /// <returns></returns>
        bool M_DownLoadHospContent();

        /// <summary>
        /// 上传科室
        /// </summary>
        bool M_UpLoadDept();

        /// <summary>
        /// 上传人员信息
        /// </summary>
        bool M_UpLoadStaff();


    }
}
