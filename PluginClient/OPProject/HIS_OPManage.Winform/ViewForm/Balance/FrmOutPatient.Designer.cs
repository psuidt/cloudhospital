namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmOutPatient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutPatient));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtIDNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtTel = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPatName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgPatInfo = new EfwControls.CustomControl.DataGrid();
            this.VisitNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patSex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.curDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cureEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtTime = new EfwControls.CustomControl.StatDateTime();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtMediCard = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(936, 451);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(812, 423);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "取 消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(704, 423);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 21;
            this.btnOK.Text = "选 择";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dgPatInfo);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 111);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(936, 300);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "panelEx2";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtMediCard);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.btnQuery);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.dtTime);
            this.groupPanel1.Controls.Add(this.txtIDNumber);
            this.groupPanel1.Controls.Add(this.txtTel);
            this.groupPanel1.Controls.Add(this.txtPatName);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.DrawTitleBox = false;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(936, 111);
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
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
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
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "检索条件";
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 11F);
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(808, 41);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(100, 28);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 22;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 47);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(81, 29);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "就诊日期";
            // 
            // txtIDNumber
            // 
            // 
            // 
            // 
            this.txtIDNumber.Border.Class = "TextBoxBorder";
            this.txtIDNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtIDNumber.Location = new System.Drawing.Point(477, 9);
            this.txtIDNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtIDNumber.Name = "txtIDNumber";
            this.txtIDNumber.Size = new System.Drawing.Size(187, 24);
            this.txtIDNumber.TabIndex = 5;
            // 
            // txtTel
            // 
            // 
            // 
            // 
            this.txtTel.Border.Class = "TextBoxBorder";
            this.txtTel.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTel.Location = new System.Drawing.Point(249, 9);
            this.txtTel.Margin = new System.Windows.Forms.Padding(4);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(142, 24);
            this.txtTel.TabIndex = 3;
            // 
            // txtPatName
            // 
            // 
            // 
            // 
            this.txtPatName.Border.Class = "TextBoxBorder";
            this.txtPatName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPatName.Location = new System.Drawing.Point(62, 9);
            this.txtPatName.Margin = new System.Windows.Forms.Padding(4);
            this.txtPatName.Name = "txtPatName";
            this.txtPatName.Size = new System.Drawing.Size(97, 24);
            this.txtPatName.TabIndex = 1;
            // 
            // dgPatInfo
            // 
            this.dgPatInfo.AllowSortWhenClickColumnHeader = false;
            this.dgPatInfo.AllowUserToAddRows = false;
            this.dgPatInfo.AllowUserToResizeColumns = false;
            this.dgPatInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgPatInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPatInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgPatInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgPatInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VisitNO,
            this.PatName,
            this.patSex,
            this.BirthDay,
            this.Column5,
            this.curDeptName,
            this.cureEmpName,
            this.RegDate});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatInfo.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgPatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPatInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPatInfo.HighlightSelectedColumnHeaders = false;
            this.dgPatInfo.Location = new System.Drawing.Point(0, 0);
            this.dgPatInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgPatInfo.Name = "dgPatInfo";
            this.dgPatInfo.ReadOnly = true;
            this.dgPatInfo.RowHeadersWidth = 30;
            this.dgPatInfo.RowTemplate.Height = 23;
            this.dgPatInfo.SelectAllSignVisible = false;
            this.dgPatInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatInfo.SeqVisible = true;
            this.dgPatInfo.SetCustomStyle = true;
            this.dgPatInfo.Size = new System.Drawing.Size(936, 300);
            this.dgPatInfo.TabIndex = 0;
            this.dgPatInfo.DoubleClick += new System.EventHandler(this.dgPatInfo_DoubleClick);
            // 
            // VisitNO
            // 
            this.VisitNO.DataPropertyName = "VisitNO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11F);
            this.VisitNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.VisitNO.HeaderText = "就诊号";
            this.VisitNO.Name = "VisitNO";
            this.VisitNO.ReadOnly = true;
            this.VisitNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.VisitNO.Width = 120;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11F);
            this.PatName.DefaultCellStyle = dataGridViewCellStyle4;
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 120;
            // 
            // patSex
            // 
            this.patSex.DataPropertyName = "patSex";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 11F);
            this.patSex.DefaultCellStyle = dataGridViewCellStyle5;
            this.patSex.HeaderText = "性别";
            this.patSex.Name = "patSex";
            this.patSex.ReadOnly = true;
            this.patSex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patSex.Width = 80;
            // 
            // BirthDay
            // 
            this.BirthDay.DataPropertyName = "BirthDay";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle6.Format = "yyyy-MM-dd HH:mm:ss";
            this.BirthDay.DefaultCellStyle = dataGridViewCellStyle6;
            this.BirthDay.HeaderText = "出生日期";
            this.BirthDay.Name = "BirthDay";
            this.BirthDay.ReadOnly = true;
            this.BirthDay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BirthDay.Width = 180;
            // 
            // Column5
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 11F);
            this.Column5.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column5.HeaderText = "电话号码";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column5.Width = 160;
            // 
            // curDeptName
            // 
            this.curDeptName.DataPropertyName = "regDeptName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 11F);
            this.curDeptName.DefaultCellStyle = dataGridViewCellStyle8;
            this.curDeptName.HeaderText = "就诊科室";
            this.curDeptName.Name = "curDeptName";
            this.curDeptName.ReadOnly = true;
            this.curDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.curDeptName.Width = 120;
            // 
            // cureEmpName
            // 
            this.cureEmpName.DataPropertyName = "regEmpName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 11F);
            this.cureEmpName.DefaultCellStyle = dataGridViewCellStyle9;
            this.cureEmpName.HeaderText = "就诊医生";
            this.cureEmpName.Name = "cureEmpName";
            this.cureEmpName.ReadOnly = true;
            this.cureEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cureEmpName.Width = 120;
            // 
            // RegDate
            // 
            this.RegDate.DataPropertyName = "RegDate";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 11F);
            this.RegDate.DefaultCellStyle = dataGridViewCellStyle10;
            this.RegDate.HeaderText = "就诊日期";
            this.RegDate.Name = "RegDate";
            this.RegDate.ReadOnly = true;
            this.RegDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RegDate.Width = 243;
            // 
            // dtTime
            // 
            this.dtTime.BackColor = System.Drawing.Color.Transparent;
            this.dtTime.DateFormat = "yyyy-MM-dd";
            this.dtTime.DateWidth = 120;
            this.dtTime.Font = new System.Drawing.Font("宋体", 11F);
            this.dtTime.Location = new System.Drawing.Point(101, 45);
            this.dtTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtTime.Name = "dtTime";
            this.dtTime.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtTime.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtTime.Size = new System.Drawing.Size(476, 24);
            this.dtTime.TabIndex = 6;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(17, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(38, 23);
            this.labelX2.TabIndex = 24;
            this.labelX2.Text = "姓名";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(170, 9);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(72, 23);
            this.labelX3.TabIndex = 25;
            this.labelX3.Text = "电话号码";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(398, 9);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(72, 23);
            this.labelX4.TabIndex = 26;
            this.labelX4.Text = "身份证号";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(671, 9);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(72, 23);
            this.labelX5.TabIndex = 27;
            this.labelX5.Text = "医保卡号";
            // 
            // txtMediCard
            // 
            // 
            // 
            // 
            this.txtMediCard.Border.Class = "TextBoxBorder";
            this.txtMediCard.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMediCard.Location = new System.Drawing.Point(737, 9);
            this.txtMediCard.Margin = new System.Windows.Forms.Padding(4);
            this.txtMediCard.Name = "txtMediCard";
            this.txtMediCard.Size = new System.Drawing.Size(175, 24);
            this.txtMediCard.TabIndex = 28;
            // 
            // FrmOutPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 451);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOutPatient";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人信息查询";
            this.Load += new System.EventHandler(this.FrmOutPatient_Load);
            this.Shown += new System.EventHandler(this.FrmOutPatient_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmOutPatient_KeyUp);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPatInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPatName;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.StatDateTime dtTime;
        private DevComponents.DotNetBar.Controls.TextBoxX txtIDNumber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTel;
        private EfwControls.CustomControl.DataGrid dgPatInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patSex;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn curDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cureEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegDate;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMediCard;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}