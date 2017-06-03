using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_Entity.MaterialManage;
using HIS_MaterialManage.Winform.IView.BaseData;

namespace HIS_MaterialManage.Winform.ViewForm
{
    /// <summary>
    /// 物资字典
    /// </summary>
    public partial class FrmMaterialDic : BaseFormBusiness, IFrmMaterialDic
    {
        /// <summary>
        /// 首次加载标志
        /// </summary>
        private bool isFirstLoad = true;

        /// <summary>
        /// 价格百分比
        /// </summary>
        private double wmp = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmMaterialDic()
        {
            InitializeComponent();
            bindGridSelectIndex(dataGrid);
            bindGridSelectIndex(this.dgHisDic);
            fmCommon.AddItem(txtCode, "CenterMatCode");
            fmCommon.AddItem(txtName, "CenterMatName", "商品名称不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(txtSpec, "Spec", "规格不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(tbMaterType, "TypeID", "类型不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(tbcStatID, "StatID", "统计大项目不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(tbMinUnit, "UnitName", "单位不能为空！", InvalidType.Empty, null);
            fmCommon.AddItem(ckIsStop, "IsStop");

            frmHosp.AddItem(txtGoodName, "MatName", "商品名称不能为空！", InvalidType.Empty, null);
            frmHosp.AddItem(tbcProduct, "ProductID", "生产厂家不能为空！", InvalidType.Empty, null);
            frmHosp.AddItem(tbcHospStatID, "StatID", "统计大项目不能为空！", InvalidType.Empty, null);
            frmHosp.AddItem(dlStockPrice, "StockPrice");
            frmHosp.AddItem(dlRetailPrice, "RetailPrice");
            frmHosp.AddItem(tbcMedicare, "MedicareID");
            frmHosp.AddItem(ilAgricultural, "Agricultural");
            frmHosp.AddItem(ckIsBle, "IsBle");
            frmHosp.AddItem(ckIsUse, "IsUse");
            frmHosp.AddItem(ckHospStop, "IsStop");
        }

        /// <summary>
        /// 窗体初始化（公共事件）
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void FrmMaterialDic_OpenWindowBefore(object sender, EventArgs e)
        {
            if (frmName == "FrmMaterialDic")
            {
                superTabItem3.Visible = false;
                this.cbLocation.Visible = false;
                this.btnAutit.Visible = true;
            }
            else
            {
                this.cbLocation.Visible = true;
                this.cbLocation.Checked = true;
                this.btnAutit.Visible = false;
                InvokeController("GetHisDic");
                InvokeController("GetDeptParameters");
            }

            InvokeController("GetMaterialType", 0, frmName);
            InvokeController("GetMaterialTypeDic", frmName);
            InvokeController("GetStatItem", frmName);
            InvokeController("GetMaterialDic", frmName, 0);
            InvokeController("GetUnit", frmName);
            ckIsAudit.Checked = true;
            if (dataGrid.Rows.Count > 0)
            {
                GetCurrentInfo(0);
            }

            isFirstLoad = false;
        }

        #region 属性（规格）

        /// <summary>
        /// 获取当前选中行
        /// </summary>
        public MW_CenterSpecDic CurrentData { get; set; }

        /// <summary>
        /// 获取当前选中节点
        /// </summary>
        public Node CurrentParentNode;

        #endregion

        #region 函数（规格）
        /// <summary>
        /// 获取物资类型
        /// </summary>
        /// <param name="typelist">物资类型</param>
        public void LoadMaterialType(DataTable typelist)
        {
            advTree.Nodes.Clear();
            NodeBind(this.advTree, typelist, "0", null);
            if (this.advTree.Nodes.Count > 0)
            {
                this.advTree.SelectedNode = this.advTree.Nodes[0];
            }

            this.advTree.ExpandAll();
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeView">树型控件</param>
        /// <param name="dt">数据</param>
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
        /// 获取查询条件
        /// </summary>
        /// <param name="workId">机构ID</param>
        /// <returns>查询条件</returns>
        public Dictionary<string, string> GetQuery(string workId)
        {
            Dictionary<string, string> query = new Dictionary<string, string>();
            if (advTree.SelectedNode != null)
            {
                string nodeId = advTree.SelectedNode.Tag.ToString();
                switch (advTree.SelectedNode.Level)
                {
                    case 1:
                        query.Add("d.TypeID", nodeId);
                        break;
                    case 2:
                        query.Add("c.TypeID", nodeId);
                        break;
                    case 3:
                        query.Add("b.TypeID", nodeId);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                query.Add("Search", txtSearch.Text.Trim());
            }

            if (ckIsAudit.Checked)
            {
                query.Add("a.AuditStatus", "1");
            }

            if (ckNoAudit.Checked)
            {
                query.Add("a.AuditStatus", "0");
            }

            if (cbStop.Checked)
            {
                query.Add("a.IsStop", "1");
            }

            if (frmName != "FrmMaterialDic")
            {
                query.Add("a.CreateWorkID", workId);
            }

            return query;
        }

        /// <summary>
        /// 绑定大类型项目
        /// </summary>
        /// <param name="dt">大类型项目</param>
        public void LoadStat(DataTable dt)
        {
            tbcStatID.DisplayField = "StatName";
            tbcStatID.MemberField = "StatID";
            tbcStatID.CardColumn = "StatName|名称|265";
            tbcStatID.QueryFieldsString = "StatName";
            tbcStatID.ShowCardWidth = 315;
            tbcStatID.ShowCardDataSource = dt;

            tbcHospStatID.DisplayField = "StatName";
            tbcHospStatID.MemberField = "StatID";
            tbcHospStatID.CardColumn = "StatName|名称|265";
            tbcHospStatID.QueryFieldsString = "StatName";
            tbcHospStatID.ShowCardWidth = 315;
            tbcHospStatID.ShowCardDataSource = dt.Copy();
        }

        /// <summary>
        /// 绑定单位
        /// </summary>
        /// <param name="dt">单位</param>
        public void LoadUnit(DataTable dt)
        {
            tbMinUnit.DisplayField = "UnitName";
            tbMinUnit.MemberField = "UnitName";
            tbMinUnit.CardColumn = "UnitName|单位名称|auto,PYCode|拼音码|auto";
            tbMinUnit.QueryFieldsString = "UnitName,PYCode";
            tbMinUnit.ShowCardWidth = 350;
            tbMinUnit.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定物资类型下拉框控件
        /// </summary>
        /// <param name="dt">药品类型</param>
        public void BindTypeCombox(DataTable dt)
        {
            tbMaterType.DisplayField = "TypeName";
            tbMaterType.MemberField = "TypeID";
            tbMaterType.CardColumn = "TypeName|三级类型名称|150,ParentName|二级类型名称|150,PParentName|一级类型名称|150";
            tbMaterType.QueryFieldsString = "TypeName";
            tbMaterType.ShowCardWidth = 485;
            tbMaterType.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 根据物资类型获取物资字典信息
        /// </summary>
        public void LoadTable()
        {
            int level = 0;
            if (advTree.SelectedNode != null)
            {
                level = advTree.SelectedNode.Level;
            }

            InvokeController("GetMaterialDic", frmName, level);
        }

        /// <summary>
        /// 获取当前选中行信息
        /// </summary>
        /// <param name="rowindex">行号</param>
        public void GetCurrentInfo(int rowindex)
        {
            DataTable dt = (DataTable)dataGrid.DataSource;
            MW_CenterSpecDic dic = new MW_CenterSpecDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_CenterSpecDic>(dt, rowindex);
            CurrentData = dic;
            fmCommon.Load<MW_CenterSpecDic>(dic);
            tbMaterType.MemberValue = dt.Rows[rowindex]["TypeID"].ToString();
        }

        /// <summary>
        /// 绑定物资字典网格信息
        /// </summary>
        /// <param name="dt">物资字典列表</param>
        public void BInddgMeter(DataTable dt)
        {
            dataGrid.DataSource = dt;
        }

        /// <summary>
        /// 保存完成后提示
        /// </summary>
        /// <param name="result">保存结果</param>
        public void CompleteSave(bool result)
        {
            if (!result)
            {
                MessageBoxShowSimple("保存成功");
                if (frmName != "FrmMaterialDic")
                {
                    superTabItem3_Click(null, null);
                }
            }
            else
            {
                MessageBoxEx.Show("已存在同名称的商品");
            }
        }

        /// <summary>
        /// 审核完成后提示
        /// </summary>
        /// <param name="result">审核结果</param>
        public void CompleteAudit(int result)
        {
            if (result > 0)
            {
                MessageBoxShowSimple("审核成功");
                LoadTable();
            }
            else
            {
                MessageBoxShowSimple("审核失败");
            }
        }
        #endregion

        #region 事件（规格）

        /// <summary>
        /// 新增规格
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentData = null;
            this.fmCommon.Clear();
            this.txtCode.Focus();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isNew = false;
            if (fmCommon.Validate())
            {
                MW_CenterSpecDic dic = null;
                if (CurrentData != null)
                {
                    dic = CurrentData;
                    if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    isNew = true;
                    dic = new MW_CenterSpecDic();
                }

                try
                {
                    fmCommon.GetValue<MW_CenterSpecDic>(dic);
                    dic.TypeID = Convert.ToInt32(this.tbMaterType.MemberValue);
                    dic.CenterMatCode = this.txtCode.Text;
                    dic.UnitName = this.tbMinUnit.Text;
                    dic.UnitID = 0;
                    System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
                    provider.PercentDecimalDigits = 4;
                    dic.RetailPrice = 0;
                    dic.StockPrice = 0;
                    dic.PyCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(dic.CenterMatName);
                    dic.WbCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(dic.CenterMatName);
                    CurrentData = dic;
                    InvokeController("SaveMaterialDic", frmName);
                    LoadTable();
                    if (isNew)
                    {
                        setGridSelectIndex(dataGrid, dataGrid.RowCount - 1);
                        if (frmName != "FrmMaterialDic")
                        {
                            this.superTabControl2.SelectedPanel = superTabControlPanel4;
                            btnHospAdd_Click(null, null);
                        }
                    }
                    else
                    {
                        setGridSelectIndex(dataGrid);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败" + ex.Message);
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadTable();
        }

        /// <summary>
        /// 选中物资类型获取物资字典信息
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void advTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.advTree.SelectedNode = e.Node;
            if (!isFirstLoad)
            {
                LoadTable();
            }
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
        /// 输入检索条件自动查询数据
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnAutit_Click(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                if (CurrentData.AuditStatus == 1)
                {
                    MessageBoxEx.Show("该记录已经审核");
                }
                else
                {
                    InvokeController("AuditDic", CurrentData.CenterMatID, frmName);
                }
            }
            else
            {
                MessageBoxEx.Show("没有选中可审核的记录");
            }
        }

        /// <summary>
        /// 选中物资字典显示字典详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            //this.fmCommon.Clear();
            if (dataGrid.CurrentCell == null)
            {
                return;
            }

            CurrentData = null;
            int rowindex = dataGrid.CurrentCell.RowIndex;
            GetCurrentInfo(rowindex);
            if (frmName == "FrmHisMaterialDic")
            {
                InvokeController("LoadHisDic", CurrentData.CenterMatID);
            }
        }
        #endregion

        #region 属性（厂家）

        /// <summary>
        /// 选中的厂家字典数据
        /// </summary>
        public MW_HospMakerDic CurrentHospDic { get; set; }

        /// <summary>
        /// 选中行行号
        /// </summary>
        public int CurrentRowIndex { get; set; }
        #endregion

        #region 事件（厂家）
        /// <summary>
        /// 选择厂家Tab
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void superTabItem3_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnHospAdd_Click(object sender, EventArgs e)
        {
            this.frmHosp.Clear();

            txtGoodName.Focus();
            CurrentHospDic = null;
        }

        /// <summary>
        /// 选中厂家显示厂家详情
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dgHisDic_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgHisDic.CurrentCell == null)
            {
                frmHosp.Clear();
                CurrentHospDic = null;
                return;
            }

            CurrentHospDic = null;
            int rowindex = dgHisDic.CurrentCell.RowIndex;
            CurrentRowIndex = rowindex;
            DataTable dt = (DataTable)dgHisDic.DataSource;
            MW_HospMakerDic dic = new MW_HospMakerDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<MW_HospMakerDic>(dt, rowindex);
            CurrentHospDic = dic;
            frmHosp.Load<MW_HospMakerDic>(dic);
        }

        /// <summary>
        /// 保存厂家信息
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnSaveHosp_Click(object sender, EventArgs e)
        {
            bool isNew = false;
            if (frmHosp.Validate())
            {
                if (dlStockPrice.Value < 0.01)
                {
                    MessageBoxEx.Show("进价不能为0");
                    return;
                }

                if (dlRetailPrice.Value < 0.01)
                {
                    MessageBoxEx.Show("售价不能为0");
                    return;
                }

                if (dlStockPrice.Value > dlRetailPrice.Value)
                {
                    MessageBoxEx.Show("售价必须大于进价");
                    return;
                }

                if (CurrentData == null)
                {
                    MessageBoxEx.Show("请选择中心典记录");
                    return;
                }

                MW_HospMakerDic dic = null;
                if (CurrentHospDic != null)
                {
                    dic = CurrentHospDic;
                    if (dic.IsStop == 1)
                    {
                        this.ckHospStop.Enabled = true;
                    }

                    if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.No)
                    { 
                        return;
                    }
                }
                else
                {
                    dic = new MW_HospMakerDic();
                    isNew = true;
                }

                try
                {
                    frmHosp.GetValue<MW_HospMakerDic>(dic);
                    dic.PYCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(dic.MatName);
                    dic.WBCode = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(dic.MatName);
                    CurrentHospDic = dic;
                    dic.CenterMatID = CurrentData.CenterMatID;
                    InvokeController("SaveHisDic");
                    if (isNew)
                    {
                        setGridSelectIndex(dgHisDic, dgHisDic.RowCount - 1);
                    }
                    else
                    {
                        setGridSelectIndex(dgHisDic);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 取消编辑
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void btnHospCancel_Click(object sender, EventArgs e)
        {
            this.frmHosp.Clear();
            CurrentHospDic = null;
        }

        /// <summary>
        /// 参考进价变更
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="e">参数</param>
        private void dlStockPrice_ValueChanged(object sender, EventArgs e)
        {
            double stockprice = dlStockPrice.Value;
            dlRetailPrice.Value = Math.Round(stockprice * wmp, 2);
        }
        #endregion

        #region 函数（厂家）
        /// <summary>
        /// 绑定生产厂家
        /// </summary>
        /// <param name="dt">生产厂家</param>
        public void LoadProduct(DataTable dt)
        {
            tbcProduct.DisplayField = "ProductName";
            tbcProduct.MemberField = "ProductID";
            tbcProduct.CardColumn = "ProductName|名称|auto,PYCode|拼音码|auto";
            tbcProduct.QueryFieldsString = "ProductName,PYCode";
            tbcProduct.ShowCardWidth = 350;
            tbcProduct.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定医保类型
        /// </summary>
        /// <param name="dt">医保类型</param>
        public void LoadMedicare(DataTable dt)
        {
            tbcMedicare.DisplayField = "MedicareName";
            tbcMedicare.MemberField = "MedicareID";
            tbcMedicare.CardColumn = "MedicareName|名称|auto,PYCode|拼音码|auto";
            tbcMedicare.QueryFieldsString = "MedicareName,PYCode";
            tbcMedicare.ShowCardWidth = 350;
            tbcMedicare.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 绑定厂家网格信息
        /// </summary>
        /// <param name="dt">厂家信息</param>
        public void LoadHisDic(DataTable dt)
        {
            dgHisDic.DataSource = dt;
        }

        /// <summary>
        /// 保存厂家信息后清空数据
        /// </summary>
        public void SaveHospSuccess()
        {
            frmHosp.Clear();
            CurrentHospDic = null;
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室参数</param>
        public void BindDeptParameters(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ParaID"].ToString() == "MWPricePercent")
                {
                    wmp = (Convert.ToDouble(dt.Rows[i]["Value"]) / 100) + 1;
                }
            }
        }
        #endregion
    }
}
