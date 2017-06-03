namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmExpenseList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpenseList));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.cbPatientAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dgPatientList = new EfwControls.CustomControl.DataGrid();
            this.Checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PatListID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BedNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalDespoit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InDays = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnterHDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.exPanelFeeList = new DevComponents.DotNetBar.ExpandablePanel();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgvCostMsg = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.ReportViewer = new Axgregn6Lib.AxGRDisplayViewer();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.sDTFee = new EfwControls.CustomControl.StatDateTime();
            this.ckbCreateDate = new DevComponents.DotNetBar.CheckBoxItem();
            this.ckbCostDate = new DevComponents.DotNetBar.CheckBoxItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.cmbListType = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnDetailQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrint = new DevComponents.DotNetBar.ButtonItem();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.sDTIn = new EfwControls.CustomControl.StatDateTime();
            this.科室 = new DevComponents.DotNetBar.LabelItem();
            this.cmbDept = new DevComponents.DotNetBar.ComboBoxItem();
            this.rcbIn = new DevComponents.DotNetBar.CheckBoxItem();
            this.rcbOutNoJS = new DevComponents.DotNetBar.CheckBoxItem();
            this.rcbOut = new DevComponents.DotNetBar.CheckBoxItem();
            this.lbDtime = new DevComponents.DotNetBar.LabelItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.txtPatient = new DevComponents.DotNetBar.TextBoxItem();
            this.btnPatientQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrintChecked = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.controlContainerItem3 = new DevComponents.DotNetBar.ControlContainerItem();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatientList)).BeginInit();
            this.exPanelFeeList.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostMsg)).BeginInit();
            this.expandablePanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.bar2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.exPanelFeeList);
            this.panelEx1.Controls.Add(this.bar2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 682);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.expandablePanel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 29);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(253, 653);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 10;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.cbPatientAll);
            this.expandablePanel1.Controls.Add(this.dgPatientList);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(253, 653);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 12;
            this.expandablePanel1.TitleHeight = 28;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "病人列表";
            // 
            // cbPatientAll
            // 
            // 
            // 
            // 
            this.cbPatientAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbPatientAll.Location = new System.Drawing.Point(27, 30);
            this.cbPatientAll.Name = "cbPatientAll";
            this.cbPatientAll.Size = new System.Drawing.Size(18, 23);
            this.cbPatientAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPatientAll.TabIndex = 12;
            this.cbPatientAll.CheckedChanged += new System.EventHandler(this.cbPatientAll_CheckedChanged);
            // 
            // dgPatientList
            // 
            this.dgPatientList.AllowSortWhenClickColumnHeader = false;
            this.dgPatientList.AllowUserToAddRows = false;
            this.dgPatientList.AllowUserToDeleteRows = false;
            this.dgPatientList.AllowUserToResizeColumns = false;
            this.dgPatientList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgPatientList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPatientList.BackgroundColor = System.Drawing.Color.White;
            this.dgPatientList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatientList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgPatientList.ColumnHeadersHeight = 25;
            this.dgPatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgPatientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Checked,
            this.PatListID,
            this.DeptName,
            this.SerialNumber,
            this.BedNo,
            this.PatName,
            this.PatTypeName,
            this.YE,
            this.TotalDespoit,
            this.TotalFee,
            this.Sex,
            this.Age,
            this.InDays,
            this.EnterHDate});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatientList.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgPatientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPatientList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgPatientList.HighlightSelectedColumnHeaders = false;
            this.dgPatientList.Location = new System.Drawing.Point(0, 28);
            this.dgPatientList.Name = "dgPatientList";
            this.dgPatientList.ReadOnly = true;
            this.dgPatientList.RowHeadersWidth = 25;
            this.dgPatientList.RowTemplate.Height = 23;
            this.dgPatientList.SelectAllSignVisible = false;
            this.dgPatientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPatientList.SeqVisible = true;
            this.dgPatientList.SetCustomStyle = false;
            this.dgPatientList.Size = new System.Drawing.Size(253, 625);
            this.dgPatientList.TabIndex = 11;
            this.dgPatientList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPatientList_CellClick);
            // 
            // Checked
            // 
            this.Checked.DataPropertyName = "Checked";
            this.Checked.HeaderText = "";
            this.Checked.Name = "Checked";
            this.Checked.ReadOnly = true;
            this.Checked.Width = 20;
            // 
            // PatListID
            // 
            this.PatListID.DataPropertyName = "PatListID";
            this.PatListID.HeaderText = "PatListID";
            this.PatListID.Name = "PatListID";
            this.PatListID.ReadOnly = true;
            this.PatListID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatListID.Visible = false;
            this.PatListID.Width = 10;
            // 
            // DeptName
            // 
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.HeaderText = "DeptName";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptName.Visible = false;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "住院号";
            this.SerialNumber.MinimumWidth = 100;
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SerialNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BedNo
            // 
            this.BedNo.DataPropertyName = "BedNo";
            this.BedNo.HeaderText = "床号";
            this.BedNo.MinimumWidth = 40;
            this.BedNo.Name = "BedNo";
            this.BedNo.ReadOnly = true;
            this.BedNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BedNo.Width = 40;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "姓名";
            this.PatName.MinimumWidth = 60;
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 90;
            // 
            // PatTypeName
            // 
            this.PatTypeName.DataPropertyName = "PatTypeName";
            this.PatTypeName.HeaderText = "病人类型";
            this.PatTypeName.MinimumWidth = 120;
            this.PatTypeName.Name = "PatTypeName";
            this.PatTypeName.ReadOnly = true;
            this.PatTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatTypeName.Width = 120;
            // 
            // YE
            // 
            this.YE.DataPropertyName = "YE";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = "0.00";
            this.YE.DefaultCellStyle = dataGridViewCellStyle3;
            this.YE.HeaderText = "余额";
            this.YE.MinimumWidth = 80;
            this.YE.Name = "YE";
            this.YE.ReadOnly = true;
            this.YE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.YE.Width = 80;
            // 
            // TotalDespoit
            // 
            this.TotalDespoit.DataPropertyName = "TotalDespoit";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle4.Format = "0.00";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.TotalDespoit.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalDespoit.HeaderText = "累计交费";
            this.TotalDespoit.MinimumWidth = 70;
            this.TotalDespoit.Name = "TotalDespoit";
            this.TotalDespoit.ReadOnly = true;
            this.TotalDespoit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TotalDespoit.Width = 70;
            // 
            // TotalFee
            // 
            this.TotalFee.DataPropertyName = "TotalFee";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.TotalFee.DefaultCellStyle = dataGridViewCellStyle5;
            this.TotalFee.HeaderText = "累计记账";
            this.TotalFee.MinimumWidth = 80;
            this.TotalFee.Name = "TotalFee";
            this.TotalFee.ReadOnly = true;
            this.TotalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TotalFee.Width = 80;
            // 
            // Sex
            // 
            this.Sex.DataPropertyName = "Sex";
            this.Sex.HeaderText = "性别";
            this.Sex.MinimumWidth = 40;
            this.Sex.Name = "Sex";
            this.Sex.ReadOnly = true;
            this.Sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Sex.Width = 40;
            // 
            // Age
            // 
            this.Age.DataPropertyName = "Age";
            this.Age.HeaderText = "年龄";
            this.Age.MinimumWidth = 40;
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Age.Width = 40;
            // 
            // InDays
            // 
            this.InDays.DataPropertyName = "InDays";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = "0";
            this.InDays.DefaultCellStyle = dataGridViewCellStyle6;
            this.InDays.HeaderText = "住院天数";
            this.InDays.MinimumWidth = 70;
            this.InDays.Name = "InDays";
            this.InDays.ReadOnly = true;
            this.InDays.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InDays.Width = 70;
            // 
            // EnterHDate
            // 
            this.EnterHDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EnterHDate.DataPropertyName = "EnterHDate";
            this.EnterHDate.HeaderText = "入院时间";
            this.EnterHDate.MinimumWidth = 150;
            this.EnterHDate.Name = "EnterHDate";
            this.EnterHDate.ReadOnly = true;
            this.EnterHDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.Dock = System.Windows.Forms.DockStyle.Right;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(253, 29);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 653);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 8;
            this.expandableSplitter1.TabStop = false;
            // 
            // exPanelFeeList
            // 
            this.exPanelFeeList.CanvasColor = System.Drawing.SystemColors.Control;
            this.exPanelFeeList.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.exPanelFeeList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.exPanelFeeList.Controls.Add(this.panelEx4);
            this.exPanelFeeList.Controls.Add(this.expandablePanel2);
            this.exPanelFeeList.Controls.Add(this.bar1);
            this.exPanelFeeList.Dock = System.Windows.Forms.DockStyle.Right;
            this.exPanelFeeList.HideControlsWhenCollapsed = true;
            this.exPanelFeeList.Location = new System.Drawing.Point(259, 29);
            this.exPanelFeeList.Name = "exPanelFeeList";
            this.exPanelFeeList.Size = new System.Drawing.Size(1005, 653);
            this.exPanelFeeList.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelFeeList.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelFeeList.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.exPanelFeeList.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.exPanelFeeList.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.exPanelFeeList.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.exPanelFeeList.Style.GradientAngle = 90;
            this.exPanelFeeList.TabIndex = 7;
            this.exPanelFeeList.TitleHeight = 28;
            this.exPanelFeeList.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.exPanelFeeList.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exPanelFeeList.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.exPanelFeeList.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.exPanelFeeList.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.exPanelFeeList.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.exPanelFeeList.TitleStyle.GradientAngle = 90;
            this.exPanelFeeList.TitleStyle.TextTrimming = System.Drawing.StringTrimming.None;
            this.exPanelFeeList.TitleText = "费用清单";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgvCostMsg);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 57);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(3, 596);
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 16;
            // 
            // dgvCostMsg
            // 
            this.dgvCostMsg.AllowUserToAddRows = false;
            this.dgvCostMsg.AllowUserToDeleteRows = false;
            this.dgvCostMsg.AllowUserToOrderColumns = true;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue;
            this.dgvCostMsg.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCostMsg.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvCostMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCostMsg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCostMsg.ColumnHeadersHeight = 25;
            this.dgvCostMsg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCostMsg.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvCostMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCostMsg.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCostMsg.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvCostMsg.HighlightSelectedColumnHeaders = false;
            this.dgvCostMsg.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dgvCostMsg.Location = new System.Drawing.Point(0, 0);
            this.dgvCostMsg.MultiSelect = false;
            this.dgvCostMsg.Name = "dgvCostMsg";
            this.dgvCostMsg.ReadOnly = true;
            this.dgvCostMsg.RowHeadersVisible = false;
            this.dgvCostMsg.RowTemplate.Height = 23;
            this.dgvCostMsg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCostMsg.Size = new System.Drawing.Size(3, 596);
            this.dgvCostMsg.StandardTab = true;
            this.dgvCostMsg.TabIndex = 13;
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.LeftToRight;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.panelEx2);
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(3, 57);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(1002, 596);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 15;
            this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "报表预览";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.ReportViewer);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 26);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1002, 570);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 14;
            this.panelEx2.Text = "panelEx2";
            // 
            // ReportViewer
            // 
            this.ReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportViewer.Enabled = true;
            this.ReportViewer.Location = new System.Drawing.Point(0, 0);
            this.ReportViewer.Name = "ReportViewer";
            this.ReportViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("ReportViewer.OcxState")));
            this.ReportViewer.Size = new System.Drawing.Size(1002, 570);
            this.ReportViewer.TabIndex = 0;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Controls.Add(this.sDTFee);
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Right;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ckbCreateDate,
            this.ckbCostDate,
            this.labelItem1,
            this.controlContainerItem1,
            this.labelItem2,
            this.cmbListType,
            this.btnDetailQuery,
            this.btnPrint});
            this.bar1.Location = new System.Drawing.Point(0, 28);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1005, 29);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 2;
            this.bar1.TabStop = false;
            // 
            // sDTFee
            // 
            this.sDTFee.BackColor = System.Drawing.Color.Transparent;
            this.sDTFee.DateFormat = "yyyy-MM-dd";
            this.sDTFee.DateWidth = 120;
            this.sDTFee.Font = new System.Drawing.Font("宋体", 9F);
            this.sDTFee.Location = new System.Drawing.Point(185, 3);
            this.sDTFee.Name = "sDTFee";
            this.sDTFee.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sDTFee.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sDTFee.Size = new System.Drawing.Size(256, 21);
            this.sDTFee.TabIndex = 1;
            // 
            // ckbCreateDate
            // 
            this.ckbCreateDate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckbCreateDate.Checked = true;
            this.ckbCreateDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCreateDate.Name = "ckbCreateDate";
            this.ckbCreateDate.Text = "记账时间";
            // 
            // ckbCostDate
            // 
            this.ckbCostDate.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckbCostDate.Name = "ckbCostDate";
            this.ckbCostDate.Text = "费用时间";
            // 
            // labelItem1
            // 
            this.labelItem1.BeginGroup = true;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "时间";
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.Control = this.sDTFee;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // labelItem2
            // 
            this.labelItem2.BeginGroup = true;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "清单类型";
            // 
            // cmbListType
            // 
            this.cmbListType.ComboWidth = 100;
            this.cmbListType.DropDownHeight = 106;
            this.cmbListType.Name = "cmbListType";
            // 
            // btnDetailQuery
            // 
            this.btnDetailQuery.FontBold = true;
            this.btnDetailQuery.ImagePaddingHorizontal = 10;
            this.btnDetailQuery.ImagePaddingVertical = 10;
            this.btnDetailQuery.Name = "btnDetailQuery";
            this.btnDetailQuery.Text = "刷新(&R)";
            this.btnDetailQuery.Click += new System.EventHandler(this.btnDetailQuery_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.FontBold = true;
            this.btnPrint.ImagePaddingHorizontal = 10;
            this.btnPrint.ImagePaddingVertical = 10;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Tooltip = "打印费用清单";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Controls.Add(this.sDTIn);
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.科室,
            this.cmbDept,
            this.rcbIn,
            this.rcbOutNoJS,
            this.rcbOut,
            this.lbDtime,
            this.controlContainerItem2,
            this.labelItem3,
            this.txtPatient,
            this.btnPatientQuery,
            this.btnPrintChecked,
            this.btnClose,
            this.buttonItem1});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(1264, 29);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 6;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // sDTIn
            // 
            this.sDTIn.BackColor = System.Drawing.Color.Transparent;
            this.sDTIn.DateFormat = "yyyy-MM-dd";
            this.sDTIn.DateWidth = 120;
            this.sDTIn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sDTIn.Location = new System.Drawing.Point(391, 3);
            this.sDTIn.Name = "sDTIn";
            this.sDTIn.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sDTIn.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sDTIn.Size = new System.Drawing.Size(274, 21);
            this.sDTIn.TabIndex = 6;
            // 
            // 科室
            // 
            this.科室.Name = "科室";
            this.科室.Text = "科室";
            // 
            // cmbDept
            // 
            this.cmbDept.ComboWidth = 100;
            this.cmbDept.DropDownHeight = 106;
            this.cmbDept.ItemHeight = 16;
            this.cmbDept.Name = "cmbDept";
            // 
            // rcbIn
            // 
            this.rcbIn.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rcbIn.Checked = true;
            this.rcbIn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rcbIn.Name = "rcbIn";
            this.rcbIn.Text = "在院";
            this.rcbIn.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.RcbPatientState_Changed);
            // 
            // rcbOutNoJS
            // 
            this.rcbOutNoJS.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rcbOutNoJS.Name = "rcbOutNoJS";
            this.rcbOutNoJS.Text = "出院未结算";
            // 
            // rcbOut
            // 
            this.rcbOut.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rcbOut.Name = "rcbOut";
            this.rcbOut.Text = "已结算";
            this.rcbOut.Visible = false;
            // 
            // lbDtime
            // 
            this.lbDtime.Name = "lbDtime";
            this.lbDtime.Text = "入院日期";
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.sDTIn;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            // 
            // labelItem3
            // 
            this.labelItem3.BeginGroup = true;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "病人";
            // 
            // txtPatient
            // 
            this.txtPatient.Name = "txtPatient";
            this.txtPatient.TextBoxWidth = 100;
            this.txtPatient.WatermarkColor = System.Drawing.SystemColors.GrayText;
            this.txtPatient.WatermarkText = "住院号/姓名";
            // 
            // btnPatientQuery
            // 
            this.btnPatientQuery.BeginGroup = true;
            this.btnPatientQuery.FontBold = true;
            this.btnPatientQuery.ImagePaddingHorizontal = 10;
            this.btnPatientQuery.ImagePaddingVertical = 10;
            this.btnPatientQuery.Name = "btnPatientQuery";
            this.btnPatientQuery.Text = "查询(&Q)";
            this.btnPatientQuery.Click += new System.EventHandler(this.btnPatientQuery_Click);
            // 
            // btnPrintChecked
            // 
            this.btnPrintChecked.FontBold = true;
            this.btnPrintChecked.Name = "btnPrintChecked";
            this.btnPrintChecked.Text = "打印所选(&A)";
            this.btnPrintChecked.Tooltip = "打印所有勾选的病人昨日清单";
            this.btnPrintChecked.Click += new System.EventHandler(this.btnPrintChecked_Click);
            // 
            // btnClose
            // 
            this.btnClose.FontBold = true;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            this.buttonItem1.Visible = false;
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // controlContainerItem3
            // 
            this.controlContainerItem3.AllowItemResize = false;
            this.controlContainerItem3.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem3.Name = "controlContainerItem3";
            // 
            // FrmExpenseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmExpenseList";
            this.Text = "费用清单";
            this.OpenWindowBefore += new System.EventHandler(this.FrmExpenseList_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPatientList)).EndInit();
            this.exPanelFeeList.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCostMsg)).EndInit();
            this.expandablePanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportViewer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.bar2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.StatDateTime sDTIn;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.ExpandablePanel exPanelFeeList;
        private DevComponents.DotNetBar.Bar bar1;
        private EfwControls.CustomControl.StatDateTime sDTFee;
        private DevComponents.DotNetBar.CheckBoxItem ckbCreateDate;
        private DevComponents.DotNetBar.CheckBoxItem ckbCostDate;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ComboBoxItem cmbListType;
        private DevComponents.DotNetBar.ButtonItem btnDetailQuery;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btnPatientQuery;
        private DevComponents.DotNetBar.LabelItem 科室;
        private DevComponents.DotNetBar.ComboBoxItem cmbDept;
        private DevComponents.DotNetBar.LabelItem lbDtime;
        private DevComponents.DotNetBar.CheckBoxItem rcbIn;
        private DevComponents.DotNetBar.CheckBoxItem rcbOutNoJS;
        private DevComponents.DotNetBar.CheckBoxItem rcbOut;
        private DevComponents.DotNetBar.ButtonItem btnPrintChecked;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ButtonItem btnPrint;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.DataGrid dgPatientList;
        public DevComponents.DotNetBar.Controls.DataGridViewX dgvCostMsg;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem3;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.TextBoxItem txtPatient;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private Axgregn6Lib.AxGRDisplayViewer ReportViewer;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbPatientAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatListID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn BedNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn YE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalDespoit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn InDays;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterHDate;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
    }
}