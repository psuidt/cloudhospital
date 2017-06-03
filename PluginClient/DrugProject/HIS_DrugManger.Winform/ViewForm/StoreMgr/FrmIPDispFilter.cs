using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.StoreMgr;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 过滤
    /// </summary>
    public partial class FrmIPDispFilter : BaseFormBusiness, IFrmIPDispFilter
    {
        /// <summary>
        /// 当前明细
        /// </summary>
        public DataTable CurrentDetail { get; set; }

        /// <summary>
        /// 当前选中明细
        /// </summary>
        public DataTable SelectedDetail { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public int Result = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmIPDispFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentDetail">当前数据源</param>
        public FrmIPDispFilter(DataTable currentDetail)
        {
            InitializeComponent();
            this.CurrentDetail = currentDetail;
        }

        #region 自定义属性和方法
        /// <summary>
        /// 设置全选，全不选
        /// </summary>
        /// <param name="isSelect">是否选中</param>
        private void SetRecipeOrderAllSelect(bool isSelect)
        {
            DataTable dt = dgDetail.DataSource as DataTable;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    int value = 1;
                    if (!isSelect)
                    {
                        value = 0;
                    }

                    for (int index = 0; index < dt.Rows.Count; index++)
                    {
                        dt.Rows[index]["chk"] = value;
                    }
                }
            }
        }

        /// <summary>
        /// 过滤明细
        /// </summary>
        public void FilterDetail()
        {
            string filter = string.Empty;
            if (txtDrugCard.MemberValue != null)
            {
                filter = "DrugID=" + txtDrugCard.MemberValue.ToString();
            }

            if (txtBedNo.Text.Trim() != string.Empty)
            {
                if (filter == string.Empty)
                {
                    filter = "BedNO='" + txtBedNo.Text.Trim() + "'";
                }
                else
                {
                    filter = filter + " and BedNO='" + txtBedNo.Text.Trim() + "'";
                }
            }

            if (txtPatientNo.Text.Trim() != string.Empty)
            {
                if (filter == string.Empty)
                {
                    filter = "InpatientNO='" + txtPatientNo.Text.Trim() + "'";
                }
                else
                {
                    filter = filter + " and InpatientNO='" + txtPatientNo.Text.Trim() + "'";
                }
            }

            if (txtName.Text.Trim() != string.Empty)
            {
                if (filter == string.Empty)
                {
                    filter = "PatName='" + txtName.Text.Trim() + "'";
                }
                else
                {
                    filter = filter + " and PatName='" + txtName.Text.Trim() + "'";
                }
            }

            DataTable dt = this.CurrentDetail.Copy();
            DataView dv = dt.DefaultView;
            dv.RowFilter = filter;
            dgDetail.DataSource = dv.ToTable();
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定药品ShowCard
        /// </summary>
        /// <param name="dtDrug">药品数据</param>
        public void BindDrugShowCard(DataTable dtDrug)
        {
            txtDrugCard.DisplayField = "ChemName";
            txtDrugCard.MemberField = "DrugID";
            txtDrugCard.CardColumn = "DrugID|编码|100,ChemName|药品名称|200,Spec|规格|120,UnPickUnit|包装单位|70,SellPrice|单价|50,Address|生产厂家|160,StoreAmount|库存数|80,MiniUnitName|单位|50,ExecDeptName|执行科室|80";
            txtDrugCard.QueryFieldsString = "DrugID,ChemName,pym,wbm,TradeName,PYCode,WbCode";
            txtDrugCard.ShowCardWidth = 600;
            txtDrugCard.ShowCardDataSource = dtDrug;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterDetail();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            SetRecipeOrderAllSelect(true);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNotAll_Click(object sender, EventArgs e)
        {
            SetRecipeOrderAllSelect(false);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectedDetail = dgDetail.DataSource as DataTable;
            for (int i = 0; i < dgDetail.Rows.Count; i++)
            {
                if (Convert.ToBoolean(((DataGridViewCheckBoxXCell)dgDetail.Rows[i].Cells["chk"]).Value) == true)
                {
                    SelectedDetail.Rows[i]["chk"] = 1;
                }
                else
                {
                    SelectedDetail.Rows[i]["chk"] = 0;
                }
            }

            SelectedDetail.AcceptChanges();
            Result = 1;
            this.DialogResult = DialogResult.OK;
            InvokeController("DispFilter", Result, SelectedDetail);
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Result = 0;
            this.DialogResult = DialogResult.Cancel;
            InvokeController("Close", this);
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmIPDispFilter_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetDrugShowCard");
            txtBedNo.Focus();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmIPDispFilter_Load(object sender, EventArgs e)
        {
            InvokeController("GetDrugShowCard");
            dgDetail.DataSource = CurrentDetail;
            txtBedNo.Focus();
        }

        /// <summary>
        /// 事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataTable dt = dgDetail.DataSource as DataTable;
                if (Convert.ToInt32(dt.Rows[e.RowIndex]["chk"]) == 0)
                {
                    dt.Rows[e.RowIndex]["chk"] = 1;
                }
                else
                {
                    dt.Rows[e.RowIndex]["chk"] = 0;
                }
            }
        }
        #endregion
    }
}
