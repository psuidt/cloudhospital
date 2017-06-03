namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmConvertPointsInfo
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
            this.cbbWork = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtCardTypeName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.intScore = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intCash = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intCash)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cbbWork);
            this.panelEx1.Controls.Add(this.txtCardTypeName);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.intScore);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.intCash);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.labelX11);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(303, 199);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // cbbWork
            // 
            this.cbbWork.DisplayMember = "Text";
            this.cbbWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWork.FormattingEnabled = true;
            this.cbbWork.ItemHeight = 15;
            this.cbbWork.Location = new System.Drawing.Point(93, 64);
            this.cbbWork.Margin = new System.Windows.Forms.Padding(2);
            this.cbbWork.Name = "cbbWork";
            this.cbbWork.Size = new System.Drawing.Size(170, 21);
            this.cbbWork.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbWork.TabIndex = 1;
            // 
            // txtCardTypeName
            // 
            // 
            // 
            // 
            this.txtCardTypeName.Border.Class = "TextBoxBorder";
            this.txtCardTypeName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCardTypeName.Enabled = false;
            this.txtCardTypeName.Location = new System.Drawing.Point(94, 28);
            this.txtCardTypeName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCardTypeName.Name = "txtCardTypeName";
            this.txtCardTypeName.Size = new System.Drawing.Size(169, 21);
            this.txtCardTypeName.TabIndex = 41;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(188, 138);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 40;
            this.btnClose.Text = "关闭（&C）";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(89, 138);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存（&S）";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // intScore
            // 
            // 
            // 
            // 
            this.intScore.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intScore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intScore.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intScore.Location = new System.Drawing.Point(203, 99);
            this.intScore.Margin = new System.Windows.Forms.Padding(2);
            this.intScore.MaxValue = 1000;
            this.intScore.MinValue = 1;
            this.intScore.Name = "intScore";
            this.intScore.ShowUpDown = true;
            this.intScore.Size = new System.Drawing.Size(60, 21);
            this.intScore.TabIndex = 3;
            this.intScore.Value = 1;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.Purple;
            this.labelX3.Location = new System.Drawing.Point(168, 101);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(31, 18);
            this.labelX3.TabIndex = 37;
            this.labelX3.Text = "兑换";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.Purple;
            this.labelX2.Location = new System.Drawing.Point(38, 101);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 36;
            this.labelX2.Text = "现金(元)";
            // 
            // intCash
            // 
            // 
            // 
            // 
            this.intCash.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intCash.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intCash.Location = new System.Drawing.Point(94, 99);
            this.intCash.Margin = new System.Windows.Forms.Padding(2);
            this.intCash.MaxValue = 200;
            this.intCash.MinValue = 1;
            this.intCash.Name = "intCash";
            this.intCash.ShowUpDown = true;
            this.intCash.Size = new System.Drawing.Size(60, 21);
            this.intCash.TabIndex = 2;
            this.intCash.Value = 1;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(28, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 33;
            this.labelX1.Text = "帐户类型";
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.ForeColor = System.Drawing.Color.Purple;
            this.labelX11.Location = new System.Drawing.Point(28, 67);
            this.labelX11.Name = "labelX11";
            this.labelX11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX11.Size = new System.Drawing.Size(61, 19);
            this.labelX11.TabIndex = 30;
            this.labelX11.Text = "机构名称";
            this.labelX11.Click += new System.EventHandler(this.labelX11_Click);
            // 
            // FrmConvertPointsInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 199);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConvertPointsInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "积分兑换设置明细";
            this.Load += new System.EventHandler(this.FrmConvertPointsInfo_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intCash)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.Editors.IntegerInput intScore;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intCash;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCardTypeName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbWork;
    }
}