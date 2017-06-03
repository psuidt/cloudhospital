namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmUpdatePatientInfo
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
            this.cboNurse = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboDoctor = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSavePat = new DevComponents.DotNetBar.ButtonX();
            this.txtPatName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.frmUpdPatDoctorNurse = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cboNurse);
            this.panelEx1.Controls.Add(this.cboDoctor);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnSavePat);
            this.panelEx1.Controls.Add(this.txtPatName);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(285, 157);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // cboNurse
            // 
            this.cboNurse.DisplayMember = "Text";
            this.cboNurse.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNurse.FormattingEnabled = true;
            this.cboNurse.ItemHeight = 15;
            this.cboNurse.Location = new System.Drawing.Point(88, 80);
            this.cboNurse.Margin = new System.Windows.Forms.Padding(2);
            this.cboNurse.Name = "cboNurse";
            this.cboNurse.Size = new System.Drawing.Size(169, 21);
            this.cboNurse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNurse.TabIndex = 7;
            // 
            // cboDoctor
            // 
            this.cboDoctor.DisplayMember = "Text";
            this.cboDoctor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDoctor.FormattingEnabled = true;
            this.cboDoctor.ItemHeight = 15;
            this.cboDoctor.Location = new System.Drawing.Point(88, 54);
            this.cboDoctor.Margin = new System.Windows.Forms.Padding(2);
            this.cboDoctor.Name = "cboDoctor";
            this.cboDoctor.Size = new System.Drawing.Size(169, 21);
            this.cboDoctor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDoctor.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(182, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&E)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSavePat
            // 
            this.btnSavePat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSavePat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSavePat.Location = new System.Drawing.Point(98, 107);
            this.btnSavePat.Name = "btnSavePat";
            this.btnSavePat.Size = new System.Drawing.Size(75, 22);
            this.btnSavePat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSavePat.TabIndex = 4;
            this.btnSavePat.Text = "确定(&S)";
            this.btnSavePat.Click += new System.EventHandler(this.btnSavePat_Click);
            // 
            // txtPatName
            // 
            // 
            // 
            // 
            this.txtPatName.Border.Class = "TextBoxBorder";
            this.txtPatName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPatName.Enabled = false;
            this.txtPatName.Location = new System.Drawing.Point(88, 28);
            this.txtPatName.Name = "txtPatName";
            this.txtPatName.ReadOnly = true;
            this.txtPatName.Size = new System.Drawing.Size(168, 21);
            this.txtPatName.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(28, 80);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 22);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "责任护士";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(28, 54);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 22);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "主管医生";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "病人姓名";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmUpdPatDoctorNurse
            // 
            this.frmUpdPatDoctorNurse.IsSkip = true;
            // 
            // FrmUpdatePatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(285, 157);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdatePatientInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改医护";
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPatName;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSavePat;
        private EfwControls.CustomControl.frmForm frmUpdPatDoctorNurse;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNurse;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDoctor;
    }
}