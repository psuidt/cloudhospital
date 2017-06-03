using System;
using System.Data;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_IPNurse.Winform.IView;

namespace HIS_IPNurse.Winform.ViewForm
{
    /// <summary>
    /// 瓶签打印确认
    /// </summary>
    public partial class FrmPrintExamConfirm : BaseFormBusiness, IPrintExamConfirm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPrintExamConfirm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 打印数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            grdPrintDt.EndEdit();
            if (grdPrintDt.DataSource != null)
            {
                DataTable dt = grdPrintDt.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Tools.ToInt32(dt.Rows[i]["Num"]) < 0)
                    {
                        grdPrintDt.CurrentCell = grdPrintDt[Num.Index, i];
                        grdPrintDt.BeginEdit(true);
                        InvokeController("MessageShow", "请输入正确的标签打印个数！");
                        return;
                    }
                }

                InvokeController("PrintReport", dt, true);
            }
        }

        /// <summary>
        /// 取消打印
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 将所有标签打印个数设置成当前选中行的个数
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSet_Click(object sender, EventArgs e)
        {
            grdPrintDt.EndEdit();
            if (grdPrintDt.CurrentCell != null)
            {
                int rowIndex = grdPrintDt.CurrentCell.RowIndex;
                DataTable dt = grdPrintDt.DataSource as DataTable;
                int num = Tools.ToInt32(dt.Rows[rowIndex]["Num"]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["Num"] = num;
                }
            }
        }

        /// <summary>
        /// 绑定报表打印数据
        /// </summary>
        /// <param name="printDt">待打印数据</param>
        public void Bind_PrintDt(DataTable printDt)
        {
            grdPrintDt.DataSource = printDt;
            grdPrintDt.ReadOnly = false;
            grdPrintDt.Columns[0].ReadOnly = true;
            grdPrintDt.Columns[1].ReadOnly = true;
        }

        /// <summary>
        /// 关闭当前界面
        /// </summary>
        public void CloseForm()
        {
            grdPrintDt.EndEdit();
            this.Close();
        }

        /// <summary>
        /// 关闭窗体是将网格退出编辑模式
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmPrintExamConfirm_VisibleChanged(object sender, EventArgs e)
        {
            grdPrintDt.EndEdit();
        }

        /// <summary>
        /// 预览报表
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            grdPrintDt.EndEdit();
            if (grdPrintDt.DataSource != null)
            {
                DataTable dt = grdPrintDt.DataSource as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Tools.ToInt32(dt.Rows[i]["Num"]) < 0)
                    {
                        grdPrintDt.CurrentCell = grdPrintDt[Num.Index, i];
                        grdPrintDt.BeginEdit(true);
                        InvokeController("MessageShow", "请输入正确的标签打印个数！");
                        return;
                    }
                }

                InvokeController("PrintReport", dt, false);
            }
        }
    }
}
