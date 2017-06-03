namespace HIS_MaterialManage.Winform.ViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOutStoreDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard2 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnAddDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelete = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
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
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.MaterialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frmCommon = new EfwControls.CustomControl.frmForm(this.components);
            this.pnlStatus = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOutData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnAddDetail,
            this.btnDelete});
            this.bar1.Location = new System.Drawing.Point(0, 65);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(955, 25);
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
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
            this.panelEx2.Size = new System.Drawing.Size(955, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
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
            this.labelX8.Location = new System.Drawing.Point(655, 12);
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
            this.barHead.Size = new System.Drawing.Size(955, 25);
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
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaterialID,
            this.MatName,
            this.Spec,
            this.ProductName,
            this.BatchNO,
            this.pAmount,
            this.UnitName,
            this.StockPrice,
            this.StockFee,
            this.RetailPrice,
            this.RetailFee,
            this.totalNum});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle9;
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
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
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
            dataGridViewSelectionCard2.BindColumnIndex = 0;
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
            this.dgDetails.Size = new System.Drawing.Size(955, 352);
            this.dgDetails.TabIndex = 0;
            this.dgDetails.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgDetails_SelectCardRowSelected);
            this.dgDetails.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dgDetails_DataGridViewCellPressEnterKey);
            this.dgDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellValueChanged);
            // 
            // MaterialID
            // 
            this.MaterialID.DataPropertyName = "MaterialID";
            this.MaterialID.HeaderText = "编号";
            this.MaterialID.Name = "MaterialID";
            this.MaterialID.ReadOnly = true;
            this.MaterialID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaterialID.Width = 50;
            // 
            // MatName
            // 
            this.MatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MatName.DataPropertyName = "MatName";
            this.MatName.HeaderText = "名称";
            this.MatName.Name = "MatName";
            this.MatName.ReadOnly = true;
            this.MatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.pAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.pAmount.HeaderText = "数量";
            this.pAmount.MinimumWidth = 30;
            this.pAmount.Name = "pAmount";
            this.pAmount.ReadOnly = true;
            this.pAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.pAmount.Width = 60;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.StockPrice.HeaderText = "进货金额";
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.ReadOnly = true;
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 65;
            // 
            // StockFee
            // 
            this.StockFee.DataPropertyName = "StockFee";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "0.00";
            this.StockFee.DefaultCellStyle = dataGridViewCellStyle5;
            this.StockFee.HeaderText = "进货价";
            this.StockFee.Name = "StockFee";
            this.StockFee.ReadOnly = true;
            this.StockFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockFee.Width = 55;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.RetailPrice.HeaderText = "零售金额";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 65;
            // 
            // RetailFee
            // 
            this.RetailFee.DataPropertyName = "RetailFee";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            this.RetailFee.DefaultCellStyle = dataGridViewCellStyle7;
            this.RetailFee.HeaderText = "零售价";
            this.RetailFee.Name = "RetailFee";
            this.RetailFee.ReadOnly = true;
            this.RetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailFee.Width = 55;
            // 
            // totalNum
            // 
            this.totalNum.DataPropertyName = "totalNum";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.totalNum.DefaultCellStyle = dataGridViewCellStyle8;
            this.totalNum.HeaderText = "库存量";
            this.totalNum.Name = "totalNum";
            this.totalNum.ReadOnly = true;
            this.totalNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.totalNum.Width = 65;
            // 
            // frmCommon
            // 
            this.frmCommon.IsSkip = true;
            // 
            // pnlStatus
            // 
            this.pnlStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 352);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(955, 30);
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
            this.panelEx1.Size = new System.Drawing.Size(955, 472);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 3;
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
            this.panelEx3.Size = new System.Drawing.Size(955, 382);
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
            // FrmOutStoreDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 472);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmOutStoreDetail";
            this.Text = "出库单编辑";
            this.OpenWindowBefore += new System.EventHandler(this.FrmOutStoreDetail_OpenWindowBefore);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeOutData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnAddDetail;
        private DevComponents.DotNetBar.ButtonItem btnDelete;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput timeOutData;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbOpType;
        private EfwControls.CustomControl.TextBoxCard txtDept;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRemark;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Bar barHead;
        private DevComponents.DotNetBar.ButtonItem btnSaveBill;
        private DevComponents.DotNetBar.ButtonItem btnNewBill;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private EfwControls.CustomControl.frmForm frmCommon;
        private DevComponents.DotNetBar.PanelEx pnlStatus;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn pAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalNum;
    }
}