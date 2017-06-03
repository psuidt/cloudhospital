namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmAddScore
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
            this.txtAddScore = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNewScore = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.intCash = new DevComponents.Editors.IntegerInput();
            this.txtOldScore = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtCardNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtCardType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtMemberName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.cbCash = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbPOS = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intCash)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel1);
            this.panelEx1.Controls.Add(this.txtAddScore);
            this.panelEx1.Controls.Add(this.txtNewScore);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.intCash);
            this.panelEx1.Controls.Add(this.txtOldScore);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txtCardNO);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txtCardType);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.txtMemberName);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(314, 313);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // txtAddScore
            // 
            // 
            // 
            // 
            this.txtAddScore.Border.Class = "TextBoxBorder";
            this.txtAddScore.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAddScore.Location = new System.Drawing.Point(178, 209);
            this.txtAddScore.Name = "txtAddScore";
            this.txtAddScore.ReadOnly = true;
            this.txtAddScore.Size = new System.Drawing.Size(98, 21);
            this.txtAddScore.TabIndex = 57;
            // 
            // txtNewScore
            // 
            // 
            // 
            // 
            this.txtNewScore.Border.Class = "TextBoxBorder";
            this.txtNewScore.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewScore.Location = new System.Drawing.Point(90, 236);
            this.txtNewScore.Name = "txtNewScore";
            this.txtNewScore.ReadOnly = true;
            this.txtNewScore.Size = new System.Drawing.Size(186, 21);
            this.txtNewScore.TabIndex = 56;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(40, 239);
            this.labelX6.Name = "labelX6";
            this.labelX6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX6.Size = new System.Drawing.Size(44, 18);
            this.labelX6.TabIndex = 55;
            this.labelX6.Text = "充值后";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(201, 271);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 54;
            this.btnClose.Text = "关闭（&C）";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(94, 271);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存（&S）";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(125, 212);
            this.labelX4.Margin = new System.Windows.Forms.Padding(2);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(44, 18);
            this.labelX4.TabIndex = 51;
            this.labelX4.Text = "元兑换";
            // 
            // intCash
            // 
            // 
            // 
            // 
            this.intCash.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intCash.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intCash.Location = new System.Drawing.Point(28, 209);
            this.intCash.MaxValue = 1000;
            this.intCash.MinValue = 1;
            this.intCash.Name = "intCash";
            this.intCash.ShowUpDown = true;
            this.intCash.Size = new System.Drawing.Size(92, 21);
            this.intCash.TabIndex = 1;
            this.intCash.Value = 5;
            this.intCash.ValueChanged += new System.EventHandler(this.intCash_ValueChanged);
            // 
            // txtOldScore
            // 
            // 
            // 
            // 
            this.txtOldScore.Border.Class = "TextBoxBorder";
            this.txtOldScore.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOldScore.Location = new System.Drawing.Point(90, 109);
            this.txtOldScore.Name = "txtOldScore";
            this.txtOldScore.ReadOnly = true;
            this.txtOldScore.Size = new System.Drawing.Size(186, 21);
            this.txtOldScore.TabIndex = 48;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(26, 112);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 47;
            this.labelX3.Text = "现在积分";
            // 
            // txtCardNO
            // 
            // 
            // 
            // 
            this.txtCardNO.Border.Class = "TextBoxBorder";
            this.txtCardNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCardNO.Location = new System.Drawing.Point(90, 82);
            this.txtCardNO.Name = "txtCardNO";
            this.txtCardNO.ReadOnly = true;
            this.txtCardNO.Size = new System.Drawing.Size(186, 21);
            this.txtCardNO.TabIndex = 46;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(28, 28);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 45;
            this.labelX2.Text = "会员姓名";
            // 
            // txtCardType
            // 
            // 
            // 
            // 
            this.txtCardType.Border.Class = "TextBoxBorder";
            this.txtCardType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCardType.Location = new System.Drawing.Point(90, 55);
            this.txtCardType.Name = "txtCardType";
            this.txtCardType.ReadOnly = true;
            this.txtCardType.Size = new System.Drawing.Size(186, 21);
            this.txtCardType.TabIndex = 44;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 58);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 43;
            this.labelX1.Text = "帐户类型";
            // 
            // txtMemberName
            // 
            // 
            // 
            // 
            this.txtMemberName.Border.Class = "TextBoxBorder";
            this.txtMemberName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMemberName.Location = new System.Drawing.Point(90, 28);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.ReadOnly = true;
            this.txtMemberName.Size = new System.Drawing.Size(186, 21);
            this.txtMemberName.TabIndex = 42;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(26, 85);
            this.labelX5.Name = "labelX5";
            this.labelX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX5.Size = new System.Drawing.Size(56, 18);
            this.labelX5.TabIndex = 41;
            this.labelX5.Text = "帐户号码";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.cbCash);
            this.expandablePanel1.Controls.Add(this.cbPOS);
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(28, 137);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(248, 64);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 58;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "支付方式";
            // 
            // cbCash
            // 
            this.cbCash.AutoSize = true;
            this.cbCash.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbCash.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbCash.Location = new System.Drawing.Point(23, 35);
            this.cbCash.Name = "cbCash";
            this.cbCash.Size = new System.Drawing.Size(51, 18);
            this.cbCash.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbCash.TabIndex = 6;
            this.cbCash.Text = "现金";
            // 
            // cbPOS
            // 
            this.cbPOS.AutoSize = true;
            this.cbPOS.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbPOS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbPOS.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbPOS.Location = new System.Drawing.Point(150, 35);
            this.cbPOS.Name = "cbPOS";
            this.cbPOS.Size = new System.Drawing.Size(45, 16);
            this.cbPOS.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPOS.TabIndex = 7;
            this.cbPOS.Text = "POS";
            // 
            // FrmAddScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 313);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddScore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "积分充值";
            this.Load += new System.EventHandler(this.FrmAddScore_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intCash)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            this.expandablePanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemberName;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.IntegerInput intCash;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOldScore;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCardNO;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCardType;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewScore;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAddScore;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbCash;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbPOS;
    }
}