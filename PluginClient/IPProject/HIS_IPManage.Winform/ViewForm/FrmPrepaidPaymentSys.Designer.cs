namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmPrepaidPaymentSys
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrepaidPaymentSys));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.grdPayDetailList = new EfwControls.CustomControl.DataGrid();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnCharge = new DevComponents.DotNetBar.ButtonItem();
            this.btnVoided = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrint = new DevComponents.DotNetBar.ButtonItem();
            this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.lblPatBedNo = new DevComponents.DotNetBar.LabelX();
            this.lblPatDept = new DevComponents.DotNetBar.LabelX();
            this.lblPatName = new DevComponents.DotNetBar.LabelX();
            this.lblSerialNumber = new DevComponents.DotNetBar.LabelX();
            this.lblPatBalance = new DevComponents.DotNetBar.LabelX();
            this.lblPatSumAccounting = new DevComponents.DotNetBar.LabelX();
            this.lblBalance = new DevComponents.DotNetBar.LabelX();
            this.lblSumAccounting = new DevComponents.DotNetBar.LabelX();
            this.lblSumPayment = new DevComponents.DotNetBar.LabelX();
            this.lblBedNo = new DevComponents.DotNetBar.LabelX();
            this.lblDept = new DevComponents.DotNetBar.LabelX();
            this.lblName = new DevComponents.DotNetBar.LabelX();
            this.lblSerialNo = new DevComponents.DotNetBar.LabelX();
            this.lblPatSumPayment = new DevComponents.DotNetBar.LabelX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.grdPatList = new EfwControls.CustomControl.DataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtDeptList = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.btnSeach = new DevComponents.DotNetBar.ButtonX();
            this.lblSeleceParm = new DevComponents.DotNetBar.LabelX();
            this.txtSeleceParm = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkLeaveHospital = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkDefinedDischarge = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkInTheHospital = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.sdtpMakerDate = new EfwControls.CustomControl.StatDateTime();
            this.chkMakerDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.frmPayment = new EfwControls.CustomControl.frmForm(this.components);
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.states = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayDetailList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).BeginInit();
            this.panelEx4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.grdPayDetailList);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            // grdPayDetailList
            // 
            this.grdPayDetailList.AllowSortWhenClickColumnHeader = false;
            this.grdPayDetailList.AllowUserToAddRows = false;
            this.grdPayDetailList.AllowUserToDeleteRows = false;
            this.grdPayDetailList.AllowUserToResizeColumns = false;
            this.grdPayDetailList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPayDetailList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPayDetailList.BackgroundColor = System.Drawing.Color.White;
            this.grdPayDetailList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPayDetailList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdPayDetailList.ColumnHeadersHeight = 30;
            this.grdPayDetailList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.states});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPayDetailList.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdPayDetailList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPayDetailList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPayDetailList.HighlightSelectedColumnHeaders = false;
            this.grdPayDetailList.Location = new System.Drawing.Point(308, 106);
            this.grdPayDetailList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grdPayDetailList.Name = "grdPayDetailList";
            this.grdPayDetailList.ReadOnly = true;
            this.grdPayDetailList.RowHeadersWidth = 25;
            this.grdPayDetailList.RowTemplate.Height = 23;
            this.grdPayDetailList.SelectAllSignVisible = false;
            this.grdPayDetailList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPayDetailList.SeqVisible = true;
            this.grdPayDetailList.SetCustomStyle = false;
            this.grdPayDetailList.Size = new System.Drawing.Size(956, 624);
            this.grdPayDetailList.TabIndex = 0;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackColor = System.Drawing.Color.Transparent;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnCharge,
            this.btnVoided,
            this.btnPrint,
            this.btnCancel});
            this.bar1.Location = new System.Drawing.Point(308, 81);
            this.bar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(956, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 0;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnCharge
            // 
            this.btnCharge.Name = "btnCharge";
            this.btnCharge.Text = "收费(F6)";
            this.btnCharge.Click += new System.EventHandler(this.btnCharge_Click);
            // 
            // btnVoided
            // 
            this.btnVoided.Name = "btnVoided";
            this.btnVoided.Text = "退费(F7)";
            this.btnVoided.Click += new System.EventHandler(this.btnVoided_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Text = "打印(F8)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.lblPatBedNo);
            this.panelEx3.Controls.Add(this.lblPatDept);
            this.panelEx3.Controls.Add(this.lblPatName);
            this.panelEx3.Controls.Add(this.lblSerialNumber);
            this.panelEx3.Controls.Add(this.lblPatBalance);
            this.panelEx3.Controls.Add(this.lblPatSumAccounting);
            this.panelEx3.Controls.Add(this.lblBalance);
            this.panelEx3.Controls.Add(this.lblSumAccounting);
            this.panelEx3.Controls.Add(this.lblSumPayment);
            this.panelEx3.Controls.Add(this.lblBedNo);
            this.panelEx3.Controls.Add(this.lblDept);
            this.panelEx3.Controls.Add(this.lblName);
            this.panelEx3.Controls.Add(this.lblSerialNo);
            this.panelEx3.Controls.Add(this.lblPatSumPayment);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(308, 0);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(956, 81);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // lblPatBedNo
            // 
            // 
            // 
            // 
            this.lblPatBedNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatBedNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatBedNo.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatBedNo.Location = new System.Drawing.Point(749, 22);
            this.lblPatBedNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatBedNo.Name = "lblPatBedNo";
            this.lblPatBedNo.Size = new System.Drawing.Size(115, 22);
            this.lblPatBedNo.TabIndex = 0;
            // 
            // lblPatDept
            // 
            // 
            // 
            // 
            this.lblPatDept.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatDept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatDept.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatDept.Location = new System.Drawing.Point(520, 19);
            this.lblPatDept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatDept.Name = "lblPatDept";
            this.lblPatDept.Size = new System.Drawing.Size(133, 22);
            this.lblPatDept.TabIndex = 0;
            // 
            // lblPatName
            // 
            // 
            // 
            // 
            this.lblPatName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatName.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatName.Location = new System.Drawing.Point(294, 22);
            this.lblPatName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatName.Name = "lblPatName";
            this.lblPatName.Size = new System.Drawing.Size(133, 22);
            this.lblPatName.TabIndex = 0;
            // 
            // lblSerialNumber
            // 
            // 
            // 
            // 
            this.lblSerialNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSerialNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSerialNumber.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblSerialNumber.Location = new System.Drawing.Point(100, 22);
            this.lblSerialNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(121, 22);
            this.lblSerialNumber.TabIndex = 0;
            // 
            // lblPatBalance
            // 
            // 
            // 
            // 
            this.lblPatBalance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatBalance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatBalance.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatBalance.Location = new System.Drawing.Point(520, 44);
            this.lblPatBalance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatBalance.Name = "lblPatBalance";
            this.lblPatBalance.Size = new System.Drawing.Size(133, 22);
            this.lblPatBalance.TabIndex = 0;
            // 
            // lblPatSumAccounting
            // 
            // 
            // 
            // 
            this.lblPatSumAccounting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatSumAccounting.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatSumAccounting.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatSumAccounting.Location = new System.Drawing.Point(294, 45);
            this.lblPatSumAccounting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatSumAccounting.Name = "lblPatSumAccounting";
            this.lblPatSumAccounting.Size = new System.Drawing.Size(133, 22);
            this.lblPatSumAccounting.TabIndex = 0;
            // 
            // lblBalance
            // 
            // 
            // 
            // 
            this.lblBalance.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBalance.Font = new System.Drawing.Font("宋体", 11F);
            this.lblBalance.Location = new System.Drawing.Point(454, 45);
            this.lblBalance.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(59, 22);
            this.lblBalance.TabIndex = 0;
            this.lblBalance.Text = "余额";
            this.lblBalance.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSumAccounting
            // 
            // 
            // 
            // 
            this.lblSumAccounting.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSumAccounting.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSumAccounting.Location = new System.Drawing.Point(222, 45);
            this.lblSumAccounting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSumAccounting.Name = "lblSumAccounting";
            this.lblSumAccounting.Size = new System.Drawing.Size(68, 22);
            this.lblSumAccounting.TabIndex = 0;
            this.lblSumAccounting.Text = "累计记账";
            this.lblSumAccounting.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSumPayment
            // 
            // 
            // 
            // 
            this.lblSumPayment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSumPayment.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSumPayment.Location = new System.Drawing.Point(22, 45);
            this.lblSumPayment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSumPayment.Name = "lblSumPayment";
            this.lblSumPayment.Size = new System.Drawing.Size(67, 22);
            this.lblSumPayment.TabIndex = 0;
            this.lblSumPayment.Text = "累计交费";
            this.lblSumPayment.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblBedNo
            // 
            // 
            // 
            // 
            this.lblBedNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBedNo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblBedNo.Location = new System.Drawing.Point(681, 22);
            this.lblBedNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblBedNo.Name = "lblBedNo";
            this.lblBedNo.Size = new System.Drawing.Size(59, 22);
            this.lblBedNo.TabIndex = 0;
            this.lblBedNo.Text = "床位号";
            this.lblBedNo.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblDept
            // 
            // 
            // 
            // 
            this.lblDept.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDept.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblDept.Font = new System.Drawing.Font("宋体", 11F);
            this.lblDept.Location = new System.Drawing.Point(454, 22);
            this.lblDept.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(59, 22);
            this.lblDept.TabIndex = 0;
            this.lblDept.Text = "科室";
            this.lblDept.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblName
            // 
            // 
            // 
            // 
            this.lblName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblName.Font = new System.Drawing.Font("宋体", 11F);
            this.lblName.Location = new System.Drawing.Point(229, 22);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(59, 22);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "姓名";
            this.lblName.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSerialNo
            // 
            // 
            // 
            // 
            this.lblSerialNo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSerialNo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSerialNo.Location = new System.Drawing.Point(18, 22);
            this.lblSerialNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblSerialNo.Name = "lblSerialNo";
            this.lblSerialNo.Size = new System.Drawing.Size(78, 22);
            this.lblSerialNo.TabIndex = 0;
            this.lblSerialNo.Text = "住院号(Z)";
            this.lblSerialNo.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblPatSumPayment
            // 
            // 
            // 
            // 
            this.lblPatSumPayment.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatSumPayment.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatSumPayment.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblPatSumPayment.Location = new System.Drawing.Point(91, 45);
            this.lblPatSumPayment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblPatSumPayment.Name = "lblPatSumPayment";
            this.lblPatSumPayment.Size = new System.Drawing.Size(115, 22);
            this.lblPatSumPayment.TabIndex = 0;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(302, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 730);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 4;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.grdPatList);
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(302, 730);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 2;
            // 
            // grdPatList
            // 
            this.grdPatList.AllowSortWhenClickColumnHeader = false;
            this.grdPatList.AllowUserToAddRows = false;
            this.grdPatList.AllowUserToDeleteRows = false;
            this.grdPatList.AllowUserToResizeColumns = false;
            this.grdPatList.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grdPatList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.grdPatList.BackgroundColor = System.Drawing.Color.White;
            this.grdPatList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.grdPatList.ColumnHeadersHeight = 30;
            this.grdPatList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPatList.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdPatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPatList.HighlightSelectedColumnHeaders = false;
            this.grdPatList.Location = new System.Drawing.Point(0, 169);
            this.grdPatList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grdPatList.Name = "grdPatList";
            this.grdPatList.ReadOnly = true;
            this.grdPatList.RowHeadersWidth = 25;
            this.grdPatList.RowTemplate.Height = 23;
            this.grdPatList.SelectAllSignVisible = false;
            this.grdPatList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatList.SeqVisible = true;
            this.grdPatList.SetCustomStyle = false;
            this.grdPatList.Size = new System.Drawing.Size(302, 561);
            this.grdPatList.TabIndex = 0;
            this.grdPatList.CurrentCellChanged += new System.EventHandler(this.grdPatList_CurrentCellChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SerialNumber";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column1.HeaderText = "住院号";
            this.Column1.MinimumWidth = 110;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 110;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "PatName";
            this.Column2.HeaderText = "姓名";
            this.Column2.MinimumWidth = 120;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Sex";
            this.Column3.HeaderText = "性别";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 45;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "BedNo";
            this.Column4.HeaderText = "床号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 55;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.labelX1);
            this.panelEx4.Controls.Add(this.txtDeptList);
            this.panelEx4.Controls.Add(this.btnSeach);
            this.panelEx4.Controls.Add(this.lblSeleceParm);
            this.panelEx4.Controls.Add(this.txtSeleceParm);
            this.panelEx4.Controls.Add(this.chkLeaveHospital);
            this.panelEx4.Controls.Add(this.chkDefinedDischarge);
            this.panelEx4.Controls.Add(this.chkInTheHospital);
            this.panelEx4.Controls.Add(this.sdtpMakerDate);
            this.panelEx4.Controls.Add(this.chkMakerDate);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(302, 169);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 25;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 72);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "科室";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
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
            this.txtDeptList.IsEnterShowCard = false;
            this.txtDeptList.IsNumSelected = false;
            this.txtDeptList.IsPage = true;
            this.txtDeptList.IsShowLetter = false;
            this.txtDeptList.IsShowPage = false;
            this.txtDeptList.IsShowSeq = true;
            this.txtDeptList.Location = new System.Drawing.Point(88, 71);
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
            this.txtDeptList.Size = new System.Drawing.Size(193, 24);
            this.txtDeptList.TabIndex = 1;
            // 
            // btnSeach
            // 
            this.btnSeach.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSeach.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSeach.Location = new System.Drawing.Point(220, 130);
            this.btnSeach.Name = "btnSeach";
            this.btnSeach.Size = new System.Drawing.Size(77, 27);
            this.btnSeach.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSeach.TabIndex = 4;
            this.btnSeach.Text = "查询(&S)";
            this.btnSeach.Click += new System.EventHandler(this.btnSeach_Click);
            // 
            // lblSeleceParm
            // 
            // 
            // 
            // 
            this.lblSeleceParm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSeleceParm.Location = new System.Drawing.Point(12, 101);
            this.lblSeleceParm.Name = "lblSeleceParm";
            this.lblSeleceParm.Size = new System.Drawing.Size(68, 22);
            this.lblSeleceParm.TabIndex = 0;
            this.lblSeleceParm.Text = "病人检索";
            this.lblSeleceParm.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtSeleceParm
            // 
            // 
            // 
            // 
            this.txtSeleceParm.Border.Class = "TextBoxBorder";
            this.txtSeleceParm.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSeleceParm.Location = new System.Drawing.Point(88, 100);
            this.txtSeleceParm.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSeleceParm.Name = "txtSeleceParm";
            this.txtSeleceParm.Size = new System.Drawing.Size(193, 24);
            this.txtSeleceParm.TabIndex = 2;
            this.txtSeleceParm.WatermarkText = "住院号、病案号、床位号、姓名";
            // 
            // chkLeaveHospital
            // 
            // 
            // 
            // 
            this.chkLeaveHospital.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkLeaveHospital.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkLeaveHospital.Location = new System.Drawing.Point(162, 132);
            this.chkLeaveHospital.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkLeaveHospital.Name = "chkLeaveHospital";
            this.chkLeaveHospital.Size = new System.Drawing.Size(55, 22);
            this.chkLeaveHospital.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkLeaveHospital.TabIndex = 3;
            this.chkLeaveHospital.Text = "出院";
            // 
            // chkDefinedDischarge
            // 
            // 
            // 
            // 
            this.chkDefinedDischarge.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkDefinedDischarge.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkDefinedDischarge.Location = new System.Drawing.Point(66, 132);
            this.chkDefinedDischarge.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkDefinedDischarge.Name = "chkDefinedDischarge";
            this.chkDefinedDischarge.Size = new System.Drawing.Size(105, 22);
            this.chkDefinedDischarge.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkDefinedDischarge.TabIndex = 3;
            this.chkDefinedDischarge.Text = "出院未结算";
            // 
            // chkInTheHospital
            // 
            // 
            // 
            // 
            this.chkInTheHospital.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkInTheHospital.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkInTheHospital.Checked = true;
            this.chkInTheHospital.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInTheHospital.CheckValue = "Y";
            this.chkInTheHospital.Location = new System.Drawing.Point(8, 132);
            this.chkInTheHospital.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkInTheHospital.Name = "chkInTheHospital";
            this.chkInTheHospital.Size = new System.Drawing.Size(72, 22);
            this.chkInTheHospital.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkInTheHospital.TabIndex = 3;
            this.chkInTheHospital.Text = "在院";
            // 
            // sdtpMakerDate
            // 
            this.sdtpMakerDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtpMakerDate.DateFormat = "yyyy-MM-dd";
            this.sdtpMakerDate.DateWidth = 120;
            this.sdtpMakerDate.Enabled = false;
            this.sdtpMakerDate.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.sdtpMakerDate.Location = new System.Drawing.Point(88, 14);
            this.sdtpMakerDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sdtpMakerDate.Name = "sdtpMakerDate";
            this.sdtpMakerDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.sdtpMakerDate.ShowStyle = EfwControls.CustomControl.showStyle.vertical;
            this.sdtpMakerDate.Size = new System.Drawing.Size(193, 52);
            this.sdtpMakerDate.TabIndex = 0;
            this.sdtpMakerDate.TabStop = false;
            // 
            // chkMakerDate
            // 
            // 
            // 
            // 
            this.chkMakerDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkMakerDate.Location = new System.Drawing.Point(22, 27);
            this.chkMakerDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkMakerDate.Name = "chkMakerDate";
            this.chkMakerDate.Size = new System.Drawing.Size(58, 22);
            this.chkMakerDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkMakerDate.TabIndex = 0;
            this.chkMakerDate.TabStop = false;
            this.chkMakerDate.Text = "日期";
            this.chkMakerDate.CheckedChanged += new System.EventHandler(this.chkMakerDate_CheckedChanged);
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // frmPayment
            // 
            this.frmPayment.IsSkip = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "InvoiceNO";
            this.Column5.HeaderText = "单据号";
            this.Column5.MinimumWidth = 100;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "MakerDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "g";
            dataGridViewCellStyle3.NullValue = null;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column6.HeaderText = "收费时间";
            this.Column6.MinimumWidth = 150;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column6.Width = 150;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.DataPropertyName = "DictContentName";
            this.Column7.HeaderText = "支付方式";
            this.Column7.MinimumWidth = 100;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "TotalFee";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column8.HeaderText = "收费金额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "MakerEmpName";
            this.Column9.HeaderText = "收费员";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "ReceivEmpName";
            this.Column10.HeaderText = "结账人";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "AccountDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "g";
            dataGridViewCellStyle5.NullValue = null;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column11.HeaderText = "结账时间";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 130;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Status";
            this.Column12.HeaderText = "记录类型";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column12.Width = 140;
            // 
            // states
            // 
            this.states.DataPropertyName = "States";
            this.states.HeaderText = "states";
            this.states.Name = "states";
            this.states.ReadOnly = true;
            this.states.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.states.Visible = false;
            // 
            // FrmPrepaidPaymentSys
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 730);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrepaidPaymentSys";
            this.Text = "预交金管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmPrepaidPaymentSys_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPrepaidPaymentSys_KeyDown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayDetailList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatList)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnCharge;
        private DevComponents.DotNetBar.ButtonItem btnVoided;
        private DevComponents.DotNetBar.ButtonItem btnPrint;
        private DevComponents.DotNetBar.ButtonItem btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private EfwControls.CustomControl.DataGrid grdPatList;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX lblBalance;
        private DevComponents.DotNetBar.LabelX lblSumAccounting;
        private DevComponents.DotNetBar.LabelX lblSumPayment;
        private DevComponents.DotNetBar.LabelX lblBedNo;
        private DevComponents.DotNetBar.LabelX lblDept;
        private DevComponents.DotNetBar.LabelX lblName;
        private DevComponents.DotNetBar.LabelX lblSerialNo;
        private DevComponents.DotNetBar.LabelX lblPatSumPayment;
        private DevComponents.DotNetBar.LabelX lblPatSumAccounting;
        private DevComponents.DotNetBar.LabelX lblPatBalance;
        private EfwControls.CustomControl.DataGrid grdPayDetailList;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btnSeach;
        private DevComponents.DotNetBar.LabelX lblSeleceParm;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSeleceParm;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkLeaveHospital;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDefinedDischarge;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkInTheHospital;
        private EfwControls.CustomControl.StatDateTime sdtpMakerDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkMakerDate;
        private DevComponents.DotNetBar.LabelX lblPatBedNo;
        private DevComponents.DotNetBar.LabelX lblPatDept;
        private DevComponents.DotNetBar.LabelX lblPatName;
        private DevComponents.DotNetBar.LabelX lblSerialNumber;
        private EfwControls.CustomControl.TextBoxCard txtDeptList;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.frmForm frmPayment;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn states;
    }
}