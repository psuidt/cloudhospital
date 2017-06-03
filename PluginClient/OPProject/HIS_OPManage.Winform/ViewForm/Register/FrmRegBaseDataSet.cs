using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.OPManage;
using HIS_OPManage.Winform.IView;

namespace HIS_OPManage.Winform.ViewForm
{
    public partial class FrmRegBaseDataSet : BaseFormBusiness,IFrmRegBaseDataSet
    { 
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmRegBaseDataSet()
        {
            InitializeComponent();
             curTypeRowindex = 0;
        }

        /// <summary>
        /// 当前挂号类别选择行
        /// </summary>
        int curTypeRowindex;

        /// <summary>
        /// 费用明细
        /// </summary>
        public DataTable DtRegTypeFees
        {
            get
            {
                return (DataTable)dgRegItemFee.DataSource;
            }       
        }

        /// <summary>
        /// 设置按钮是否可用
        /// </summary>
        /// <param name="enabled">true可用false不可用</param>
        public void setBarEnabled(bool enabled)
        {
            bar1.Enabled = enabled;
        }

        /// <summary>
        /// 绑定界面费用明细数据源
        /// </summary>
        /// <param name="dt">费用明细数据源</param>
        public void GetDgRegItemFees(DataTable dt)
        {
            dgRegItemFee.DataSource = dt;
        }

        /// <summary>
        /// 当前挂号类型对象
        /// </summary>
        private OP_RegType curRegtype;

        /// <summary>
        /// 当前挂号类型对象
        /// </summary>
        public OP_RegType CurRegtype
        {
            get
            {
                curRegtype.RegTypeCode = txtRegTypeCode.Text.Trim();
                curRegtype.RegTypeName = txtRegTypeName.Text.Trim();
                curRegtype.Flag = chkValid.Checked == true ? 1 : 0;
                return curRegtype;
            }

            set
            {
                curRegtype = value;
                txtRegTypeCode.Text = curRegtype.RegTypeCode;
                txtRegTypeName.Text = curRegtype.RegTypeName;
                chkValid.Checked = curRegtype.Flag == 0 ? false : true;
            }
        }

        /// <summary>
        /// 加载挂号类别数据源
        /// </summary>
        /// <param name="dtRegTypeList">挂号类别</param>
        public void loadRegTypes(DataTable  dtRegTypeList)
        {
            dgRegType.DataSource = dtRegTypeList;
            if (dtRegTypeList.Rows.Count > 0)
            {
                dgRegType.CurrentCell = dgRegType[RegTypeCode.Name, curTypeRowindex];
            }
        }

        /// <summary>
        /// 新增挂号类别按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnAddRegType_Click(object sender, EventArgs e)
        {
            curRegtype = new OP_RegType();
            curRegtype.Flag = 1;
            curRegtype.RegTypeCode = string.Empty;
            curRegtype.RegTypeName = string.Empty;
            curRegtype.RegTypeID = 0;
            CurRegtype = curRegtype;
        }

        /// <summary>
        /// 保存挂号类别按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSaveRegType_Click(object sender, EventArgs e)
        {
            if (txtRegTypeCode.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("类型编码不能为空");
                txtRegTypeCode.Focus();
                return;
            }

            if (txtRegTypeName.Text.Trim() == string.Empty)
            {
                MessageBoxEx.Show("类型名称不能为空");
                txtRegTypeName.Focus();
                return;
            }

            string newCode = txtRegTypeCode.Text.Trim();
            InvokeController("SaveRegType");
            DataTable dtRegType =(DataTable) dgRegType.DataSource;
            int rowindex = 0;
            for (int i = 0; i < dtRegType.Rows.Count; i++)
            {
                if (dtRegType.Rows[i]["RegTypeCode"].ToString() == newCode)
                {
                    rowindex = i;
                    break;
                }
            }

            dgRegType.CurrentCell = dgRegType[0, rowindex];
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close",this);
        }

        /// <summary>
        /// 窗体OpenWindowBefore事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void FrmRegBaseDataSet_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("LoadRegType");
        }

        /// <summary>
        /// 设置费用录入选项卡数据源
        /// </summary>
        /// <param name="dtFeeSource">费用录入选项卡数据</param>
        public void SetRegItemShowCard(DataTable dtFeeSource)
        {
            dgRegItemFee.SelectionCards[0].BindColumnIndex = ItemCode.Index;
            dgRegItemFee.SelectionCards[0].CardColumn = "ItemId|项目ID|50,ItemCode|编码|80,ItemName|项目名称|150,UnitPrice|单价|auto";
            dgRegItemFee.SelectionCards[0].CardSize = new System.Drawing.Size(350, 300);
            dgRegItemFee.SelectionCards[0].QueryFieldsString = "ItemName,ItemCode,PYm,WBm";
            dgRegItemFee.BindSelectionCardDataSource(0, dtFeeSource);
        }

        /// <summary>
        /// 新增费用明细按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (CurRegtype == null)
            {
                return;
            }

            if (CurRegtype.Flag == 0)
            {
                MessageBoxEx.Show("当前挂号类型无效，不能添加收费项目");
                return;
            }

            SetGridState = false;
            dgRegItemFee.AddRow();
        }

        /// <summary>
        /// 设置费用明细网格只读
        /// </summary>
        public bool SetGridState
        {
            set
            {
                bool b = value;
                if (b == false)
                {
                    dgRegItemFee.ReadOnly = false;
                    ItemCode.ReadOnly = false;
                    ItemID.ReadOnly = true;
                    ItemName.ReadOnly = true;
                    ItemPrice.ReadOnly = true;                 
                }
                else
                {
                    dgRegItemFee.ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// 删除对应费用
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnDelItem_Click(object sender, EventArgs e)
        {
            if (dgRegItemFee.CurrentCell != null)
            {
                if (CurRegtype.Flag == 0)
                {
                    MessageBoxEx.Show("当前挂号类型无效，不能删除收费项目");
                    return;
                }

                if (MessageBoxEx.Show("确定要删除该费用信息吗？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {                  
                    int rowid = this.dgRegItemFee.CurrentCell.RowIndex;
                    DataTable dt = (DataTable)dgRegItemFee.DataSource;
                    if (dt.Rows[rowid]["ID"] == DBNull.Value || Convert.ToInt32(dt.Rows[rowid]["ID"]) == 0)
                    {
                        dt.Rows.RemoveAt(rowid);
                    }
                    else
                    {
                        InvokeController("DeleteRegItemFees", Convert.ToInt32(dt.Rows[rowid]["ID"]));
                        InvokeController("SaveRegType");
                    }
                }
            }
        }

        /// <summary>
        /// 选项卡选中事件
        /// </summary>
        /// <param name="SelectedValue">选中行</param>
        /// <param name="stop">是否往后跳行</param>
        /// <param name="customNextColumnIndex">下一列号</param>
        private void dgRegItemFee_SelectCardRowSelected(object SelectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)SelectedValue;
                int colid = this.dgRegItemFee.CurrentCell.ColumnIndex;
                int rowid = this.dgRegItemFee.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgRegItemFee.DataSource;
                if (colid == ItemCode.Index)
                {
                    dt.Rows[rowid]["ItemID"] = row["ItemID"];
                    dt.Rows[rowid]["ItemName"] = row["ItemName"];
                    dt.Rows[rowid]["ItemPrice"] = row["UnitPrice"];
                    dt.Rows[rowid]["ID"] = 0;
                }
                dgRegItemFee.Refresh();
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 保存费用明细按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            dgRegItemFee.EndEdit();
            SetGridState = true;
            int curSelIndex = dgRegType.CurrentCell.RowIndex;
            InvokeController("SaveRegItemFees");
            InvokeController("SaveRegType");
            dgRegType.CurrentCell = dgRegType[RegTypeCode.Name, curSelIndex];
        }
  
        /// <summary>
        /// 显示挂号明细费用
        /// </summary>
        private void ShowRegItemFees()
        {
            int rowindex = dgRegType.CurrentCell.RowIndex;
            List<OP_RegType> listregtype = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToList<OP_RegType>((DataTable)dgRegType.DataSource);// (List<OP_RegType>)dgRegType.DataSource;
            CurRegtype = listregtype[rowindex];
            int regtypeid = CurRegtype.RegTypeID;
            InvokeController("LoadRegItems", regtypeid);
        }

        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowRegItemFees();
            SetGridState = true;
        }

        /// <summary>
        /// 挂号类别网格CurrentCellChanged事件
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void dgRegType_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgRegType.CurrentCell != null)
            {
                dgRegItemFee.EndEdit();
                curTypeRowindex = dgRegType.CurrentCell.RowIndex;
                ShowRegItemFees();
            }
        }
    }
}
