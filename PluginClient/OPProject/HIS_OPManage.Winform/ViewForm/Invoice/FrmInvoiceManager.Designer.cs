namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmInvoiceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInvoiceManager));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnNewAllot = new DevComponents.DotNetBar.ButtonItem();
            this.btnStopUse = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dtAllotDate = new EfwControls.CustomControl.StatDateTime();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.chkEmp = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAllotDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkStopUse = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkNoUse = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkUseAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkInUse = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtRefundMoney = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.txtRefundCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtTotalMoney = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtAllCount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtEnd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtStart = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgInvoice = new EfwControls.CustomControl.DataGrid();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerfChar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllotDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllotEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoicetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNewAllot,
            this.btnStopUse,
            this.btnDelete,
            this.btnRefresh,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1012, 26);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 1;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnNewAllot
            // 
            this.btnNewAllot.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNewAllot.Image = ((System.Drawing.Image)(resources.GetObject("btnNewAllot.Image")));
            this.btnNewAllot.Name = "btnNewAllot";
            this.btnNewAllot.Text = "新分配(&N)";
            this.btnNewAllot.Click += new System.EventHandler(this.btnNewAllot_Click);
            // 
            // btnStopUse
            // 
            this.btnStopUse.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnStopUse.Image = ((System.Drawing.Image)(resources.GetObject("btnStopUse.Image")));
            this.btnStopUse.Name = "btnStopUse";
            this.btnStopUse.Text = "停用(&T)";
            this.btnStopUse.Click += new System.EventHandler(this.btnStopUse_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "删除(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Text = "刷新(&F)";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dtAllotDate);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.txtEmp);
            this.panelEx1.Controls.Add(this.chkEmp);
            this.panelEx1.Controls.Add(this.chkAllotDate);
            this.panelEx1.Controls.Add(this.chkStopUse);
            this.panelEx1.Controls.Add(this.chkNoUse);
            this.panelEx1.Controls.Add(this.chkUseAll);
            this.panelEx1.Controls.Add(this.chkInUse);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 26);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1012, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Top;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // dtAllotDate
            // 
            this.dtAllotDate.BackColor = System.Drawing.Color.Transparent;
            this.dtAllotDate.DateFormat = "yyyy-MM-dd";
            this.dtAllotDate.DateWidth = 120;
            this.dtAllotDate.Enabled = false;
            this.dtAllotDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtAllotDate.Location = new System.Drawing.Point(365, 9);
            this.dtAllotDate.Name = "dtAllotDate";
            this.dtAllotDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtAllotDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtAllotDate.Size = new System.Drawing.Size(343, 21);
            this.dtAllotDate.TabIndex = 11;
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(934, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 9;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtEmp
            // 
            // 
            // 
            // 
            this.txtEmp.Border.Class = "TextBoxBorder";
            this.txtEmp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmp.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtEmp.ButtonCustom.Image")));
            this.txtEmp.ButtonCustom.Visible = true;
            this.txtEmp.CardColumn = null;
            this.txtEmp.DisplayField = "";
            this.txtEmp.Enabled = false;
            this.txtEmp.IsEnterShowCard = true;
            this.txtEmp.IsNumSelected = false;
            this.txtEmp.IsPage = true;
            this.txtEmp.IsShowLetter = false;
            this.txtEmp.IsShowPage = false;
            this.txtEmp.IsShowSeq = true;
            this.txtEmp.Location = new System.Drawing.Point(795, 9);
            this.txtEmp.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtEmp.MemberField = "";
            this.txtEmp.MemberValue = null;
            this.txtEmp.Name = "txtEmp";
            this.txtEmp.QueryFields = new string[] {
        ""};
            this.txtEmp.QueryFieldsString = "";
            this.txtEmp.SelectedValue = null;
            this.txtEmp.ShowCardColumns = null;
            this.txtEmp.ShowCardDataSource = null;
            this.txtEmp.ShowCardHeight = 0;
            this.txtEmp.ShowCardWidth = 0;
            this.txtEmp.Size = new System.Drawing.Size(133, 21);
            this.txtEmp.TabIndex = 8;
            // 
            // chkEmp
            // 
            // 
            // 
            // 
            this.chkEmp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkEmp.Location = new System.Drawing.Point(720, 10);
            this.chkEmp.Name = "chkEmp";
            this.chkEmp.Size = new System.Drawing.Size(69, 23);
            this.chkEmp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkEmp.TabIndex = 7;
            this.chkEmp.Text = "领用人";
            this.chkEmp.CheckedChanged += new System.EventHandler(this.chkEmp_CheckedChanged);
            // 
            // chkAllotDate
            // 
            // 
            // 
            // 
            this.chkAllotDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAllotDate.Location = new System.Drawing.Point(269, 8);
            this.chkAllotDate.Name = "chkAllotDate";
            this.chkAllotDate.Size = new System.Drawing.Size(90, 23);
            this.chkAllotDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAllotDate.TabIndex = 4;
            this.chkAllotDate.Text = "领用时间";
            this.chkAllotDate.CheckedChanged += new System.EventHandler(this.chkAllotDate_CheckedChanged);
            // 
            // chkStopUse
            // 
            // 
            // 
            // 
            this.chkStopUse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkStopUse.Location = new System.Drawing.Point(214, 9);
            this.chkStopUse.Name = "chkStopUse";
            this.chkStopUse.Size = new System.Drawing.Size(65, 23);
            this.chkStopUse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkStopUse.TabIndex = 3;
            this.chkStopUse.Text = "停用";
            // 
            // chkNoUse
            // 
            // 
            // 
            // 
            this.chkNoUse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNoUse.Location = new System.Drawing.Point(148, 9);
            this.chkNoUse.Name = "chkNoUse";
            this.chkNoUse.Size = new System.Drawing.Size(60, 23);
            this.chkNoUse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNoUse.TabIndex = 2;
            this.chkNoUse.Text = "备用";
            // 
            // chkUseAll
            // 
            // 
            // 
            // 
            this.chkUseAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUseAll.Location = new System.Drawing.Point(80, 9);
            this.chkUseAll.Name = "chkUseAll";
            this.chkUseAll.Size = new System.Drawing.Size(62, 23);
            this.chkUseAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUseAll.TabIndex = 1;
            this.chkUseAll.Text = "用完";
            // 
            // chkInUse
            // 
            // 
            // 
            // 
            this.chkInUse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkInUse.Checked = true;
            this.chkInUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInUse.CheckValue = "Y";
            this.chkInUse.Location = new System.Drawing.Point(3, 9);
            this.chkInUse.Name = "chkInUse";
            this.chkInUse.Size = new System.Drawing.Size(71, 23);
            this.chkInUse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkInUse.TabIndex = 0;
            this.chkInUse.Text = "使用中";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtRefundMoney);
            this.panelEx2.Controls.Add(this.labelX10);
            this.panelEx2.Controls.Add(this.labelX9);
            this.panelEx2.Controls.Add(this.txtRefundCount);
            this.panelEx2.Controls.Add(this.labelX8);
            this.panelEx2.Controls.Add(this.txtTotalMoney);
            this.panelEx2.Controls.Add(this.labelX7);
            this.panelEx2.Controls.Add(this.labelX6);
            this.panelEx2.Controls.Add(this.txtCount);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.txtAllCount);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.txtEnd);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.txtStart);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Location = new System.Drawing.Point(0, 400);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1012, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 4;
            // 
            // txtRefundMoney
            // 
            // 
            // 
            // 
            this.txtRefundMoney.Border.Class = "TextBoxBorder";
            this.txtRefundMoney.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRefundMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRefundMoney.Location = new System.Drawing.Point(889, 8);
            this.txtRefundMoney.Name = "txtRefundMoney";
            this.txtRefundMoney.ReadOnly = true;
            this.txtRefundMoney.Size = new System.Drawing.Size(86, 21);
            this.txtRefundMoney.TabIndex = 16;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX10.Location = new System.Drawing.Point(832, 11);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(56, 18);
            this.labelX10.TabIndex = 15;
            this.labelX10.Text = "票面金额";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX9.Location = new System.Drawing.Point(813, 11);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(17, 18);
            this.labelX9.TabIndex = 14;
            this.labelX9.Text = "张";
            // 
            // txtRefundCount
            // 
            // 
            // 
            // 
            this.txtRefundCount.Border.Class = "TextBoxBorder";
            this.txtRefundCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRefundCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRefundCount.Location = new System.Drawing.Point(766, 8);
            this.txtRefundCount.Name = "txtRefundCount";
            this.txtRefundCount.ReadOnly = true;
            this.txtRefundCount.Size = new System.Drawing.Size(44, 21);
            this.txtRefundCount.TabIndex = 13;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(705, 11);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(55, 18);
            this.labelX8.TabIndex = 12;
            this.labelX8.Text = "其中退费";
            // 
            // txtTotalMoney
            // 
            // 
            // 
            // 
            this.txtTotalMoney.Border.Class = "TextBoxBorder";
            this.txtTotalMoney.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTotalMoney.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTotalMoney.Location = new System.Drawing.Point(621, 9);
            this.txtTotalMoney.Name = "txtTotalMoney";
            this.txtTotalMoney.ReadOnly = true;
            this.txtTotalMoney.Size = new System.Drawing.Size(78, 21);
            this.txtTotalMoney.TabIndex = 11;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(553, 11);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(62, 18);
            this.labelX7.TabIndex = 10;
            this.labelX7.Text = "票面金额";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(532, 11);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(15, 18);
            this.labelX6.TabIndex = 9;
            this.labelX6.Text = "张";
            // 
            // txtCount
            // 
            // 
            // 
            // 
            this.txtCount.Border.Class = "TextBoxBorder";
            this.txtCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtCount.Location = new System.Drawing.Point(460, 10);
            this.txtCount.Name = "txtCount";
            this.txtCount.ReadOnly = true;
            this.txtCount.Size = new System.Drawing.Size(66, 21);
            this.txtCount.TabIndex = 8;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(403, 11);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(63, 18);
            this.labelX5.TabIndex = 7;
            this.labelX5.Text = "实际使用";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(379, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(31, 18);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "张";
            // 
            // txtAllCount
            // 
            // 
            // 
            // 
            this.txtAllCount.Border.Class = "TextBoxBorder";
            this.txtAllCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAllCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAllCount.Location = new System.Drawing.Point(308, 10);
            this.txtAllCount.Name = "txtAllCount";
            this.txtAllCount.ReadOnly = true;
            this.txtAllCount.Size = new System.Drawing.Size(65, 21);
            this.txtAllCount.TabIndex = 5;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(285, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(17, 18);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "共";
            // 
            // txtEnd
            // 
            // 
            // 
            // 
            this.txtEnd.Border.Class = "TextBoxBorder";
            this.txtEnd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEnd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtEnd.Location = new System.Drawing.Point(188, 10);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.ReadOnly = true;
            this.txtEnd.Size = new System.Drawing.Size(91, 21);
            this.txtEnd.TabIndex = 3;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(168, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(14, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "至";
            // 
            // txtStart
            // 
            // 
            // 
            // 
            this.txtStart.Border.Class = "TextBoxBorder";
            this.txtStart.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStart.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtStart.Location = new System.Drawing.Point(80, 9);
            this.txtStart.Name = "txtStart";
            this.txtStart.ReadOnly = true;
            this.txtStart.Size = new System.Drawing.Size(82, 21);
            this.txtStart.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(8, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(66, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "发票起始号";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgInvoice);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx3.Location = new System.Drawing.Point(0, 66);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1012, 334);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 5;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgInvoice
            // 
            this.dgInvoice.AllowSortWhenClickColumnHeader = false;
            this.dgInvoice.AllowUserToAddRows = false;
            this.dgInvoice.AllowUserToResizeColumns = false;
            this.dgInvoice.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.AliceBlue;
            this.dgInvoice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgInvoice.BackgroundColor = System.Drawing.Color.White;
            this.dgInvoice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgInvoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgInvoice.ColumnHeadersHeight = 33;
            this.dgInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.PerfChar,
            this.InvoiceTypeName,
            this.EmpName,
            this.StartNo,
            this.EndNO,
            this.CurrentNO,
            this.StatusName,
            this.AllotDate,
            this.AllotEmpName,
            this.invoicetype,
            this.Status});
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle24.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInvoice.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoice.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.dgInvoice.HighlightSelectedColumnHeaders = false;
            this.dgInvoice.Location = new System.Drawing.Point(0, 0);
            this.dgInvoice.Name = "dgInvoice";
            this.dgInvoice.ReadOnly = true;
            this.dgInvoice.RowHeadersWidth = 30;
            this.dgInvoice.RowTemplate.Height = 23;
            this.dgInvoice.SelectAllSignVisible = false;
            this.dgInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoice.SeqVisible = true;
            this.dgInvoice.SetCustomStyle = true;
            this.dgInvoice.Size = new System.Drawing.Size(1012, 334);
            this.dgInvoice.TabIndex = 3;
            this.dgInvoice.Click += new System.EventHandler(this.dgInvoice_Click);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "发票卷号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Width = 50;
            // 
            // PerfChar
            // 
            this.PerfChar.DataPropertyName = "PerfChar";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PerfChar.DefaultCellStyle = dataGridViewCellStyle15;
            this.PerfChar.HeaderText = "前缀字符";
            this.PerfChar.Name = "PerfChar";
            this.PerfChar.ReadOnly = true;
            this.PerfChar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PerfChar.Width = 40;
            // 
            // InvoiceTypeName
            // 
            this.InvoiceTypeName.DataPropertyName = "InvoiceTypeName";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.InvoiceTypeName.DefaultCellStyle = dataGridViewCellStyle16;
            this.InvoiceTypeName.HeaderText = "票据类型";
            this.InvoiceTypeName.Name = "InvoiceTypeName";
            this.InvoiceTypeName.ReadOnly = true;
            this.InvoiceTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EmpName
            // 
            this.EmpName.DataPropertyName = "EmpName";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EmpName.DefaultCellStyle = dataGridViewCellStyle17;
            this.EmpName.HeaderText = "领用人";
            this.EmpName.Name = "EmpName";
            this.EmpName.ReadOnly = true;
            this.EmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StartNo
            // 
            this.StartNo.DataPropertyName = "StartNo";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.StartNo.DefaultCellStyle = dataGridViewCellStyle18;
            this.StartNo.HeaderText = "开始票号";
            this.StartNo.Name = "StartNo";
            this.StartNo.ReadOnly = true;
            this.StartNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EndNO
            // 
            this.EndNO.DataPropertyName = "EndNO";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.EndNO.DefaultCellStyle = dataGridViewCellStyle19;
            this.EndNO.HeaderText = "结束票号";
            this.EndNO.Name = "EndNO";
            this.EndNO.ReadOnly = true;
            this.EndNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CurrentNO
            // 
            this.CurrentNO.DataPropertyName = "CurrentNO";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CurrentNO.DefaultCellStyle = dataGridViewCellStyle20;
            this.CurrentNO.HeaderText = "当前票号";
            this.CurrentNO.Name = "CurrentNO";
            this.CurrentNO.ReadOnly = true;
            this.CurrentNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StatusName
            // 
            this.StatusName.DataPropertyName = "StatusName";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.StatusName.DefaultCellStyle = dataGridViewCellStyle21;
            this.StatusName.HeaderText = "状态";
            this.StatusName.Name = "StatusName";
            this.StatusName.ReadOnly = true;
            this.StatusName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StatusName.Width = 98;
            // 
            // AllotDate
            // 
            this.AllotDate.DataPropertyName = "AllotDate";
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.Format = "yyyy-MM-dd HH:mm:ss";
            this.AllotDate.DefaultCellStyle = dataGridViewCellStyle22;
            this.AllotDate.HeaderText = "分配时间";
            this.AllotDate.Name = "AllotDate";
            this.AllotDate.ReadOnly = true;
            this.AllotDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AllotDate.Width = 180;
            // 
            // AllotEmpName
            // 
            this.AllotEmpName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AllotEmpName.DataPropertyName = "AllotEmpName";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.AllotEmpName.DefaultCellStyle = dataGridViewCellStyle23;
            this.AllotEmpName.HeaderText = "分配人";
            this.AllotEmpName.Name = "AllotEmpName";
            this.AllotEmpName.ReadOnly = true;
            this.AllotEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // invoicetype
            // 
            this.invoicetype.DataPropertyName = "invoicetype";
            this.invoicetype.HeaderText = "Column1";
            this.invoicetype.Name = "invoicetype";
            this.invoicetype.ReadOnly = true;
            this.invoicetype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.invoicetype.Visible = false;
            this.invoicetype.Width = 5;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Column1";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Status.Visible = false;
            // 
            // FrmInvoiceManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 440);
            this.Controls.Add(this.panelEx3);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.bar1);
            this.Name = "FrmInvoiceManager";
            this.Text = "票据管理";
            this.Load += new System.EventHandler(this.FrmInvoiceManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnNewAllot;
        private DevComponents.DotNetBar.ButtonItem btnStopUse;
        private DevComponents.DotNetBar.ButtonItem btnDelete;
        private DevComponents.DotNetBar.ButtonItem btnRefresh;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private EfwControls.CustomControl.TextBoxCard txtEmp;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkEmp;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAllotDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkStopUse;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNoUse;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUseAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkInUse;
        private EfwControls.CustomControl.StatDateTime dtAllotDate;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRefundMoney;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRefundCount;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTotalMoney;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCount;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAllCount;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEnd;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStart;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.DataGrid dgInvoice;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerfChar;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllotDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllotEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoicetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}