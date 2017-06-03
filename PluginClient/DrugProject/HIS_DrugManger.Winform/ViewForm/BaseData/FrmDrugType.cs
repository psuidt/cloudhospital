using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    public partial class FrmDrugType : BaseFormBusiness, IFrmDrugType
    {
        #region 药品类型
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmDrugType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 药品类型对象
        /// </summary>
        public DG_TypeDic CurrentDataP { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryConditionP { get; set; }

        /// <summary>
        /// 读取药品类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugType(DataTable dt)
        {
            this.dgDrugType.DataSource = dt;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugType_OpenWindowBefore(object sender, EventArgs e)
        {
            QueryConditionP = new Dictionary<string, string>();
            InvokeController("GetDrugType");
        }
        #endregion

        #region 药品类型事件
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugType_Click(object sender, EventArgs e)
        {
            if (dgDrugType.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDrugType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugType.DataSource;
            var pType = new DG_TypeDic();
            pType.TypeName = dt.Rows[rowindex]["TypeName"].ToString();
            pType.TypeID = Convert.ToInt32(dt.Rows[rowindex]["TypeID"]);
            pType.PYCode = dt.Rows[rowindex]["PYCode"].ToString();
            pType.WBCode = dt.Rows[rowindex]["WBCode"].ToString();
            CurrentDataP = pType;
            this.txtDrugTypeA.Text = pType.TypeName;
            #region 加载子表
            QueryConditionC = new Dictionary<string, string>();
            string p;
            if (!QueryConditionC.TryGetValue("TypeID", out p))
            {
                QueryConditionC.Add("TypeID", pType.TypeID.ToString());
            }

            InvokeController("GetChildDrugType");
            #endregion
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btn_SaveP_Click(object sender, EventArgs e)
        {
            if (this.txtDrugTypeA.Text.Trim() == string.Empty)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("类型名称不能为空");
                return;
            }

            if (this.txtDrugTypeA.Text.Trim().Length > 10)
            {
                MessageBoxEx.Show("名称长度不能大于10");
                return;
            }

            DG_TypeDic productType = null;
            if (CurrentDataP != null)
            {
                if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                productType = CurrentDataP;
            }
            else
            {
                productType = new DG_TypeDic();
            }

            productType.TypeName = this.txtDrugTypeA.Text;
            int rowindex = 0;
            if (dgDrugType.CurrentCell != null)
            {
                rowindex = dgDrugType.CurrentCell.RowIndex;
            }

            productType.TypeName = this.txtDrugTypeA.Text.Trim();
            productType.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productType.TypeName);
            productType.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productType.TypeName);
            CurrentDataP = productType;
            InvokeController("SaveDrugType");
            if (rowindex != 0)
            {
                dgDrugType.CurrentCell = dgDrugType[1, rowindex];
            }

            CurrentDataP = null;
            txtDrugTypeA.Text = string.Empty;
            txtDrugTypeA.Focus();
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtDrugTypeA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btn_SaveP.Focus();
            }
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnNewP_Click(object sender, EventArgs e)
        {
            this.txtDrugTypeA.Text = string.Empty;
            CurrentDataP = null;
            this.txtDrugTypeA.Focus();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCancleP_Click(object sender, EventArgs e)
        {
            SetControlPCancle();
        }

        /// <summary>
        /// 编辑操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnEditP_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        public void SetControlPNew()
        {
            this.btnNewP.Enabled = false;
            this.btn_SaveP.Enabled = true;
            this.txtDrugTypeA.Text = string.Empty;
            this.txtDrugTypeA.Focus();
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        public void SetControlPCancle()
        {
            this.btnNewP.Enabled = true;
            this.btn_SaveP.Enabled = false;
            this.txtDrugTypeA.Focus();
            this.txtDrugTypeA.Text = string.Empty;
        }
        #endregion

        #region 药品子类型
        /// <summary>
        /// 子类型对象
        /// </summary>
        public DG_ChildTypeDic CurrentDataC { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryConditionC { get; set; }

        /// <summary>
        /// 读取子药品类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadChildDrugType(DataTable dt)
        {
            this.dgDrugChildType.DataSource = dt;
        }
        #endregion

        #region 药品子类型事件
        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCancelC_Click(object sender, EventArgs e)
        {
            SetControlCCancle();
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAddC_Click(object sender, EventArgs e)
        {
            CurrentDataC = null;
            this.txtChildTypeName.Text = string.Empty;
            this.txtChildTypeName.Focus();
        }

        /// <summary>
        /// 键盘操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtChildTypeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSaveC.Focus();
            }
        }

        /// <summary>
        /// 控件启用
        /// </summary>
        public void SetControlCNew()
        {
            this.btnAddC.Enabled = false;
            this.btnSaveC.Enabled = true;
            this.txtChildTypeName.Text = string.Empty;
            this.txtChildTypeName.Focus();
        }

        /// <summary>
        /// 控件禁用
        /// </summary>
        public void SetControlCCancle()
        {
            this.btnAddC.Enabled = true;
            this.btnSaveC.Enabled = false;
            this.txtChildTypeName.Focus();
            this.txtChildTypeName.Text = string.Empty;
        }

        /// <summary>
        /// 报错操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSaveC_Click(object sender, EventArgs e)
        {
            if (this.txtChildTypeName.Text.Trim() == string.Empty)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("药品子类型名称不能为空");
                return;
            }

            if (this.txtChildTypeName.Text.Trim().Length > 10)
            {
                MessageBoxEx.Show("名称长度不能大于10");
                return;
            }

            if (CurrentDataP == null)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("请选择药品类型表项");
                return;
            }

            DG_ChildTypeDic productCType = null;
            if (CurrentDataC != null)
            {
                if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                productCType = CurrentDataC;
            }
            else
            {
                productCType = new DG_ChildTypeDic();
            }

            int rowindexchild = 0;
            if (dgDrugChildType.CurrentCell != null)
            {
                rowindexchild = dgDrugChildType.CurrentCell.RowIndex;
            }

            productCType.CTypeName = this.txtChildTypeName.Text.Trim();
            productCType.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productCType.CTypeName);
            productCType.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productCType.CTypeName);
            int rowindex = dgDrugType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugType.DataSource;
            productCType.TypeID = Convert.ToInt32(dt.Rows[rowindex]["TypeID"]);
            CurrentDataC = productCType;
            InvokeController("SaveChildDrugType");
            if (rowindexchild != 0)
            {
                dgDrugChildType.CurrentCell = dgDrugChildType[0, rowindexchild];
            }

            dgDrugType.Rows[rowindex].Selected = true;
            CurrentDataC = null;
            txtChildTypeName.Text = string.Empty;
            txtChildTypeName.Focus();
        }
        #endregion

        /// <summary>
        /// 药品子类型操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugChildType_Click(object sender, EventArgs e)
        {
            if (dgDrugChildType.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDrugChildType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugChildType.DataSource;
            var pType = new DG_ChildTypeDic();
            pType.CTypeID = Convert.ToInt32(dt.Rows[rowindex]["CTypeID"]);
            pType.CTypeName = dt.Rows[rowindex]["CTypeName"].ToString();
            pType.TypeID = Convert.ToInt32(dt.Rows[rowindex]["TypeID"]);
            pType.PYCode = dt.Rows[rowindex]["PYCode"].ToString();
            pType.WBCode = dt.Rows[rowindex]["WBCode"].ToString();
            CurrentDataC = pType;
            this.txtChildTypeName.Text = pType.CTypeName;
        }

        /// <summary>
        /// 改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugType_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDrugType.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDrugType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugType.DataSource;

            DG_TypeDic pType = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_TypeDic>(dt, rowindex);

            CurrentDataP = pType;
            this.txtDrugTypeA.Text = pType.TypeName;

            #region 加载子表
            QueryConditionC = new Dictionary<string, string>();
            string p;
            if (!QueryConditionC.TryGetValue("TypeID", out p))
            {
                QueryConditionC.Add("TypeID", pType.TypeID.ToString());
            }

            InvokeController("GetChildDrugType");
            #endregion
        }

        /// <summary>
        /// 当前选中行改变事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugChildType_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDrugChildType.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgDrugChildType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugChildType.DataSource;
            DG_ChildTypeDic pType = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_ChildTypeDic>(dt, rowindex);
            CurrentDataC = pType;
            this.txtChildTypeName.Text = pType.CTypeName;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugChildType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
