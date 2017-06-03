namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmAddDoctorTemp
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveTemp = new DevComponents.DotNetBar.ButtonX();
            this.txtTempMemo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtTempName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.frmAddTemp = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSaveTemp);
            this.panelEx1.Controls.Add(this.txtTempMemo);
            this.panelEx1.Controls.Add(this.txtTempName);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(342, 237);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(239, 187);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "退出(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveTemp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveTemp.Location = new System.Drawing.Point(159, 187);
            this.btnSaveTemp.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Size = new System.Drawing.Size(75, 22);
            this.btnSaveTemp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveTemp.TabIndex = 3;
            this.btnSaveTemp.Text = "保存(&S)";
            this.btnSaveTemp.Click += new System.EventHandler(this.btnSaveTemp_Click);
            // 
            // txtTempMemo
            // 
            // 
            // 
            // 
            this.txtTempMemo.Border.Class = "TextBoxBorder";
            this.txtTempMemo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTempMemo.Location = new System.Drawing.Point(93, 54);
            this.txtTempMemo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTempMemo.Multiline = true;
            this.txtTempMemo.Name = "txtTempMemo";
            this.txtTempMemo.Size = new System.Drawing.Size(221, 128);
            this.txtTempMemo.TabIndex = 2;
            // 
            // txtTempName
            // 
            // 
            // 
            // 
            this.txtTempName.Border.Class = "TextBoxBorder";
            this.txtTempName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTempName.Location = new System.Drawing.Point(93, 28);
            this.txtTempName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTempName.Name = "txtTempName";
            this.txtTempName.Size = new System.Drawing.Size(221, 21);
            this.txtTempName.TabIndex = 1;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(28, 110);
            this.labelX2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 19);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "模板说明";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Purple;
            this.labelX1.Location = new System.Drawing.Point(28, 28);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "模板名";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmAddTemp
            // 
            this.frmAddTemp.IsSkip = true;
            // 
            // FrmAddDoctorTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(342, 237);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddDoctorTemp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新增账单模板";
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTempName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTempMemo;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSaveTemp;
        private EfwControls.CustomControl.frmForm frmAddTemp;
    }
}