namespace HIS_FinancialStatistics.Winform.ViewForm
{
    partial class FrmFinacialItemStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinacialItemStatistics));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dtTimer = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtItem = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cbbPatType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.tbcItem = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbbTimeType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbWork = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.GridViewResult = new Axgregn6Lib.AxGRDisplayViewer();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dtTimer);
            this.panelEx1.Controls.Add(this.btnClear);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnPrint);
            this.panelEx1.Controls.Add(this.btQuery);
            this.panelEx1.Controls.Add(this.txtItem);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.cbbPatType);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.tbcItem);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.cbbTimeType);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.cmbWork);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1143, 86);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dtTimer
            // 
            // 
            // 
            // 
            this.dtTimer.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtTimer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimer.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtTimer.ButtonDropDown.Visible = true;
            this.dtTimer.CustomFormat = "yyyy-MM";
            this.dtTimer.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtTimer.IsPopupCalendarOpen = false;
            this.dtTimer.Location = new System.Drawing.Point(346, 15);
            // 
            // 
            // 
            this.dtTimer.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTimer.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimer.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtTimer.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtTimer.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimer.MonthCalendar.DisplayMonth = new System.DateTime(2016, 9, 1, 0, 0, 0, 0);
            this.dtTimer.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtTimer.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtTimer.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtTimer.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtTimer.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtTimer.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtTimer.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtTimer.MonthCalendar.TodayButtonVisible = true;
            this.dtTimer.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtTimer.Name = "dtTimer";
            this.dtTimer.Size = new System.Drawing.Size(130, 21);
            this.dtTimer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtTimer.TabIndex = 16;
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(1046, 50);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "清空";
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1046, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(965, 13);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "打印";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btQuery
            // 
            this.btQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btQuery.Image = ((System.Drawing.Image)(resources.GetObject("btQuery.Image")));
            this.btQuery.Location = new System.Drawing.Point(884, 13);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(75, 23);
            this.btQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btQuery.TabIndex = 12;
            this.btQuery.Text = " 查询";
            this.btQuery.Click += new System.EventHandler(this.BtQuery_Click);
            // 
            // txtItem
            // 
            // 
            // 
            // 
            this.txtItem.Border.Class = "TextBoxBorder";
            this.txtItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtItem.Location = new System.Drawing.Point(346, 50);
            this.txtItem.Name = "txtItem";
            this.txtItem.ReadOnly = true;
            this.txtItem.Size = new System.Drawing.Size(694, 21);
            this.txtItem.TabIndex = 11;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(284, 53);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(56, 18);
            this.labelX6.TabIndex = 10;
            this.labelX6.Text = "选择项目";
            // 
            // cbbPatType
            // 
            this.cbbPatType.DisplayMember = "Text";
            this.cbbPatType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbPatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPatType.FormattingEnabled = true;
            this.cbbPatType.ItemHeight = 15;
            this.cbbPatType.Location = new System.Drawing.Point(745, 15);
            this.cbbPatType.Name = "cbbPatType";
            this.cbbPatType.Size = new System.Drawing.Size(133, 21);
            this.cbbPatType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbPatType.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(683, 17);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(56, 18);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "统计类型";
            // 
            // tbcItem
            // 
            // 
            // 
            // 
            this.tbcItem.Border.Class = "TextBoxBorder";
            this.tbcItem.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbcItem.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("tbcItem.ButtonCustom.Image")));
            this.tbcItem.ButtonCustom.Visible = true;
            this.tbcItem.CardColumn = null;
            this.tbcItem.DisplayField = "";
            this.tbcItem.IsEnterShowCard = true;
            this.tbcItem.IsNumSelected = false;
            this.tbcItem.IsPage = true;
            this.tbcItem.IsShowLetter = false;
            this.tbcItem.IsShowPage = false;
            this.tbcItem.IsShowSeq = true;
            this.tbcItem.Location = new System.Drawing.Point(75, 50);
            this.tbcItem.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.tbcItem.MemberField = "";
            this.tbcItem.MemberValue = null;
            this.tbcItem.Name = "tbcItem";
            this.tbcItem.QueryFields = new string[] {
        ""};
            this.tbcItem.QueryFieldsString = "";
            this.tbcItem.SelectedValue = null;
            this.tbcItem.ShowCardColumns = null;
            this.tbcItem.ShowCardDataSource = null;
            this.tbcItem.ShowCardHeight = 0;
            this.tbcItem.ShowCardWidth = 0;
            this.tbcItem.Size = new System.Drawing.Size(203, 21);
            this.tbcItem.TabIndex = 7;
            this.tbcItem.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.TbcItem_AfterSelectedRow);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(56, 18);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "选择项目";
            // 
            // cbbTimeType
            // 
            this.cbbTimeType.DisplayMember = "Text";
            this.cbbTimeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTimeType.FormattingEnabled = true;
            this.cbbTimeType.ItemHeight = 15;
            this.cbbTimeType.Location = new System.Drawing.Point(544, 15);
            this.cbbTimeType.Name = "cbbTimeType";
            this.cbbTimeType.Size = new System.Drawing.Size(133, 21);
            this.cbbTimeType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbTimeType.TabIndex = 5;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(482, 17);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "时间类型";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(284, 17);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "时间范围";
            // 
            // cmbWork
            // 
            this.cmbWork.DisplayMember = "Text";
            this.cmbWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWork.FormattingEnabled = true;
            this.cmbWork.ItemHeight = 15;
            this.cmbWork.Location = new System.Drawing.Point(75, 15);
            this.cmbWork.Name = "cmbWork";
            this.cmbWork.Size = new System.Drawing.Size(203, 21);
            this.cmbWork.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWork.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 18);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "医疗机构";
            // 
            // GridViewResult
            // 
            this.GridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewResult.Enabled = true;
            this.GridViewResult.Location = new System.Drawing.Point(0, 86);
            this.GridViewResult.Name = "GridViewResult";
            this.GridViewResult.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("GridViewResult.OcxState")));
            this.GridViewResult.Size = new System.Drawing.Size(1143, 407);
            this.GridViewResult.TabIndex = 3;
            // 
            // FrmFinacialItemStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 493);
            this.Controls.Add(this.GridViewResult);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmFinacialItemStatistics";
            this.Text = "单项目收入统计";
            this.OpenWindowBefore += new System.EventHandler(this.FrmFinacialItemStatistics_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWork;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btQuery;
        private DevComponents.DotNetBar.Controls.TextBoxX txtItem;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbPatType;
        private DevComponents.DotNetBar.LabelX labelX5;
        private EfwControls.CustomControl.TextBoxCard tbcItem;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTimeType;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnClear;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private Axgregn6Lib.AxGRDisplayViewer GridViewResult;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtTimer;
    }
}