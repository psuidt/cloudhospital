using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.BasicData;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 往来科室配置
    /// </summary>
    public partial class FrmMaterialRelateDept : BaseFormBusiness, IFrmMaterialRelateDept
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialRelateDept()
        {
            InitializeComponent();
        }

        #region 自定义属性方法
        /// <summary>
        /// 菜单类型0药房往来科室维护，1药库往来科室维护
        /// </summary>
        private int menuTypeFlag = 0;

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns>科室ID</returns>
        private int LoadData()
        {
            int deptId = Convert.ToInt32(txtc_ds.MemberValue);
            InvokeController("GetRelateDeptData", deptId);//加载往来科室数据
            return deptId;
        }

        /// <summary>
        /// 递归绑定科室树
        /// </summary>
        /// <param name="allLayer">科室级别列表</param>
        /// <param name="pId">父ID</param>
        /// <param name="pNode">父节点</param>
        /// <param name="deptlist">科室列表</param>
        /// <param name="deptId">当前登录科室Id</param>
        private void recursionDeptLayer(List<BaseDeptLayer> allLayer, int pId, TreeNode pNode, List<BaseDept> deptlist, int deptId)
        {
            List<BaseDeptLayer> newLayerlist = allLayer.FindAll(x => x.PId == pId);
            foreach (BaseDeptLayer layer in newLayerlist)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = layer.Name;
                newNode.Tag = layer;
                newNode.ImageIndex = 0;
                pNode.Nodes.Add(newNode);
                loadDeptNode(deptlist, layer.LayerId, newNode, deptId);
                recursionDeptLayer(allLayer, layer.LayerId, newNode, deptlist, deptId);
            }
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="deptlist">科室列表</param>
        /// <param name="layerId">级别ID</param>
        /// <param name="node">节点</param>
        /// <param name="deptId">当前登录科室Id</param>
        private void loadDeptNode(List<BaseDept> deptlist, int layerId, TreeNode node, int deptId)
        {
            List<BaseDept> newDeptlist = deptlist.FindAll(x => x.Layer == layerId);
            foreach (BaseDept val in newDeptlist)
            {
                if (val.DeptId == deptId)
                {
                    continue;
                }

                TreeNode newNode = new TreeNode();
                newNode.Text = val.Name;
                newNode.Tag = val;
                node.Nodes.Add(newNode);
            }
        }

        /// <summary>
        /// 加载树形控件数据
        /// </summary>
        /// <param name="layerList">父节点集</param>
        /// <param name="deptList">科室列表</param>
        /// <param name="deptId">当前登录科室Id</param>
        public void LoadDeptTree(List<BaseDeptLayer> layerList, List<BaseDept> deptList, int deptId)
        {
            treeView1.Nodes.Clear();
            List<BaseDeptLayer> root = layerList.FindAll(x => x.PId == 0);
            if (root.Count <= 0)
            {
                root = layerList.FindAll(x => x.PId > 0);
            }

            foreach (BaseDeptLayer layer in root)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = layer.Name;
                newNode.Tag = layer;
                newNode.ImageIndex = 0;
                treeView1.Nodes.Add(newNode);
                loadDeptNode(deptList, layer.LayerId, newNode, deptId);
                recursionDeptLayer(layerList, layer.LayerId, newNode, deptList, deptId);
            }

            treeView1.ExpandAll();
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }

        /// <summary>
        /// 创建往来关系表结构
        /// </summary>
        /// <returns>返回表</returns>
        private DataTable CreateRelateDeptType()
        {
            DataTable dtData = new DataTable();
            dtData.Columns.Add("ID");
            dtData.Columns.Add("Name");
            DataRow drData;
            drData = dtData.NewRow();
            drData[0] = 0;
            drData[1] = "药库";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 1;
            drData[1] = "药房";
            dtData.Rows.Add(drData);
            drData = dtData.NewRow();
            drData[0] = 2;
            drData[1] = "科室";
            dtData.Rows.Add(drData);
            return dtData;
        }

        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="text">待验证的数据</param>
        public void Nextnodes(TreeNode node, string text)
        {
            foreach (TreeNode tn in node.Nodes)
            {
                //判断节点的名称是否和你的treeview1中显示的Text值相等
                if (tn.Text == text.Trim())
                {
                    treeView1.SelectedNode = tn;
                }
                else
                {
                    tn.BackColor = Color.White;
                }
            }

            foreach (TreeNode tn in node.Nodes)
            {
                Nextnodes(tn, text);  //递归
            }
        }

        /// <summary>
        /// 打开节点
        /// </summary>
        /// <param name="node">节点</param>
        public void ShowNodes(TreeNode node)
        {
            if (node != null)
            {
                node.Expand();
                ShowNodes(node.Parent);  //递归
            }
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="dt">空表</param>
        /// <returns>表结构</returns>
        private DataTable CreateRelateConstruct(DataTable dt)
        {
            dt.Columns.Add("RelationDeptName", Type.GetType("System.String"));
            dt.Columns.Add("RelationDeptTypeName", Type.GetType("System.String"));
            dt.Columns.Add("DrugDeptID", Type.GetType("System.Int32"));
            dt.Columns.Add("RelationDeptID", Type.GetType("System.Int32"));
            return dt;
        }
        #endregion

        #region 接口
        /// <summary>
        /// 绑定科室选项卡
        /// </summary>
        /// <param name="dt">科室数据集</param>
        public void BindDept(DataTable dt)
        {
            txtc_dept.DisplayField = "Name";
            txtc_dept.MemberField = "DeptId";
            txtc_dept.CardColumn = "DeptId|编码|100,Name|科室名称|auto";
            txtc_dept.QueryFieldsString = "Name,Pym,Wbm";
            txtc_dept.ShowCardWidth = 350;
            txtc_dept.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        /// <param name="dt">科室列表数据</param>
        public void BindRelateDeptGrid(DataTable dt)
        {
            dg_deptlist.EndEdit();
            if (dt.Columns.Count <= 0)
            {
                dt = CreateRelateConstruct(dt);
            }

            dg_deptlist.DataSource = dt;
        }

        /// <summary>
        /// 库房选择卡片
        /// </summary>
        /// <param name="dt">库房数据集</param>
        public void BindStoreRoom(DataTable dt)
        {
            txtc_ds.DisplayField = "DeptName";
            txtc_ds.MemberField = "DeptID";
            txtc_ds.CardColumn = "DeptID|编码|100,DeptName|科室名称|auto";
            txtc_ds.QueryFieldsString = "DeptName,Pym,Wbm";
            txtc_ds.ShowCardWidth = 350;
            txtc_ds.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定科室类别
        /// </summary>
        private void BindComboxItems()
        {
            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)dg_deptlist.Columns[1];
            col.Items.Add("药库");
            col.Items.Add("药房");
            col.Items.Add("科室");
        }

        /// <summary>
        /// 绑定往来科室类型数据
        /// </summary>
        public void BindDeptType()
        {
            DataTable dt = CreateRelateDeptType();
            dg_deptlist.SelectionCards[0].BindColumnIndex = 1;
            dg_deptlist.SelectionCards[0].CardColumn = "ID|编码|100,Name|类型名称|auto";
            dg_deptlist.SelectionCards[0].CardSize = new System.Drawing.Size(350, 276);
            dg_deptlist.SelectionCards[0].QueryFieldsString = "ID,Name";
            dg_deptlist.BindSelectionCardDataSource(0, dt);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmRelateDept_OpenWindowBefore(object sender, EventArgs e)
        {
            string menuName = frmName;
            //绑定数据
            InvokeController("GetAllDeptData");//加载科室数据
            InvokeController("GetStoreRoomData");//加载库房列表数据
            //加载科室树数据
            InvokeController("LoadDeptTreeData");
        }

        /// <summary>
        /// 选择物资库房
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="selectedValue">弹出框选定的数据</param>
        private void txtc_ds_AfterSelectedRow(object sender, object selectedValue)
        {
            int deptId = 0;
            DataRow row = (DataRow)selectedValue;
            if (txtc_ds.MemberValue == null)
            {
                deptId = 0;
            }
            else
            {
                deptId = LoadData();
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            //校验
            if (dg_deptlist.Rows.Count <= 0)
            {
                MessageBox.Show("表格中没有需要保存的数据，不需要保存");
                return;
            }
            else
            {
                for (int i = 0; i < dg_deptlist.Rows.Count; i++)
                {
                    if (dg_deptlist.Rows[i].Cells[0].Value.ToString() == string.Empty)
                    {
                        MessageBoxEx.Show("科室不能为空");
                        dg_deptlist.Rows[i].Selected = true;
                        this.dg_deptlist.CurrentCell = this.dg_deptlist.Rows[i].Cells[0];
                        return;
                    }

                    if (dg_deptlist.Rows[i].Cells[1].Value.ToString() == string.Empty)
                    {
                        MessageBoxEx.Show("科室类别不能为空");
                        dg_deptlist.Rows[i].Selected = true;
                        this.dg_deptlist.CurrentCell = this.dg_deptlist.Rows[i].Cells[1];
                        return;
                    }
                }
            }

            try
            {
                DataTable dtSave = (DataTable)dg_deptlist.DataSource;
                InvokeController("BatchSaveRelateDept", dtSave);
                LoadData();
                MessageBox.Show("保存成功");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 定位查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="selectedValue">弹出框选定的数据</param>
        private void txtc_dept_AfterSelectedRow(object sender, object selectedValue)
        {
            string name = txtc_dept.Text;
            //遍历你的 treeView1
            foreach (TreeNode tnc in treeView1.Nodes) 
            {
                Nextnodes(tnc, name); // 这个是你textBox1 中的文本
            }

            treeView1.Focus();
        }

        /// <summary>
        /// 双击库房
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (txtc_ds.MemberValue == null)
            {
                MessageBox.Show("请选择库房");
                return;
            }

            int drugDeptID = Convert.ToInt32(txtc_ds.MemberValue);

            //叶子节点
            if (treeView1.SelectedNode.Nodes.Count == 0)
            {
                BaseDept dept = treeView1.SelectedNode.Tag as BaseDept;
                if (dept == null)
                {
                    MessageBoxEx.Show("您选择的节点不是科室节点而是科室分类，请查看科室维护界面");
                    return;
                }

                int relationDeptID = dept.DeptId;
                string relationDeptName = dept.Name;
                DataTable relateDeptTable = (DataTable)dg_deptlist.DataSource;
                if (relateDeptTable.Columns.Count == 0)
                {
                    relateDeptTable = CreateRelateConstruct(relateDeptTable);
                }

                if (dg_deptlist.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dg_deptlist.Rows)
                    {
                        if (row.Cells["relationDeptID"].Value.ToString() == relationDeptID.ToString())
                        {
                            MessageBox.Show("您已经添加了【" + relationDeptName + "】");
                            return;
                        }
                    }
                }

                int index = this.dg_deptlist.AddRow();
                dg_deptlist.SetRowColor(index, Color.Blue, true);
                this.dg_deptlist.Rows[index].Cells[0].Value = relationDeptName;
                this.dg_deptlist.Rows[index].Cells[1].Value = "药库";
                this.dg_deptlist.Rows[index].Cells[2].Value = drugDeptID;
                this.dg_deptlist.Rows[index].Cells[3].Value = relationDeptID;
            }
            else
            {
                MessageBox.Show("请选择叶子节点");
            }
        }

        /// <summary>
        /// 科室类别弹出网格选定数据
        /// </summary>
        /// <param name="selectedValue">选定的数据</param>
        /// <param name="stop">Stop标志</param>
        /// <param name="customNextColumnIndex">下一个获得焦点的列</param>
        private void dg_deptlist_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            try
            {
                DataRow row = (DataRow)selectedValue;
                int colid = this.dg_deptlist.CurrentCell.ColumnIndex;
                int rowid = this.dg_deptlist.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dg_deptlist.DataSource;
                dt.Rows[rowid]["RelationDeptTypeName"] = row.ItemArray[1];
                stop = true;

                dg_deptlist.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btn_del_Click(object sender, EventArgs e)
        {
            try
            {
                if (dg_deptlist.Rows.Count <= 0)
                {
                    MessageBox.Show("列表中没有数据，不能进行删除操作");
                    return;
                }

                DataGridViewRow gridRow = dg_deptlist.CurrentRow;
                if (gridRow == null)
                {
                    MessageBox.Show("请选择一条科室数据");
                    return;
                }

                if (MessageBox.Show("您确定要删除该科室么？", "删除确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    return;
                }

                int lastRowIndex = this.dg_deptlist.CurrentCell.RowIndex;
                if (gridRow.Cells["DrugDeptID"].Value == DBNull.Value)
                {
                    dg_deptlist.Rows.Remove(gridRow);
                    return;
                }

                int drugDeptID = Convert.ToInt32(gridRow.Cells["DrugDeptID"].Value);
                int relationDeptID = Convert.ToInt32(gridRow.Cells["relationDeptID"].Value);
                InvokeController("DeleteRelateDept", drugDeptID, relationDeptID);
                dg_deptlist.Rows.RemoveAt(lastRowIndex);
                if (lastRowIndex != 0)
                {
                    dg_deptlist.CurrentCell = dg_deptlist[dg_deptlist.CurrentCell.ColumnIndex, lastRowIndex - 1];
                }

                MessageBox.Show("删除成功");
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="colIndex">列</param>
        /// <param name="rowIndex">行</param>
        /// <param name="jumpStop">是否自动跳到下一个框</param>
        private void dg_deptlist_DataGridViewCellPressEnterKey(object sender, int colIndex, int rowIndex, ref bool jumpStop)
        {
            if (colIndex == 1)
            {
                jumpStop = true;
            }
        }
        #endregion
    }
}