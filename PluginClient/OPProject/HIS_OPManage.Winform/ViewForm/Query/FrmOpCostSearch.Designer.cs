namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmOpCostSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOpCostSearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnPresDetailPrintAgain = new DevComponents.DotNetBar.ButtonX();
            this.btnPrintAgain = new DevComponents.DotNetBar.ButtonX();
            this.btnSetBegin = new DevComponents.DotNetBar.ButtonX();
            this.btnExcel = new DevComponents.DotNetBar.ButtonX();
            this.txtEndInvoiceNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtBeInvoiceNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.dtpEdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtpBdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtQueryContent = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtPresEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.txtPresDept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cmbPatType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.radAccountDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radChargeDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnCostDetail = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtChareEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.radAllRecord = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radBalanceRecord = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radRegRecord = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.radAllStatus = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radRefundStatus = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radNormalStatus = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.TabControl = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.dgPayMentInfo = new EfwControls.CustomControl.DataGrid();
            this.CostHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeInvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoctName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundingFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expPayMentInfo = new DevComponents.DotNetBar.ExpandablePanel();
            this.lblPayMentInfo = new DevComponents.DotNetBar.LabelX();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.dgItemInfo = new EfwControls.CustomControl.DataGrid();
            this.CostHeadIDItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regItemflag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostStatusItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expItemInfo = new DevComponents.DotNetBar.ExpandablePanel();
            this.lblItemInfo = new DevComponents.DotNetBar.LabelX();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBdate)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).BeginInit();
            this.TabControl.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayMentInfo)).BeginInit();
            this.expPayMentInfo.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItemInfo)).BeginInit();
            this.expItemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnPresDetailPrintAgain);
            this.panelEx1.Controls.Add(this.btnPrintAgain);
            this.panelEx1.Controls.Add(this.btnSetBegin);
            this.panelEx1.Controls.Add(this.btnExcel);
            this.panelEx1.Controls.Add(this.txtEndInvoiceNO);
            this.panelEx1.Controls.Add(this.labelX8);
            this.panelEx1.Controls.Add(this.txtBeInvoiceNO);
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.dtpEdate);
            this.panelEx1.Controls.Add(this.dtpBdate);
            this.panelEx1.Controls.Add(this.txtQueryContent);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.txtPresEmp);
            this.panelEx1.Controls.Add(this.labelX12);
            this.panelEx1.Controls.Add(this.txtPresDept);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.cmbPatType);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.radAccountDate);
            this.panelEx1.Controls.Add(this.radChargeDate);
            this.panelEx1.Controls.Add(this.btnCostDetail);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.txtChareEmp);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 89);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnPresDetailPrintAgain
            // 
            this.btnPresDetailPrintAgain.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPresDetailPrintAgain.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPresDetailPrintAgain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPresDetailPrintAgain.Location = new System.Drawing.Point(984, 52);
            this.btnPresDetailPrintAgain.Name = "btnPresDetailPrintAgain";
            this.btnPresDetailPrintAgain.Size = new System.Drawing.Size(112, 22);
            this.btnPresDetailPrintAgain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPresDetailPrintAgain.TabIndex = 58;
            this.btnPresDetailPrintAgain.Text = "费用清单补打(&O)";
            this.btnPresDetailPrintAgain.Click += new System.EventHandler(this.btnPresDetailPrintAgain_Click);
            // 
            // btnPrintAgain
            // 
            this.btnPrintAgain.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintAgain.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintAgain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrintAgain.Location = new System.Drawing.Point(903, 52);
            this.btnPrintAgain.Name = "btnPrintAgain";
            this.btnPrintAgain.Size = new System.Drawing.Size(75, 22);
            this.btnPrintAgain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintAgain.TabIndex = 57;
            this.btnPrintAgain.Text = "发票补打(&P)";
            this.btnPrintAgain.Visible = false;
            this.btnPrintAgain.Click += new System.EventHandler(this.btnPrintAgain_Click);
            // 
            // btnSetBegin
            // 
            this.btnSetBegin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSetBegin.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSetBegin.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetBegin.Image = ((System.Drawing.Image)(resources.GetObject("btnSetBegin.Image")));
            this.btnSetBegin.Location = new System.Drawing.Point(1102, 52);
            this.btnSetBegin.Name = "btnSetBegin";
            this.btnSetBegin.Size = new System.Drawing.Size(119, 22);
            this.btnSetBegin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSetBegin.TabIndex = 56;
            this.btnSetBegin.Text = "重置查询条件(&Z)";
            this.btnSetBegin.Click += new System.EventHandler(this.btnSetBegin_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExcel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExcel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(1065, 23);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 22);
            this.btnExcel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExcel.TabIndex = 55;
            this.btnExcel.Text = "导出(&E)";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtEndInvoiceNO
            // 
            // 
            // 
            // 
            this.txtEndInvoiceNO.Border.Class = "TextBoxBorder";
            this.txtEndInvoiceNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEndInvoiceNO.Location = new System.Drawing.Point(638, 62);
            this.txtEndInvoiceNO.Name = "txtEndInvoiceNO";
            this.txtEndInvoiceNO.Size = new System.Drawing.Size(120, 21);
            this.txtEndInvoiceNO.TabIndex = 54;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(564, 63);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(67, 23);
            this.labelX8.TabIndex = 53;
            this.labelX8.Text = "发票结束号";
            // 
            // txtBeInvoiceNO
            // 
            // 
            // 
            // 
            this.txtBeInvoiceNO.Border.Class = "TextBoxBorder";
            this.txtBeInvoiceNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBeInvoiceNO.Location = new System.Drawing.Point(412, 63);
            this.txtBeInvoiceNO.Name = "txtBeInvoiceNO";
            this.txtBeInvoiceNO.Size = new System.Drawing.Size(147, 21);
            this.txtBeInvoiceNO.TabIndex = 52;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(338, 61);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(67, 23);
            this.labelX7.TabIndex = 51;
            this.labelX7.Text = "发票起始号";
            // 
            // dtpEdate
            // 
            // 
            // 
            // 
            this.dtpEdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpEdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpEdate.ButtonDropDown.Visible = true;
            this.dtpEdate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEdate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpEdate.IsPopupCalendarOpen = false;
            this.dtpEdate.Location = new System.Drawing.Point(137, 34);
            // 
            // 
            // 
            this.dtpEdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpEdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEdate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpEdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpEdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEdate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.dtpEdate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpEdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpEdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpEdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpEdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpEdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpEdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpEdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpEdate.Name = "dtpEdate";
            this.dtpEdate.Size = new System.Drawing.Size(199, 21);
            this.dtpEdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpEdate.TabIndex = 50;
            // 
            // dtpBdate
            // 
            // 
            // 
            // 
            this.dtpBdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpBdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpBdate.ButtonDropDown.Visible = true;
            this.dtpBdate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBdate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpBdate.IsPopupCalendarOpen = false;
            this.dtpBdate.Location = new System.Drawing.Point(138, 8);
            // 
            // 
            // 
            this.dtpBdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpBdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBdate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpBdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpBdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBdate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.dtpBdate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpBdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpBdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpBdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpBdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpBdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpBdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpBdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpBdate.Name = "dtpBdate";
            this.dtpBdate.Size = new System.Drawing.Size(198, 21);
            this.dtpBdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpBdate.TabIndex = 49;
            // 
            // txtQueryContent
            // 
            // 
            // 
            // 
            this.txtQueryContent.Border.Class = "TextBoxBorder";
            this.txtQueryContent.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtQueryContent.Location = new System.Drawing.Point(137, 63);
            this.txtQueryContent.Name = "txtQueryContent";
            this.txtQueryContent.Size = new System.Drawing.Size(199, 21);
            this.txtQueryContent.TabIndex = 48;
            this.txtQueryContent.WatermarkText = "诊疗卡号、姓名、就诊号、发票号";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(80, 63);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(61, 23);
            this.labelX6.TabIndex = 47;
            this.labelX6.Text = "检索条件";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(577, 36);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(54, 21);
            this.labelX5.TabIndex = 46;
            this.labelX5.Text = "就诊医生";
            // 
            // txtPresEmp
            // 
            // 
            // 
            // 
            this.txtPresEmp.Border.Class = "TextBoxBorder";
            this.txtPresEmp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPresEmp.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtPresEmp.ButtonCustom.Image")));
            this.txtPresEmp.ButtonCustom.Visible = true;
            this.txtPresEmp.CardColumn = null;
            this.txtPresEmp.DisplayField = "";
            this.txtPresEmp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPresEmp.IsEnterShowCard = true;
            this.txtPresEmp.IsNumSelected = false;
            this.txtPresEmp.IsPage = true;
            this.txtPresEmp.IsShowLetter = false;
            this.txtPresEmp.IsShowPage = false;
            this.txtPresEmp.IsShowSeq = true;
            this.txtPresEmp.Location = new System.Drawing.Point(637, 34);
            this.txtPresEmp.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtPresEmp.MemberField = "";
            this.txtPresEmp.MemberValue = null;
            this.txtPresEmp.Name = "txtPresEmp";
            this.txtPresEmp.QueryFields = new string[] {
        ""};
            this.txtPresEmp.QueryFieldsString = "";
            this.txtPresEmp.SelectedValue = null;
            this.txtPresEmp.ShowCardColumns = null;
            this.txtPresEmp.ShowCardDataSource = null;
            this.txtPresEmp.ShowCardHeight = 0;
            this.txtPresEmp.ShowCardWidth = 0;
            this.txtPresEmp.Size = new System.Drawing.Size(121, 21);
            this.txtPresEmp.TabIndex = 45;
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX12.Location = new System.Drawing.Point(577, 9);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(54, 21);
            this.labelX12.TabIndex = 44;
            this.labelX12.Text = "就诊科室";
            // 
            // txtPresDept
            // 
            // 
            // 
            // 
            this.txtPresDept.Border.Class = "TextBoxBorder";
            this.txtPresDept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPresDept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtPresDept.ButtonCustom.Image")));
            this.txtPresDept.ButtonCustom.Visible = true;
            this.txtPresDept.CardColumn = null;
            this.txtPresDept.DisplayField = "";
            this.txtPresDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPresDept.IsEnterShowCard = true;
            this.txtPresDept.IsNumSelected = false;
            this.txtPresDept.IsPage = true;
            this.txtPresDept.IsShowLetter = false;
            this.txtPresDept.IsShowPage = false;
            this.txtPresDept.IsShowSeq = true;
            this.txtPresDept.Location = new System.Drawing.Point(637, 7);
            this.txtPresDept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtPresDept.MemberField = "";
            this.txtPresDept.MemberValue = null;
            this.txtPresDept.Name = "txtPresDept";
            this.txtPresDept.QueryFields = new string[] {
        ""};
            this.txtPresDept.QueryFieldsString = "";
            this.txtPresDept.SelectedValue = null;
            this.txtPresDept.ShowCardColumns = null;
            this.txtPresDept.ShowCardDataSource = null;
            this.txtPresDept.ShowCardHeight = 0;
            this.txtPresDept.ShowCardWidth = 0;
            this.txtPresDept.Size = new System.Drawing.Size(121, 21);
            this.txtPresDept.TabIndex = 43;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(349, 36);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(57, 23);
            this.labelX4.TabIndex = 42;
            this.labelX4.Text = "病人类型";
            // 
            // cmbPatType
            // 
            this.cmbPatType.DisplayMember = "Text";
            this.cmbPatType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatType.FormattingEnabled = true;
            this.cmbPatType.ItemHeight = 15;
            this.cmbPatType.Location = new System.Drawing.Point(412, 37);
            this.cmbPatType.Name = "cmbPatType";
            this.cmbPatType.Size = new System.Drawing.Size(147, 21);
            this.cmbPatType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPatType.TabIndex = 41;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(360, 7);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(45, 23);
            this.labelX3.TabIndex = 40;
            this.labelX3.Text = "收费员";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(80, 33);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(61, 23);
            this.labelX2.TabIndex = 39;
            this.labelX2.Text = "结束时间";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(80, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 38;
            this.labelX1.Text = "开始时间";
            // 
            // radAccountDate
            // 
            // 
            // 
            // 
            this.radAccountDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radAccountDate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radAccountDate.Location = new System.Drawing.Point(0, 32);
            this.radAccountDate.Name = "radAccountDate";
            this.radAccountDate.Size = new System.Drawing.Size(74, 23);
            this.radAccountDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radAccountDate.TabIndex = 37;
            this.radAccountDate.Text = "缴款时间";
            // 
            // radChargeDate
            // 
            // 
            // 
            // 
            this.radChargeDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radChargeDate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radChargeDate.Checked = true;
            this.radChargeDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radChargeDate.CheckValue = "Y";
            this.radChargeDate.Location = new System.Drawing.Point(0, 6);
            this.radChargeDate.Name = "radChargeDate";
            this.radChargeDate.Size = new System.Drawing.Size(74, 23);
            this.radChargeDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radChargeDate.TabIndex = 36;
            this.radChargeDate.Text = "收费时间";
            // 
            // btnCostDetail
            // 
            this.btnCostDetail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCostDetail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCostDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCostDetail.Location = new System.Drawing.Point(984, 23);
            this.btnCostDetail.Name = "btnCostDetail";
            this.btnCostDetail.Size = new System.Drawing.Size(75, 22);
            this.btnCostDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCostDetail.TabIndex = 35;
            this.btnCostDetail.Text = "查看明细(&T)";
            this.btnCostDetail.Click += new System.EventHandler(this.btnCostDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1146, 23);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(903, 23);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 29;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtChareEmp
            // 
            // 
            // 
            // 
            this.txtChareEmp.Border.Class = "TextBoxBorder";
            this.txtChareEmp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChareEmp.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtChareEmp.ButtonCustom.Image")));
            this.txtChareEmp.ButtonCustom.Visible = true;
            this.txtChareEmp.CardColumn = null;
            this.txtChareEmp.DisplayField = "";
            this.txtChareEmp.IsEnterShowCard = true;
            this.txtChareEmp.IsNumSelected = false;
            this.txtChareEmp.IsPage = true;
            this.txtChareEmp.IsShowLetter = false;
            this.txtChareEmp.IsShowPage = false;
            this.txtChareEmp.IsShowSeq = true;
            this.txtChareEmp.Location = new System.Drawing.Point(411, 7);
            this.txtChareEmp.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtChareEmp.MemberField = "";
            this.txtChareEmp.MemberValue = null;
            this.txtChareEmp.Name = "txtChareEmp";
            this.txtChareEmp.QueryFields = new string[] {
        ""};
            this.txtChareEmp.QueryFieldsString = "";
            this.txtChareEmp.SelectedValue = null;
            this.txtChareEmp.ShowCardColumns = null;
            this.txtChareEmp.ShowCardDataSource = null;
            this.txtChareEmp.ShowCardHeight = 0;
            this.txtChareEmp.ShowCardWidth = 0;
            this.txtChareEmp.Size = new System.Drawing.Size(148, 21);
            this.txtChareEmp.TabIndex = 28;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.radAllRecord);
            this.panelEx3.Controls.Add(this.radBalanceRecord);
            this.panelEx3.Controls.Add(this.radRegRecord);
            this.panelEx3.Location = new System.Drawing.Point(820, 10);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(77, 68);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 26;
            // 
            // radAllRecord
            // 
            // 
            // 
            // 
            this.radAllRecord.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radAllRecord.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radAllRecord.Checked = true;
            this.radAllRecord.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radAllRecord.CheckValue = "Y";
            this.radAllRecord.Location = new System.Drawing.Point(4, 3);
            this.radAllRecord.Name = "radAllRecord";
            this.radAllRecord.Size = new System.Drawing.Size(61, 23);
            this.radAllRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radAllRecord.TabIndex = 22;
            this.radAllRecord.Text = "全部";
            // 
            // radBalanceRecord
            // 
            // 
            // 
            // 
            this.radBalanceRecord.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radBalanceRecord.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radBalanceRecord.Location = new System.Drawing.Point(4, 42);
            this.radBalanceRecord.Name = "radBalanceRecord";
            this.radBalanceRecord.Size = new System.Drawing.Size(82, 23);
            this.radBalanceRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radBalanceRecord.TabIndex = 24;
            this.radBalanceRecord.Text = "收费记录";
            // 
            // radRegRecord
            // 
            // 
            // 
            // 
            this.radRegRecord.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radRegRecord.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radRegRecord.Location = new System.Drawing.Point(4, 21);
            this.radRegRecord.Name = "radRegRecord";
            this.radRegRecord.Size = new System.Drawing.Size(82, 23);
            this.radRegRecord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radRegRecord.TabIndex = 23;
            this.radRegRecord.Text = "挂号记录";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.radAllStatus);
            this.panelEx2.Controls.Add(this.radRefundStatus);
            this.panelEx2.Controls.Add(this.radNormalStatus);
            this.panelEx2.Location = new System.Drawing.Point(762, 10);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(52, 68);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 25;
            // 
            // radAllStatus
            // 
            // 
            // 
            // 
            this.radAllStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radAllStatus.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radAllStatus.Checked = true;
            this.radAllStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radAllStatus.CheckValue = "Y";
            this.radAllStatus.Location = new System.Drawing.Point(0, 2);
            this.radAllStatus.Name = "radAllStatus";
            this.radAllStatus.Size = new System.Drawing.Size(61, 23);
            this.radAllStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radAllStatus.TabIndex = 22;
            this.radAllStatus.Text = "全部";
            // 
            // radRefundStatus
            // 
            // 
            // 
            // 
            this.radRefundStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radRefundStatus.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radRefundStatus.Location = new System.Drawing.Point(0, 42);
            this.radRefundStatus.Name = "radRefundStatus";
            this.radRefundStatus.Size = new System.Drawing.Size(61, 23);
            this.radRefundStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radRefundStatus.TabIndex = 24;
            this.radRefundStatus.Text = "退费";
            // 
            // radNormalStatus
            // 
            // 
            // 
            // 
            this.radNormalStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radNormalStatus.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radNormalStatus.Location = new System.Drawing.Point(0, 21);
            this.radNormalStatus.Name = "radNormalStatus";
            this.radNormalStatus.Size = new System.Drawing.Size(61, 23);
            this.radNormalStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radNormalStatus.TabIndex = 23;
            this.radNormalStatus.Text = "正常";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.TabControl);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 89);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1264, 572);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            // 
            // TabControl
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.TabControl.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.TabControl.ControlBox.MenuBox.Name = "";
            this.TabControl.ControlBox.Name = "";
            this.TabControl.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.TabControl.ControlBox.MenuBox,
            this.TabControl.ControlBox.CloseBox});
            this.TabControl.Controls.Add(this.superTabControlPanel1);
            this.TabControl.Controls.Add(this.superTabControlPanel2);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.ReorderTabsEnabled = true;
            this.TabControl.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.TabControl.SelectedTabIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1264, 572);
            this.TabControl.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabControl.TabIndex = 1;
            this.TabControl.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1,
            this.superTabItem2});
            this.TabControl.Text = "superTabControl1";
            this.TabControl.SelectedTabChanged += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs>(this.TabControl_SelectedTabChanged);
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.dgPayMentInfo);
            this.superTabControlPanel1.Controls.Add(this.expPayMentInfo);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1264, 544);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // dgPayMentInfo
            // 
            this.dgPayMentInfo.AllowSortWhenClickColumnHeader = false;
            this.dgPayMentInfo.AllowUserToAddRows = false;
            this.dgPayMentInfo.AllowUserToDeleteRows = false;
            this.dgPayMentInfo.AllowUserToResizeColumns = false;
            this.dgPayMentInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgPayMentInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPayMentInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgPayMentInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPayMentInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgPayMentInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPayMentInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CostHeadID,
            this.regflag,
            this.CostDate,
            this.BeInvoiceNO,
            this.ChargeEmpName,
            this.CostStatus,
            this.CardNO,
            this.VisitNO,
            this.PatName,
            this.PatSex,
            this.PatAge,
            this.PatTypeName,
            this.DoctName,
            this.DeptName,
            this.TotalFee,
            this.RoundingFee});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPayMentInfo.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgPayMentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPayMentInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPayMentInfo.HighlightSelectedColumnHeaders = false;
            this.dgPayMentInfo.Location = new System.Drawing.Point(0, 0);
            this.dgPayMentInfo.Name = "dgPayMentInfo";
            this.dgPayMentInfo.ReadOnly = true;
            this.dgPayMentInfo.RowHeadersWidth = 25;
            this.dgPayMentInfo.RowTemplate.Height = 23;
            this.dgPayMentInfo.SelectAllSignVisible = false;
            this.dgPayMentInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPayMentInfo.SeqVisible = true;
            this.dgPayMentInfo.SetCustomStyle = false;
            this.dgPayMentInfo.Size = new System.Drawing.Size(1234, 544);
            this.dgPayMentInfo.TabIndex = 8;
            this.dgPayMentInfo.DoubleClick += new System.EventHandler(this.dgPayMentInfo_DoubleClick);
            // 
            // CostHeadID
            // 
            this.CostHeadID.DataPropertyName = "costHeadID";
            this.CostHeadID.HeaderText = "结算编码";
            this.CostHeadID.Name = "CostHeadID";
            this.CostHeadID.ReadOnly = true;
            this.CostHeadID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostHeadID.Width = 60;
            // 
            // regflag
            // 
            this.regflag.DataPropertyName = "regflag";
            this.regflag.HeaderText = "类型";
            this.regflag.Name = "regflag";
            this.regflag.ReadOnly = true;
            this.regflag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.regflag.Width = 40;
            // 
            // CostDate
            // 
            this.CostDate.DataPropertyName = "costDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Format = "yyyy-MM-dd HH:m:ss";
            this.CostDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.CostDate.HeaderText = "操作时间";
            this.CostDate.Name = "CostDate";
            this.CostDate.ReadOnly = true;
            this.CostDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostDate.Width = 130;
            // 
            // BeInvoiceNO
            // 
            this.BeInvoiceNO.DataPropertyName = "EndInvoiceNO";
            this.BeInvoiceNO.HeaderText = "票号";
            this.BeInvoiceNO.Name = "BeInvoiceNO";
            this.BeInvoiceNO.ReadOnly = true;
            this.BeInvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BeInvoiceNO.Width = 80;
            // 
            // ChargeEmpName
            // 
            this.ChargeEmpName.DataPropertyName = "ChargeEmpName";
            this.ChargeEmpName.HeaderText = "收费员";
            this.ChargeEmpName.Name = "ChargeEmpName";
            this.ChargeEmpName.ReadOnly = true;
            this.ChargeEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ChargeEmpName.Width = 80;
            // 
            // CostStatus
            // 
            this.CostStatus.DataPropertyName = "costStatus";
            this.CostStatus.HeaderText = "状态";
            this.CostStatus.Name = "CostStatus";
            this.CostStatus.ReadOnly = true;
            this.CostStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostStatus.Width = 50;
            // 
            // CardNO
            // 
            this.CardNO.DataPropertyName = "cardNO";
            this.CardNO.HeaderText = "卡号";
            this.CardNO.Name = "CardNO";
            this.CardNO.ReadOnly = true;
            this.CardNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardNO.Width = 80;
            // 
            // VisitNO
            // 
            this.VisitNO.DataPropertyName = "visitNO";
            this.VisitNO.HeaderText = "门诊流水号";
            this.VisitNO.Name = "VisitNO";
            this.VisitNO.ReadOnly = true;
            this.VisitNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VisitNO.Width = 80;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "病人姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 80;
            // 
            // PatSex
            // 
            this.PatSex.DataPropertyName = "PatSex";
            this.PatSex.HeaderText = "性别";
            this.PatSex.Name = "PatSex";
            this.PatSex.ReadOnly = true;
            this.PatSex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatSex.Width = 60;
            // 
            // PatAge
            // 
            this.PatAge.DataPropertyName = "Age";
            this.PatAge.HeaderText = "年龄";
            this.PatAge.Name = "PatAge";
            this.PatAge.ReadOnly = true;
            this.PatAge.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatAge.Width = 60;
            // 
            // PatTypeName
            // 
            this.PatTypeName.DataPropertyName = "patTypeName";
            this.PatTypeName.HeaderText = "病人类别";
            this.PatTypeName.Name = "PatTypeName";
            this.PatTypeName.ReadOnly = true;
            this.PatTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatTypeName.Width = 80;
            // 
            // DoctName
            // 
            this.DoctName.DataPropertyName = "DoctName";
            this.DoctName.HeaderText = "就诊医生";
            this.DoctName.Name = "DoctName";
            this.DoctName.ReadOnly = true;
            this.DoctName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DoctName.Width = 80;
            // 
            // DeptName
            // 
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.HeaderText = "就诊科室";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptName.Width = 80;
            // 
            // TotalFee
            // 
            this.TotalFee.DataPropertyName = "totalFee";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.TotalFee.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalFee.HeaderText = "总金额";
            this.TotalFee.Name = "TotalFee";
            this.TotalFee.ReadOnly = true;
            this.TotalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TotalFee.Width = 60;
            // 
            // RoundingFee
            // 
            this.RoundingFee.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.RoundingFee.DefaultCellStyle = dataGridViewCellStyle5;
            this.RoundingFee.HeaderText = "凑整金额";
            this.RoundingFee.Name = "RoundingFee";
            this.RoundingFee.ReadOnly = true;
            this.RoundingFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RoundingFee.Width = 60;
            // 
            // expPayMentInfo
            // 
            this.expPayMentInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.expPayMentInfo.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expPayMentInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expPayMentInfo.Controls.Add(this.lblPayMentInfo);
            this.expPayMentInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.expPayMentInfo.Expanded = false;
            this.expPayMentInfo.ExpandedBounds = new System.Drawing.Rectangle(1066, 0, 198, 538);
            this.expPayMentInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expPayMentInfo.HideControlsWhenCollapsed = true;
            this.expPayMentInfo.Location = new System.Drawing.Point(1234, 0);
            this.expPayMentInfo.Name = "expPayMentInfo";
            this.expPayMentInfo.Size = new System.Drawing.Size(30, 544);
            this.expPayMentInfo.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expPayMentInfo.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expPayMentInfo.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expPayMentInfo.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expPayMentInfo.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expPayMentInfo.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expPayMentInfo.Style.GradientAngle = 90;
            this.expPayMentInfo.TabIndex = 7;
            this.expPayMentInfo.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expPayMentInfo.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expPayMentInfo.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expPayMentInfo.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expPayMentInfo.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expPayMentInfo.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expPayMentInfo.TitleStyle.GradientAngle = 90;
            this.expPayMentInfo.TitleText = "支付方式汇总信息";
            // 
            // lblPayMentInfo
            // 
            this.lblPayMentInfo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblPayMentInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPayMentInfo.Location = new System.Drawing.Point(22, 40);
            this.lblPayMentInfo.Name = "lblPayMentInfo";
            this.lblPayMentInfo.Size = new System.Drawing.Size(164, 435);
            this.lblPayMentInfo.TabIndex = 1;
            this.lblPayMentInfo.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "按支付方式统计";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.dgItemInfo);
            this.superTabControlPanel2.Controls.Add(this.expItemInfo);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(1264, 572);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // dgItemInfo
            // 
            this.dgItemInfo.AllowSortWhenClickColumnHeader = false;
            this.dgItemInfo.AllowUserToAddRows = false;
            this.dgItemInfo.AllowUserToResizeColumns = false;
            this.dgItemInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgItemInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgItemInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgItemInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItemInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgItemInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItemInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CostHeadIDItem,
            this.regItemflag,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.CostStatusItem,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgItemInfo.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgItemInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItemInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgItemInfo.HighlightSelectedColumnHeaders = false;
            this.dgItemInfo.Location = new System.Drawing.Point(0, 0);
            this.dgItemInfo.Name = "dgItemInfo";
            this.dgItemInfo.ReadOnly = true;
            this.dgItemInfo.RowHeadersWidth = 25;
            this.dgItemInfo.RowTemplate.Height = 23;
            this.dgItemInfo.SelectAllSignVisible = false;
            this.dgItemInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgItemInfo.SeqVisible = true;
            this.dgItemInfo.SetCustomStyle = false;
            this.dgItemInfo.Size = new System.Drawing.Size(1234, 572);
            this.dgItemInfo.TabIndex = 9;
            this.dgItemInfo.DoubleClick += new System.EventHandler(this.dgItemInfo_DoubleClick);
            // 
            // CostHeadIDItem
            // 
            this.CostHeadIDItem.DataPropertyName = "costHeadID";
            this.CostHeadIDItem.HeaderText = "结算编码";
            this.CostHeadIDItem.Name = "CostHeadIDItem";
            this.CostHeadIDItem.ReadOnly = true;
            this.CostHeadIDItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostHeadIDItem.Width = 60;
            // 
            // regItemflag
            // 
            this.regItemflag.DataPropertyName = "regflag";
            this.regItemflag.HeaderText = "类型";
            this.regItemflag.Name = "regItemflag";
            this.regItemflag.ReadOnly = true;
            this.regItemflag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.regItemflag.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "costDate";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Format = "yyyy-MM-dd HH:m:ss";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.HeaderText = "操作时间";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "EndInvoiceNO";
            this.dataGridViewTextBoxColumn4.HeaderText = "票号";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 80;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "ChargeEmpName";
            this.dataGridViewTextBoxColumn5.HeaderText = "收费员";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // CostStatusItem
            // 
            this.CostStatusItem.DataPropertyName = "costStatus";
            this.CostStatusItem.HeaderText = "状态";
            this.CostStatusItem.Name = "CostStatusItem";
            this.CostStatusItem.ReadOnly = true;
            this.CostStatusItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostStatusItem.Width = 50;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "cardNO";
            this.dataGridViewTextBoxColumn7.HeaderText = "卡号";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 80;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "visitNO";
            this.dataGridViewTextBoxColumn8.HeaderText = "门诊流水号";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 80;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "PatName";
            this.dataGridViewTextBoxColumn9.HeaderText = "病人姓名";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "PatSex";
            this.dataGridViewTextBoxColumn10.HeaderText = "性别";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn10.Width = 60;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Age";
            this.dataGridViewTextBoxColumn11.HeaderText = "年龄";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn11.Width = 60;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "patTypeName";
            this.dataGridViewTextBoxColumn12.HeaderText = "病人类别";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn12.Width = 80;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "DoctName";
            this.dataGridViewTextBoxColumn13.HeaderText = "就诊医生";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn13.Width = 80;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "DeptName";
            this.dataGridViewTextBoxColumn14.HeaderText = "就诊科室";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn14.Width = 80;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "totalFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.dataGridViewTextBoxColumn15.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn15.HeaderText = "总金额";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            this.dataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn15.Width = 60;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.dataGridViewTextBoxColumn16.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn16.HeaderText = "凑整金额";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.ReadOnly = true;
            this.dataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // expItemInfo
            // 
            this.expItemInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.expItemInfo.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expItemInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expItemInfo.Controls.Add(this.lblItemInfo);
            this.expItemInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.expItemInfo.Expanded = false;
            this.expItemInfo.ExpandedBounds = new System.Drawing.Rectangle(1030, 0, 234, 538);
            this.expItemInfo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expItemInfo.HideControlsWhenCollapsed = true;
            this.expItemInfo.Location = new System.Drawing.Point(1234, 0);
            this.expItemInfo.Name = "expItemInfo";
            this.expItemInfo.Size = new System.Drawing.Size(30, 572);
            this.expItemInfo.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expItemInfo.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expItemInfo.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expItemInfo.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expItemInfo.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expItemInfo.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expItemInfo.Style.GradientAngle = 90;
            this.expItemInfo.TabIndex = 8;
            this.expItemInfo.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expItemInfo.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expItemInfo.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expItemInfo.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expItemInfo.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expItemInfo.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expItemInfo.TitleStyle.GradientAngle = 90;
            this.expItemInfo.TitleText = "项目分类汇总信息";
            // 
            // lblItemInfo
            // 
            this.lblItemInfo.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblItemInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblItemInfo.Location = new System.Drawing.Point(14, 35);
            this.lblItemInfo.Name = "lblItemInfo";
            this.lblItemInfo.Size = new System.Drawing.Size(208, 466);
            this.lblItemInfo.TabIndex = 1;
            this.lblItemInfo.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "按项目分类统计";
            // 
            // FrmOpCostSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.panelEx4);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmOpCostSearch";
            this.Text = "门诊费用查询";
            this.OpenWindowBefore += new System.EventHandler(this.FrmOpCostSearch_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpEdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBdate)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TabControl)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPayMentInfo)).EndInit();
            this.expPayMentInfo.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgItemInfo)).EndInit();
            this.expItemInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.TextBoxCard txtChareEmp;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Controls.CheckBoxX radAllRecord;
        private DevComponents.DotNetBar.Controls.CheckBoxX radBalanceRecord;
        private DevComponents.DotNetBar.Controls.CheckBoxX radRegRecord;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.CheckBoxX radAllStatus;
        private DevComponents.DotNetBar.Controls.CheckBoxX radRefundStatus;
        private DevComponents.DotNetBar.Controls.CheckBoxX radNormalStatus;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btnCostDetail;
        private DevComponents.DotNetBar.SuperTabControl TabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX radAccountDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX radChargeDate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPatType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtQueryContent;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private EfwControls.CustomControl.TextBoxCard txtPresEmp;
        private DevComponents.DotNetBar.LabelX labelX12;
        private EfwControls.CustomControl.TextBoxCard txtPresDept;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpEdate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpBdate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndInvoiceNO;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBeInvoiceNO;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnExcel;
        private DevComponents.DotNetBar.ButtonX btnSetBegin;
        private DevComponents.DotNetBar.ExpandablePanel expItemInfo;
        private DevComponents.DotNetBar.LabelX lblItemInfo;
        private DevComponents.DotNetBar.ExpandablePanel expPayMentInfo;
        private DevComponents.DotNetBar.LabelX lblPayMentInfo;
        private EfwControls.CustomControl.DataGrid dgPayMentInfo;
        private EfwControls.CustomControl.DataGrid dgItemInfo;
        private DevComponents.DotNetBar.ButtonX btnPrintAgain;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostHeadIDItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn regItemflag;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostStatusItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostHeadID;
        private System.Windows.Forms.DataGridViewTextBoxColumn regflag;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeInvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChargeEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoctName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoundingFee;
        private DevComponents.DotNetBar.ButtonX btnPresDetailPrintAgain;
    }
}