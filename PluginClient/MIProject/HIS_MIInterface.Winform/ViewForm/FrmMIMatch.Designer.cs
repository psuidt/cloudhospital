namespace HIS_MIInterface.Winform.ViewForm
{
    partial class FrmMIMatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMIMatch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgvMatchList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dHospItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dHospItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dCenterItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dCenterItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenterSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenterDrugDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenterUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CenterSellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YBItemLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YBItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar4 = new DevComponents.DotNetBar.Bar();
            this.tbMatch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelItem6 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem8 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem9 = new DevComponents.DotNetBar.LabelItem();
            this.cmbMatchState = new DevComponents.DotNetBar.ComboBoxItem();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.btnMatchQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnMatchDel = new DevComponents.DotNetBar.ButtonItem();
            this.btnAuditReset = new DevComponents.DotNetBar.ButtonItem();
            this.btnMatchExport = new DevComponents.DotNetBar.ButtonItem();
            this.btnAllReset = new DevComponents.DotNetBar.ButtonItem();
            this.btnImportMatchLog = new DevComponents.DotNetBar.ButtonItem();
            this.btnDrugImport = new DevComponents.DotNetBar.ButtonItem();
            this.btnItemImport = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveMatch = new DevComponents.DotNetBar.ButtonItem();
            this.btnUploadMatch = new DevComponents.DotNetBar.ButtonItem();
            this.btnDownloadAudit = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.dgvExamGroup = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.tbHosSerch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.医院目录 = new DevComponents.DotNetBar.LabelItem();
            this.cbUnMatch = new DevComponents.DotNetBar.CheckBoxItem();
            this.cbUnStop = new DevComponents.DotNetBar.CheckBoxItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.cmbHosLogType = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnHisLogQuery = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem11 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.btnHisLogExport = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgvYbItemList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PYCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WBCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsYBFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar3 = new DevComponents.DotNetBar.Bar();
            this.tbMISerch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.cmbMILogType = new DevComponents.DotNetBar.ComboBoxItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.labelItem5 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem12 = new DevComponents.DotNetBar.LabelItem();
            this.btnMatch = new DevComponents.DotNetBar.ButtonItem();
            this.btnMILogImport = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveMiLog = new DevComponents.DotNetBar.ButtonItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem7 = new DevComponents.DotNetBar.LabelItem();
            this.cmbMIType = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.labelItem10 = new DevComponents.DotNetBar.LabelItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdateMIlog = new DevComponents.DotNetBar.ButtonItem();
            this.btnAutoMatch = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdateLevel = new DevComponents.DotNetBar.ButtonItem();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hStockID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hMedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MedicareItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hSellPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hItemLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hFacName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hPyCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hWbCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).BeginInit();
            this.bar4.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYbItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.bar3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1362, 678);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgvMatchList);
            this.panelEx3.Controls.Add(this.bar4);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 301);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1362, 377);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // dgvMatchList
            // 
            this.dgvMatchList.AllowUserToAddRows = false;
            this.dgvMatchList.AllowUserToDeleteRows = false;
            this.dgvMatchList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvMatchList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMatchList.BackgroundColor = System.Drawing.Color.White;
            this.dgvMatchList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatchList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMatchList.ColumnHeadersHeight = 25;
            this.dgvMatchList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMatchList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.AuditFlag,
            this.Column2,
            this.dHospItemCode,
            this.dHospItemName,
            this.dCenterItemCode,
            this.dCenterItemName,
            this.CenterSpec,
            this.CenterDrugDosage,
            this.CenterUnit,
            this.CenterSellPrice,
            this.YBItemLevel,
            this.YBItemType,
            this.Column1,
            this.Memo});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatchList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMatchList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatchList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvMatchList.HighlightSelectedColumnHeaders = false;
            this.dgvMatchList.Location = new System.Drawing.Point(0, 28);
            this.dgvMatchList.MultiSelect = false;
            this.dgvMatchList.Name = "dgvMatchList";
            this.dgvMatchList.ReadOnly = true;
            this.dgvMatchList.RowHeadersVisible = false;
            this.dgvMatchList.RowHeadersWidth = 21;
            this.dgvMatchList.RowTemplate.Height = 25;
            this.dgvMatchList.SelectAllSignVisible = false;
            this.dgvMatchList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMatchList.Size = new System.Drawing.Size(1362, 349);
            this.dgvMatchList.TabIndex = 35;
            this.dgvMatchList.CurrentCellChanged += new System.EventHandler(this.dgvMatchList_CurrentCellChanged);
            this.dgvMatchList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvMatchList_RowPrePaint);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // AuditFlag
            // 
            this.AuditFlag.DataPropertyName = "AuditFlag";
            this.AuditFlag.HeaderText = "状态";
            this.AuditFlag.Name = "AuditFlag";
            this.AuditFlag.ReadOnly = true;
            this.AuditFlag.Width = 50;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "AuditDate";
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = "yyyy-MM-dd hh:mm:ss";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "审核时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // dHospItemCode
            // 
            this.dHospItemCode.DataPropertyName = "ItemCode";
            this.dHospItemCode.HeaderText = "HIS目录编码";
            this.dHospItemCode.Name = "dHospItemCode";
            this.dHospItemCode.ReadOnly = true;
            // 
            // dHospItemName
            // 
            this.dHospItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dHospItemName.DataPropertyName = "HospItemName";
            this.dHospItemName.HeaderText = "HIS目录名称";
            this.dHospItemName.MinimumWidth = 135;
            this.dHospItemName.Name = "dHospItemName";
            this.dHospItemName.ReadOnly = true;
            // 
            // dCenterItemCode
            // 
            this.dCenterItemCode.DataPropertyName = "YBItemCode";
            this.dCenterItemCode.HeaderText = "社保目录编码";
            this.dCenterItemCode.Name = "dCenterItemCode";
            this.dCenterItemCode.ReadOnly = true;
            // 
            // dCenterItemName
            // 
            this.dCenterItemName.DataPropertyName = "YBItemName";
            this.dCenterItemName.HeaderText = "社保目录名称";
            this.dCenterItemName.Name = "dCenterItemName";
            this.dCenterItemName.ReadOnly = true;
            this.dCenterItemName.Width = 135;
            // 
            // CenterSpec
            // 
            this.CenterSpec.DataPropertyName = "YBSpec";
            this.CenterSpec.HeaderText = "规格";
            this.CenterSpec.Name = "CenterSpec";
            this.CenterSpec.ReadOnly = true;
            this.CenterSpec.Width = 125;
            // 
            // CenterDrugDosage
            // 
            this.CenterDrugDosage.DataPropertyName = "YBDosage";
            this.CenterDrugDosage.HeaderText = "剂型";
            this.CenterDrugDosage.Name = "CenterDrugDosage";
            this.CenterDrugDosage.ReadOnly = true;
            this.CenterDrugDosage.Width = 50;
            // 
            // CenterUnit
            // 
            this.CenterUnit.DataPropertyName = "YBUnit";
            this.CenterUnit.HeaderText = "单位";
            this.CenterUnit.Name = "CenterUnit";
            this.CenterUnit.ReadOnly = true;
            this.CenterUnit.Width = 50;
            // 
            // CenterSellPrice
            // 
            this.CenterSellPrice.DataPropertyName = "YBPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.CenterSellPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.CenterSellPrice.HeaderText = "单价";
            this.CenterSellPrice.Name = "CenterSellPrice";
            this.CenterSellPrice.ReadOnly = true;
            this.CenterSellPrice.Width = 50;
            // 
            // YBItemLevel
            // 
            this.YBItemLevel.DataPropertyName = "YBItemLevel";
            this.YBItemLevel.HeaderText = "收费等级";
            this.YBItemLevel.Name = "YBItemLevel";
            this.YBItemLevel.ReadOnly = true;
            this.YBItemLevel.Width = 125;
            // 
            // YBItemType
            // 
            this.YBItemType.DataPropertyName = "YBItemType";
            this.YBItemType.HeaderText = "收费类别";
            this.YBItemType.Name = "YBItemType";
            this.YBItemType.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ItemType";
            this.Column1.HeaderText = "目录类别";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Memo
            // 
            this.Memo.DataPropertyName = "Memo";
            this.Memo.HeaderText = "备注";
            this.Memo.Name = "Memo";
            this.Memo.ReadOnly = true;
            // 
            // bar4
            // 
            this.bar4.AntiAlias = true;
            this.bar4.Controls.Add(this.tbMatch);
            this.bar4.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar4.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar4.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar4.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem6,
            this.labelItem8,
            this.labelItem9,
            this.cmbMatchState,
            this.btnMatchQuery,
            this.btnMatchDel,
            this.btnAuditReset,
            this.btnMatchExport,
            this.btnAllReset,
            this.btnImportMatchLog,
            this.btnDrugImport,
            this.btnItemImport,
            this.btnSaveMatch,
            this.btnUploadMatch,
            this.btnDownloadAudit,
            this.btnUpdateLevel});
            this.bar4.Location = new System.Drawing.Point(0, 0);
            this.bar4.Name = "bar4";
            this.bar4.Size = new System.Drawing.Size(1362, 28);
            this.bar4.Stretch = true;
            this.bar4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar4.TabIndex = 1;
            this.bar4.TabStop = false;
            this.bar4.Text = "bar4";
            // 
            // tbMatch
            // 
            // 
            // 
            // 
            this.tbMatch.Border.Class = "TextBoxBorder";
            this.tbMatch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMatch.Location = new System.Drawing.Point(113, 2);
            this.tbMatch.Name = "tbMatch";
            this.tbMatch.Size = new System.Drawing.Size(198, 23);
            this.tbMatch.TabIndex = 10000;
            this.tbMatch.WatermarkText = "项目名称、拼音码、五笔码";
            // 
            // labelItem6
            // 
            this.labelItem6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelItem6.Name = "labelItem6";
            this.labelItem6.Text = "匹配关系";
            // 
            // labelItem8
            // 
            this.labelItem8.Name = "labelItem8";
            this.labelItem8.Text = "快速检索";
            // 
            // labelItem9
            // 
            this.labelItem9.Name = "labelItem9";
            this.labelItem9.Text = "                                                          ";
            // 
            // cmbMatchState
            // 
            this.cmbMatchState.ComboWidth = 75;
            this.cmbMatchState.DropDownHeight = 106;
            this.cmbMatchState.ItemHeight = 17;
            this.cmbMatchState.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem7});
            this.cmbMatchState.Name = "cmbMatchState";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "全部";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "已匹配";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "已审核";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "未通过";
            // 
            // btnMatchQuery
            // 
            this.btnMatchQuery.Name = "btnMatchQuery";
            this.btnMatchQuery.Text = "查询";
            this.btnMatchQuery.Click += new System.EventHandler(this.btnMatchQuery_Click);
            // 
            // btnMatchDel
            // 
            this.btnMatchDel.Enabled = false;
            this.btnMatchDel.Name = "btnMatchDel";
            this.btnMatchDel.Text = "删除匹配";
            this.btnMatchDel.Click += new System.EventHandler(this.btnMatchDel_Click);
            // 
            // btnAuditReset
            // 
            this.btnAuditReset.Enabled = false;
            this.btnAuditReset.Name = "btnAuditReset";
            this.btnAuditReset.Text = "单条重置";
            this.btnAuditReset.Click += new System.EventHandler(this.btnAuditReset_Click);
            // 
            // btnMatchExport
            // 
            this.btnMatchExport.Name = "btnMatchExport";
            this.btnMatchExport.Text = "导出匹配信息";
            this.btnMatchExport.Click += new System.EventHandler(this.btnMatchExport_Click);
            // 
            // btnAllReset
            // 
            this.btnAllReset.Name = "btnAllReset";
            this.btnAllReset.Text = "全部重置";
            this.btnAllReset.Visible = false;
            this.btnAllReset.Click += new System.EventHandler(this.btnAllReset_Click);
            // 
            // btnImportMatchLog
            // 
            this.btnImportMatchLog.BeginGroup = true;
            this.btnImportMatchLog.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnImportMatchLog.Name = "btnImportMatchLog";
            this.btnImportMatchLog.Text = "导入审核结果";
            this.btnImportMatchLog.Visible = false;
            this.btnImportMatchLog.Click += new System.EventHandler(this.ImportMatchLog_Click);
            // 
            // btnDrugImport
            // 
            this.btnDrugImport.Name = "btnDrugImport";
            this.btnDrugImport.Text = "药品导入";
            this.btnDrugImport.Click += new System.EventHandler(this.btnDrugImport_Click);
            // 
            // btnItemImport
            // 
            this.btnItemImport.Name = "btnItemImport";
            this.btnItemImport.Text = "材料项目导入";
            this.btnItemImport.Click += new System.EventHandler(this.btnItemImport_Click);
            // 
            // btnSaveMatch
            // 
            this.btnSaveMatch.Name = "btnSaveMatch";
            this.btnSaveMatch.Text = "保存导入";
            this.btnSaveMatch.Click += new System.EventHandler(this.btnSaveMatch_Click);
            // 
            // btnUploadMatch
            // 
            this.btnUploadMatch.BeginGroup = true;
            this.btnUploadMatch.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnUploadMatch.Name = "btnUploadMatch";
            this.btnUploadMatch.Text = "上传匹配目录";
            this.btnUploadMatch.Click += new System.EventHandler(this.btnUploadMatch_Click);
            // 
            // btnDownloadAudit
            // 
            this.btnDownloadAudit.Name = "btnDownloadAudit";
            this.btnDownloadAudit.Text = "下载审核结果";
            this.btnDownloadAudit.Click += new System.EventHandler(this.btnDownloadAudit_Click);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 295);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(1362, 6);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 2;
            this.expandableSplitter1.TabStop = false;
            this.expandableSplitter1.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.expandableSplitter1_ExpandedChanged);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx5);
            this.panelEx2.Controls.Add(this.expandableSplitter2);
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 29);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1362, 266);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.dgvExamGroup);
            this.panelEx5.Controls.Add(this.bar2);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(748, 266);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 3;
            // 
            // dgvExamGroup
            // 
            this.dgvExamGroup.AllowUserToAddRows = false;
            this.dgvExamGroup.AllowUserToDeleteRows = false;
            this.dgvExamGroup.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvExamGroup.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvExamGroup.BackgroundColor = System.Drawing.Color.White;
            this.dgvExamGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExamGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvExamGroup.ColumnHeadersHeight = 25;
            this.dgvExamGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvExamGroup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemID,
            this.hStockID,
            this.hMedName,
            this.MedicareItemCode,
            this.hSpec,
            this.hUnit,
            this.hSellPrice,
            this.hDosage,
            this.hItemLevel,
            this.hFacName,
            this.hPyCode,
            this.hWbCode});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExamGroup.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvExamGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExamGroup.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvExamGroup.HighlightSelectedColumnHeaders = false;
            this.dgvExamGroup.Location = new System.Drawing.Point(0, 27);
            this.dgvExamGroup.MultiSelect = false;
            this.dgvExamGroup.Name = "dgvExamGroup";
            this.dgvExamGroup.ReadOnly = true;
            this.dgvExamGroup.RowHeadersVisible = false;
            this.dgvExamGroup.RowHeadersWidth = 21;
            this.dgvExamGroup.RowTemplate.Height = 25;
            this.dgvExamGroup.SelectAllSignVisible = false;
            this.dgvExamGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExamGroup.Size = new System.Drawing.Size(748, 239);
            this.dgvExamGroup.TabIndex = 33;
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Controls.Add(this.tbHosSerch);
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.医院目录,
            this.cbUnMatch,
            this.cbUnStop,
            this.labelItem1,
            this.cmbHosLogType,
            this.btnHisLogQuery,
            this.labelItem2,
            this.labelItem11,
            this.btnHisLogExport});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(748, 27);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 0;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // tbHosSerch
            // 
            // 
            // 
            // 
            this.tbHosSerch.Border.Class = "TextBoxBorder";
            this.tbHosSerch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbHosSerch.Location = new System.Drawing.Point(448, 2);
            this.tbHosSerch.Name = "tbHosSerch";
            this.tbHosSerch.Size = new System.Drawing.Size(148, 21);
            this.tbHosSerch.TabIndex = 10000;
            this.tbHosSerch.WatermarkText = "项目名称、拼音码、五笔码";
            this.tbHosSerch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbHosSerch_KeyUp);
            // 
            // 医院目录
            // 
            this.医院目录.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.医院目录.Name = "医院目录";
            this.医院目录.Text = "医院目录";
            // 
            // cbUnMatch
            // 
            this.cbUnMatch.Checked = true;
            this.cbUnMatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUnMatch.Name = "cbUnMatch";
            this.cbUnMatch.Text = "未匹配";
            // 
            // cbUnStop
            // 
            this.cbUnStop.Checked = true;
            this.cbUnStop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUnStop.Name = "cbUnStop";
            this.cbUnStop.Text = "过滤停用状态";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "   类型";
            // 
            // cmbHosLogType
            // 
            this.cmbHosLogType.ComboWidth = 80;
            this.cmbHosLogType.DropDownHeight = 106;
            this.cmbHosLogType.ItemHeight = 16;
            this.cmbHosLogType.Name = "cmbHosLogType";
            this.cmbHosLogType.SelectedIndexChanged += new System.EventHandler(this.cmbHosLogType_SelectedIndexChanged);
            // 
            // btnHisLogQuery
            // 
            this.btnHisLogQuery.Name = "btnHisLogQuery";
            this.btnHisLogQuery.Text = "查询";
            this.btnHisLogQuery.Click += new System.EventHandler(this.btnHisLogQuery_Click);
            // 
            // labelItem2
            // 
            this.labelItem2.BeginGroup = true;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "快速检索";
            // 
            // labelItem11
            // 
            this.labelItem11.Name = "labelItem11";
            this.labelItem11.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.controlContainerItem2});
            this.labelItem11.Text = "                               ";
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            // 
            // btnHisLogExport
            // 
            this.btnHisLogExport.BeginGroup = true;
            this.btnHisLogExport.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnHisLogExport.Name = "btnHisLogExport";
            this.btnHisLogExport.Text = "导出";
            this.btnHisLogExport.Click += new System.EventHandler(this.btnHisLogExport_Click);
            // 
            // expandableSplitter2
            // 
            this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.Location = new System.Drawing.Point(748, 0);
            this.expandableSplitter2.Name = "expandableSplitter2";
            this.expandableSplitter2.Size = new System.Drawing.Size(6, 266);
            this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter2.TabIndex = 2;
            this.expandableSplitter2.TabStop = false;
            this.expandableSplitter2.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(this.expandableSplitter2_ExpandedChanged);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgvYbItemList);
            this.panelEx4.Controls.Add(this.bar3);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx4.Location = new System.Drawing.Point(754, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(608, 266);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            // 
            // dgvYbItemList
            // 
            this.dgvYbItemList.AllowUserToAddRows = false;
            this.dgvYbItemList.AllowUserToDeleteRows = false;
            this.dgvYbItemList.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvYbItemList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvYbItemList.BackgroundColor = System.Drawing.Color.White;
            this.dgvYbItemList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvYbItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvYbItemList.ColumnHeadersHeight = 25;
            this.dgvYbItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvYbItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemName,
            this.ItemType,
            this.Spec,
            this.Unit,
            this.Price,
            this.Dosage,
            this.ItemLevel,
            this.Factory,
            this.PYCode,
            this.WBCode,
            this.IsYBFlag});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvYbItemList.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvYbItemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvYbItemList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvYbItemList.HighlightSelectedColumnHeaders = false;
            this.dgvYbItemList.Location = new System.Drawing.Point(0, 28);
            this.dgvYbItemList.MultiSelect = false;
            this.dgvYbItemList.Name = "dgvYbItemList";
            this.dgvYbItemList.ReadOnly = true;
            this.dgvYbItemList.RowHeadersVisible = false;
            this.dgvYbItemList.RowHeadersWidth = 21;
            this.dgvYbItemList.RowTemplate.Height = 25;
            this.dgvYbItemList.SelectAllSignVisible = false;
            this.dgvYbItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYbItemList.Size = new System.Drawing.Size(608, 238);
            this.dgvYbItemList.TabIndex = 33;
            // 
            // ItemCode
            // 
            this.ItemCode.DataPropertyName = "ItemCode";
            this.ItemCode.HeaderText = "社保目录编码";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "社保目录名称";
            this.ItemName.MinimumWidth = 135;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // ItemType
            // 
            this.ItemType.DataPropertyName = "ItemType";
            this.ItemType.HeaderText = "发票分类";
            this.ItemType.Name = "ItemType";
            this.ItemType.ReadOnly = true;
            this.ItemType.Width = 80;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Width = 125;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 50;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle12;
            this.Price.HeaderText = "价格";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.Width = 50;
            // 
            // Dosage
            // 
            this.Dosage.DataPropertyName = "Dosage";
            this.Dosage.HeaderText = "剂型";
            this.Dosage.Name = "Dosage";
            this.Dosage.ReadOnly = true;
            this.Dosage.Width = 80;
            // 
            // ItemLevel
            // 
            this.ItemLevel.DataPropertyName = "ItemLevel";
            this.ItemLevel.HeaderText = "项目等级";
            this.ItemLevel.Name = "ItemLevel";
            this.ItemLevel.ReadOnly = true;
            this.ItemLevel.Width = 80;
            // 
            // Factory
            // 
            this.Factory.DataPropertyName = "Factory";
            this.Factory.HeaderText = "生产厂家";
            this.Factory.Name = "Factory";
            this.Factory.ReadOnly = true;
            this.Factory.Width = 125;
            // 
            // PYCode
            // 
            this.PYCode.DataPropertyName = "PYCode";
            this.PYCode.HeaderText = "拼音码";
            this.PYCode.Name = "PYCode";
            this.PYCode.ReadOnly = true;
            this.PYCode.Visible = false;
            // 
            // WBCode
            // 
            this.WBCode.DataPropertyName = "WBCode";
            this.WBCode.HeaderText = "五笔码";
            this.WBCode.Name = "WBCode";
            this.WBCode.ReadOnly = true;
            this.WBCode.Visible = false;
            // 
            // IsYBFlag
            // 
            this.IsYBFlag.DataPropertyName = "IsYBFlag";
            this.IsYBFlag.HeaderText = "可报";
            this.IsYBFlag.Name = "IsYBFlag";
            this.IsYBFlag.ReadOnly = true;
            // 
            // bar3
            // 
            this.bar3.AntiAlias = true;
            this.bar3.Controls.Add(this.tbMISerch);
            this.bar3.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar3.DockSide = DevComponents.DotNetBar.eDockSide.Right;
            this.bar3.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem3,
            this.labelItem4,
            this.cmbMILogType,
            this.labelItem5,
            this.labelItem12,
            this.btnMatch,
            this.btnMILogImport,
            this.btnSaveMiLog});
            this.bar3.Location = new System.Drawing.Point(0, 0);
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(608, 28);
            this.bar3.Stretch = true;
            this.bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar3.TabIndex = 1;
            this.bar3.TabStop = false;
            this.bar3.Text = "bar3";
            // 
            // tbMISerch
            // 
            // 
            // 
            // 
            this.tbMISerch.Border.Class = "TextBoxBorder";
            this.tbMISerch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbMISerch.Location = new System.Drawing.Point(248, 2);
            this.tbMISerch.Name = "tbMISerch";
            this.tbMISerch.Size = new System.Drawing.Size(198, 23);
            this.tbMISerch.TabIndex = 10000;
            this.tbMISerch.WatermarkText = "项目名称、拼音码、五笔码";
            // 
            // labelItem3
            // 
            this.labelItem3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "医保目录";
            // 
            // labelItem4
            // 
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "      类型";
            // 
            // cmbMILogType
            // 
            this.cmbMILogType.ComboWidth = 80;
            this.cmbMILogType.DropDownHeight = 106;
            this.cmbMILogType.ItemHeight = 17;
            this.cmbMILogType.Items.AddRange(new object[] {
            this.comboItem4,
            this.comboItem5,
            this.comboItem6});
            this.cmbMILogType.Name = "cmbMILogType";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "药品";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "项目";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "材料";
            // 
            // labelItem5
            // 
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.Text = "快速检索";
            // 
            // labelItem12
            // 
            this.labelItem12.Name = "labelItem12";
            this.labelItem12.Text = "                                                              ";
            // 
            // btnMatch
            // 
            this.btnMatch.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Text = "匹配";
            // 
            // btnMILogImport
            // 
            this.btnMILogImport.BeginGroup = true;
            this.btnMILogImport.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnMILogImport.Name = "btnMILogImport";
            this.btnMILogImport.Text = "导入";
            this.btnMILogImport.Click += new System.EventHandler(this.btnMILogImport_Click);
            // 
            // btnSaveMiLog
            // 
            this.btnSaveMiLog.Name = "btnSaveMiLog";
            this.btnSaveMiLog.Text = "保存";
            this.btnSaveMiLog.Click += new System.EventHandler(this.btnSaveMiLog_Click);
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem7,
            this.cmbMIType,
            this.btnRefresh,
            this.labelItem10,
            this.btnClose,
            this.btnUpdateMIlog,
            this.btnAutoMatch});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1362, 29);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem7
            // 
            this.labelItem7.Name = "labelItem7";
            this.labelItem7.Text = "医保类型";
            // 
            // cmbMIType
            // 
            this.cmbMIType.ComboWidth = 100;
            this.cmbMIType.DropDownHeight = 106;
            this.cmbMIType.ItemHeight = 17;
            this.cmbMIType.Name = "cmbMIType";
            this.cmbMIType.SelectedIndexChanged += new System.EventHandler(this.cmbMIType_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Visible = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // labelItem10
            // 
            this.labelItem10.Name = "labelItem10";
            this.labelItem10.Text = "           ";
            // 
            // btnClose
            // 
            this.btnClose.BeginGroup = true;
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdateMIlog
            // 
            this.btnUpdateMIlog.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUpdateMIlog.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateMIlog.Image")));
            this.btnUpdateMIlog.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnUpdateMIlog.Name = "btnUpdateMIlog";
            this.btnUpdateMIlog.Text = "更新社保目录";
            this.btnUpdateMIlog.Click += new System.EventHandler(this.btnUpdateMIlog_Click);
            // 
            // btnAutoMatch
            // 
            this.btnAutoMatch.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAutoMatch.Image = global::HIS_MIInterface.Winform.Properties.Resources.出院记录;
            this.btnAutoMatch.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btnAutoMatch.Name = "btnAutoMatch";
            this.btnAutoMatch.Text = "辅助匹配";
            // 
            // btnUpdateLevel
            // 
            this.btnUpdateLevel.Name = "btnUpdateLevel";
            this.btnUpdateLevel.Text = "更新医保级别";
            this.btnUpdateLevel.Click += new System.EventHandler(this.btnUpdateLevel_Click);
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "HIS目录编码";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.Width = 90;
            // 
            // hStockID
            // 
            this.hStockID.DataPropertyName = "ItemCode";
            this.hStockID.HeaderText = "HIS目录编码";
            this.hStockID.Name = "hStockID";
            this.hStockID.ReadOnly = true;
            this.hStockID.Visible = false;
            this.hStockID.Width = 80;
            // 
            // hMedName
            // 
            this.hMedName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.hMedName.DataPropertyName = "ItemName";
            this.hMedName.HeaderText = "HIS目录名称";
            this.hMedName.MinimumWidth = 135;
            this.hMedName.Name = "hMedName";
            this.hMedName.ReadOnly = true;
            // 
            // MedicareItemCode
            // 
            this.MedicareItemCode.DataPropertyName = "MedicareItemCode";
            this.MedicareItemCode.HeaderText = "医保编码";
            this.MedicareItemCode.Name = "MedicareItemCode";
            this.MedicareItemCode.ReadOnly = true;
            // 
            // hSpec
            // 
            this.hSpec.DataPropertyName = "Standard";
            this.hSpec.HeaderText = "规格";
            this.hSpec.Name = "hSpec";
            this.hSpec.ReadOnly = true;
            this.hSpec.Width = 125;
            // 
            // hUnit
            // 
            this.hUnit.DataPropertyName = "UnPickUnit";
            this.hUnit.HeaderText = "单位";
            this.hUnit.Name = "hUnit";
            this.hUnit.ReadOnly = true;
            this.hUnit.Width = 50;
            // 
            // hSellPrice
            // 
            this.hSellPrice.DataPropertyName = "SellPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.hSellPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.hSellPrice.HeaderText = "价格";
            this.hSellPrice.Name = "hSellPrice";
            this.hSellPrice.ReadOnly = true;
            this.hSellPrice.Width = 50;
            // 
            // hDosage
            // 
            this.hDosage.DataPropertyName = "Dosage";
            this.hDosage.HeaderText = "剂型";
            this.hDosage.Name = "hDosage";
            this.hDosage.ReadOnly = true;
            this.hDosage.Width = 80;
            // 
            // hItemLevel
            // 
            this.hItemLevel.DataPropertyName = "ItemClass";
            this.hItemLevel.HeaderText = "项目级别";
            this.hItemLevel.Name = "hItemLevel";
            this.hItemLevel.ReadOnly = true;
            this.hItemLevel.Width = 80;
            // 
            // hFacName
            // 
            this.hFacName.DataPropertyName = "FacName";
            this.hFacName.HeaderText = "生产厂家";
            this.hFacName.Name = "hFacName";
            this.hFacName.ReadOnly = true;
            this.hFacName.Width = 125;
            // 
            // hPyCode
            // 
            this.hPyCode.DataPropertyName = "PyCode";
            this.hPyCode.HeaderText = "PyCode";
            this.hPyCode.Name = "hPyCode";
            this.hPyCode.ReadOnly = true;
            this.hPyCode.Visible = false;
            // 
            // hWbCode
            // 
            this.hWbCode.DataPropertyName = "WbCode";
            this.hWbCode.HeaderText = "WbCode";
            this.hWbCode.Name = "hWbCode";
            this.hWbCode.ReadOnly = true;
            this.hWbCode.Visible = false;
            // 
            // FrmMIMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 678);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmMIMatch";
            this.ShowIcon = false;
            this.Text = "目录匹配";
            this.OpenWindowBefore += new System.EventHandler(this.FrmMIMatch_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar4)).EndInit();
            this.bar4.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExamGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYbItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.bar3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.ButtonItem btnUpdateMIlog;
        private DevComponents.DotNetBar.ButtonItem btnAutoMatch;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.LabelItem 医院目录;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ComboBoxItem cmbHosLogType;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem btnHisLogExport;
        private DevComponents.DotNetBar.Controls.TextBoxX tbHosSerch;
        protected DevComponents.DotNetBar.Controls.DataGridViewX dgvExamGroup;
        private DevComponents.DotNetBar.CheckBoxItem cbUnMatch;
        private DevComponents.DotNetBar.CheckBoxItem cbUnStop;
        private DevComponents.DotNetBar.Bar bar3;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMISerch;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.ComboBoxItem cmbMILogType;
        private DevComponents.DotNetBar.LabelItem labelItem5;
        private DevComponents.DotNetBar.ButtonItem btnMILogImport;
        protected DevComponents.DotNetBar.Controls.DataGridViewX dgvYbItemList;
        protected DevComponents.DotNetBar.Controls.DataGridViewX dgvMatchList;
        private DevComponents.DotNetBar.Bar bar4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbMatch;
        private DevComponents.DotNetBar.LabelItem labelItem6;
        private DevComponents.DotNetBar.LabelItem labelItem8;
        private DevComponents.DotNetBar.LabelItem labelItem9;
        private DevComponents.DotNetBar.ButtonItem btnAuditReset;
        private DevComponents.DotNetBar.ButtonItem btnMatch;
        private DevComponents.DotNetBar.ButtonItem btnAllReset;
        private DevComponents.DotNetBar.ButtonItem btnMatchDel;
        private DevComponents.DotNetBar.ButtonItem btnUploadMatch;
        private DevComponents.DotNetBar.ButtonItem btnDownloadAudit;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.DotNetBar.LabelItem labelItem7;
        private DevComponents.DotNetBar.ComboBoxItem cmbMIType;
        private DevComponents.DotNetBar.LabelItem labelItem10;
        private DevComponents.DotNetBar.ButtonItem btnMatchExport;
        private DevComponents.DotNetBar.ButtonItem btnImportMatchLog;
        private DevComponents.DotNetBar.LabelItem labelItem11;
        private DevComponents.DotNetBar.ButtonItem btnHisLogQuery;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.ButtonItem btnSaveMiLog;
        private DevComponents.DotNetBar.ButtonItem btnSaveMatch;
        private DevComponents.DotNetBar.ComboBoxItem cmbMatchState;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.DotNetBar.ButtonItem btnMatchQuery;
        private DevComponents.DotNetBar.LabelItem labelItem12;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factory;
        private System.Windows.Forms.DataGridViewTextBoxColumn PYCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WBCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsYBFlag;
        private DevComponents.DotNetBar.ButtonItem btnDrugImport;
        private DevComponents.DotNetBar.ButtonItem btnItemImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Memo;
        private System.Windows.Forms.DataGridViewTextBoxColumn YBItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn YBItemLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenterSellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenterUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenterDrugDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn CenterSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn dCenterItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dCenterItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dHospItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dHospItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private DevComponents.DotNetBar.ButtonItem btnUpdateLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn MedicareItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn hWbCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn hPyCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn hFacName;
        private System.Windows.Forms.DataGridViewTextBoxColumn hItemLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn hSellPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn hUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn hSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn hMedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn hStockID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
    }
}