namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmDrugDept
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDrugDept));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetail = new EfwControls.CustomControl.DataGrid();
            this.BusinessType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusiTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btn_stop = new DevComponents.DotNetBar.ButtonX();
            this.btn_start = new DevComponents.DotNetBar.ButtonX();
            this.txtUseState = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgDrugDeptList = new EfwControls.CustomControl.DataGrid();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopFlagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptDicID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.ckb_ds = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ckb_dw = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btn_del = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btn_add = new DevComponents.DotNetBar.ButtonX();
            this.txtCard_Dept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugDeptList)).BeginInit();
            this.panelEx7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(984, 662);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx6);
            this.panelEx2.Controls.Add(this.panelEx5);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Location = new System.Drawing.Point(348, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(636, 662);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.dgDetail);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 37);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(636, 625);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 14;
            this.panelEx6.Text = "panelEx6";
            // 
            // dgDetail
            // 
            this.dgDetail.AllowSortWhenClickColumnHeader = false;
            this.dgDetail.AllowUserToAddRows = false;
            this.dgDetail.AllowUserToDeleteRows = false;
            this.dgDetail.AllowUserToResizeColumns = false;
            this.dgDetail.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgDetail.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BusinessType,
            this.CurrSequence,
            this.BusiTypeName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetail.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetail.HighlightSelectedColumnHeaders = false;
            this.dgDetail.Location = new System.Drawing.Point(0, 0);
            this.dgDetail.MultiSelect = false;
            this.dgDetail.Name = "dgDetail";
            this.dgDetail.ReadOnly = true;
            this.dgDetail.RowHeadersWidth = 30;
            this.dgDetail.RowTemplate.Height = 23;
            this.dgDetail.SelectAllSignVisible = false;
            this.dgDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetail.SeqVisible = true;
            this.dgDetail.SetCustomStyle = true;
            this.dgDetail.Size = new System.Drawing.Size(636, 625);
            this.dgDetail.TabIndex = 0;
            // 
            // BusinessType
            // 
            this.BusinessType.DataPropertyName = "BusinessType";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BusinessType.DefaultCellStyle = dataGridViewCellStyle3;
            this.BusinessType.HeaderText = "业务编号";
            this.BusinessType.Name = "BusinessType";
            this.BusinessType.ReadOnly = true;
            this.BusinessType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CurrSequence
            // 
            this.CurrSequence.DataPropertyName = "CurrSequence";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CurrSequence.DefaultCellStyle = dataGridViewCellStyle4;
            this.CurrSequence.HeaderText = "当前单据号";
            this.CurrSequence.Name = "CurrSequence";
            this.CurrSequence.ReadOnly = true;
            this.CurrSequence.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CurrSequence.Width = 80;
            // 
            // BusiTypeName
            // 
            this.BusiTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.BusiTypeName.DataPropertyName = "BusiTypeName";
            this.BusiTypeName.HeaderText = "业务名称";
            this.BusiTypeName.Name = "BusiTypeName";
            this.BusiTypeName.ReadOnly = true;
            this.BusiTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.btnClose);
            this.panelEx5.Controls.Add(this.btn_stop);
            this.panelEx5.Controls.Add(this.btn_start);
            this.panelEx5.Controls.Add(this.txtUseState);
            this.panelEx5.Controls.Add(this.labelX3);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(636, 37);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 13;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(429, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_stop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_stop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_stop.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop.Image")));
            this.btn_stop.Location = new System.Drawing.Point(346, 7);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 22);
            this.btn_stop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_stop.TabIndex = 11;
            this.btn_stop.Text = "停用(&T)";
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_start.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_start.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_start.Image = ((System.Drawing.Image)(resources.GetObject("btn_start.Image")));
            this.btn_start.Location = new System.Drawing.Point(263, 7);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 22);
            this.btn_start.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_start.TabIndex = 10;
            this.btn_start.Text = "启用(&Q)";
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // txtUseState
            // 
            // 
            // 
            // 
            this.txtUseState.Border.Class = "TextBoxBorder";
            this.txtUseState.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUseState.Enabled = false;
            this.txtUseState.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUseState.Location = new System.Drawing.Point(109, 8);
            this.txtUseState.Name = "txtUseState";
            this.txtUseState.ReadOnly = true;
            this.txtUseState.Size = new System.Drawing.Size(136, 21);
            this.txtUseState.TabIndex = 4;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(22, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(81, 18);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "当前科室状态";
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandableControl = this.panelEx3;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(338, 0);
            this.expandableSplitter1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(10, 662);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 5;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgDrugDeptList);
            this.panelEx3.Controls.Add(this.panelEx7);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(338, 662);
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
            // dgDrugDeptList
            // 
            this.dgDrugDeptList.AllowSortWhenClickColumnHeader = false;
            this.dgDrugDeptList.AllowUserToAddRows = false;
            this.dgDrugDeptList.AllowUserToDeleteRows = false;
            this.dgDrugDeptList.AllowUserToResizeColumns = false;
            this.dgDrugDeptList.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.AliceBlue;
            this.dgDrugDeptList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgDrugDeptList.BackgroundColor = System.Drawing.Color.White;
            this.dgDrugDeptList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrugDeptList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgDrugDeptList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeptName,
            this.DeptTypeName,
            this.StopFlagName,
            this.DeptCode,
            this.DeptDicID,
            this.DeptType,
            this.StopFlag,
            this.DeptID,
            this.WorkID,
            this.CheckStatus});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDrugDeptList.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgDrugDeptList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDrugDeptList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDrugDeptList.HighlightSelectedColumnHeaders = false;
            this.dgDrugDeptList.Location = new System.Drawing.Point(0, 0);
            this.dgDrugDeptList.MultiSelect = false;
            this.dgDrugDeptList.Name = "dgDrugDeptList";
            this.dgDrugDeptList.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDrugDeptList.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgDrugDeptList.RowHeadersWidth = 30;
            this.dgDrugDeptList.RowTemplate.Height = 23;
            this.dgDrugDeptList.SelectAllSignVisible = false;
            this.dgDrugDeptList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDrugDeptList.SeqVisible = true;
            this.dgDrugDeptList.SetCustomStyle = true;
            this.dgDrugDeptList.Size = new System.Drawing.Size(338, 526);
            this.dgDrugDeptList.TabIndex = 0;
            this.dgDrugDeptList.CurrentCellChanged += new System.EventHandler(this.dg_drugDeptList_CurrentCellChanged);
            this.dgDrugDeptList.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dg_drugDeptList_RowPrePaint);
            // 
            // DeptName
            // 
            this.DeptName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.HeaderText = "名称";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DeptTypeName
            // 
            this.DeptTypeName.DataPropertyName = "DeptTypeName";
            this.DeptTypeName.HeaderText = "科室类型";
            this.DeptTypeName.Name = "DeptTypeName";
            this.DeptTypeName.ReadOnly = true;
            this.DeptTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptTypeName.Width = 65;
            // 
            // StopFlagName
            // 
            this.StopFlagName.DataPropertyName = "StopFlagName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StopFlagName.DefaultCellStyle = dataGridViewCellStyle8;
            this.StopFlagName.HeaderText = "启用标识";
            this.StopFlagName.Name = "StopFlagName";
            this.StopFlagName.ReadOnly = true;
            this.StopFlagName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StopFlagName.Width = 65;
            // 
            // DeptCode
            // 
            this.DeptCode.DataPropertyName = "DeptCode";
            this.DeptCode.HeaderText = "库房编码";
            this.DeptCode.Name = "DeptCode";
            this.DeptCode.ReadOnly = true;
            this.DeptCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptCode.Visible = false;
            // 
            // DeptDicID
            // 
            this.DeptDicID.DataPropertyName = "DeptDicID";
            this.DeptDicID.HeaderText = "DeptDicID";
            this.DeptDicID.Name = "DeptDicID";
            this.DeptDicID.ReadOnly = true;
            this.DeptDicID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptDicID.Visible = false;
            // 
            // DeptType
            // 
            this.DeptType.DataPropertyName = "DeptType";
            this.DeptType.HeaderText = "DeptType";
            this.DeptType.Name = "DeptType";
            this.DeptType.ReadOnly = true;
            this.DeptType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptType.Visible = false;
            // 
            // StopFlag
            // 
            this.StopFlag.DataPropertyName = "StopFlag";
            this.StopFlag.HeaderText = "StopFlag";
            this.StopFlag.Name = "StopFlag";
            this.StopFlag.ReadOnly = true;
            this.StopFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StopFlag.Visible = false;
            // 
            // DeptID
            // 
            this.DeptID.DataPropertyName = "DeptID";
            this.DeptID.HeaderText = "DeptID";
            this.DeptID.Name = "DeptID";
            this.DeptID.ReadOnly = true;
            this.DeptID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptID.Visible = false;
            // 
            // WorkID
            // 
            this.WorkID.DataPropertyName = "WorkID";
            this.WorkID.HeaderText = "WorkID";
            this.WorkID.Name = "WorkID";
            this.WorkID.ReadOnly = true;
            this.WorkID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WorkID.Visible = false;
            // 
            // CheckStatus
            // 
            this.CheckStatus.DataPropertyName = "CheckStatus";
            this.CheckStatus.HeaderText = "CheckStatus";
            this.CheckStatus.Name = "CheckStatus";
            this.CheckStatus.ReadOnly = true;
            this.CheckStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CheckStatus.Visible = false;
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.ckb_ds);
            this.panelEx7.Controls.Add(this.labelX2);
            this.panelEx7.Controls.Add(this.ckb_dw);
            this.panelEx7.Controls.Add(this.btn_del);
            this.panelEx7.Controls.Add(this.labelX1);
            this.panelEx7.Controls.Add(this.btn_add);
            this.panelEx7.Controls.Add(this.txtCard_Dept);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx7.Location = new System.Drawing.Point(0, 526);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(338, 136);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 1;
            // 
            // ckb_ds
            // 
            this.ckb_ds.AutoSize = true;
            // 
            // 
            // 
            this.ckb_ds.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckb_ds.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckb_ds.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_ds.Location = new System.Drawing.Point(90, 64);
            this.ckb_ds.Name = "ckb_ds";
            this.ckb_ds.Size = new System.Drawing.Size(51, 18);
            this.ckb_ds.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckb_ds.TabIndex = 4;
            this.ckb_ds.Text = "药房";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 10F);
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(21, 62);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(62, 19);
            this.labelX2.TabIndex = 11;
            this.labelX2.Text = "科室类型";
            // 
            // ckb_dw
            // 
            this.ckb_dw.AutoSize = true;
            // 
            // 
            // 
            this.ckb_dw.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckb_dw.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckb_dw.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckb_dw.Location = new System.Drawing.Point(147, 64);
            this.ckb_dw.Name = "ckb_dw";
            this.ckb_dw.Size = new System.Drawing.Size(51, 18);
            this.ckb_dw.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckb_dw.TabIndex = 5;
            this.ckb_dw.Text = "药库";
            // 
            // btn_del
            // 
            this.btn_del.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_del.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_del.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_del.Image = ((System.Drawing.Image)(resources.GetObject("btn_del.Image")));
            this.btn_del.Location = new System.Drawing.Point(231, 98);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 22);
            this.btn_del.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_del.TabIndex = 10;
            this.btn_del.Text = "删除(&D)";
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10F);
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(21, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(62, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "选择科室";
            // 
            // btn_add
            // 
            this.btn_add.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_add.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_add.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_add.Image = ((System.Drawing.Image)(resources.GetObject("btn_add.Image")));
            this.btn_add.Location = new System.Drawing.Point(150, 98);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 22);
            this.btn_add.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_add.TabIndex = 9;
            this.btn_add.Text = "新增(&N)";
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // txtCard_Dept
            // 
            // 
            // 
            // 
            this.txtCard_Dept.Border.Class = "TextBoxBorder";
            this.txtCard_Dept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCard_Dept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtCard_Dept.ButtonCustom.Image")));
            this.txtCard_Dept.ButtonCustom.Visible = true;
            this.txtCard_Dept.CardColumn = null;
            this.txtCard_Dept.DisplayField = "";
            this.txtCard_Dept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCard_Dept.IsEnterShowCard = true;
            this.txtCard_Dept.IsNumSelected = false;
            this.txtCard_Dept.IsPage = true;
            this.txtCard_Dept.IsShowLetter = false;
            this.txtCard_Dept.IsShowPage = false;
            this.txtCard_Dept.IsShowSeq = true;
            this.txtCard_Dept.Location = new System.Drawing.Point(90, 28);
            this.txtCard_Dept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtCard_Dept.MemberField = "";
            this.txtCard_Dept.MemberValue = null;
            this.txtCard_Dept.Name = "txtCard_Dept";
            this.txtCard_Dept.QueryFields = new string[] {
        ""};
            this.txtCard_Dept.QueryFieldsString = "";
            this.txtCard_Dept.SelectedValue = null;
            this.txtCard_Dept.ShowCardColumns = null;
            this.txtCard_Dept.ShowCardDataSource = null;
            this.txtCard_Dept.ShowCardHeight = 0;
            this.txtCard_Dept.ShowCardWidth = 0;
            this.txtCard_Dept.Size = new System.Drawing.Size(212, 21);
            this.txtCard_Dept.TabIndex = 1;
            // 
            // FrmDrugDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmDrugDept";
            this.Text = "药剂科室设置";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDrugDept_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDrugDeptList)).EndInit();
            this.panelEx7.ResumeLayout(false);
            this.panelEx7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgDrugDeptList;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btn_start;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUseState;
        private DevComponents.DotNetBar.ButtonX btn_stop;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private EfwControls.CustomControl.DataGrid dgDetail;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.TextBoxCard txtCard_Dept;
        private DevComponents.DotNetBar.ButtonX btn_del;
        private DevComponents.DotNetBar.ButtonX btn_add;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckb_ds;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckb_dw;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopFlagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptDicID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptType;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CheckStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinessType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusiTypeName;
        private DevComponents.DotNetBar.ButtonX btnClose;
    }
}