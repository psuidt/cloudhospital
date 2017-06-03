namespace HIS_IPDoctor.Winform.ViewForm
{
    partial class FrmOrderTempManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard6 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard7 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard8 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard9 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard2 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard3 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard4 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard5 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOrderTempManage));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.grdLongOrderDeteil = new EfwControls.CustomControl.GridBoxCard();
            this.LongItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDosageUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongFrenquency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongDropSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongExecDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongEntrust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tolInsert = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.tolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tolDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tolAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.tolRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.grdTempOrderDetail = new EfwControls.CustomControl.GridBoxCard();
            this.TempItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempDosage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempDosageUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempChannelName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempFrenquency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempDropSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempExecDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TempEntrust = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.cmbDrugStore = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtTempName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnNew = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpd = new DevComponents.DotNetBar.ButtonItem();
            this.btnDel = new DevComponents.DotNetBar.ButtonItem();
            this.btnSave = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.advTempDetails = new DevComponents.AdvTree.AdvTree();
            this.TempManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tolAddType = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdType = new System.Windows.Forms.ToolStripMenuItem();
            this.tolDelType = new System.Windows.Forms.ToolStripMenuItem();
            this.tolAddTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tolDelTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.advRoot = new DevComponents.AdvTree.AdvTree();
            this.advRootNode = new DevComponents.AdvTree.Node();
            this.advTheHospitalTemp = new DevComponents.AdvTree.Node();
            this.advDeptTemp = new DevComponents.AdvTree.Node();
            this.advPersonalTemp = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLongOrderDeteil)).BeginInit();
            this.GridManage.SuspendLayout();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTempOrderDetail)).BeginInit();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advTempDetails)).BeginInit();
            this.TempManage.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advRoot)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabControl1);
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 730);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.Transparent;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(228, 57);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1036, 673);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 3;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.grdLongOrderDeteil);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1036, 648);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // grdLongOrderDeteil
            // 
            this.grdLongOrderDeteil.AllowSortWhenClickColumnHeader = false;
            this.grdLongOrderDeteil.AllowUserToAddRows = false;
            this.grdLongOrderDeteil.AllowUserToDeleteRows = false;
            this.grdLongOrderDeteil.AllowUserToResizeColumns = false;
            this.grdLongOrderDeteil.AllowUserToResizeRows = false;
            this.grdLongOrderDeteil.BackgroundColor = System.Drawing.Color.White;
            this.grdLongOrderDeteil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLongOrderDeteil.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdLongOrderDeteil.ColumnHeadersHeight = 30;
            this.grdLongOrderDeteil.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LongItemID,
            this.LongItemName,
            this.LongDosage,
            this.LongDosageUnit,
            this.LongChannelName,
            this.LongFrenquency,
            this.LongDropSpec,
            this.LongExecDeptName,
            this.LongEntrust});
            this.grdLongOrderDeteil.ContextMenuStrip = this.GridManage;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdLongOrderDeteil.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdLongOrderDeteil.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLongOrderDeteil.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdLongOrderDeteil.HideSelectionCardWhenCustomInput = false;
            this.grdLongOrderDeteil.HighlightSelectedColumnHeaders = false;
            this.grdLongOrderDeteil.IsInputNumSelectedCard = true;
            this.grdLongOrderDeteil.IsShowLetter = false;
            this.grdLongOrderDeteil.IsShowPage = true;
            this.grdLongOrderDeteil.Location = new System.Drawing.Point(1, 1);
            this.grdLongOrderDeteil.Name = "grdLongOrderDeteil";
            this.grdLongOrderDeteil.ReadOnly = true;
            this.grdLongOrderDeteil.RowHeadersWidth = 30;
            this.grdLongOrderDeteil.RowTemplate.Height = 23;
            this.grdLongOrderDeteil.SelectAllSignVisible = false;
            dataGridViewSelectionCard6.BindColumnIndex = 0;
            dataGridViewSelectionCard6.CardColumn = "ItemCode|编码|100,ItemName|项目名称|150,Standard|规格|100,StoreAmount|库存数|80,SellPrice|单价" +
    "|80,UnPickUnit|单位|80,ExecDeptName|执行科室|90";
            dataGridViewSelectionCard6.CardSize = new System.Drawing.Size(680, 280);
            dataGridViewSelectionCard6.DataSource = null;
            dataGridViewSelectionCard6.FilterResult = null;
            dataGridViewSelectionCard6.IsPage = true;
            dataGridViewSelectionCard6.Memo = "药品项目材料";
            dataGridViewSelectionCard6.PageTotalRecord = 0;
            dataGridViewSelectionCard6.QueryFields = new string[] {
        "ItemCode",
        "ItemName",
        "Pym",
        "Wbm"};
            dataGridViewSelectionCard6.QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            dataGridViewSelectionCard6.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard6.ShowCardColumns = null;
            dataGridViewSelectionCard7.BindColumnIndex = 0;
            dataGridViewSelectionCard7.CardColumn = null;
            dataGridViewSelectionCard7.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard7.DataSource = null;
            dataGridViewSelectionCard7.FilterResult = null;
            dataGridViewSelectionCard7.IsPage = true;
            dataGridViewSelectionCard7.Memo = null;
            dataGridViewSelectionCard7.PageTotalRecord = 0;
            dataGridViewSelectionCard7.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard7.QueryFieldsString = "";
            dataGridViewSelectionCard7.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard7.ShowCardColumns = null;
            dataGridViewSelectionCard8.BindColumnIndex = 0;
            dataGridViewSelectionCard8.CardColumn = null;
            dataGridViewSelectionCard8.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard8.DataSource = null;
            dataGridViewSelectionCard8.FilterResult = null;
            dataGridViewSelectionCard8.IsPage = true;
            dataGridViewSelectionCard8.Memo = null;
            dataGridViewSelectionCard8.PageTotalRecord = 0;
            dataGridViewSelectionCard8.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard8.QueryFieldsString = "";
            dataGridViewSelectionCard8.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard8.ShowCardColumns = null;
            dataGridViewSelectionCard9.BindColumnIndex = 0;
            dataGridViewSelectionCard9.CardColumn = null;
            dataGridViewSelectionCard9.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard9.DataSource = null;
            dataGridViewSelectionCard9.FilterResult = null;
            dataGridViewSelectionCard9.IsPage = true;
            dataGridViewSelectionCard9.Memo = null;
            dataGridViewSelectionCard9.PageTotalRecord = 0;
            dataGridViewSelectionCard9.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard9.QueryFieldsString = "";
            dataGridViewSelectionCard9.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard9.ShowCardColumns = null;
            this.grdLongOrderDeteil.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard6,
        dataGridViewSelectionCard7,
        dataGridViewSelectionCard8,
        dataGridViewSelectionCard9};
            this.grdLongOrderDeteil.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdLongOrderDeteil.SelectionNumKeyBoards = null;
            this.grdLongOrderDeteil.SeqVisible = true;
            this.grdLongOrderDeteil.SetCustomStyle = false;
            this.grdLongOrderDeteil.Size = new System.Drawing.Size(1034, 646);
            this.grdLongOrderDeteil.TabIndex = 0;
            this.grdLongOrderDeteil.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.grdLongOrderDeteil_SelectCardRowSelected);
            this.grdLongOrderDeteil.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.grdLongOrderDeteil_DataGridViewCellPressEnterKey);
            this.grdLongOrderDeteil.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdLongOrderDeteil_CellValueChanged);
            this.grdLongOrderDeteil.CurrentCellChanged += new System.EventHandler(this.grdLongOrderDeteil_CurrentCellChanged);
            // 
            // LongItemID
            // 
            this.LongItemID.DataPropertyName = "ItemID";
            this.LongItemID.HeaderText = "编码";
            this.LongItemID.Name = "LongItemID";
            this.LongItemID.ReadOnly = true;
            this.LongItemID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LongItemName
            // 
            this.LongItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LongItemName.DataPropertyName = "ItemName";
            this.LongItemName.HeaderText = "医嘱内容";
            this.LongItemName.Name = "LongItemName";
            this.LongItemName.ReadOnly = true;
            this.LongItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LongDosage
            // 
            this.LongDosage.DataPropertyName = "Dosage";
            this.LongDosage.HeaderText = "剂量";
            this.LongDosage.Name = "LongDosage";
            this.LongDosage.ReadOnly = true;
            this.LongDosage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongDosage.Width = 70;
            // 
            // LongDosageUnit
            // 
            this.LongDosageUnit.DataPropertyName = "DosageUnit";
            this.LongDosageUnit.HeaderText = "单位";
            this.LongDosageUnit.Name = "LongDosageUnit";
            this.LongDosageUnit.ReadOnly = true;
            this.LongDosageUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongDosageUnit.Width = 70;
            // 
            // LongChannelName
            // 
            this.LongChannelName.DataPropertyName = "ChannelName";
            this.LongChannelName.HeaderText = "用法";
            this.LongChannelName.Name = "LongChannelName";
            this.LongChannelName.ReadOnly = true;
            this.LongChannelName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongChannelName.Width = 70;
            // 
            // LongFrenquency
            // 
            this.LongFrenquency.DataPropertyName = "Frenquency";
            this.LongFrenquency.HeaderText = "频次";
            this.LongFrenquency.Name = "LongFrenquency";
            this.LongFrenquency.ReadOnly = true;
            this.LongFrenquency.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongFrenquency.Width = 70;
            // 
            // LongDropSpec
            // 
            this.LongDropSpec.DataPropertyName = "DropSpec";
            this.LongDropSpec.HeaderText = "滴速";
            this.LongDropSpec.Name = "LongDropSpec";
            this.LongDropSpec.ReadOnly = true;
            this.LongDropSpec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongDropSpec.Width = 70;
            // 
            // LongExecDeptName
            // 
            this.LongExecDeptName.DataPropertyName = "ExecDeptName";
            this.LongExecDeptName.HeaderText = "执行科室";
            this.LongExecDeptName.Name = "LongExecDeptName";
            this.LongExecDeptName.ReadOnly = true;
            this.LongExecDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongExecDeptName.Width = 120;
            // 
            // LongEntrust
            // 
            this.LongEntrust.DataPropertyName = "Entrust";
            this.LongEntrust.HeaderText = "嘱托";
            this.LongEntrust.Name = "LongEntrust";
            this.LongEntrust.ReadOnly = true;
            this.LongEntrust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LongEntrust.Width = 200;
            // 
            // GridManage
            // 
            this.GridManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolInsert,
            this.tolUpdate,
            this.tolDelete,
            this.tolDeleteGroup,
            this.tolAuto,
            this.tolRefresh});
            this.GridManage.Name = "GridManage";
            this.GridManage.Size = new System.Drawing.Size(211, 136);
            // 
            // tolInsert
            // 
            this.tolInsert.Name = "tolInsert";
            this.tolInsert.Size = new System.Drawing.Size(210, 22);
            this.tolInsert.Text = "插入一行(Insert)";
            this.tolInsert.Click += new System.EventHandler(this.tolInsert_Click);
            // 
            // tolUpdate
            // 
            this.tolUpdate.Name = "tolUpdate";
            this.tolUpdate.Size = new System.Drawing.Size(210, 22);
            this.tolUpdate.Text = "修改医嘱(Ctrl+F2)";
            this.tolUpdate.Click += new System.EventHandler(this.tolUpdate_Click);
            // 
            // tolDelete
            // 
            this.tolDelete.Name = "tolDelete";
            this.tolDelete.Size = new System.Drawing.Size(210, 22);
            this.tolDelete.Text = "删除一行(Delete)";
            this.tolDelete.Click += new System.EventHandler(this.tolDelete_Click);
            // 
            // tolDeleteGroup
            // 
            this.tolDeleteGroup.Name = "tolDeleteGroup";
            this.tolDeleteGroup.Size = new System.Drawing.Size(210, 22);
            this.tolDeleteGroup.Text = "删除一组(Ctrl+Del)";
            this.tolDeleteGroup.Click += new System.EventHandler(this.tolDeleteGroup_Click);
            // 
            // tolAuto
            // 
            this.tolAuto.Name = "tolAuto";
            this.tolAuto.Size = new System.Drawing.Size(210, 22);
            this.tolAuto.Text = "自由录入(Crtl+Insert)";
            this.tolAuto.Click += new System.EventHandler(this.tolAuto_Click);
            // 
            // tolRefresh
            // 
            this.tolRefresh.Name = "tolRefresh";
            this.tolRefresh.Size = new System.Drawing.Size(210, 22);
            this.tolRefresh.Text = "刷新药品选项卡(Ctrl+F5)";
            this.tolRefresh.Click += new System.EventHandler(this.tolRefresh_Click);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "长期医嘱";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.grdTempOrderDetail);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1036, 648);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // grdTempOrderDetail
            // 
            this.grdTempOrderDetail.AllowSortWhenClickColumnHeader = false;
            this.grdTempOrderDetail.AllowUserToAddRows = false;
            this.grdTempOrderDetail.AllowUserToDeleteRows = false;
            this.grdTempOrderDetail.AllowUserToResizeColumns = false;
            this.grdTempOrderDetail.AllowUserToResizeRows = false;
            this.grdTempOrderDetail.BackgroundColor = System.Drawing.Color.White;
            this.grdTempOrderDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTempOrderDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTempOrderDetail.ColumnHeadersHeight = 30;
            this.grdTempOrderDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TempItemID,
            this.TempItemName,
            this.TempDosage,
            this.TempDosageUnit,
            this.TempChannelName,
            this.TempFrenquency,
            this.TempDropSpec,
            this.TempExecDeptName,
            this.TempEntrust});
            this.grdTempOrderDetail.ContextMenuStrip = this.GridManage;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTempOrderDetail.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdTempOrderDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTempOrderDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTempOrderDetail.HideSelectionCardWhenCustomInput = false;
            this.grdTempOrderDetail.HighlightSelectedColumnHeaders = false;
            this.grdTempOrderDetail.IsInputNumSelectedCard = true;
            this.grdTempOrderDetail.IsShowLetter = false;
            this.grdTempOrderDetail.IsShowPage = true;
            this.grdTempOrderDetail.Location = new System.Drawing.Point(1, 1);
            this.grdTempOrderDetail.Name = "grdTempOrderDetail";
            this.grdTempOrderDetail.ReadOnly = true;
            this.grdTempOrderDetail.RowHeadersWidth = 30;
            this.grdTempOrderDetail.RowTemplate.Height = 23;
            this.grdTempOrderDetail.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 0;
            dataGridViewSelectionCard1.CardColumn = "ItemCode|编码|100,ItemName|项目名称|150,Standard|规格|100,StoreAmount|库存数|80,SellPrice|单价" +
    "|80,UnPickUnit|单位|80,ExecDeptName|执行科室|90";
            dataGridViewSelectionCard1.CardSize = new System.Drawing.Size(680, 280);
            dataGridViewSelectionCard1.DataSource = null;
            dataGridViewSelectionCard1.FilterResult = null;
            dataGridViewSelectionCard1.IsPage = true;
            dataGridViewSelectionCard1.Memo = "药品项目材料";
            dataGridViewSelectionCard1.PageTotalRecord = 0;
            dataGridViewSelectionCard1.QueryFields = new string[] {
        "ItemCode",
        "ItemName",
        "Pym",
        "Wbm"};
            dataGridViewSelectionCard1.QueryFieldsString = "ItemCode,ItemName,Pym,Wbm";
            dataGridViewSelectionCard1.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard1.ShowCardColumns = null;
            dataGridViewSelectionCard2.BindColumnIndex = 0;
            dataGridViewSelectionCard2.CardColumn = null;
            dataGridViewSelectionCard2.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard2.DataSource = null;
            dataGridViewSelectionCard2.FilterResult = null;
            dataGridViewSelectionCard2.IsPage = true;
            dataGridViewSelectionCard2.Memo = null;
            dataGridViewSelectionCard2.PageTotalRecord = 0;
            dataGridViewSelectionCard2.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard2.QueryFieldsString = "";
            dataGridViewSelectionCard2.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard2.ShowCardColumns = null;
            dataGridViewSelectionCard3.BindColumnIndex = 0;
            dataGridViewSelectionCard3.CardColumn = null;
            dataGridViewSelectionCard3.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard3.DataSource = null;
            dataGridViewSelectionCard3.FilterResult = null;
            dataGridViewSelectionCard3.IsPage = true;
            dataGridViewSelectionCard3.Memo = null;
            dataGridViewSelectionCard3.PageTotalRecord = 0;
            dataGridViewSelectionCard3.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard3.QueryFieldsString = "";
            dataGridViewSelectionCard3.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard3.ShowCardColumns = null;
            dataGridViewSelectionCard4.BindColumnIndex = 0;
            dataGridViewSelectionCard4.CardColumn = null;
            dataGridViewSelectionCard4.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard4.DataSource = null;
            dataGridViewSelectionCard4.FilterResult = null;
            dataGridViewSelectionCard4.IsPage = true;
            dataGridViewSelectionCard4.Memo = null;
            dataGridViewSelectionCard4.PageTotalRecord = 0;
            dataGridViewSelectionCard4.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard4.QueryFieldsString = "";
            dataGridViewSelectionCard4.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard4.ShowCardColumns = null;
            dataGridViewSelectionCard5.BindColumnIndex = 0;
            dataGridViewSelectionCard5.CardColumn = null;
            dataGridViewSelectionCard5.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard5.DataSource = null;
            dataGridViewSelectionCard5.FilterResult = null;
            dataGridViewSelectionCard5.IsPage = true;
            dataGridViewSelectionCard5.Memo = null;
            dataGridViewSelectionCard5.PageTotalRecord = 0;
            dataGridViewSelectionCard5.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard5.QueryFieldsString = "";
            dataGridViewSelectionCard5.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard5.ShowCardColumns = null;
            this.grdTempOrderDetail.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1,
        dataGridViewSelectionCard2,
        dataGridViewSelectionCard3,
        dataGridViewSelectionCard4,
        dataGridViewSelectionCard5};
            this.grdTempOrderDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTempOrderDetail.SelectionNumKeyBoards = null;
            this.grdTempOrderDetail.SeqVisible = true;
            this.grdTempOrderDetail.SetCustomStyle = false;
            this.grdTempOrderDetail.Size = new System.Drawing.Size(1034, 646);
            this.grdTempOrderDetail.TabIndex = 1;
            this.grdTempOrderDetail.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.grdTempOrderDetail_SelectCardRowSelected);
            this.grdTempOrderDetail.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.grdTempOrderDetail_DataGridViewCellPressEnterKey);
            this.grdTempOrderDetail.CurrentCellChanged += new System.EventHandler(this.grdTempOrderDetail_CurrentCellChanged);
            // 
            // TempItemID
            // 
            this.TempItemID.DataPropertyName = "ItemID";
            this.TempItemID.HeaderText = "编码";
            this.TempItemID.Name = "TempItemID";
            this.TempItemID.ReadOnly = true;
            this.TempItemID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TempItemName
            // 
            this.TempItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TempItemName.DataPropertyName = "ItemName";
            this.TempItemName.HeaderText = "医嘱内容";
            this.TempItemName.Name = "TempItemName";
            this.TempItemName.ReadOnly = true;
            this.TempItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TempDosage
            // 
            this.TempDosage.DataPropertyName = "Dosage";
            this.TempDosage.HeaderText = "剂量";
            this.TempDosage.Name = "TempDosage";
            this.TempDosage.ReadOnly = true;
            this.TempDosage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempDosage.Width = 70;
            // 
            // TempDosageUnit
            // 
            this.TempDosageUnit.DataPropertyName = "DosageUnit";
            this.TempDosageUnit.HeaderText = "单位";
            this.TempDosageUnit.Name = "TempDosageUnit";
            this.TempDosageUnit.ReadOnly = true;
            this.TempDosageUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempDosageUnit.Width = 70;
            // 
            // TempChannelName
            // 
            this.TempChannelName.DataPropertyName = "ChannelName";
            this.TempChannelName.HeaderText = "用法";
            this.TempChannelName.Name = "TempChannelName";
            this.TempChannelName.ReadOnly = true;
            this.TempChannelName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempChannelName.Width = 70;
            // 
            // TempFrenquency
            // 
            this.TempFrenquency.DataPropertyName = "Frenquency";
            this.TempFrenquency.HeaderText = "频次";
            this.TempFrenquency.Name = "TempFrenquency";
            this.TempFrenquency.ReadOnly = true;
            this.TempFrenquency.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempFrenquency.Width = 70;
            // 
            // TempDropSpec
            // 
            this.TempDropSpec.DataPropertyName = "DropSpec";
            this.TempDropSpec.HeaderText = "滴速";
            this.TempDropSpec.Name = "TempDropSpec";
            this.TempDropSpec.ReadOnly = true;
            this.TempDropSpec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempDropSpec.Width = 70;
            // 
            // TempExecDeptName
            // 
            this.TempExecDeptName.DataPropertyName = "ExecDeptName";
            this.TempExecDeptName.HeaderText = "执行科室";
            this.TempExecDeptName.Name = "TempExecDeptName";
            this.TempExecDeptName.ReadOnly = true;
            this.TempExecDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempExecDeptName.Width = 120;
            // 
            // TempEntrust
            // 
            this.TempEntrust.DataPropertyName = "Entrust";
            this.TempEntrust.HeaderText = "嘱托";
            this.TempEntrust.Name = "TempEntrust";
            this.TempEntrust.ReadOnly = true;
            this.TempEntrust.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TempEntrust.Width = 150;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "临时医嘱";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.cmbDrugStore);
            this.panelEx4.Controls.Add(this.labelX2);
            this.panelEx4.Controls.Add(this.txtTempName);
            this.panelEx4.Controls.Add(this.labelX1);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(228, 25);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1036, 32);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 4;
            // 
            // cmbDrugStore
            // 
            this.cmbDrugStore.DisplayMember = "Text";
            this.cmbDrugStore.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDrugStore.FormattingEnabled = true;
            this.cmbDrugStore.ItemHeight = 15;
            this.cmbDrugStore.Location = new System.Drawing.Point(434, 6);
            this.cmbDrugStore.Name = "cmbDrugStore";
            this.cmbDrugStore.Size = new System.Drawing.Size(175, 21);
            this.cmbDrugStore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDrugStore.TabIndex = 4;
            this.cmbDrugStore.SelectedIndexChanged += new System.EventHandler(this.cmbDrugStore_SelectedIndexChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(371, 8);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(57, 21);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "选择药房";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTempName
            // 
            // 
            // 
            // 
            this.txtTempName.Border.Class = "TextBoxBorder";
            this.txtTempName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTempName.Enabled = false;
            this.txtTempName.Location = new System.Drawing.Point(73, 6);
            this.txtTempName.Name = "txtTempName";
            this.txtTempName.ReadOnly = true;
            this.txtTempName.Size = new System.Drawing.Size(274, 21);
            this.txtTempName.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(21, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(46, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "模板名";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNew,
            this.btnUpd,
            this.btnDel,
            this.btnSave,
            this.btnRefresh,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(228, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1036, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 2;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnNew
            // 
            this.btnNew.Name = "btnNew";
            this.btnNew.Text = "新增(F6)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnUpd
            // 
            this.btnUpd.Name = "btnUpd";
            this.btnUpd.Text = "修改(F7)";
            this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Name = "btnDel";
            this.btnDel.Text = "删除(F8)";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Name = "btnSave";
            this.btnSave.Text = "保存(F9)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新(F10)";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
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
            this.expandableSplitter1.Location = new System.Drawing.Point(222, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 730);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 1;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.advTempDetails);
            this.panelEx2.Controls.Add(this.expandableSplitter2);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(222, 730);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // advTempDetails
            // 
            this.advTempDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTempDetails.AllowDrop = true;
            this.advTempDetails.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTempDetails.BackgroundStyle.Class = "TreeBorderKey";
            this.advTempDetails.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTempDetails.ContextMenuStrip = this.TempManage;
            this.advTempDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advTempDetails.Location = new System.Drawing.Point(0, 117);
            this.advTempDetails.Name = "advTempDetails";
            this.advTempDetails.NodesConnector = this.nodeConnector2;
            this.advTempDetails.NodeStyle = this.elementStyle2;
            this.advTempDetails.PathSeparator = ";";
            this.advTempDetails.Size = new System.Drawing.Size(222, 613);
            this.advTempDetails.Styles.Add(this.elementStyle2);
            this.advTempDetails.TabIndex = 2;
            this.advTempDetails.Text = "advTree1";
            this.advTempDetails.SelectedIndexChanged += new System.EventHandler(this.advTempDetails_SelectedIndexChanged);
            // 
            // TempManage
            // 
            this.TempManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolAddType,
            this.tolUpdType,
            this.tolDelType,
            this.tolAddTemp,
            this.tolUpdTemp,
            this.tolDelTemp});
            this.TempManage.Name = "TempManage";
            this.TempManage.Size = new System.Drawing.Size(149, 136);
            this.TempManage.Opening += new System.ComponentModel.CancelEventHandler(this.TempManage_Opening);
            // 
            // tolAddType
            // 
            this.tolAddType.Name = "tolAddType";
            this.tolAddType.Size = new System.Drawing.Size(148, 22);
            this.tolAddType.Text = "新增医嘱分类";
            this.tolAddType.Click += new System.EventHandler(this.tolAddType_Click);
            // 
            // tolUpdType
            // 
            this.tolUpdType.Name = "tolUpdType";
            this.tolUpdType.Size = new System.Drawing.Size(148, 22);
            this.tolUpdType.Text = "修改医嘱分类";
            this.tolUpdType.Click += new System.EventHandler(this.tolUpdType_Click);
            // 
            // tolDelType
            // 
            this.tolDelType.Name = "tolDelType";
            this.tolDelType.Size = new System.Drawing.Size(148, 22);
            this.tolDelType.Text = "删除医嘱分类";
            this.tolDelType.Click += new System.EventHandler(this.tolDelType_Click);
            // 
            // tolAddTemp
            // 
            this.tolAddTemp.Name = "tolAddTemp";
            this.tolAddTemp.Size = new System.Drawing.Size(148, 22);
            this.tolAddTemp.Text = "新增医嘱模板";
            this.tolAddTemp.Click += new System.EventHandler(this.tolAddTemp_Click);
            // 
            // tolUpdTemp
            // 
            this.tolUpdTemp.Name = "tolUpdTemp";
            this.tolUpdTemp.Size = new System.Drawing.Size(148, 22);
            this.tolUpdTemp.Text = "修改医嘱模板";
            this.tolUpdTemp.Click += new System.EventHandler(this.tolUpdTemp_Click);
            // 
            // tolDelTemp
            // 
            this.tolDelTemp.Name = "tolDelTemp";
            this.tolDelTemp.Size = new System.Drawing.Size(148, 22);
            this.tolDelTemp.Text = "删除医嘱模板";
            this.tolDelTemp.Click += new System.EventHandler(this.tolDelTemp_Click);
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle2
            // 
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // expandableSplitter2
            // 
            this.expandableSplitter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.expandableSplitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandableSplitter2.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter2.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter2.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter2.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter2.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter2.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter2.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter2.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter2.Location = new System.Drawing.Point(0, 111);
            this.expandableSplitter2.Name = "expandableSplitter2";
            this.expandableSplitter2.Size = new System.Drawing.Size(222, 6);
            this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter2.TabIndex = 1;
            this.expandableSplitter2.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.advRoot);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(222, 111);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // advRoot
            // 
            this.advRoot.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advRoot.AllowDrop = true;
            this.advRoot.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advRoot.BackgroundStyle.Class = "TreeBorderKey";
            this.advRoot.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advRoot.Location = new System.Drawing.Point(0, 0);
            this.advRoot.Name = "advRoot";
            this.advRoot.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.advRootNode});
            this.advRoot.NodesConnector = this.nodeConnector1;
            this.advRoot.NodeStyle = this.elementStyle1;
            this.advRoot.PathSeparator = ";";
            this.advRoot.Size = new System.Drawing.Size(222, 111);
            this.advRoot.Styles.Add(this.elementStyle1);
            this.advRoot.TabIndex = 0;
            this.advRoot.Text = "advTree1";
            this.advRoot.SelectedIndexChanged += new System.EventHandler(this.advRoot_SelectedIndexChanged);
            // 
            // advRootNode
            // 
            this.advRootNode.Expanded = true;
            this.advRootNode.Name = "advRootNode";
            this.advRootNode.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.advTheHospitalTemp,
            this.advDeptTemp,
            this.advPersonalTemp});
            this.advRootNode.Text = "所有级别";
            // 
            // advTheHospitalTemp
            // 
            this.advTheHospitalTemp.Expanded = true;
            this.advTheHospitalTemp.Name = "advTheHospitalTemp";
            this.advTheHospitalTemp.Text = "全院模板";
            // 
            // advDeptTemp
            // 
            this.advDeptTemp.Name = "advDeptTemp";
            this.advDeptTemp.Text = "科室模板";
            // 
            // advPersonalTemp
            // 
            this.advPersonalTemp.Expanded = true;
            this.advPersonalTemp.Name = "advPersonalTemp";
            this.advPersonalTemp.Text = "个人模板";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "包类型.png");
            this.imageList1.Images.SetKeyName(1, "Order.png");
            // 
            // FrmOrderTempManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOrderTempManage";
            this.ShowIcon = false;
            this.Text = "医嘱模板管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmOrderTempManage_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOrderTempManage_KeyDown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLongOrderDeteil)).EndInit();
            this.GridManage.ResumeLayout(false);
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTempOrderDetail)).EndInit();
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advTempDetails)).EndInit();
            this.TempManage.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advRoot)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.AdvTree.AdvTree advRoot;
        private DevComponents.AdvTree.Node advRootNode;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
        private DevComponents.AdvTree.Node advTheHospitalTemp;
        private DevComponents.AdvTree.Node advDeptTemp;
        private DevComponents.AdvTree.Node advPersonalTemp;
        private DevComponents.AdvTree.AdvTree advTempDetails;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private System.Windows.Forms.ContextMenuStrip TempManage;
        private System.Windows.Forms.ToolStripMenuItem tolAddType;
        private System.Windows.Forms.ToolStripMenuItem tolDelType;
        private System.Windows.Forms.ToolStripMenuItem tolAddTemp;
        private System.Windows.Forms.ToolStripMenuItem tolDelTemp;
        private System.Windows.Forms.ToolStripMenuItem tolUpdType;
        private System.Windows.Forms.ToolStripMenuItem tolUpdTemp;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnNew;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonItem btnUpd;
        private DevComponents.DotNetBar.ButtonItem btnDel;
        private DevComponents.DotNetBar.ButtonItem btnSave;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTempName;
        private EfwControls.CustomControl.GridBoxCard grdLongOrderDeteil;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDrugStore;
        private EfwControls.CustomControl.GridBoxCard grdTempOrderDetail;
        private System.Windows.Forms.ContextMenuStrip GridManage;
        private System.Windows.Forms.ToolStripMenuItem tolInsert;
        private System.Windows.Forms.ToolStripMenuItem tolUpdate;
        private System.Windows.Forms.ToolStripMenuItem tolDelete;
        private System.Windows.Forms.ToolStripMenuItem tolDeleteGroup;
        private System.Windows.Forms.ToolStripMenuItem tolAuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDosageUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongChannelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongFrenquency;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongDropSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongExecDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongEntrust;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempDosage;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempDosageUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempChannelName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempFrenquency;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempDropSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempExecDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TempEntrust;
        private System.Windows.Forms.ToolStripMenuItem tolRefresh;
    }
}