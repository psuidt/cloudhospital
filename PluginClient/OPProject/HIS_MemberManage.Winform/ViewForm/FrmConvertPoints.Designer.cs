namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmConvertPoints
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dgCardTypeInfo = new EfwControls.CustomControl.DataGrid();
            this.CardTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flagdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btiNew = new DevComponents.DotNetBar.ButtonItem();
            this.btiEdit = new DevComponents.DotNetBar.ButtonItem();
            this.btnStop = new DevComponents.DotNetBar.ButtonItem();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgPoints = new EfwControls.CustomControl.DataGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UseFlagDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btiNewConvertPoints = new DevComponents.DotNetBar.ButtonItem();
            this.btnEditPoints = new DevComponents.DotNetBar.ButtonItem();
            this.btiStop = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCardTypeInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgCardTypeInfo);
            this.panelEx2.Controls.Add(this.bar1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(447, 536);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // dgCardTypeInfo
            // 
            this.dgCardTypeInfo.AllowSortWhenClickColumnHeader = false;
            this.dgCardTypeInfo.AllowUserToAddRows = false;
            this.dgCardTypeInfo.AllowUserToDeleteRows = false;
            this.dgCardTypeInfo.AllowUserToResizeColumns = false;
            this.dgCardTypeInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgCardTypeInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCardTypeInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgCardTypeInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCardTypeInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgCardTypeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardTypeName,
            this.CardTypeDesc,
            this.flagdesc});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCardTypeInfo.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgCardTypeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCardTypeInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCardTypeInfo.HighlightSelectedColumnHeaders = false;
            this.dgCardTypeInfo.Location = new System.Drawing.Point(0, 25);
            this.dgCardTypeInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dgCardTypeInfo.MultiSelect = false;
            this.dgCardTypeInfo.Name = "dgCardTypeInfo";
            this.dgCardTypeInfo.ReadOnly = true;
            this.dgCardTypeInfo.RowHeadersWidth = 30;
            this.dgCardTypeInfo.RowTemplate.Height = 23;
            this.dgCardTypeInfo.SelectAllSignVisible = false;
            this.dgCardTypeInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCardTypeInfo.SeqVisible = true;
            this.dgCardTypeInfo.SetCustomStyle = true;
            this.dgCardTypeInfo.Size = new System.Drawing.Size(447, 511);
            this.dgCardTypeInfo.TabIndex = 8;
            this.dgCardTypeInfo.CurrentCellChanged += new System.EventHandler(this.dgCardTypeInfo_CurrentCellChanged);
            this.dgCardTypeInfo.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgCardTypeInfo_RowPrePaint);
            // 
            // CardTypeName
            // 
            this.CardTypeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardTypeName.DataPropertyName = "CardTypeName";
            this.CardTypeName.HeaderText = "帐户类型";
            this.CardTypeName.Name = "CardTypeName";
            this.CardTypeName.ReadOnly = true;
            this.CardTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CardTypeDesc
            // 
            this.CardTypeDesc.DataPropertyName = "CardTypeDesc";
            this.CardTypeDesc.HeaderText = "卡片类型";
            this.CardTypeDesc.Name = "CardTypeDesc";
            this.CardTypeDesc.ReadOnly = true;
            this.CardTypeDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // flagdesc
            // 
            this.flagdesc.DataPropertyName = "flagdesc";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.flagdesc.DefaultCellStyle = dataGridViewCellStyle3;
            this.flagdesc.HeaderText = "帐户状态";
            this.flagdesc.Name = "flagdesc";
            this.flagdesc.ReadOnly = true;
            this.flagdesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Left;
            this.bar1.DockTabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Top;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btiNew,
            this.btiEdit,
            this.btnStop});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(447, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btiNew
            // 
            this.btiNew.FixedSize = new System.Drawing.Size(90, 22);
            this.btiNew.Name = "btiNew";
            this.btiNew.Text = "新增帐户类型";
            this.btiNew.Click += new System.EventHandler(this.btiNew_Click);
            // 
            // btiEdit
            // 
            this.btiEdit.FixedSize = new System.Drawing.Size(90, 22);
            this.btiEdit.Name = "btiEdit";
            this.btiEdit.Text = "编辑帐户类型";
            this.btiEdit.Click += new System.EventHandler(this.btiEdit_Click);
            // 
            // btnStop
            // 
            this.btnStop.FixedSize = new System.Drawing.Size(40, 22);
            this.btnStop.Name = "btnStop";
            this.btnStop.Text = "停用";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
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
            this.expandableSplitter1.Location = new System.Drawing.Point(447, 0);
            this.expandableSplitter1.Margin = new System.Windows.Forms.Padding(2);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(4, 536);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 2;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel1);
            this.panelEx1.Controls.Add(this.bar2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(451, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(792, 536);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.dgPoints);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 25);
            this.expandablePanel1.Margin = new System.Windows.Forms.Padding(2);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(792, 511);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 7;
            this.expandablePanel1.TitleHeight = 22;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "帐户类型积分兑换设置详细信息";
            // 
            // dgPoints
            // 
            this.dgPoints.AllowSortWhenClickColumnHeader = false;
            this.dgPoints.AllowUserToAddRows = false;
            this.dgPoints.AllowUserToDeleteRows = false;
            this.dgPoints.AllowUserToResizeColumns = false;
            this.dgPoints.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue;
            this.dgPoints.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgPoints.BackgroundColor = System.Drawing.Color.White;
            this.dgPoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPoints.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgPoints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column3,
            this.Column4,
            this.UseFlagDesc,
            this.Column7});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPoints.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPoints.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPoints.HighlightSelectedColumnHeaders = false;
            this.dgPoints.Location = new System.Drawing.Point(0, 22);
            this.dgPoints.Margin = new System.Windows.Forms.Padding(2);
            this.dgPoints.MultiSelect = false;
            this.dgPoints.Name = "dgPoints";
            this.dgPoints.ReadOnly = true;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPoints.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgPoints.RowHeadersWidth = 30;
            this.dgPoints.RowTemplate.Height = 23;
            this.dgPoints.SelectAllSignVisible = false;
            this.dgPoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPoints.SeqVisible = true;
            this.dgPoints.SetCustomStyle = true;
            this.dgPoints.Size = new System.Drawing.Size(792, 489);
            this.dgPoints.TabIndex = 5;
            this.dgPoints.CurrentCellChanged += new System.EventHandler(this.dgPoints_CurrentCellChanged);
            this.dgPoints.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgPoints_RowPrePaint);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "WorkName";
            this.dataGridViewTextBoxColumn1.HeaderText = "机构名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Cash";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column3.HeaderText = "现金";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Score";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column4.HeaderText = "兑换积分";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UseFlagDesc
            // 
            this.UseFlagDesc.DataPropertyName = "UseFlagDesc";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UseFlagDesc.DefaultCellStyle = dataGridViewCellStyle9;
            this.UseFlagDesc.HeaderText = "启用状态";
            this.UseFlagDesc.Name = "UseFlagDesc";
            this.UseFlagDesc.ReadOnly = true;
            this.UseFlagDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "OperateDate";
            this.Column7.HeaderText = "操作时间";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 150;
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btiNewConvertPoints,
            this.btnEditPoints,
            this.btiStop,
            this.btnClose});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.PaddingLeft = 22;
            this.bar2.Size = new System.Drawing.Size(792, 25);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 5;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btiNewConvertPoints
            // 
            this.btiNewConvertPoints.FixedSize = new System.Drawing.Size(90, 22);
            this.btiNewConvertPoints.Name = "btiNewConvertPoints";
            this.btiNewConvertPoints.Text = "新增积分设置";
            this.btiNewConvertPoints.Click += new System.EventHandler(this.btiNewConvertPoints_Click);
            // 
            // btnEditPoints
            // 
            this.btnEditPoints.FixedSize = new System.Drawing.Size(90, 22);
            this.btnEditPoints.Name = "btnEditPoints";
            this.btnEditPoints.Text = "编辑积分设置";
            this.btnEditPoints.Click += new System.EventHandler(this.btnEditPoints_Click);
            // 
            // btiStop
            // 
            this.btiStop.FixedSize = new System.Drawing.Size(40, 22);
            this.btiStop.Name = "btiStop";
            this.btiStop.Text = "停用";
            this.btiStop.Click += new System.EventHandler(this.btiStop_Click);
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭（&C）";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmConvertPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 536);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.expandableSplitter1);
            this.Controls.Add(this.panelEx2);
            this.Name = "FrmConvertPoints";
            this.Text = "积分兑换规则设置";
            this.OpenWindowBefore += new System.EventHandler(this.FrmConvertPoints_OpenWindowBefore);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCardTypeInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btiNew;
        private DevComponents.DotNetBar.ButtonItem btiEdit;
        private DevComponents.DotNetBar.ButtonItem btnStop;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btiNewConvertPoints;
        private DevComponents.DotNetBar.ButtonItem btnEditPoints;
        private DevComponents.DotNetBar.ButtonItem btiStop;
        private EfwControls.CustomControl.DataGrid dgCardTypeInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardTypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn flagdesc;
        private EfwControls.CustomControl.DataGrid dgPoints;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn UseFlagDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private DevComponents.DotNetBar.ButtonItem btnClose;
    }
}