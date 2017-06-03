namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmAllot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAllot));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtInvoiceType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtPerfChar = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtStartNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtEndNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(12, 24);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "  领用人";
            // 
            // txtEmp
            // 
            // 
            // 
            // 
            this.txtEmp.Border.Class = "TextBoxBorder";
            this.txtEmp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmp.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtEmp.ButtonCustom.Image")));
            this.txtEmp.ButtonCustom.Visible = true;
            this.txtEmp.CardColumn = null;
            this.txtEmp.DisplayField = "";
            this.txtEmp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmp.IsEnterShowCard = false;
            this.txtEmp.IsNumSelected = false;
            this.txtEmp.IsPage = true;
            this.txtEmp.IsShowLetter = false;
            this.txtEmp.IsShowPage = false;
            this.txtEmp.IsShowSeq = true;
            this.txtEmp.Location = new System.Drawing.Point(75, 25);
            this.txtEmp.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtEmp.MemberField = "";
            this.txtEmp.MemberValue = null;
            this.txtEmp.Name = "txtEmp";
            this.txtEmp.QueryFields = new string[] {
        ""};
            this.txtEmp.QueryFieldsString = "";
            this.txtEmp.SelectedValue = null;
            this.txtEmp.ShowCardColumns = null;
            this.txtEmp.ShowCardDataSource = null;
            this.txtEmp.ShowCardHeight = 0;
            this.txtEmp.ShowCardWidth = 0;
            this.txtEmp.Size = new System.Drawing.Size(172, 21);
            this.txtEmp.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(12, 55);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(57, 22);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "票据类型";
            // 
            // txtInvoiceType
            // 
            this.txtInvoiceType.DisplayMember = "Text";
            this.txtInvoiceType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.txtInvoiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtInvoiceType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtInvoiceType.FormattingEnabled = true;
            this.txtInvoiceType.ItemHeight = 15;
            this.txtInvoiceType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5});
            this.txtInvoiceType.Location = new System.Drawing.Point(75, 56);
            this.txtInvoiceType.Name = "txtInvoiceType";
            this.txtInvoiceType.Size = new System.Drawing.Size(171, 21);
            this.txtInvoiceType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtInvoiceType.TabIndex = 3;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "门诊收费";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "门诊挂号";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "住院预交金";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "住院结算";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "账户充值";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(12, 117);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(57, 22);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "票据号段";
            // 
            // txtPerfChar
            // 
            // 
            // 
            // 
            this.txtPerfChar.Border.Class = "TextBoxBorder";
            this.txtPerfChar.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPerfChar.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPerfChar.Location = new System.Drawing.Point(75, 85);
            this.txtPerfChar.Name = "txtPerfChar";
            this.txtPerfChar.Size = new System.Drawing.Size(78, 21);
            this.txtPerfChar.TabIndex = 5;
            // 
            // txtStartNO
            // 
            // 
            // 
            // 
            this.txtStartNO.Border.Class = "TextBoxBorder";
            this.txtStartNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStartNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStartNO.Location = new System.Drawing.Point(75, 118);
            this.txtStartNO.Name = "txtStartNO";
            this.txtStartNO.Size = new System.Drawing.Size(171, 21);
            this.txtStartNO.TabIndex = 6;
            // 
            // txtEndNO
            // 
            // 
            // 
            // 
            this.txtEndNO.Border.Class = "TextBoxBorder";
            this.txtEndNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEndNO.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEndNO.Location = new System.Drawing.Point(275, 118);
            this.txtEndNO.Name = "txtEndNO";
            this.txtEndNO.Size = new System.Drawing.Size(158, 21);
            this.txtEndNO.TabIndex = 7;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(252, 118);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(22, 27);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "至";
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(12, 86);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(57, 22);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "前缀字符";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(262, 263);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(345, 263);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.txtEndNO);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.txtEmp);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txtInvoiceType);
            this.panelEx1.Controls.Add(this.txtStartNO);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.txtPerfChar);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(441, 291);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 13;
            // 
            // FrmAllot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 291);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAllot";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "票据分配";
            this.Load += new System.EventHandler(this.FrmAllot_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmAllot_KeyUp);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.TextBoxCard txtEmp;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx txtInvoiceType;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPerfChar;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartNO;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndNO;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
    }
}