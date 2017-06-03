namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmAdmissionRegistration
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.grdPatList = new EfwControls.CustomControl.DataGrid();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CaseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DoctorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnterHDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnterDiseaseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnNew = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdate = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.cboPatType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboDeptList = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.chkLeaveHospital = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblPatType = new DevComponents.DotNetBar.LabelX();
            this.chkDefinedDischarge = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkMakerDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtSearchParm = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkBeInBed = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblDept = new DevComponents.DotNetBar.LabelX();
            this.sdtpMakerDate = new EfwControls.CustomControl.StatDateTime();
            this.lblSearchParm = new DevComponents.DotNetBar.LabelX();
            this.chkNewHospital = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.frmAdmission = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.grdPatList);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 730);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.Sunken;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // grdPatList
            // 
            this.grdPatList.AllowSortWhenClickColumnHeader = false;
            this.grdPatList.AllowUserToAddRows = false;
            this.grdPatList.AllowUserToDeleteRows = false;
            this.grdPatList.AllowUserToResizeColumns = false;
            this.grdPatList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grdPatList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPatList.BackgroundColor = System.Drawing.Color.White;
            this.grdPatList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdPatList.ColumnHeadersHeight = 30;
            this.grdPatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNumber,
            this.CaseNumber,
            this.PatName,
            this.Sex,
            this.DeptName,
            this.DoctorName,
            this.EnterHDate,
            this.BedNo,
            this.EnterDiseaseName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPatList.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPatList.HighlightSelectedColumnHeaders = false;
            this.grdPatList.Location = new System.Drawing.Point(0, 107);
            this.grdPatList.Margin = new System.Windows.Forms.Padding(4);
            this.grdPatList.Name = "grdPatList";
            this.grdPatList.ReadOnly = true;
            this.grdPatList.RowHeadersWidth = 25;
            this.grdPatList.RowTemplate.Height = 23;
            this.grdPatList.SelectAllSignVisible = false;
            this.grdPatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatList.SeqVisible = true;
            this.grdPatList.SetCustomStyle = false;
            this.grdPatList.Size = new System.Drawing.Size(1264, 623);
            this.grdPatList.TabIndex = 8;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SerialNumber.DefaultCellStyle = dataGridViewCellStyle3;
            this.SerialNumber.HeaderText = "住院号";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SerialNumber.Width = 120;
            // 
            // CaseNumber
            // 
            this.CaseNumber.DataPropertyName = "CaseNumber";
            this.CaseNumber.HeaderText = "病案号";
            this.CaseNumber.Name = "CaseNumber";
            this.CaseNumber.ReadOnly = true;
            this.CaseNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CaseNumber.Width = 150;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 130;
            // 
            // Sex
            // 
            this.Sex.DataPropertyName = "Sex";
            this.Sex.HeaderText = "性别";
            this.Sex.Name = "Sex";
            this.Sex.ReadOnly = true;
            this.Sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sex.Width = 50;
            // 
            // DeptName
            // 
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.HeaderText = "科室";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptName.Width = 150;
            // 
            // DoctorName
            // 
            this.DoctorName.DataPropertyName = "DoctorName";
            this.DoctorName.HeaderText = "医生";
            this.DoctorName.Name = "DoctorName";
            this.DoctorName.ReadOnly = true;
            this.DoctorName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DoctorName.Width = 70;
            // 
            // EnterHDate
            // 
            this.EnterHDate.DataPropertyName = "EnterHDate";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "g";
            dataGridViewCellStyle4.NullValue = null;
            this.EnterHDate.DefaultCellStyle = dataGridViewCellStyle4;
            this.EnterHDate.HeaderText = "入院时间";
            this.EnterHDate.Name = "EnterHDate";
            this.EnterHDate.ReadOnly = true;
            this.EnterHDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EnterHDate.Width = 170;
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.HeaderText = "床位号";
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BedNo.Width = 60;
            // 
            // EnterDiseaseName
            // 
            this.EnterDiseaseName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EnterDiseaseName.DataPropertyName = "EnterDiseaseName";
            this.EnterDiseaseName.HeaderText = "入院诊断";
            this.EnterDiseaseName.Name = "EnterDiseaseName";
            this.EnterDiseaseName.ReadOnly = true;
            this.EnterDiseaseName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNew,
            this.btnCancel,
            this.btnUpdate,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(0, 79);
            this.bar1.Margin = new System.Windows.Forms.Padding(4);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1264, 28);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 7;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.FontBold = true;
            this.btnNew.Name = "btnNew";
            this.btnNew.Text = "新证(&F6)";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.FontBold = true;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "取消入院(&F7)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Enabled = false;
            this.btnUpdate.FontBold = true;
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Text = "修改(&F8)";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.FontBold = true;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.cboPatType);
            this.panelEx3.Controls.Add(this.cboDeptList);
            this.panelEx3.Controls.Add(this.btnSearch);
            this.panelEx3.Controls.Add(this.chkLeaveHospital);
            this.panelEx3.Controls.Add(this.lblPatType);
            this.panelEx3.Controls.Add(this.chkDefinedDischarge);
            this.panelEx3.Controls.Add(this.chkMakerDate);
            this.panelEx3.Controls.Add(this.txtSearchParm);
            this.panelEx3.Controls.Add(this.chkBeInBed);
            this.panelEx3.Controls.Add(this.lblDept);
            this.panelEx3.Controls.Add(this.sdtpMakerDate);
            this.panelEx3.Controls.Add(this.lblSearchParm);
            this.panelEx3.Controls.Add(this.chkNewHospital);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1264, 79);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // cboPatType
            // 
            this.cboPatType.DisplayMember = "Text";
            this.cboPatType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPatType.FormattingEnabled = true;
            this.cboPatType.ItemHeight = 19;
            this.cboPatType.Location = new System.Drawing.Point(977, 12);
            this.cboPatType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboPatType.Name = "cboPatType";
            this.cboPatType.Size = new System.Drawing.Size(247, 25);
            this.cboPatType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPatType.TabIndex = 4;
            // 
            // cboDeptList
            // 
            this.cboDeptList.DisplayMember = "Text";
            this.cboDeptList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeptList.FormattingEnabled = true;
            this.cboDeptList.ItemHeight = 19;
            this.cboDeptList.Location = new System.Drawing.Point(620, 12);
            this.cboDeptList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboDeptList.Name = "cboDeptList";
            this.cboDeptList.Size = new System.Drawing.Size(247, 25);
            this.cboDeptList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeptList.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(997, 44);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkLeaveHospital
            // 
            // 
            // 
            // 
            this.chkLeaveHospital.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkLeaveHospital.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkLeaveHospital.Location = new System.Drawing.Point(904, 48);
            this.chkLeaveHospital.Margin = new System.Windows.Forms.Padding(4);
            this.chkLeaveHospital.Name = "chkLeaveHospital";
            this.chkLeaveHospital.Size = new System.Drawing.Size(64, 24);
            this.chkLeaveHospital.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkLeaveHospital.TabIndex = 9;
            this.chkLeaveHospital.Text = "出院";
            // 
            // lblPatType
            // 
            // 
            // 
            // 
            this.lblPatType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatType.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatType.Location = new System.Drawing.Point(897, 12);
            this.lblPatType.Margin = new System.Windows.Forms.Padding(4);
            this.lblPatType.Name = "lblPatType";
            this.lblPatType.Size = new System.Drawing.Size(73, 24);
            this.lblPatType.TabIndex = 0;
            this.lblPatType.Text = "病人类型";
            this.lblPatType.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // chkDefinedDischarge
            // 
            // 
            // 
            // 
            this.chkDefinedDischarge.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkDefinedDischarge.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkDefinedDischarge.Location = new System.Drawing.Point(777, 48);
            this.chkDefinedDischarge.Margin = new System.Windows.Forms.Padding(4);
            this.chkDefinedDischarge.Name = "chkDefinedDischarge";
            this.chkDefinedDischarge.Size = new System.Drawing.Size(97, 24);
            this.chkDefinedDischarge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkDefinedDischarge.TabIndex = 8;
            this.chkDefinedDischarge.Text = "定义出院";
            // 
            // chkMakerDate
            // 
            // 
            // 
            // 
            this.chkMakerDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkMakerDate.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkMakerDate.Location = new System.Drawing.Point(37, 12);
            this.chkMakerDate.Margin = new System.Windows.Forms.Padding(4);
            this.chkMakerDate.Name = "chkMakerDate";
            this.chkMakerDate.Size = new System.Drawing.Size(69, 24);
            this.chkMakerDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkMakerDate.TabIndex = 1;
            this.chkMakerDate.Text = "时间";
            this.chkMakerDate.CheckedChanged += new System.EventHandler(this.chkTime_CheckedChanged);
            // 
            // txtSearchParm
            // 
            // 
            // 
            // 
            this.txtSearchParm.Border.Class = "TextBoxBorder";
            this.txtSearchParm.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearchParm.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearchParm.Location = new System.Drawing.Point(113, 44);
            this.txtSearchParm.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchParm.Name = "txtSearchParm";
            this.txtSearchParm.Size = new System.Drawing.Size(431, 25);
            this.txtSearchParm.TabIndex = 5;
            this.txtSearchParm.WatermarkText = "住院号、病案号、床位号、姓名";
            // 
            // chkBeInBed
            // 
            // 
            // 
            // 
            this.chkBeInBed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkBeInBed.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkBeInBed.Location = new System.Drawing.Point(684, 48);
            this.chkBeInBed.Margin = new System.Windows.Forms.Padding(4);
            this.chkBeInBed.Name = "chkBeInBed";
            this.chkBeInBed.Size = new System.Drawing.Size(64, 24);
            this.chkBeInBed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkBeInBed.TabIndex = 7;
            this.chkBeInBed.Text = "在床";
            // 
            // lblDept
            // 
            // 
            // 
            // 
            this.lblDept.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDept.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(573, 12);
            this.lblDept.Margin = new System.Windows.Forms.Padding(4);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(40, 24);
            this.lblDept.TabIndex = 0;
            this.lblDept.Text = "科室";
            this.lblDept.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // sdtpMakerDate
            // 
            this.sdtpMakerDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtpMakerDate.DateFormat = "yyyy-MM-dd";
            this.sdtpMakerDate.DateWidth = 120;
            this.sdtpMakerDate.Enabled = false;
            this.sdtpMakerDate.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtpMakerDate.Location = new System.Drawing.Point(113, 12);
            this.sdtpMakerDate.Margin = new System.Windows.Forms.Padding(4);
            this.sdtpMakerDate.Name = "sdtpMakerDate";
            this.sdtpMakerDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtpMakerDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtpMakerDate.Size = new System.Drawing.Size(431, 25);
            this.sdtpMakerDate.TabIndex = 2;
            // 
            // lblSearchParm
            // 
            // 
            // 
            // 
            this.lblSearchParm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSearchParm.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSearchParm.Location = new System.Drawing.Point(23, 46);
            this.lblSearchParm.Margin = new System.Windows.Forms.Padding(4);
            this.lblSearchParm.Name = "lblSearchParm";
            this.lblSearchParm.Size = new System.Drawing.Size(84, 24);
            this.lblSearchParm.TabIndex = 8;
            this.lblSearchParm.Text = "检索条件";
            this.lblSearchParm.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // chkNewHospital
            // 
            // 
            // 
            // 
            this.chkNewHospital.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNewHospital.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkNewHospital.Location = new System.Drawing.Point(573, 48);
            this.chkNewHospital.Margin = new System.Windows.Forms.Padding(4);
            this.chkNewHospital.Name = "chkNewHospital";
            this.chkNewHospital.Size = new System.Drawing.Size(81, 24);
            this.chkNewHospital.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNewHospital.TabIndex = 6;
            this.chkNewHospital.Text = "新入院";
            // 
            // frmAdmission
            // 
            this.frmAdmission.IsSkip = true;
            // 
            // FrmAdmissionRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAdmissionRegistration";
            this.ShowIcon = false;
            this.Text = "病人入院登记";
            this.OpenWindowBefore += new System.EventHandler(this.FrmAdmissionRegistration_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAdmissionRegistration_KeyDown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnNew;
        private DevComponents.DotNetBar.ButtonItem btnCancel;
        private DevComponents.DotNetBar.ButtonItem btnUpdate;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkLeaveHospital;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDefinedDischarge;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkBeInBed;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNewHospital;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkMakerDate;
        private EfwControls.CustomControl.StatDateTime sdtpMakerDate;
        private DevComponents.DotNetBar.LabelX lblPatType;
        private DevComponents.DotNetBar.LabelX lblDept;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearchParm;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.LabelX lblSearchParm;
        private EfwControls.CustomControl.DataGrid grdPatList;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeptList;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPatType;
        private EfwControls.CustomControl.frmForm frmAdmission;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CaseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DoctorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterHDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterDiseaseName;
    }
}