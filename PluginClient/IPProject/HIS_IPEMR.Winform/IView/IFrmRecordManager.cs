using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFWCoreLib.WinformFrame.Controller;
using System.Data;

namespace HIS_IPEMR.Winform.IView
{
    interface IFrmRecordManager: IBaseView
    {
        /// <summary>
        /// 绑定模板记录数据
        /// </summary>
        /// <param name="dtRecord"></param>
        void BindTemplateRecord(DataTable dtRecord);

        /// <summary>
        /// 绑定病历树
        /// </summary>
        /// <param name="dtMedicalTree"></param>
        void BindMedicalTree(DataTable dtMedicalTree);

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dtDept">科室列表</param>
        void BindDept(DataTable dtDept);

        /// <summary>
        /// 模板权限
        /// </summary>
        int RoleType { get; set; }

        /// <summary>
        /// 选择的科室ID
        /// </summary>
        int DeptId { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        int TempTypeID { get; set; }

        /// <summary>
        /// 病历树父节点
        /// </summary>
        int MedicalTreeId { get; set; }

        /// <summary>
        /// 病历分类ID-子节点
        /// </summary>
        int tempTypeId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        string TemplateName { get; set; }
    }
}
