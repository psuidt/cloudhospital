using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 诊断弹出窗口
    /// </summary>
    public partial class FrmDiagnosis : BaseFormBusiness, IFrmDiagnosis
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDiagnosis()
        {
            InitializeComponent();
        }

        #region 接口
        /// <summary>
        /// 绑定诊断showcard
        /// </summary>
        /// <param name="dt">诊断数据</param>
        public void BindDiseaseShowCard(DataTable dt)
        {
            txtDigShowCard.DisplayField = "Name";
            txtDigShowCard.MemberField = "ICDCode";
            txtDigShowCard.CardColumn = "Name|诊断名称|175,ICDCode|ICD编码|80";
            txtDigShowCard.QueryFieldsString = "Name,PYCode,WBCode,ICDCode";
            txtDigShowCard.ShowCardWidth = 260;
            txtDigShowCard.ShowCardHeight = 100;
            txtDigShowCard.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定诊断网格
        /// </summary>
        /// <param name="dtDisease">诊断表</param>
        public void BindDiseaseDataGrid(DataTable dtDisease)
        {
            dgDisease.DataSource = dtDisease;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmDiagnosis_OpenWindowBefore(object sender, EventArgs e)
        {
            txtFeeDisease.Focus();
            //绑定诊断ShowCard
            InvokeController("GetDisease", frmName);
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDisease_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex <= -1 || e.RowIndex <= -1)
            {
                return;
            }

            string buttonText = this.dgDisease.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue.ToString();
            if (buttonText == "移除")
            {
                string diagnosisRecordID = this.dgDisease.Rows[e.RowIndex].Cells["DiagnosisRecordID"].Value.ToString();
                if (MessageBox.Show("您确认移除该诊断吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        InvokeController("DeleteDiagnosis", Convert.ToInt32(diagnosisRecordID));
                    }
                    catch (Exception error)
                    {
                        MessageBoxEx.Show(error.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 单元格式事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgDisease_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgDisease.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex > -1)
            {
                e.Value = "移除";
            }
        }

        /// <summary>
        /// 上移事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgDisease.Rows.Count <= 0)
            {
                return;
            }

            int rowIndex = dgDisease.SelectedRows[0].Index;  //得到当前选中行的索引

            if (rowIndex == 0)
            {
                return;
            }

            List<string> list = new List<string>();
            for (int i = 0; i < dgDisease.Columns.Count; i++)
            {
                if (dgDisease.SelectedRows[0].Cells[i].Value == null)
                {
                    list.Add(string.Empty);
                }
                else
                {
                    list.Add(dgDisease.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中
                }
            }

            for (int j = 0; j < dgDisease.Columns.Count; j++)
            {
                dgDisease.Rows[rowIndex].Cells[j].Value = dgDisease.Rows[rowIndex - 1].Cells[j].Value;
                dgDisease.Rows[rowIndex - 1].Cells[j].Value = list[j].ToString();
            }

            dgDisease.Rows[rowIndex - 1].Selected = true;
            dgDisease.Rows[rowIndex].Selected = false;
            dgDisease.CurrentCell = dgDisease[0, rowIndex - 1];
            DataTable dt = (DataTable)dgDisease.DataSource;
            InvokeController("SortDiagnosis", dt);
        }

        /// <summary>
        /// 下移事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgDisease.Rows.Count <= 0)
            {
                return;
            }

            int rowIndex = dgDisease.SelectedRows[0].Index;  //得到当前选中行的索引

            if (rowIndex == dgDisease.Rows.Count - 1)
            {
                return;
            }

            List<string> list = new List<string>();
            for (int i = 0; i < dgDisease.Columns.Count; i++)
            {
                if (dgDisease.SelectedRows[0].Cells[i].Value == null)
                {
                    list.Add(string.Empty);
                }
                else
                {
                    list.Add(dgDisease.SelectedRows[0].Cells[i].Value.ToString());   //把当前选中行的数据存入list数组中
                }
            }

            for (int j = 0; j < dgDisease.Columns.Count; j++)
            {
                dgDisease.Rows[rowIndex].Cells[j].Value = dgDisease.Rows[rowIndex + 1].Cells[j].Value;
                dgDisease.Rows[rowIndex + 1].Cells[j].Value = list[j].ToString();
            }

            dgDisease.Rows[rowIndex + 1].Selected = true;
            dgDisease.Rows[rowIndex].Selected = false;
            dgDisease.CurrentCell = dgDisease[0, rowIndex + 1];
            DataTable dt = (DataTable)dgDisease.DataSource;
            InvokeController("SortDiagnosis", dt);
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDigShowCard.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("请输入诊断信息");
                txtDigShowCard.Focus();
                return;
            }

            string name = txtDigShowCard.Text.Trim();
            DataTable dtDisease = (DataTable)dgDisease.DataSource;
            if (dtDisease.Rows.Count > 0)
            {
                foreach (DataRow row in dtDisease.Rows)
                {
                    if (row["DiagnosisName"].ToString() == name)
                    {
                        txtDigShowCard.Clear();
                        txtDigShowCard.Focus();
                        MessageBoxShowSimple("您已经添加【" + name + "】诊断");
                      
                        return;
                    }
                }
            }

            if (txtDigShowCard.MemberValue == null)
            {
                return;
            }

            string code = txtDigShowCard.MemberValue.ToString();
            InvokeController("AddDiagnosis", code, name);
            txtDigShowCard.Text = string.Empty;
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnFeeAdd_Click(object sender, EventArgs e)
        {
            if (txtFeeDisease.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("请输入诊断信息");
                txtFeeDisease.Focus();
                return;
            }

            string name = txtFeeDisease.Text.Trim();
            DataTable dtDisease = (DataTable)dgDisease.DataSource;
            if (dtDisease.Rows.Count > 0)
            {
                foreach (DataRow row in dtDisease.Rows)
                {
                    if (row["DiagnosisName"].ToString() == name)
                    {
                        MessageBoxShowSimple("您已经添加【" + name + "】诊断");
                        txtFeeDisease.Focus();
                        return;
                    }
                }
            }

            string code = string.Empty;
            InvokeController("AddDiagnosis", code, name);
            txtFeeDisease.Text = string.Empty;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmDiagnosis_Load(object sender, EventArgs e)
        {
            txtFeeDisease.Focus();
            //绑定诊断ShowCard
            InvokeController("GetDisease", frmName);
        }

        /// <summary>
        /// Visible事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmDiagnosis_VisibleChanged(object sender, EventArgs e)
        {
            //绑定诊断列表
            InvokeController("LoadDiagnosisList");
        }

        /// <summary>
        /// keyDown事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtDigShowCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (txtDigShowCard.MemberValue == null)
                {
                    return;
                }
                else
                {
                    // btnAdd_Click(null, null);
                    btnAdd.Focus();
                }
            }
        }

        /// <summary>
        /// KeyDown事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtFeeDisease_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnFeeAdd_Click(null, null);
            }
        }
        #endregion
    }
}
