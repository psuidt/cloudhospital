namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmDoctorTempManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.trvTempList = new DevComponents.AdvTree.AdvTree();
            this.TempManage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tolAddTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tolDelTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.tolOperationTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.node1 = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.expandableSplitter2 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.grdTempList = new EfwControls.CustomControl.GridBoxCard();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddTemp = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdTemp = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelTemp = new DevComponents.DotNetBar.ButtonItem();
            this.btnSaveTemp = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.LocalCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trvTempList)).BeginInit();
            this.TempManage.SuspendLayout();
            this.expandablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTempList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.trvTempList);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Margin = new System.Windows.Forms.Padding(2);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(191, 730);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 8;
            this.expandablePanel1.TitleHeight = 30;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "模板列表";
            // 
            // trvTempList
            // 
            this.trvTempList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.trvTempList.AllowDrop = true;
            this.trvTempList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.trvTempList.BackgroundStyle.Class = "TreeBorderKey";
            this.trvTempList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.trvTempList.ContextMenuStrip = this.TempManage;
            this.trvTempList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvTempList.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trvTempList.Location = new System.Drawing.Point(0, 30);
            this.trvTempList.Margin = new System.Windows.Forms.Padding(2);
            this.trvTempList.Name = "trvTempList";
            this.trvTempList.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node1});
            this.trvTempList.NodesConnector = this.nodeConnector1;
            this.trvTempList.NodeStyle = this.elementStyle1;
            this.trvTempList.PathSeparator = ";";
            this.trvTempList.Size = new System.Drawing.Size(191, 700);
            this.trvTempList.TabIndex = 4;
            this.trvTempList.Text = "advTree1";
            this.trvTempList.SelectedIndexChanged += new System.EventHandler(this.trvTempList_SelectedIndexChanged);
            // 
            // TempManage
            // 
            this.TempManage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolAddTemp,
            this.tolUpdTemp,
            this.tolDelTemp,
            this.tolOperationTemp});
            this.TempManage.Name = "contextMenuStrip1";
            this.TempManage.Size = new System.Drawing.Size(125, 92);
            this.TempManage.Text = "模板管理";
            this.TempManage.Opening += new System.ComponentModel.CancelEventHandler(this.TempManage_Opening);
            // 
            // tolAddTemp
            // 
            this.tolAddTemp.Name = "tolAddTemp";
            this.tolAddTemp.Size = new System.Drawing.Size(124, 22);
            this.tolAddTemp.Text = "新增模板";
            this.tolAddTemp.Click += new System.EventHandler(this.tolAddTemp_Click);
            // 
            // tolUpdTemp
            // 
            this.tolUpdTemp.Name = "tolUpdTemp";
            this.tolUpdTemp.Size = new System.Drawing.Size(124, 22);
            this.tolUpdTemp.Text = "重命名";
            this.tolUpdTemp.Click += new System.EventHandler(this.tolUpdTemp_Click);
            // 
            // tolDelTemp
            // 
            this.tolDelTemp.Name = "tolDelTemp";
            this.tolDelTemp.Size = new System.Drawing.Size(124, 22);
            this.tolDelTemp.Text = "停用模板";
            this.tolDelTemp.Click += new System.EventHandler(this.tolDelTemp_Click);
            // 
            // tolOperationTemp
            // 
            this.tolOperationTemp.Name = "tolOperationTemp";
            this.tolOperationTemp.Size = new System.Drawing.Size(124, 22);
            this.tolOperationTemp.Text = "启用模板";
            this.tolOperationTemp.Click += new System.EventHandler(this.tolOperationTemp_Click);
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "全部模板";
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
            // expandableSplitter2
            // 
            this.expandableSplitter2.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter2.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter2.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
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
            this.expandableSplitter2.Location = new System.Drawing.Point(191, 0);
            this.expandableSplitter2.Margin = new System.Windows.Forms.Padding(2);
            this.expandableSplitter2.Name = "expandableSplitter2";
            this.expandableSplitter2.Size = new System.Drawing.Size(4, 730);
            this.expandableSplitter2.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter2.TabIndex = 10;
            this.expandableSplitter2.TabStop = false;
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.grdTempList);
            this.expandablePanel2.Controls.Add(this.bar1);
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel2.ExpandButtonVisible = false;
            this.expandablePanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(195, 0);
            this.expandablePanel2.Margin = new System.Windows.Forms.Padding(2);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(1069, 730);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 9;
            this.expandablePanel2.TitleHeight = 30;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "账单模板明细";
            // 
            // grdTempList
            // 
            this.grdTempList.AllowSortWhenClickColumnHeader = false;
            this.grdTempList.AllowUserToAddRows = false;
            this.grdTempList.AllowUserToDeleteRows = false;
            this.grdTempList.AllowUserToResizeColumns = false;
            this.grdTempList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.grdTempList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTempList.BackgroundColor = System.Drawing.Color.White;
            this.grdTempList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTempList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdTempList.ColumnHeadersHeight = 27;
            this.grdTempList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocalCode,
            this.ItemName,
            this.Type,
            this.Price,
            this.Count});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTempList.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdTempList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTempList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.grdTempList.HideSelectionCardWhenCustomInput = false;
            this.grdTempList.HighlightSelectedColumnHeaders = false;
            this.grdTempList.IsInputNumSelectedCard = false;
            this.grdTempList.IsShowLetter = false;
            this.grdTempList.IsShowPage = true;
            this.grdTempList.Location = new System.Drawing.Point(0, 55);
            this.grdTempList.Margin = new System.Windows.Forms.Padding(2);
            this.grdTempList.Name = "grdTempList";
            this.grdTempList.ReadOnly = true;
            this.grdTempList.RowHeadersWidth = 30;
            this.grdTempList.RowTemplate.Height = 23;
            this.grdTempList.SelectAllSignVisible = false;
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
            this.grdTempList.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.grdTempList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTempList.SelectionNumKeyBoards = null;
            this.grdTempList.SeqVisible = true;
            this.grdTempList.SetCustomStyle = true;
            this.grdTempList.Size = new System.Drawing.Size(1069, 675);
            this.grdTempList.TabIndex = 4;
            this.grdTempList.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.grdTempList_SelectCardRowSelected);
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.DockTabStripHeight = 30;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddTemp,
            this.btnUpdTemp,
            this.btnDelTemp,
            this.btnSaveTemp,
            this.btnCancel});
            this.bar1.Location = new System.Drawing.Point(0, 30);
            this.bar1.Margin = new System.Windows.Forms.Padding(2);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1069, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnAddTemp
            // 
            this.btnAddTemp.Name = "btnAddTemp";
            this.btnAddTemp.Text = "新增(&N)";
            this.btnAddTemp.Click += new System.EventHandler(this.btnAddTemp_Click);
            // 
            // btnUpdTemp
            // 
            this.btnUpdTemp.Name = "btnUpdTemp";
            this.btnUpdTemp.Text = "修改(&U)";
            this.btnUpdTemp.Click += new System.EventHandler(this.btnUpdTemp_Click);
            // 
            // btnDelTemp
            // 
            this.btnDelTemp.Name = "btnDelTemp";
            this.btnDelTemp.Text = "删除(&D)";
            this.btnDelTemp.Click += new System.EventHandler(this.btnDelTemp_Click);
            // 
            // btnSaveTemp
            // 
            this.btnSaveTemp.Name = "btnSaveTemp";
            this.btnSaveTemp.Text = "保存(&S)";
            this.btnSaveTemp.Click += new System.EventHandler(this.btnSaveTemp_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel2);
            this.panelEx1.Controls.Add(this.expandableSplitter2);
            this.panelEx1.Controls.Add(this.expandablePanel1);
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
            // LocalCode
            // 
            this.LocalCode.DataPropertyName = "ItemCode";
            this.LocalCode.FillWeight = 180F;
            this.LocalCode.HeaderText = "本院编码";
            this.LocalCode.Name = "LocalCode";
            this.LocalCode.ReadOnly = true;
            this.LocalCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LocalCode.Width = 180;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.FillWeight = 350F;
            this.ItemName.HeaderText = "项目名称";
            this.ItemName.MinimumWidth = 350;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "ItemClassName";
            this.Type.FillWeight = 180F;
            this.Type.HeaderText = "项目类型";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Type.Width = 180;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N4";
            dataGridViewCellStyle3.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle3;
            this.Price.FillWeight = 150F;
            this.Price.HeaderText = "单价";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            this.Price.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Price.Width = 150;
            // 
            // Count
            // 
            this.Count.DataPropertyName = "ItemAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Count.DefaultCellStyle = dataGridViewCellStyle4;
            this.Count.HeaderText = "数量";
            this.Count.Name = "Count";
            this.Count.ReadOnly = true;
            this.Count.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Count.Width = 110;
            // 
            // FrmDoctorTempManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDoctorTempManage";
            this.ShowIcon = false;
            this.Text = "账单模板管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDoctorTempManage_OpenWindowBefore);
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trvTempList)).EndInit();
            this.TempManage.ResumeLayout(false);
            this.expandablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTempList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ContextMenuStrip TempManage;
        private System.Windows.Forms.ToolStripMenuItem tolAddTemp;
        private System.Windows.Forms.ToolStripMenuItem tolUpdTemp;
        private System.Windows.Forms.ToolStripMenuItem tolDelTemp;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.AdvTree.AdvTree trvTempList;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.Windows.Forms.ToolStripMenuItem tolOperationTemp;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private DevComponents.AdvTree.Node node1;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter2;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private EfwControls.CustomControl.GridBoxCard grdTempList;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddTemp;
        private DevComponents.DotNetBar.ButtonItem btnUpdTemp;
        private DevComponents.DotNetBar.ButtonItem btnDelTemp;
        private DevComponents.DotNetBar.ButtonItem btnSaveTemp;
        private DevComponents.DotNetBar.ButtonItem btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocalCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
    }
}