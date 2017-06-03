using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EfwControls.Common;
using EFWCoreLib.CoreFrame.Business;
using HIS_ThatFee.Winform.IView;

namespace HIS_ThatFee.Winform.ViewForm
{
    public partial class FrmThatFee : BaseFormBusiness, IFrmThatFee
    {
        #region 属性
        /// <summary>
        /// 0门诊 1住院
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 开方科室
        /// </summary>
        public string ClincDeptId { get; set; }

        /// <summary>
        /// 是否选中检查检验
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// 是否选中化验检验
        /// </summary>
        public bool IsTest { get; set; }

        /// <summary>
        /// 是否选中治疗检验
        /// </summary>
        public bool IsTreat { get; set; }

        /// <summary>
        /// 是否选中未确费
        /// </summary>
        public bool IsNotThatFee { get; set; }

        /// <summary>
        /// 是否选中确费
        /// </summary>
        public bool IsThatFee { get; set; }

        /// <summary>
        /// 申请单号
        /// </summary>
        public string StrNO { get; set; }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmThatFee()
        {
            InitializeComponent();
            bindGridSelectIndex(dgThatFee);
        }

        #region 接口
        /// <summary>
        /// 绑定确费网格信息
        /// </summary>
        /// <param name="dtFee">申请明细数据源</param>
        public void BindThatFee(DataTable dtFee)
        {
            dgFee.DataSource = null;
            cbOrder.Checked = false;
            for (int i = 0; i < dtFee.Rows.Count; i++)
            {
                if (Convert.ToDateTime(dtFee.Rows[i]["ConfirDate"]).ToString("yyyy-MM-dd") == "1900-01-01")
                {
                    dtFee.Rows[i]["ConfirDate"] = DBNull.Value;
                }
            }

            dgThatFee.DataSource = dtFee;
            for (int i = 0; i < dtFee.Rows.Count; i++)
            {
                if (dtFee.Rows[i]["ApplyStatus"].ToString() == "2")
                {
                    dgThatFee.SetRowColor(i, Color.Blue, true);
                }
            }
        }

        /// <summary>
        /// 绑定执行科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        public void BindDept(DataTable dtDept)
        {
            DataRow dr = dtDept.NewRow();
            dr["DeptId"] = 0;
            dr["Name"] = "所有科室";
            dtDept.Rows.InsertAt(dr, 0);
            cmbDept.DataSource = dtDept;
            if (dtDept != null)
            {
                if (dtDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定费用网格信息
        /// </summary>
        /// <param name="dtFee">费用明细数据源</param>
        public void BindFee(DataTable dtFee)
        {
            dgFee.DataSource = dtFee;
        }

        /// <summary>
        /// 绑定开方科室
        /// </summary>
        /// <param name="dtDept">科室数据源</param>
        public void BindClincDept(DataTable dtDept)
        {
            DataRow dr = dtDept.NewRow();
            dr["DeptId"] = 0;
            dr["Name"] = "所有科室";
            dtDept.Rows.InsertAt(dr, 0);
            cmbClincDept.DataSource = dtDept;
            if (dtDept != null)
            {
                if (dtDept.Rows.Count > 0)
                {
                    cmbClincDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 操作完成后提示
        /// </summary>
        /// <param name="message">消息内容</param>
        public void CompleteMessage(string message)
        {
            MessageBoxEx.Show(message);
        }

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        public void GetQueryWhere()
        {
            SystemType = rbOP.Checked ? 0 : 1;
            DeptId = cmbDept.SelectedIndex > 0 ? cmbDept.SelectedValue.ToString() : string.Empty;
            ClincDeptId = cmbClincDept.SelectedIndex > 0 ? cmbClincDept.SelectedValue.ToString() : string.Empty;
            IsCheck = ckCheck.Checked;
            IsTest = ckTest.Checked;
            IsTreat = ckTreat.Checked;
            IsNotThatFee = ckNotThatFee.Checked;
            IsThatFee = ckThatFee.Checked;
            StrNO = !string.IsNullOrEmpty(txtNO.Text) ? txtNO.Text.Trim() : string.Empty;
        }
        #endregion

        #region 函数
        /// <summary>
        /// 获取当前选中记录ID
        /// </summary>
        /// <returns>处方id</returns>
        private string GetPresId()
        {
            string ids = string.Empty;
            DataTable dt = dgThatFee.DataSource as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["ck"]) == 1)
                {
                    ids += dt.Rows[i]["PresDetailID"] + ",";
                }
            }

            if (ids.Contains(","))
            {
                ids = ids.Substring(0, ids.Length - 1);
            }

            return ids;
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 查询明细单元格改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgThatFee_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgThatFee.CurrentRow==null)
            {
                return;
            }

            var rowIndex = dgThatFee.CurrentRow.Index;
            var dataSource = dgThatFee.DataSource as DataTable;
            int presId = Tools.ToInt32(dataSource.Rows[rowIndex]["PresDetailID"]);
            if (rbOP.Checked)
            {
                InvokeController("GetOPFee", presId);
            }
            else
            {
                InvokeController("GetIPFee", presId);
            }
        }

        /// <summary>
        /// 打开窗体前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmThatFee_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDept", frmName);
            InvokeController("GetClincDept", rbOP.Checked ? 0 : 1);
            InvokeController("GetThatFee");
        }

        /// <summary>
        /// 查询点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InvokeController("GetThatFee");
        }

        /// <summary>
        /// 关闭点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgThatFee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = dgThatFee.DataSource as DataTable;
                if (Convert.ToInt32(dt.Rows[e.RowIndex]["ck"]) == 0)
                {
                    dt.Rows[e.RowIndex]["ck"] = 1;
                    DataRow[] dr = dt.Select("ck=0");
                    if (dr.Length == 0)
                    {
                        cbOrder.CheckedChanged -= cbOrder_CheckedChanged;
                        cbOrder.Checked = true;
                        cbOrder.CheckedChanged += cbOrder_CheckedChanged;
                    }
                }
                else
                {
                    dt.Rows[e.RowIndex]["ck"] = 0;
                    cbOrder.CheckedChanged -= cbOrder_CheckedChanged;
                    cbOrder.Checked = false;
                    cbOrder.CheckedChanged += cbOrder_CheckedChanged;
                }
            }
        }

        /// <summary>
        /// 确费点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgThatFee.Rows.Count > 0)
                {
                    if (MessageBoxEx.Show("是否确定确费？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string ids = GetPresId();
                        if (!string.IsNullOrEmpty(ids))
                        {
                            InvokeController("ThatFee", ids);
                            InvokeController("GetThatFee");
                            setGridSelectIndex(dgThatFee);
                        }
                        else
                        {
                            MessageBoxEx.Show("请选择需要确费的记录");
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可确费记录");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 取消确费点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgThatFee.Rows.Count > 0)
                {
                    if (MessageBoxEx.Show("是否确定取消确费？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string ids = GetPresId();
                        if (!string.IsNullOrEmpty(ids))
                        {
                            InvokeController("CancelThatFee", ids);
                            InvokeController("GetThatFee");
                            setGridSelectIndex(dgThatFee);
                        }
                        else
                        {
                            MessageBoxEx.Show("请选择可取消确费的记录");
                        }
                    }
                }
                else
                {
                    MessageBoxEx.Show("没有可取消确费记录");
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 键盘按下事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void txtNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        /// <summary>
        /// 单选按钮选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbOrder_CheckedChanged(object sender, EventArgs e)
        {
            DataTable doctorList = dgThatFee.DataSource as DataTable;
            if (doctorList != null && doctorList.Rows.Count > 0)
            {
                for (int i = 0; i < doctorList.Rows.Count; i++)
                {
                    doctorList.Rows[i]["ck"] = cbOrder.Checked ? 1 : 0;
                }
            }
        }

        /// <summary>
        /// CheckBox选中改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void rbIP_CheckedChanged(object sender, EventArgs e)
        {
            InvokeController("GetThatFee");
        }
        #endregion
    }
}
