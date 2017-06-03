using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资分类维护
    /// </summary>
    public partial class FrmMaterialType : BaseFormBusiness, IFrmMaterialType
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialType()
        {
            InitializeComponent();
            fmCommon.AddItem(this.btn_QueryP, "btn_QueryP");
            fmCommon.AddItem(this.txtDrugChildTypeQ, "txtDrugChildTypeQ");
            fmCommon.AddItem(this.btn_QueryC, "btn_QueryC");
            fmCommon.AddItem(this.txtChildTypeName, "txtChildTypeName");
        }

        /// <summary>
        /// 是否为首次加载
        /// </summary>
        public bool IsFirstLoad = true;

        /// <summary>
        /// 选中的父节点
        /// </summary>
        public TreeNode CurrentParentNode;

        /// <summary>
        /// 选中的物资分类
        /// </summary>
        public MW_TypeDic CurrentDataP { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryConditionP { get; set; }

        /// <summary>
        /// 绑定类型节点信息
        /// </summary>
        /// <param name="typelist">物资分类列表</param>
        public void LoadMaterialType(DataTable typelist)
        {
            advTree.Nodes.Clear();
            Bind_Textboxcard(typelist);
            NodeBind(this.advTree, typelist, "0", null);
            this.advTree.ExpandAll();
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeView">树型控件</param>
        /// <param name="dt">数据源</param>
        /// <param name="pid">父ID</param>
        /// <param name="pNode">父节点</param>
        public void NodeBind(TreeView treeView, DataTable dt, string pid, TreeNode pNode)
        {
            string sFilter = "ParentID=" + pid;
            TreeNode parentNode = pNode;
            DataView dv = new DataView(dt);
            dv.RowFilter = sFilter;
            if (dv.Count > 0)
            {
                foreach (DataRowView drv in dv)
                {
                    TreeNode tempNode = new TreeNode();
                    tempNode.Text = drv["TypeName"].ToString();
                    tempNode.Name = drv["TypeName"].ToString();
                    tempNode.Tag = drv["TypeID"].ToString();
                    if (parentNode != null)
                    {
                        parentNode.Nodes.Add(tempNode);
                    }
                    else
                    {
                        treeView.Nodes.Add(tempNode);
                    }

                    NodeBind(treeView, dt, drv["TypeID"].ToString(), tempNode);
                }
            }
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialType_OpenWindowBefore(object sender, EventArgs e)
        {
            QueryConditionP = new Dictionary<string, string>();
            InvokeController("GetMaterialType");
            IsFirstLoad = false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_QueryP_Click(object sender, EventArgs e)
        {
            string nodeString = this.tbcPharm.Text.Trim();
            var nodes = this.advTree.Nodes.Find(nodeString, true);
            if (nodes.Length > 0)
            {
                this.advTree.SelectedNode = nodes[0];
                QueryConditionC = new Dictionary<string, string>();
                QueryConditionC.Add("ParentID", nodes[0].Tag.ToString());
                InvokeController("GetChildMaterialType");
            }
        }

        /// <summary>
        /// 绑定类型数据
        /// </summary>
        /// <param name="dt">类型数据</param>
        public void Bind_Textboxcard(DataTable dt)
        {
            tbcPharm.DisplayField = "TypeName";
            tbcPharm.MemberField = "TypeID";
            tbcPharm.CardColumn = "TypeName|类型名称|auto,PYCode|拼音码|auto";
            tbcPharm.QueryFieldsString = "TypeName,PYCode";
            tbcPharm.ShowCardWidth = 350;
            tbcPharm.ShowCardDataSource = dt;
        }

        #region 物资子类型
        /// <summary>
        /// 选中的物资子类型
        /// </summary>
        public MW_TypeDic CurrentDataC { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryConditionC { get; set; }

        /// <summary>
        /// 绑定物资子类型
        /// </summary>
        /// <param name="dt">物资子类型</param>
        public void LoadChildMaterialType(DataTable dt)
        {
            this.dgMaterialChildType.DataSource = dt;
        }

        #endregion

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSaveC_Click(object sender, EventArgs e)
        {
            if (this.txtChildTypeName.Text.Trim() == string.Empty)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("物资类型名称不能为空");
                return;
            }

            if (this.txtChildTypeName.Text.Trim().Length > 10)
            {
                MessageBoxEx.Show("名称长度不能大于10");
                return;
            }

            if (this.advTree.SelectedNode == null)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("请选择物资父类型");
                return;
            }

            MW_TypeDic productCType = null;
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
                productCType = new MW_TypeDic();
            }

            int rowindexchild = 0;
            if (dgMaterialChildType.CurrentCell != null)
            {
                rowindexchild = dgMaterialChildType.CurrentCell.RowIndex;
            }

            productCType.TypeName = this.txtChildTypeName.Text.Trim();
            productCType.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productCType.TypeName);
            productCType.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productCType.TypeName);
            productCType.ParentID = Convert.ToInt32(this.advTree.SelectedNode.Tag);
            if (this.advTree.SelectedNode.Level > 3)
            {
                MessageBoxEx.Show("三级节点不能添加子类");
                return;
            }
            else
            {
                productCType.Level = this.advTree.SelectedNode.Level + 1;
            }

            CurrentParentNode = this.advTree.SelectedNode;
            CurrentDataC = productCType;
            InvokeController("SaveChildMaterialType");

            if (rowindexchild != 0)
            {
                dgMaterialChildType.CurrentCell = dgMaterialChildType[0, rowindexchild];
            }

            CurrentDataC = null;
            InvokeController("GetMaterialType");
        }

        /// <summary>
        /// 选中物资子类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgMaterialChildType_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgMaterialChildType.CurrentCell == null)
            {
                this.txtChildTypeName.Text = string.Empty;
                CurrentDataC = null;
                return;
            }

            int rowindex = dgMaterialChildType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgMaterialChildType.DataSource;

            MW_TypeDic pType = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_TypeDic>(dt, rowindex);
            CurrentDataC = pType;
            this.txtChildTypeName.Text = pType.TypeName;
        }

        /// <summary>
        /// 选中物资子类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgMaterialChildType_Click(object sender, EventArgs e)
        {
            if (dgMaterialChildType.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgMaterialChildType.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgMaterialChildType.DataSource;

            var pType = new MW_TypeDic();
            pType.TypeID = Convert.ToInt32(dt.Rows[rowindex]["TypeID"]);
            pType.TypeName = dt.Rows[rowindex]["TypeName"].ToString();
            pType.ParentID = Convert.ToInt32(dt.Rows[rowindex]["ParentID"]);
            pType.PYCode = dt.Rows[rowindex]["PYCode"].ToString();
            pType.WBCode = dt.Rows[rowindex]["WBCode"].ToString();
            CurrentDataC = pType;
            this.txtChildTypeName.Text = pType.TypeName;
        }

        /// <summary>
        /// 加载物资子类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_QueryC_Click(object sender, EventArgs e)
        {
            QueryConditionC = new Dictionary<string, string>();
            if (this.advTree.SelectedNode == null)
            {
                MessageBoxEx.Show("请先选择父节点");
                return;
            }

            QueryConditionC.Add("ParentID", this.advTree.SelectedNode.Tag.ToString());
            if (txtDrugChildTypeQ.Text.Trim() != string.Empty)
            {
                string p;
                if (!QueryConditionC.TryGetValue("TypeName", out p))
                {
                    QueryConditionC.Add("TypeName", txtDrugChildTypeQ.Text.Trim());
                    QueryConditionC.Add("PYCode", txtDrugChildTypeQ.Text.Trim());
                }
                else
                {
                    QueryConditionC["TypeName"] = txtDrugChildTypeQ.Text.Trim();
                    QueryConditionC["PYCode"] = txtDrugChildTypeQ.Text.Trim();
                }
            }

            InvokeController("GetChildMaterialType");
        }

        /// <summary>
        /// 新增子类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAddC_Click(object sender, EventArgs e)
        {
            this.txtChildTypeName.Text = string.Empty;
            this.txtChildTypeName.Focus();
            CurrentDataC = null;
        }

        /// <summary>
        /// 删除子类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (CurrentDataC == null)
            {
                MessageBoxEx.Show("请选择要删除的记录");
                return;
            }

            if (MessageBox.Show("确定删除记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            MW_TypeDic productCType = CurrentDataC;
            InvokeController("DelChildMaterialType");
            CurrentDataC = null;
            InvokeController("GetMaterialType");
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 选择分类节点
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void advTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            QueryConditionC = new Dictionary<string, string>();
            QueryConditionC.Add("ParentID", e.Node.Tag.ToString());
            QueryConditionC.Add("Level", e.Node.Level.ToString());
            this.txtDrugChildTypeQ.Text = string.Empty;
            InvokeController("GetChildMaterialType");
        }
    }
}
