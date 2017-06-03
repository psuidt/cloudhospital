namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmRegBaseDataSet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegBaseDataSet));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgRegItemFee = new EfwControls.CustomControl.GridBoxCard();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddItem = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveItem = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelItem = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgRegType = new EfwControls.CustomControl.DataGrid();
            this.Flag = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeCodeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalRegFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnSaveRegType = new DevComponents.DotNetBar.ButtonX();
            this.btnAddRegType = new DevComponents.DotNetBar.ButtonX();
            this.chkValid = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtRegTypeName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtRegTypeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegItemFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegType)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // superTabControl1
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1008, 661);
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 0;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControl1.Text = "superTabControl1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.panelEx1);
            this.superTabControlPanel1.Controls.Add(this.expandableSplitter1);
            this.superTabControlPanel1.Controls.Add(this.panelEx3);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1008, 633);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgRegItemFee);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(358, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(650, 633);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dgRegItemFee
            // 
            this.dgRegItemFee.AllowSortWhenClickColumnHeader = false;
            this.dgRegItemFee.AllowUserToAddRows = false;
            this.dgRegItemFee.AllowUserToResizeColumns = false;
            this.dgRegItemFee.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgRegItemFee.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRegItemFee.BackgroundColor = System.Drawing.Color.White;
            this.dgRegItemFee.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRegItemFee.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgRegItemFee.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemCode,
            this.ItemID,
            this.ItemName,
            this.ItemPrice});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRegItemFee.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgRegItemFee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRegItemFee.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgRegItemFee.HideSelectionCardWhenCustomInput = false;
            this.dgRegItemFee.HighlightSelectedColumnHeaders = false;
            this.dgRegItemFee.IsInputNumSelectedCard = false;
            this.dgRegItemFee.IsShowLetter = false;
            this.dgRegItemFee.IsShowPage = false;
            this.dgRegItemFee.Location = new System.Drawing.Point(0, 26);
            this.dgRegItemFee.MultiSelect = false;
            this.dgRegItemFee.Name = "dgRegItemFee";
            this.dgRegItemFee.ReadOnly = true;
            this.dgRegItemFee.RowHeadersWidth = 30;
            this.dgRegItemFee.RowTemplate.Height = 23;
            this.dgRegItemFee.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 0;
            dataGridViewSelectionCard1.CardColumn = null;
            dataGridViewSelectionCard1.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard1.DataSource = null;
            dataGridViewSelectionCard1.FilterResult = null;
            dataGridViewSelectionCard1.IsPage = true;
            dataGridViewSelectionCard1.Memo = null;
            dataGridViewSelectionCard1.PageTotalRecord = 0;
            dataGridViewSelectionCard1.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard1.QueryFieldsString = "";
            dataGridViewSelectionCard1.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard1.ShowCardColumns = null;
            this.dgRegItemFee.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.dgRegItemFee.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegItemFee.SelectionNumKeyBoards = null;
            this.dgRegItemFee.SeqVisible = true;
            this.dgRegItemFee.SetCustomStyle = true;
            this.dgRegItemFee.Size = new System.Drawing.Size(650, 607);
            this.dgRegItemFee.TabIndex = 1;
            this.dgRegItemFee.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgRegItemFee_SelectCardRowSelected);
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "助记码";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ReadOnly = true;
            this.ItemCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemID
            // 
            this.ItemID.DataPropertyName = "ItemID";
            this.ItemID.HeaderText = "项目ID";
            this.ItemID.Name = "ItemID";
            this.ItemID.ReadOnly = true;
            this.ItemID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "项目名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemPrice
            // 
            this.ItemPrice.DataPropertyName = "ItemPrice";
            this.ItemPrice.HeaderText = "价格";
            this.ItemPrice.Name = "ItemPrice";
            this.ItemPrice.ReadOnly = true;
            this.ItemPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddItem,
            this.btnSaveItem,
            this.btnDelItem,
            this.btnRefresh,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(650, 26);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnAddItem
            // 
            this.btnAddItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAddItem.Image = ((System.Drawing.Image)(resources.GetObject("btnAddItem.Image")));
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Text = "新增收费项目(&N)";
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveItem.Image")));
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Text = "保存收费项目(&S)";
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // btnDelItem
            // 
            this.btnDelItem.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelItem.Image = ((System.Drawing.Image)(resources.GetObject("btnDelItem.Image")));
            this.btnDelItem.Name = "btnDelItem";
            this.btnDelItem.Text = "删除收费项目(&D)";
            this.btnDelItem.Click += new System.EventHandler(this.btnDelItem_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新收费项目(&F)";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(352, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 633);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 2;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgRegType);
            this.panelEx3.Controls.Add(this.panelEx2);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(352, 633);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgRegType
            // 
            this.dgRegType.AllowSortWhenClickColumnHeader = false;
            this.dgRegType.AllowUserToAddRows = false;
            this.dgRegType.AllowUserToResizeColumns = false;
            this.dgRegType.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue;
            this.dgRegType.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgRegType.BackgroundColor = System.Drawing.Color.White;
            this.dgRegType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRegType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgRegType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Flag,
            this.Column4,
            this.RegTypeCode,
            this.TypeCodeName,
            this.TotalRegFee});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRegType.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgRegType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRegType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgRegType.HighlightSelectedColumnHeaders = false;
            this.dgRegType.Location = new System.Drawing.Point(0, 0);
            this.dgRegType.MultiSelect = false;
            this.dgRegType.Name = "dgRegType";
            this.dgRegType.ReadOnly = true;
            this.dgRegType.RowHeadersWidth = 30;
            this.dgRegType.RowTemplate.Height = 23;
            this.dgRegType.SelectAllSignVisible = false;
            this.dgRegType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegType.SeqVisible = true;
            this.dgRegType.SetCustomStyle = true;
            this.dgRegType.Size = new System.Drawing.Size(352, 497);
            this.dgRegType.TabIndex = 0;
            this.dgRegType.CurrentCellChanged += new System.EventHandler(this.dgRegType_CurrentCellChanged);
            // 
            // Flag
            // 
            this.Flag.Checked = true;
            this.Flag.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.Flag.CheckValue = "N";
            this.Flag.DataPropertyName = "Flag";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Flag.DefaultCellStyle = dataGridViewCellStyle6;
            this.Flag.HeaderText = "有效";
            this.Flag.Name = "Flag";
            this.Flag.ReadOnly = true;
            this.Flag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Flag.Width = 40;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "regtypeid";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column4.HeaderText = "类型ID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Visible = false;
            this.Column4.Width = 70;
            // 
            // RegTypeCode
            // 
            this.RegTypeCode.DataPropertyName = "RegTypeCode";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RegTypeCode.DefaultCellStyle = dataGridViewCellStyle8;
            this.RegTypeCode.HeaderText = "类型代码";
            this.RegTypeCode.Name = "RegTypeCode";
            this.RegTypeCode.ReadOnly = true;
            this.RegTypeCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RegTypeCode.Width = 70;
            // 
            // TypeCodeName
            // 
            this.TypeCodeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TypeCodeName.DataPropertyName = "regTypeName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.TypeCodeName.DefaultCellStyle = dataGridViewCellStyle9;
            this.TypeCodeName.HeaderText = "类型名称";
            this.TypeCodeName.Name = "TypeCodeName";
            this.TypeCodeName.ReadOnly = true;
            this.TypeCodeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TotalRegFee
            // 
            this.TotalRegFee.DataPropertyName = "TotalRegFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalRegFee.DefaultCellStyle = dataGridViewCellStyle10;
            this.TotalRegFee.HeaderText = "挂号金额";
            this.TotalRegFee.Name = "TotalRegFee";
            this.TotalRegFee.ReadOnly = true;
            this.TotalRegFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnSaveRegType);
            this.panelEx2.Controls.Add(this.btnAddRegType);
            this.panelEx2.Controls.Add(this.chkValid);
            this.panelEx2.Controls.Add(this.txtRegTypeName);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.txtRegTypeCode);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Location = new System.Drawing.Point(0, 497);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(352, 136);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // btnSaveRegType
            // 
            this.btnSaveRegType.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveRegType.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveRegType.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRegType.Image")));
            this.btnSaveRegType.Location = new System.Drawing.Point(200, 91);
            this.btnSaveRegType.Name = "btnSaveRegType";
            this.btnSaveRegType.Size = new System.Drawing.Size(75, 22);
            this.btnSaveRegType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveRegType.TabIndex = 6;
            this.btnSaveRegType.Text = "保存类型";
            this.btnSaveRegType.Click += new System.EventHandler(this.btnSaveRegType_Click);
            // 
            // btnAddRegType
            // 
            this.btnAddRegType.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddRegType.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddRegType.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRegType.Image")));
            this.btnAddRegType.Location = new System.Drawing.Point(119, 91);
            this.btnAddRegType.Name = "btnAddRegType";
            this.btnAddRegType.Size = new System.Drawing.Size(75, 22);
            this.btnAddRegType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddRegType.TabIndex = 5;
            this.btnAddRegType.Text = "新增类型";
            this.btnAddRegType.Click += new System.EventHandler(this.btnAddRegType_Click);
            // 
            // chkValid
            // 
            // 
            // 
            // 
            this.chkValid.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkValid.Location = new System.Drawing.Point(224, 50);
            this.chkValid.Name = "chkValid";
            this.chkValid.Size = new System.Drawing.Size(64, 23);
            this.chkValid.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkValid.TabIndex = 4;
            this.chkValid.Text = "有效";
            // 
            // txtRegTypeName
            // 
            // 
            // 
            // 
            this.txtRegTypeName.Border.Class = "TextBoxBorder";
            this.txtRegTypeName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRegTypeName.Location = new System.Drawing.Point(75, 50);
            this.txtRegTypeName.Name = "txtRegTypeName";
            this.txtRegTypeName.Size = new System.Drawing.Size(143, 21);
            this.txtRegTypeName.TabIndex = 3;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX2.Location = new System.Drawing.Point(8, 50);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(61, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "类型名称";
            // 
            // txtRegTypeCode
            // 
            // 
            // 
            // 
            this.txtRegTypeCode.Border.Class = "TextBoxBorder";
            this.txtRegTypeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRegTypeCode.Location = new System.Drawing.Point(75, 14);
            this.txtRegTypeCode.Name = "txtRegTypeCode";
            this.txtRegTypeCode.Size = new System.Drawing.Size(143, 21);
            this.txtRegTypeCode.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX1.Location = new System.Drawing.Point(8, 14);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(61, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "类型代码";
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "挂号基础数据维护";
            // 
            // FrmRegBaseDataSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.superTabControl1);
            this.Name = "FrmRegBaseDataSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "挂号基础数据设置";
            this.OpenWindowBefore += new System.EventHandler(this.FrmRegBaseDataSet_OpenWindowBefore);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRegItemFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRegType)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgRegType;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRegTypeCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkValid;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRegTypeName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btnSaveRegType;
        private DevComponents.DotNetBar.ButtonX btnAddRegType;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddItem;
        private DevComponents.DotNetBar.ButtonItem btnDelItem;
        private DevComponents.DotNetBar.ButtonItem btnSaveItem;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.GridBoxCard dgRegItemFee;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn Flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegTypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeCodeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalRegFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemPrice;
    }
}