namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmInStoreDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard2 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInStoreDetail));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStatus = new DevComponents.DotNetBar.PanelEx();
            this.barDetail = new DevComponents.DotNetBar.Bar();
            this.btnNewDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefreshDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnLoadBuyBill = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.cmbOpType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtpBillDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.txtSupport = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtInvoiceNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDeliveryNO = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.dtpInvoiceDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.barHead = new DevComponents.DotNetBar.Bar();
            this.btnSaveBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnNewBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresth = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.frmFormInHead = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDetail)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1084, 662);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.Controls.Add(this.barDetail);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 65);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1084, 597);
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
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.dgDetails);
            this.panelEx4.Controls.Add(this.pnlStatus);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 25);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1084, 572);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 10;
            this.panelEx4.Text = "panelEx4";
            // 
            // dgDetails
            // 
            this.dgDetails.AllowSortWhenClickColumnHeader = false;
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.ColumnHeadersHeight = 30;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.BatchNO,
            this.ValidityDate,
            this.pAmount,
            this.PackUnitName,
            this.uAmount,
            this.UnitName,
            this.StockPrice,
            this.StockFee,
            this.RetailPrice,
            this.RetailFee});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetails.EnableHeadersVisualStyles = false;
            this.dgDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetails.HideSelectionCardWhenCustomInput = false;
            this.dgDetails.HighlightSelectedColumnHeaders = false;
            this.dgDetails.IsInputNumSelectedCard = false;
            this.dgDetails.IsShowLetter = false;
            this.dgDetails.IsShowPage = false;
            this.dgDetails.Location = new System.Drawing.Point(0, 0);
            this.dgDetails.Name = "dgDetails";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgDetails.RowHeadersWidth = 25;
            this.dgDetails.RowTemplate.Height = 23;
            this.dgDetails.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 0;
            dataGridViewSelectionCard1.CardColumn = null;
            dataGridViewSelectionCard1.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard1.DataSource = null;
            dataGridViewSelectionCard1.FilterResult = null;
            dataGridViewSelectionCard1.IsPage = true;
            dataGridViewSelectionCard1.Memo = null;
            dataGridViewSelectionCard1.PageTotalRecord = 0;
            dataGridViewSelectionCard1.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard1.QueryFieldsString = "";
            dataGridViewSelectionCard1.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard1.ShowCardColumns = null;
            dataGridViewSelectionCard2.BindColumnIndex = 4;
            dataGridViewSelectionCard2.CardColumn = null;
            dataGridViewSelectionCard2.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard2.DataSource = null;
            dataGridViewSelectionCard2.FilterResult = null;
            dataGridViewSelectionCard2.IsPage = true;
            dataGridViewSelectionCard2.Memo = null;
            dataGridViewSelectionCard2.PageTotalRecord = 0;
            dataGridViewSelectionCard2.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard2.QueryFieldsString = "";
            dataGridViewSelectionCard2.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard2.ShowCardColumns = null;
            this.dgDetails.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1,
        dataGridViewSelectionCard2};
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.SelectionNumKeyBoards = null;
            this.dgDetails.SeqVisible = true;
            this.dgDetails.SetCustomStyle = false;
            this.dgDetails.Size = new System.Drawing.Size(1084, 544);
            this.dgDetails.TabIndex = 0;
            this.dgDetails.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgDetails_SelectCardRowSelected);
            this.dgDetails.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dgDetails_DataGridViewCellPressEnterKey);
            this.dgDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellValueChanged);
            this.dgDetails.CurrentCellChanged += new System.EventHandler(this.dgDetails_CurrentCellChanged);
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编号";
            this.DrugID.Name = "DrugID";
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 65;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "化学名称";
            this.ChemName.Name = "ChemName";
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.DataPropertyName = "ProductName";
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.Name = "ProductName";
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.MaxInputLength = 15;
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchNO.Width = 80;
            // 
            // ValidityDate
            // 
            this.ValidityDate.DataPropertyName = "ValidityDate";
            dataGridViewCellStyle3.Format = "yyyy-MM-dd";
            dataGridViewCellStyle3.NullValue = null;
            this.ValidityDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.ValidityDate.HeaderText = "到效日期";
            this.ValidityDate.Name = "ValidityDate";
            this.ValidityDate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ValidityDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ValidityDate.Width = 90;
            // 
            // pAmount
            // 
            this.pAmount.DataPropertyName = "pAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.pAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.pAmount.HeaderText = "包装数";
            this.pAmount.MaxInputLength = 10;
            this.pAmount.MinimumWidth = 40;
            this.pAmount.Name = "pAmount";
            this.pAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pAmount.Width = 60;
            // 
            // PackUnitName
            // 
            this.PackUnitName.DataPropertyName = "PackUnitName";
            this.PackUnitName.HeaderText = "包装单位";
            this.PackUnitName.MinimumWidth = 40;
            this.PackUnitName.Name = "PackUnitName";
            this.PackUnitName.ReadOnly = true;
            this.PackUnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnitName.Width = 40;
            // 
            // uAmount
            // 
            this.uAmount.DataPropertyName = "uAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.uAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.uAmount.HeaderText = "基本数";
            this.uAmount.MinimumWidth = 40;
            this.uAmount.Name = "uAmount";
            this.uAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.uAmount.Width = 60;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.FillWeight = 40F;
            this.UnitName.HeaderText = "基本单位";
            this.UnitName.MinimumWidth = 40;
            this.UnitName.Name = "UnitName";
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 40;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle6.Format = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.StockPrice.HeaderText = "进货价格";
            this.StockPrice.MaxInputLength = 10;
            this.StockPrice.MinimumWidth = 40;
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 65;
            // 
            // StockFee
            // 
            this.StockFee.DataPropertyName = "StockFee";
            dataGridViewCellStyle7.Format = "0.00";
            this.StockFee.DefaultCellStyle = dataGridViewCellStyle7;
            this.StockFee.HeaderText = "进货金额";
            this.StockFee.Name = "StockFee";
            this.StockFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockFee.Width = 60;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle8.Format = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle8;
            this.RetailPrice.HeaderText = "零售价格";
            this.RetailPrice.MaxInputLength = 10;
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 65;
            // 
            // RetailFee
            // 
            this.RetailFee.DataPropertyName = "RetailFee";
            dataGridViewCellStyle9.Format = "0.00";
            this.RetailFee.DefaultCellStyle = dataGridViewCellStyle9;
            this.RetailFee.HeaderText = "零售金额";
            this.RetailFee.Name = "RetailFee";
            this.RetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailFee.Width = 70;
            // 
            // pnlStatus
            // 
            this.pnlStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 544);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1084, 28);
            this.pnlStatus.Style.Alignment = System.Drawing.StringAlignment.Far;
            this.pnlStatus.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlStatus.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pnlStatus.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlStatus.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pnlStatus.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlStatus.Style.GradientAngle = 90;
            this.pnlStatus.TabIndex = 1;
            this.pnlStatus.Text = "合计零售金额：0.00  合计进货金额：0.00";
            // 
            // barDetail
            // 
            this.barDetail.AntiAlias = true;
            this.barDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDetail.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.barDetail.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.barDetail.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNewDetail,
            this.btnDelDetail,
            this.btnRefreshDetail,
            this.btnLoadBuyBill});
            this.barDetail.Location = new System.Drawing.Point(0, 0);
            this.barDetail.Name = "barDetail";
            this.barDetail.PaddingLeft = 22;
            this.barDetail.Size = new System.Drawing.Size(1084, 25);
            this.barDetail.Stretch = true;
            this.barDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barDetail.TabIndex = 9;
            this.barDetail.TabStop = false;
            this.barDetail.Text = "bar1";
            // 
            // btnNewDetail
            // 
            this.btnNewDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNewDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDetail.Image")));
            this.btnNewDetail.Name = "btnNewDetail";
            this.btnNewDetail.Text = "新增明细(&N)";
            this.btnNewDetail.Click += new System.EventHandler(this.btnNewDetail_Click);
            // 
            // btnDelDetail
            // 
            this.btnDelDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDelDetail.Image")));
            this.btnDelDetail.Name = "btnDelDetail";
            this.btnDelDetail.Text = "删除明细(F4)";
            this.btnDelDetail.Click += new System.EventHandler(this.btnDelDetail_Click);
            // 
            // btnRefreshDetail
            // 
            this.btnRefreshDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefreshDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshDetail.Image")));
            this.btnRefreshDetail.Name = "btnRefreshDetail";
            this.btnRefreshDetail.Text = "刷新网格(F5)";
            this.btnRefreshDetail.Click += new System.EventHandler(this.btnRefreshDetail_Click);
            // 
            // btnLoadBuyBill
            // 
            this.btnLoadBuyBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnLoadBuyBill.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadBuyBill.Image")));
            this.btnLoadBuyBill.Name = "btnLoadBuyBill";
            this.btnLoadBuyBill.Text = "加载采购计划单(F6)";
            this.btnLoadBuyBill.Click += new System.EventHandler(this.btnLoadBuyBill_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx6);
            this.panelEx2.Controls.Add(this.barHead);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1084, 65);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 4;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.cmbOpType);
            this.panelEx6.Controls.Add(this.dtpBillDate);
            this.panelEx6.Controls.Add(this.txtSupport);
            this.panelEx6.Controls.Add(this.labelX6);
            this.panelEx6.Controls.Add(this.labelX1);
            this.panelEx6.Controls.Add(this.txtInvoiceNO);
            this.panelEx6.Controls.Add(this.txtDeliveryNO);
            this.panelEx6.Controls.Add(this.labelX2);
            this.panelEx6.Controls.Add(this.labelX3);
            this.panelEx6.Controls.Add(this.labelX4);
            this.panelEx6.Controls.Add(this.labelX8);
            this.panelEx6.Controls.Add(this.dtpInvoiceDate);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 25);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(1084, 40);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 85;
            // 
            // cmbOpType
            // 
            this.cmbOpType.DisplayMember = "Text";
            this.cmbOpType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbOpType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpType.FormattingEnabled = true;
            this.cmbOpType.ItemHeight = 15;
            this.cmbOpType.Location = new System.Drawing.Point(84, 10);
            this.cmbOpType.Name = "cmbOpType";
            this.cmbOpType.Size = new System.Drawing.Size(83, 21);
            this.cmbOpType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbOpType.TabIndex = 0;
            this.cmbOpType.SelectedIndexChanged += new System.EventHandler(this.cmbOpType_SelectedIndexChanged);
            // 
            // dtpBillDate
            // 
            // 
            // 
            // 
            this.dtpBillDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpBillDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBillDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpBillDate.ButtonDropDown.Visible = true;
            this.dtpBillDate.CustomFormat = "yyyy-MM-dd";
            this.dtpBillDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpBillDate.IsPopupCalendarOpen = false;
            this.dtpBillDate.Location = new System.Drawing.Point(248, 10);
            // 
            // 
            // 
            this.dtpBillDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpBillDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBillDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpBillDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpBillDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBillDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpBillDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpBillDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpBillDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpBillDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpBillDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpBillDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpBillDate.MonthCalendar.TodayButtonVisible = true;
            this.dtpBillDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(110, 21);
            this.dtpBillDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpBillDate.TabIndex = 1;
            // 
            // txtSupport
            // 
            this.txtSupport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSupport.Border.Class = "TextBoxBorder";
            this.txtSupport.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSupport.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtSupport.ButtonCustom.Image")));
            this.txtSupport.ButtonCustom.Visible = true;
            this.txtSupport.CardColumn = null;
            this.txtSupport.DisplayField = "";
            this.txtSupport.IsEnterShowCard = true;
            this.txtSupport.IsNumSelected = false;
            this.txtSupport.IsPage = true;
            this.txtSupport.IsShowLetter = false;
            this.txtSupport.IsShowPage = false;
            this.txtSupport.IsShowSeq = true;
            this.txtSupport.Location = new System.Drawing.Point(940, 10);
            this.txtSupport.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtSupport.MemberField = "";
            this.txtSupport.MemberValue = null;
            this.txtSupport.Name = "txtSupport";
            this.txtSupport.QueryFields = new string[] {
        ""};
            this.txtSupport.QueryFieldsString = "";
            this.txtSupport.SelectedValue = null;
            this.txtSupport.ShowCardColumns = null;
            this.txtSupport.ShowCardDataSource = null;
            this.txtSupport.ShowCardHeight = 0;
            this.txtSupport.ShowCardWidth = 0;
            this.txtSupport.Size = new System.Drawing.Size(129, 21);
            this.txtSupport.TabIndex = 5;
            this.txtSupport.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtSupport_AfterSelectedRow);
            this.txtSupport.Leave += new System.EventHandler(this.txtSupport_Leave);
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX6.Location = new System.Drawing.Point(25, 11);
            this.labelX6.Name = "labelX6";
            this.labelX6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX6.Size = new System.Drawing.Size(56, 18);
            this.labelX6.TabIndex = 80;
            this.labelX6.Text = "业务类型";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX1.Location = new System.Drawing.Point(881, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "供货单位";
            // 
            // txtInvoiceNO
            // 
            // 
            // 
            // 
            this.txtInvoiceNO.Border.Class = "TextBoxBorder";
            this.txtInvoiceNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInvoiceNO.Location = new System.Drawing.Point(618, 10);
            this.txtInvoiceNO.Name = "txtInvoiceNO";
            this.txtInvoiceNO.Size = new System.Drawing.Size(80, 21);
            this.txtInvoiceNO.TabIndex = 3;
            // 
            // txtDeliveryNO
            // 
            // 
            // 
            // 
            this.txtDeliveryNO.Border.Class = "TextBoxBorder";
            this.txtDeliveryNO.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeliveryNO.Location = new System.Drawing.Point(779, 10);
            this.txtDeliveryNO.Name = "txtDeliveryNO";
            this.txtDeliveryNO.Size = new System.Drawing.Size(80, 21);
            this.txtDeliveryNO.TabIndex = 4;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.DarkViolet;
            this.labelX2.Location = new System.Drawing.Point(189, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "单据日期";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(380, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "发票日期";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(571, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX4.Size = new System.Drawing.Size(44, 18);
            this.labelX4.TabIndex = 55;
            this.labelX4.Text = "发票号";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(720, 11);
            this.labelX8.Name = "labelX8";
            this.labelX8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX8.Size = new System.Drawing.Size(56, 18);
            this.labelX8.TabIndex = 59;
            this.labelX8.Text = "送货单号";
            // 
            // dtpInvoiceDate
            // 
            // 
            // 
            // 
            this.dtpInvoiceDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpInvoiceDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpInvoiceDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpInvoiceDate.ButtonDropDown.Visible = true;
            this.dtpInvoiceDate.CustomFormat = "yyyy-MM-dd";
            this.dtpInvoiceDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpInvoiceDate.IsPopupCalendarOpen = false;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(439, 10);
            // 
            // 
            // 
            this.dtpInvoiceDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpInvoiceDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpInvoiceDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpInvoiceDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpInvoiceDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpInvoiceDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpInvoiceDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpInvoiceDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpInvoiceDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpInvoiceDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpInvoiceDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpInvoiceDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpInvoiceDate.MonthCalendar.TodayButtonVisible = true;
            this.dtpInvoiceDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.Size = new System.Drawing.Size(110, 21);
            this.dtpInvoiceDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpInvoiceDate.TabIndex = 2;
            // 
            // barHead
            // 
            this.barHead.AntiAlias = true;
            this.barHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.barHead.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barHead.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.barHead.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnSaveBill,
            this.btnNewBill,
            this.btnRefresth,
            this.btnClose});
            this.barHead.Location = new System.Drawing.Point(0, 0);
            this.barHead.Name = "barHead";
            this.barHead.PaddingLeft = 22;
            this.barHead.Size = new System.Drawing.Size(1084, 25);
            this.barHead.Stretch = true;
            this.barHead.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barHead.TabIndex = 84;
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
            // btnRefresth
            // 
            this.btnRefresth.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefresth.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresth.Image")));
            this.btnRefresth.Name = "btnRefresth";
            this.btnRefresth.Text = "刷新选项卡(&R)";
            this.btnRefresth.Click += new System.EventHandler(this.btnRefresth_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭窗体(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmFormInHead
            // 
            this.frmFormInHead.IsSkip = true;
            // 
            // FrmInStoreDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmInStoreDetail";
            this.Text = "入库单明细";
            this.OpenWindowBefore += new System.EventHandler(this.FrmInStoreDetail_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmInStoreDetail_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDetail)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.panelEx6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpBillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpBillDate;
        private DevComponents.DotNetBar.LabelX labelX2;
        private EfwControls.CustomControl.TextBoxCard txtSupport;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInvoiceNO;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpInvoiceDate;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeliveryNO;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbOpType;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Bar barDetail;
        private DevComponents.DotNetBar.ButtonItem btnNewDetail;
        private DevComponents.DotNetBar.ButtonItem btnDelDetail;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar barHead;
        private DevComponents.DotNetBar.ButtonItem btnNewBill;
        private DevComponents.DotNetBar.ButtonItem btnSaveBill;
        private DevComponents.DotNetBar.ButtonItem btnRefresth;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx pnlStatus;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private EfwControls.CustomControl.frmForm frmFormInHead;
        private DevComponents.DotNetBar.ButtonItem btnRefreshDetail;
        private DevComponents.DotNetBar.ButtonItem btnLoadBuyBill;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValidityDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn uAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailFee;
    }
}