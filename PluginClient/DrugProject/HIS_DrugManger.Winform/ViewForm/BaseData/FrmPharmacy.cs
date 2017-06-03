using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using EFWCoreLib.WcfFrame.DataSerialize;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_Entity.DrugManage;

namespace HIS_DrugManage.Winform.ViewForm
{
    /// <summary>
    /// 药理分类维护
    /// </summary>
    public partial class FrmPharmacy : BaseFormBusiness, IFrmPharmacy
    {
        /// <summary>
        /// 行索引
        /// </summary>
        private int rowIndex = 0;

        /// <summary>
        /// 树节点索引
        /// </summary>
        private string nodeTag = string.Empty;

        /// <summary>
        /// 树节点名称
        /// </summary>
        private string nodeText = string.Empty;

        /// <summary>
        /// Key值
        /// </summary>
        private int key = 0;

        /// <summary>
        /// 获取树节点Key
        /// </summary>
        /// <param name="k">Key值</param>
        public void GetDG_PharmacologyKey(int k)
        {
            key = k;
        }

        #region 接口
        /// <summary>
        /// 当前药理分类对象
        /// </summary>
        public DG_Pharmacology CurrentData { get; set;  }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Dictionary<string, string> QueryCondition { get; set; }

        /// <summary>
        /// 读取药理分类数据源
        /// </summary>
        /// <param name="dt">药理分类数据源</param>
        public void LoadGrid(DataTable dt)
        {
            this.dgFharm.DataSource = dt;
        }

        /// <summary>
        /// 绑定药理分类树节点
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindTree(DataTable dt)
        {
            this.treeP.Nodes.Clear();
            BindTextboxcard(dt);
            NodeBind(this.treeP, dt, "0", null);
            this.treeP.ExpandAll();
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPharmacy()
        {
            InitializeComponent();
        }

        #region 事件
        /// <summary>
        /// 键盘操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.btnSave.Focus();
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            bool flag = false;//标识状态 true：修改 false:新增
            this.txtName.Focus();
            DG_Pharmacology productDic = null;
            if (string.IsNullOrEmpty(this.txtName.Text.Trim()))
            {
                MessageBoxEx.Show("请输入类型名称");
                return;
            }

            if (string.IsNullOrEmpty(nodeTag))
            {
                MessageBoxEx.Show("请选择上级列数据");
                return;
            }

            if (CurrentData != null)
            {
                if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                productDic = CurrentData;
                flag = true;
            }
            else
            {
                productDic = new DG_Pharmacology();
            }

            productDic.ParentID = Convert.ToInt32(nodeTag);
            productDic.PharmName = this.txtName.Text.Trim();
            productDic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(productDic.PharmName);
            productDic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(productDic.PharmName);
            CurrentData = productDic;
            bool isSave = (bool)InvokeController("SavePharmacy");
            DataTable dt = dgFharm.DataSource as DataTable;
            if (isSave)
            {
                if (flag)
                {
                    dt.Rows[rowIndex]["PharmName"] = productDic.PharmName;
                    dt.Rows[rowIndex]["PYCode"] = productDic.PYCode;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["PharmName"] = productDic.PharmName;
                    dr["PYCode"] = productDic.PYCode;
                    dr["PharmID"] = key;
                    dr["PNAME"] = nodeText;
                    dt.Rows.Add(dr);
                }

                SetGridSelectIndex(dgFharm);
            }
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            string nodeString = this.tbcPharm.Text.Trim();
            var nodes = this.treeP.Nodes.Find(nodeString, true);
            if (nodes.Length > 0)
            {
                ExpandNode(nodes[0]);
                this.treeP.SelectedNode = nodes[0];
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmPharmacy_OpenWindowBefore(object sender, System.EventArgs e)
        {
            InvokeController("LoadTree");
            txtName.Text = string.Empty;
            dgFharm.DataSource = null;
            tbcPharm.Text = string.Empty;
            rowIndex = 0;
            nodeTag = string.Empty;
            nodeText = string.Empty;
            key = 0;
            GetGridSelectIndex(dgFharm);
        }

        /// <summary>
        /// 鼠标点击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void treeP_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            QueryCondition = new Dictionary<string, string>();
            nodeTag = e.Node.Tag.ToString();
            nodeText = e.Node.Text;
            rowIndex = 0;
            QueryCondition.Add("ParentID", e.Node.Tag.ToString());
            InvokeController("LoadGrid");
            CurrentData = null;
        }

        /// <summary>
        /// 刷新操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnRfresh_Click(object sender, EventArgs e)
        {
            InvokeController("LoadTree");
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.txtName.Text = string.Empty;
            this.txtName.Focus();
            CurrentData = null;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                if (MessageBox.Show("确定要删除记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }

                InvokeController("DeletePharmacy");
                CurrentData = null;
                if (rowIndex != -1)
                {
                    dgFharm.Rows.RemoveAt(rowIndex);
                }
            }
            else
            {
                MessageBoxEx.Show("请选择记录");
            }
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 按钮操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbcPharm_KeyPress(object sender, KeyPressEventArgs e)
        {
            btnQuery_Click(null, null);
            this.btnQuery.Focus();
        }

        /// <summary>
        /// 点击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgFharm_Click(object sender, EventArgs e)
        {
            if (dgFharm.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgFharm.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgFharm.DataSource;
            DG_Pharmacology pruDic = new DG_Pharmacology();
            pruDic.PharmID = Convert.ToInt32(dt.Rows[rowindex]["PharmID"]);
            pruDic.PharmName = dt.Rows[rowindex]["PharmName"].ToString();
            pruDic.ParentID = Convert.ToInt32(nodeTag);
            CurrentData = pruDic;
            this.txtName.Text = pruDic.PharmName;
        }

        /// <summary>
        /// 获取选中行索引
        /// </summary>
        /// <param name="grids">数据源</param>
        private void GetGridSelectIndex(params DataGridView[] grids)
        {
            foreach (DataGridView grid in grids)
            {
                grid.Click += new EventHandler(delegate (object sender, EventArgs e)
                {
                    if ((sender as DataGridView).CurrentCell != null)
                    {
                        rowIndex = (sender as DataGridView).CurrentCell.RowIndex;
                    }
                    else
                    {
                        rowIndex = -1;
                    }
                });
            }
        }

        /// <summary>
        /// 设置选中行索引
        /// </summary>
        /// <param name="grids">数据源</param>
        private void SetGridSelectIndex(params DataGridView[] grids)
        {
            foreach (DataGridView grid in grids)
            {
                int rowindex = rowIndex;
                int colindex = 0;
                if (rowindex != -1)
                {
                    for (int i = 0; i < grid.Columns.Count; i++)
                    {
                        if (grid.Columns[i].Visible)
                        {
                            colindex = i;
                            break;
                        }
                    }

                    grid.CurrentCell = grid[colindex, rowindex];
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeView">树节点对象</param>
        /// <param name="dt">数据源</param>
        /// <param name="pid">父级ID</param>
        /// <param name="pNode">父节点对象</param>
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
                    tempNode.Text = drv["PharmName"].ToString();
                    tempNode.Name = drv["PharmName"].ToString();
                    tempNode.Tag = drv["PharmID"].ToString();
                    if (parentNode != null)
                    {
                        parentNode.Nodes.Add(tempNode);
                    }
                    else
                    {
                        treeView.Nodes.Add(tempNode);
                    }

                    NodeBind(treeView, dt, drv["PharmID"].ToString(), tempNode);
                }
            }
        }

        /// <summary>
        /// 展开树节点
        /// </summary>
        /// <param name="node">节点对象</param>
        private void ExpandNode(TreeNode node)
        {
            if (node.Parent != null)
            {
                node.Parent.Expand();
                ExpandNode(node.Parent);
            }
        }

        /// <summary>
        /// 绑定药理下拉框
        /// </summary>
        /// <param name="dt">数据源</param>
        public void BindTextboxcard(DataTable dt)
        {
            tbcPharm.DisplayField = "PharmName";
            tbcPharm.MemberField = "PharmID";
            tbcPharm.CardColumn = "PharmName|药理名称|auto,PYCode|拼音码|auto";
            tbcPharm.QueryFieldsString = "PharmName,PYCode";
            tbcPharm.ShowCardWidth = 350;
            tbcPharm.ShowCardDataSource = dt;
        }
        #endregion

        /// <summary>
        /// 改变选中行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgFharm_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgFharm.CurrentCell == null)
            {
                return;
            }

            int rowindex = dgFharm.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgFharm.DataSource;
            DG_Pharmacology pruDic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_Pharmacology>(dt, rowindex);
            CurrentData = pruDic;
            this.txtName.Text = pruDic.PharmName;
        }
    }
}
