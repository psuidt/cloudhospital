using System.Collections.Generic;
using HIS_Entity.OPManage;

namespace HIS_OPManage.Winform.IView
{
    /// <summary>
    /// 挂号病人查询界面接口类
    /// </summary>
    interface IFrmOutPatient
    {
        /// <summary>
        /// 界面绑定病人列表
        /// </summary>
        /// <param name="oplist">病人列表</param>
        void SetPatLists(List<OP_PatList> oplist);

        /// <summary>
        /// 获取当前选中的病人对象
        /// </summary>
        OP_PatList GetcurPatlist { get; }
    }
}
