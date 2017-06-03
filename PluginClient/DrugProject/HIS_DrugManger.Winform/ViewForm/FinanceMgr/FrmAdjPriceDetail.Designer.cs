namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmAdjPriceDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAdjPriceDetail));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniUnitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OldRetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewRetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdjRetailFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btnNewDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefreshDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnExcute = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtAdjRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtAdjCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.frmAdjPrice = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.AntiAlias = false;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(985, 490);
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
            this.panelEx3.Controls.Add(this.dgDetails);
            this.panelEx3.Controls.Add(this.bar1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 40);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(985, 450);
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
            this.dgDetails.ColumnHeadersHeight = 32;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.BatchNO,
            this.AdjAmount,
            this.PackUnit,
            this.PackUnitID,
            this.MiniUnit,
            this.MiniUnitID,
            this.OldRetailPrice,
            this.NewRetailPrice,
            this.AdjRetailFee});
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
            this.dgDetails.Location = new System.Drawing.Point(0, 25);
            this.dgDetails.Name = "dgDetails";
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
            this.dgDetails.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.SelectionNumKeyBoards = null;
            this.dgDetails.SeqVisible = true;
            this.dgDetails.SetCustomStyle = false;
            this.dgDetails.Size = new System.Drawing.Size(985, 425);
            this.dgDetails.TabIndex = 2;
            this.dgDetails.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgDetails_SelectCardRowSelected);
            this.dgDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellValueChanged);
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DrugID.DefaultCellStyle = dataGridViewCellStyle3;
            this.DrugID.HeaderText = "编码";
            this.DrugID.Name = "DrugID";
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 55;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "名称";
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BatchNO.DefaultCellStyle = dataGridViewCellStyle4;
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.ReadOnly = true;
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchNO.Width = 90;
            // 
            // AdjAmount
            // 
            this.AdjAmount.DataPropertyName = "AdjAmount";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.AdjAmount.DefaultCellStyle = dataGridViewCellStyle5;
            this.AdjAmount.HeaderText = "库存量";
            this.AdjAmount.Name = "AdjAmount";
            this.AdjAmount.ReadOnly = true;
            this.AdjAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AdjAmount.Width = 55;
            // 
            // PackUnit
            // 
            this.PackUnit.DataPropertyName = "PackUnit";
            this.PackUnit.HeaderText = "包装单位";
            this.PackUnit.Name = "PackUnit";
            this.PackUnit.ReadOnly = true;
            this.PackUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnit.Width = 40;
            // 
            // PackUnitID
            // 
            this.PackUnitID.DataPropertyName = "PackUnitID";
            this.PackUnitID.HeaderText = "单位ID";
            this.PackUnitID.Name = "PackUnitID";
            this.PackUnitID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnitID.Visible = false;
            // 
            // MiniUnit
            // 
            this.MiniUnit.DataPropertyName = "MiniUnit";
            this.MiniUnit.HeaderText = "基本单位";
            this.MiniUnit.Name = "MiniUnit";
            this.MiniUnit.ReadOnly = true;
            this.MiniUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniUnit.Width = 40;
            // 
            // MiniUnitID
            // 
            this.MiniUnitID.DataPropertyName = "MiniUnitID";
            this.MiniUnitID.HeaderText = "基本单位ID";
            this.MiniUnitID.Name = "MiniUnitID";
            this.MiniUnitID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniUnitID.Visible = false;
            // 
            // OldRetailPrice
            // 
            this.OldRetailPrice.DataPropertyName = "OldRetailPrice";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "0.00";
            this.OldRetailPrice.DefaultCellStyle = dataGridViewCellStyle6;
            this.OldRetailPrice.HeaderText = "原零售价";
            this.OldRetailPrice.Name = "OldRetailPrice";
            this.OldRetailPrice.ReadOnly = true;
            this.OldRetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OldRetailPrice.Width = 75;
            // 
            // NewRetailPrice
            // 
            this.NewRetailPrice.DataPropertyName = "NewRetailPrice";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            this.NewRetailPrice.DefaultCellStyle = dataGridViewCellStyle7;
            this.NewRetailPrice.HeaderText = "现零售价";
            this.NewRetailPrice.Name = "NewRetailPrice";
            this.NewRetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NewRetailPrice.Width = 75;
            // 
            // AdjRetailFee
            // 
            this.AdjRetailFee.DataPropertyName = "AdjRetailFee";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "0.00";
            this.AdjRetailFee.DefaultCellStyle = dataGridViewCellStyle8;
            this.AdjRetailFee.HeaderText = "调整金额";
            this.AdjRetailFee.Name = "AdjRetailFee";
            this.AdjRetailFee.ReadOnly = true;
            this.AdjRetailFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AdjRetailFee.Width = 75;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnNewDetail,
            this.btnDelDetail,
            this.btnRefreshDetail,
            this.btnExcute});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(985, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btnNewDetail
            // 
            this.btnNewDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnNewDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnNewDetail.Image")));
            this.btnNewDetail.Name = "btnNewDetail";
            this.btnNewDetail.Text = "新增明细";
            this.btnNewDetail.Click += new System.EventHandler(this.btnNewDetail_Click);
            // 
            // btnDelDetail
            // 
            this.btnDelDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDelDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDelDetail.Image")));
            this.btnDelDetail.Name = "btnDelDetail";
            this.btnDelDetail.Text = "删除明细";
            this.btnDelDetail.Click += new System.EventHandler(this.btnDelDetail_Click);
            // 
            // btnRefreshDetail
            // 
            this.btnRefreshDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefreshDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshDetail.Image")));
            this.btnRefreshDetail.Name = "btnRefreshDetail";
            this.btnRefreshDetail.Text = "网格刷新";
            this.btnRefreshDetail.Click += new System.EventHandler(this.btnRefreshDetail_Click);
            // 
            // btnExcute
            // 
            this.btnExcute.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnExcute.Image = ((System.Drawing.Image)(resources.GetObject("btnExcute.Image")));
            this.btnExcute.Name = "btnExcute";
            this.btnExcute.Text = "执行调价单";
            this.btnExcute.Click += new System.EventHandler(this.btnExcute_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtAdjRemark);
            this.panelEx2.Controls.Add(this.txtAdjCode);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(985, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // txtAdjRemark
            // 
            // 
            // 
            // 
            this.txtAdjRemark.Border.Class = "TextBoxBorder";
            this.txtAdjRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAdjRemark.Location = new System.Drawing.Point(387, 10);
            this.txtAdjRemark.Name = "txtAdjRemark";
            this.txtAdjRemark.Size = new System.Drawing.Size(380, 21);
            this.txtAdjRemark.TabIndex = 37;
            // 
            // txtAdjCode
            // 
            // 
            // 
            // 
            this.txtAdjCode.Border.Class = "TextBoxBorder";
            this.txtAdjCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAdjCode.Location = new System.Drawing.Point(106, 10);
            this.txtAdjCode.Name = "txtAdjCode";
            this.txtAdjCode.Size = new System.Drawing.Size(200, 21);
            this.txtAdjCode.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(328, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 38;
            this.labelX2.Text = "备注说明";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(81, 18);
            this.labelX1.TabIndex = 36;
            this.labelX1.Text = "调价文书编号";
            // 
            // frmAdjPrice
            // 
            this.frmAdjPrice.IsSkip = true;
            // 
            // FrmAdjPriceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 490);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmAdjPriceDetail";
            this.Text = "调价单添加";
            this.OpenWindowBefore += new System.EventHandler(this.FrmAdjPriceDetail_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAdjCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btnNewDetail;
        private DevComponents.DotNetBar.ButtonItem btnDelDetail;
        private DevComponents.DotNetBar.ButtonItem btnRefreshDetail;
        private DevComponents.DotNetBar.ButtonItem btnExcute;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAdjRemark;
        private EfwControls.CustomControl.frmForm frmAdjPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniUnitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn OldRetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewRetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdjRetailFee;
    }
}