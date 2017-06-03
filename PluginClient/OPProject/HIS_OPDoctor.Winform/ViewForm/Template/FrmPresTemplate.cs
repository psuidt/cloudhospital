using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.Controller;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 处方模板
    /// </summary>
    public partial class FrmPresTemplate : BaseFormBusiness, IFrmPresTemplate
    {
        #region 模板属性
        /// <summary>
        /// 使用的树控件
        /// </summary>
        private AdvTree useTree;

        /// <summary>
        /// 使用的树控件
        /// </summary>
        public AdvTree UseTree
        {
            get { return useTree; }
            set { useTree = value; }
        }

        /// <summary>
        /// 模板级别
        /// </summary>
        private int treeLevel;

        /// <summary>
        /// 模板级别
        /// </summary>
        public int TreeLevel
        {
            get { return treeLevel; }
            set { treeLevel = value; }
        }

        /// <summary>
        /// 西药处方树节点集合
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
        /// 西药模板树选中节点
        /// </summary>
        private Node selectWestDrugNode;

        /// <summary>
        /// 西药模板树选中节点
        /// </summary>
        public Node SelectWestDrugNode
        {
            get
            {
                return treWestDrug.SelectedNode;
            }

            set
            {
                selectWestDrugNode = value;
            }
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
            get
            {
                return intModilLevel;
            }

            set
            {
                intModilLevel = value;
            }
        }

        /// <summary>
        /// 模板头列表类
        /// </summary>
        private List<OPD_PresMouldHead> listHead;

        /// <summary>
        /// 模板头列表类
        /// </summary>
        public List<OPD_PresMouldHead> ListHead
        {
            get
            {
                return listHead;
            }

            set
            {
                listHead = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmPresTemplate()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// 窗体打开前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmPresTemplate_OpenWindowBefore(object sender, EventArgs e)
        {
            TemplateName.Text = string.Empty;
            MidTemplateName.Text = string.Empty;

            //树控件初始化
            advWestDrugLevel.SelectedNode = ndPeople;
            GetPresTemplate(2, 0, treWestDrug);

            cmbExecStoreRoom.SelectedIndexChanged -= new EventHandler(cmbExecStoreRoom_SelectedIndexChanged);
            cbbMidDrugRoom.SelectedIndexChanged -= new EventHandler(cbbMidDrugRoom_SelectedIndexChanged);
            //初始化药房执行科室下拉框，默认为西药处方执行科室      
            DataTable dt = (DataTable)InvokeController("GetDrugStoreRoom",0);
            DataTable dtMidDrug = (DataTable)InvokeController("GetDrugStoreRoom", 1);
            BindDrugStoreRoom(dt, dtMidDrug);

            //初始化系统参数
            InvokeController("GetSystemParameter");

            //初始化处方控件
            int presCount = Convert.ToInt32(InvokeController("GetPresCount"));
            int deptid = Convert.ToInt32(cmbExecStoreRoom.SelectedValue);
            int midDrugDeptid = Convert.ToInt32(cbbMidDrugRoom.SelectedValue);
            PresTemplateDbHelper presHelper = new Controller.PresTemplateDbHelper(deptid);
            WestPresControl.InitDbHelper(presHelper);
            WestPresControl.PresCount = presCount;
            WestPresControl.IsShowFootText = false;
            WestPresControl.Enabled = false;

            PresTemplateDbHelper midPresHelper = new Controller.PresTemplateDbHelper(midDrugDeptid);
            MidDrugPresControl.InitDbHelper(midPresHelper);
            MidDrugPresControl.IsShowFootText = false;
            cmbExecStoreRoom.SelectedIndexChanged += new EventHandler(cmbExecStoreRoom_SelectedIndexChanged);
            cbbMidDrugRoom.SelectedIndexChanged += new EventHandler(cbbMidDrugRoom_SelectedIndexChanged);
        }

        /// <summary>
        /// 索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmbExecStoreRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deptid = Convert.ToInt32(cmbExecStoreRoom.SelectedValue);
            PresTemplateDbHelper presHelper = new Controller.PresTemplateDbHelper(deptid);
            WestPresControl.InitDbHelper(presHelper);
        }  

        /// <summary>
        /// 绑定药房科室
        /// </summary>
        /// <param name="dt">药房数据集</param>
        /// <param name="dtMidDrug">中药房数据集</param>
        public void BindDrugStoreRoom(DataTable dt,DataTable dtMidDrug)
        {
            if (dt != null)
            {
                cmbExecStoreRoom.DisplayMember = "DeptName";
                cmbExecStoreRoom.ValueMember = "DeptID";
                cmbExecStoreRoom.DataSource = dt;
                cmbExecStoreRoom.SelectedIndex = 0;
            }

            cbbMidDrugRoom.DisplayMember = "DeptName";
            cbbMidDrugRoom.ValueMember = "DeptID";
            cbbMidDrugRoom.DataSource = dtMidDrug;
            cbbMidDrugRoom.SelectedIndex = 0;
        }

        #region 模板操作
        /// <summary>
        /// 递归遍历加载树节点
        /// </summary>
        /// <param name="treeView">树控件</param>
        /// <param name="list">处方列表</param>
        /// <param name="pid">父id</param>
        /// <param name="pNode">树节点</param>
        public void LoadTree(AdvTree treeView, List<OPD_PresMouldHead> list, string pid, Node pNode)
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
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbHospital_Click(object sender, EventArgs e)
        {
            IntModilLevel = 0;
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbDept_Click(object sender, EventArgs e)
        {
            GetPresTemplate(1, 0, treWestDrug);
        }

        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="presType">处方类型</param>
        /// <param name="tree">树控件</param>
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
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void panelEx5_Click(object sender, EventArgs e)
        {
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
            tempHead.PresType = 0; //西药模板
            tempHead.ModulLevel = IntModilLevel; //模板级别
            InvokeController("PopInfoWindow", 1, tempHead);
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
        /// 添加节点
        /// </summary>
        /// <param name="node">节点</param>
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
            if (tree.Name == "treWestDrug")
            {
                OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
                if ((treWestDrug.SelectedNode.AccessibleDescription == "1") && (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId))
                {
                    WestPresControl.Enabled = true;
                    TemplateName.Text = node.Text;
                }
                else
                {
                    TemplateName.Text = string.Empty;
                    WestPresControl.Enabled = false;
                }

                WestPresControl.LoadPatData(Convert.ToInt32(node.Name), 99999, "xxx", 99999, "xxxx");
            }
            else
            {
                OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
                if ((treMidDrug.SelectedNode.AccessibleDescription == "1") && (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId))
                {
                    MidDrugPresControl.Enabled = true;
                    MidTemplateName.Text = node.Text;
                }
                else
                {
                    MidDrugPresControl.Enabled = false;
                    MidTemplateName.Text = string.Empty;
                }

                MidDrugPresControl.LoadPatData(Convert.ToInt32(node.Name), 99999, "xxx", 99999, "xxxx");
            }
        }

        /// <summary>
        /// 修改树节点的名称
        /// </summary>
        /// <param name="nodeText">名称</param>
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
        /// 树点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void advWestDrugLevel_Click(object sender, EventArgs e)
        {
            switch (advWestDrugLevel.SelectedNode.Text)
            {
                case "全院模板":
                    IntModilLevel = 0;
                    break;
                case "科室模板":
                    IntModilLevel = 1;
                    break;
                case "个人模板":
                    IntModilLevel = 2;
                    break;
            }

            WestPresControl.Enabled = false;
            GetPresTemplate(IntModilLevel, 0, treWestDrug);
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
                tempHead.PresType = 0; //西药模板
                tempHead.ModulLevel = IntModilLevel; //模板级别
                InvokeController("PopInfoWindow", 1, tempHead);
            }
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

            cmTree.PointToScreen(MousePosition);

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
        /// 删除事件
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
        /// 选中选项卡改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tabMoudel_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (tabMoudel.SelectedTab.Name == "stWestDrug")
            {
                advWestDrugLevel.SelectedNode = ndPeople;
                GetPresTemplate(2, 0, treWestDrug);
            }
            else
            {
                advMidDrug.SelectedNode = node7;
                GetPresTemplate(2, 1, treMidDrug);
            }
        }

        #region 中药处方模板
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void advMidDrug_Click(object sender, EventArgs e)
        {
            if (advMidDrug.SelectedNode == null)
            {
                return;
            }

            switch (advMidDrug.SelectedNode.Text)
            {
                case "全院模板":
                    IntModilLevel = 0;
                    break;
                case "科室模板":
                    IntModilLevel = 1;
                    break;
                case "个人模板":
                    IntModilLevel = 2;
                    break;
            }

            MidDrugPresControl.Enabled = false;
            GetPresTemplate(IntModilLevel, 1, treMidDrug);
        }

        /// <summary>
        /// 重要删除事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemMidDelete_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("删除已选择的模板分类或处方，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (treMidDrug.SelectedNode.Nodes.Count > 0)
            {
                MessageBoxEx.Show("该节点下还存在子节点，不能直接删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (treMidDrug.SelectedNode != null)
            {
                if (treMidDrug.SelectedNode.Nodes.Count > 0)
                {
                    return;
                }

                OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
                if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
                {
                    ListHead.Remove(delrHead);
                    InvokeController("DeleteMoudelInfo", delrHead);
                    DeleteNode(treMidDrug);
                }
                else
                {
                    MessageBoxEx.Show("只有模板的创建者才能删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                
            }
        }

        /// <summary>
        /// 新增事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemMidNew_Click(object sender, EventArgs e)
        {
            OPD_PresMouldHead tempHead = new OPD_PresMouldHead();
            if (treMidDrug.SelectedNode == null)
            {
                tempHead.PID = 0;
                WestDrugNode = treMidDrug.Nodes;
            }
            else
            {
                OPD_PresMouldHead fartherHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
                tempHead.PID = fartherHead.PID;
                WestDrugNode = treMidDrug.SelectedNodes;
            }

            UseTree = treMidDrug;
            TreeLevel = 0;  //新增同级节点
            tempHead.PresType = 1; //中药模板
            tempHead.ModulLevel = IntModilLevel; //模板级别
            InvokeController("PopInfoWindow", 1, tempHead);
        }

        /// <summary>
        /// 新增子节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemMidNewSub_Click(object sender, EventArgs e)
        {
            OPD_PresMouldHead tempHead = new OPD_PresMouldHead();
            if (treMidDrug.SelectedNode != null)
            {
                OPD_PresMouldHead fartherHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
                tempHead.PID = fartherHead.PresMouldHeadID;
                WestDrugNode = treMidDrug.SelectedNodes;
                UseTree = treMidDrug;
                TreeLevel = 1;  //新增子节点
                tempHead.PresType = 1; //中药模板
                tempHead.ModulLevel = IntModilLevel; //模板级别
                InvokeController("PopInfoWindow", 1, tempHead);
            }
        }

        /// <summary>
        /// 中药菜单打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void MenuMid_Opening(object sender, CancelEventArgs e)
        {
            if (treMidDrug.SelectedNode == null)
            {
                return;
            }

            if (treMidDrug.SelectedNode.Text == "全部模板")
            {
                itemMidNew.Enabled = false;
                itemMidDelete.Enabled = false;
                itemEditMidDrug.Enabled = false;
            }
            else
            {
                itemMidNew.Enabled = true;
                itemMidDelete.Enabled = true;
                itemEditMidDrug.Enabled = true;
            }

            if (treMidDrug.SelectedNode.AccessibleDescription == "1")
            {
                itemMidNewSub.Enabled = false;
            }
            else
            {
                itemMidNewSub.Enabled = true;
            }
        }

        /// <summary>
        /// 编辑节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemEditMidDrug_Click(object sender, EventArgs e)
        {
            if (treMidDrug.SelectedNode == null)
            {
                return;
            }

            OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
            if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
            {
                UseTree = treMidDrug;

                OPD_PresMouldHead tempHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));

                InvokeController("PopInfoWindow", 2, tempHead);
            }
            else
            {
                MessageBoxEx.Show("只有模板的创建者才能修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        #endregion

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void treWestDrug_Click(object sender, EventArgs e)
        {
            //treWestDrug.ContextMenu = null;
        }

        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void treWestDrug_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treWestDrug.SelectedNode.Name));
            if ((treWestDrug.SelectedNode.AccessibleDescription=="1") && (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId))
            {
                WestPresControl.Enabled = true;
                TemplateName.Text = e.Node.Text;
            }
            else
            {
                TemplateName.Text = string.Empty;
                WestPresControl.Enabled = false;
            }   
                   
            WestPresControl.LoadPatData(Convert.ToInt32(e.Node.Name), 99999, "xxx", 99999, "xxxx");
        }

        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void treMidDrug_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            OPD_PresMouldHead delrHead = ListHead.Find((OPD_PresMouldHead head) => head.PresMouldHeadID == Convert.ToInt32(treMidDrug.SelectedNode.Name));
            if ((treMidDrug.SelectedNode.AccessibleDescription == "1") && (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId))
            {
                MidDrugPresControl.Enabled = true;
                MidTemplateName.Text = e.Node.Text;              
            }
            else
            {
                MidDrugPresControl.Enabled = false;
                MidTemplateName.Text = string.Empty;
            }

            MidDrugPresControl.LoadPatData(Convert.ToInt32(e.Node.Name), 99999, "xxx", 99999, "xxxx");
        }

        /// <summary>
        /// 选中索引改变事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cbbMidDrugRoom_SelectedIndexChanged(object sender, EventArgs e)
        {            
            int midDrugDeptid = Convert.ToInt32(cbbMidDrugRoom.SelectedValue);
            PresTemplateDbHelper midPresHelper = new Controller.PresTemplateDbHelper(midDrugDeptid);
            MidDrugPresControl.InitDbHelper(midPresHelper);
            MidDrugPresControl.IsShowFootText = false;
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnWestClose_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            InvokeController("Close", this);
        }
    }
    #endregion  
 }
