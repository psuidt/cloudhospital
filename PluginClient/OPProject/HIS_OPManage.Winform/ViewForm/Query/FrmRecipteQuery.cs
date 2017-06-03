using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRecipteQuery : BaseFormBusiness,IFrmRecipteQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRecipteQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        Dictionary<string, object> myDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 查询条件
        /// </summary>
       public Dictionary<string, object> QueryDictionary
        {
            get
            {
                return myDictionary;
            }

            set
            {
                myDictionary = value;
            }
        }

        /// <summary>
        /// 绑定查询数据
        /// </summary>
        /// <param name="dtData">处方头数据</param>
        public void BindQueryData(DataTable dtData)
        {
            dgQueryData.DataSource = dtData;
            SetGridColor();
            dgDetail.DataSource = new DataTable();
        }

        /// <summary>
        /// 设置网格颜色
        /// </summary>
        private void SetGridColor()
        {
            if (dgQueryData != null && dgQueryData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgQueryData.Rows)
                {
                    string status_flag = row.Cells["status"].Value.ToString();
                    Color foreColor = Color.Black;
                    switch (status_flag)
                    {
                        case "正常":
                            foreColor = Color.Black;
                            break;
                        case "退费":
                            foreColor = Color.Red;
                            break;
                    }

                    dgQueryData.SetRowColor(row.Index, foreColor, true);
                }
            }
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            myDictionary.Clear();        
            if (chkCostDate.Checked)
            {
                if (sdtCostDate.Edate.Value < sdtCostDate.Bdate.Value)
                {
                    MessageBoxEx.Show("开始时间不能大于结束时间");
                    sdtCostDate.Edate.Focus();
                    return;
                }

                if (sdtCostDate.Edate.Value.AddDays(-7)>sdtCostDate.Bdate.Value.Date)
                {
                    MessageBoxEx.Show("收费时间选择范围超过7天，查询数据量过大，请重新选择日期");
                    sdtCostDate.Edate.Focus();
                    return;
                }

                if (sdtCostDate.Bdate.Value != null && sdtCostDate.Edate.Value != null)
                {
                    myDictionary.Add("CostBdate", sdtCostDate.Bdate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    myDictionary.Add("CostEdate", sdtCostDate.Edate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                }                
            }

            if (chkInvoiceNO.Checked)
            {
                if (txtInvoiceNO.Text.Trim() != string.Empty)
                {
                    myDictionary.Add("InvoiceNO", txtInvoiceNO.Text.Trim());
                }
            }

            if (chkChareEmp.Checked)
            {
                if (txtChareEmp.Text.Trim() != string.Empty)
                {
                    myDictionary.Add("ChargeEmp", txtChareEmp.MemberValue);
                }
            } 
                    
            if (chkPayType.Checked)
            {
                if (cmbPatType.Text.Trim() != string.Empty)
                {
                    myDictionary.Add("PayType", cmbPatType.SelectedValue);
                }
            }

            if (chkPatName.Checked)
            {
                if (txtPatName.Text.Trim() != string.Empty)
                {
                    myDictionary.Add("PatName", txtPatName.Text.Trim());
                }
            }

            if (chkCardNO.Checked)
            {
                if (txtCardNo.Text.Trim() != string.Empty)
                {
                    myDictionary.Add("CardNO", txtCardNo.Text.Trim());
                }
            }

            if (chkRefund.Checked)
            {
                myDictionary.Add("RefundFalg", 1);
            }

            InvokeController("ReciptQuery");
        }

        /// <summary>
        /// 收费员
        /// </summary>
        /// <param name="dtCharger">收费员数据</param>
        public void BindCharger(DataTable dtCharger)
        {
            txtChareEmp.DisplayField = "Name";
            txtChareEmp.MemberField = "EmpID";
            txtChareEmp.CardColumn = "Name|姓名|auto";
            txtChareEmp.QueryFieldsString = "Name,pym,wbm";
            txtChareEmp.ShowCardHeight = 160;
            txtChareEmp.ShowCardWidth = 140;
            txtChareEmp.ShowCardDataSource = dtCharger;
        }

        /// <summary>
        /// 病人类型
        /// </summary>
        /// <param name="dtPatType">病人类型数据</param>
        public void BindPatType(DataTable dtPatType)
        {
            cmbPatType.DataSource = dtPatType;
            cmbPatType.ValueMember = "PatTypeID";
            cmbPatType.DisplayMember = "PatTypeName";
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRecipteQuery_OpenWindowBefore(object sender, EventArgs e)
        {
            sdtCostDate.Bdate.Value =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            sdtCostDate.Edate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
            InvokeController("RecipterInit");
        }

        /// <summary>
        /// 网格CellClick事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgQueryData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgQueryData.CurrentCell == null)
            {
                return;
            }

            if (dgQueryData.CurrentCell.ColumnIndex == dgQueryData.Columns[selected.Name].Index)
            {
                int rowindex = dgQueryData.CurrentCell.RowIndex;
                dgQueryData[selected.Index, rowindex].Value =Convert.ToInt32( dgQueryData[selected.Index, rowindex].Value) == 0 ? 1 : 0;
            }

            DataTable dtSource = (DataTable)dgQueryData.DataSource;           
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strFeeItemHeadIDs = "0";
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtSource.Rows[i]["selected"]) == 1)
                    {
                        strFeeItemHeadIDs += "," + dtSource.Rows[i]["FeeItemHeadID"];
                    }
                }

                if (strFeeItemHeadIDs == "0")
                {
                    return;
                }

                InvokeController("QueryReciptDetail", strFeeItemHeadIDs);
            }
        }

        /// <summary>
        /// 处方明细按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDetail_Click(object sender, EventArgs e)
        {
            DataTable dtSource =(DataTable)dgQueryData.DataSource;
            if (dtSource != null && dtSource.Rows.Count > 0)
            {
                string strFeeItemHeadIDs = "0";
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtSource.Rows[i]["selected"]) == 1)
                    {
                        strFeeItemHeadIDs += "," + dtSource.Rows[i]["FeeItemHeadID"];
                    }                  
                }

                if (strFeeItemHeadIDs == "0")
                {
                    MessageBoxEx.Show("没有选择需要查看明细的处方");
                    return;
                }

                InvokeController("QueryReciptDetail", strFeeItemHeadIDs);
            }            
        }

        /// <summary>
        /// 绑定明细数据
        /// </summary>
        /// <param name="dtDetail">处方明细数据</param>
        public void BindDetailData(DataTable dtDetail)
        {
            dgDetail.DataSource = dtDetail;
        }

        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgQueryData_Click(object sender, EventArgs e)
        {
            if (dgQueryData.CurrentCell == null)
            {
                return;
            }

            dgDetail.DataSource = new DataTable();
            string strFeeItemHeadIDs = "0";
            strFeeItemHeadIDs += ","+dgQueryData[FeeItemHeadID.Index, dgQueryData.CurrentCell.RowIndex].Value;  
            InvokeController("QueryReciptDetail", strFeeItemHeadIDs);
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
    }
}
