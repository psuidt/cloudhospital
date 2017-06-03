using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.FinanceMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 采购单选择窗口
    /// </summary>
    public partial class FrmBuyBillChose : BaseFormBusiness, IFrmBuyBillChose
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmBuyBillChose()
        {
            InitializeComponent();
        }

        #region 接口

        /// <summary>
        /// 绑定采购计划头表网格
        /// </summary>
        /// <param name="dt">采购计划头表数据集</param>
        public void BindPlanHeadGrid(DataTable dt)
        {
            dgBillHead.DataSource = dt;
        }

        /// <summary>
        /// 绑定采购计划明细表格
        /// </summary>
        /// <param name="dt">采购计划明细表数据集</param>
        public void BindPlanDetailGrid(DataTable dt)
        {
            if (dt != null)
            {
                dgDetails.DataSource = dt;
            }
        }

        /// <summary>
        /// 取得当前编辑单据明细
        /// </summary>
        /// <returns>单据明细</returns>
        public DataTable GetPlanDetailInfo()
        {
            DataTable dtPanDetail = ((DataTable)(dgDetails.DataSource));
            return dtPanDetail;
        }

        /// <summary>
        /// 获取选中表头ID
        /// </summary>
        /// <returns>当前选中表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    string planHeadId = currentRow["PlanHeadID"].ToString();
                    if (planHeadId == string.Empty)
                    {
                        planHeadId = "0";
                    }

                    rtn.Add("PlanHeadID", planHeadId);
                    return rtn;
                }
                else
                {
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("PlanHeadID", "-1");
                    return rtn;
                }
            }
            else
            {
                Dictionary<string, string> rtn = new Dictionary<string, string>();
                rtn.Add("PlanHeadID", "-1");
                return rtn;
            }
        }
        #endregion

        #region 事件

        /// <summary>
        /// 选中变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                InvokeController("GetPlanDetailData");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dthead = (DataTable)dgBillHead.DataSource;
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    DataTable dtDetails = (DataTable)dgDetails.DataSource;
                    InvokeController("ConvertBuyToInStore", dtDetails);
                }
                else
                {
                    MessageBoxEx.Show("请选择头表记录");
                }
            }
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOutApply_Load(object sender, EventArgs e)
        {
            InvokeController("GetPlanHeadData");
        }
        #endregion
    }
}
