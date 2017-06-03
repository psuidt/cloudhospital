namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmAccountDetail
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
            this.ucAccountTab1 = new HIS_IPManage.Winform.ViewForm.UCAccountTab();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.ucAccountTab1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(905, 590);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            this.panelEx1.Text = "panelEx1";
            // 
            // ucAccountTab1
            // 
            this.ucAccountTab1.AccountId = 0;
            this.ucAccountTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountTab1.DTotalFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.DTotalPaymentFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.DTotalRoundFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.frmName = "UCAccountTab";
            this.ucAccountTab1.InvokeController = null;
            this.ucAccountTab1.Location = new System.Drawing.Point(0, 0);
            this.ucAccountTab1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucAccountTab1.Name = "ucAccountTab1";
            this.ucAccountTab1.Size = new System.Drawing.Size(905, 590);
            this.ucAccountTab1.TabIndex = 0;
            // 
            // FrmAccountDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 590);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAccountDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "缴款明细";
            this.Load += new System.EventHandler(this.FrmAccountDetail_Load);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private UCAccountTab ucAccountTab1;
    }
}