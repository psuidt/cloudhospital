using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.FinanceMgr;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmAdjPrice : BaseFormBusiness, IFrmAdjPrice
    {
        /// <summary>
        /// 行对象
        /// </summary>
        public DataRow _currentRow { get; set; }

        /// <summary>
        /// 科室ID
        /// </summary>
        public string DeprtID { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmAdjPrice()
        {
            InitializeComponent();
            frmAdj.AddItem(dtpBillDate, "dtpBillDate");
            frmAdj.AddItem(cmbDept, "cmbDept");
            frmAdj.AddItem(chkBillNO, "chkBillNO");
            frmAdj.AddItem(txtBillNO, "txtBillNO");
            frmAdj.AddItem(ckAdjCode, "ckAdjCode");
            frmAdj.AddItem(txtAdjCode, "txtAdjCode");
        }

        #region 事件

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 添加调价单事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void AddAdjHead_Click(object sender, EventArgs e)
        {
            DeprtID = cmbDept.SelectedValue.ToString();
            InvokeController("InitBillHead", DGEnum.BillEditStatus.ADD_STATUS, 0);
            InvokeController("Show", "FrmAdjPriceDetail");
        }

        /// <summary>
        ///  第一次打开窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmAdjPrice_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugDept");
            dtpBillDate.Bdate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            dtpBillDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            if (cmbDept.SelectedValue != null)
            {
                int id = 0;
                if (int.TryParse(cmbDept.SelectedValue.ToString(), out id))
                {
                    InvokeController("LoadAdjHead", cmbDept.SelectedValue.ToString());
                }
            }
        }

        /// <summary>
        /// 选中变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgAdjHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadAdjDetail");
        }

        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cmbDept.SelectedValue != null)
            {
                int id = 0;
                if (int.TryParse(cmbDept.SelectedValue.ToString(), out id))
                {
                    InvokeController("LoadAdjHead", cmbDept.SelectedValue.ToString());
                }
            }
            else
            {
                MessageBoxEx.Show("请选择科室");
            }
        }
        #endregion

        #region 绑定函数
        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
            if (dtDrugDept != null)
            {
                cmbDept.DataSource = dtDrugDept;
                if (dtDrugDept.Rows.Count > 0)
                {
                    cmbDept.SelectedIndex = 0;
                }
            }
        }

        /// <summary>
        /// 绑定头表
        /// </summary>
        /// <param name="dtInadjHead">表头网格数据源</param>
        public void BindInHeadGrid(DataTable dtInadjHead)
        {
            dgAdjHead.DataSource = dtInadjHead;
        }

        /// <summary>
        /// 绑定详细
        /// </summary>
        /// <param name="dtInadjDetail">调价详情数据源</param>
        public void BindInDetailGrid(DataTable dtInadjDetail)
        {
            dgAdjDetail.DataSource = dtInadjDetail;
        }

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgAdjHead.CurrentCell != null)
            {
                if (dgAdjHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgAdjHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgAdjHead.DataSource)).Rows[currentIndex];
                    _currentRow = currentRow;
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("AdjHeadID", currentRow["AdjHeadID"].ToString());
                    return rtn;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="deptID">科室ID</param>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition(int deptID)
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            queryCondition.Add(string.Empty, "a.ExecTime between '" + dtpBillDate.Bdate.Value.ToString("yyyy-MM-dd 00:00:00") + "' and '" + dtpBillDate.Edate.Value.ToString("yyyy-MM-dd 23:59:59") + "'");
            queryCondition.Add("a.DeptID", deptID.ToString());
            if (chkBillNO.Checked && !string.IsNullOrEmpty(txtBillNO.Text))
            {
                queryCondition.Add("a.BillNO", "'" + txtBillNO.Text.Trim() + "'");
            }

            if (ckAdjCode.Checked && !string.IsNullOrEmpty(txtAdjCode.Text))
            {
                queryCondition.Add("a.AdjCode", "'" + txtAdjCode.Text.Trim() + "'");
            }

            return queryCondition;
        }
        #endregion
    }
}
