namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmWardMaintenance
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWardMaintenance));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.grdBedFreeList = new EfwControls.CustomControl.DataGrid();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expandablePanel3 = new DevComponents.DotNetBar.ExpandablePanel();
            this.grdBedList = new EfwControls.CustomControl.DataGrid();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txtInpatientAreaList = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.lblInpatientAreaList = new DevComponents.DotNetBar.LabelX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddHospitalBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdHospitalBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopHospitalBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnBatchAddBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnExit = new DevComponents.DotNetBar.ButtonItem();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.expandablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBedFreeList)).BeginInit();
            this.expandablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBedList)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel2);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.expandablePanel3);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.bar1);
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
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.grdBedFreeList);
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel2.ExpandButtonVisible = false;
            this.expandablePanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(786, 55);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(478, 675);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 13;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "费用列表";
            // 
            // grdBedFreeList
            // 
            this.grdBedFreeList.AllowSortWhenClickColumnHeader = false;
            this.grdBedFreeList.AllowUserToAddRows = false;
            this.grdBedFreeList.AllowUserToDeleteRows = false;
            this.grdBedFreeList.AllowUserToResizeColumns = false;
            this.grdBedFreeList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.grdBedFreeList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdBedFreeList.BackgroundColor = System.Drawing.Color.White;
            this.grdBedFreeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdBedFreeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdBedFreeList.ColumnHeadersHeight = 27;
            this.grdBedFreeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column1,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdBedFreeList.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdBedFreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBedFreeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdBedFreeList.HighlightSelectedColumnHeaders = false;
            this.grdBedFreeList.Location = new System.Drawing.Point(0, 26);
            this.grdBedFreeList.Margin = new System.Windows.Forms.Padding(2);
            this.grdBedFreeList.Name = "grdBedFreeList";
            this.grdBedFreeList.ReadOnly = true;
            this.grdBedFreeList.RowHeadersWidth = 30;
            this.grdBedFreeList.RowTemplate.Height = 23;
            this.grdBedFreeList.SelectAllSignVisible = false;
            this.grdBedFreeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBedFreeList.SeqVisible = true;
            this.grdBedFreeList.SetCustomStyle = true;
            this.grdBedFreeList.Size = new System.Drawing.Size(478, 649);
            this.grdBedFreeList.TabIndex = 1;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(782, 55);
            this.expandableSplitter1.Margin = new System.Windows.Forms.Padding(2);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(4, 675);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 14;
            this.expandableSplitter1.TabStop = false;
            // 
            // expandablePanel3
            // 
            this.expandablePanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel3.Controls.Add(this.grdBedList);
            this.expandablePanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanel3.ExpandButtonVisible = false;
            this.expandablePanel3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel3.HideControlsWhenCollapsed = true;
            this.expandablePanel3.Location = new System.Drawing.Point(0, 55);
            this.expandablePanel3.Name = "expandablePanel3";
            this.expandablePanel3.Size = new System.Drawing.Size(782, 675);
            this.expandablePanel3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel3.Style.GradientAngle = 90;
            this.expandablePanel3.TabIndex = 12;
            this.expandablePanel3.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel3.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel3.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel3.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel3.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel3.TitleStyle.GradientAngle = 90;
            this.expandablePanel3.TitleText = " 病床列表";
            // 
            // grdBedList
            // 
            this.grdBedList.AllowSortWhenClickColumnHeader = false;
            this.grdBedList.AllowUserToAddRows = false;
            this.grdBedList.AllowUserToDeleteRows = false;
            this.grdBedList.AllowUserToResizeColumns = false;
            this.grdBedList.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.AliceBlue;
            this.grdBedList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.grdBedList.BackgroundColor = System.Drawing.Color.White;
            this.grdBedList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdBedList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdBedList.ColumnHeadersHeight = 27;
            this.grdBedList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column8,
            this.Column5,
            this.Column6,
            this.Column4,
            this.Column7});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdBedList.DefaultCellStyle = dataGridViewCellStyle12;
            this.grdBedList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBedList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdBedList.HighlightSelectedColumnHeaders = false;
            this.grdBedList.Location = new System.Drawing.Point(0, 26);
            this.grdBedList.Margin = new System.Windows.Forms.Padding(2);
            this.grdBedList.Name = "grdBedList";
            this.grdBedList.ReadOnly = true;
            this.grdBedList.RowHeadersWidth = 30;
            this.grdBedList.RowTemplate.Height = 23;
            this.grdBedList.SelectAllSignVisible = false;
            this.grdBedList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBedList.SeqVisible = true;
            this.grdBedList.SetCustomStyle = true;
            this.grdBedList.Size = new System.Drawing.Size(782, 649);
            this.grdBedList.TabIndex = 1;
            this.grdBedList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBedList_CellClick);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.txtInpatientAreaList);
            this.panelEx3.Controls.Add(this.lblInpatientAreaList);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 25);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1264, 30);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 8;
            // 
            // txtInpatientAreaList
            // 
            // 
            // 
            // 
            this.txtInpatientAreaList.Border.Class = "TextBoxBorder";
            this.txtInpatientAreaList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInpatientAreaList.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtInpatientAreaList.ButtonCustom.Image")));
            this.txtInpatientAreaList.ButtonCustom.Visible = true;
            this.txtInpatientAreaList.CardColumn = null;
            this.txtInpatientAreaList.DisplayField = "";
            this.txtInpatientAreaList.IsEnterShowCard = false;
            this.txtInpatientAreaList.IsNumSelected = false;
            this.txtInpatientAreaList.IsPage = true;
            this.txtInpatientAreaList.IsShowLetter = false;
            this.txtInpatientAreaList.IsShowPage = false;
            this.txtInpatientAreaList.IsShowSeq = true;
            this.txtInpatientAreaList.Location = new System.Drawing.Point(84, 5);
            this.txtInpatientAreaList.Margin = new System.Windows.Forms.Padding(2);
            this.txtInpatientAreaList.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtInpatientAreaList.MemberField = "";
            this.txtInpatientAreaList.MemberValue = null;
            this.txtInpatientAreaList.Name = "txtInpatientAreaList";
            this.txtInpatientAreaList.QueryFields = new string[] {
        ""};
            this.txtInpatientAreaList.QueryFieldsString = "";
            this.txtInpatientAreaList.SelectedValue = null;
            this.txtInpatientAreaList.ShowCardColumns = null;
            this.txtInpatientAreaList.ShowCardDataSource = null;
            this.txtInpatientAreaList.ShowCardHeight = 0;
            this.txtInpatientAreaList.ShowCardWidth = 0;
            this.txtInpatientAreaList.Size = new System.Drawing.Size(167, 21);
            this.txtInpatientAreaList.TabIndex = 1;
            this.txtInpatientAreaList.TabStop = false;
            this.txtInpatientAreaList.TextChanged += new System.EventHandler(this.txtInpatientAreaList_TextChanged);
            // 
            // lblInpatientAreaList
            // 
            // 
            // 
            // 
            this.lblInpatientAreaList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblInpatientAreaList.Location = new System.Drawing.Point(22, 4);
            this.lblInpatientAreaList.Name = "lblInpatientAreaList";
            this.lblInpatientAreaList.Size = new System.Drawing.Size(57, 22);
            this.lblInpatientAreaList.TabIndex = 7;
            this.lblInpatientAreaList.Text = "病区列表";
            this.lblInpatientAreaList.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddHospitalBed,
            this.btnUpdHospitalBed,
            this.btnStopHospitalBed,
            this.btnBatchAddBed,
            this.btnExit});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1264, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnAddHospitalBed
            // 
            this.btnAddHospitalBed.Enabled = false;
            this.btnAddHospitalBed.Name = "btnAddHospitalBed";
            this.btnAddHospitalBed.Text = "新增病床(&F6)";
            this.btnAddHospitalBed.Click += new System.EventHandler(this.btnAddHospitalBed_Click);
            // 
            // btnUpdHospitalBed
            // 
            this.btnUpdHospitalBed.Enabled = false;
            this.btnUpdHospitalBed.Name = "btnUpdHospitalBed";
            this.btnUpdHospitalBed.Text = "编辑病床(&F7)";
            this.btnUpdHospitalBed.Click += new System.EventHandler(this.btnUpdHospitalBed_Click);
            // 
            // btnStopHospitalBed
            // 
            this.btnStopHospitalBed.Enabled = false;
            this.btnStopHospitalBed.Name = "btnStopHospitalBed";
            this.btnStopHospitalBed.Text = "停用病床(&F8)";
            this.btnStopHospitalBed.Click += new System.EventHandler(this.btnStopHospitalBed_Click);
            // 
            // btnBatchAddBed
            // 
            this.btnBatchAddBed.Enabled = false;
            this.btnBatchAddBed.Name = "btnBatchAddBed";
            this.btnBatchAddBed.Text = "批量加床(&F9)";
            this.btnBatchAddBed.Click += new System.EventHandler(this.btnBatchAddBed_Click);
            // 
            // btnExit
            // 
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "关闭(&C)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "FeeTypeName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column9.HeaderText = "费用类型";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 80;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.DataPropertyName = "ItemName";
            this.Column1.HeaderText = "费用明细";
            this.Column1.MinimumWidth = 205;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "ItemAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column10.HeaderText = "数量";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N4";
            dataGridViewCellStyle5.NullValue = null;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column11.HeaderText = "单价";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 80;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "RoomNO";
            this.Column2.HeaderText = "房间号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "BedNO";
            this.Column3.HeaderText = "床位号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "IsUsedName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column8.HeaderText = "在用";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 50;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "IsStopedName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column5.HeaderText = "停用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 50;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "IsPlusName";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column6.HeaderText = "加床";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 52;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.DataPropertyName = "DoctorName";
            this.Column4.HeaderText = "责任医生";
            this.Column4.MinimumWidth = 218;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "NurseName";
            this.Column7.HeaderText = "责任护士";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 218;
            // 
            // FrmWardMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWardMaintenance";
            this.Text = "病区维护";
            this.OpenWindowBefore += new System.EventHandler(this.FrmWardMaintenance_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmWardMaintenance_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBedFreeList)).EndInit();
            this.expandablePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBedList)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddHospitalBed;
        private DevComponents.DotNetBar.ButtonItem btnUpdHospitalBed;
        private DevComponents.DotNetBar.ButtonItem btnStopHospitalBed;
        private DevComponents.DotNetBar.ButtonItem btnExit;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX lblInpatientAreaList;
        private EfwControls.CustomControl.TextBoxCard txtInpatientAreaList;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel3;
        private EfwControls.CustomControl.DataGrid grdBedList;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private EfwControls.CustomControl.DataGrid grdBedFreeList;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.ButtonItem btnBatchAddBed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}