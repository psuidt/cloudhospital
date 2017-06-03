namespace HIS_IPManage.Winform.ViewForm
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
            this.dtkPresDate = new EfwControls.CustomControl.StatDateTime();
            this.chkBedOrder = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPresDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.dtkPresDate);
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
            this.labelX1.Location = new System.Drawing.Point(28, 39);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(30, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "日期";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(223, 115);
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
            this.btnSave.Location = new System.Drawing.Point(146, 115);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "确定(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtkPresDate
            // 
            this.dtkPresDate.BackColor = System.Drawing.Color.Transparent;
            this.dtkPresDate.DateFormat = "yyyy-MM-dd";
            this.dtkPresDate.DateWidth = 120;
            this.dtkPresDate.Font = new System.Drawing.Font("宋体", 9.5F);
            this.dtkPresDate.Location = new System.Drawing.Point(63, 28);
            this.dtkPresDate.Name = "dtkPresDate";
            this.dtkPresDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtkPresDate.ShowStyle = EfwControls.CustomControl.showStyle.vertical;
            this.dtkPresDate.Size = new System.Drawing.Size(235, 44);
            this.dtkPresDate.TabIndex = 2;
            // 
            // chkBedOrder
            // 
            // 
            // 
            // 
            this.chkBedOrder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBedOrder.Location = new System.Drawing.Point(234, 83);
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
            this.chkPresDate.Location = new System.Drawing.Point(174, 83);
            this.chkPresDate.Name = "chkPresDate";
            this.chkPresDate.Size = new System.Drawing.Size(49, 23);
            this.chkPresDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPresDate.TabIndex = 0;
            this.chkPresDate.Text = "账单";
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
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPresDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBedOrder;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private EfwControls.CustomControl.StatDateTime dtkPresDate;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}