namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmAllAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAllAccount));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.cmbStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.btnRecive = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnStatic = new DevComponents.DotNetBar.ButtonX();
            this.txtEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.chkEmp = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.sdtDate = new EfwControls.CustomControl.StatDateTime();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgAllNotAccount = new EfwControls.CustomControl.DataGrid();
            this.empName1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundingFee1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountid1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dgAllAccount = new EfwControls.CustomControl.DataGrid();
            this.Selected = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.ReceivBillNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivFlagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundingFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accountid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAllNotAccount)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAllAccount)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cmbStatus);
            this.panelEx1.Controls.Add(this.btnRecive);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnStatic);
            this.panelEx1.Controls.Add(this.txtEmp);
            this.panelEx1.Controls.Add(this.chkEmp);
            this.panelEx1.Controls.Add(this.sdtDate);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1101, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DisplayMember = "Text";
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.ItemHeight = 15;
            this.cmbStatus.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cmbStatus.Location = new System.Drawing.Point(605, 10);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(104, 21);
            this.cmbStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbStatus.TabIndex = 13;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "未收款";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "已收款";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "全部";
            // 
            // btnRecive
            // 
            this.btnRecive.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRecive.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRecive.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRecive.Image = ((System.Drawing.Image)(resources.GetObject("btnRecive.Image")));
            this.btnRecive.Location = new System.Drawing.Point(923, 9);
            this.btnRecive.Name = "btnRecive";
            this.btnRecive.Size = new System.Drawing.Size(75, 22);
            this.btnRecive.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRecive.TabIndex = 12;
            this.btnRecive.Text = "收款(&S)";
            this.btnRecive.Click += new System.EventHandler(this.btnRecive_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1014, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStatic
            // 
            this.btnStatic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStatic.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStatic.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStatic.Image = ((System.Drawing.Image)(resources.GetObject("btnStatic.Image")));
            this.btnStatic.Location = new System.Drawing.Point(832, 9);
            this.btnStatic.Name = "btnStatic";
            this.btnStatic.Size = new System.Drawing.Size(75, 22);
            this.btnStatic.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStatic.TabIndex = 9;
            this.btnStatic.Text = "查询(&Q)";
            this.btnStatic.Click += new System.EventHandler(this.btnStatic_Click);
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
            this.txtEmp.Enabled = false;
            this.txtEmp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEmp.IsEnterShowCard = true;
            this.txtEmp.IsNumSelected = false;
            this.txtEmp.IsPage = true;
            this.txtEmp.IsShowLetter = false;
            this.txtEmp.IsShowPage = false;
            this.txtEmp.IsShowSeq = true;
            this.txtEmp.Location = new System.Drawing.Point(469, 10);
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
            this.txtEmp.Size = new System.Drawing.Size(130, 21);
            this.txtEmp.TabIndex = 7;
            // 
            // chkEmp
            // 
            // 
            // 
            // 
            this.chkEmp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkEmp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEmp.Location = new System.Drawing.Point(403, 10);
            this.chkEmp.Name = "chkEmp";
            this.chkEmp.Size = new System.Drawing.Size(69, 19);
            this.chkEmp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkEmp.TabIndex = 6;
            this.chkEmp.Text = "交款人";
            this.chkEmp.CheckedChanged += new System.EventHandler(this.chkEmp_CheckedChanged);
            // 
            // sdtDate
            // 
            this.sdtDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtDate.DateFormat = "yyyy-MM-dd";
            this.sdtDate.DateWidth = 120;
            this.sdtDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtDate.Location = new System.Drawing.Point(57, 10);
            this.sdtDate.Name = "sdtDate";
            this.sdtDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtDate.Size = new System.Drawing.Size(340, 21);
            this.sdtDate.TabIndex = 5;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(3, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 19);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "交款日期";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.dgAllNotAccount);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.expandablePanel1.ExpandOnTitleClick = true;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 369);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(1101, 163);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 1;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "未缴款记录";
            // 
            // dgAllNotAccount
            // 
            this.dgAllNotAccount.AllowSortWhenClickColumnHeader = false;
            this.dgAllNotAccount.AllowUserToAddRows = false;
            this.dgAllNotAccount.AllowUserToResizeColumns = false;
            this.dgAllNotAccount.AllowUserToResizeRows = false;
            this.dgAllNotAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgAllNotAccount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAllNotAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgAllNotAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAllNotAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.empName1,
            this.FeeType,
            this.LastDate,
            this.TotalFee1,
            this.RoundingFee1,
            this.accountid1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAllNotAccount.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgAllNotAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAllNotAccount.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgAllNotAccount.HighlightSelectedColumnHeaders = false;
            this.dgAllNotAccount.Location = new System.Drawing.Point(0, 26);
            this.dgAllNotAccount.Name = "dgAllNotAccount";
            this.dgAllNotAccount.ReadOnly = true;
            this.dgAllNotAccount.RowHeadersWidth = 30;
            this.dgAllNotAccount.RowTemplate.Height = 23;
            this.dgAllNotAccount.SelectAllSignVisible = false;
            this.dgAllNotAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAllNotAccount.SeqVisible = true;
            this.dgAllNotAccount.SetCustomStyle = false;
            this.dgAllNotAccount.Size = new System.Drawing.Size(1101, 137);
            this.dgAllNotAccount.TabIndex = 1;
            this.dgAllNotAccount.DoubleClick += new System.EventHandler(this.dgAllNotAccount_DoubleClick);
            // 
            // empName1
            // 
            this.empName1.DataPropertyName = "empName";
            this.empName1.HeaderText = "收费员";
            this.empName1.Name = "empName1";
            this.empName1.ReadOnly = true;
            this.empName1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FeeType
            // 
            this.FeeType.DataPropertyName = "FeeType";
            this.FeeType.HeaderText = "费用类型";
            this.FeeType.Name = "FeeType";
            this.FeeType.ReadOnly = true;
            this.FeeType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LastDate
            // 
            this.LastDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LastDate.DataPropertyName = "LastDate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Format = "yyyy-MM-dd HH:mm:ss";
            this.LastDate.DefaultCellStyle = dataGridViewCellStyle2;
            this.LastDate.HeaderText = "上次缴款日期";
            this.LastDate.Name = "LastDate";
            this.LastDate.ReadOnly = true;
            this.LastDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotalFee1
            // 
            this.TotalFee1.DataPropertyName = "TotalFee";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.TotalFee1.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalFee1.HeaderText = "总金额";
            this.TotalFee1.Name = "TotalFee1";
            this.TotalFee1.ReadOnly = true;
            this.TotalFee1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TotalFee1.Width = 140;
            // 
            // RoundingFee1
            // 
            this.RoundingFee1.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.RoundingFee1.DefaultCellStyle = dataGridViewCellStyle4;
            this.RoundingFee1.HeaderText = "凑整金额";
            this.RoundingFee1.Name = "RoundingFee1";
            this.RoundingFee1.ReadOnly = true;
            this.RoundingFee1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // accountid1
            // 
            this.accountid1.DataPropertyName = "accountid";
            this.accountid1.HeaderText = "Column1";
            this.accountid1.Name = "accountid1";
            this.accountid1.ReadOnly = true;
            this.accountid1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.accountid1.Visible = false;
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 363);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(1101, 6);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 2;
            this.expandableSplitter1.TabStop = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.dgAllAccount);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.DrawTitleBox = false;
            this.groupPanel1.Location = new System.Drawing.Point(0, 40);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(1101, 323);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 3;
            this.groupPanel1.Text = "已缴款记录";
            // 
            // dgAllAccount
            // 
            this.dgAllAccount.AllowSortWhenClickColumnHeader = false;
            this.dgAllAccount.AllowUserToAddRows = false;
            this.dgAllAccount.AllowUserToResizeColumns = false;
            this.dgAllAccount.AllowUserToResizeRows = false;
            this.dgAllAccount.BackgroundColor = System.Drawing.Color.White;
            this.dgAllAccount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAllAccount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgAllAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAllAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.ReceivBillNO,
            this.AccountType,
            this.empName,
            this.AccountDate,
            this.ReceivFlagName,
            this.ReceivEmpName,
            this.Column1,
            this.TotalFee,
            this.RoundingFee,
            this.accountid,
            this.ReceivFlag});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAllAccount.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgAllAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAllAccount.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgAllAccount.HighlightSelectedColumnHeaders = false;
            this.dgAllAccount.Location = new System.Drawing.Point(0, 0);
            this.dgAllAccount.Name = "dgAllAccount";
            this.dgAllAccount.ReadOnly = true;
            this.dgAllAccount.RowHeadersWidth = 30;
            this.dgAllAccount.RowTemplate.Height = 23;
            this.dgAllAccount.SelectAllSignVisible = false;
            this.dgAllAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAllAccount.SeqVisible = true;
            this.dgAllAccount.SetCustomStyle = false;
            this.dgAllAccount.Size = new System.Drawing.Size(1101, 305);
            this.dgAllAccount.TabIndex = 0;
            this.dgAllAccount.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAllAccount_CellClick);
            this.dgAllAccount.DoubleClick += new System.EventHandler(this.dgAllAccount_DoubleClick);
            // 
            // Selected
            // 
            this.Selected.Checked = true;
            this.Selected.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Selected.CheckValue = "N";
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "选";
            this.Selected.Name = "Selected";
            this.Selected.ReadOnly = true;
            this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Selected.Width = 30;
            // 
            // ReceivBillNO
            // 
            this.ReceivBillNO.DataPropertyName = "ReceivBillNO";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ReceivBillNO.DefaultCellStyle = dataGridViewCellStyle7;
            this.ReceivBillNO.HeaderText = "缴款单号";
            this.ReceivBillNO.MinimumWidth = 60;
            this.ReceivBillNO.Name = "ReceivBillNO";
            this.ReceivBillNO.ReadOnly = true;
            this.ReceivBillNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReceivBillNO.Width = 60;
            // 
            // AccountType
            // 
            this.AccountType.DataPropertyName = "AccountType";
            this.AccountType.HeaderText = "交款类型";
            this.AccountType.MinimumWidth = 100;
            this.AccountType.Name = "AccountType";
            this.AccountType.ReadOnly = true;
            this.AccountType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // empName
            // 
            this.empName.DataPropertyName = "empName";
            this.empName.HeaderText = "收费员";
            this.empName.MinimumWidth = 80;
            this.empName.Name = "empName";
            this.empName.ReadOnly = true;
            this.empName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.empName.Width = 80;
            // 
            // AccountDate
            // 
            this.AccountDate.DataPropertyName = "AccountDate";
            dataGridViewCellStyle8.Format = "yyyy-MM-dd HH:mm:ss";
            this.AccountDate.DefaultCellStyle = dataGridViewCellStyle8;
            this.AccountDate.HeaderText = "缴款日期";
            this.AccountDate.MinimumWidth = 120;
            this.AccountDate.Name = "AccountDate";
            this.AccountDate.ReadOnly = true;
            this.AccountDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AccountDate.Width = 160;
            // 
            // ReceivFlagName
            // 
            this.ReceivFlagName.DataPropertyName = "ReceivFlagName";
            this.ReceivFlagName.HeaderText = "收款状态";
            this.ReceivFlagName.MinimumWidth = 60;
            this.ReceivFlagName.Name = "ReceivFlagName";
            this.ReceivFlagName.ReadOnly = true;
            this.ReceivFlagName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReceivFlagName.Width = 60;
            // 
            // ReceivEmpName
            // 
            this.ReceivEmpName.DataPropertyName = "ReceivEmpName";
            this.ReceivEmpName.HeaderText = "收款人";
            this.ReceivEmpName.MinimumWidth = 100;
            this.ReceivEmpName.Name = "ReceivEmpName";
            this.ReceivEmpName.ReadOnly = true;
            this.ReceivEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "ReceivDate";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Format = "yyyy-MM-dd HH:mm:ss";
            this.Column1.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column1.HeaderText = "收款时间";
            this.Column1.MinimumWidth = 120;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotalFee
            // 
            this.TotalFee.DataPropertyName = "TotalFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.TotalFee.DefaultCellStyle = dataGridViewCellStyle10;
            this.TotalFee.HeaderText = "总金额";
            this.TotalFee.MinimumWidth = 80;
            this.TotalFee.Name = "TotalFee";
            this.TotalFee.ReadOnly = true;
            this.TotalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TotalFee.Width = 140;
            // 
            // RoundingFee
            // 
            this.RoundingFee.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.RoundingFee.DefaultCellStyle = dataGridViewCellStyle11;
            this.RoundingFee.HeaderText = "凑整金额";
            this.RoundingFee.MinimumWidth = 60;
            this.RoundingFee.Name = "RoundingFee";
            this.RoundingFee.ReadOnly = true;
            this.RoundingFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RoundingFee.Width = 60;
            // 
            // accountid
            // 
            this.accountid.DataPropertyName = "accountid";
            this.accountid.HeaderText = "Column1";
            this.accountid.Name = "accountid";
            this.accountid.ReadOnly = true;
            this.accountid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.accountid.Visible = false;
            // 
            // ReceivFlag
            // 
            this.ReceivFlag.DataPropertyName = "ReceivFlag";
            this.ReceivFlag.HeaderText = "Column1";
            this.ReceivFlag.Name = "ReceivFlag";
            this.ReceivFlag.ReadOnly = true;
            this.ReceivFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReceivFlag.Visible = false;
            // 
            // FrmAllAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 532);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.expandablePanel1);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmAllAccount";
            this.Text = "缴款查询";
            this.OpenWindowBefore += new System.EventHandler(this.FrmAllAccount_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAllNotAccount)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAllAccount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.TextBoxCard txtEmp;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkEmp;
        private EfwControls.CustomControl.StatDateTime sdtDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnRecive;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnStatic;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private EfwControls.CustomControl.DataGrid dgAllNotAccount;
        private EfwControls.CustomControl.DataGrid dgAllAccount;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbStatus;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoundingFee1;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn accountid;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoundingFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivFlagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn empName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivBillNO;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Selected;
    }
}