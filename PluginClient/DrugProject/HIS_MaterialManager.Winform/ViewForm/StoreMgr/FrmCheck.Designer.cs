namespace HIS_MaterialManage.Winform.ViewForm
{
    partial class FrmCheck
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheck));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.dgBillDetail = new EfwControls.CustomControl.DataGrid();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgBillHead = new EfwControls.CustomControl.DataGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.barBillMgr = new DevComponents.DotNetBar.Bar();
            this.btnAddBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdateBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnClearStatus = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.cmbDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.txtCheckBillNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkCheckBillNO = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dtpRegDate = new EfwControls.CustomControl.StatDateTime();
            this.chkRegTime = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radNotAudit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radAudit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillDetail)).BeginInit();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barBillMgr)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx5);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(984, 517);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.dgBillDetail);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx5.Location = new System.Drawing.Point(0, 202);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(984, 315);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 4;
            this.panelEx5.Text = "panelEx5";
            // 
            // dgBillDetail
            // 
            this.dgBillDetail.AllowSortWhenClickColumnHeader = false;
            this.dgBillDetail.AllowUserToAddRows = false;
            this.dgBillDetail.AllowUserToDeleteRows = false;
            this.dgBillDetail.AllowUserToResizeColumns = false;
            this.dgBillDetail.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgBillDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBillDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgBillDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgBillDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.BatchNO,
            this.ValidityDate,
            this.ActAmount,
            this.UnitName,
            this.FactAmount,
            this.Column17,
            this.Column18,
            this.Column4});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBillDetail.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgBillDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBillDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBillDetail.HighlightSelectedColumnHeaders = false;
            this.dgBillDetail.Location = new System.Drawing.Point(0, 0);
            this.dgBillDetail.MultiSelect = false;
            this.dgBillDetail.Name = "dgBillDetail";
            this.dgBillDetail.ReadOnly = true;
            this.dgBillDetail.RowHeadersWidth = 30;
            this.dgBillDetail.RowTemplate.Height = 23;
            this.dgBillDetail.SelectAllSignVisible = false;
            this.dgBillDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillDetail.SeqVisible = true;
            this.dgBillDetail.SetCustomStyle = true;
            this.dgBillDetail.Size = new System.Drawing.Size(984, 315);
            this.dgBillDetail.TabIndex = 1;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "MaterialID";
            this.Column11.HeaderText = "编码";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column12.DataPropertyName = "CenterMatName";
            this.Column12.HeaderText = "名称";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column13.DataPropertyName = "Spec";
            this.Column13.HeaderText = "规格";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column14.DataPropertyName = "ProductName";
            this.Column14.HeaderText = "生产厂家";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.ReadOnly = true;
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValidityDate
            // 
            this.ValidityDate.DataPropertyName = "ValidityDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "yyyy-MM-dd";
            this.ValidityDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.ValidityDate.HeaderText = "有效日期";
            this.ValidityDate.Name = "ValidityDate";
            this.ValidityDate.ReadOnly = true;
            this.ValidityDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValidityDate.Width = 80;
            // 
            // ActAmount
            // 
            this.ActAmount.DataPropertyName = "ActAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "0";
            dataGridViewCellStyle4.NullValue = "0";
            this.ActAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ActAmount.HeaderText = "账存包装数";
            this.ActAmount.Name = "ActAmount";
            this.ActAmount.ReadOnly = true;
            this.ActAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ActAmount.Width = 80;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "包装单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 70;
            // 
            // FactAmount
            // 
            this.FactAmount.DataPropertyName = "FactAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "0";
            dataGridViewCellStyle5.NullValue = "0";
            this.FactAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.FactAmount.HeaderText = "盘存包装数";
            this.FactAmount.Name = "FactAmount";
            this.FactAmount.ReadOnly = true;
            this.FactAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FactAmount.Width = 80;
            // 
            // Column17
            // 
            this.Column17.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "0.00";
            dataGridViewCellStyle6.NullValue = "0.00";
            this.Column17.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column17.HeaderText = "零售价格";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column17.Width = 65;
            // 
            // Column18
            // 
            this.Column18.DataPropertyName = "ActRetailFee";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            dataGridViewCellStyle7.NullValue = "0.00";
            this.Column18.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column18.HeaderText = "账存金额";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column18.Width = 65;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FactRetailFee";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "0.00";
            dataGridViewCellStyle8.NullValue = "0.00";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column4.HeaderText = "盘存金额";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 65;
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandableSplitter1.ExpandableControl = this.panelEx4;
            this.expandableSplitter1.ExpandActionClick = false;
            this.expandableSplitter1.ExpandActionDoubleClick = true;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 195);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(984, 7);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 6;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgBillHead);
            this.panelEx4.Controls.Add(this.barBillMgr);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx4.Location = new System.Drawing.Point(0, 40);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(984, 155);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 3;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgBillHead
            // 
            this.dgBillHead.AllowSortWhenClickColumnHeader = false;
            this.dgBillHead.AllowUserToAddRows = false;
            this.dgBillHead.AllowUserToDeleteRows = false;
            this.dgBillHead.AllowUserToResizeColumns = false;
            this.dgBillHead.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue;
            this.dgBillHead.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgBillHead.BackgroundColor = System.Drawing.Color.White;
            this.dgBillHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillHead.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgBillHead.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.AuditFlag});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBillHead.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgBillHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBillHead.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBillHead.HighlightSelectedColumnHeaders = false;
            this.dgBillHead.Location = new System.Drawing.Point(0, 25);
            this.dgBillHead.MultiSelect = false;
            this.dgBillHead.Name = "dgBillHead";
            this.dgBillHead.ReadOnly = true;
            this.dgBillHead.RowHeadersWidth = 30;
            this.dgBillHead.RowTemplate.Height = 23;
            this.dgBillHead.SelectAllSignVisible = false;
            this.dgBillHead.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillHead.SeqVisible = true;
            this.dgBillHead.SetCustomStyle = true;
            this.dgBillHead.Size = new System.Drawing.Size(984, 130);
            this.dgBillHead.TabIndex = 2;
            this.dgBillHead.CurrentCellChanged += new System.EventHandler(this.dgBillHead_CurrentCellChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "BillNO";
            this.dataGridViewTextBoxColumn1.HeaderText = "单据号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 110;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "RegTime";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.Format = "yyyy-MM-dd HH:mm:ss";
            dataGridViewCellStyle12.NullValue = null;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn2.HeaderText = "登记时间";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "RegEmpName";
            this.dataGridViewTextBoxColumn3.HeaderText = "登记人";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AuditTime";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.Format = "yyyy-MM-dd HH:mm:ss";
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn4.HeaderText = "审核时间";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "AuditEmpName";
            this.dataGridViewTextBoxColumn5.HeaderText = "审核人";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BusiType";
            this.Column1.HeaderText = "业务类型";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "AuditNO";
            this.Column2.HeaderText = "审核单号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 90;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "Remark";
            this.Column3.HeaderText = "备注";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AuditFlag
            // 
            this.AuditFlag.DataPropertyName = "AuditFlag";
            this.AuditFlag.HeaderText = "审核标志";
            this.AuditFlag.Name = "AuditFlag";
            this.AuditFlag.ReadOnly = true;
            this.AuditFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AuditFlag.Visible = false;
            // 
            // barBillMgr
            // 
            this.barBillMgr.AntiAlias = true;
            this.barBillMgr.Dock = System.Windows.Forms.DockStyle.Top;
            this.barBillMgr.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barBillMgr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.barBillMgr.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddBill,
            this.btnUpdateBill,
            this.btnDelBill,
            this.btnClearStatus});
            this.barBillMgr.Location = new System.Drawing.Point(0, 0);
            this.barBillMgr.Name = "barBillMgr";
            this.barBillMgr.PaddingLeft = 22;
            this.barBillMgr.Size = new System.Drawing.Size(984, 25);
            this.barBillMgr.Stretch = true;
            this.barBillMgr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barBillMgr.TabIndex = 1;
            this.barBillMgr.TabStop = false;
            this.barBillMgr.Text = "bar1";
            // 
            // btnAddBill
            // 
            this.btnAddBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAddBill.Image = ((System.Drawing.Image)(resources.GetObject("btnAddBill.Image")));
            this.btnAddBill.Name = "btnAddBill";
            this.btnAddBill.Text = "新增单据(F2)";
            this.btnAddBill.Click += new System.EventHandler(this.btnAddBill_Click);
            // 
            // btnUpdateBill
            // 
            this.btnUpdateBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUpdateBill.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateBill.Image")));
            this.btnUpdateBill.Name = "btnUpdateBill";
            this.btnUpdateBill.Text = "修改单据(F3)";
            this.btnUpdateBill.Click += new System.EventHandler(this.btnUpdateBill_Click);
            // 
            // btnDelBill
            // 
            this.btnDelBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelBill.Image = ((System.Drawing.Image)(resources.GetObject("btnDelBill.Image")));
            this.btnDelBill.Name = "btnDelBill";
            this.btnDelBill.Text = "删除单据(F4)";
            this.btnDelBill.Click += new System.EventHandler(this.btnDelBill_Click);
            // 
            // btnClearStatus
            // 
            this.btnClearStatus.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClearStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnClearStatus.Image")));
            this.btnClearStatus.Name = "btnClearStatus";
            this.btnClearStatus.Text = "清除盘点状态(F5)";
            this.btnClearStatus.Click += new System.EventHandler(this.btnClearStatus_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.cmbDept);
            this.panelEx3.Controls.Add(this.labelX1);
            this.panelEx3.Controls.Add(this.btnQuery);
            this.panelEx3.Controls.Add(this.btnClose);
            this.panelEx3.Controls.Add(this.txtCheckBillNo);
            this.panelEx3.Controls.Add(this.chkCheckBillNO);
            this.panelEx3.Controls.Add(this.dtpRegDate);
            this.panelEx3.Controls.Add(this.chkRegTime);
            this.panelEx3.Controls.Add(this.radNotAudit);
            this.panelEx3.Controls.Add(this.radAudit);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(984, 40);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            // 
            // cmbDept
            // 
            this.cmbDept.DisplayMember = "DeptName";
            this.cmbDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.ItemHeight = 15;
            this.cmbDept.Location = new System.Drawing.Point(189, 10);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(97, 21);
            this.cmbDept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDept.TabIndex = 94;
            this.cmbDept.ValueMember = "DeptID";
            this.cmbDept.SelectedIndexChanged += new System.EventHandler(this.cmbDept_SelectedIndexChanged);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(155, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(31, 18);
            this.labelX1.TabIndex = 93;
            this.labelX1.Text = "库房";
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(804, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 7;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(885, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtCheckBillNo
            // 
            // 
            // 
            // 
            this.txtCheckBillNo.Border.Class = "TextBoxBorder";
            this.txtCheckBillNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCheckBillNo.Enabled = false;
            this.txtCheckBillNo.Location = new System.Drawing.Point(714, 10);
            this.txtCheckBillNo.Name = "txtCheckBillNo";
            this.txtCheckBillNo.Size = new System.Drawing.Size(84, 21);
            this.txtCheckBillNo.TabIndex = 5;
            // 
            // chkCheckBillNO
            // 
            this.chkCheckBillNO.AutoSize = true;
            // 
            // 
            // 
            this.chkCheckBillNO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkCheckBillNO.Font = new System.Drawing.Font("宋体", 9F);
            this.chkCheckBillNO.Location = new System.Drawing.Point(635, 11);
            this.chkCheckBillNO.Name = "chkCheckBillNO";
            this.chkCheckBillNO.Size = new System.Drawing.Size(76, 18);
            this.chkCheckBillNO.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkCheckBillNO.TabIndex = 4;
            this.chkCheckBillNO.Text = "审核单号";
            this.chkCheckBillNO.CheckedChanged += new System.EventHandler(this.chkCheckBillNO_CheckedChanged);
            // 
            // dtpRegDate
            // 
            this.dtpRegDate.BackColor = System.Drawing.Color.Transparent;
            this.dtpRegDate.DateFormat = "yyyy-MM-dd";
            this.dtpRegDate.DateWidth = 120;
            this.dtpRegDate.Enabled = false;
            this.dtpRegDate.Font = new System.Drawing.Font("宋体", 9F);
            this.dtpRegDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.dtpRegDate.Location = new System.Drawing.Point(374, 10);
            this.dtpRegDate.Name = "dtpRegDate";
            this.dtpRegDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtpRegDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtpRegDate.Size = new System.Drawing.Size(252, 21);
            this.dtpRegDate.TabIndex = 3;
            // 
            // chkRegTime
            // 
            this.chkRegTime.AutoSize = true;
            // 
            // 
            // 
            this.chkRegTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkRegTime.Font = new System.Drawing.Font("宋体", 9F);
            this.chkRegTime.Location = new System.Drawing.Point(295, 11);
            this.chkRegTime.Name = "chkRegTime";
            this.chkRegTime.Size = new System.Drawing.Size(76, 18);
            this.chkRegTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkRegTime.TabIndex = 2;
            this.chkRegTime.Text = "登记日期";
            this.chkRegTime.CheckedChanged += new System.EventHandler(this.chkRegTime_CheckedChanged);
            // 
            // radNotAudit
            // 
            this.radNotAudit.AutoSize = true;
            // 
            // 
            // 
            this.radNotAudit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radNotAudit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radNotAudit.Checked = true;
            this.radNotAudit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radNotAudit.CheckValue = "Y";
            this.radNotAudit.Font = new System.Drawing.Font("宋体", 9F);
            this.radNotAudit.Location = new System.Drawing.Point(12, 11);
            this.radNotAudit.Name = "radNotAudit";
            this.radNotAudit.Size = new System.Drawing.Size(64, 18);
            this.radNotAudit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radNotAudit.TabIndex = 1;
            this.radNotAudit.Text = "未审核";
            this.radNotAudit.CheckedChanged += new System.EventHandler(this.radNotAudit_CheckedChanged);
            // 
            // radAudit
            // 
            this.radAudit.AutoSize = true;
            // 
            // 
            // 
            this.radAudit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radAudit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radAudit.Font = new System.Drawing.Font("宋体", 9F);
            this.radAudit.Location = new System.Drawing.Point(82, 11);
            this.radAudit.Name = "radAudit";
            this.radAudit.Size = new System.Drawing.Size(64, 18);
            this.radAudit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radAudit.TabIndex = 0;
            this.radAudit.Text = "已审核";
            this.radAudit.CheckedChanged += new System.EventHandler(this.radNotAudit_CheckedChanged);
            // 
            // FrmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 517);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmCheck";
            this.Text = "盘点单管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmCheck_OpenWindowBefore);
            this.Load += new System.EventHandler(this.FrmCheck_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCheck_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBillDetail)).EndInit();
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barBillMgr)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Controls.CheckBoxX radAudit;
        private DevComponents.DotNetBar.Controls.CheckBoxX radNotAudit;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRegTime;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCheckBillNO;
        private EfwControls.CustomControl.StatDateTime dtpRegDate;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private EfwControls.CustomControl.DataGrid dgBillDetail;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCheckBillNo;
        private EfwControls.CustomControl.DataGrid dgBillHead;
        private DevComponents.DotNetBar.Bar barBillMgr;
        private DevComponents.DotNetBar.ButtonItem btnAddBill;
        private DevComponents.DotNetBar.ButtonItem btnUpdateBill;
        private DevComponents.DotNetBar.ButtonItem btnDelBill;
        private DevComponents.DotNetBar.ButtonItem btnClearStatus;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDept;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValidityDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
    }
}