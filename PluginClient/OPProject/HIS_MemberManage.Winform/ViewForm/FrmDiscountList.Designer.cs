namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmDiscountList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtCardCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.statRegDate = new EfwControls.CustomControl.StatDateTime();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.cbbCardType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbbWork = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgDiscountList = new EfwControls.CustomControl.DataGrid();
            this.MemberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SettlementNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatientTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromTypeDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDiscountList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.txtCardCode);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.statRegDate);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.cbbCardType);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.cbbWork);
            this.panelEx1.Controls.Add(this.labelX11);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1206, 41);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
            // 
            // txtCardCode
            // 
            // 
            // 
            // 
            this.txtCardCode.Border.Class = "TextBoxBorder";
            this.txtCardCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCardCode.Location = new System.Drawing.Point(908, 13);
            this.txtCardCode.Name = "txtCardCode";
            this.txtCardCode.Size = new System.Drawing.Size(100, 21);
            this.txtCardCode.TabIndex = 37;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(846, 16);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 36;
            this.labelX3.Text = "帐户号码";
            // 
            // statRegDate
            // 
            this.statRegDate.BackColor = System.Drawing.Color.Transparent;
            this.statRegDate.DateFormat = "yyyy-MM-dd";
            this.statRegDate.DateWidth = 120;
            this.statRegDate.Font = new System.Drawing.Font("宋体", 9.5F);
            this.statRegDate.Location = new System.Drawing.Point(71, 12);
            this.statRegDate.Margin = new System.Windows.Forms.Padding(2);
            this.statRegDate.Name = "statRegDate";
            this.statRegDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.statRegDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.statRegDate.Size = new System.Drawing.Size(284, 22);
            this.statRegDate.TabIndex = 35;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(10, 15);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 34;
            this.labelX2.Text = "消费时间";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(1100, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 21);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "关闭（&C）";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Location = new System.Drawing.Point(1017, 12);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 21);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 32;
            this.btnQuery.Text = "查询（&Q）";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // cbbCardType
            // 
            this.cbbCardType.DisplayMember = "Text";
            this.cbbCardType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCardType.FormattingEnabled = true;
            this.cbbCardType.ItemHeight = 15;
            this.cbbCardType.Location = new System.Drawing.Point(678, 13);
            this.cbbCardType.Name = "cbbCardType";
            this.cbbCardType.Size = new System.Drawing.Size(161, 21);
            this.cbbCardType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbCardType.TabIndex = 30;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(616, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 31;
            this.labelX1.Text = "帐户类型";
            // 
            // cbbWork
            // 
            this.cbbWork.DisplayMember = "Text";
            this.cbbWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWork.FormattingEnabled = true;
            this.cbbWork.ItemHeight = 15;
            this.cbbWork.Location = new System.Drawing.Point(424, 13);
            this.cbbWork.Name = "cbbWork";
            this.cbbWork.Size = new System.Drawing.Size(186, 21);
            this.cbbWork.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbWork.TabIndex = 28;
            this.cbbWork.SelectedValueChanged += new System.EventHandler(this.CbbWork_SelectedValueChanged);
            // 
            // labelX11
            // 
            this.labelX11.AutoSize = true;
            this.labelX11.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX11.Location = new System.Drawing.Point(362, 14);
            this.labelX11.Name = "labelX11";
            this.labelX11.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX11.Size = new System.Drawing.Size(56, 18);
            this.labelX11.TabIndex = 29;
            this.labelX11.Text = "机构名称";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.dgDiscountList);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 41);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(1206, 485);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 5;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "优惠明细记录列表";
            // 
            // dgDiscountList
            // 
            this.dgDiscountList.AllowSortWhenClickColumnHeader = false;
            this.dgDiscountList.AllowUserToAddRows = false;
            this.dgDiscountList.AllowUserToDeleteRows = false;
            this.dgDiscountList.AllowUserToResizeColumns = false;
            this.dgDiscountList.AllowUserToResizeRows = false;
            this.dgDiscountList.BackgroundColor = System.Drawing.Color.White;
            this.dgDiscountList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDiscountList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDiscountList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDiscountList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MemberName,
            this.CardTypeName,
            this.CardNO,
            this.OperateDate,
            this.SettlementNO,
            this.PatientTypeDesc,
            this.PatTypeName,
            this.PromTypeDesc,
            this.StatName,
            this.ItemName,
            this.Amount,
            this.DiscountTotal});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDiscountList.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgDiscountList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDiscountList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDiscountList.HighlightSelectedColumnHeaders = false;
            this.dgDiscountList.Location = new System.Drawing.Point(0, 26);
            this.dgDiscountList.MultiSelect = false;
            this.dgDiscountList.Name = "dgDiscountList";
            this.dgDiscountList.ReadOnly = true;
            this.dgDiscountList.RowHeadersWidth = 30;
            this.dgDiscountList.RowTemplate.Height = 23;
            this.dgDiscountList.SelectAllSignVisible = false;
            this.dgDiscountList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDiscountList.SeqVisible = true;
            this.dgDiscountList.SetCustomStyle = false;
            this.dgDiscountList.Size = new System.Drawing.Size(1206, 459);
            this.dgDiscountList.TabIndex = 1;
            // 
            // MemberName
            // 
            this.MemberName.DataPropertyName = "MemberName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MemberName.DefaultCellStyle = dataGridViewCellStyle2;
            this.MemberName.HeaderText = "会员姓名";
            this.MemberName.MinimumWidth = 100;
            this.MemberName.Name = "MemberName";
            this.MemberName.ReadOnly = true;
            this.MemberName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CardTypeName
            // 
            this.CardTypeName.DataPropertyName = "CardTypeName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CardTypeName.DefaultCellStyle = dataGridViewCellStyle3;
            this.CardTypeName.HeaderText = "帐户类型";
            this.CardTypeName.MinimumWidth = 120;
            this.CardTypeName.Name = "CardTypeName";
            this.CardTypeName.ReadOnly = true;
            this.CardTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardTypeName.Width = 120;
            // 
            // CardNO
            // 
            this.CardNO.DataPropertyName = "CardNO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CardNO.DefaultCellStyle = dataGridViewCellStyle4;
            this.CardNO.HeaderText = "帐户号";
            this.CardNO.MinimumWidth = 120;
            this.CardNO.Name = "CardNO";
            this.CardNO.ReadOnly = true;
            this.CardNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CardNO.Width = 120;
            // 
            // OperateDate
            // 
            this.OperateDate.DataPropertyName = "OperateDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.OperateDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.OperateDate.HeaderText = "消费日期";
            this.OperateDate.MinimumWidth = 140;
            this.OperateDate.Name = "OperateDate";
            this.OperateDate.ReadOnly = true;
            this.OperateDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperateDate.Width = 140;
            // 
            // SettlementNO
            // 
            this.SettlementNO.DataPropertyName = "SettlementNO";
            this.SettlementNO.HeaderText = "结算号";
            this.SettlementNO.MinimumWidth = 100;
            this.SettlementNO.Name = "SettlementNO";
            this.SettlementNO.ReadOnly = true;
            this.SettlementNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PatientTypeDesc
            // 
            this.PatientTypeDesc.DataPropertyName = "PatientTypeDesc";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PatientTypeDesc.DefaultCellStyle = dataGridViewCellStyle6;
            this.PatientTypeDesc.HeaderText = "病人类型";
            this.PatientTypeDesc.MinimumWidth = 80;
            this.PatientTypeDesc.Name = "PatientTypeDesc";
            this.PatientTypeDesc.ReadOnly = true;
            this.PatientTypeDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PatTypeName
            // 
            this.PatTypeName.DataPropertyName = "PatTypeName";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PatTypeName.DefaultCellStyle = dataGridViewCellStyle7;
            this.PatTypeName.HeaderText = "费用类型";
            this.PatTypeName.MinimumWidth = 140;
            this.PatTypeName.Name = "PatTypeName";
            this.PatTypeName.ReadOnly = true;
            this.PatTypeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatTypeName.Width = 140;
            // 
            // PromTypeDesc
            // 
            this.PromTypeDesc.DataPropertyName = "PromTypeDesc";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PromTypeDesc.DefaultCellStyle = dataGridViewCellStyle8;
            this.PromTypeDesc.HeaderText = "优惠类型";
            this.PromTypeDesc.MinimumWidth = 80;
            this.PromTypeDesc.Name = "PromTypeDesc";
            this.PromTypeDesc.ReadOnly = true;
            this.PromTypeDesc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StatName
            // 
            this.StatName.DataPropertyName = "StatName";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.StatName.DefaultCellStyle = dataGridViewCellStyle9;
            this.StatName.HeaderText = "优惠类别";
            this.StatName.MinimumWidth = 140;
            this.StatName.Name = "StatName";
            this.StatName.ReadOnly = true;
            this.StatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StatName.Width = 140;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle10;
            this.ItemName.HeaderText = "优惠项目";
            this.ItemName.MinimumWidth = 120;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle11;
            this.Amount.HeaderText = "消费金额";
            this.Amount.MinimumWidth = 140;
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 140;
            // 
            // DiscountTotal
            // 
            this.DiscountTotal.DataPropertyName = "DiscountTotal";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountTotal.DefaultCellStyle = dataGridViewCellStyle12;
            this.DiscountTotal.HeaderText = "优惠金额";
            this.DiscountTotal.MinimumWidth = 140;
            this.DiscountTotal.Name = "DiscountTotal";
            this.DiscountTotal.ReadOnly = true;
            this.DiscountTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiscountTotal.Width = 140;
            // 
            // FrmDiscountList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1206, 526);
            this.Controls.Add(this.expandablePanel1);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmDiscountList";
            this.Text = "优惠明细";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDiscountList_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDiscountList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbCardType;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbWork;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCardCode;
        private DevComponents.DotNetBar.LabelX labelX3;
        private EfwControls.CustomControl.StatDateTime statRegDate;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private EfwControls.CustomControl.DataGrid dgDiscountList;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SettlementNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatientTypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromTypeDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountTotal;
    }
}