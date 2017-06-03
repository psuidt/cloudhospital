using System;
using System.Data;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmInvoiceDetail : BaseFormBusiness,IFrmInvoiceDetail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmInvoiceDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 票据详细信息
        /// </summary>
        DataTable dtInvoice;

        /// <summary>
        /// 绑定票据详细信息
        /// </summary>
        /// <param name="dtInvoice">票据信息</param>
        public void BindInvoiceDt(DataTable dtInvoice)
        {          
            this.dtInvoice = dtInvoice;
        }

        /// <summary>
        /// 界面Load事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmInvoiceDetail_Load(object sender, EventArgs e)
        {
            for (int i = 6; i < dgInvoiceDetail.Columns.Count; i++)
            {
                dgInvoiceDetail.Columns.RemoveAt(i);
            }

            for (int i = 7; i < dtInvoice.Columns.Count; i++)
            {
                if (!dgInvoiceDetail.Columns.Contains(dtInvoice.Columns[i].ColumnName))
                {
                    DataGridViewColumn col = new DataGridViewColumn();
                    col.HeaderText = dtInvoice.Columns[i].ColumnName;
                    col.DataPropertyName = dtInvoice.Columns[i].ColumnName;
                    col.Name = dtInvoice.Columns[i].ColumnName;
                    DataGridViewCell dgvcell = new DataGridViewTextBoxCell();
                    col.CellTemplate = dgvcell;
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgInvoiceDetail.Columns.Add(col);
                }
            }

            dgInvoiceDetail.DataSource = dtInvoice;         
        }
    }
}
