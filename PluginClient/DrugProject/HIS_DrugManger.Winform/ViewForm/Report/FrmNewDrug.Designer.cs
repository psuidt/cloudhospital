namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmNewDrug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewDrug));
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dtYearMonth = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.CmbDeptRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtDrugName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.axGRDisplayViewer = new Axgregn6Lib.AxGRDisplayViewer();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtYearMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dtYearMonth);
            this.panelEx2.Controls.Add(this.CmbDeptRoom);
            this.panelEx2.Controls.Add(this.buttonX3);
            this.panelEx2.Controls.Add(this.buttonX2);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.txtDrugName);
            this.panelEx2.Controls.Add(this.labelX7);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1070, 45);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // dtYearMonth
            // 
            // 
            // 
            // 
            this.dtYearMonth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtYearMonth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtYearMonth.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtYearMonth.ButtonDropDown.Visible = true;
            this.dtYearMonth.CustomFormat = "yyyy-MM";
            this.dtYearMonth.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtYearMonth.IsPopupCalendarOpen = false;
            this.dtYearMonth.Location = new System.Drawing.Point(263, 14);
            // 
            // 
            // 
            this.dtYearMonth.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtYearMonth.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtYearMonth.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtYearMonth.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtYearMonth.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtYearMonth.MonthCalendar.DisplayMonth = new System.DateTime(2016, 11, 1, 0, 0, 0, 0);
            this.dtYearMonth.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtYearMonth.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtYearMonth.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtYearMonth.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtYearMonth.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtYearMonth.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtYearMonth.MonthCalendar.TodayButtonVisible = true;
            this.dtYearMonth.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtYearMonth.Name = "dtYearMonth";
            this.dtYearMonth.Size = new System.Drawing.Size(150, 21);
            this.dtYearMonth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtYearMonth.TabIndex = 23;
            // 
            // CmbDeptRoom
            // 
            this.CmbDeptRoom.DisplayMember = "Text";
            this.CmbDeptRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CmbDeptRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbDeptRoom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CmbDeptRoom.FormattingEnabled = true;
            this.CmbDeptRoom.ItemHeight = 15;
            this.CmbDeptRoom.Location = new System.Drawing.Point(54, 14);
            this.CmbDeptRoom.Name = "CmbDeptRoom";
            this.CmbDeptRoom.Size = new System.Drawing.Size(141, 21);
            this.CmbDeptRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.CmbDeptRoom.TabIndex = 22;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX3.Image = ((System.Drawing.Image)(resources.GetObject("buttonX3.Image")));
            this.buttonX3.Location = new System.Drawing.Point(831, 11);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 22);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 19;
            this.buttonX3.Text = "关闭(&C)";
            this.buttonX3.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX2.Image = ((System.Drawing.Image)(resources.GetObject("buttonX2.Image")));
            this.buttonX2.Location = new System.Drawing.Point(750, 11);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 22);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 18;
            this.buttonX2.Text = "打印(&P)";
            this.buttonX2.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(669, 11);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtDrugName
            // 
            // 
            // 
            // 
            this.txtDrugName.Border.Class = "TextBoxBorder";
            this.txtDrugName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDrugName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDrugName.Location = new System.Drawing.Point(478, 14);
            this.txtDrugName.Name = "txtDrugName";
            this.txtDrugName.Size = new System.Drawing.Size(170, 21);
            this.txtDrugName.TabIndex = 16;
            this.txtDrugName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDrugName_KeyDown);
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(419, 14);
            this.labelX7.Name = "labelX7";
            this.labelX7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX7.Size = new System.Drawing.Size(56, 18);
            this.labelX7.TabIndex = 15;
            this.labelX7.Text = "查询药品";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(16, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(31, 18);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "库房";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(201, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "统计年月";
            // 
            // axGRDisplayViewer
            // 
            this.axGRDisplayViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGRDisplayViewer.Enabled = true;
            this.axGRDisplayViewer.Location = new System.Drawing.Point(0, 45);
            this.axGRDisplayViewer.Name = "axGRDisplayViewer";
            this.axGRDisplayViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDisplayViewer.OcxState")));
            this.axGRDisplayViewer.Size = new System.Drawing.Size(1070, 393);
            this.axGRDisplayViewer.TabIndex = 3;
            // 
            // FrmNewDrug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 438);
            this.Controls.Add(this.axGRDisplayViewer);
            this.Controls.Add(this.panelEx2);
            this.Name = "FrmNewDrug";
            this.Text = "新药入库统计";
            this.OpenWindowBefore += new System.EventHandler(this.FrmNewDrug_OpenWindowBefore);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtYearMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtYearMonth;
        private DevComponents.DotNetBar.Controls.ComboBoxEx CmbDeptRoom;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDrugName;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private Axgregn6Lib.AxGRDisplayViewer axGRDisplayViewer;
    }
}