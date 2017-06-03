namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmOutApply
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutApply));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.dgBillHead = new EfwControls.CustomControl.DataGrid();
            this.ApplyHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNOH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegEmpNameH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegTimeH = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.AuditEmpNameH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AuditTimeH = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemarkH = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.totalNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).BeginInit();
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
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(927, 389);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 166);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(927, 10);
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
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(927, 136);
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
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(927, 136);
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
            this.dgBillHead.Location = new System.Drawing.Point(0, 0);
            this.dgBillHead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgBillHead.Name = "dgBillHead";
            this.dgBillHead.ReadOnly = true;
            this.dgBillHead.RowHeadersWidth = 25;
            this.dgBillHead.RowTemplate.Height = 23;
            this.dgBillHead.SelectAllSignVisible = false;
            this.dgBillHead.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBillHead.SeqVisible = true;
            this.dgBillHead.SetCustomStyle = false;
            this.dgBillHead.Size = new System.Drawing.Size(927, 136);
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
            this.ApplyHeadID.Width = 55;
            // 
            // BatchNOH
            // 
            this.BatchNOH.DataPropertyName = "BillNO";
            this.BatchNOH.HeaderText = "单据号";
            this.BatchNOH.MinimumWidth = 100;
            this.BatchNOH.Name = "BatchNOH";
            this.BatchNOH.ReadOnly = true;
            this.BatchNOH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchNOH.Width = 120;
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
            // 
            // 
            // 
            this.RegTimeH.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.RegTimeH.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RegTimeH.CustomFormat = "yyyy-MM-dd";
            this.RegTimeH.DataPropertyName = "RegTime";
            this.RegTimeH.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.RegTimeH.HeaderText = "登记时间";
            this.RegTimeH.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            // 
            // 
            // 
            this.RegTimeH.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.RegTimeH.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RegTimeH.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.RegTimeH.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RegTimeH.MonthCalendar.DisplayMonth = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.RegTimeH.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.RegTimeH.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.RegTimeH.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.RegTimeH.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.RegTimeH.Name = "RegTimeH";
            this.RegTimeH.ReadOnly = true;
            this.RegTimeH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            this.AuditEmpNameH.Visible = false;
            // 
            // AuditTimeH
            // 
            // 
            // 
            // 
            this.AuditTimeH.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.AuditTimeH.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AuditTimeH.CustomFormat = "yyyy-MM-dd";
            this.AuditTimeH.DataPropertyName = "AuditTime";
            this.AuditTimeH.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.AuditTimeH.HeaderText = "审核时间";
            this.AuditTimeH.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            // 
            // 
            // 
            this.AuditTimeH.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.AuditTimeH.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AuditTimeH.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            // 
            // 
            // 
            this.AuditTimeH.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AuditTimeH.MonthCalendar.DisplayMonth = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.AuditTimeH.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.AuditTimeH.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.AuditTimeH.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.AuditTimeH.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.AuditTimeH.Name = "AuditTimeH";
            this.AuditTimeH.ReadOnly = true;
            this.AuditTimeH.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AuditTimeH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AuditTimeH.Visible = false;
            this.AuditTimeH.Width = 246;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ApplyDeptName";
            this.Column1.HeaderText = "申请科室";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RemarkH
            // 
            this.RemarkH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RemarkH.DataPropertyName = "Remark";
            this.RemarkH.HeaderText = "备注";
            this.RemarkH.Name = "RemarkH";
            this.RemarkH.ReadOnly = true;
            this.RemarkH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx7);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 176);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(927, 213);
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
            this.panelEx7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(927, 213);
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
            this.totalNum,
            this.StockFee,
            this.RetailFee,
            this.StockPrice,
            this.RetailPrice});
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
            this.dgDetails.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.dgDetails.Size = new System.Drawing.Size(927, 213);
            this.dgDetails.TabIndex = 1;
            // 
            // DrugID
            // 
            this.DrugID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编号";
            this.DrugID.MinimumWidth = 80;
            this.DrugID.Name = "DrugID";
            this.DrugID.ReadOnly = true;
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "药品名称";
            this.ChemName.MinimumWidth = 150;
            this.ChemName.Name = "ChemName";
            this.ChemName.ReadOnly = true;
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.MinimumWidth = 120;
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.MinimumWidth = 150;
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle6;
            this.Amount.HeaderText = "数量";
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
            this.UnitName.Width = 55;
            // 
            // totalNum
            // 
            this.totalNum.DataPropertyName = "totalNum";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            this.totalNum.DefaultCellStyle = dataGridViewCellStyle7;
            this.totalNum.HeaderText = "库存量";
            this.totalNum.Name = "totalNum";
            this.totalNum.ReadOnly = true;
            this.totalNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalNum.Width = 55;
            // 
            // StockFee
            // 
            this.StockFee.DataPropertyName = "StockFee";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "0.00";
            this.StockFee.DefaultCellStyle = dataGridViewCellStyle8;
            this.StockFee.HeaderText = "零售价";
            this.StockFee.Name = "StockFee";
            this.StockFee.ReadOnly = true;
            this.StockFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockFee.Width = 55;
            // 
            // RetailFee
            // 
            this.RetailFee.DataPropertyName = "RetailFee";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "0.00";
            this.RetailFee.DefaultCellStyle = dataGridViewCellStyle9;
            this.RetailFee.HeaderText = "零售金额";
            this.RetailFee.Name = "RetailFee";
            this.RetailFee.ReadOnly = true;
            this.RetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailFee.Width = 65;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle10;
            this.StockPrice.HeaderText = "进货价";
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.ReadOnly = true;
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 55;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle11;
            this.RetailPrice.HeaderText = "进货金额";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 65;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnOk);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(927, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Location = new System.Drawing.Point(12, 9);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 22);
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确认生成出库单";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(124, 9);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmOutApply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 389);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmOutApply";
            this.Text = "药品申请单信息";
            this.OpenWindowBefore += new System.EventHandler(this.FrmOutApply_OpenWindowBefore);
            this.Load += new System.EventHandler(this.FrmOutApply_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBillHead)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private EfwControls.CustomControl.DataGrid dgBillHead;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyHeadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNOH;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegEmpNameH;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn RegTimeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuditEmpNameH;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn AuditTimeH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemarkH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
    }
}