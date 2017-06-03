namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmSetLimit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard3 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard4 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetLimit));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgLimit = new EfwControls.CustomControl.GridBoxCard();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UpperLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LowerLimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StorageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btn_Setting = new DevComponents.DotNetBar.ButtonItem();
            this.btn_Save = new DevComponents.DotNetBar.ButtonItem();
            this.btn_Cancel = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmbStatus = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.cmb_StoreRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btn_Close = new DevComponents.DotNetBar.ButtonX();
            this.txt_Code = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
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
            this.panelEx1.Size = new System.Drawing.Size(984, 466);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgLimit);
            this.panelEx3.Controls.Add(this.bar1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 40);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(984, 426);
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
            // dgLimit
            // 
            this.dgLimit.AllowSortWhenClickColumnHeader = false;
            this.dgLimit.AllowUserToAddRows = false;
            this.dgLimit.AllowUserToDeleteRows = false;
            this.dgLimit.AllowUserToResizeColumns = false;
            this.dgLimit.AllowUserToResizeRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.AliceBlue;
            this.dgLimit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgLimit.BackgroundColor = System.Drawing.Color.White;
            this.dgLimit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLimit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgLimit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.Place,
            this.Amount,
            this.UnitName,
            this.UpperLimit,
            this.LowerLimit,
            this.StorageID,
            this.DeptID});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgLimit.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLimit.EnableHeadersVisualStyles = false;
            this.dgLimit.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgLimit.HideSelectionCardWhenCustomInput = false;
            this.dgLimit.HighlightSelectedColumnHeaders = false;
            this.dgLimit.IsInputNumSelectedCard = true;
            this.dgLimit.IsShowLetter = false;
            this.dgLimit.IsShowPage = false;
            this.dgLimit.Location = new System.Drawing.Point(0, 25);
            this.dgLimit.MultiSelect = false;
            this.dgLimit.Name = "dgLimit";
            this.dgLimit.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLimit.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgLimit.RowHeadersWidth = 30;
            this.dgLimit.RowTemplate.Height = 23;
            this.dgLimit.SelectAllSignVisible = false;
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
            this.dgLimit.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard3,
        dataGridViewSelectionCard4};
            this.dgLimit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLimit.SelectionNumKeyBoards = null;
            this.dgLimit.SeqVisible = true;
            this.dgLimit.SetCustomStyle = true;
            this.dgLimit.Size = new System.Drawing.Size(984, 401);
            this.dgLimit.TabIndex = 4;
            this.dgLimit.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dgLimit_DataGridViewCellPressEnterKey);
            this.dgLimit.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgLimit_DataError);
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编码";
            this.DrugID.Name = "DrugID";
            this.DrugID.ReadOnly = true;
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 60;
            // 
            // ChemName
            // 
            this.ChemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "药品名称";
            this.ChemName.Name = "ChemName";
            this.ChemName.ReadOnly = true;
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "药品规格";
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
            // Place
            // 
            this.Place.DataPropertyName = "Place";
            this.Place.HeaderText = "库位名称";
            this.Place.Name = "Place";
            this.Place.ReadOnly = true;
            this.Place.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "0";
            dataGridViewCellStyle10.NullValue = "0";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle10;
            this.Amount.HeaderText = "库存数";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 75;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 45;
            // 
            // UpperLimit
            // 
            this.UpperLimit.DataPropertyName = "UpperLimit";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "0";
            dataGridViewCellStyle11.NullValue = "0";
            this.UpperLimit.DefaultCellStyle = dataGridViewCellStyle11;
            this.UpperLimit.HeaderText = "库存上限";
            this.UpperLimit.Name = "UpperLimit";
            this.UpperLimit.ReadOnly = true;
            this.UpperLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UpperLimit.Width = 80;
            // 
            // LowerLimit
            // 
            this.LowerLimit.DataPropertyName = "LowerLimit";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "0";
            dataGridViewCellStyle12.NullValue = "0";
            this.LowerLimit.DefaultCellStyle = dataGridViewCellStyle12;
            this.LowerLimit.HeaderText = "库存下限";
            this.LowerLimit.Name = "LowerLimit";
            this.LowerLimit.ReadOnly = true;
            this.LowerLimit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LowerLimit.Width = 80;
            // 
            // StorageID
            // 
            this.StorageID.DataPropertyName = "StorageID";
            this.StorageID.HeaderText = "StorageID";
            this.StorageID.Name = "StorageID";
            this.StorageID.ReadOnly = true;
            this.StorageID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StorageID.Visible = false;
            // 
            // DeptID
            // 
            this.DeptID.DataPropertyName = "DeptID";
            this.DeptID.HeaderText = "DeptID";
            this.DeptID.Name = "DeptID";
            this.DeptID.ReadOnly = true;
            this.DeptID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeptID.Visible = false;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_Setting,
            this.btn_Save,
            this.btn_Cancel});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bar1.Name = "bar1";
            this.bar1.PaddingLeft = 22;
            this.bar1.Size = new System.Drawing.Size(984, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 3;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btn_Setting
            // 
            this.btn_Setting.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_Setting.Image = ((System.Drawing.Image)(resources.GetObject("btn_Setting.Image")));
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Text = "设置库存上下限(&T)";
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_Save.Enabled = false;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Text = "保存库存上下限(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_Cancel.Enabled = false;
            this.btn_Cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_Cancel.Image")));
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Text = "取消(&X)";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cmbStatus);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.cmb_StoreRoom);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.btn_Close);
            this.panelEx2.Controls.Add(this.txt_Code);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(984, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DisplayMember = "Text";
            this.cmbStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.ItemHeight = 15;
            this.cmbStatus.Location = new System.Drawing.Point(483, 10);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 21);
            this.cmbStatus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbStatus.TabIndex = 24;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(449, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(31, 18);
            this.labelX3.TabIndex = 23;
            this.labelX3.Text = "状态";
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(605, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 22;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cmb_StoreRoom
            // 
            this.cmb_StoreRoom.DisplayMember = "Text";
            this.cmb_StoreRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_StoreRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_StoreRoom.FormattingEnabled = true;
            this.cmb_StoreRoom.ItemHeight = 15;
            this.cmb_StoreRoom.Location = new System.Drawing.Point(56, 10);
            this.cmb_StoreRoom.Name = "cmb_StoreRoom";
            this.cmb_StoreRoom.Size = new System.Drawing.Size(90, 21);
            this.cmb_StoreRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_StoreRoom.TabIndex = 21;
            this.cmb_StoreRoom.SelectedIndexChanged += new System.EventHandler(this.cmb_StoreRoom_SelectedIndexChanged);
            this.cmb_StoreRoom.SelectedValueChanged += new System.EventHandler(this.cmb_StoreRoom_SelectedValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(22, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(31, 18);
            this.labelX2.TabIndex = 20;
            this.labelX2.Text = "库房";
            // 
            // btn_Close
            // 
            this.btn_Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Close.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.Location = new System.Drawing.Point(686, 9);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 22);
            this.btn_Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Close.TabIndex = 3;
            this.btn_Close.Text = "关闭(&C)";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // txt_Code
            // 
            // 
            // 
            // 
            this.txt_Code.Border.Class = "TextBoxBorder";
            this.txt_Code.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Code.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txt_Code.ButtonCustom.Image")));
            this.txt_Code.ButtonCustom.Visible = true;
            this.txt_Code.CardColumn = null;
            this.txt_Code.DisplayField = "";
            this.txt_Code.IsEnterShowCard = true;
            this.txt_Code.IsNumSelected = false;
            this.txt_Code.IsPage = true;
            this.txt_Code.IsShowLetter = false;
            this.txt_Code.IsShowPage = false;
            this.txt_Code.IsShowSeq = true;
            this.txt_Code.Location = new System.Drawing.Point(227, 10);
            this.txt_Code.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txt_Code.MemberField = "";
            this.txt_Code.MemberValue = null;
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.QueryFields = new string[] {
        ""};
            this.txt_Code.QueryFieldsString = "";
            this.txt_Code.SelectedValue = null;
            this.txt_Code.ShowCardColumns = new System.Windows.Forms.DataGridViewTextBoxColumn[] {
        this.dataGridViewTextBoxColumn1};
            this.txt_Code.ShowCardDataSource = null;
            this.txt_Code.ShowCardHeight = 0;
            this.txt_Code.ShowCardWidth = 0;
            this.txt_Code.Size = new System.Drawing.Size(200, 21);
            this.txt_Code.TabIndex = 1;
            this.txt_Code.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txt_Code_AfterSelectedRow);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(168, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "查询代码";
            // 
            // FrmSetLimit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 466);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmSetLimit";
            this.Text = "库存上下限";
            this.OpenWindowBefore += new System.EventHandler(this.FrmSetLimit_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.TextBoxCard txt_Code;
        private DevComponents.DotNetBar.ButtonX btn_Close;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btn_Setting;
        private DevComponents.DotNetBar.ButtonItem btn_Save;
        private EfwControls.CustomControl.GridBoxCard dgLimit;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_StoreRoom;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonItem btn_Cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbStatus;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn StorageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LowerLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn UpperLimit;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Place;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
    }
}