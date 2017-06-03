using System;
using System.Collections.Generic;
using System.Data;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药品申请单
    /// </summary>
    public partial class FrmOutApply : BaseFormBusiness,IFrmApply
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOutApply()
        {
            InitializeComponent();
        }

        #region 接口
        /// <summary>
        /// 绑定药剂科室控件
        /// </summary>
        /// <param name="dtDrugDept">数据源</param>
        public void BindDrugDept(DataTable dtDrugDept)
        {
        }

        /// <summary>
        /// 获取当前表头ID
        /// </summary>
        /// <returns>表头ID</returns>
        public Dictionary<string, string> GetCurrentHeadID()
        {
            if (dgBillHead.CurrentCell != null)
            {
                if (dgBillHead.CurrentCell.RowIndex >= 0)
                {
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    Dictionary<string, string> rtn = new Dictionary<string, string>();
                    rtn.Add("ApplyHeadID", currentRow["ApplyHeadID"].ToString());
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
        /// 绑定表头信息
        /// </summary>
        /// <param name="dt">表头信息</param>
        public void BindApplyHead(DataTable dt)
        {
            dgBillHead.DataSource = dt;
            if (dt.Rows.Count == 0)
            {
                InvokeController("LoadBillDetails", frmName);
            }
        }

        /// <summary>
        /// 绑定明细信息
        /// </summary>
        /// <param name="dt">明细信息</param>
        public void BindApplyDetail(DataTable dt)
        {
            this.dgDetails.DataSource = dt;
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>表头查询条件</returns>
        public Dictionary<string, string> GetQueryCondition()
        {
            Dictionary<string, string> queryCondition = new Dictionary<string, string>();
            return queryCondition;
        }
        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmOutApply_OpenWindowBefore(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgBillHead_CurrentCellChanged(object sender, EventArgs e)
        {
            InvokeController("LoadApplyDetails");
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 事件
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
                    int currentIndex = dgBillHead.CurrentCell.RowIndex;
                    DataRow currentRow = ((DataTable)(dgBillHead.DataSource)).Rows[currentIndex];
                    InvokeController("ConvertDsApply", currentRow);
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
            InvokeController("LoadApplyHead");
        }
    }
}
