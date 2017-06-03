namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmConversionRull
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.cbbCardType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbbWork = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btkNew = new DevComponents.DotNetBar.ButtonItem();
            this.btiEdit = new DevComponents.DotNetBar.ButtonItem();
            this.btiStop = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cbbCardTypeName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.intScore = new DevComponents.Editors.IntegerInput();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtGift = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtWorkName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgGiftInfo = new EfwControls.CustomControl.DataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GiftName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseFalgDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intScore)).BeginInit();
            this.expandablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgGiftInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Location = new System.Drawing.Point(539, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 21);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 32;
            this.btnQuery.Text = "查询（&Q）";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // cbbCardType
            // 
            this.cbbCardType.DisplayMember = "Text";
            this.cbbCardType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCardType.FormattingEnabled = true;
            this.cbbCardType.ItemHeight = 15;
            this.cbbCardType.Location = new System.Drawing.Point(347, 10);
            this.cbbCardType.Name = "cbbCardType";
            this.cbbCardType.Size = new System.Drawing.Size(186, 21);
            this.cbbCardType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbCardType.TabIndex = 30;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(285, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 21);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "帐户类型";
            // 
            // cbbWork
            // 
            this.cbbWork.DisplayMember = "Text";
            this.cbbWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWork.FormattingEnabled = true;
            this.cbbWork.ItemHeight = 15;
            this.cbbWork.Location = new System.Drawing.Point(84, 10);
            this.cbbWork.Name = "cbbWork";
            this.cbbWork.Size = new System.Drawing.Size(186, 21);
            this.cbbWork.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbWork.TabIndex = 28;
            this.cbbWork.SelectedIndexChanged += new System.EventHandler(this.cbbWork_SelectedIndexChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.cbbCardType);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.cbbWork);
            this.panelEx1.Controls.Add(this.labelX11);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1115, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(620, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "关闭（&C）";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labelX11
            // 
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX11.Location = new System.Drawing.Point(22, 10);
            this.labelX11.Name = "labelX11";
            this.labelX11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX11.Size = new System.Drawing.Size(56, 21);
            this.labelX11.TabIndex = 29;
            this.labelX11.Text = "机构名称";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockTabStripHeight = 21;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btkNew,
            this.btiEdit,
            this.btiStop});
            this.bar1.Location = new System.Drawing.Point(0, 40);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1115, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 11;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btkNew
            // 
            this.btkNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.btkNew.Name = "btkNew";
            this.btkNew.Text = "新增礼品信息（&N）";
            this.btkNew.Click += new System.EventHandler(this.btkNew_Click);
            // 
            // btiEdit
            // 
            this.btiEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.btiEdit.Name = "btiEdit";
            this.btiEdit.Text = "编辑礼品信息（&E）";
            this.btiEdit.Click += new System.EventHandler(this.BtiEdit_Click);
            // 
            // btiStop
            // 
            this.btiStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.btiStop.Name = "btiStop";
            this.btiStop.Text = "停用";
            this.btiStop.Click += new System.EventHandler(this.BtiStop_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cbbCardTypeName);
            this.panelEx2.Controls.Add(this.btnSave);
            this.panelEx2.Controls.Add(this.btnCancel);
            this.panelEx2.Controls.Add(this.intScore);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.txtGift);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.txtWorkName);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx2.Location = new System.Drawing.Point(802, 65);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(313, 370);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 12;
            // 
            // cbbCardTypeName
            // 
            this.cbbCardTypeName.DisplayMember = "Text";
            this.cbbCardTypeName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCardTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCardTypeName.FormattingEnabled = true;
            this.cbbCardTypeName.ItemHeight = 15;
            this.cbbCardTypeName.Location = new System.Drawing.Point(73, 67);
            this.cbbCardTypeName.Name = "cbbCardTypeName";
            this.cbbCardTypeName.Size = new System.Drawing.Size(228, 21);
            this.cbbCardTypeName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbCardTypeName.TabIndex = 41;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(224, 205);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 21);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "保存（&S）";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(143, 205);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 21);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "取消（&C）";
            this.btnCancel.Click += new System.EventHandler(this.BtiCancel_Click);
            // 
            // intScore
            // 
            // 
            // 
            // 
            this.intScore.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intScore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intScore.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intScore.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.intScore.Location = new System.Drawing.Point(73, 164);
            this.intScore.MaxValue = 100000;
            this.intScore.MinValue = 1;
            this.intScore.Name = "intScore";
            this.intScore.ShowUpDown = true;
            this.intScore.Size = new System.Drawing.Size(228, 21);
            this.intScore.TabIndex = 37;
            this.intScore.Value = 1;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX5.Location = new System.Drawing.Point(6, 166);
            this.labelX5.Name = "labelX5";
            this.labelX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX5.Size = new System.Drawing.Size(61, 22);
            this.labelX5.TabIndex = 36;
            this.labelX5.Text = "所需积分";
            // 
            // txtGift
            // 
            // 
            // 
            // 
            this.txtGift.Border.Class = "TextBoxBorder";
            this.txtGift.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGift.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtGift.Location = new System.Drawing.Point(73, 114);
            this.txtGift.Name = "txtGift";
            this.txtGift.Size = new System.Drawing.Size(228, 21);
            this.txtGift.TabIndex = 35;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX4.Location = new System.Drawing.Point(6, 117);
            this.labelX4.Name = "labelX4";
            this.labelX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX4.Size = new System.Drawing.Size(61, 22);
            this.labelX4.TabIndex = 34;
            this.labelX4.Text = "礼品名称";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(6, 67);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(61, 22);
            this.labelX3.TabIndex = 32;
            this.labelX3.Text = "帐户类型";
            // 
            // txtWorkName
            // 
            // 
            // 
            // 
            this.txtWorkName.Border.Class = "TextBoxBorder";
            this.txtWorkName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWorkName.Enabled = false;
            this.txtWorkName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWorkName.Location = new System.Drawing.Point(73, 18);
            this.txtWorkName.Name = "txtWorkName";
            this.txtWorkName.Size = new System.Drawing.Size(228, 21);
            this.txtWorkName.TabIndex = 31;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(6, 21);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(61, 22);
            this.labelX2.TabIndex = 30;
            this.labelX2.Text = "机构名称";
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.dgGiftInfo);
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel2.ExpandButtonVisible = false;
            this.expandablePanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(0, 65);
            this.expandablePanel2.Margin = new System.Windows.Forms.Padding(2);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(802, 370);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 13;
            this.expandablePanel2.TitleHeight = 22;
            this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel2.TitleStyle.ForeColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "会员礼品详细信息";
            // 
            // dgGiftInfo
            // 
            this.dgGiftInfo.AllowSortWhenClickColumnHeader = false;
            this.dgGiftInfo.AllowUserToAddRows = false;
            this.dgGiftInfo.AllowUserToDeleteRows = false;
            this.dgGiftInfo.AllowUserToResizeColumns = false;
            this.dgGiftInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgGiftInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgGiftInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgGiftInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGiftInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgGiftInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgGiftInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.CardTypeName,
            this.GiftName,
            this.Score,
            this.UseFalgDesc,
            this.OperateDate});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgGiftInfo.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgGiftInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgGiftInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgGiftInfo.HighlightSelectedColumnHeaders = false;
            this.dgGiftInfo.Location = new System.Drawing.Point(0, 22);
            this.dgGiftInfo.MultiSelect = false;
            this.dgGiftInfo.Name = "dgGiftInfo";
            this.dgGiftInfo.ReadOnly = true;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgGiftInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgGiftInfo.RowHeadersWidth = 35;
            this.dgGiftInfo.RowTemplate.Height = 23;
            this.dgGiftInfo.SelectAllSignVisible = false;
            this.dgGiftInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgGiftInfo.SeqVisible = true;
            this.dgGiftInfo.SetCustomStyle = false;
            this.dgGiftInfo.Size = new System.Drawing.Size(802, 348);
            this.dgGiftInfo.TabIndex = 2;
            this.dgGiftInfo.CurrentCellChanged += new System.EventHandler(this.dgGiftInfo_CurrentCellChanged);
            this.dgGiftInfo.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgGiftInfo_RowPrePaint);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "WorkName";
            this.Column1.HeaderText = "机构名称";
            this.Column1.MinimumWidth = 150;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CardTypeName
            // 
            this.CardTypeName.DataPropertyName = "CardTypeName";
            this.CardTypeName.HeaderText = "帐户类型";
            this.CardTypeName.MinimumWidth = 250;
            this.CardTypeName.Name = "CardTypeName";
            this.CardTypeName.ReadOnly = true;
            this.CardTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardTypeName.Width = 250;
            // 
            // GiftName
            // 
            this.GiftName.DataPropertyName = "GiftName";
            this.GiftName.HeaderText = "礼物";
            this.GiftName.MinimumWidth = 200;
            this.GiftName.Name = "GiftName";
            this.GiftName.ReadOnly = true;
            this.GiftName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GiftName.Width = 200;
            // 
            // Score
            // 
            this.Score.DataPropertyName = "Score";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Score.DefaultCellStyle = dataGridViewCellStyle3;
            this.Score.HeaderText = "兑换积分";
            this.Score.MinimumWidth = 100;
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            this.Score.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UseFalgDesc
            // 
            this.UseFalgDesc.DataPropertyName = "UseFalgDesc";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UseFalgDesc.DefaultCellStyle = dataGridViewCellStyle4;
            this.UseFalgDesc.HeaderText = "使用状态";
            this.UseFalgDesc.MinimumWidth = 80;
            this.UseFalgDesc.Name = "UseFalgDesc";
            this.UseFalgDesc.ReadOnly = true;
            this.UseFalgDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UseFalgDesc.Width = 80;
            // 
            // OperateDate
            // 
            this.OperateDate.DataPropertyName = "OperateDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OperateDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.OperateDate.HeaderText = "操作时间";
            this.OperateDate.MinimumWidth = 150;
            this.OperateDate.Name = "OperateDate";
            this.OperateDate.ReadOnly = true;
            this.OperateDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperateDate.Width = 150;
            // 
            // FrmConversionRull
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 435);
            this.Controls.Add(this.expandablePanel2);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmConversionRull";
            this.Text = "会员礼品管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmConversionRull_OpenWindowBefore);
            this.Shown += new System.EventHandler(this.FrmConversionRull_Shown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intScore)).EndInit();
            this.expandablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgGiftInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCardType;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbWork;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btkNew;
        private DevComponents.DotNetBar.ButtonItem btiEdit;
        private DevComponents.DotNetBar.ButtonItem btiStop;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private EfwControls.CustomControl.DataGrid dgGiftInfo;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intScore;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGift;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtWorkName;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCardTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GiftName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseFalgDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperateDate;
        private DevComponents.DotNetBar.ButtonX btnClose;
    }
}