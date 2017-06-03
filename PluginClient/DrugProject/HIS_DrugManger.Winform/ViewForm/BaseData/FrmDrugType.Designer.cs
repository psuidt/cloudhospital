namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmDrugType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrugType));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dgDrugChildType = new EfwControls.CustomControl.DataGrid();
            this.CTypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeIDName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeCID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.btnAddC = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txtChildTypeName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSaveC = new DevComponents.DotNetBar.ButtonX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgDrugType = new EfwControls.CustomControl.DataGrid();
            this.TypeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PYCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WBCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.btnNewP = new DevComponents.DotNetBar.ButtonX();
            this.btn_SaveP = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtDrugTypeA = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugChildType)).BeginInit();
            this.panelEx9.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugType)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1006, 462);
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
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgDrugChildType);
            this.panelEx2.Controls.Add(this.panelEx9);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(475, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(531, 462);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 51;
            this.panelEx2.Text = "panelEx2";
            // 
            // dgDrugChildType
            // 
            this.dgDrugChildType.AllowSortWhenClickColumnHeader = false;
            this.dgDrugChildType.AllowUserToAddRows = false;
            this.dgDrugChildType.AllowUserToDeleteRows = false;
            this.dgDrugChildType.AllowUserToResizeColumns = false;
            this.dgDrugChildType.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDrugChildType.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDrugChildType.BackgroundColor = System.Drawing.Color.White;
            this.dgDrugChildType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrugChildType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDrugChildType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CTypeID,
            this.CTypeName,
            this.Column2,
            this.Column3,
            this.TypeIDName,
            this.typeCID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDrugChildType.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDrugChildType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDrugChildType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDrugChildType.HighlightSelectedColumnHeaders = false;
            this.dgDrugChildType.Location = new System.Drawing.Point(0, 0);
            this.dgDrugChildType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgDrugChildType.MultiSelect = false;
            this.dgDrugChildType.Name = "dgDrugChildType";
            this.dgDrugChildType.ReadOnly = true;
            this.dgDrugChildType.RowHeadersWidth = 25;
            this.dgDrugChildType.RowTemplate.Height = 23;
            this.dgDrugChildType.SelectAllSignVisible = false;
            this.dgDrugChildType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDrugChildType.SeqVisible = true;
            this.dgDrugChildType.SetCustomStyle = false;
            this.dgDrugChildType.Size = new System.Drawing.Size(531, 422);
            this.dgDrugChildType.TabIndex = 31;
            this.dgDrugChildType.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDrugChildType_CellContentClick);
            this.dgDrugChildType.CurrentCellChanged += new System.EventHandler(this.dgDrugChildType_CurrentCellChanged);
            this.dgDrugChildType.Click += new System.EventHandler(this.dgDrugChildType_Click);
            // 
            // CTypeID
            // 
            this.CTypeID.DataPropertyName = "CTypeID";
            this.CTypeID.HeaderText = "编码";
            this.CTypeID.Name = "CTypeID";
            this.CTypeID.ReadOnly = true;
            this.CTypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CTypeID.Width = 65;
            // 
            // CTypeName
            // 
            this.CTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CTypeName.DataPropertyName = "CTypeName";
            this.CTypeName.HeaderText = "类型名称";
            this.CTypeName.Name = "CTypeName";
            this.CTypeName.ReadOnly = true;
            this.CTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "PYCode";
            this.Column2.HeaderText = "拼音码";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "WBCode";
            this.Column3.HeaderText = "五笔码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TypeIDName
            // 
            this.TypeIDName.DataPropertyName = "PNAME";
            this.TypeIDName.HeaderText = "父类型";
            this.TypeIDName.Name = "TypeIDName";
            this.TypeIDName.ReadOnly = true;
            this.TypeIDName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // typeCID
            // 
            this.typeCID.DataPropertyName = "TypeID";
            this.typeCID.HeaderText = "父类型ID";
            this.typeCID.Name = "typeCID";
            this.typeCID.ReadOnly = true;
            this.typeCID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.typeCID.Visible = false;
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Controls.Add(this.btnAddC);
            this.panelEx9.Controls.Add(this.labelX7);
            this.panelEx9.Controls.Add(this.txtChildTypeName);
            this.panelEx9.Controls.Add(this.btnSaveC);
            this.panelEx9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx9.Location = new System.Drawing.Point(0, 422);
            this.panelEx9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(531, 40);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.TabIndex = 50;
            // 
            // btnAddC
            // 
            this.btnAddC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddC.Image = ((System.Drawing.Image)(resources.GetObject("btnAddC.Image")));
            this.btnAddC.Location = new System.Drawing.Point(261, 9);
            this.btnAddC.Name = "btnAddC";
            this.btnAddC.Size = new System.Drawing.Size(110, 22);
            this.btnAddC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddC.TabIndex = 13;
            this.btnAddC.Text = "新增子类型(&A)";
            this.btnAddC.Click += new System.EventHandler(this.btnAddC_Click);
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(22, 11);
            this.labelX7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX7.Name = "labelX7";
            this.labelX7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX7.Size = new System.Drawing.Size(68, 18);
            this.labelX7.TabIndex = 199;
            this.labelX7.Text = "子类型名称";
            // 
            // txtChildTypeName
            // 
            // 
            // 
            // 
            this.txtChildTypeName.Border.Class = "TextBoxBorder";
            this.txtChildTypeName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChildTypeName.Location = new System.Drawing.Point(96, 10);
            this.txtChildTypeName.MaxLength = 25;
            this.txtChildTypeName.Name = "txtChildTypeName";
            this.txtChildTypeName.Size = new System.Drawing.Size(159, 21);
            this.txtChildTypeName.TabIndex = 12;
            this.txtChildTypeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChildTypeName_KeyPress);
            // 
            // btnSaveC
            // 
            this.btnSaveC.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveC.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveC.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveC.Image")));
            this.btnSaveC.Location = new System.Drawing.Point(377, 9);
            this.btnSaveC.Name = "btnSaveC";
            this.btnSaveC.Size = new System.Drawing.Size(110, 22);
            this.btnSaveC.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveC.TabIndex = 14;
            this.btnSaveC.Text = "保存子类型(&E)";
            this.btnSaveC.Click += new System.EventHandler(this.btnSaveC_Click);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(469, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 462);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 52;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgDrugType);
            this.panelEx4.Controls.Add(this.panelEx5);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(469, 462);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgDrugType
            // 
            this.dgDrugType.AllowSortWhenClickColumnHeader = false;
            this.dgDrugType.AllowUserToAddRows = false;
            this.dgDrugType.AllowUserToDeleteRows = false;
            this.dgDrugType.AllowUserToResizeColumns = false;
            this.dgDrugType.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDrugType.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDrugType.BackgroundColor = System.Drawing.Color.White;
            this.dgDrugType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrugType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgDrugType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TypeID,
            this.TypeName,
            this.PYCode,
            this.WBCode});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDrugType.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgDrugType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDrugType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDrugType.HighlightSelectedColumnHeaders = false;
            this.dgDrugType.Location = new System.Drawing.Point(0, 0);
            this.dgDrugType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgDrugType.MultiSelect = false;
            this.dgDrugType.Name = "dgDrugType";
            this.dgDrugType.ReadOnly = true;
            this.dgDrugType.RowHeadersWidth = 25;
            this.dgDrugType.RowTemplate.Height = 23;
            this.dgDrugType.SelectAllSignVisible = false;
            this.dgDrugType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDrugType.SeqVisible = true;
            this.dgDrugType.SetCustomStyle = false;
            this.dgDrugType.Size = new System.Drawing.Size(469, 422);
            this.dgDrugType.TabIndex = 1;
            this.dgDrugType.CurrentCellChanged += new System.EventHandler(this.dgDrugType_CurrentCellChanged);
            this.dgDrugType.Click += new System.EventHandler(this.dgDrugType_Click);
            // 
            // TypeID
            // 
            this.TypeID.DataPropertyName = "TypeID";
            this.TypeID.HeaderText = "编码";
            this.TypeID.Name = "TypeID";
            this.TypeID.ReadOnly = true;
            this.TypeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TypeID.Width = 50;
            // 
            // TypeName
            // 
            this.TypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TypeName.DataPropertyName = "TypeName";
            this.TypeName.HeaderText = "类型名称";
            this.TypeName.Name = "TypeName";
            this.TypeName.ReadOnly = true;
            this.TypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PYCode
            // 
            this.PYCode.DataPropertyName = "PYCode";
            this.PYCode.HeaderText = "拼音码";
            this.PYCode.Name = "PYCode";
            this.PYCode.ReadOnly = true;
            this.PYCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // WBCode
            // 
            this.WBCode.DataPropertyName = "WBCode";
            this.WBCode.HeaderText = "五笔码";
            this.WBCode.Name = "WBCode";
            this.WBCode.ReadOnly = true;
            this.WBCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.btnNewP);
            this.panelEx5.Controls.Add(this.btn_SaveP);
            this.panelEx5.Controls.Add(this.labelX2);
            this.panelEx5.Controls.Add(this.txtDrugTypeA);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx5.Location = new System.Drawing.Point(0, 422);
            this.panelEx5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(469, 40);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 21;
            // 
            // btnNewP
            // 
            this.btnNewP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNewP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNewP.Image = ((System.Drawing.Image)(resources.GetObject("btnNewP.Image")));
            this.btnNewP.Location = new System.Drawing.Point(236, 10);
            this.btnNewP.Name = "btnNewP";
            this.btnNewP.Size = new System.Drawing.Size(100, 22);
            this.btnNewP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNewP.TabIndex = 11;
            this.btnNewP.Text = "新增类型(&N)";
            this.btnNewP.Click += new System.EventHandler(this.btnNewP_Click);
            // 
            // btn_SaveP
            // 
            this.btn_SaveP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_SaveP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_SaveP.Image = ((System.Drawing.Image)(resources.GetObject("btn_SaveP.Image")));
            this.btn_SaveP.Location = new System.Drawing.Point(342, 10);
            this.btn_SaveP.Name = "btn_SaveP";
            this.btn_SaveP.Size = new System.Drawing.Size(100, 22);
            this.btn_SaveP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_SaveP.TabIndex = 5;
            this.btn_SaveP.Text = "保存类型(&S)";
            this.btn_SaveP.Click += new System.EventHandler(this.btn_SaveP_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(22, 11);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "类型名称";
            // 
            // txtDrugTypeA
            // 
            // 
            // 
            // 
            this.txtDrugTypeA.Border.Class = "TextBoxBorder";
            this.txtDrugTypeA.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDrugTypeA.Location = new System.Drawing.Point(82, 10);
            this.txtDrugTypeA.MaxLength = 25;
            this.txtDrugTypeA.Name = "txtDrugTypeA";
            this.txtDrugTypeA.Size = new System.Drawing.Size(148, 21);
            this.txtDrugTypeA.TabIndex = 3;
            this.txtDrugTypeA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDrugTypeA_KeyPress);
            // 
            // FrmDrugType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 462);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmDrugType";
            this.Text = "药品类型";
            this.Load += new System.EventHandler(this.FrmDrugType_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugChildType)).EndInit();
            this.panelEx9.ResumeLayout(false);
            this.panelEx9.PerformLayout();
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugType)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgDrugChildType;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private EfwControls.CustomControl.DataGrid dgDrugType;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.ButtonX btn_SaveP;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDrugTypeA;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private DevComponents.DotNetBar.ButtonX btnSaveC;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtChildTypeName;
        private DevComponents.DotNetBar.ButtonX btnNewP;
        private DevComponents.DotNetBar.ButtonX btnAddC;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PYCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WBCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeIDName;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeCID;
    }
}