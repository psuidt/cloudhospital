namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmPatientBedChanging
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
            this.cboNewBedNo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.txtOldBedNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSerialNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPatName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cboNewBedNo);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.txtOldBedNo);
            this.panelEx1.Controls.Add(this.txtSerialNumber);
            this.panelEx1.Controls.Add(this.txtPatName);
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(438, 132);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // cboNewBedNo
            // 
            this.cboNewBedNo.DisplayMember = "Text";
            this.cboNewBedNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNewBedNo.Font = new System.Drawing.Font("宋体", 9F);
            this.cboNewBedNo.FormattingEnabled = true;
            this.cboNewBedNo.ItemHeight = 15;
            this.cboNewBedNo.Location = new System.Drawing.Point(282, 55);
            this.cboNewBedNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboNewBedNo.Name = "cboNewBedNo";
            this.cboNewBedNo.Size = new System.Drawing.Size(128, 21);
            this.cboNewBedNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNewBedNo.TabIndex = 49;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(335, 82);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 48;
            this.btnCancel.Text = "取消(&E)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(255, 82);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 22);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "确定(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOldBedNo
            // 
            // 
            // 
            // 
            this.txtOldBedNo.Border.Class = "TextBoxBorder";
            this.txtOldBedNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOldBedNo.Enabled = false;
            this.txtOldBedNo.Font = new System.Drawing.Font("宋体", 9F);
            this.txtOldBedNo.Location = new System.Drawing.Point(79, 55);
            this.txtOldBedNo.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOldBedNo.Name = "txtOldBedNo";
            this.txtOldBedNo.ReadOnly = true;
            this.txtOldBedNo.Size = new System.Drawing.Size(130, 21);
            this.txtOldBedNo.TabIndex = 10;
            // 
            // txtSerialNumber
            // 
            // 
            // 
            // 
            this.txtSerialNumber.Border.Class = "TextBoxBorder";
            this.txtSerialNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSerialNumber.Enabled = false;
            this.txtSerialNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSerialNumber.Location = new System.Drawing.Point(282, 28);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.ReadOnly = true;
            this.txtSerialNumber.Size = new System.Drawing.Size(128, 21);
            this.txtSerialNumber.TabIndex = 8;
            // 
            // txtPatName
            // 
            // 
            // 
            // 
            this.txtPatName.Border.Class = "TextBoxBorder";
            this.txtPatName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPatName.Enabled = false;
            this.txtPatName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPatName.Location = new System.Drawing.Point(79, 28);
            this.txtPatName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPatName.Name = "txtPatName";
            this.txtPatName.ReadOnly = true;
            this.txtPatName.Size = new System.Drawing.Size(130, 21);
            this.txtPatName.TabIndex = 7;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(231, 28);
            this.labelX7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(46, 22);
            this.labelX7.TabIndex = 6;
            this.labelX7.Text = "住院号";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(28, 55);
            this.labelX5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(46, 22);
            this.labelX5.TabIndex = 4;
            this.labelX5.Text = "原病床";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(231, 55);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(46, 22);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "新病床";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(28, 28);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(46, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "病人名";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // FrmPatientBedChanging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(438, 132);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPatientBedChanging";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人换床";
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOldBedNo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSerialNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPatName;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNewBedNo;
    }
}