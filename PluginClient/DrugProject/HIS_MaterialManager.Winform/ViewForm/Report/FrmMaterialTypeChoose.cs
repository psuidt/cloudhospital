using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using EFWCoreLib.CoreFrame.Business;
using HIS_MaterialManage.Winform.IView.Report;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资类型选择
    /// </summary>
    public partial class FrmMaterialTypeChoose : BaseFormBusiness, IFrmMaterialTypeChoose
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialTypeChoose()
        {
            InitializeComponent();
        }

        #region 自定义属性和方法

        /// <summary>
        /// 结果
        /// </summary>
        public int Result = 0;

        /// <summary>
        /// 物资类型Id
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 物资类型名称
        /// </summary>
        public string TypeName { get; set; }

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
                    if (pid == "0")
                    {
                        tempNode.Tag = 0;
                    }
                    else
                    {
                        tempNode.Tag = drv["TypeID"].ToString();
                    }

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
        #endregion

        #region 接口
        /// <summary>
        /// 物资类型加载
        /// </summary>
        /// <param name="dtTypeList">物资类型</param>
        public void LoadMaterialType(DataTable dtTypeList)
        {
            advTree.Nodes.Clear();
            NodeBind(this.advTree, dtTypeList, "0", null);
            this.advTree.ExpandAll();
        }
        #endregion

        #region 事件
        #endregion

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = 0;
            this.Close();
        }

        /// <summary>
        /// 打开界面加载数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialTypeChoose_Load(object sender, EventArgs e)
        {
            InvokeController("GetMaterialTypeList");
        }

        /// <summary>
        /// 确定选择物资类型
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if(advTree.Nodes.Count<=0)
            {
                MessageBoxEx.Show("没有物资类型可以选择");
                return;
            }

            if (advTree.SelectedNode != null)
            {
                this.TypeId =Convert.ToInt32(advTree.SelectedNode.Tag);
                this.TypeName = advTree.SelectedNode.Text;
            }
            else
            {
                MessageBoxEx.Show("请选择树节点！");
                return;
            }

            DialogResult = DialogResult.OK;
            Result = 1;
        }
    }
}
