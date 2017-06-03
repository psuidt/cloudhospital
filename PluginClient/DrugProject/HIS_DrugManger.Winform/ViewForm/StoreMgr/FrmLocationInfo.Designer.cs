namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmLocationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocationInfo));
            this.LocationName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.BtnSave = new DevComponents.DotNetBar.ButtonX();
            this.BtnCancel = new DevComponents.DotNetBar.ButtonX();
            this.LocationRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.frmLocation = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LocationName
            // 
            // 
            // 
            // 
            this.LocationName.Border.Class = "TextBoxBorder";
            this.LocationName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LocationName.Location = new System.Drawing.Point(87, 28);
            this.LocationName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocationName.MaxLength = 20;
            this.LocationName.Name = "LocationName";
            this.LocationName.Size = new System.Drawing.Size(200, 21);
            this.LocationName.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.Location = new System.Drawing.Point(131, 99);
            this.BtnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 22);
            this.BtnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "保存(&S)";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(212, 99);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 22);
            this.BtnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnCancel.TabIndex = 5;
            this.BtnCancel.Text = "关闭(&C)";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // LocationRemark
            // 
            // 
            // 
            // 
            this.LocationRemark.Border.Class = "TextBoxBorder";
            this.LocationRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LocationRemark.Location = new System.Drawing.Point(87, 53);
            this.LocationRemark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocationRemark.Multiline = true;
            this.LocationRemark.Name = "LocationRemark";
            this.LocationRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LocationRemark.Size = new System.Drawing.Size(200, 42);
            this.LocationRemark.TabIndex = 6;
            // 
            // frmLocation
            // 
            this.frmLocation.IsSkip = true;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.BtnSave);
            this.panelEx1.Controls.Add(this.BtnCancel);
            this.panelEx1.Controls.Add(this.LocationRemark);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.LocationName);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(304, 142);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 7;
            this.panelEx1.Text = "panelEx1";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelX2.Location = new System.Drawing.Point(22, 54);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(63, 22);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "备       注";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.labelX1.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX1.Location = new System.Drawing.Point(22, 28);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(62, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "库位名称";
            // 
            // FrmLocationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 142);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmLocationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库位信息";
            this.Load += new System.EventHandler(this.FrmLocationInfo_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX LocationName;
        private DevComponents.DotNetBar.ButtonX BtnSave;
        private DevComponents.DotNetBar.ButtonX BtnCancel;
        private DevComponents.DotNetBar.Controls.TextBoxX LocationRemark;
        private EfwControls.CustomControl.frmForm frmLocation;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}