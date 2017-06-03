namespace HIS_IPNurse.Winform.ViewForm
{
    partial class FrmExecBillRecord
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.cbOrder = new System.Windows.Forms.CheckBox();
            this.grdOrderList = new EfwControls.CustomControl.DataGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.设置已打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置未打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.cbPatList = new System.Windows.Forms.CheckBox();
            this.grdPatList = new EfwControls.CustomControl.DataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dTimeFee = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cmbExecuteType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.cbPrinted = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbUnprint = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbTemp = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbLong = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Printer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dTimeFee)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx5);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1153, 620);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.cbOrder);
            this.panelEx3.Controls.Add(this.grdOrderList);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(250, 35);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(903, 585);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 8;
            // 
            // cbOrder
            // 
            this.cbOrder.AutoSize = true;
            this.cbOrder.Checked = true;
            this.cbOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbOrder.Location = new System.Drawing.Point(38, 11);
            this.cbOrder.Name = "cbOrder";
            this.cbOrder.Size = new System.Drawing.Size(15, 14);
            this.cbOrder.TabIndex = 11;
            this.cbOrder.UseVisualStyleBackColor = true;
            this.cbOrder.CheckedChanged += new System.EventHandler(this.cbOrder_CheckedChanged);
            // 
            // grdOrderList
            // 
            this.grdOrderList.AllowSortWhenClickColumnHeader = false;
            this.grdOrderList.AllowUserToAddRows = false;
            this.grdOrderList.AllowUserToDeleteRows = false;
            this.grdOrderList.AllowUserToResizeColumns = false;
            this.grdOrderList.AllowUserToResizeRows = false;
            this.grdOrderList.BackgroundColor = System.Drawing.Color.White;
            this.grdOrderList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdOrderList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdOrderList.ColumnHeadersHeight = 34;
            this.grdOrderList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.cDocName,
            this.colOrderName,
            this.dataGridViewTextBoxColumn10,
            this.Column4,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.Printer,
            this.Column8,
            this.Column6});
            this.grdOrderList.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdOrderList.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdOrderList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOrderList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdOrderList.HighlightSelectedColumnHeaders = false;
            this.grdOrderList.Location = new System.Drawing.Point(0, 0);
            this.grdOrderList.MultiSelect = false;
            this.grdOrderList.Name = "grdOrderList";
            this.grdOrderList.ReadOnly = true;
            this.grdOrderList.RowHeadersWidth = 30;
            this.grdOrderList.RowTemplate.Height = 23;
            this.grdOrderList.SelectAllSignVisible = false;
            this.grdOrderList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdOrderList.SeqVisible = true;
            this.grdOrderList.SetCustomStyle = false;
            this.grdOrderList.Size = new System.Drawing.Size(903, 585);
            this.grdOrderList.TabIndex = 10;
            this.grdOrderList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdOrderList_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置已打印ToolStripMenuItem,
            this.设置未打印ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(137, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 设置已打印ToolStripMenuItem
            // 
            this.设置已打印ToolStripMenuItem.Name = "设置已打印ToolStripMenuItem";
            this.设置已打印ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.设置已打印ToolStripMenuItem.Text = "设置已打印";
            this.设置已打印ToolStripMenuItem.Click += new System.EventHandler(this.设置已打印ToolStripMenuItem_Click);
            // 
            // 设置未打印ToolStripMenuItem
            // 
            this.设置未打印ToolStripMenuItem.Name = "设置未打印ToolStripMenuItem";
            this.设置未打印ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.设置未打印ToolStripMenuItem.Text = "设置未打印";
            this.设置未打印ToolStripMenuItem.Click += new System.EventHandler(this.设置未打印ToolStripMenuItem_Click);
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
            this.expandableSplitter1.Location = new System.Drawing.Point(244, 35);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 585);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 7;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.cbPatList);
            this.panelEx5.Controls.Add(this.grdPatList);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx5.Location = new System.Drawing.Point(0, 35);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(244, 585);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 6;
            // 
            // cbPatList
            // 
            this.cbPatList.AutoSize = true;
            this.cbPatList.Checked = true;
            this.cbPatList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPatList.Location = new System.Drawing.Point(40, 10);
            this.cbPatList.Name = "cbPatList";
            this.cbPatList.Size = new System.Drawing.Size(15, 14);
            this.cbPatList.TabIndex = 1;
            this.cbPatList.UseVisualStyleBackColor = true;
            this.cbPatList.CheckedChanged += new System.EventHandler(this.cbPatList_CheckedChanged);
            // 
            // grdPatList
            // 
            this.grdPatList.AllowSortWhenClickColumnHeader = false;
            this.grdPatList.AllowUserToAddRows = false;
            this.grdPatList.AllowUserToDeleteRows = false;
            this.grdPatList.AllowUserToResizeColumns = false;
            this.grdPatList.AllowUserToResizeRows = false;
            this.grdPatList.BackgroundColor = System.Drawing.Color.White;
            this.grdPatList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdPatList.ColumnHeadersHeight = 34;
            this.grdPatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPatList.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPatList.HighlightSelectedColumnHeaders = false;
            this.grdPatList.Location = new System.Drawing.Point(0, 0);
            this.grdPatList.MultiSelect = false;
            this.grdPatList.Name = "grdPatList";
            this.grdPatList.ReadOnly = true;
            this.grdPatList.RowHeadersWidth = 30;
            this.grdPatList.RowTemplate.Height = 23;
            this.grdPatList.SelectAllSignVisible = false;
            this.grdPatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatList.SeqVisible = true;
            this.grdPatList.SetCustomStyle = false;
            this.grdPatList.Size = new System.Drawing.Size(244, 585);
            this.grdPatList.TabIndex = 0;
            this.grdPatList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatList_CellClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "checked";
            this.Column1.HeaderText = "选";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "BedCode";
            this.Column2.HeaderText = "床号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 40;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "PatName";
            this.Column3.HeaderText = "姓名";
            this.Column3.MinimumWidth = 70;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "SerialNumber";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column5.HeaderText = "住院号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 80;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.dTimeFee);
            this.panelEx2.Controls.Add(this.cmbExecuteType);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.cmbDept);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Controls.Add(this.btnPrint);
            this.panelEx2.Controls.Add(this.cbPrinted);
            this.panelEx2.Controls.Add(this.cbUnprint);
            this.panelEx2.Controls.Add(this.cbTemp);
            this.panelEx2.Controls.Add(this.cbLong);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1153, 35);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(395, 7);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 22);
            this.labelX3.TabIndex = 10010;
            this.labelX3.Text = "费用日期";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dTimeFee
            // 
            // 
            // 
            // 
            this.dTimeFee.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dTimeFee.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dTimeFee.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dTimeFee.ButtonDropDown.Visible = true;
            this.dTimeFee.CustomFormat = "yyyy-MM-dd";
            this.dTimeFee.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dTimeFee.IsPopupCalendarOpen = false;
            this.dTimeFee.Location = new System.Drawing.Point(453, 7);
            // 
            // 
            // 
            this.dTimeFee.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dTimeFee.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dTimeFee.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dTimeFee.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dTimeFee.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dTimeFee.MonthCalendar.DisplayMonth = new System.DateTime(2016, 11, 1, 0, 0, 0, 0);
            this.dTimeFee.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dTimeFee.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dTimeFee.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dTimeFee.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dTimeFee.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dTimeFee.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dTimeFee.MonthCalendar.TodayButtonVisible = true;
            this.dTimeFee.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dTimeFee.Name = "dTimeFee";
            this.dTimeFee.Size = new System.Drawing.Size(110, 21);
            this.dTimeFee.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dTimeFee.TabIndex = 10009;
            this.dTimeFee.Value = new System.DateTime(2016, 11, 8, 16, 49, 36, 0);
            // 
            // cmbExecuteType
            // 
            this.cmbExecuteType.DisplayMember = "Text";
            this.cmbExecuteType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbExecuteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExecuteType.FormattingEnabled = true;
            this.cmbExecuteType.ItemHeight = 17;
            this.cmbExecuteType.Location = new System.Drawing.Point(252, 6);
            this.cmbExecuteType.Name = "cmbExecuteType";
            this.cmbExecuteType.Size = new System.Drawing.Size(137, 23);
            this.cmbExecuteType.TabIndex = 10008;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(204, 7);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(44, 22);
            this.labelX2.TabIndex = 10007;
            this.labelX2.Text = "执行单";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cmbDept
            // 
            this.cmbDept.DisplayMember = "Text";
            this.cmbDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.ItemHeight = 17;
            this.cmbDept.Location = new System.Drawing.Point(61, 6);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(137, 23);
            this.cmbDept.TabIndex = 10006;
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = global::HIS_IPNurse.Winform.Properties.Resources.搜索;
            this.btnQuery.Location = new System.Drawing.Point(902, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 23;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = global::HIS_IPNurse.Winform.Properties.Resources.关闭;
            this.btnClose.Location = new System.Drawing.Point(1067, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Image = global::HIS_IPNurse.Winform.Properties.Resources.打印;
            this.btnPrint.Location = new System.Drawing.Point(986, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 22);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbPrinted
            // 
            // 
            // 
            // 
            this.cbPrinted.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbPrinted.Location = new System.Drawing.Point(750, 7);
            this.cbPrinted.Name = "cbPrinted";
            this.cbPrinted.Size = new System.Drawing.Size(65, 22);
            this.cbPrinted.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPrinted.TabIndex = 17;
            this.cbPrinted.Text = "已打印";
            // 
            // cbUnprint
            // 
            // 
            // 
            // 
            this.cbUnprint.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbUnprint.Checked = true;
            this.cbUnprint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUnprint.CheckValue = "Y";
            this.cbUnprint.Location = new System.Drawing.Point(685, 7);
            this.cbUnprint.Name = "cbUnprint";
            this.cbUnprint.Size = new System.Drawing.Size(65, 22);
            this.cbUnprint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbUnprint.TabIndex = 16;
            this.cbUnprint.Text = "未打印";
            // 
            // cbTemp
            // 
            // 
            // 
            // 
            this.cbTemp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbTemp.Checked = true;
            this.cbTemp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTemp.CheckValue = "Y";
            this.cbTemp.Location = new System.Drawing.Point(629, 6);
            this.cbTemp.Name = "cbTemp";
            this.cbTemp.Size = new System.Drawing.Size(50, 22);
            this.cbTemp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTemp.TabIndex = 15;
            this.cbTemp.Text = "临时";
            // 
            // cbLong
            // 
            // 
            // 
            // 
            this.cbLong.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbLong.Checked = true;
            this.cbLong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLong.CheckValue = "Y";
            this.cbLong.Location = new System.Drawing.Point(570, 6);
            this.cbLong.Name = "cbLong";
            this.cbLong.Size = new System.Drawing.Size(50, 22);
            this.cbLong.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLong.TabIndex = 14;
            this.cbLong.Text = "长期";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(25, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(30, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "科室";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "checked";
            this.dataGridViewCheckBoxColumn1.FalseValue = "0";
            this.dataGridViewCheckBoxColumn1.HeaderText = "";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.TrueValue = "1";
            this.dataGridViewCheckBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OrderCategory";
            this.dataGridViewTextBoxColumn1.HeaderText = "长/临";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "BedNo";
            this.dataGridViewTextBoxColumn3.HeaderText = "床号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 40;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PatName";
            this.dataGridViewTextBoxColumn4.HeaderText = "姓名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "SerialNumber";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText = "住院号";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 90;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ExecDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "MM-dd HH:mm";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn6.HeaderText = "费用时间";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 80;
            // 
            // cDocName
            // 
            this.cDocName.DataPropertyName = "DocName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.cDocName.DefaultCellStyle = dataGridViewCellStyle4;
            this.cDocName.HeaderText = "开嘱医生";
            this.cDocName.MinimumWidth = 60;
            this.cDocName.Name = "cDocName";
            this.cDocName.ReadOnly = true;
            this.cDocName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cDocName.Width = 60;
            // 
            // colOrderName
            // 
            this.colOrderName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colOrderName.DataPropertyName = "OrderContent";
            this.colOrderName.HeaderText = "医嘱内容";
            this.colOrderName.MinimumWidth = 250;
            this.colOrderName.Name = "colOrderName";
            this.colOrderName.ReadOnly = true;
            this.colOrderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Amount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn10.HeaderText = "剂量";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Entrust";
            this.Column4.HeaderText = "嘱托";
            this.Column4.MinimumWidth = 100;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Usage";
            this.dataGridViewTextBoxColumn12.HeaderText = "用法";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 80;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 80;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Frequency";
            this.dataGridViewTextBoxColumn13.HeaderText = "频次";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 60;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 60;
            // 
            // Printer
            // 
            this.Printer.DataPropertyName = "Printer";
            this.Printer.HeaderText = "打印人";
            this.Printer.Name = "Printer";
            this.Printer.ReadOnly = true;
            this.Printer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Printer.Width = 70;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "PrintDate";
            dataGridViewCellStyle6.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column8.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column8.HeaderText = "打印日期";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 130;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Entrust";
            this.Column6.HeaderText = "备注";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmExecBillRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 620);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmExecBillRecord";
            this.ShowIcon = false;
            this.Text = "执行单";
            this.OpenWindowBefore += new System.EventHandler(this.FrmExecBillRecord_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOrderList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dTimeFee)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDept;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbPrinted;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbUnprint;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbTemp;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbLong;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbExecuteType;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dTimeFee;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private System.Windows.Forms.CheckBox cbPatList;
        private EfwControls.CustomControl.DataGrid grdPatList;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.CheckBox cbOrder;
        private EfwControls.CustomControl.DataGrid grdOrderList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置已打印ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置未打印ToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Printer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDocName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    }
}