namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmCheckType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckType));
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.chkDrugType = new System.Windows.Forms.CheckBox();
            this.chkDrugDose = new System.Windows.Forms.CheckBox();
            this.chkIsHaveStore = new System.Windows.Forms.CheckBox();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.cbxDosage = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmb_Type = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelEx7.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(217, 108);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(136, 108);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkDrugType
            // 
            this.chkDrugType.AutoSize = true;
            this.chkDrugType.BackColor = System.Drawing.Color.Transparent;
            this.chkDrugType.Font = new System.Drawing.Font("宋体", 9F);
            this.chkDrugType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chkDrugType.Location = new System.Drawing.Point(39, 15);
            this.chkDrugType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDrugType.Name = "chkDrugType";
            this.chkDrugType.Size = new System.Drawing.Size(72, 16);
            this.chkDrugType.TabIndex = 12;
            this.chkDrugType.Text = "药品类型";
            this.chkDrugType.UseVisualStyleBackColor = false;
            this.chkDrugType.CheckedChanged += new System.EventHandler(this.chkDrugType_CheckedChanged);
            // 
            // chkDrugDose
            // 
            this.chkDrugDose.AutoSize = true;
            this.chkDrugDose.BackColor = System.Drawing.Color.Transparent;
            this.chkDrugDose.Font = new System.Drawing.Font("宋体", 9F);
            this.chkDrugDose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chkDrugDose.Location = new System.Drawing.Point(39, 48);
            this.chkDrugDose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkDrugDose.Name = "chkDrugDose";
            this.chkDrugDose.Size = new System.Drawing.Size(48, 16);
            this.chkDrugDose.TabIndex = 13;
            this.chkDrugDose.Text = "剂型";
            this.chkDrugDose.UseVisualStyleBackColor = false;
            this.chkDrugDose.CheckedChanged += new System.EventHandler(this.chkDrugDose_CheckedChanged);
            // 
            // chkIsHaveStore
            // 
            this.chkIsHaveStore.AutoSize = true;
            this.chkIsHaveStore.BackColor = System.Drawing.Color.Transparent;
            this.chkIsHaveStore.Font = new System.Drawing.Font("宋体", 9F);
            this.chkIsHaveStore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chkIsHaveStore.Location = new System.Drawing.Point(39, 81);
            this.chkIsHaveStore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsHaveStore.Name = "chkIsHaveStore";
            this.chkIsHaveStore.Size = new System.Drawing.Size(138, 16);
            this.chkIsHaveStore.TabIndex = 11;
            this.chkIsHaveStore.Text = "仅盘库存不为0的药品";
            this.chkIsHaveStore.UseVisualStyleBackColor = false;
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.cbxDosage);
            this.panelEx7.Controls.Add(this.cmb_Type);
            this.panelEx7.Controls.Add(this.chkDrugType);
            this.panelEx7.Controls.Add(this.btnCancel);
            this.panelEx7.Controls.Add(this.chkIsHaveStore);
            this.panelEx7.Controls.Add(this.btnOK);
            this.panelEx7.Controls.Add(this.chkDrugDose);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx7.Location = new System.Drawing.Point(0, 0);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(304, 142);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 14;
            // 
            // cbxDosage
            // 
            this.cbxDosage.DisplayMember = "Text";
            this.cbxDosage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxDosage.Enabled = false;
            this.cbxDosage.FormattingEnabled = true;
            this.cbxDosage.ItemHeight = 15;
            this.cbxDosage.Location = new System.Drawing.Point(113, 43);
            this.cbxDosage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxDosage.Name = "cbxDosage";
            this.cbxDosage.Size = new System.Drawing.Size(153, 21);
            this.cbxDosage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxDosage.TabIndex = 20;
            // 
            // cmb_Type
            // 
            this.cmb_Type.DisplayMember = "Text";
            this.cmb_Type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_Type.Enabled = false;
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.ItemHeight = 15;
            this.cmb_Type.Location = new System.Drawing.Point(113, 12);
            this.cmb_Type.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(153, 21);
            this.cmb_Type.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_Type.TabIndex = 18;
            this.cmb_Type.DropDownClosed += new System.EventHandler(this.cmb_Type_DropDownClosed);
            // 
            // FrmCheckType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(304, 142);
            this.Controls.Add(this.panelEx7);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCheckType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据选择";
            this.TopMost = true;
            this.OpenWindowBefore += new System.EventHandler(this.FrmCheckType_OpenWindowBefore);
            this.Load += new System.EventHandler(this.FrmCheckType_Load);
            this.panelEx7.ResumeLayout(false);
            this.panelEx7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private System.Windows.Forms.CheckBox chkDrugType;
        private System.Windows.Forms.CheckBox chkDrugDose;
        private System.Windows.Forms.CheckBox chkIsHaveStore;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_Type;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbxDosage;
    }
}