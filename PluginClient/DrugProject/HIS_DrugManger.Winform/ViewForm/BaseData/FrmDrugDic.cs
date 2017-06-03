using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using EfwControls.Common;
using EfwControls.CustomControl;
using EFWCoreLib.CoreFrame.Business;
using HIS_DrugManage.Winform.IView.BaseData;
using HIS_DrugManage.Winform.ViewForm.DrugDic;
using HIS_Entity.BasicData;
using HIS_Entity.DrugManage;
using HIS_Entity.SqlAly;

namespace HIS_DrugManage.Winform.ViewForm.DrugDic
{
    /// <summary>
    /// 药品字典
    /// </summary>
    public partial class FrmDrugDic : BaseFormBusiness, IFrmDrugDic
    {
        /// <summary>
        /// 药品子类型
        /// </summary>
        private DataTable dtDrugChildType;

        /// <summary>
        /// 药品药剂
        /// </summary>
        private DataTable dtDosageData;

        /// <summary>
        /// 西药
        /// </summary>
        double wmp = 0;

        /// <summary>
        /// 中成药
        /// </summary>
        double cpmp = 0;

        /// <summary>
        /// 中药
        /// </summary>
        double tcmp = 0;

        /// <summary>
        /// 参数列表
        /// </summary>
        public FrmDrugDic()
        {
            InitializeComponent();
            bindGridSelectIndex(dgDrugDic);
            bindGridSelectIndex(this.dgHisDic);
            this.comCheck.SelectedIndex = 2;
            frmForm1.AddItem(this.comCheck, "AuditStatus");
            frmForm1.AddItem(this.cbLocation, "WorkID");
            frmForm1.AddItem(this.cbStop, "IsStop");
            frmForm1.AddItem(this.txtName, "ChemName");

            frmForm2.AddItem(this.txtCName, "ChemName", "化学名称不能为空！", InvalidType.Empty, null);
            //frmForm2.AddItem(this.txtEName, "LatinName");
            frmForm2.AddItem(this.tbDrugType, "TypeID", "类型不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.tbDrugCType, "CTypeID", "子类型不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.tbPackUnit, "PackUnitID", "包装单位不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.packNum, "PackAmount", "包装系数不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.tbMinUnit, "MiniUnitID", "最小单位不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.txtDoseAmount, "DoseAmount", "含量系数不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.tbcDoseUnit, "DoseUnitID", "含量单位不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.tbcStatID, "StatID", "统计大项目不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.txtDurgCode, "CenterDrugCode");
            frmForm2.AddItem(this.tbcPharm, "PharmID");
            frmForm2.AddItem(this.tbcDosage, "DosageID");
            frmForm2.AddItem(this.txtSpec, "Spec");
            frmForm2.AddItem(this.tbcAnt, "AntID");
            frmForm2.AddItem(this.cbLunacy, "LunacyGrade");
            frmForm2.AddItem(this.ckRecipe, "IsRecipe");
            frmForm2.AddItem(this.ckIsPosition, "IsPosion");
            frmForm2.AddItem(this.ckIsAze, "IsAze");
            frmForm2.AddItem(this.ckIsSkin, "IsSkin");
            frmForm2.AddItem(this.ckBigTrans, "IsBigTransfu");
            frmForm2.AddItem(this.ckIsStop, "IsStop");
            frmForm2.AddItem(this.txtDicPY, "PYCode","拼音码不能为空！", InvalidType.Empty, null);
            frmForm2.AddItem(this.txtDicWb, "WBCode", "五笔码不能为空！", InvalidType.Empty, null);

            frmForm3.AddItem(txtNationalCode, "NationalCode", "国药准字号不能为空！", InvalidType.Empty, null);
            frmForm3.AddItem(txtGoodName, "TradeName", "名称不能为空！", InvalidType.Empty, null);
            frmForm3.AddItem(tbcProduct, "ProductID", "厂家名不能为空！", InvalidType.Empty, null);
            frmForm3.AddItem(txtStockPrice, "StockPrice");
            frmForm3.AddItem(txtRetailPrice, "RetailPrice");
            frmForm3.AddItem(tbcMedicare, "MedicareID");
            frmForm3.AddItem(txtAgricultural, "Agricultural");
            frmForm3.AddItem(cbRoundOff, "RoundingMode");
            frmForm3.AddItem(txtValidity, "Duration");
            frmForm3.AddItem(cbMaker, "Maker");
            frmForm3.AddItem(this.ckOpfree, "OPFree");
            frmForm3.AddItem(this.ckIpFree, "IPFree");
            frmForm3.AddItem(this.ckHospStop, "IsStop");
            frmForm3.AddItem(this.txtHDicPY, "PYCode", "拼音码不能为空！", InvalidType.Empty, null);
            frmForm3.AddItem(this.txtHDicWb, "WBCode", "五笔码不能为空！", InvalidType.Empty, null);
            this.cbLunacy.SelectedIndex = 0;
            this.cbRoundOff.SelectedIndex = 0;
            this.cbMaker.SelectedIndex = 0;
        }

        /// <summary>
        /// 打开窗体执行事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void FrmDrugDic_OpenWindowBefore(object sender, EventArgs e)
        {
            this.frmForm2.Clear();
            this.frmForm1.Clear();
            dgDrugDic.DataSource = null;
            if (frmName == "FrmDrugDic")
            {
                this.superTabItem3.Visible = false;
                this.btnAutit.Visible = true;
            }

            if (frmName == "FrmHisDrugDic")
            {
                this.cbLocation.Visible = true;
                this.cbLocation.Checked = true;
            }

            InvokeController("ChangeView", frmName);
            InvokeController("InitData", frmName);
            InvokeController("GetDurgDic", frmName);
            InvokeController("GetDeptParameters");

            btnCacel.Visible = false; //新增的时候才显示取消按钮
            btnHospCancel.Visible = false;
        }

        #region 中心典接口
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> AndWhere { get; set; }

        /// <summary>
        /// 读取药品字典
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugDic(DataTable dt)
        {
            this.dgDrugDic.DataSource = dt;
            setGridSelectIndex(dgDrugDic);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> OrWhere { get; set; }

        /// <summary>
        /// 当前药品字典对象
        /// </summary>
        public DG_CenterSpecDic CurrentData { get; set; }

        /// <summary>
        /// 当前本院药品字典对象
        /// </summary>
        public DG_HospMakerDic CurrentHospDic { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<Tuple<string, string, SqlOperator>> HospAndWhere { get; set; }

        /// <summary>
        /// 药品类型
        /// </summary>
        public Dictionary<string, string> DrugType { get; set; }
        #endregion

        #region 本院典接口
        /// <summary>
        /// 读取生产商
        /// </summary>
        /// <param name="dt">生产商数据源</param>
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
        /// 读取医保类型
        /// </summary>
        /// <param name="dt">医保类型数据源</param>
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
        /// 读取本院信息
        /// </summary>
        /// <param name="dt">本院信息数据源</param>
        public void LoadHisDic(DataTable dt)
        {
            this.dgHisDic.DataSource = dt;
            setGridSelectIndex(dgHisDic);
        }
        #endregion

        #region 树构造
        /// <summary>
        /// 药品类型树
        /// </summary>
        /// <param name="dt">药品类型数据源</param>
        public void LoadDrugType(DataTable dt)
        {
            if (dt != null)
            {
                var parentQ = from t in dt.AsEnumerable()
                              group t by new { t1 = t.Field<int>("TypeID"), t2 = t.Field<string>("TypeName") } into m
                              select new
                              {
                                  key = m.Key.t1,
                                  text = m.Key.t2
                              };
                this.treeDrugType.Nodes.Clear();
                foreach (var node in parentQ)
                {
                    TreeNode tempNode = new TreeNode();
                    tempNode.Name = node.key.ToString();
                    tempNode.Text = node.text;
                    tempNode.Tag = node.key.ToString();
                    var dr = dt.Select("TypeId=" + node.key);
                    foreach (var d in dr)
                    {
                        if (!string.IsNullOrEmpty(d["CTypeName"].ToString()))
                        {
                            tempNode.Nodes.Add(new TreeNode
                            {
                                Name = d["CTypeName"].ToString(),
                                Tag = d["CTypeID"].ToString(),
                                Text = d["CTypeName"].ToString()
                            });
                        }
                    }

                    this.treeDrugType.Nodes.Add(tempNode);
                }
            }
        }

        /// <summary>
        /// 加载药理树
        /// </summary>
        /// <param name="dt">药理树数据源</param>
        public void LoadPharms(DataTable dt)
        {
            this.treePharm.Nodes.Clear();
            Bind_Textboxcard(dt);
            NodeBind(this.treePharm, dt, "0", null);
        }

        /// <summary>
        /// 绑定药理树节点
        /// </summary>
        /// <param name="dt">药理树数据源</param>
        public void BindTree(DataTable dt)
        {
            this.treePharm.Nodes.Clear();

            //  Bind_Textboxcard(dt);
            NodeBind(treePharm, dt, "0", null);
        }

        /// <summary>
        /// 构造树
        /// </summary>
        /// <param name="treeView">树对象</param>
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
        #endregion

        #region 中心典事件 
        /// <summary>
        ///  获取查询条件
        /// </summary>
        private void BuildQueryConnection()
        {
            AndWhere = new List<Tuple<string, string, SqlOperator>>();
            OrWhere = new List<Tuple<string, string, SqlOperator>>();

            if (frmName == "FrmHisDrugDic")
            {
                this.dgHisDic.DataSource = null;
                if (cbLocation.Checked)
                {
                    AndWhere.Add(Tuple.Create("CreateWorkIDS", (InvokeController("this") as AbstractController).LoginUserInfo.WorkId.ToString(), SqlOperator.Equal));
                }
            }

            if (this.txtName.Text.Trim() != string.Empty)
            {
                OrWhere.Add(Tuple.Create("PYCode", txtName.Text.Trim(), SqlOperator.Like));
                OrWhere.Add(Tuple.Create("ChemName", txtName.Text.Trim(), SqlOperator.Like));
            }

            if (comCheck.SelectedIndex != 2 && comCheck.SelectedIndex >= 0)
            {
                AndWhere.Add(Tuple.Create("AuditStatus", comCheck.SelectedIndex.ToString(), SqlOperator.Equal));
            }

            if (!this.cbStop.Checked)
            {
                AndWhere.Add(Tuple.Create("IsStop", "0", SqlOperator.Equal));
            }

            if (this.cbStop.Checked)
            {
                AndWhere.Add(Tuple.Create("IsStop", "1", SqlOperator.Equal));
            }
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BuildQueryConnection();
            InvokeController("ChangeView", frmName);
            InvokeController("GetDurgDic", frmName);
        }

        /// <summary>
        /// 关闭操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("ChangeView", frmName);
            InvokeController("Close", this);
        }

        /// <summary>
        /// 点击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void treePharm_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AndWhere = new List<Tuple<string, string, SqlOperator>>();
            OrWhere = new List<Tuple<string, string, SqlOperator>>();
            AndWhere.Add(Tuple.Create("PharmID", e.Node.Tag.ToString(), SqlOperator.Equal));
            InvokeController("ChangeView", frmName);
            InvokeController("GetDurgDic", frmName);
        }

        /// <summary>
        /// 双击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void treeDrugType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AndWhere = new List<Tuple<string, string, SqlOperator>>();
            OrWhere = new List<Tuple<string, string, SqlOperator>>();
            OrWhere.Add(Tuple.Create("TypeID", e.Node.Tag.ToString(), SqlOperator.Equal));
            OrWhere.Add(Tuple.Create("CTypeID", e.Node.Tag.ToString(), SqlOperator.Equal));
            InvokeController("ChangeView", frmName);
            InvokeController("GetDurgDic", frmName);
        }

        /// <summary>
        /// 审核操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAutit_Click(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                if (MessageBox.Show("确定审核通过？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.No)
                {
                    return;
                }

                InvokeController("ChangeView", frmName);
                InvokeController("AuditDurgDic", frmName);
                frmForm2.Clear();
                CurrentData = null;
            }
            else
            {
                MessageBoxEx.Show("请选择记录");
            }
        }

        /// <summary>
        /// 键盘操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BuildQueryConnection();
                InvokeController("GetDurgDic", frmName);
                this.txtName.Focus();
                ((TextBoxX)sender).SelectAll();
            }
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnCacel.Visible = true; //新增的时候才显示取消按钮
            btnAdd.Visible = false;
            bar2.Refresh();
            CurrentData = null;
            this.frmForm2.Clear();
            this.txtCName.Focus();
        }

        /// <summary>
        ///保存操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnCacel.Visible = false;
            bar2.Refresh();
            bool isNew = false;
            if (frmForm2.Validate())
            {
                DG_CenterSpecDic dic = null;
                if (CurrentData != null)
                {
                    dic = CurrentData;
                    if (MessageBox.Show("确定更改记录？", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                        DialogResult.No)
                    {
                        return;
                    }
                }
                else
                {
                    isNew = true;
                    dic = new DG_CenterSpecDic();
                }

                try
                {
                    frmForm2.GetValue<DG_CenterSpecDic>(dic);
                    dic.PackUnit = this.tbPackUnit.Text;
                    dic.MiniUnit = this.tbMinUnit.Text;
                    dic.DoseUnit = this.tbcDoseUnit.Text;
                    dic.PYCode = string.IsNullOrEmpty(txtDicPY.Text) == true ? EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(dic.ChemName): txtDicPY.Text;
                    dic.WBCode = string.IsNullOrEmpty(txtDicWb.Text) == true ? EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(dic.ChemName): txtDicWb.Text;
                    dic.LunacyGrade = this.cbLunacy.SelectedIndex;
                    CurrentData = dic;
                    InvokeController("ChangeView", frmName);
                    InvokeController("SaveDrugDic", frmName, isNew);
                    if (isNew)
                    {
                        setGridSelectIndex(dgDrugDic, dgDrugDic.Rows.Count - 1);
                        if (frmName == "FrmHisDrugDic")
                        {
                            this.superTabControl2.SelectedPanel = superTabControlPanel4;
                            btnAddHosp_Click(null, null);
                        }
                    }
                    else
                    {
                        setGridSelectIndex(dgDrugDic);
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
        /// 保存成功后执行的函数
        /// </summary>
        /// <param name="isNew">是否是新增</param>
        public void SaveSuccess(bool isNew)
        {
            if (isNew)
            {
                DataTable dt = dgDrugDic.DataSource as DataTable;
                DataRow dr = dt.NewRow();
                dr["CenteDrugID"] = CurrentData.CenteDrugID;
                dr["CenterDrugCode"] = CurrentData.CenterDrugCode;
                dr["ChemName"] = CurrentData.ChemName;
                //dr["LatinName"] = CurrentData.LatinName;
                dr["Spec"] = CurrentData.Spec;
                dr["drugTypeNam"] = tbDrugType.Text;
                dr["CTypeName"] = tbDrugCType.Text;
                dr["PackUnit"] = CurrentData.PackUnit;
                dr["PackAmount"] = CurrentData.PackAmount;
                dr["DoseUnit"] = CurrentData.DoseUnit;
                dr["DoseAmount"] = CurrentData.DoseAmount;
                dr["MiniUnit"] = CurrentData.MiniUnit;
                dr["DosageName"] = tbcDosage.Text;
                dr["AuditName"] = "未审核";
                dr["UserName"] = string.Empty;
                dr["AuditTime"] = "1900/1/1";
                dr["StopName"] = CurrentData.IsStop == 0 ? "启用" : "停用";
                dr["PackUnitID"] = CurrentData.PackUnitID;
                dr["MiniUnitID"] = CurrentData.MiniUnitID;
                dr["DoseUnitID"] = CurrentData.DoseUnitID;
                dr["StockPrice"] = CurrentData.StockPrice;
                dr["RetailPrice"] = CurrentData.RetailPrice;
                dr["TypeID"] = CurrentData.TypeID;
                dr["CTypeID"] = CurrentData.CTypeID;
                dr["StatID"] = CurrentData.StatID;
                dr["PharmID"] = CurrentData.PharmID;
                dr["DosageID"] = CurrentData.DosageID;
                dr["LunacyGrade"] = CurrentData.LunacyGrade;
                dr["IsBigTransfu"] = CurrentData.IsBigTransfu;
                dr["IsRecipe"] = CurrentData.IsRecipe;
                dr["IsStop"] = CurrentData.IsStop;
                dr["Auditor"] = CurrentData.Auditor;
                dr["AuditStatus"] = CurrentData.AuditStatus;
                dr["ModEmpID"] = CurrentData.ModEmpID;
                dr["AntID"] = CurrentData.AntID;
                dr["IsSkin"] = CurrentData.IsSkin;
                dr["IsBid"] = CurrentData.IsBid;
                dr["IsLunacy"] = CurrentData.IsLunacy;
                dr["IsCostly"] = CurrentData.IsCostly;
                dr["IsBasic"] = CurrentData.IsBasic;
                dr["IsPosion"] = CurrentData.IsPosion;
                dr["IsAze"] = CurrentData.IsAze;
                dr["IsGMP"] = CurrentData.IsGMP;
                dt.Rows.Add(dr);
                dgDrugDic.DataSource = dt;
            }
            else
            {
                frmForm2.Clear();
                CurrentData = null;
            }
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnCacel_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            bar2.Refresh();
            //CurrentData = null;
            //frmForm2.Clear();
            if (dgDrugDic.CurrentCell == null)
            {
                return;
            }

            CurrentData = null;
            int rowindex = dgDrugDic.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugDic.DataSource;
            DG_CenterSpecDic dic = new DG_CenterSpecDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_CenterSpecDic>(dt, rowindex);
            if (frmName == "FrmHisDrugDic")
            {
                HospAndWhere = new List<Tuple<string, string, SqlOperator>>();
                HospAndWhere.Add(Tuple.Create("d.CenteDrugID", dic.CenteDrugID.ToString(), SqlOperator.Equal));
                InvokeController("LoadHisDic");
            }

            CurrentData = dic;
            frmForm2.Load<DG_CenterSpecDic>(dic);
            if (dic.IsAze == 1)
            {
                this.ckIsAze.Checked = true;
            }

            if (dic.IsRecipe == 1)
            {
                this.ckRecipe.Checked = true;
            }

            if (dic.IsSkin == 1)
            {
                this.ckIsSkin.Checked = true;
            }

            if (dic.IsPosion == 1)
            {
                this.ckIsPosition.Checked = true;
            }

            if (dic.IsBigTransfu == 1)
            {
                this.ckBigTrans.Checked = true;
            }

            if (dic.IsStop == 1)
            {
                this.ckIsStop.Checked = true;
            }

            this.cbLunacy.SelectedIndex = dic.LunacyGrade;
            if (dic.AuditStatus == 1)
            {
                this.txtAutitMan.Text = dt.Rows[rowindex]["UserName"].ToString();
                this.timeAu.Text = dt.Rows[rowindex]["AuditTime"].ToString();
                this.txtAuditResult.Text = "已经审核";
            }
            else
            {
                this.txtAuditResult.Text = "未审核";
            }

            btnCacel.Visible = false; //新增的时候才显示取消按钮
        }

        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbDrugType_TextChanged(object sender, EventArgs e)
        {
            tbDrugCType.Text = string.Empty;
            tbcDosage.Text = string.Empty;
            DataTable data = null;
            DataTable dsdata = null;
            if (dtDrugChildType != null)
            {
                data = dtDrugChildType;
            }
            else
            {
                data = tbDrugCType.ShowCardDataSource;
            }

            if (dtDosageData != null)
            {
                dsdata = dtDosageData;
            }
            else
            {
                dsdata = tbcDosage.ShowCardDataSource;
            }

            if (data != null && tbDrugType.MemberValue != null)
            {
                var cdata = data.Select("TypeID=" + tbDrugType.MemberValue);
                DataTable dtNew = data.Clone();
                for (int i = 0; i < cdata.Length; i++)
                {
                    dtNew.ImportRow(cdata[i]);
                }

                tbDrugCType.ShowCardDataSource = dtNew;
            }

            if (dsdata != null && tbDrugType.MemberValue != null)
            {
                var cdata = dsdata.Select("TypeID=" + tbDrugType.MemberValue);
                DataTable dtNew = dsdata.Clone();
                for (int i = 0; i < cdata.Length; i++)
                {
                    dtNew.ImportRow(cdata[i]);
                }

                tbcDosage.ShowCardDataSource = dtNew;
            }
        }

        #region 计算规格
        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbPackUnit_TextChanged(object sender, EventArgs e)
        {
            GetSpecText();
        }

        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void packNum_TextChanged(object sender, EventArgs e)
        {
            GetSpecText();
        }

        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbMinUnit_TextChanged(object sender, EventArgs e)
        {
            GetSpecText();
        }

        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtDoseAmount_TextChanged(object sender, EventArgs e)
        {
            GetSpecText();
        }

        /// <summary>
        /// 文本改变操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void tbcDoseUnit_TextChanged(object sender, EventArgs e)
        {
            GetSpecText();
        }

        /// <summary>
        /// 获取规格文本
        /// </summary>
        private void GetSpecText()
        {
            //txtSpec.Text = txtDoseAmount.Text + tbcDoseUnit.Text + "*" + packNum.Text + "*" + this.tbMinUnit.Text + "/" + this.tbPackUnit.Text;
            txtSpec.Text = txtDoseAmount.Text + tbcDoseUnit.Text + "*" + packNum.Text + this.tbMinUnit.Text + "/" + this.tbPackUnit.Text;
        }
        #endregion
        #endregion

        #region 绑定CARD控件
        /// <summary>
        /// 读取药品子类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugCType(DataTable dt)
        {
            dtDrugChildType = dt;
            tbDrugCType.DisplayField = "CTypeName";
            tbDrugCType.MemberField = "CTypeID";
            tbDrugCType.CardColumn = "CTypeName|药品子类型|auto,PYCode|拼音码|auto";
            tbDrugCType.QueryFieldsString = "CTypeName,PYCode";
            tbDrugCType.ShowCardWidth = 350;
            tbDrugCType.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 读取药品类型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDrugTypeForTb(DataTable dt)
        {
            tbDrugType.DisplayField = "TypeName";
            tbDrugType.MemberField = "TypeID";
            tbDrugType.CardColumn = "TypeName|药品类型|auto,PYCode|拼音码|auto";
            tbDrugType.QueryFieldsString = "TypeName,PYCode";
            tbDrugType.ShowCardWidth = 350;
            tbDrugType.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 读取单位
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadCommUnit(DataTable dt)
        {
            tbPackUnit.DisplayField = "UnitName";
            tbPackUnit.MemberField = "UnitID";
            tbPackUnit.CardColumn = "UnitName|单位名称|auto,PYCode|拼音码|auto";
            tbPackUnit.QueryFieldsString = "UnitName,PYCode";
            tbPackUnit.ShowCardWidth = 350;
            tbPackUnit.ShowCardDataSource = dt;

            tbMinUnit.DisplayField = "UnitName";
            tbMinUnit.MemberField = "UnitID";
            tbMinUnit.CardColumn = "UnitName|单位名称|auto,PYCode|拼音码|auto";
            tbMinUnit.QueryFieldsString = "UnitName,PYCode";
            tbMinUnit.ShowCardWidth = 350;
            tbMinUnit.ShowCardDataSource = dt;

            tbcDoseUnit.DisplayField = "UnitName";
            tbcDoseUnit.MemberField = "UnitID";
            tbcDoseUnit.CardColumn = "UnitName|单位名称|auto,PYCode|拼音码|auto";
            tbcDoseUnit.QueryFieldsString = "UnitName,PYCode";
            tbcDoseUnit.ShowCardWidth = 350;
            tbcDoseUnit.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 读取大项目
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadStat(DataTable dt)
        {
            tbcStatID.DisplayField = "StatName";
            tbcStatID.MemberField = "StatID";
            tbcStatID.CardColumn = "StatName|名称|auto,PYCode|拼音码|auto";
            tbcStatID.QueryFieldsString = "StatName,PYCode";
            tbcStatID.ShowCardWidth = 350;
            tbcStatID.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 读取剂型
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadDosage(DataTable dt)
        {
            dtDosageData = dt;
            tbcDosage.DisplayField = "DosageName";
            tbcDosage.MemberField = "DosageID";
            tbcDosage.CardColumn = "DosageName|药品剂型|auto,PYCode|拼音码|auto";
            tbcDosage.QueryFieldsString = "DosageName,PYCode";
            tbcDosage.ShowCardWidth = 350;
            tbcDosage.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 读取抗生素
        /// </summary>
        /// <param name="dt">数据源</param>
        public void LoadAnt(DataTable dt)
        {
            this.tbcAnt.DisplayField = "AntName";
            tbcAnt.MemberField = "AntID";
            tbcAnt.CardColumn = "AntName|名称|auto,PYCode|拼音码|auto";
            tbcAnt.QueryFieldsString = "AntName,PYCode";
            tbcAnt.ShowCardWidth = 350;
            tbcAnt.ShowCardDataSource = dt;
        }

        /// <summary>
        /// 药理
        /// </summary>
        /// <param name="dt">药理数据源</param>
        public void Bind_Textboxcard(DataTable dt)
        {
            tbcPharm.DisplayField = "PharmName";
            tbcPharm.MemberField = "PharmID";
            tbcPharm.CardColumn = "PharmName|药理名称|auto,PYCode|拼音码|auto";
            tbcPharm.QueryFieldsString = "PharmName,PYCode";
            tbcPharm.ShowCardWidth = 350;
            tbcPharm.ShowCardDataSource = dt;
        }
        #endregion

        #region 本院典事件
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnAddHosp_Click(object sender, EventArgs e)
        {
            btnHospCancel.Visible = true;
            btnAddHosp.Visible = false;
            bar1.Refresh();
            this.frmForm3.Clear();
            txtGoodName.Text = txtCName.Text;
            txtNationalCode.Focus();
            CurrentHospDic = null;
        }

        /// <summary>
        ///保存操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnSaveHosp_Click(object sender, EventArgs e)
        {
            btnHospCancel.Visible = false;
            btnAddHosp.Visible = true;
            bar1.Refresh();
            bool isNew = false;
            if (frmForm3.Validate())
            {
                if (cbRoundOff.SelectedIndex == -1)
                {
                    MessageBoxEx.Show("不能为空");
                    cbRoundOff.Focus();
                    return;
                }

                if (cbMaker.SelectedIndex == -1)
                {
                    MessageBoxEx.Show("不能为空");
                    cbMaker.Focus();
                    return;
                }

                if (CurrentData == null)
                {
                    MessageBoxEx.Show("请选择中心典记录");
                    return;
                }

                if (txtStockPrice.Value < 0.01)
                {
                    MessageBoxEx.Show("进价不能为0");
                    return;
                }

                if (txtRetailPrice.Value < 0.01)
                {
                    MessageBoxEx.Show("售价不能为0");
                    return;
                }

                if (txtStockPrice.Value > txtRetailPrice.Value)
                {
                    MessageBoxEx.Show("售价必须大于进价");
                    return;
                }

                DG_HospMakerDic dic = null;
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
                    isNew = true;
                    dic = new DG_HospMakerDic();
                }

                int id = dic.ProductID;
                try
                {
                    frmForm3.GetValue<DG_HospMakerDic>(dic);

                    dic.PYCode = string.IsNullOrEmpty(txtHDicPY.Text) == true ? EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(dic.TradeName) : txtHDicPY.Text;
                    dic.WBCode = string.IsNullOrEmpty(txtHDicWb.Text) == true ? EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(dic.TradeName) : txtHDicWb.Text;
                    dic.RoundingMode = this.cbRoundOff.SelectedIndex;
                    dic.Maker = this.cbMaker.SelectedIndex;
                    CurrentHospDic = dic;
                    dic.CenteDrugID = CurrentData.CenteDrugID;
                    InvokeController("ChangeView", frmName);
                    InvokeController("SaveHisDic");
                    if (isNew)
                    {
                        setGridSelectIndex(this.dgHisDic, this.dgHisDic.Rows.Count - 1);
                    }
                    else
                    {
                        setGridSelectIndex(this.dgHisDic);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 保存成功后执行的函数
        /// </summary>
        public void SaveHospSuccess()
        {
            frmForm3.Clear();
            CurrentHospDic = null;
        }

        /// <summary>
        /// 点击操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgHisDic_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///取消操作
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void btnHospCancel_Click(object sender, EventArgs e)
        {
            btnAddHosp.Visible = true;
            bar1.Refresh();
            //frmForm3.Clear();
            //CurrentHospDic = null;
            btnHospCancel.Visible = false;
            if (dgHisDic.CurrentCell == null)
            {
                frmForm3.Clear();
                CurrentHospDic = null;
                return;
            }

            CurrentHospDic = null;
            int rowindex = dgHisDic.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgHisDic.DataSource;
            DG_HospMakerDic dic = new DG_HospMakerDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_HospMakerDic>(dt, rowindex);
            CurrentHospDic = dic;
            this.cbRoundOff.SelectedIndex = dic.RoundingMode;
            this.cbMaker.SelectedIndex = dic.Maker;
            frmForm3.Load<DG_HospMakerDic>(dic);
        }
        #endregion

        #region 正则验证部分
        /// <summary>
        /// 名称验证
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtEName_Leave(object sender, EventArgs e)
        {
            /*if (!string.IsNullOrEmpty(txtEName.Text))
            {
                Regex rx = new Regex(".*");
                if (!rx.IsMatch(txtEName.Text))
                {
                    MessageBoxEx.Show("只能字符");
                    txtEName.Focus();
                    return;
                }
            }*/
        }

        /// <summary>
        /// 编码验证
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtDurgCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDurgCode.Text.Trim()))
            {
                Regex rx = new Regex("^[A-Za-z0-9]+$");
                if (!rx.IsMatch(txtDurgCode.Text))
                {
                    MessageBoxEx.Show("请由字母数据组合");
                    txtDurgCode.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 数量验证
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtDoseAmount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDoseAmount.Text))
            {
                Regex r = new Regex(@"^[0]({1-9}\d{0,4})?(.\d{0,3})?$|^[1-9](\d{0,5})?(.\d{0,3})?$");
                if (!r.IsMatch(txtDoseAmount.Text))
                {
                    MessageBoxEx.Show("请输入整数位小于6位");
                    txtDoseAmount.Focus();
                    return;
                }
            }
        }

        /// <summary>
        /// 数量验证
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void packNum_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(packNum.Text))
            {
                Regex r = new Regex(@"^[0]({1-9}\d{0,4})?(.\d{0,3})?$|^[1-9](\d{0,5})?(.\d{0,3})?$");
                if (!r.IsMatch(packNum.Text))
                {
                    MessageBoxEx.Show("请输入整数位小于6位");
                    packNum.Focus();
                    return;
                }
            }
        }
        #endregion

        /// <summary>
        /// 选中发生变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgDrugDic_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgDrugDic.CurrentCell == null)
            {
                return;
            }

            CurrentData = null;
            int rowindex = dgDrugDic.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgDrugDic.DataSource;
            DG_CenterSpecDic dic = new DG_CenterSpecDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_CenterSpecDic>(dt, rowindex);
            if (frmName == "FrmHisDrugDic")
            {
                btnAdd.Visible = true;
                btnCacel.Visible = false;
                bar2.Refresh();

                btnAddHosp.Visible = true;
                btnHospCancel.Visible = false;
                bar1.Refresh();

                HospAndWhere = new List<Tuple<string, string, SqlOperator>>();
                HospAndWhere.Add(Tuple.Create("d.CenteDrugID", dic.CenteDrugID.ToString(), SqlOperator.Equal));
                InvokeController("LoadHisDic");
            }

            CurrentData = dic;
            frmForm2.Load<DG_CenterSpecDic>(dic);
            if (dic.IsAze == 1)
            {
                this.ckIsAze.Checked = true;
            }

            if (dic.IsRecipe == 1)
            {
                this.ckRecipe.Checked = true;
            }

            if (dic.IsSkin == 1)
            {
                this.ckIsSkin.Checked = true;
            }

            if (dic.IsPosion == 1)
            {
                this.ckIsPosition.Checked = true;
            }

            if (dic.IsBigTransfu == 1)
            {
                this.ckBigTrans.Checked = true;
            }

            if (dic.IsStop == 1)
            {
                this.ckIsStop.Checked = true;
            }

            this.cbLunacy.SelectedIndex = dic.LunacyGrade;
            if (dic.AuditStatus == 1)
            {
                this.txtAutitMan.Text = dt.Rows[rowindex]["UserName"].ToString();
                this.timeAu.Text = dt.Rows[rowindex]["AuditTime"].ToString();
                this.txtAuditResult.Text = "已经审核";
            }
            else
            {
                this.txtAuditResult.Text = "未审核";
            }
        }

        /// <summary>
        /// 选中发生变化事件
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void dgHisDic_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgHisDic.CurrentCell == null)
            {
                frmForm3.Clear();
                CurrentHospDic = null;
                return;
            }

            CurrentHospDic = null;
            int rowindex = dgHisDic.CurrentCell.RowIndex;
            DataTable dt = (DataTable)dgHisDic.DataSource;
            DG_HospMakerDic dic = new DG_HospMakerDic();
            dic = EFWCoreLib.CoreFrame.Common.ConvertExtend.ToObject<DG_HospMakerDic>(dt, rowindex);
            CurrentHospDic = dic;
            this.cbRoundOff.SelectedIndex = dic.RoundingMode;
            this.cbMaker.SelectedIndex = dic.Maker;
            frmForm3.Load<DG_HospMakerDic>(dic);
        }

        /// <summary>
        /// 绑定科室参数
        /// </summary>
        /// <param name="dt">科室参数数据源</param>
        public void BindDeptParameters(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double value = dt.Rows[i]["Value"] == null || dt.Rows[i]["Value"].ToString() == string.Empty ? 0 : Convert.ToDouble(dt.Rows[i]["Value"]);
                if (dt.Rows[i]["ParaID"].ToString() == "WMPricePercent")
                {
                    wmp = (value / 100) + 1;
                }

                if (dt.Rows[i]["ParaID"].ToString() == "CPMPricePercent")
                {
                    cpmp = (value / 100) + 1;
                }

                if (dt.Rows[i]["ParaID"].ToString() == "TCMPricePercent")
                {
                    tcmp = (value / 100) + 1;
                }
            }
        }

        /// <summary>
        /// 值发生变化
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        private void txtStockPrice_ValueChanged(object sender, EventArgs e)
        {
            double stockprice = txtStockPrice.Value;
            switch (tbDrugType.Text)
            {
                case "西药":
                    txtRetailPrice.Value = Math.Round(stockprice * wmp, 2);
                    break;
                case "中成药":
                    txtRetailPrice.Value = Math.Round(stockprice * cpmp, 2);
                    break;
                case "中药":
                    txtRetailPrice.Value = Math.Round(stockprice * tcmp, 2);
                    break;
            }
        }

        /// <summary>
        /// 当名称发生改变时，重新生成拼音码五笔码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCName_TextChanged(object sender, EventArgs e)
        {
            txtDicPY.Text = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(txtCName.Text);
            txtDicWb.Text = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(txtCName.Text);
        }

        /// <summary>
        /// 根据商品名称自动生成五笔码和拼音码
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void txtGoodName_TextChanged(object sender, EventArgs e)
        {
            txtHDicPY.Text = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetSpellCode(txtGoodName.Text);
            txtHDicWb.Text = EFWCoreLib.CoreFrame.Common.SpellAndWbCode.GetWBCode(txtGoodName.Text);
        }
    }
}
