namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmOutStoreDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard3 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard4 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutStoreDetail));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStatus = new DevComponents.DotNetBar.PanelEx();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btnReadInStore = new DevComponents.DotNetBar.ButtonItem();
            this.btnApply = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtLostReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbLostReason = new DevComponents.DotNetBar.LabelX();
            this.timeOutData = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cmbOpType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtDept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.barHead = new DevComponents.DotNetBar.Bar();
            this.btnSaveBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnNewBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.frmCommon = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOutData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Controls.Add(this.barHead);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1243, 337);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 2;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgDetails);
            this.panelEx3.Controls.Add(this.pnlStatus);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx3.Location = new System.Drawing.Point(0, 90);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1243, 247);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            this.panelEx3.Text = "panelEx3";
            // 
            // dgDetails
            // 
            this.dgDetails.AllowSortWhenClickColumnHeader = false;
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.BatchNO,
            this.pAmount,
            this.PackUnitName,
            this.uAmount,
            this.UnitName,
            this.StockPrice,
            this.StockFee,
            this.RetailPrice,
            this.RetailFee,
            this.totalNum,
            this.factAmount});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetails.HideSelectionCardWhenCustomInput = false;
            this.dgDetails.HighlightSelectedColumnHeaders = false;
            this.dgDetails.IsInputNumSelectedCard = false;
            this.dgDetails.IsShowLetter = false;
            this.dgDetails.IsShowPage = false;
            this.dgDetails.Location = new System.Drawing.Point(0, 0);
            this.dgDetails.MultiSelect = false;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersWidth = 25;
            this.dgDetails.RowTemplate.Height = 23;
            this.dgDetails.SelectAllSignVisible = false;
            dataGridViewSelectionCard3.BindColumnIndex = 0;
            dataGridViewSelectionCard3.CardColumn = null;
            dataGridViewSelectionCard3.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard3.DataSource = null;
            dataGridViewSelectionCard3.FilterResult = null;
            dataGridViewSelectionCard3.IsPage = true;
            dataGridViewSelectionCard3.Memo = null;
            dataGridViewSelectionCard3.PageTotalRecord = 0;
            dataGridViewSelectionCard3.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard3.QueryFieldsString = "";
            dataGridViewSelectionCard3.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard3.ShowCardColumns = null;
            dataGridViewSelectionCard4.BindColumnIndex = 0;
            dataGridViewSelectionCard4.CardColumn = null;
            dataGridViewSelectionCard4.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard4.DataSource = null;
            dataGridViewSelectionCard4.FilterResult = null;
            dataGridViewSelectionCard4.IsPage = true;
            dataGridViewSelectionCard4.Memo = null;
            dataGridViewSelectionCard4.PageTotalRecord = 0;
            dataGridViewSelectionCard4.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard4.QueryFieldsString = "";
            dataGridViewSelectionCard4.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard4.ShowCardColumns = null;
            this.dgDetails.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard3,
        dataGridViewSelectionCard4};
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.SelectionNumKeyBoards = null;
            this.dgDetails.SeqVisible = true;
            this.dgDetails.SetCustomStyle = false;
            this.dgDetails.Size = new System.Drawing.Size(1243, 217);
            this.dgDetails.TabIndex = 0;
            this.dgDetails.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgDetails_SelectCardRowSelected);
            this.dgDetails.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dgDetails_DataGridViewCellPressEnterKey);
            this.dgDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellValueChanged);
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编号";
            this.DrugID.Name = "DrugID";
            this.DrugID.ReadOnly = true;
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 50;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "化学名称";
            this.ChemName.Name = "ChemName";
            this.ChemName.ReadOnly = true;
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.ReadOnly = true;
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchNO.Width = 90;
            // 
            // pAmount
            // 
            this.pAmount.DataPropertyName = "pAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.pAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.pAmount.HeaderText = "包装数量";
            this.pAmount.MinimumWidth = 30;
            this.pAmount.Name = "pAmount";
            this.pAmount.ReadOnly = true;
            this.pAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pAmount.Width = 60;
            // 
            // PackUnitName
            // 
            this.PackUnitName.DataPropertyName = "PackUnitName";
            this.PackUnitName.HeaderText = "包装单位";
            this.PackUnitName.MinimumWidth = 30;
            this.PackUnitName.Name = "PackUnitName";
            this.PackUnitName.ReadOnly = true;
            this.PackUnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnitName.Width = 40;
            // 
            // uAmount
            // 
            this.uAmount.DataPropertyName = "uAmount";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.uAmount.DefaultCellStyle = dataGridViewCellStyle14;
            this.uAmount.HeaderText = "基本数量";
            this.uAmount.MinimumWidth = 30;
            this.uAmount.Name = "uAmount";
            this.uAmount.ReadOnly = true;
            this.uAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.uAmount.Width = 60;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "基本单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 40;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle15;
            this.StockPrice.HeaderText = "进货金额";
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.ReadOnly = true;
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 65;
            // 
            // StockFee
            // 
            this.StockFee.DataPropertyName = "StockFee";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "0.00";
            this.StockFee.DefaultCellStyle = dataGridViewCellStyle16;
            this.StockFee.HeaderText = "进货价";
            this.StockFee.Name = "StockFee";
            this.StockFee.ReadOnly = true;
            this.StockFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockFee.Width = 55;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle17;
            this.RetailPrice.HeaderText = "零售金额";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 65;
            // 
            // RetailFee
            // 
            this.RetailFee.DataPropertyName = "RetailFee";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "0.00";
            this.RetailFee.DefaultCellStyle = dataGridViewCellStyle18;
            this.RetailFee.HeaderText = "零售价";
            this.RetailFee.Name = "RetailFee";
            this.RetailFee.ReadOnly = true;
            this.RetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailFee.Width = 55;
            // 
            // totalNum
            // 
            this.totalNum.DataPropertyName = "totalNum";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.totalNum.DefaultCellStyle = dataGridViewCellStyle19;
            this.totalNum.HeaderText = "库存量";
            this.totalNum.Name = "totalNum";
            this.totalNum.ReadOnly = true;
            this.totalNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalNum.Width = 65;
            // 
            // factAmount
            // 
            this.factAmount.DataPropertyName = "factAmount";
            this.factAmount.HeaderText = "申请数";
            this.factAmount.Name = "factAmount";
            this.factAmount.ReadOnly = true;
            this.factAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.factAmount.Visible = false;
            // 
            // pnlStatus
            // 
            this.pnlStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 217);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1243, 30);
            this.pnlStatus.Style.Alignment = System.Drawing.StringAlignment.Far;
            this.pnlStatus.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlStatus.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlStatus.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlStatus.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlStatus.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlStatus.Style.GradientAngle = 90;
            this.pnlStatus.TabIndex = 2;
            this.pnlStatus.Text = "合计零售金额：0.00  合计进货金额：0.00";
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddDetail,
            this.btnDelete,
            this.btnReadInStore,
            this.btnApply});
            this.bar1.Location = new System.Drawing.Point(0, 65);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(1243, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 88;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnAddDetail
            // 
            this.btnAddDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAddDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDetail.Image")));
            this.btnAddDetail.Name = "btnAddDetail";
            this.btnAddDetail.Text = "新增明细(&N)";
            this.btnAddDetail.Click += new System.EventHandler(this.btnAddDetail_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "删除明细(&D)";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnReadInStore
            // 
            this.btnReadInStore.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnReadInStore.Image = ((System.Drawing.Image)(resources.GetObject("btnReadInStore.Image")));
            this.btnReadInStore.Name = "btnReadInStore";
            this.btnReadInStore.Text = "读取入库单数据(&R)";
            this.btnReadInStore.Click += new System.EventHandler(this.btnReadInStore_Click);
            // 
            // btnApply
            // 
            this.btnApply.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnApply.Image = ((System.Drawing.Image)(resources.GetObject("btnApply.Image")));
            this.btnApply.Name = "btnApply";
            this.btnApply.Text = "加载领药申请单(&A)";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtLostReason);
            this.panelEx2.Controls.Add(this.lbLostReason);
            this.panelEx2.Controls.Add(this.timeOutData);
            this.panelEx2.Controls.Add(this.cmbOpType);
            this.panelEx2.Controls.Add(this.txtDept);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.txtRemark);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX8);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 25);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1243, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // txtLostReason
            // 
            // 
            // 
            // 
            this.txtLostReason.Border.Class = "TextBoxBorder";
            this.txtLostReason.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLostReason.Location = new System.Drawing.Point(974, 10);
            this.txtLostReason.Name = "txtLostReason";
            this.txtLostReason.Size = new System.Drawing.Size(256, 21);
            this.txtLostReason.TabIndex = 85;
            // 
            // lbLostReason
            // 
            this.lbLostReason.AutoSize = true;
            // 
            // 
            // 
            this.lbLostReason.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbLostReason.Location = new System.Drawing.Point(914, 11);
            this.lbLostReason.Name = "lbLostReason";
            this.lbLostReason.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lbLostReason.Size = new System.Drawing.Size(56, 18);
            this.lbLostReason.TabIndex = 86;
            this.lbLostReason.Text = "报损原因";
            // 
            // timeOutData
            // 
            // 
            // 
            // 
            this.timeOutData.BackgroundStyle.Class = "DateTimeInputBackground";
            this.timeOutData.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.timeOutData.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.timeOutData.ButtonDropDown.Visible = true;
            this.timeOutData.CustomFormat = "yyyy-MM-dd";
            this.timeOutData.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.timeOutData.IsPopupCalendarOpen = false;
            this.timeOutData.Location = new System.Drawing.Point(272, 10);
            // 
            // 
            // 
            this.timeOutData.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.timeOutData.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.timeOutData.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.timeOutData.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.timeOutData.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.timeOutData.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.timeOutData.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.timeOutData.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.timeOutData.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.timeOutData.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.timeOutData.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.timeOutData.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.timeOutData.MonthCalendar.TodayButtonVisible = true;
            this.timeOutData.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.timeOutData.Name = "timeOutData";
            this.timeOutData.Size = new System.Drawing.Size(160, 21);
            this.timeOutData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.timeOutData.TabIndex = 1;
            // 
            // cmbOpType
            // 
            this.cmbOpType.DisplayMember = "Text";
            this.cmbOpType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpType.FormattingEnabled = true;
            this.cmbOpType.ItemHeight = 15;
            this.cmbOpType.Location = new System.Drawing.Point(81, 10);
            this.cmbOpType.Name = "cmbOpType";
            this.cmbOpType.Size = new System.Drawing.Size(110, 21);
            this.cmbOpType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbOpType.TabIndex = 0;
            this.cmbOpType.SelectedIndexChanged += new System.EventHandler(this.cmbOpType_SelectedIndexChanged);
            // 
            // txtDept
            // 
            // 
            // 
            // 
            this.txtDept.Border.Class = "TextBoxBorder";
            this.txtDept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtDept.ButtonCustom.Image")));
            this.txtDept.ButtonCustom.Visible = true;
            this.txtDept.CardColumn = null;
            this.txtDept.DisplayField = "";
            this.txtDept.IsEnterShowCard = true;
            this.txtDept.IsNumSelected = false;
            this.txtDept.IsPage = true;
            this.txtDept.IsShowLetter = false;
            this.txtDept.IsShowPage = false;
            this.txtDept.IsShowSeq = true;
            this.txtDept.Location = new System.Drawing.Point(513, 10);
            this.txtDept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtDept.MemberField = "";
            this.txtDept.MemberValue = null;
            this.txtDept.Name = "txtDept";
            this.txtDept.QueryFields = new string[] {
        ""};
            this.txtDept.QueryFieldsString = "";
            this.txtDept.SelectedValue = null;
            this.txtDept.ShowCardColumns = null;
            this.txtDept.ShowCardDataSource = null;
            this.txtDept.ShowCardHeight = 0;
            this.txtDept.ShowCardWidth = 0;
            this.txtDept.Size = new System.Drawing.Size(120, 21);
            this.txtDept.TabIndex = 2;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX3.Location = new System.Drawing.Point(454, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 84;
            this.labelX3.Text = "往来科室";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX1.Location = new System.Drawing.Point(22, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.SingleLineColor = System.Drawing.Color.Magenta;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 82;
            this.labelX1.Text = "业务类型";
            // 
            // txtRemark
            // 
            // 
            // 
            // 
            this.txtRemark.Border.Class = "TextBoxBorder";
            this.txtRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRemark.Location = new System.Drawing.Point(689, 10);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(200, 21);
            this.txtRemark.TabIndex = 3;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRemark_KeyDown);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX2.Location = new System.Drawing.Point(213, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "出库日期";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(655, 11);
            this.labelX8.Name = "labelX8";
            this.labelX8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX8.Size = new System.Drawing.Size(31, 18);
            this.labelX8.TabIndex = 59;
            this.labelX8.Text = "备注";
            // 
            // barHead
            // 
            this.barHead.AntiAlias = true;
            this.barHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.barHead.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.barHead.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.barHead.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveBill,
            this.btnNewBill,
            this.btnClose});
            this.barHead.Location = new System.Drawing.Point(0, 0);
            this.barHead.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barHead.Name = "barHead";
            this.barHead.PaddingLeft = 22;
            this.barHead.Size = new System.Drawing.Size(1243, 25);
            this.barHead.Stretch = true;
            this.barHead.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barHead.TabIndex = 87;
            this.barHead.TabStop = false;
            this.barHead.Text = "bar2";
            // 
            // btnSaveBill
            // 
            this.btnSaveBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnSaveBill.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveBill.Image")));
            this.btnSaveBill.Name = "btnSaveBill";
            this.btnSaveBill.Text = "保存单据(F2)";
            this.btnSaveBill.Click += new System.EventHandler(this.btnSaveBill_Click);
            // 
            // btnNewBill
            // 
            this.btnNewBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNewBill.Image = ((System.Drawing.Image)(resources.GetObject("btnNewBill.Image")));
            this.btnNewBill.Name = "btnNewBill";
            this.btnNewBill.Text = "新增单据(F3)";
            this.btnNewBill.Click += new System.EventHandler(this.btnNewBill_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭窗体(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmCommon
            // 
            this.frmCommon.IsSkip = true;
            // 
            // FrmOutStoreDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 337);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmOutStoreDetail";
            this.Text = "出库单编辑";
            this.OpenWindowBefore += new System.EventHandler(this.FrmOutStoreDetail_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOutStoreDetail_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOutData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRemark;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput timeOutData;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.TextBoxCard txtDept;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbOpType;
        private DevComponents.DotNetBar.Bar barHead;
        private DevComponents.DotNetBar.ButtonItem btnSaveBill;
        private DevComponents.DotNetBar.ButtonItem btnNewBill;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddDetail;
        private DevComponents.DotNetBar.ButtonItem btnDelete;
        private DevComponents.DotNetBar.ButtonItem btnReadInStore;
        private EfwControls.CustomControl.frmForm frmCommon;
        private DevComponents.DotNetBar.PanelEx pnlStatus;
        private DevComponents.DotNetBar.ButtonItem btnApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn uAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn factAmount;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLostReason;
        private DevComponents.DotNetBar.LabelX lbLostReason;
    }
}