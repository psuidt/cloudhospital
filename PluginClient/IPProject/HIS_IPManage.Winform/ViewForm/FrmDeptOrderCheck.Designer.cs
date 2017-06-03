namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmDeptOrderCheck
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
            this.endDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.progress = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.cbAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbLongOrder = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbTempOrder = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbLong = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnSend = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnLook = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbBed = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.rTxtError = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.dgnSendResult = new System.Windows.Forms.DataGridView();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.endDate)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgnSendResult)).BeginInit();
            this.SuspendLayout();
            // 
            // endDate
            // 
            // 
            // 
            // 
            this.endDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.endDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.endDate.ButtonDropDown.Visible = true;
            this.endDate.IsPopupCalendarOpen = false;
            this.endDate.Location = new System.Drawing.Point(130, 23);
            // 
            // 
            // 
            this.endDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.endDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.endDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 11, 1, 0, 0, 0, 0);
            this.endDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.endDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.endDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.endDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.endDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.endDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.endDate.MonthCalendar.TodayButtonVisible = true;
            this.endDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(200, 21);
            this.endDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.endDate.TabIndex = 1;
            this.endDate.Value = new System.DateTime(2016, 11, 7, 14, 22, 18, 0);
            // 
            // progress
            // 
            // 
            // 
            // 
            this.progress.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.progress.Location = new System.Drawing.Point(53, 117);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(430, 23);
            this.progress.TabIndex = 2;
            this.progress.Text = "progressBarX1";
            // 
            // cbAll
            // 
            // 
            // 
            // 
            this.cbAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbAll.Checked = true;
            this.cbAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAll.CheckValue = "Y";
            this.cbAll.Location = new System.Drawing.Point(52, 72);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(54, 23);
            this.cbAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbAll.TabIndex = 3;
            this.cbAll.Text = "全部(";
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // cbLongOrder
            // 
            // 
            // 
            // 
            this.cbLongOrder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbLongOrder.Checked = true;
            this.cbLongOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLongOrder.CheckValue = "Y";
            this.cbLongOrder.Location = new System.Drawing.Point(109, 72);
            this.cbLongOrder.Name = "cbLongOrder";
            this.cbLongOrder.Size = new System.Drawing.Size(74, 23);
            this.cbLongOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLongOrder.TabIndex = 4;
            this.cbLongOrder.Text = "长期医嘱";
            // 
            // cbTempOrder
            // 
            // 
            // 
            // 
            this.cbTempOrder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbTempOrder.Checked = true;
            this.cbTempOrder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTempOrder.CheckValue = "Y";
            this.cbTempOrder.Location = new System.Drawing.Point(188, 72);
            this.cbTempOrder.Name = "cbTempOrder";
            this.cbTempOrder.Size = new System.Drawing.Size(74, 23);
            this.cbTempOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbTempOrder.TabIndex = 5;
            this.cbTempOrder.Text = "临时医嘱";
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
            this.cbLong.Location = new System.Drawing.Point(267, 72);
            this.cbLong.Name = "cbLong";
            this.cbLong.Size = new System.Drawing.Size(48, 23);
            this.cbLong.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbLong.TabIndex = 6;
            this.cbLong.Text = "账单";
            // 
            // btnSend
            // 
            this.btnSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSend.Location = new System.Drawing.Point(119, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "发送(&S)";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(321, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnLook);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSend);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx1.Location = new System.Drawing.Point(0, 217);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(542, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 11;
            // 
            // btnLook
            // 
            this.btnLook.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLook.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLook.Location = new System.Drawing.Point(225, 6);
            this.btnLook.Name = "btnLook";
            this.btnLook.Size = new System.Drawing.Size(75, 23);
            this.btnLook.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLook.TabIndex = 11;
            this.btnLook.Text = "查看结果(&L)";
            this.btnLook.Click += new System.EventHandler(this.btnLook_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.cbTempOrder);
            this.panelEx2.Controls.Add(this.cbLong);
            this.panelEx2.Controls.Add(this.endDate);
            this.panelEx2.Controls.Add(this.cbLongOrder);
            this.panelEx2.Controls.Add(this.progress);
            this.panelEx2.Controls.Add(this.cbAll);
            this.panelEx2.Controls.Add(this.cbBed);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(543, 217);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 12;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(53, 23);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "截止日期";
            // 
            // cbBed
            // 
            // 
            // 
            // 
            this.cbBed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbBed.Checked = true;
            this.cbBed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBed.CheckValue = "Y";
            this.cbBed.Location = new System.Drawing.Point(321, 72);
            this.cbBed.Name = "cbBed";
            this.cbBed.Size = new System.Drawing.Size(72, 23);
            this.cbBed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbBed.TabIndex = 8;
            this.cbBed.Text = "床位费)";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.rTxtError);
            this.panelEx3.Controls.Add(this.dgnSendResult);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(543, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(0, 217);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 13;
            // 
            // rTxtError
            // 
            // 
            // 
            // 
            this.rTxtError.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rTxtError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rTxtError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxtError.Location = new System.Drawing.Point(0, 0);
            this.rTxtError.Name = "rTxtError";
            this.rTxtError.ReadOnly = true;
            this.rTxtError.Size = new System.Drawing.Size(0, 217);
            this.rTxtError.TabIndex = 3;
            // 
            // dgnSendResult
            // 
            this.dgnSendResult.AllowUserToAddRows = false;
            this.dgnSendResult.AllowUserToDeleteRows = false;
            this.dgnSendResult.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgnSendResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgnSendResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgnSendResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgnSendResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BedNo,
            this.GroupID,
            this.OrderID,
            this.OrderName,
            this.ErrorMessage});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgnSendResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgnSendResult.Location = new System.Drawing.Point(390, 0);
            this.dgnSendResult.MultiSelect = false;
            this.dgnSendResult.Name = "dgnSendResult";
            this.dgnSendResult.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgnSendResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgnSendResult.RowTemplate.Height = 23;
            this.dgnSendResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgnSendResult.Size = new System.Drawing.Size(30, 28);
            this.dgnSendResult.TabIndex = 2;
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.HeaderText = "床号";
            this.BedNo.MinimumWidth = 45;
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BedNo.Width = 45;
            // 
            // GroupID
            // 
            this.GroupID.DataPropertyName = "GroupID";
            this.GroupID.HeaderText = "组号";
            this.GroupID.MinimumWidth = 60;
            this.GroupID.Name = "GroupID";
            this.GroupID.ReadOnly = true;
            this.GroupID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GroupID.Width = 60;
            // 
            // OrderID
            // 
            this.OrderID.DataPropertyName = "OrderID";
            this.OrderID.HeaderText = "医嘱ID";
            this.OrderID.MinimumWidth = 50;
            this.OrderID.Name = "OrderID";
            this.OrderID.ReadOnly = true;
            this.OrderID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OrderID.Width = 50;
            // 
            // OrderName
            // 
            this.OrderName.DataPropertyName = "OrderName";
            this.OrderName.HeaderText = "医嘱名";
            this.OrderName.MinimumWidth = 150;
            this.OrderName.Name = "OrderName";
            this.OrderName.ReadOnly = true;
            this.OrderName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OrderName.Width = 200;
            // 
            // ErrorMessage
            // 
            this.ErrorMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorMessage.DataPropertyName = "ErrorMessage";
            this.ErrorMessage.HeaderText = "异常信息";
            this.ErrorMessage.MinimumWidth = 100;
            this.ErrorMessage.Name = "ErrorMessage";
            this.ErrorMessage.ReadOnly = true;
            this.ErrorMessage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmDeptOrderCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 257);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDeptOrderCheck";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量医嘱发送";
            ((System.ComponentModel.ISupportInitialize)(this.endDate)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgnSendResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.Editors.DateTimeAdv.DateTimeInput endDate;
        private DevComponents.DotNetBar.Controls.ProgressBarX progress;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbLongOrder;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbTempOrder;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbLong;
        private DevComponents.DotNetBar.ButtonX btnSend;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnLook;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridView dgnSendResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupID;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTxtError;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbBed;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}