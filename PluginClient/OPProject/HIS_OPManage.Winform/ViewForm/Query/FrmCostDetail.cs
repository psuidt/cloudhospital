using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmCostDetail : BaseFormBusiness,IFrmCostDetail
    {
        /// <summary>
        /// 结算明细显示
        /// </summary>
        /// <param name="dtData">结算明细数据</param>
        public void BindData(DataTable dtData)
        {
            dgCostDetail.DataSource = dtData;
            if (dtData.Rows.Count > 0)
            {
                this.Text = dtData.Rows[0]["patName"].ToString().Trim() + Convert.ToDateTime(dtData.Rows[0]["ChargeDate"]).ToString("yyyy-MM-dd HH:mm:ss") + "费用明细清单";
            }         
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCostDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        public void SetGridColor()
        {
            foreach (DataGridViewRow row in dgCostDetail.Rows)
            {
                string status_flag = row.Cells["ItemName"].Value.ToString();
                Color foreColor = Color.Black;
                if (status_flag == "小 计")
                {
                    foreColor = Color.Blue;
                }

                dgCostDetail.SetRowColor(row.Index, foreColor, true);
            }
        }

        /// <summary>
        /// 窗体Shown事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmCostDetail_Shown(object sender, EventArgs e)
        {
            SetGridColor();
        }
    }
}
