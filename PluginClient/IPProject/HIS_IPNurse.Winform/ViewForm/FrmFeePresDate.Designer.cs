namespace HIS_IPNurse.Winform.ViewForm
{
    partial class FrmFeePresDate
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.chkBedOrder = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPresDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dtEndTine = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTine)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dtEndTine);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.chkBedOrder);
            this.panelEx1.Controls.Add(this.chkPresDate);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(349, 168);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(26, 32);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(84, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "记账结束日期";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(223, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(120, 108);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "确定(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkBedOrder
            // 
            // 
            // 
            // 
            this.chkBedOrder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBedOrder.Location = new System.Drawing.Point(239, 67);
            this.chkBedOrder.Name = "chkBedOrder";
            this.chkBedOrder.Size = new System.Drawing.Size(61, 23);
            this.chkBedOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkBedOrder.TabIndex = 1;
            this.chkBedOrder.Text = "床位费";
            // 
            // chkPresDate
            // 
            // 
            // 
            // 
            this.chkPresDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPresDate.Location = new System.Drawing.Point(119, 67);
            this.chkPresDate.Name = "chkPresDate";
            this.chkPresDate.Size = new System.Drawing.Size(49, 23);
            this.chkPresDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPresDate.TabIndex = 0;
            this.chkPresDate.Text = "账单";
            // 
            // dtEndTine
            // 
            // 
            // 
            // 
            this.dtEndTine.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEndTine.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTine.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtEndTine.ButtonDropDown.Visible = true;
            this.dtEndTine.IsPopupCalendarOpen = false;
            this.dtEndTine.Location = new System.Drawing.Point(116, 32);
            // 
            // 
            // 
            this.dtEndTine.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndTine.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTine.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtEndTine.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEndTine.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTine.MonthCalendar.DisplayMonth = new System.DateTime(2016, 12, 1, 0, 0, 0, 0);
            this.dtEndTine.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEndTine.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndTine.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEndTine.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndTine.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEndTine.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTine.MonthCalendar.TodayButtonVisible = true;
            this.dtEndTine.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtEndTine.Name = "dtEndTine";
            this.dtEndTine.Size = new System.Drawing.Size(182, 21);
            this.dtEndTine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtEndTine.TabIndex = 6;
            // 
            // FrmFeePresDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(349, 168);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFeePresDate";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "处方日期确认";
            this.Shown += new System.EventHandler(this.FrmFeePresDate_Shown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPresDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBedOrder;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtEndTine;
    }
}