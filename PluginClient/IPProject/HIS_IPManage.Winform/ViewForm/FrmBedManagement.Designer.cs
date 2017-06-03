namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmBedManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBedManagement));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.cmenuOutHospital = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tolDistributionBed = new System.Windows.Forms.ToolStripMenuItem();
            this.tolCancelTheBed = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdDoctorNurse = new System.Windows.Forms.ToolStripMenuItem();
            this.tolUpdBed = new System.Windows.Forms.ToolStripMenuItem();
            this.tolPackBed = new System.Windows.Forms.ToolStripMenuItem();
            this.tolCancelPackBed = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tolDept = new System.Windows.Forms.ToolStripMenuItem();
            this.tolOutHospital = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tolPrintReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.查看血糖图表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.cboInpatientAreaList = new DevComponents.DotNetBar.ComboBoxItem();
            this.btnDistributionBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancelTheBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdDoctorNurse = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnPackBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancelPackBed = new DevComponents.DotNetBar.ButtonItem();
            this.btnOutHospital = new DevComponents.DotNetBar.ButtonItem();
            this.btnDept = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
            this.lblBedStatistical = new DevComponents.DotNetBar.LabelItem();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.chkPatAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.rdoNew = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnPrintReminder = new DevComponents.DotNetBar.ButtonX();
            this.chkIsReminder = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnDeptSend = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.rdoLeaveHospital = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rdoDefinedDischarge = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.rdoBeInBed = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnQueryPatList = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.bedCardControl1 = new EfwControls.HISControl.BedCard.Controls.BedCardControlNew();
            this.grdPatList = new EfwControls.CustomControl.DataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDeptList = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.sdtEnterHDate = new EfwControls.CustomControl.StatDateTime();
            this.tolSeeTemperature = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.cmenuOutHospital.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.tabControl1);
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
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.SelectedTabIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(1264, 730);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.panelEx4);
            this.tabControlPanel1.Controls.Add(this.panelEx3);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(1264, 705);
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
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.bedCardControl1);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(1, 31);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1262, 673);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 4;
            // 
            // cmenuOutHospital
            // 
            this.cmenuOutHospital.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolDistributionBed,
            this.tolCancelTheBed,
            this.tolUpdDoctorNurse,
            this.tolUpdBed,
            this.tolPackBed,
            this.tolCancelPackBed,
            this.toolStripSeparator1,
            this.tolDept,
            this.tolOutHospital,
            this.toolStripSeparator2,
            this.tolPrintReminder,
            this.查看血糖图表ToolStripMenuItem,
            this.tolSeeTemperature});
            this.cmenuOutHospital.Name = "cmenuOutHospital";
            this.cmenuOutHospital.Size = new System.Drawing.Size(161, 280);
            // 
            // tolDistributionBed
            // 
            this.tolDistributionBed.Name = "tolDistributionBed";
            this.tolDistributionBed.Size = new System.Drawing.Size(160, 22);
            this.tolDistributionBed.Text = "分配床位";
            this.tolDistributionBed.Click += new System.EventHandler(this.btnDistributionBed_Click);
            // 
            // tolCancelTheBed
            // 
            this.tolCancelTheBed.Name = "tolCancelTheBed";
            this.tolCancelTheBed.Size = new System.Drawing.Size(160, 22);
            this.tolCancelTheBed.Text = "取消分床";
            this.tolCancelTheBed.Click += new System.EventHandler(this.btnCancelTheBed_Click);
            // 
            // tolUpdDoctorNurse
            // 
            this.tolUpdDoctorNurse.Name = "tolUpdDoctorNurse";
            this.tolUpdDoctorNurse.Size = new System.Drawing.Size(160, 22);
            this.tolUpdDoctorNurse.Text = "修改医护";
            this.tolUpdDoctorNurse.Click += new System.EventHandler(this.btnUpdDoctorNurse_Click);
            // 
            // tolUpdBed
            // 
            this.tolUpdBed.Name = "tolUpdBed";
            this.tolUpdBed.Size = new System.Drawing.Size(160, 22);
            this.tolUpdBed.Text = "病人换床";
            this.tolUpdBed.Click += new System.EventHandler(this.btnUpdBed_Click);
            // 
            // tolPackBed
            // 
            this.tolPackBed.Name = "tolPackBed";
            this.tolPackBed.Size = new System.Drawing.Size(160, 22);
            this.tolPackBed.Text = "病人包床";
            this.tolPackBed.Click += new System.EventHandler(this.btnPackBed_Click);
            // 
            // tolCancelPackBed
            // 
            this.tolCancelPackBed.Name = "tolCancelPackBed";
            this.tolCancelPackBed.Size = new System.Drawing.Size(160, 22);
            this.tolCancelPackBed.Text = "取消包床";
            this.tolCancelPackBed.Click += new System.EventHandler(this.tolCancelPackBed_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // tolDept
            // 
            this.tolDept.Name = "tolDept";
            this.tolDept.Size = new System.Drawing.Size(160, 22);
            this.tolDept.Text = "病人转科";
            this.tolDept.Click += new System.EventHandler(this.tolDept_Click);
            // 
            // tolOutHospital
            // 
            this.tolOutHospital.Name = "tolOutHospital";
            this.tolOutHospital.Size = new System.Drawing.Size(160, 22);
            this.tolOutHospital.Text = "定义出区";
            this.tolOutHospital.Click += new System.EventHandler(this.tolOutHospital_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // tolPrintReminder
            // 
            this.tolPrintReminder.Name = "tolPrintReminder";
            this.tolPrintReminder.Size = new System.Drawing.Size(160, 22);
            this.tolPrintReminder.Text = "打印催款单";
            this.tolPrintReminder.Click += new System.EventHandler(this.tolPrintReminder_Click);
            // 
            // 查看血糖图表ToolStripMenuItem
            // 
            this.查看血糖图表ToolStripMenuItem.Name = "查看血糖图表ToolStripMenuItem";
            this.查看血糖图表ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.查看血糖图表ToolStripMenuItem.Text = "查看血糖图表";
            this.查看血糖图表ToolStripMenuItem.Click += new System.EventHandler(this.查看血糖图表ToolStripMenuItem_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.bar1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(1, 1);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1262, 30);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.cboInpatientAreaList,
            this.btnDistributionBed,
            this.btnCancelTheBed,
            this.btnUpdDoctorNurse,
            this.btnUpdBed,
            this.btnPackBed,
            this.btnCancelPackBed,
            this.btnOutHospital,
            this.btnDept,
            this.btnCancel,
            this.lblBedStatistical});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1262, 32);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "选择病区";
            // 
            // cboInpatientAreaList
            // 
            this.cboInpatientAreaList.ComboWidth = 120;
            this.cboInpatientAreaList.DropDownHeight = 106;
            this.cboInpatientAreaList.Name = "cboInpatientAreaList";
            this.cboInpatientAreaList.SelectedIndexChanged += new System.EventHandler(this.cboInpatientAreaList_SelectedIndexChanged);
            // 
            // btnDistributionBed
            // 
            this.btnDistributionBed.ImagePaddingVertical = 13;
            this.btnDistributionBed.Name = "btnDistributionBed";
            this.btnDistributionBed.Text = "分配床位(&F6)";
            this.btnDistributionBed.Click += new System.EventHandler(this.btnDistributionBed_Click);
            // 
            // btnCancelTheBed
            // 
            this.btnCancelTheBed.Name = "btnCancelTheBed";
            this.btnCancelTheBed.Text = "取消分床(&F7)";
            this.btnCancelTheBed.Click += new System.EventHandler(this.btnCancelTheBed_Click);
            // 
            // btnUpdDoctorNurse
            // 
            this.btnUpdDoctorNurse.Name = "btnUpdDoctorNurse";
            this.btnUpdDoctorNurse.Text = "修改医护(F8)";
            this.btnUpdDoctorNurse.Click += new System.EventHandler(this.btnUpdDoctorNurse_Click);
            // 
            // btnUpdBed
            // 
            this.btnUpdBed.Name = "btnUpdBed";
            this.btnUpdBed.Text = "换床(&F9)";
            this.btnUpdBed.Click += new System.EventHandler(this.btnUpdBed_Click);
            // 
            // btnPackBed
            // 
            this.btnPackBed.Name = "btnPackBed";
            this.btnPackBed.Text = "包床(F10)";
            this.btnPackBed.Click += new System.EventHandler(this.btnPackBed_Click);
            // 
            // btnCancelPackBed
            // 
            this.btnCancelPackBed.Name = "btnCancelPackBed";
            this.btnCancelPackBed.Text = "取消包床(F11)";
            this.btnCancelPackBed.Click += new System.EventHandler(this.btnCancelPackBed_Click);
            // 
            // btnOutHospital
            // 
            this.btnOutHospital.Name = "btnOutHospital";
            this.btnOutHospital.Text = "定义出区";
            this.btnOutHospital.Click += new System.EventHandler(this.btnOutHospital_Click);
            // 
            // btnDept
            // 
            this.btnDept.Name = "btnDept";
            this.btnDept.Text = "病人转科";
            this.btnDept.Click += new System.EventHandler(this.btnDept_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblBedStatistical
            // 
            this.lblBedStatistical.Name = "lblBedStatistical";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "床位一览";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.chkPatAll);
            this.tabControlPanel2.Controls.Add(this.grdPatList);
            this.tabControlPanel2.Controls.Add(this.panelEx2);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 25);
            this.tabControlPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(1264, 705);
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
            // chkPatAll
            // 
            // 
            // 
            // 
            this.chkPatAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPatAll.Location = new System.Drawing.Point(33, 44);
            this.chkPatAll.Name = "chkPatAll";
            this.chkPatAll.Size = new System.Drawing.Size(19, 25);
            this.chkPatAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPatAll.TabIndex = 2;
            this.chkPatAll.CheckedChanged += new System.EventHandler(this.chkPatAll_CheckedChanged);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.rdoNew);
            this.panelEx2.Controls.Add(this.btnPrintReminder);
            this.panelEx2.Controls.Add(this.chkIsReminder);
            this.panelEx2.Controls.Add(this.btnDeptSend);
            this.panelEx2.Controls.Add(this.txtDeptList);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.rdoLeaveHospital);
            this.panelEx2.Controls.Add(this.rdoDefinedDischarge);
            this.panelEx2.Controls.Add(this.rdoBeInBed);
            this.panelEx2.Controls.Add(this.btnQueryPatList);
            this.panelEx2.Controls.Add(this.sdtEnterHDate);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(1, 1);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1262, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // rdoNew
            // 
            // 
            // 
            // 
            this.rdoNew.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoNew.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoNew.Location = new System.Drawing.Point(27, 8);
            this.rdoNew.Name = "rdoNew";
            this.rdoNew.Size = new System.Drawing.Size(68, 22);
            this.rdoNew.TabIndex = 15;
            this.rdoNew.Text = "新入院";
            this.rdoNew.CheckedChanged += new System.EventHandler(this.rdoNew_CheckedChanged);
            // 
            // btnPrintReminder
            // 
            this.btnPrintReminder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintReminder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintReminder.Enabled = false;
            this.btnPrintReminder.Location = new System.Drawing.Point(1114, 9);
            this.btnPrintReminder.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintReminder.Name = "btnPrintReminder";
            this.btnPrintReminder.Size = new System.Drawing.Size(91, 22);
            this.btnPrintReminder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintReminder.TabIndex = 14;
            this.btnPrintReminder.Text = "打印催款单(&P)";
            this.btnPrintReminder.Click += new System.EventHandler(this.btnPrintReminder_Click);
            // 
            // chkIsReminder
            // 
            // 
            // 
            // 
            this.chkIsReminder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkIsReminder.Location = new System.Drawing.Point(881, 9);
            this.chkIsReminder.Name = "chkIsReminder";
            this.chkIsReminder.Size = new System.Drawing.Size(61, 23);
            this.chkIsReminder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkIsReminder.TabIndex = 13;
            this.chkIsReminder.Text = "可催款";
            this.chkIsReminder.CheckedChanged += new System.EventHandler(this.chkIsReminder_CheckedChanged);
            // 
            // btnDeptSend
            // 
            this.btnDeptSend.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeptSend.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeptSend.Location = new System.Drawing.Point(1035, 9);
            this.btnDeptSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeptSend.Name = "btnDeptSend";
            this.btnDeptSend.Size = new System.Drawing.Size(75, 22);
            this.btnDeptSend.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeptSend.TabIndex = 12;
            this.btnDeptSend.Text = "医嘱发送(&S)";
            this.btnDeptSend.Click += new System.EventHandler(this.btnDeptSend_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(334, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(35, 22);
            this.labelX2.TabIndex = 10;
            this.labelX2.Text = "科室";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // rdoLeaveHospital
            // 
            // 
            // 
            // 
            this.rdoLeaveHospital.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoLeaveHospital.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoLeaveHospital.Location = new System.Drawing.Point(274, 8);
            this.rdoLeaveHospital.Name = "rdoLeaveHospital";
            this.rdoLeaveHospital.Size = new System.Drawing.Size(54, 22);
            this.rdoLeaveHospital.TabIndex = 9;
            this.rdoLeaveHospital.Text = "出院";
            this.rdoLeaveHospital.CheckedChanged += new System.EventHandler(this.rdoLeaveHospital_CheckedChanged);
            // 
            // rdoDefinedDischarge
            // 
            // 
            // 
            // 
            this.rdoDefinedDischarge.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoDefinedDischarge.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoDefinedDischarge.Location = new System.Drawing.Point(174, 8);
            this.rdoDefinedDischarge.Name = "rdoDefinedDischarge";
            this.rdoDefinedDischarge.Size = new System.Drawing.Size(88, 22);
            this.rdoDefinedDischarge.TabIndex = 8;
            this.rdoDefinedDischarge.Text = "出院未结算";
            this.rdoDefinedDischarge.CheckedChanged += new System.EventHandler(this.rdoDefinedDischarge_CheckedChanged);
            // 
            // rdoBeInBed
            // 
            // 
            // 
            // 
            this.rdoBeInBed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rdoBeInBed.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.rdoBeInBed.Location = new System.Drawing.Point(107, 8);
            this.rdoBeInBed.Name = "rdoBeInBed";
            this.rdoBeInBed.Size = new System.Drawing.Size(55, 22);
            this.rdoBeInBed.TabIndex = 7;
            this.rdoBeInBed.Text = " 在床";
            this.rdoBeInBed.CheckedChanged += new System.EventHandler(this.rdoBeInBed_CheckedChanged);
            // 
            // btnQueryPatList
            // 
            this.btnQueryPatList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQueryPatList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQueryPatList.Location = new System.Drawing.Point(949, 9);
            this.btnQueryPatList.Margin = new System.Windows.Forms.Padding(2);
            this.btnQueryPatList.Name = "btnQueryPatList";
            this.btnQueryPatList.Size = new System.Drawing.Size(75, 22);
            this.btnQueryPatList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQueryPatList.TabIndex = 6;
            this.btnQueryPatList.Text = "查询(&Q)";
            this.btnQueryPatList.Click += new System.EventHandler(this.btnQueryPatList_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(544, 9);
            this.labelX1.Margin = new System.Windows.Forms.Padding(2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(30, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "日期";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "病人列表";
            // 
            // bedCardControl1
            // 
            this.bedCardControl1.AutoScroll = true;
            this.bedCardControl1.BackColor = System.Drawing.Color.White;
            this.bedCardControl1.BedContextFields = null;
            this.bedCardControl1.BedHeight = 200;
            this.bedCardControl1.BedWidth = 168;
            this.bedCardControl1.ContextMenuStrip = this.cmenuOutHospital;
            this.bedCardControl1.DataSource = null;
            this.bedCardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bedCardControl1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bedCardControl1.LicenseKey = null;
            this.bedCardControl1.Location = new System.Drawing.Point(0, 0);
            this.bedCardControl1.Margin = new System.Windows.Forms.Padding(2);
            this.bedCardControl1.Name = "bedCardControl1";
            this.bedCardControl1.SelectedBed = null;
            this.bedCardControl1.SelectedBedIndex = -1;
            this.bedCardControl1.Size = new System.Drawing.Size(1262, 673);
            this.bedCardControl1.TabIndex = 0;
            // 
            // grdPatList
            // 
            this.grdPatList.AllowSortWhenClickColumnHeader = false;
            this.grdPatList.AllowUserToAddRows = false;
            this.grdPatList.AllowUserToDeleteRows = false;
            this.grdPatList.AllowUserToResizeColumns = false;
            this.grdPatList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPatList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPatList.BackgroundColor = System.Drawing.Color.White;
            this.grdPatList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdPatList.ColumnHeadersHeight = 30;
            this.grdPatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.SerialNumber,
            this.Column4,
            this.Column5,
            this.Column3,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPatList.DefaultCellStyle = dataGridViewCellStyle12;
            this.grdPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPatList.HighlightSelectedColumnHeaders = false;
            this.grdPatList.Location = new System.Drawing.Point(1, 41);
            this.grdPatList.Margin = new System.Windows.Forms.Padding(2);
            this.grdPatList.Name = "grdPatList";
            this.grdPatList.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatList.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.grdPatList.RowHeadersWidth = 25;
            this.grdPatList.RowTemplate.Height = 23;
            this.grdPatList.SelectAllSignVisible = false;
            this.grdPatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatList.SeqVisible = true;
            this.grdPatList.SetCustomStyle = false;
            this.grdPatList.Size = new System.Drawing.Size(1262, 663);
            this.grdPatList.TabIndex = 1;
            this.grdPatList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPatList_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "CheckFlg";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "BedNo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = " 床位号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 60;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SerialNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.SerialNumber.HeaderText = "住院号";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.ReadOnly = true;
            this.SerialNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SerialNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Times";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "住院次数";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 70;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "PatName";
            this.Column5.HeaderText = "姓名";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "SexName";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column3.HeaderText = "性别";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 50;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "AgeName";
            this.Column6.HeaderText = "年龄";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 50;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "EnterHDate";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "d";
            dataGridViewCellStyle7.NullValue = null;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column7.HeaderText = "入院时间";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column7.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "DoctorName";
            this.Column8.HeaderText = "主管医生";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "NurseName";
            this.Column9.HeaderText = "责任护士";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.DataPropertyName = "EnterDiseaseName";
            this.Column10.HeaderText = "入院诊断";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "SumDeposit";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column11.HeaderText = "预交金";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 80;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "SumFee";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this.Column12.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column12.HeaderText = "累计记账";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 80;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "Balance";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column13.HeaderText = "余额";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 80;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "PatStatus";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column14.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column14.HeaderText = "状态";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtDeptList
            // 
            // 
            // 
            // 
            this.txtDeptList.Border.Class = "TextBoxBorder";
            this.txtDeptList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeptList.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtDeptList.ButtonCustom.Image")));
            this.txtDeptList.ButtonCustom.Visible = true;
            this.txtDeptList.CardColumn = null;
            this.txtDeptList.DisplayField = "";
            this.txtDeptList.IsEnterShowCard = true;
            this.txtDeptList.IsNumSelected = false;
            this.txtDeptList.IsPage = true;
            this.txtDeptList.IsShowLetter = false;
            this.txtDeptList.IsShowPage = false;
            this.txtDeptList.IsShowSeq = true;
            this.txtDeptList.Location = new System.Drawing.Point(375, 9);
            this.txtDeptList.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtDeptList.MemberField = "";
            this.txtDeptList.MemberValue = null;
            this.txtDeptList.Name = "txtDeptList";
            this.txtDeptList.QueryFields = new string[] {
        ""};
            this.txtDeptList.QueryFieldsString = "";
            this.txtDeptList.SelectedValue = null;
            this.txtDeptList.ShowCardColumns = null;
            this.txtDeptList.ShowCardDataSource = null;
            this.txtDeptList.ShowCardHeight = 0;
            this.txtDeptList.ShowCardWidth = 0;
            this.txtDeptList.Size = new System.Drawing.Size(145, 21);
            this.txtDeptList.TabIndex = 11;
            // 
            // sdtEnterHDate
            // 
            this.sdtEnterHDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtEnterHDate.DateFormat = "yyyy-MM-dd";
            this.sdtEnterHDate.DateWidth = 120;
            this.sdtEnterHDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtEnterHDate.Location = new System.Drawing.Point(581, 9);
            this.sdtEnterHDate.Margin = new System.Windows.Forms.Padding(2);
            this.sdtEnterHDate.Name = "sdtEnterHDate";
            this.sdtEnterHDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtEnterHDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtEnterHDate.Size = new System.Drawing.Size(290, 21);
            this.sdtEnterHDate.TabIndex = 1;
            // 
            // tolSeeTemperature
            // 
            this.tolSeeTemperature.Name = "tolSeeTemperature";
            this.tolSeeTemperature.Size = new System.Drawing.Size(160, 22);
            this.tolSeeTemperature.Text = "查看三测单数据";
            this.tolSeeTemperature.Click += new System.EventHandler(this.tolSeeTemperature_Click);
            // 
            // FrmBedManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBedManagement";
            this.ShowIcon = false;
            this.Text = "床位管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmBedManagement_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBedManagement_KeyDown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.cmenuOutHospital.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private EfwControls.CustomControl.StatDateTime sdtEnterHDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnQueryPatList;
        private EfwControls.CustomControl.DataGrid grdPatList;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnDistributionBed;
        private DevComponents.DotNetBar.ButtonItem btnCancelTheBed;
        private DevComponents.DotNetBar.ButtonItem btnUpdDoctorNurse;
        private DevComponents.DotNetBar.ButtonItem btnUpdBed;
        private DevComponents.DotNetBar.ButtonItem btnOutHospital;
        private DevComponents.DotNetBar.ButtonItem btnPackBed;
        private DevComponents.DotNetBar.ButtonItem btnCancel;
        private EfwControls.HISControl.BedCard.Controls.BedCardControlNew bedCardControl1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ComboBoxItem cboInpatientAreaList;
        private DevComponents.DotNetBar.LabelItem lblBedStatistical;
        private System.Windows.Forms.ContextMenuStrip cmenuOutHospital;
        private System.Windows.Forms.ToolStripMenuItem tolOutHospital;
        private System.Windows.Forms.ToolStripMenuItem tolDistributionBed;
        private System.Windows.Forms.ToolStripMenuItem tolCancelTheBed;
        private System.Windows.Forms.ToolStripMenuItem tolUpdDoctorNurse;
        private System.Windows.Forms.ToolStripMenuItem tolUpdBed;
        private System.Windows.Forms.ToolStripMenuItem tolPackBed;
        private DevComponents.DotNetBar.ButtonItem btnCancelPackBed;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoLeaveHospital;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoDefinedDischarge;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoBeInBed;
        private DevComponents.DotNetBar.LabelX labelX2;
        private EfwControls.CustomControl.TextBoxCard txtDeptList;
        private DevComponents.DotNetBar.ButtonX btnDeptSend;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPatAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.ToolStripMenuItem tolPrintReminder;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkIsReminder;
        private DevComponents.DotNetBar.ButtonX btnPrintReminder;
        private DevComponents.DotNetBar.ButtonItem btnDept;
        private System.Windows.Forms.ToolStripMenuItem tolDept;
        private System.Windows.Forms.ToolStripMenuItem tolCancelPackBed;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 查看血糖图表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private DevComponents.DotNetBar.Controls.CheckBoxX rdoNew;
        private System.Windows.Forms.ToolStripMenuItem tolSeeTemperature;
    }
}