namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmRecipteQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecipteQuery));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetail = new EfwControls.CustomControl.DataGrid();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PresAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgQueryData = new EfwControls.CustomControl.DataGrid();
            this.selected = new DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn();
            this.costHeadid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.presDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.presEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChargeEmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeeItemHeadID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.chkRefund = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtCardNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkCardNO = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtPatName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkPatName = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmbPatType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkPayType = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtChareEmp = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.chkChareEmp = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtInvoiceNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkInvoiceNO = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.sdtCostDate = new EfwControls.CustomControl.StatDateTime();
            this.chkCostDate = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQueryData)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1276, 532);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgDetail);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(546, 66);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(730, 466);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 4;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgDetail
            // 
            this.dgDetail.AllowSortWhenClickColumnHeader = false;
            this.dgDetail.AllowUserToAddRows = false;
            this.dgDetail.AllowUserToResizeColumns = false;
            this.dgDetail.AllowUserToResizeRows = false;
            this.dgDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Spec,
            this.PackNum,
            this.PackUnit,
            this.MiniNum,
            this.MiniUnit,
            this.PresAmount,
            this.RetailPrice,
            this.Column8});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetail.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetail.HighlightSelectedColumnHeaders = false;
            this.dgDetail.Location = new System.Drawing.Point(0, 0);
            this.dgDetail.Name = "dgDetail";
            this.dgDetail.ReadOnly = true;
            this.dgDetail.RowHeadersWidth = 30;
            this.dgDetail.RowTemplate.Height = 23;
            this.dgDetail.SelectAllSignVisible = false;
            this.dgDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetail.SeqVisible = true;
            this.dgDetail.SetCustomStyle = false;
            this.dgDetail.Size = new System.Drawing.Size(730, 466);
            this.dgDetail.TabIndex = 0;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemName.HeaderText = "项目名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Spec.DefaultCellStyle = dataGridViewCellStyle3;
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Spec.Width = 120;
            // 
            // PackNum
            // 
            this.PackNum.DataPropertyName = "PackNum";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.PackNum.DefaultCellStyle = dataGridViewCellStyle4;
            this.PackNum.HeaderText = "包装数量";
            this.PackNum.Name = "PackNum";
            this.PackNum.ReadOnly = true;
            this.PackNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackNum.Width = 60;
            // 
            // PackUnit
            // 
            this.PackUnit.DataPropertyName = "PackUnit";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PackUnit.DefaultCellStyle = dataGridViewCellStyle5;
            this.PackUnit.HeaderText = "包装单位";
            this.PackUnit.Name = "PackUnit";
            this.PackUnit.ReadOnly = true;
            this.PackUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnit.Width = 60;
            // 
            // MiniNum
            // 
            this.MiniNum.DataPropertyName = "MiniNum";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.MiniNum.DefaultCellStyle = dataGridViewCellStyle6;
            this.MiniNum.HeaderText = "基本数量";
            this.MiniNum.Name = "MiniNum";
            this.MiniNum.ReadOnly = true;
            this.MiniNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniNum.Width = 60;
            // 
            // MiniUnit
            // 
            this.MiniUnit.DataPropertyName = "MiniUnit";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MiniUnit.DefaultCellStyle = dataGridViewCellStyle7;
            this.MiniUnit.HeaderText = "基本单位";
            this.MiniUnit.Name = "MiniUnit";
            this.MiniUnit.ReadOnly = true;
            this.MiniUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniUnit.Width = 60;
            // 
            // PresAmount
            // 
            this.PresAmount.DataPropertyName = "PresAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.PresAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.PresAmount.HeaderText = "付数";
            this.PresAmount.Name = "PresAmount";
            this.PresAmount.ReadOnly = true;
            this.PresAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PresAmount.Width = 35;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N4";
            dataGridViewCellStyle9.NullValue = null;
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.RetailPrice.HeaderText = "零售价";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "TotalFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column8.HeaderText = "项目金额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 80;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(540, 66);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 466);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 3;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgQueryData);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 66);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(540, 466);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 2;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgQueryData
            // 
            this.dgQueryData.AllowSortWhenClickColumnHeader = false;
            this.dgQueryData.AllowUserToAddRows = false;
            this.dgQueryData.AllowUserToResizeColumns = false;
            this.dgQueryData.AllowUserToResizeRows = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgQueryData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgQueryData.BackgroundColor = System.Drawing.Color.White;
            this.dgQueryData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgQueryData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgQueryData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQueryData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected,
            this.costHeadid,
            this.InvoiceNO,
            this.totalFee,
            this.status,
            this.patname,
            this.presDeptName,
            this.presEmpName,
            this.ExecDeptName,
            this.costdate,
            this.ChargeEmpName,
            this.FeeItemHeadID});
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgQueryData.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgQueryData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgQueryData.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgQueryData.HighlightSelectedColumnHeaders = false;
            this.dgQueryData.Location = new System.Drawing.Point(0, 0);
            this.dgQueryData.MultiSelect = false;
            this.dgQueryData.Name = "dgQueryData";
            this.dgQueryData.ReadOnly = true;
            this.dgQueryData.RowHeadersWidth = 25;
            this.dgQueryData.RowTemplate.Height = 23;
            this.dgQueryData.SelectAllSignVisible = false;
            this.dgQueryData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgQueryData.SeqVisible = true;
            this.dgQueryData.SetCustomStyle = false;
            this.dgQueryData.Size = new System.Drawing.Size(540, 466);
            this.dgQueryData.TabIndex = 1;
            this.dgQueryData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgQueryData_CellClick);
            this.dgQueryData.Click += new System.EventHandler(this.dgQueryData_Click);
            // 
            // selected
            // 
            this.selected.Checked = true;
            this.selected.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.selected.CheckValue = null;
            this.selected.DataPropertyName = "selected";
            this.selected.HeaderText = "选";
            this.selected.Name = "selected";
            this.selected.ReadOnly = true;
            this.selected.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.selected.Visible = false;
            this.selected.Width = 30;
            // 
            // costHeadid
            // 
            this.costHeadid.DataPropertyName = "costHeadid";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.costHeadid.DefaultCellStyle = dataGridViewCellStyle14;
            this.costHeadid.HeaderText = "结算ID";
            this.costHeadid.Name = "costHeadid";
            this.costHeadid.ReadOnly = true;
            this.costHeadid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.costHeadid.Visible = false;
            this.costHeadid.Width = 60;
            // 
            // InvoiceNO
            // 
            this.InvoiceNO.DataPropertyName = "InvoiceNO";
            this.InvoiceNO.HeaderText = "票据号";
            this.InvoiceNO.Name = "InvoiceNO";
            this.InvoiceNO.ReadOnly = true;
            this.InvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvoiceNO.Width = 70;
            // 
            // totalFee
            // 
            this.totalFee.DataPropertyName = "totalFee";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            dataGridViewCellStyle15.NullValue = null;
            this.totalFee.DefaultCellStyle = dataGridViewCellStyle15;
            this.totalFee.HeaderText = "总金额";
            this.totalFee.Name = "totalFee";
            this.totalFee.ReadOnly = true;
            this.totalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalFee.Width = 60;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.status.DefaultCellStyle = dataGridViewCellStyle16;
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.status.Width = 50;
            // 
            // patname
            // 
            this.patname.DataPropertyName = "patname";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.patname.DefaultCellStyle = dataGridViewCellStyle17;
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.patname.Width = 70;
            // 
            // presDeptName
            // 
            this.presDeptName.DataPropertyName = "presDeptName";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.presDeptName.DefaultCellStyle = dataGridViewCellStyle18;
            this.presDeptName.HeaderText = "开方科室";
            this.presDeptName.Name = "presDeptName";
            this.presDeptName.ReadOnly = true;
            this.presDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.presDeptName.Width = 70;
            // 
            // presEmpName
            // 
            this.presEmpName.DataPropertyName = "presEmpName";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.presEmpName.DefaultCellStyle = dataGridViewCellStyle19;
            this.presEmpName.HeaderText = "开方医生";
            this.presEmpName.Name = "presEmpName";
            this.presEmpName.ReadOnly = true;
            this.presEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.presEmpName.Width = 70;
            // 
            // ExecDeptName
            // 
            this.ExecDeptName.DataPropertyName = "ExecDeptName";
            this.ExecDeptName.HeaderText = "执行科室";
            this.ExecDeptName.Name = "ExecDeptName";
            this.ExecDeptName.ReadOnly = true;
            this.ExecDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // costdate
            // 
            this.costdate.DataPropertyName = "ChargeDate";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.Format = "yyyy-MM-dd HH:mm:ss";
            this.costdate.DefaultCellStyle = dataGridViewCellStyle20;
            this.costdate.HeaderText = "收费日期";
            this.costdate.Name = "costdate";
            this.costdate.ReadOnly = true;
            this.costdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.costdate.Width = 130;
            // 
            // ChargeEmpName
            // 
            this.ChargeEmpName.DataPropertyName = "ChargeEmpName";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ChargeEmpName.DefaultCellStyle = dataGridViewCellStyle21;
            this.ChargeEmpName.HeaderText = "收费员";
            this.ChargeEmpName.Name = "ChargeEmpName";
            this.ChargeEmpName.ReadOnly = true;
            this.ChargeEmpName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ChargeEmpName.Width = 70;
            // 
            // FeeItemHeadID
            // 
            this.FeeItemHeadID.DataPropertyName = "FeeItemHeadID";
            this.FeeItemHeadID.HeaderText = "FeeItemHeadID";
            this.FeeItemHeadID.Name = "FeeItemHeadID";
            this.FeeItemHeadID.ReadOnly = true;
            this.FeeItemHeadID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FeeItemHeadID.Visible = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.chkRefund);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.txtCardNo);
            this.panelEx2.Controls.Add(this.chkCardNO);
            this.panelEx2.Controls.Add(this.txtPatName);
            this.panelEx2.Controls.Add(this.chkPatName);
            this.panelEx2.Controls.Add(this.cmbPatType);
            this.panelEx2.Controls.Add(this.chkPayType);
            this.panelEx2.Controls.Add(this.txtChareEmp);
            this.panelEx2.Controls.Add(this.chkChareEmp);
            this.panelEx2.Controls.Add(this.txtInvoiceNO);
            this.panelEx2.Controls.Add(this.chkInvoiceNO);
            this.panelEx2.Controls.Add(this.sdtCostDate);
            this.panelEx2.Controls.Add(this.chkCostDate);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1276, 66);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // chkRefund
            // 
            // 
            // 
            // 
            this.chkRefund.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkRefund.Location = new System.Drawing.Point(752, 31);
            this.chkRefund.Name = "chkRefund";
            this.chkRefund.Size = new System.Drawing.Size(76, 23);
            this.chkRefund.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkRefund.TabIndex = 28;
            this.chkRefund.Text = "只查退费";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1052, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(970, 30);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 25;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtCardNo
            // 
            // 
            // 
            // 
            this.txtCardNo.Border.Class = "TextBoxBorder";
            this.txtCardNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCardNo.Location = new System.Drawing.Point(574, 33);
            this.txtCardNo.Name = "txtCardNo";
            this.txtCardNo.Size = new System.Drawing.Size(156, 21);
            this.txtCardNo.TabIndex = 24;
            // 
            // chkCardNO
            // 
            // 
            // 
            // 
            this.chkCardNO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkCardNO.Location = new System.Drawing.Point(498, 32);
            this.chkCardNO.Name = "chkCardNO";
            this.chkCardNO.Size = new System.Drawing.Size(78, 23);
            this.chkCardNO.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkCardNO.TabIndex = 23;
            this.chkCardNO.Text = "会员卡号";
            // 
            // txtPatName
            // 
            // 
            // 
            // 
            this.txtPatName.Border.Class = "TextBoxBorder";
            this.txtPatName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPatName.Location = new System.Drawing.Point(339, 31);
            this.txtPatName.Name = "txtPatName";
            this.txtPatName.Size = new System.Drawing.Size(153, 21);
            this.txtPatName.TabIndex = 22;
            // 
            // chkPatName
            // 
            // 
            // 
            // 
            this.chkPatName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPatName.Location = new System.Drawing.Point(249, 31);
            this.chkPatName.Name = "chkPatName";
            this.chkPatName.Size = new System.Drawing.Size(78, 23);
            this.chkPatName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPatName.TabIndex = 21;
            this.chkPatName.Text = "病人姓名";
            // 
            // cmbPatType
            // 
            this.cmbPatType.DisplayMember = "Text";
            this.cmbPatType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatType.FormattingEnabled = true;
            this.cmbPatType.ItemHeight = 15;
            this.cmbPatType.Location = new System.Drawing.Point(78, 32);
            this.cmbPatType.Name = "cmbPatType";
            this.cmbPatType.Size = new System.Drawing.Size(168, 21);
            this.cmbPatType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPatType.TabIndex = 20;
            // 
            // chkPayType
            // 
            // 
            // 
            // 
            this.chkPayType.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPayType.Location = new System.Drawing.Point(3, 32);
            this.chkPayType.Name = "chkPayType";
            this.chkPayType.Size = new System.Drawing.Size(78, 23);
            this.chkPayType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPayType.TabIndex = 19;
            this.chkPayType.Text = "病人类型";
            // 
            // txtChareEmp
            // 
            // 
            // 
            // 
            this.txtChareEmp.Border.Class = "TextBoxBorder";
            this.txtChareEmp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChareEmp.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtChareEmp.ButtonCustom.Image")));
            this.txtChareEmp.ButtonCustom.Visible = true;
            this.txtChareEmp.CardColumn = null;
            this.txtChareEmp.DisplayField = "";
            this.txtChareEmp.IsEnterShowCard = true;
            this.txtChareEmp.IsNumSelected = false;
            this.txtChareEmp.IsPage = true;
            this.txtChareEmp.IsShowLetter = false;
            this.txtChareEmp.IsShowPage = false;
            this.txtChareEmp.IsShowSeq = true;
            this.txtChareEmp.Location = new System.Drawing.Point(819, 4);
            this.txtChareEmp.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtChareEmp.MemberField = "";
            this.txtChareEmp.MemberValue = null;
            this.txtChareEmp.Name = "txtChareEmp";
            this.txtChareEmp.QueryFields = new string[] {
        ""};
            this.txtChareEmp.QueryFieldsString = "";
            this.txtChareEmp.SelectedValue = null;
            this.txtChareEmp.ShowCardColumns = null;
            this.txtChareEmp.ShowCardDataSource = null;
            this.txtChareEmp.ShowCardHeight = 0;
            this.txtChareEmp.ShowCardWidth = 0;
            this.txtChareEmp.Size = new System.Drawing.Size(128, 21);
            this.txtChareEmp.TabIndex = 8;
            // 
            // chkChareEmp
            // 
            // 
            // 
            // 
            this.chkChareEmp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkChareEmp.Location = new System.Drawing.Point(752, 2);
            this.chkChareEmp.Name = "chkChareEmp";
            this.chkChareEmp.Size = new System.Drawing.Size(63, 23);
            this.chkChareEmp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkChareEmp.TabIndex = 7;
            this.chkChareEmp.Text = "收费员";
            // 
            // txtInvoiceNO
            // 
            // 
            // 
            // 
            this.txtInvoiceNO.Border.Class = "TextBoxBorder";
            this.txtInvoiceNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInvoiceNO.Location = new System.Drawing.Point(573, 4);
            this.txtInvoiceNO.Name = "txtInvoiceNO";
            this.txtInvoiceNO.Size = new System.Drawing.Size(157, 21);
            this.txtInvoiceNO.TabIndex = 6;
            // 
            // chkInvoiceNO
            // 
            // 
            // 
            // 
            this.chkInvoiceNO.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkInvoiceNO.Location = new System.Drawing.Point(498, 3);
            this.chkInvoiceNO.Name = "chkInvoiceNO";
            this.chkInvoiceNO.Size = new System.Drawing.Size(69, 23);
            this.chkInvoiceNO.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkInvoiceNO.TabIndex = 5;
            this.chkInvoiceNO.Text = "发票号";
            // 
            // sdtCostDate
            // 
            this.sdtCostDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtCostDate.DateFormat = "yyyy-MM-dd HH:mm:ss";
            this.sdtCostDate.DateWidth = 120;
            this.sdtCostDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtCostDate.Location = new System.Drawing.Point(78, 5);
            this.sdtCostDate.Name = "sdtCostDate";
            this.sdtCostDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtCostDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtCostDate.Size = new System.Drawing.Size(414, 21);
            this.sdtCostDate.TabIndex = 4;
            // 
            // chkCostDate
            // 
            // 
            // 
            // 
            this.chkCostDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkCostDate.Checked = true;
            this.chkCostDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCostDate.CheckValue = "Y";
            this.chkCostDate.Enabled = false;
            this.chkCostDate.Location = new System.Drawing.Point(3, 3);
            this.chkCostDate.Name = "chkCostDate";
            this.chkCostDate.Size = new System.Drawing.Size(78, 23);
            this.chkCostDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkCostDate.TabIndex = 3;
            this.chkCostDate.Text = "收费时间";
            this.chkCostDate.TextColor = System.Drawing.Color.DarkViolet;
            // 
            // FrmRecipteQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 532);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmRecipteQuery";
            this.Text = "处方查询";
            this.OpenWindowBefore += new System.EventHandler(this.FrmRecipteQuery_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetail)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgQueryData)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private EfwControls.CustomControl.DataGrid dgQueryData;
        private EfwControls.CustomControl.StatDateTime sdtCostDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCostDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkInvoiceNO;
        private EfwControls.CustomControl.TextBoxCard txtChareEmp;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkChareEmp;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInvoiceNO;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCardNo;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkCardNO;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPatName;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPatName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPatType;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPayType;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRefund;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.DataGrid dgDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PresAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private DevComponents.DotNetBar.Controls.DataGridViewCheckBoxXColumn selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn costHeadid;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
        private System.Windows.Forms.DataGridViewTextBoxColumn presDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn presEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn costdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChargeEmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeItemHeadID;
    }
}