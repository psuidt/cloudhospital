using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 费用模板
    /// </summary>
    public partial class FrmFeeTemplate : BaseFormBusiness, IFrmFeeTemplate
    {
        #region 属性
        /// <summary>
        /// 当前ShowCard数据集
        /// </summary>
        private DataTable dTShowCard;

        /// <summary>
        /// 当然网格数据集
        /// </summary>
        private DataTable currentDt;

        /// <summary>
        /// 树集合
        /// </summary>
        private AdvTree useTree;

        /// <summary>
        /// 开方医生
        /// </summary>
        public string StrDocName { get; set; }

        /// <summary>
        /// 模板类型
        /// </summary>
        public string MouldType { get; set; }

        /// <summary>
        /// 开方科室
        /// </summary>
        public string StrDeptName { get; set; }

        /// <summary>
        /// 使用的树
        /// </summary>
        public AdvTree UseTree
        {
            get { return useTree; }
            set { useTree = value; }
        }

        /// <summary>
        /// 模板类别
        /// </summary>
        private int treeLevel;

        /// <summary>
        /// 模板类别
        /// </summary>
        public int TreeLevel
        {
            get { return treeLevel; }
            set { treeLevel = value; }
        }

        /// <summary>
        /// 当前选中树节点
        /// </summary>
        public NodeCollection WestDrugNode
        {
            get
            {
                if (treWestDrug.SelectedNodes == null)
                {
                    return treWestDrug.Nodes;
                }
                else
                {
                    return treWestDrug.SelectedNodes;
                }
            }

            set
            {
                if (treWestDrug.SelectedNodes == null)
                {
                    value = treWestDrug.Nodes;
                }
                else
                {
                    value = treWestDrug.SelectedNodes;
                }
            }
        }

        /// <summary>
        /// 费用模板树选中节点
        /// </summary>
        private Node selectWestDrugNode;

        /// <summary>
        /// 费用模板树选中节点
        /// </summary>
        public Node SelectWestDrugNode
        {
            get { return treWestDrug.SelectedNode; }
            set { selectWestDrugNode = value; }
        }

        /// <summary>
        /// 模板级别
        /// </summary>
        private int intModilLevel;

        /// <summary>
        /// 模板级别
        /// </summary>
        public int IntModilLevel
        {
            get { return intModilLevel; }
            set { intModilLevel = value; }
        }

        /// <summary>
        /// 模板头列表信息
        /// </summary>
        private List<OPD_PresMouldHead> listHead;

        /// <summary>
        /// 模板头列表信息
        /// </summary>
        public List<OPD_PresMouldHead> ListHead
        {
            get { return listHead; }
            set { listHead = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmFeeTemplate()
        {
            InitializeComponent();
        }

        #region 函数
        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="presType">类型</param>
        /// <param name="tree">当前树对象</param>
        private void GetPresTemplate(int intLevel, int presType, AdvTree tree)
        {
            ListHead = (List<OPD_PresMouldHead>)InvokeController("GetPresTemplate", intLevel, presType);

            tree.Nodes.Clear();
            LoadTree(tree, ListHead, "99999", null);
            if (tree.Nodes.Count > 0)
            {
                tree.SelectedNode = tree.Nodes[0];
            }

            tree.ExpandAll();
        }

        /// <summary>
        /// 设置网格编辑
        /// </summary>
        /// <param name="type">当前是否可编辑</param>
        private void SetGridEdit(int type)
        {
            switch (type)
            {
                case 0:
                    dgFee.Columns["ItemID"].ReadOnly = false;
                    dgFee.Columns["PresAmount"].ReadOnly = false;
                    break;
                case 1:
                    dgFee.Columns["ItemID"].ReadOnly = true;
                    dgFee.Columns["PresAmount"].ReadOnly = true;
                    break;
            }
        }

        /// <summary>
        /// 设置按钮编辑
        /// </summary>
        /// <param name="type">当前是否可编辑</param>
        private void SetActionEdit(int type)
        {
            switch (type)
            {
                case 0:
                    btnAdd.Enabled = true;
                    btnDel.Enabled = true;
                    btnFresh.Enabled = true;
                    btnSave.Enabled = true;
                    btnUpdate.Enabled = true;
                    break;
                case 1:
                    btnAdd.Enabled = false;
                    btnDel.Enabled = false;
                    btnFresh.Enabled = false;
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 递归遍历加载树节点
        /// </summary>
        /// <param name="treeView">树控件</param>
        /// <param name="list">处方头列表</param>
        /// <param name="pid">父id</param>
        /// <param name="pNode">节点</param>
        private void LoadTree(AdvTree treeView, List<OPD_PresMouldHead> list, string pid, Node pNode)
        {
            string sFilter = "PID=" + pid;
            Node parentNode = pNode;
            List<OPD_PresMouldHead> templist = list.Where(item => item.PID == Convert.ToInt32(pid)).ToList();
            foreach (OPD_PresMouldHead bd in templist)
            {
                Node tempNode = new Node();

                tempNode.Text = bd.ModuldName;
                tempNode.Name = bd.PresMouldHeadID.ToString();
                tempNode.AccessibleDescription = bd.MouldType.ToString();  //模板类型
                if (bd.MouldType == 0)
                {
                    tempNode.ImageIndex = 0;
                }
                else
                {
                    tempNode.ImageIndex = 1;
                }

                if (parentNode != null)
                {
                    parentNode.Nodes.Add(tempNode);
                }
                else
                {
                    treeView.Nodes.Add(tempNode);
                }

                LoadTree(treeView, list, bd.PresMouldHeadID.ToString(), tempNode);
            }
        }

        /// <summary>
        /// 新增节点
        /// </summary>
        /// <param name="node">树节点</param>
        /// <param name="tree">树控件</param>
        public void AddNode(Node node, AdvTree tree)
        {
            if (node.AccessibleDescription == "0")
            {
                node.ImageIndex = 0;
            }
            else
            {
                node.ImageIndex = 1;
            }

            if (SelectWestDrugNode != null)
            {
                if (TreeLevel == 1)
                {
                    tree.SelectedNode.Nodes.Add(node);
                }
                else
                {
                    tree.SelectedNode.Parent.Nodes.Add(node);
                }
            }

            tree.SelectedNode = node;
        }

        /// <summary>
        /// 修改树节点的名称
        /// </summary>
        /// <param name="nodeText">节点名称</param>
        /// <param name="tree">树控件</param>
        public void EditNode(string nodeText, AdvTree tree)
        {
            tree.SelectedNode.Text = nodeText;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="tree">树控件</param>
        private void DeleteNode(AdvTree tree)
        {
            if (tree.SelectedNode != null)
            {
                int iTemp = tree.SelectedNode.Index;
                tree.SelectedNode.Remove();
                tree.SelectedIndex = iTemp + 1;
            }
        }

        /// <summary>
        /// 绑定录入ShowCard
        /// </summary>
        /// <param name="dtFeeInfo">费用联动信息</param>
        public void BindFeeInfoCard(DataTable dtFeeInfo)
        {
            dTShowCard = dtFeeInfo;
            dgFee.SelectionCards[0].BindColumnIndex = 0;
            dgFee.SelectionCards[0].CardColumn = "ItemID|编码|55,ItemName|项目名称|auto,MiniUnitName|单位|40,UnitPrice|价格|50";
            dgFee.SelectionCards[0].CardSize = new System.Drawing.Size(360, 200);
            dgFee.SelectionCards[0].QueryFieldsString = "ItemName,Pym,Wbm";
            dgFee.SelectionCards[0].SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByFirstChar;
            dgFee.BindSelectionCardDataSource(0, dtFeeInfo);
        }

        /// <summary>
        /// 绑定费用模板信息
        /// </summary>
        /// <param name="dtFee">费用模板数据源</param>
        public void BindDgFee(DataTable dtFee)
        {
            currentDt = dtFee;
            dgFee.SelectCardRowSelected -= dgFee_SelectCardRowSelected;
            dgFee.CellValueChanged -= dgFee_CellValueChanged;
            dgFee.DataSource = dtFee;
            dgFee.SelectCardRowSelected += dgFee_SelectCardRowSelected;
            dgFee.CellValueChanged += dgFee_CellValueChanged;
            dgFee.EndEdit();
        }

        /// <summary>
        /// 检查网格
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="checkRows">检查行(为空表全部)</param>
        /// <param name="uniqueCol">唯一列名(为空则没有主键)</param>
        /// <param name="errMsg">错误信息</param>
        /// <param name="errCol">错误列名</param>
        /// <param name="errRow">错误行索引</param>
        /// <returns>false存在不符合要求数据</returns>
        public bool CheckDetails(DataTable dtSource, List<int> checkRows, string[] uniqueCol, out string errMsg, out string errCol, out int errRow)
        {
            errMsg = string.Empty;
            errCol = string.Empty;
            errRow = -1;
            for (int index = 0; index < dtSource.Rows.Count; index++)
            {
                if (checkRows != null &&
                    checkRows.FindIndex((x) => { return x == index; }) < 0)
                {
                    continue;
                }

                DataRow dRow = dtSource.Rows[index];
                //重复性检查
                if (uniqueCol != null)
                {
                    string colName = string.Empty;
                    for (int temp = index + 1; temp < dtSource.Rows.Count; temp++)
                    {
                        bool isUnique = false;
                        foreach (string name in uniqueCol)
                        {
                            errCol = name;
                            colName += (name + ",");
                            if (dRow[name].ToString() != dtSource.Rows[temp][name].ToString())
                            {
                                isUnique = true;
                                break;
                            }
                        }

                        if (!isUnique)
                        {
                            errRow = temp;
                            errMsg = "【{0}】不允许重复，请重新录入";
                            return false;
                        }
                    }
                }

                //按每列对正则表达式判断
                for (int count = 0; count < dtSource.Columns.Count; count++)
                {
                    object key = "Regex";
                    if (dtSource.Columns[count].ExtendedProperties.Contains(key))
                    {
                        string express = dtSource.Columns[count].ExtendedProperties[key].ToString();
                        if (express != string.Empty)
                        {
                            if (Regex.IsMatch(dRow[count].ToString(), express))
                            {
                                continue;
                            }
                            else
                            {
                                errMsg = "【{0}】的录入数据格式不正确，请重新录入";
                                errCol = dtSource.Columns[count].ColumnName;
                                errRow = index;
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        #region 窗体事件
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void advWestDrugLevel_Click(object sender, EventArgs e)
        {
            IntModilLevel = Convert.ToInt32(advWestDrugLevel.SelectedNode.Tag);
            GetPresTemplate(IntModilLevel, 2, treWestDrug);
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            btnFresh.Enabled = false;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
        }

        /// <summary>
        /// 菜单打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmTree_Opening(object sender, CancelEventArgs e)
        {
            if (treWestDrug.SelectedNode == null)
            {
                return;
            }

            if (treWestDrug.SelectedNode.Text == "全部模板")
            {
                itemNew.Enabled = false;
                itemDelete.Enabled = false;
                itemEdit.Enabled = false;
            }
            else
            {
                itemNew.Enabled = true;
                itemDelete.Enabled = true;
                itemEdit.Enabled = true;
            }

            if (treWestDrug.SelectedNode.AccessibleDescription == "1")
            {
                itemNewSub.Enabled = false;
            }
            else
            {
                itemNewSub.Enabled = true;
            }
        }

        /// <summary>
        /// 新增节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemNew_Click(object sender, EventArgs e)
        {
            OPD_PresMouldHead tempHead = new OPD_PresMouldHead();
            if (treWestDrug.SelectedNode == null)
            {
                tempHead.PID = 0;
                WestDrugNode = treWestDrug.Nodes;
            }
            else
            {
                OPD_PresMouldHead fartherHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
                tempHead.PID = fartherHead.PID;
                WestDrugNode = treWestDrug.SelectedNodes;
            }

            UseTree = treWestDrug;
            TreeLevel = 0;  //新增同级节点
            tempHead.PresType = 2; //费用模板
            tempHead.ModulLevel = IntModilLevel; //模板级别
            InvokeController("PopInfoWindow", 1, tempHead);
        }

        /// <summary>
        /// 添加子节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemNewSub_Click(object sender, EventArgs e)
        {
            OPD_PresMouldHead tempHead = new OPD_PresMouldHead();
            if (treWestDrug.SelectedNode != null)
            {
                OPD_PresMouldHead fartherHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
                tempHead.PID = fartherHead.PresMouldHeadID;
                WestDrugNode = treWestDrug.SelectedNodes;
                UseTree = treWestDrug;
                TreeLevel = 1;  //新增子节点
                tempHead.PresType = 2; //费用模板
                tempHead.ModulLevel = IntModilLevel; //模板级别
                InvokeController("PopInfoWindow", 1, tempHead);
            }
        }

        /// <summary>
        /// 编辑节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemEdit_Click(object sender, EventArgs e)
        {
            if (treWestDrug.SelectedNode == null)
            {
                return;
            }

            OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
            if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
            {
                if (treWestDrug.SelectedNode == null)
                {
                    return;
                }

                UseTree = treWestDrug;
                OPD_PresMouldHead tempHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));

                InvokeController("PopInfoWindow", 2, tempHead);
            }
            else
            {
                MessageBoxEx.Show("只有模板的创建者才能修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 删除节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("删除已选择的模板分类或处方，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (treWestDrug.SelectedNode.Nodes.Count > 0)
            {
                MessageBoxEx.Show("该节点下还存在子节点，不能直接删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treWestDrug.SelectedNode != null)
            {
                OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
                if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
                {
                    ListHead.Remove(delrHead);
                    InvokeController("DeleteMoudelInfo", delrHead);
                    DeleteNode(treWestDrug);
                }
                else
                {
                    MessageBoxEx.Show("只有模板的创建者才能删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void treWestDrug_Click(object sender, EventArgs e)
        {
            treWestDrug.ContextMenu = null;
        }

        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void treWestDrug_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            treWestDrug.ContextMenuStrip = cmTree;
            Node node = treWestDrug.SelectedNode;
            if (node != null)
            {
                OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(node.Name));
                if (delrHead.MouldType == 0)
                {
                    lbMouldName.Text = string.Empty;
                    SetActionEdit(1);
                }
                else
                {
                    lbMouldName.Text = node.Text;
                    if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
                    {
                        SetActionEdit(0);
                    }
                    else
                    {
                        SetActionEdit(1);
                    }
                }

                InvokeController("LoadFee", node.Name);
                InvokeController("LoadHead", node.Name);
            }
        }

        /// <summary>
        /// 打开窗体前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFeeTemplate_OpenWindowBefore(object sender, EventArgs e)
        {
            advWestDrugLevel.SelectedNode = ndPeople;

            GetPresTemplate(2, 2, treWestDrug);

            InvokeController("LoadFeeInfoCard");
            btnAdd.Enabled = false;
            btnDel.Enabled = false;
            btnFresh.Enabled = false;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
        }

        /// <summary>
        /// 添加处方事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SetGridEdit(0);
                int rowindex = dgFee.AddRow();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgFee.CurrentCell != null)
            {
                int rowid = this.dgFee.CurrentCell.RowIndex;
                DataTable dt = (DataTable)dgFee.DataSource;
                if (!string.IsNullOrEmpty(dt.Rows[rowid]["PresMouldDetailID"].ToString()))
                {
                    if (MessageBoxEx.Show("删除后无法恢复，确定删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        InvokeController("DelDetail", dt.Rows[rowid]["PresMouldDetailID"].ToString());
                        InvokeController("LoadFee", treWestDrug.SelectedNode.Name);
                    }
                }
                else
                {
                    dt.Rows.RemoveAt(rowid);
                }
            }
            else
            {
                MessageBoxEx.Show("没有可以删除的数据");
            }
        }

        /// <summary>
        /// 行选中事件
        /// </summary>
        /// <param name="selectedValue">选中值</param>
        /// <param name="stop">停止标识</param>
        /// <param name="customNextColumnIndex">下个列索引</param>
        private void dgFee_SelectCardRowSelected(object selectedValue, ref bool stop, ref int customNextColumnIndex)
        {
            DataRow selectRow = (DataRow)selectedValue;
            int colId = dgFee.CurrentCell.ColumnIndex;
            int rowId = dgFee.CurrentCell.RowIndex;
            DataTable dtSource = (DataTable)dgFee.DataSource;
            if (customNextColumnIndex == 0)
            {
                dtSource.Rows[rowId]["ItemID"] = selectRow["ItemID"];
                dtSource.Rows[rowId]["ItemName"] = selectRow["ItemName"];
                dtSource.Rows[rowId]["Price"] = selectRow["UnitPrice"];
                dtSource.Rows[rowId]["PresAmount"] = "1";
                dtSource.Rows[rowId]["PresAmountUnit"] = selectRow["MiniUnitName"];
                dtSource.Rows[rowId]["ItemMoney"] = selectRow["UnitPrice"];
                dtSource.Rows[rowId]["DocName"] = StrDocName;
            }
            dgFee.Refresh();
        }

        /// <summary>
        /// 单元格值改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void dgFee_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //数量列
            if (e.ColumnIndex == 6)
            {
                DataRow currentRow = ((DataTable)dgFee.DataSource).Rows[e.RowIndex];
                if (currentRow["PresAmount"].ToString() == string.Empty || currentRow["PresAmount"].ToString() == "0")
                {
                    currentRow["PresAmount"] = "1";
                }

                currentRow["ItemMoney"] = Convert.ToDecimal(currentRow["PresAmount"]) * Convert.ToDecimal(currentRow["Price"]);
            }
        }

        /// <summary>
        /// 刷新事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnFresh_Click(object sender, EventArgs e)
        {
            if (treWestDrug.SelectedNode != null)
            {
                InvokeController("LoadFee", treWestDrug.SelectedNode.Name);
            }
        }

        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dgFee.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBoxEx.Show("没有可保存的数据");
                    return;
                }

                List<OPD_PresMouldDetail> mouldList = new List<OPD_PresMouldDetail>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ItemID"] == DBNull.Value)
                    {
                        continue;
                    }

                    DataRow dr = dTShowCard.Select("ItemID=" + dt.Rows[i]["ItemID"].ToString() ).FirstOrDefault();
                    OPD_PresMouldDetail mouldDetatil = new OPD_PresMouldDetail();
                    if (!string.IsNullOrEmpty(dt.Rows[i]["PresMouldDetailID"].ToString()))
                    {
                        mouldDetatil.PresMouldDetailID = Convert.ToInt32(dt.Rows[i]["PresMouldDetailID"].ToString());
                    }

                    mouldDetatil.ChannelID = 0;
                    mouldDetatil.ChargeAmount = Convert.ToDecimal(dt.Rows[i]["PresAmount"]);
                    mouldDetatil.ChargeUnit = dt.Rows[i]["PresAmountUnit"].ToString();
                    mouldDetatil.StatID = 1;
                    mouldDetatil.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    mouldDetatil.PresNO = 1;
                    mouldDetatil.PresMouldHeadID = Convert.ToInt32(treWestDrug.SelectedNode.Name);
                    mouldDetatil.PresFactor = 1;
                    mouldDetatil.PresAmountUnit = dt.Rows[i]["PresAmountUnit"].ToString();
                    mouldDetatil.PresAmount = Convert.ToDecimal(dt.Rows[i]["PresAmount"]);
                    mouldDetatil.GroupID = 1;
                    mouldDetatil.GroupSortNO = 1;
                    mouldDetatil.ItemID = Convert.ToInt32(dt.Rows[i]["ItemID"].ToString());
                    mouldDetatil.ItemName = dt.Rows[i]["ItemName"].ToString();
                    mouldDetatil.Days = 1;
                    mouldDetatil.DosageUnit = dt.Rows[i]["PresAmountUnit"].ToString();
                    mouldDetatil.DoseNum = 0;
                    mouldDetatil.Dosage = Convert.ToDecimal(dt.Rows[i]["PresAmount"]);
                    mouldDetatil.Spec = string.Empty;
                    if (dr != null)
                    {
                        mouldDetatil.ExecDeptID = Convert.ToInt32(dr["ExecDeptId"]);
                    }

                    mouldList.Add(mouldDetatil);
                }

                InvokeController("SaveDetail", mouldList);
                InvokeController("LoadFee", treWestDrug.SelectedNode.Name);
                SetGridEdit(1);
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgFee == null)
            {
                return;
            }

            if (dgFee.CurrentCell != null)
            {
                int rowId = dgFee.CurrentCell.RowIndex;
                SetGridEdit(0);
            }
            else
            {
                MessageBoxEx.Show("当前没有可修改数据");
            }
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
        #endregion
    }
}
