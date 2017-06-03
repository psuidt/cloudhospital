namespace HIS_IPDoctor.Winform.ViewForm
{
    partial class FrmIPDoctorMain
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
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnOrder = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.myBedPatient = new EfwControls.HISControl.BedCard.Controls.BedCardControlNew();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.护理级别ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.饮食ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.病人情况ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.血糖数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHomePage = new System.Windows.Forms.ToolStripMenuItem();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.DeptBedPatient = new EfwControls.HISControl.BedCard.Controls.BedCardControlNew();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.superTabControlPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Controls.Add(this.labelX1);
            this.bar1.Controls.Add(this.cmbDept);
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOrder,
            this.btnRefresh,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(992, 26);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelX1.Location = new System.Drawing.Point(722, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(62, 26);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "科室选择";
            // 
            // cmbDept
            // 
            this.cmbDept.DisplayMember = "Text";
            this.cmbDept.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmbDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.ItemHeight = 16;
            this.cmbDept.Location = new System.Drawing.Point(784, 0);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(208, 22);
            this.cmbDept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDept.TabIndex = 0;
            this.cmbDept.SelectionChangeCommitted += new System.EventHandler(this.cmbDept_SelectionChangeCommitted);
            // 
            // btnOrder
            // 
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Text = "病人医嘱";
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新病人";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(0, 26);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(992, 506);
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 1;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1,
            this.superTabItem2});
            this.superTabControl1.Text = "superTabControl1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Controls.Add(this.myBedPatient);
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(992, 478);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // myBedPatient
            // 
            this.myBedPatient.AutoScroll = true;
            this.myBedPatient.BackColor = System.Drawing.Color.White;
            this.myBedPatient.BedContextFields = null;
            this.myBedPatient.BedHeight = 200;
            this.myBedPatient.BedWidth = 168;
            this.myBedPatient.ContextMenuStrip = this.contextMenuStrip1;
            this.myBedPatient.DataSource = null;
            this.myBedPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myBedPatient.Font = new System.Drawing.Font("宋体", 10F);
            this.myBedPatient.LicenseKey = null;
            this.myBedPatient.Location = new System.Drawing.Point(0, 0);
            this.myBedPatient.Name = "myBedPatient";
            this.myBedPatient.SelectedBed = null;
            this.myBedPatient.SelectedBedIndex = -1;
            this.myBedPatient.Size = new System.Drawing.Size(992, 478);
            this.myBedPatient.TabIndex = 0;
            this.myBedPatient.BedDoubleClick += new System.EventHandler(this.myBedPatient_BedDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.护理级别ToolStripMenuItem,
            this.饮食ToolStripMenuItem,
            this.病人情况ToolStripMenuItem,
            this.血糖数据ToolStripMenuItem,
            this.tsmHomePage});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 114);
            // 
            // 护理级别ToolStripMenuItem
            // 
            this.护理级别ToolStripMenuItem.Name = "护理级别ToolStripMenuItem";
            this.护理级别ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.护理级别ToolStripMenuItem.Text = "护理等级";
            // 
            // 饮食ToolStripMenuItem
            // 
            this.饮食ToolStripMenuItem.Name = "饮食ToolStripMenuItem";
            this.饮食ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.饮食ToolStripMenuItem.Text = "饮食等级";
            // 
            // 病人情况ToolStripMenuItem
            // 
            this.病人情况ToolStripMenuItem.Name = "病人情况ToolStripMenuItem";
            this.病人情况ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.病人情况ToolStripMenuItem.Text = "病人情况";
            // 
            // 血糖数据ToolStripMenuItem
            // 
            this.血糖数据ToolStripMenuItem.Name = "血糖数据ToolStripMenuItem";
            this.血糖数据ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.血糖数据ToolStripMenuItem.Text = "血糖图表";
            this.血糖数据ToolStripMenuItem.Click += new System.EventHandler(this.血糖数据ToolStripMenuItem_Click);
            // 
            // tsmHomePage
            // 
            this.tsmHomePage.Name = "tsmHomePage";
            this.tsmHomePage.ShowShortcutKeys = false;
            this.tsmHomePage.Size = new System.Drawing.Size(124, 22);
            this.tsmHomePage.Text = "病案首页";
            this.tsmHomePage.Click += new System.EventHandler(this.tsmHomePage_Click);
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "我的病人";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.DeptBedPatient);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 28);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(992, 478);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // DeptBedPatient
            // 
            this.DeptBedPatient.AutoScroll = true;
            this.DeptBedPatient.BackColor = System.Drawing.Color.White;
            this.DeptBedPatient.BedContextFields = null;
            this.DeptBedPatient.BedHeight = 200;
            this.DeptBedPatient.BedWidth = 168;
            this.DeptBedPatient.ContextMenuStrip = this.contextMenuStrip1;
            this.DeptBedPatient.DataSource = null;
            this.DeptBedPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeptBedPatient.Font = new System.Drawing.Font("宋体", 10F);
            this.DeptBedPatient.LicenseKey = null;
            this.DeptBedPatient.Location = new System.Drawing.Point(0, 0);
            this.DeptBedPatient.Name = "DeptBedPatient";
            this.DeptBedPatient.SelectedBed = null;
            this.DeptBedPatient.SelectedBedIndex = -1;
            this.DeptBedPatient.Size = new System.Drawing.Size(992, 478);
            this.DeptBedPatient.TabIndex = 0;
            this.DeptBedPatient.BedDoubleClick += new System.EventHandler(this.DeptBedPatient_BedDoubleClick);
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "本科病人";
            // 
            // FrmIPDoctorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 532);
            this.Controls.Add(this.superTabControl1);
            this.Controls.Add(this.bar1);
            this.Name = "FrmIPDoctorMain";
            this.Text = "床头卡显示";
            this.OpenWindowBefore += new System.EventHandler(this.FrmIPDoctorMain_OpenWindowBefore);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.superTabControlPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnOrder;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private EfwControls.HISControl.BedCard.Controls.BedCardControlNew myBedPatient;
        private EfwControls.HISControl.BedCard.Controls.BedCardControlNew DeptBedPatient;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDept;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 护理级别ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 饮食ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 病人情况ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 血糖数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmHomePage;
    }
}