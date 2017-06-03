using EFWCoreLib.CoreFrame.Business;
using HIS_IPEMR.Winform.IView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS_IPEMR.Winform.ViewForm
{
    public partial class FrmMedicalCase : BaseFormBusiness, IFrmMedicalCase
    {
        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public FrmMedicalCase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取病案首页内容
        /// </summary>
        /// <param name="patlistid">病人ID</param>
        /// <param name="deptid">科室ID</param>
        /// <param name="deptname">科室名称</param>
        /// <param name="empid">人员ID</param>
        /// <param name="empname">人员名称</param>
        public void GetMedicalCase(int patlistid, int deptid, string deptname, int empid, string empname)
        {
            dcControl.GetMedicalCase(patlistid, deptid, deptname, empid, empname);
        }

        /// <summary>
        /// 保存按钮事件病案首页
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dcControl.SaveCaseFile())
            {
                InvokeController("ShowMessage", "保存成功");
            }
            else
            {
                InvokeController("ShowMessage", "保存失败");
            }
        }

        /// <summary>
        /// 导出病案首页按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            dcControl.ExportMedicalCase();
        }

        /// <summary>
        /// 打印病案首页按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            dcControl.PrintMedicalCase();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">控件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            InvokeController("ShowEmrModel");
        }
    }
}
