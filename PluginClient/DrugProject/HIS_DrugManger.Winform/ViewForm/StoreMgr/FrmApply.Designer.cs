namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmApply
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmApply));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.dgBillHead = new EfwControls.CustomControl.DataGrid();
            this.ApplyHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNOH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegEmpNameH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegTimeH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditEmpNameH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditTimeH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemarkH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btnAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btnEdit = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmbDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ckNoAudit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckAudit = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.timeBillDate = new EfwControls.CustomControl.StatDateTime();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1084, 662);
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
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 376);
            this.expandableSplitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(1084, 4);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 3;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.panelEx5);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 40);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1084, 340);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 2;
            this.panelEx4.Text = "panelEx4";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.dgBillHead);
            this.panelEx5.Controls.Add(this.bar2);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(1084, 340);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 2;
            this.panelEx5.Text = "panelEx5";
            // 
            // dgBillHead
            // 
            this.dgBillHead.AllowSortWhenClickColumnHeader = false;
            this.dgBillHead.AllowUserToAddRows = false;
            this.dgBillHead.AllowUserToDeleteRows = false;
            this.dgBillHead.AllowUserToResizeColumns = false;
            this.dgBillHead.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgBillHead.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBillHead.BackgroundColor = System.Drawing.Color.White;
            this.dgBillHead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBillHead.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgBillHead.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ApplyHeadID,
            this.BatchNOH,
            this.RegEmpNameH,
            this.RegTimeH,
            this.AuditEmpNameH,
            this.AuditTimeH,
            this.Column1,
            this.RemarkH});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBillHead.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgBillHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBillHead.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBillHead.HighlightSelectedColumnHeaders = false;
            this.dgBillHead.Location = new System.Drawing.Point(0, 25);
            this.dgBillHead.Name = "dgBillHead";
            this.dgBillHead.ReadOnly = true;
            this.dgBillHead.RowHeadersWidth = 25;
            this.dgBillHead.RowTemplate.Height = 23;
            this.dgBillHead.SelectAllSignVisible = false;
            this.dgBillHead.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillHead.SeqVisible = true;
            this.dgBillHead.SetCustomStyle = false;
            this.dgBillHead.Size = new System.Drawing.Size(1084, 315);
            this.dgBillHead.TabIndex = 0;
            this.dgBillHead.CurrentCellChanged += new System.EventHandler(this.dgBillHead_CurrentCellChanged);
            // 
            // ApplyHeadID
            // 
            this.ApplyHeadID.DataPropertyName = "ApplyHeadID";
            this.ApplyHeadID.HeaderText = "编号";
            this.ApplyHeadID.Name = "ApplyHeadID";
            this.ApplyHeadID.ReadOnly = true;
            this.ApplyHeadID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ApplyHeadID.Visible = false;
            // 
            // BatchNOH
            // 
            this.BatchNOH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BatchNOH.DataPropertyName = "BillNO";
            this.BatchNOH.HeaderText = "单据号";
            this.BatchNOH.MinimumWidth = 100;
            this.BatchNOH.Name = "BatchNOH";
            this.BatchNOH.ReadOnly = true;
            this.BatchNOH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RegEmpNameH
            // 
            this.RegEmpNameH.DataPropertyName = "RegEmpName";
            this.RegEmpNameH.HeaderText = "登记人";
            this.RegEmpNameH.MinimumWidth = 80;
            this.RegEmpNameH.Name = "RegEmpNameH";
            this.RegEmpNameH.ReadOnly = true;
            this.RegEmpNameH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RegTimeH
            // 
            this.RegTimeH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RegTimeH.DataPropertyName = "RegTime";
            this.RegTimeH.HeaderText = "登记时间";
            this.RegTimeH.Name = "RegTimeH";
            this.RegTimeH.ReadOnly = true;
            this.RegTimeH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AuditEmpNameH
            // 
            this.AuditEmpNameH.DataPropertyName = "AuditEmpName";
            this.AuditEmpNameH.HeaderText = "审核人";
            this.AuditEmpNameH.MinimumWidth = 80;
            this.AuditEmpNameH.Name = "AuditEmpNameH";
            this.AuditEmpNameH.ReadOnly = true;
            this.AuditEmpNameH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // AuditTimeH
            // 
            this.AuditTimeH.DataPropertyName = "AuditTime";
            this.AuditTimeH.HeaderText = "审核时间";
            this.AuditTimeH.Name = "AuditTimeH";
            this.AuditTimeH.ReadOnly = true;
            this.AuditTimeH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AuditTimeH.Width = 246;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ToDeptName";
            this.Column1.HeaderText = "申请科室";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RemarkH
            // 
            this.RemarkH.DataPropertyName = "Remark";
            this.RemarkH.HeaderText = "备注";
            this.RemarkH.Name = "RemarkH";
            this.RemarkH.ReadOnly = true;
            this.RemarkH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAdd,
            this.btnEdit,
            this.btnDelete});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.PaddingLeft = 22;
            this.bar2.Size = new System.Drawing.Size(1084, 25);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 17;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btnAdd
            // 
            this.btnAdd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Text = "新增申请单(&F2)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Text = "修改申请单(&F3)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "删除申请单(&F4)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx7);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 380);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1084, 282);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            this.panelEx3.Text = "panelEx3";
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.dgDetails);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx7.Location = new System.Drawing.Point(0, 0);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(1084, 282);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 3;
            this.panelEx7.Text = "panelEx7";
            // 
            // dgDetails
            // 
            this.dgDetails.AllowSortWhenClickColumnHeader = false;
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.BatchNO,
            this.Amount,
            this.UnitName,
            this.FactAmount,
            this.totalNum,
            this.StockPrice,
            this.StockFee,
            this.RetailPrice,
            this.RetailFee});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetails.HideSelectionCardWhenCustomInput = false;
            this.dgDetails.HighlightSelectedColumnHeaders = false;
            this.dgDetails.IsInputNumSelectedCard = true;
            this.dgDetails.IsShowLetter = false;
            this.dgDetails.IsShowPage = false;
            this.dgDetails.Location = new System.Drawing.Point(0, 0);
            this.dgDetails.MultiSelect = false;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersWidth = 25;
            this.dgDetails.RowTemplate.Height = 23;
            this.dgDetails.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 0;
            dataGridViewSelectionCard1.CardColumn = null;
            dataGridViewSelectionCard1.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard1.DataSource = null;
            dataGridViewSelectionCard1.FilterResult = null;
            dataGridViewSelectionCard1.IsPage = true;
            dataGridViewSelectionCard1.Memo = null;
            dataGridViewSelectionCard1.PageTotalRecord = 0;
            dataGridViewSelectionCard1.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard1.QueryFieldsString = "";
            dataGridViewSelectionCard1.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard1.ShowCardColumns = null;
            this.dgDetails.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.SelectionNumKeyBoards = null;
            this.dgDetails.SeqVisible = true;
            this.dgDetails.SetCustomStyle = false;
            this.dgDetails.Size = new System.Drawing.Size(1084, 282);
            this.dgDetails.TabIndex = 1;
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编号";
            this.DrugID.MinimumWidth = 40;
            this.DrugID.Name = "DrugID";
            this.DrugID.ReadOnly = true;
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 55;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "药品名称";
            this.ChemName.MinimumWidth = 80;
            this.ChemName.Name = "ChemName";
            this.ChemName.ReadOnly = true;
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.MinimumWidth = 60;
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ProductName.Width = 150;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.ReadOnly = true;
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "申请数";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 55;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 40;
            // 
            // FactAmount
            // 
            this.FactAmount.DataPropertyName = "FactAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FactAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.FactAmount.HeaderText = "出库数";
            this.FactAmount.Name = "FactAmount";
            this.FactAmount.ReadOnly = true;
            this.FactAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FactAmount.Width = 55;
            // 
            // totalNum
            // 
            this.totalNum.DataPropertyName = "totalNum";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.totalNum.DefaultCellStyle = dataGridViewCellStyle7;
            this.totalNum.HeaderText = "库存量";
            this.totalNum.Name = "totalNum";
            this.totalNum.ReadOnly = true;
            this.totalNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalNum.Width = 55;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.StockPrice.HeaderText = "进货价";
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.ReadOnly = true;
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 55;
            // 
            // StockFee
            // 
            this.StockFee.DataPropertyName = "StockFee";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "0.00";
            this.StockFee.DefaultCellStyle = dataGridViewCellStyle9;
            this.StockFee.HeaderText = "进货金额";
            this.StockFee.Name = "StockFee";
            this.StockFee.ReadOnly = true;
            this.StockFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockFee.Width = 65;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle10;
            this.RetailPrice.HeaderText = "零售价";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 65;
            // 
            // RetailFee
            // 
            this.RetailFee.DataPropertyName = "RetailFee";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "0.00";
            this.RetailFee.DefaultCellStyle = dataGridViewCellStyle11;
            this.RetailFee.HeaderText = "零售金额";
            this.RetailFee.Name = "RetailFee";
            this.RetailFee.ReadOnly = true;
            this.RetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailFee.Width = 65;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cmbDept);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.ckNoAudit);
            this.panelEx2.Controls.Add(this.ckAudit);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.timeBillDate);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1084, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // cmbDept
            // 
            this.cmbDept.DisplayMember = "DeptName";
            this.cmbDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.ItemHeight = 15;
            this.cmbDept.Location = new System.Drawing.Point(212, 10);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(90, 21);
            this.cmbDept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDept.TabIndex = 94;
            this.cmbDept.ValueMember = "DeptID";
            this.cmbDept.SelectedValueChanged += new System.EventHandler(this.cmbDept_SelectedValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(178, 11);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(31, 18);
            this.labelX2.TabIndex = 93;
            this.labelX2.Text = "库房";
            // 
            // ckNoAudit
            // 
            this.ckNoAudit.AutoSize = true;
            // 
            // 
            // 
            this.ckNoAudit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckNoAudit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckNoAudit.Checked = true;
            this.ckNoAudit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckNoAudit.CheckValue = "Y";
            this.ckNoAudit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckNoAudit.Location = new System.Drawing.Point(22, 11);
            this.ckNoAudit.Name = "ckNoAudit";
            this.ckNoAudit.Size = new System.Drawing.Size(64, 18);
            this.ckNoAudit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckNoAudit.TabIndex = 6;
            this.ckNoAudit.Text = "未审核";
            // 
            // ckAudit
            // 
            this.ckAudit.AutoSize = true;
            // 
            // 
            // 
            this.ckAudit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckAudit.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckAudit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckAudit.Location = new System.Drawing.Point(92, 11);
            this.ckAudit.Name = "ckAudit";
            this.ckAudit.Size = new System.Drawing.Size(64, 18);
            this.ckAudit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckAudit.TabIndex = 7;
            this.ckAudit.Text = "已审核";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(806, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(725, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(324, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "申请日期";
            // 
            // timeBillDate
            // 
            this.timeBillDate.BackColor = System.Drawing.Color.Transparent;
            this.timeBillDate.DateFormat = "yyyy-MM-dd";
            this.timeBillDate.DateWidth = 120;
            this.timeBillDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timeBillDate.Location = new System.Drawing.Point(383, 10);
            this.timeBillDate.Name = "timeBillDate";
            this.timeBillDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.timeBillDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.timeBillDate.Size = new System.Drawing.Size(320, 21);
            this.timeBillDate.TabIndex = 2;
            // 
            // FrmApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmApply";
            this.Text = "领药申请管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmApply_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmApply_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.StatDateTime timeBillDate;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btnAdd;
        private DevComponents.DotNetBar.ButtonItem btnDelete;
        private DevComponents.DotNetBar.ButtonItem btnEdit;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckNoAudit;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckAudit;
        private EfwControls.CustomControl.DataGrid dgBillHead;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDept;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyHeadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNOH;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegEmpNameH;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegTimeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditEmpNameH;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditTimeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemarkH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailFee;
    }
}