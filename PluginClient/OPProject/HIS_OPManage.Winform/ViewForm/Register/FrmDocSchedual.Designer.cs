namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmDocSchedual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDocSchedual));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnCopySchedual = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtQueryDoc = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtQueryDept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtDate = new EfwControls.CustomControl.StatDateTime();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgSchedualData = new EfwControls.CustomControl.DataGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.schedualFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txtDocProfessor = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSchdualDept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.chkSchedual = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnDelSchedual = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveSchedual = new DevComponents.DotNetBar.ButtonX();
            this.btnAddSchedual = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cmbSchedualTime = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtSchedualDoc = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.dtpSchedualDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgScheDate = new EfwControls.CustomControl.DataGrid();
            this.SchedualDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WeekDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedualData)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpSchedualDate)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgScheDate)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnRefresh);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnCopySchedual);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.txtQueryDoc);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.txtQueryDept);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.dtDate);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1271, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(976, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 22);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "刷新(&F)";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1057, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopySchedual
            // 
            this.btnCopySchedual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCopySchedual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCopySchedual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCopySchedual.Image = ((System.Drawing.Image)(resources.GetObject("btnCopySchedual.Image")));
            this.btnCopySchedual.Location = new System.Drawing.Point(876, 8);
            this.btnCopySchedual.Name = "btnCopySchedual";
            this.btnCopySchedual.Size = new System.Drawing.Size(94, 22);
            this.btnCopySchedual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCopySchedual.TabIndex = 7;
            this.btnCopySchedual.Text = "复制排班(&T)";
            this.btnCopySchedual.Click += new System.EventHandler(this.btnCopySchedual_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(795, 8);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 6;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtQueryDoc
            // 
            // 
            // 
            // 
            this.txtQueryDoc.Border.Class = "TextBoxBorder";
            this.txtQueryDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtQueryDoc.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtQueryDoc.ButtonCustom.Image")));
            this.txtQueryDoc.ButtonCustom.Visible = true;
            this.txtQueryDoc.CardColumn = null;
            this.txtQueryDoc.DisplayField = "";
            this.txtQueryDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQueryDoc.IsEnterShowCard = true;
            this.txtQueryDoc.IsNumSelected = false;
            this.txtQueryDoc.IsPage = true;
            this.txtQueryDoc.IsShowLetter = false;
            this.txtQueryDoc.IsShowPage = false;
            this.txtQueryDoc.IsShowSeq = true;
            this.txtQueryDoc.Location = new System.Drawing.Point(651, 8);
            this.txtQueryDoc.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtQueryDoc.MemberField = "";
            this.txtQueryDoc.MemberValue = null;
            this.txtQueryDoc.Name = "txtQueryDoc";
            this.txtQueryDoc.QueryFields = new string[] {
        ""};
            this.txtQueryDoc.QueryFieldsString = "";
            this.txtQueryDoc.SelectedValue = null;
            this.txtQueryDoc.ShowCardColumns = null;
            this.txtQueryDoc.ShowCardDataSource = null;
            this.txtQueryDoc.ShowCardHeight = 0;
            this.txtQueryDoc.ShowCardWidth = 0;
            this.txtQueryDoc.Size = new System.Drawing.Size(123, 21);
            this.txtQueryDoc.TabIndex = 5;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(598, 8);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(47, 23);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "医 生";
            // 
            // txtQueryDept
            // 
            // 
            // 
            // 
            this.txtQueryDept.Border.Class = "TextBoxBorder";
            this.txtQueryDept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtQueryDept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtQueryDept.ButtonCustom.Image")));
            this.txtQueryDept.ButtonCustom.Visible = true;
            this.txtQueryDept.CardColumn = null;
            this.txtQueryDept.DisplayField = "";
            this.txtQueryDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtQueryDept.IsEnterShowCard = true;
            this.txtQueryDept.IsNumSelected = false;
            this.txtQueryDept.IsPage = true;
            this.txtQueryDept.IsShowLetter = false;
            this.txtQueryDept.IsShowPage = false;
            this.txtQueryDept.IsShowSeq = true;
            this.txtQueryDept.Location = new System.Drawing.Point(458, 8);
            this.txtQueryDept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtQueryDept.MemberField = "";
            this.txtQueryDept.MemberValue = null;
            this.txtQueryDept.Name = "txtQueryDept";
            this.txtQueryDept.QueryFields = new string[] {
        ""};
            this.txtQueryDept.QueryFieldsString = "";
            this.txtQueryDept.SelectedValue = null;
            this.txtQueryDept.ShowCardColumns = null;
            this.txtQueryDept.ShowCardDataSource = null;
            this.txtQueryDept.ShowCardHeight = 0;
            this.txtQueryDept.ShowCardWidth = 0;
            this.txtQueryDept.Size = new System.Drawing.Size(129, 21);
            this.txtQueryDept.TabIndex = 3;
            this.txtQueryDept.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtQueryDept_AfterSelectedRow);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(416, 8);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "科 室";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(6, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(54, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "排班日期";
            // 
            // dtDate
            // 
            this.dtDate.BackColor = System.Drawing.Color.Transparent;
            this.dtDate.DateFormat = "yyyy-MM-dd";
            this.dtDate.DateWidth = 120;
            this.dtDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtDate.Location = new System.Drawing.Point(66, 8);
            this.dtDate.Name = "dtDate";
            this.dtDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtDate.Size = new System.Drawing.Size(340, 21);
            this.dtDate.TabIndex = 0;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.Controls.Add(this.expandableSplitter1);
            this.panelEx2.Controls.Add(this.expandablePanel1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Location = new System.Drawing.Point(0, 40);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1271, 621);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 5;
            this.panelEx2.Text = "panelEx2";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgSchedualData);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(271, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1000, 498);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 8;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgSchedualData
            // 
            this.dgSchedualData.AllowSortWhenClickColumnHeader = true;
            this.dgSchedualData.AllowUserToAddRows = false;
            this.dgSchedualData.AllowUserToResizeColumns = false;
            this.dgSchedualData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgSchedualData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgSchedualData.BackgroundColor = System.Drawing.Color.White;
            this.dgSchedualData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSchedualData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgSchedualData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column2,
            this.Column3,
            this.schedualFlag,
            this.日期,
            this.Column1,
            this.Column6});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSchedualData.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgSchedualData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSchedualData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSchedualData.HighlightSelectedColumnHeaders = false;
            this.dgSchedualData.Location = new System.Drawing.Point(0, 0);
            this.dgSchedualData.Name = "dgSchedualData";
            this.dgSchedualData.ReadOnly = true;
            this.dgSchedualData.RowHeadersWidth = 30;
            this.dgSchedualData.RowTemplate.Height = 23;
            this.dgSchedualData.SelectAllSignVisible = false;
            this.dgSchedualData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSchedualData.SeqVisible = true;
            this.dgSchedualData.SetCustomStyle = true;
            this.dgSchedualData.ShowEditingIcon = false;
            this.dgSchedualData.Size = new System.Drawing.Size(1000, 498);
            this.dgSchedualData.TabIndex = 1;
            this.dgSchedualData.CurrentCellChanged += new System.EventHandler(this.dgSchedualData_CurrentCellChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "deptname";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn1.HeaderText = "科室";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "docname";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column2.HeaderText = "医生";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "TimeRangeName";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column3.HeaderText = "班次";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // schedualFlag
            // 
            this.schedualFlag.DataPropertyName = "schedualFlag";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.schedualFlag.DefaultCellStyle = dataGridViewCellStyle6;
            this.schedualFlag.HeaderText = "出诊状态";
            this.schedualFlag.Name = "schedualFlag";
            this.schedualFlag.ReadOnly = true;
            this.schedualFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.schedualFlag.Width = 80;
            // 
            // 日期
            // 
            this.日期.DataPropertyName = "SchedualDate";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.Format = "yyyy-MM-dd";
            this.日期.DefaultCellStyle = dataGridViewCellStyle7;
            this.日期.HeaderText = "日期";
            this.日期.Name = "日期";
            this.日期.ReadOnly = true;
            this.日期.Width = 170;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "WeekDay";
            this.Column1.HeaderText = "星期";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.DataPropertyName = "DocProfessionName";
            this.Column6.HeaderText = "医生职称";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.txtDocProfessor);
            this.panelEx3.Controls.Add(this.txtSchdualDept);
            this.panelEx3.Controls.Add(this.labelX8);
            this.panelEx3.Controls.Add(this.chkSchedual);
            this.panelEx3.Controls.Add(this.btnDelSchedual);
            this.panelEx3.Controls.Add(this.btnSaveSchedual);
            this.panelEx3.Controls.Add(this.btnAddSchedual);
            this.panelEx3.Controls.Add(this.labelX7);
            this.panelEx3.Controls.Add(this.cmbSchedualTime);
            this.panelEx3.Controls.Add(this.labelX6);
            this.panelEx3.Controls.Add(this.txtSchedualDoc);
            this.panelEx3.Controls.Add(this.labelX5);
            this.panelEx3.Controls.Add(this.labelX4);
            this.panelEx3.Controls.Add(this.dtpSchedualDate);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(271, 498);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1000, 123);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 6;
            // 
            // txtDocProfessor
            // 
            // 
            // 
            // 
            this.txtDocProfessor.Border.Class = "TextBoxBorder";
            this.txtDocProfessor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDocProfessor.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDocProfessor.Location = new System.Drawing.Point(299, 70);
            this.txtDocProfessor.Name = "txtDocProfessor";
            this.txtDocProfessor.ReadOnly = true;
            this.txtDocProfessor.Size = new System.Drawing.Size(128, 21);
            this.txtDocProfessor.TabIndex = 18;
            // 
            // txtSchdualDept
            // 
            // 
            // 
            // 
            this.txtSchdualDept.Border.Class = "TextBoxBorder";
            this.txtSchdualDept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSchdualDept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtSchdualDept.ButtonCustom.Image")));
            this.txtSchdualDept.ButtonCustom.Visible = true;
            this.txtSchdualDept.CardColumn = null;
            this.txtSchdualDept.DisplayField = "";
            this.txtSchdualDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSchdualDept.IsEnterShowCard = true;
            this.txtSchdualDept.IsNumSelected = false;
            this.txtSchdualDept.IsPage = true;
            this.txtSchdualDept.IsShowLetter = false;
            this.txtSchdualDept.IsShowPage = false;
            this.txtSchdualDept.IsShowSeq = true;
            this.txtSchdualDept.Location = new System.Drawing.Point(511, 31);
            this.txtSchdualDept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtSchdualDept.MemberField = "";
            this.txtSchdualDept.MemberValue = null;
            this.txtSchdualDept.Name = "txtSchdualDept";
            this.txtSchdualDept.QueryFields = new string[] {
        ""};
            this.txtSchdualDept.QueryFieldsString = "";
            this.txtSchdualDept.SelectedValue = null;
            this.txtSchdualDept.ShowCardColumns = null;
            this.txtSchdualDept.ShowCardDataSource = null;
            this.txtSchdualDept.ShowCardHeight = 0;
            this.txtSchdualDept.ShowCardWidth = 0;
            this.txtSchdualDept.Size = new System.Drawing.Size(139, 21);
            this.txtSchdualDept.TabIndex = 17;
            this.txtSchdualDept.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtSchdualDept_AfterSelectedRow);
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX8.Location = new System.Drawing.Point(6, 69);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(63, 23);
            this.labelX8.TabIndex = 16;
            this.labelX8.Text = "出诊医生";
            // 
            // chkSchedual
            // 
            // 
            // 
            // 
            this.chkSchedual.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkSchedual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkSchedual.Location = new System.Drawing.Point(446, 68);
            this.chkSchedual.Name = "chkSchedual";
            this.chkSchedual.Size = new System.Drawing.Size(61, 23);
            this.chkSchedual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkSchedual.TabIndex = 15;
            this.chkSchedual.Text = "出诊";
            // 
            // btnDelSchedual
            // 
            this.btnDelSchedual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelSchedual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelSchedual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelSchedual.Location = new System.Drawing.Point(837, 68);
            this.btnDelSchedual.Name = "btnDelSchedual";
            this.btnDelSchedual.Size = new System.Drawing.Size(75, 22);
            this.btnDelSchedual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelSchedual.TabIndex = 14;
            this.btnDelSchedual.Text = "删除(&D)";
            this.btnDelSchedual.Click += new System.EventHandler(this.btnDelSchedual_Click);
            // 
            // btnSaveSchedual
            // 
            this.btnSaveSchedual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveSchedual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveSchedual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveSchedual.Location = new System.Drawing.Point(748, 68);
            this.btnSaveSchedual.Name = "btnSaveSchedual";
            this.btnSaveSchedual.Size = new System.Drawing.Size(75, 22);
            this.btnSaveSchedual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveSchedual.TabIndex = 13;
            this.btnSaveSchedual.Text = "保存(&S)";
            this.btnSaveSchedual.Click += new System.EventHandler(this.btnSaveSchedual_Click);
            // 
            // btnAddSchedual
            // 
            this.btnAddSchedual.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddSchedual.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddSchedual.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddSchedual.Location = new System.Drawing.Point(656, 68);
            this.btnAddSchedual.Name = "btnAddSchedual";
            this.btnAddSchedual.Size = new System.Drawing.Size(75, 22);
            this.btnAddSchedual.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddSchedual.TabIndex = 11;
            this.btnAddSchedual.Text = "新增(&N)";
            this.btnAddSchedual.Click += new System.EventHandler(this.btnAddSchedual_Click);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX7.Location = new System.Drawing.Point(446, 29);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(63, 23);
            this.labelX7.TabIndex = 9;
            this.labelX7.Text = "出诊科室";
            // 
            // cmbSchedualTime
            // 
            this.cmbSchedualTime.DisplayMember = "Text";
            this.cmbSchedualTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSchedualTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSchedualTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSchedualTime.FormattingEnabled = true;
            this.cmbSchedualTime.ItemHeight = 15;
            this.cmbSchedualTime.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cmbSchedualTime.Location = new System.Drawing.Point(299, 31);
            this.cmbSchedualTime.Name = "cmbSchedualTime";
            this.cmbSchedualTime.Size = new System.Drawing.Size(128, 21);
            this.cmbSchedualTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSchedualTime.TabIndex = 8;
            this.cmbSchedualTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSchedualTime_KeyDown);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "上午";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "下午";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "晚上";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX6.Location = new System.Drawing.Point(230, 29);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(63, 23);
            this.labelX6.TabIndex = 7;
            this.labelX6.Text = "出诊班次";
            // 
            // txtSchedualDoc
            // 
            // 
            // 
            // 
            this.txtSchedualDoc.Border.Class = "TextBoxBorder";
            this.txtSchedualDoc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSchedualDoc.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtSchedualDoc.ButtonCustom.Image")));
            this.txtSchedualDoc.ButtonCustom.Visible = true;
            this.txtSchedualDoc.CardColumn = null;
            this.txtSchedualDoc.DisplayField = "";
            this.txtSchedualDoc.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSchedualDoc.IsEnterShowCard = true;
            this.txtSchedualDoc.IsNumSelected = false;
            this.txtSchedualDoc.IsPage = true;
            this.txtSchedualDoc.IsShowLetter = false;
            this.txtSchedualDoc.IsShowPage = false;
            this.txtSchedualDoc.IsShowSeq = true;
            this.txtSchedualDoc.Location = new System.Drawing.Point(75, 70);
            this.txtSchedualDoc.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtSchedualDoc.MemberField = "";
            this.txtSchedualDoc.MemberValue = null;
            this.txtSchedualDoc.Name = "txtSchedualDoc";
            this.txtSchedualDoc.QueryFields = new string[] {
        ""};
            this.txtSchedualDoc.QueryFieldsString = "";
            this.txtSchedualDoc.SelectedValue = null;
            this.txtSchedualDoc.ShowCardColumns = null;
            this.txtSchedualDoc.ShowCardDataSource = null;
            this.txtSchedualDoc.ShowCardHeight = 0;
            this.txtSchedualDoc.ShowCardWidth = 0;
            this.txtSchedualDoc.Size = new System.Drawing.Size(139, 21);
            this.txtSchedualDoc.TabIndex = 6;
            this.txtSchedualDoc.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtSchedualDoc_AfterSelectedRow);
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX5.Location = new System.Drawing.Point(230, 69);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(63, 23);
            this.labelX5.TabIndex = 3;
            this.labelX5.Text = "医生职称";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX4.Location = new System.Drawing.Point(6, 29);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(63, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "出诊日期";
            // 
            // dtpSchedualDate
            // 
            // 
            // 
            // 
            this.dtpSchedualDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpSchedualDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpSchedualDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpSchedualDate.ButtonDropDown.Visible = true;
            this.dtpSchedualDate.CustomFormat = "yyyy-MM-dd";
            this.dtpSchedualDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpSchedualDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpSchedualDate.IsPopupCalendarOpen = false;
            this.dtpSchedualDate.Location = new System.Drawing.Point(75, 31);
            // 
            // 
            // 
            this.dtpSchedualDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpSchedualDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpSchedualDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpSchedualDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpSchedualDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpSchedualDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpSchedualDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtpSchedualDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpSchedualDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpSchedualDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpSchedualDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpSchedualDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpSchedualDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpSchedualDate.MonthCalendar.TodayButtonVisible = true;
            this.dtpSchedualDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpSchedualDate.Name = "dtpSchedualDate";
            this.dtpSchedualDate.Size = new System.Drawing.Size(139, 21);
            this.dtpSchedualDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpSchedualDate.TabIndex = 0;
            this.dtpSchedualDate.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dtpSchedualDate_PreviewKeyDown);
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
            this.expandableSplitter1.Location = new System.Drawing.Point(265, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 621);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 7;
            this.expandableSplitter1.TabStop = false;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.dgScheDate);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(265, 621);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "排班日期";
            // 
            // dgScheDate
            // 
            this.dgScheDate.AllowSortWhenClickColumnHeader = false;
            this.dgScheDate.AllowUserToAddRows = false;
            this.dgScheDate.AllowUserToResizeColumns = false;
            this.dgScheDate.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.AliceBlue;
            this.dgScheDate.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgScheDate.BackgroundColor = System.Drawing.Color.White;
            this.dgScheDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgScheDate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgScheDate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SchedualDate,
            this.WeekDay});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgScheDate.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgScheDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgScheDate.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgScheDate.HighlightSelectedColumnHeaders = false;
            this.dgScheDate.Location = new System.Drawing.Point(0, 26);
            this.dgScheDate.Name = "dgScheDate";
            this.dgScheDate.ReadOnly = true;
            this.dgScheDate.RowHeadersWidth = 30;
            this.dgScheDate.RowTemplate.Height = 23;
            this.dgScheDate.SelectAllSignVisible = false;
            this.dgScheDate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgScheDate.SeqVisible = true;
            this.dgScheDate.SetCustomStyle = true;
            this.dgScheDate.Size = new System.Drawing.Size(265, 595);
            this.dgScheDate.TabIndex = 1;
            this.dgScheDate.Click += new System.EventHandler(this.dgScheDate_Click);
            // 
            // SchedualDate
            // 
            this.SchedualDate.DataPropertyName = "SchedualDate";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.Format = "yyyy-MM-dd";
            this.SchedualDate.DefaultCellStyle = dataGridViewCellStyle11;
            this.SchedualDate.HeaderText = "排班日期";
            this.SchedualDate.Name = "SchedualDate";
            this.SchedualDate.ReadOnly = true;
            this.SchedualDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SchedualDate.Width = 120;
            // 
            // WeekDay
            // 
            this.WeekDay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WeekDay.DataPropertyName = "WeekDay";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WeekDay.DefaultCellStyle = dataGridViewCellStyle12;
            this.WeekDay.HeaderText = "星期";
            this.WeekDay.Name = "WeekDay";
            this.WeekDay.ReadOnly = true;
            this.WeekDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmDocSchedual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 661);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmDocSchedual";
            this.Text = "医生排班";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDocSchedual_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSchedualData)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpSchedualDate)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgScheDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.StatDateTime dtDate;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private EfwControls.CustomControl.TextBoxCard txtQueryDoc;
        private DevComponents.DotNetBar.LabelX labelX3;
        private EfwControls.CustomControl.TextBoxCard txtQueryDept;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private EfwControls.CustomControl.DataGrid dgSchedualData;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private EfwControls.CustomControl.DataGrid dgScheDate;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.TextBoxCard txtSchedualDoc;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpSchedualDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSchedual;
        private DevComponents.DotNetBar.ButtonX btnDelSchedual;
        private DevComponents.DotNetBar.ButtonX btnSaveSchedual;
        private DevComponents.DotNetBar.ButtonX btnAddSchedual;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSchedualTime;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnCopySchedual;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private EfwControls.CustomControl.TextBoxCard txtSchdualDept;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchedualDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WeekDay;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDocProfessor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn schedualFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn 日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}