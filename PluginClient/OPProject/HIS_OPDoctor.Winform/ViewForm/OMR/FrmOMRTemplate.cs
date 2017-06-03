using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.AdvTree;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.ClinicManage;
using HIS_OPDoctor.Winform.IView;

namespace HIS_OPDoctor.Winform.ViewForm
{
    /// <summary>
    /// 门诊病历模板
    /// </summary>
    public partial class FrmOMRTemplate : BaseFormBusiness, IFrmOMRTemplate
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmOMRTemplate()
        {
            InitializeComponent();
        }

        #region 自定义属性方法

        /// <summary>
        /// 开方医生
        /// </summary>
        public string StrDocName { get; set; }

        /// <summary>
        /// 开方科室
        /// </summary>
        public string StrDeptName { get; set; }

        /// <summary>
        /// 树集合
        /// </summary>
        private AdvTree useTree;

        /// <summary>
        /// 模板类型
        /// </summary>
        public string mouldType { get; set; }

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
        public NodeCollection SelectedNode
        {
            get
            {
                if (tvOMRTpl.SelectedNodes == null)
                {
                    return tvOMRTpl.Nodes;
                }
                else
                {
                    return tvOMRTpl.SelectedNodes;
                }
            }

            set
            {
                if (tvOMRTpl.SelectedNodes == null)
                {
                    value = tvOMRTpl.Nodes;
                }
                else
                {
                    value = tvOMRTpl.SelectedNodes;
                }
            }
        }

        /// <summary>
        /// 费用模板树选中节点
        /// </summary>
        private Node selectWestDrugNode;

        /// <summary>
        /// 选中的树节点
        /// </summary>
        public Node SelectOMRNode
        {
            get { return tvOMRTpl.SelectedNode; }
            set { selectWestDrugNode = value; }
        }

        /// <summary>
        /// 模板级别
        /// </summary>
        private int modilLevel;

        /// <summary>
        /// 模板级别
        /// </summary>
        public int ModilLevel
        {
            get { return modilLevel; }
            set { modilLevel = value; }
        }

        /// <summary>
        /// 模板头列表信息
        /// </summary>
        private List<OPD_OMRTmpHead> listHead;

        /// <summary>
        /// 模板头列表信息
        /// </summary>
        public List<OPD_OMRTmpHead> ListHead
        {
            get { return listHead; }
            set { listHead = value; }
        }
        #endregion      

        #region 接口
        /// <summary>
        /// 创建树
        /// </summary>
        /// <param name="intLevel">模板级别</param>
        /// <param name="tree">当前树对象</param>
        private void GetPresTemplate(int intLevel, AdvTree tree)
        {
            ListHead = (List<OPD_OMRTmpHead>)InvokeController("GetOMRTemplate", intLevel);
            tree.Nodes.Clear();
            LoadTree(tree, ListHead, "-1", null);
            if (tree.Nodes.Count > 0)
            {
                tree.SelectedNode = tree.Nodes[0];
            }

            tree.ExpandAll();
        } 
                     
        /// <summary>
        /// 递归遍历加载树节点
        /// </summary>
        /// <param name="treeView">树控件</param>
        /// <param name="list">模板列表</param>
        /// <param name="pid">父Id</param>
        /// <param name="pNode">节点</param>
        private void LoadTree(AdvTree treeView, List<OPD_OMRTmpHead> list, string pid, Node pNode)
        {
            string sFilter = "PID=" + pid;
            Node parentNode = pNode;
            List<OPD_OMRTmpHead> templist = list.Where(item => item.PID == Convert.ToInt32(pid)).ToList();
            foreach (OPD_OMRTmpHead bd in templist)
            {
                Node tempNode = new Node();
                tempNode.Text = bd.ModuldName;
                tempNode.Name = bd.OMRTmpHeadID.ToString();
                tempNode.AccessibleDescription = bd.MouldType.ToString();  //模板类型
                tempNode.Tag = bd.ModulLevel;
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

                LoadTree(treeView, list, bd.OMRTmpHeadID.ToString(), tempNode);
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

            if (SelectOMRNode != null)
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
        #endregion

        #region 窗体事件       
        /// <summary>
        /// 菜单打开事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void cmTree_Opening(object sender, CancelEventArgs e)
        {
            if (tvOMRTpl.SelectedNode == null)
            {
                return;
            }

            if (tvOMRTpl.SelectedNode.Text == "全部模板")
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

            if (tvOMRTpl.SelectedNode.AccessibleDescription == "1")
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
            OPD_OMRTmpHead tempHead = new OPD_OMRTmpHead();
            if (tvOMRTpl.SelectedNode == null)
            {
                tempHead.PID = 0;
                SelectedNode = tvOMRTpl.Nodes;
            }
            else
            {
                OPD_OMRTmpHead fartherHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));
                tempHead.PID = fartherHead.PID;
                SelectedNode = tvOMRTpl.SelectedNodes;
            }

            UseTree = tvOMRTpl;
            TreeLevel = 0;  //新增同级节点
            tempHead.ModulLevel = ModilLevel; //模板级别
            InvokeController("PopInfoWindow", 1, tempHead);
        }

        /// <summary>
        /// 新增子节点事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemNewSub_Click(object sender, EventArgs e)
        {
            OPD_OMRTmpHead tempHead = new OPD_OMRTmpHead();
            if (tvOMRTpl.SelectedNode != null)
            {
                OPD_OMRTmpHead fartherHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));
                tempHead.PID = fartherHead.OMRTmpHeadID;
                SelectedNode = tvOMRTpl.SelectedNodes;
                UseTree = tvOMRTpl;
                TreeLevel = 1;  //新增子节点
                tempHead.ModulLevel = ModilLevel; //模板级别
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
            if (tvOMRTpl.SelectedNode == null)
            {
                return;
            }

            OPD_OMRTmpHead delrHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));
            if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
            {
                if (tvOMRTpl.SelectedNode == null)
                {
                    return;
                }

                UseTree = tvOMRTpl;
                OPD_OMRTmpHead tempHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));

                InvokeController("PopInfoWindow", 2, tempHead);
            }
            else
            {
                MessageBox.Show("只有模板的创建者才能修改！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 删除项目事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void itemDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除已选择的模板分类或模板，请确认是否继续？", "提示框", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            if (tvOMRTpl.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该节点下还存在子节点，不能直接删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tvOMRTpl.SelectedNode != null)
            {
                OPD_OMRTmpHead delrHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));
                if (delrHead.CreateEmpID == (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
                {
                    ListHead.Remove(delrHead);
                    InvokeController("DeleteMoudelInfo", delrHead);
                    DeleteNode(tvOMRTpl);
                }
                else
                {
                    MessageBox.Show("只有模板的创建者才能删除！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            tvOMRTpl.ContextMenu = null;
        }

        /// <summary>
        /// 窗体打开前事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmFeeTemplate_OpenWindowBefore(object sender, EventArgs e)
        {
            GetPresTemplate(2, tvOMRTpl);
        }

        /// <summary>
        /// 单选按钮选中事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void radOAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOAll.Checked)
            {
                ModilLevel = 0;//全院
            }
            else if (radODept.Checked)
            {
                ModilLevel = 1;//科室
            }
            else
            {
                ModilLevel = 2;//个人
            }

            GetPresTemplate(ModilLevel, tvOMRTpl);
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtMouldName.Text.Trim() == string.Empty)
            {
                MessageBoxShowSimple("模板名称不能为空");
                txtMouldName.Focus();
                return;
            }

            OPD_OMRTmpHead tempHead = new OPD_OMRTmpHead();
            if (tvOMRTpl.SelectedNode != null)
            {
                OPD_OMRTmpHead fartherHead = ListHead.Find((OPD_OMRTmpHead head) => head.OMRTmpHeadID == Convert.ToInt32(tvOMRTpl.SelectedNode.Name));
                
                //是模板，那么提示是否替换该模板
                if (tvOMRTpl.SelectedNode.AccessibleDescription == "1")
                {
                    if (MessageBox.Show("你确定要替换原来的模板吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                    if (fartherHead.CreateEmpID != (InvokeController("this") as AbstractController).LoginUserInfo.EmpId)
                    {
                        MessageBoxShowSimple("该模板不是您创建的不能替换该模板");
                        return;
                    }

                    tempHead.OMRTmpHeadID = fartherHead.OMRTmpHeadID;
                    tempHead.PID = fartherHead.PID;
                }
                else
                {
                    tempHead.PID = fartherHead.OMRTmpHeadID;
                    if (Convert.ToBoolean(InvokeController("CheckName", txtMouldName.Text.Trim(), ModilLevel, tempHead.PID, 0)) == false)
                    {
                        MessageBox.Show("同级别模板名称不能重复！", "提示框", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                tempHead.ModulLevel = ModilLevel; //模板级别
                tempHead.ModuldName = txtMouldName.Text.Trim();
                tempHead.MouldType = 1;
                InvokeController("AsSaveTmp",tempHead);
            }
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("SetSuccessFlag");
            InvokeController("Close", this);
            this.Close();
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvOMRTpl_Click(object sender, EventArgs e)
        {
            tvOMRTpl.ContextMenu = null;
        }

        /// <summary>
        /// 节点点击事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void tvOMRTpl_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            tvOMRTpl.ContextMenuStrip = cmTree;
            if (tvOMRTpl.SelectedNode != null)
            {
                if (tvOMRTpl.SelectedNode.AccessibleDescription == "1")
                {
                    txtMouldName.Text = tvOMRTpl.SelectedNode.Text;
                }
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmOMRTemplate_Load(object sender, EventArgs e)
        {
            radOAll_CheckedChanged(null, null);
            txtMouldName.Text = string.Empty;
            txtMouldName.Focus();
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmOMRTemplate_FormClosed(object sender, FormClosedEventArgs e)
        {
            InvokeController("SetSuccessFlag");
        }

        /// <summary>
        /// 关闭窗体之后事件
        /// </summary>
        /// <param name="sender">控件对象</param>
        /// <param name="e">事件参数</param>
        private void FrmOMRTemplate_CloseWindowAfter(object sender, EventArgs e)
        {
            InvokeController("SetSuccessFlag");
        }
        #endregion
    }
}