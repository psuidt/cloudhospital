namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmCopySchedual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCopySchedual));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtWeek = new DevComponents.Editors.IntegerInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dtpCopyBdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtpCopyEndDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtpNewScheBdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.dtpNewScheEdate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtCopyDoc = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtCopyDept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCopyBdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCopyEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNewScheBdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNewScheEdate)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(-2, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(115, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = " 复制排班周次(前)";
            // 
            // txtWeek
            // 
            // 
            // 
            // 
            this.txtWeek.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtWeek.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWeek.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtWeek.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWeek.Location = new System.Drawing.Point(119, 9);
            this.txtWeek.MinValue = 0;
            this.txtWeek.Name = "txtWeek";
            this.txtWeek.ShowUpDown = true;
            this.txtWeek.Size = new System.Drawing.Size(73, 21);
            this.txtWeek.TabIndex = 1;
            this.txtWeek.ValueChanged += new System.EventHandler(this.txtWeek_ValueChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(22, 40);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(91, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "复制开始日期";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(22, 68);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(91, 23);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "复制结束日期";
            // 
            // dtpCopyBdate
            // 
            // 
            // 
            // 
            this.dtpCopyBdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpCopyBdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyBdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpCopyBdate.ButtonDropDown.Visible = true;
            this.dtpCopyBdate.CustomFormat = "yyyy-MM-dd";
            this.dtpCopyBdate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCopyBdate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpCopyBdate.IsPopupCalendarOpen = false;
            this.dtpCopyBdate.Location = new System.Drawing.Point(119, 40);
            // 
            // 
            // 
            this.dtpCopyBdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpCopyBdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyBdate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpCopyBdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpCopyBdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyBdate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpCopyBdate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpCopyBdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpCopyBdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpCopyBdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpCopyBdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpCopyBdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpCopyBdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyBdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpCopyBdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpCopyBdate.Name = "dtpCopyBdate";
            this.dtpCopyBdate.Size = new System.Drawing.Size(139, 21);
            this.dtpCopyBdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpCopyBdate.TabIndex = 4;
            this.dtpCopyBdate.ValueChanged += new System.EventHandler(this.dtpCopyBdate_ValueChanged);
            // 
            // dtpCopyEndDate
            // 
            // 
            // 
            // 
            this.dtpCopyEndDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpCopyEndDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyEndDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpCopyEndDate.ButtonDropDown.Visible = true;
            this.dtpCopyEndDate.CustomFormat = "yyyy-MM-dd";
            this.dtpCopyEndDate.Enabled = false;
            this.dtpCopyEndDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpCopyEndDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpCopyEndDate.IsPopupCalendarOpen = false;
            this.dtpCopyEndDate.Location = new System.Drawing.Point(119, 68);
            // 
            // 
            // 
            this.dtpCopyEndDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpCopyEndDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyEndDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpCopyEndDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpCopyEndDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyEndDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpCopyEndDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpCopyEndDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpCopyEndDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpCopyEndDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpCopyEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpCopyEndDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpCopyEndDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpCopyEndDate.MonthCalendar.TodayButtonVisible = true;
            this.dtpCopyEndDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpCopyEndDate.Name = "dtpCopyEndDate";
            this.dtpCopyEndDate.Size = new System.Drawing.Size(139, 21);
            this.dtpCopyEndDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpCopyEndDate.TabIndex = 5;
            // 
            // dtpNewScheBdate
            // 
            // 
            // 
            // 
            this.dtpNewScheBdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpNewScheBdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheBdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpNewScheBdate.ButtonDropDown.Visible = true;
            this.dtpNewScheBdate.CustomFormat = "yyyy-MM-dd";
            this.dtpNewScheBdate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpNewScheBdate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpNewScheBdate.IsPopupCalendarOpen = false;
            this.dtpNewScheBdate.Location = new System.Drawing.Point(375, 39);
            // 
            // 
            // 
            this.dtpNewScheBdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpNewScheBdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheBdate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpNewScheBdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpNewScheBdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheBdate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpNewScheBdate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpNewScheBdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpNewScheBdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpNewScheBdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpNewScheBdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpNewScheBdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpNewScheBdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheBdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpNewScheBdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpNewScheBdate.Name = "dtpNewScheBdate";
            this.dtpNewScheBdate.Size = new System.Drawing.Size(139, 21);
            this.dtpNewScheBdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpNewScheBdate.TabIndex = 7;
            this.dtpNewScheBdate.ValueChanged += new System.EventHandler(this.dtpNewScheBdate_ValueChanged);
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(275, 38);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(102, 23);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "新排班开始日期";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(275, 67);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(102, 23);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "新排班结束日期";
            // 
            // dtpNewScheEdate
            // 
            // 
            // 
            // 
            this.dtpNewScheEdate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpNewScheEdate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheEdate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpNewScheEdate.ButtonDropDown.Visible = true;
            this.dtpNewScheEdate.CustomFormat = "yyyy-MM-dd";
            this.dtpNewScheEdate.Enabled = false;
            this.dtpNewScheEdate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpNewScheEdate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpNewScheEdate.IsPopupCalendarOpen = false;
            this.dtpNewScheEdate.Location = new System.Drawing.Point(375, 66);
            // 
            // 
            // 
            this.dtpNewScheEdate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpNewScheEdate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheEdate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpNewScheEdate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpNewScheEdate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheEdate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpNewScheEdate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpNewScheEdate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpNewScheEdate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpNewScheEdate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpNewScheEdate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpNewScheEdate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpNewScheEdate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpNewScheEdate.MonthCalendar.TodayButtonVisible = true;
            this.dtpNewScheEdate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpNewScheEdate.Name = "dtpNewScheEdate";
            this.dtpNewScheEdate.Size = new System.Drawing.Size(139, 21);
            this.dtpNewScheEdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpNewScheEdate.TabIndex = 9;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(437, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(356, 108);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.txtCopyDoc);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.txtCopyDept);
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.txtWeek);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.dtpNewScheEdate);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.dtpCopyBdate);
            this.panelEx1.Controls.Add(this.dtpNewScheBdate);
            this.panelEx1.Controls.Add(this.dtpCopyEndDate);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(543, 161);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 15;
            // 
            // txtCopyDoc
            // 
            // 
            // 
            // 
            this.txtCopyDoc.Border.Class = "TextBoxBorder";
            this.txtCopyDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCopyDoc.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtCopyDoc.ButtonCustom.Image")));
            this.txtCopyDoc.ButtonCustom.Visible = true;
            this.txtCopyDoc.CardColumn = null;
            this.txtCopyDoc.DisplayField = "";
            this.txtCopyDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCopyDoc.IsEnterShowCard = true;
            this.txtCopyDoc.IsNumSelected = false;
            this.txtCopyDoc.IsPage = true;
            this.txtCopyDoc.IsShowLetter = false;
            this.txtCopyDoc.IsShowPage = false;
            this.txtCopyDoc.IsShowSeq = true;
            this.txtCopyDoc.Location = new System.Drawing.Point(116, 130);
            this.txtCopyDoc.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtCopyDoc.MemberField = "";
            this.txtCopyDoc.MemberValue = null;
            this.txtCopyDoc.Name = "txtCopyDoc";
            this.txtCopyDoc.QueryFields = new string[] {
        ""};
            this.txtCopyDoc.QueryFieldsString = "";
            this.txtCopyDoc.SelectedValue = null;
            this.txtCopyDoc.ShowCardColumns = null;
            this.txtCopyDoc.ShowCardDataSource = null;
            this.txtCopyDoc.ShowCardHeight = 0;
            this.txtCopyDoc.ShowCardWidth = 0;
            this.txtCopyDoc.Size = new System.Drawing.Size(139, 21);
            this.txtCopyDoc.TabIndex = 19;
            this.txtCopyDoc.Visible = false;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(42, 126);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(60, 23);
            this.labelX6.TabIndex = 18;
            this.labelX6.Text = "复制医生";
            this.labelX6.Visible = false;
            // 
            // txtCopyDept
            // 
            // 
            // 
            // 
            this.txtCopyDept.Border.Class = "TextBoxBorder";
            this.txtCopyDept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCopyDept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtCopyDept.ButtonCustom.Image")));
            this.txtCopyDept.ButtonCustom.Visible = true;
            this.txtCopyDept.CardColumn = null;
            this.txtCopyDept.DisplayField = "";
            this.txtCopyDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCopyDept.IsEnterShowCard = true;
            this.txtCopyDept.IsNumSelected = false;
            this.txtCopyDept.IsPage = true;
            this.txtCopyDept.IsShowLetter = false;
            this.txtCopyDept.IsShowPage = false;
            this.txtCopyDept.IsShowSeq = true;
            this.txtCopyDept.Location = new System.Drawing.Point(118, 98);
            this.txtCopyDept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtCopyDept.MemberField = "";
            this.txtCopyDept.MemberValue = null;
            this.txtCopyDept.Name = "txtCopyDept";
            this.txtCopyDept.QueryFields = new string[] {
        ""};
            this.txtCopyDept.QueryFieldsString = "";
            this.txtCopyDept.SelectedValue = null;
            this.txtCopyDept.ShowCardColumns = null;
            this.txtCopyDept.ShowCardDataSource = null;
            this.txtCopyDept.ShowCardHeight = 0;
            this.txtCopyDept.ShowCardWidth = 0;
            this.txtCopyDept.Size = new System.Drawing.Size(139, 21);
            this.txtCopyDept.TabIndex = 17;
            this.txtCopyDept.Visible = false;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(42, 97);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(60, 23);
            this.labelX7.TabIndex = 16;
            this.labelX7.Text = "复制科室";
            this.labelX7.Visible = false;
            // 
            // FrmCopySchedual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 161);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCopySchedual";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "复制排班";
            this.Load += new System.EventHandler(this.FrmCopySchedual_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmCopySchedual_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.txtWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCopyBdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpCopyEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNewScheBdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNewScheEdate)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput txtWeek;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpCopyBdate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpCopyEndDate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpNewScheBdate;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpNewScheEdate;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.TextBoxCard txtCopyDoc;
        private DevComponents.DotNetBar.LabelX labelX6;
        private EfwControls.CustomControl.TextBoxCard txtCopyDept;
        private DevComponents.DotNetBar.LabelX labelX7;
    }
}