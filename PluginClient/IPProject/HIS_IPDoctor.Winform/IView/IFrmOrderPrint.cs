using System.Collections.Generic;
using System.Data;

namespace HIS_IPDoctor.Winform.IView
{
    /// <summary>
    /// 医嘱打印界面接口
    /// </summary>
    interface IFrmOrderPrint
    {
        /// <summary>
        /// 绑定病人列表
        /// </summary>
        /// <param name="dtPatInfo">病人列表信息</param>
        void BindPatInfo(DataTable dtPatInfo);

        /// <summary>
        /// 科室ID
        /// </summary>
         int DeptId { get; set; }

        /// <summary>
        /// 当前选择病人ID
        /// </summary>
        int CurPatListId { get; set; }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室数据</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 绑定医嘱明细数据
        /// </summary>
        /// <param name="dtLongOrder">长期医嘱数据</param>
        /// <param name="dtTempOrder">临时医嘱数据</param>
        void BindOrderDetail(DataTable dtLongOrder, DataTable dtTempOrder);

        /// <summary>
        /// 医嘱批量打印
        /// </summary>
        /// <param name="myDictionary">打印参数</param>
        /// <param name="dtLongOrder">长期医嘱数据</param>
        /// <param name="dtTempOrder">临时医嘱数据</param>
        void PrintAll(Dictionary<string, object> myDictionary, DataTable dtLongOrder, DataTable dtTempOrder);
    }
}
