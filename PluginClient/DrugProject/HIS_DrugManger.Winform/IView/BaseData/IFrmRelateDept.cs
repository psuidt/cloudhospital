using System.Collections.Generic;
using System.Data;
using EFWCoreLib.WinformFrame.Controller;
using HIS_Entity.BasicData;

namespace HIS_DrugManage.Winform.IView.BaseData
{
    /// <summary>
    /// 往来科室
    /// </summary>
    interface IFrmRelateDept: IBaseView
    {
        /// <summary>
        /// 绑定科室选择卡片
        /// </summary>
        /// <param name="dt">科室数据集</param>
        void BindDept(DataTable dt);

        /// <summary>
        /// 加载树形控件数据
        /// </summary>
        /// <param name="layerList">父节点集</param>
        /// <param name="deptList">科室列表</param>
        /// <param name="deptId">当前登录科室Id</param>
        void LoadDeptTree(List<BaseDeptLayer> layerList, List<BaseDept> deptList,int deptId);

        /// <summary>
        /// 绑定库房选择卡片
        /// </summary>
        /// <param name="dt">库房数据集</param>
        void BindStoreRoom(DataTable dt);

        /// <summary>
        /// 绑定往来科室网格数据
        /// </summary>
        /// <param name="dt">往来科室数据集</param>
        void BindRelateDeptGrid(DataTable dt);

        /// <summary>
        /// 绑定科室类型选择卡片
        /// </summary>
        void BindDeptType();
    }
}
