namespace HIS_MaterialManage.Winform.ViewForm
{
    partial class FrmStoreQuery
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStoreQuery));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.dgStore = new EfwControls.CustomControl.DataGrid();
            this.MaterialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgBatch = new EfwControls.CustomControl.DataGrid();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.零售金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.进货金额 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnShowTypeTree = new DevComponents.DotNetBar.ButtonX();
            this.txtType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btn_Query = new DevComponents.DotNetBar.ButtonX();
            this.chk_store = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.btn_Close = new DevComponents.DotNetBar.ButtonX();
            this.btn_Print = new DevComponents.DotNetBar.ButtonX();
            this.txt_Code = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.DeptRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.fmCommon = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).BeginInit();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBatch)).BeginInit();
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
            this.panelEx1.Size = new System.Drawing.Size(1192, 421);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "panelEx1";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.dgStore);
            this.panelEx3.Controls.Add(this.expandablePanel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 40);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1192, 381);
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
            // dgStore
            // 
            this.dgStore.AllowSortWhenClickColumnHeader = false;
            this.dgStore.AllowUserToAddRows = false;
            this.dgStore.AllowUserToDeleteRows = false;
            this.dgStore.AllowUserToResizeColumns = false;
            this.dgStore.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgStore.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgStore.BackgroundColor = System.Drawing.Color.White;
            this.dgStore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgStore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaterialID,
            this.MatName,
            this.Spec,
            this.ProductName,
            this.Amount});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStore.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStore.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgStore.HighlightSelectedColumnHeaders = false;
            this.dgStore.Location = new System.Drawing.Point(0, 0);
            this.dgStore.MultiSelect = false;
            this.dgStore.Name = "dgStore";
            this.dgStore.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStore.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgStore.RowHeadersWidth = 30;
            this.dgStore.RowTemplate.Height = 23;
            this.dgStore.SelectAllSignVisible = false;
            this.dgStore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStore.SeqVisible = true;
            this.dgStore.SetCustomStyle = true;
            this.dgStore.Size = new System.Drawing.Size(646, 381);
            this.dgStore.TabIndex = 2;
            this.dgStore.CurrentCellChanged += new System.EventHandler(this.dgStore_CurrentCellChanged);
            // 
            // MaterialID
            // 
            this.MaterialID.DataPropertyName = "MaterialID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MaterialID.DefaultCellStyle = dataGridViewCellStyle3;
            this.MaterialID.HeaderText = "编码";
            this.MaterialID.Name = "MaterialID";
            this.MaterialID.ReadOnly = true;
            this.MaterialID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaterialID.Width = 55;
            // 
            // MatName
            // 
            this.MatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MatName.DataPropertyName = "MatName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MatName.DefaultCellStyle = dataGridViewCellStyle4;
            this.MatName.HeaderText = "商品名称";
            this.MatName.Name = "MatName";
            this.MatName.ReadOnly = true;
            this.MatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Spec.DefaultCellStyle = dataGridViewCellStyle5;
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ProductName
            // 
            this.ProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle6;
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0.00";
            dataGridViewCellStyle7.NullValue = "0.00";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.HeaderText = "数量";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 78;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.CollapseDirection = DevComponents.DotNetBar.eCollapseDirection.RightToLeft;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.dgBatch);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.expandablePanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(646, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.ShowFocusRectangle = true;
            this.expandablePanel1.Size = new System.Drawing.Size(546, 381);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 3;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "批次信息";
            // 
            // dgBatch
            // 
            this.dgBatch.AllowSortWhenClickColumnHeader = false;
            this.dgBatch.AllowUserToAddRows = false;
            this.dgBatch.AllowUserToDeleteRows = false;
            this.dgBatch.AllowUserToResizeColumns = false;
            this.dgBatch.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.AliceBlue;
            this.dgBatch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgBatch.BackgroundColor = System.Drawing.Color.White;
            this.dgBatch.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBatch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgBatch.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.BatchAmount,
            this.Column1,
            this.Column2,
            this.Column3,
            this.零售金额,
            this.进货金额,
            this.Column4});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBatch.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBatch.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBatch.HighlightSelectedColumnHeaders = false;
            this.dgBatch.Location = new System.Drawing.Point(0, 26);
            this.dgBatch.MultiSelect = false;
            this.dgBatch.Name = "dgBatch";
            this.dgBatch.ReadOnly = true;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBatch.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgBatch.RowHeadersWidth = 30;
            this.dgBatch.RowTemplate.Height = 23;
            this.dgBatch.SelectAllSignVisible = false;
            this.dgBatch.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBatch.SeqVisible = true;
            this.dgBatch.SetCustomStyle = true;
            this.dgBatch.Size = new System.Drawing.Size(546, 355);
            this.dgBatch.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "BatchNO";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn2.HeaderText = "批次号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BatchAmount
            // 
            this.BatchAmount.DataPropertyName = "BatchAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "0.00";
            dataGridViewCellStyle13.NullValue = "0.00";
            this.BatchAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.BatchAmount.HeaderText = "数量";
            this.BatchAmount.Name = "BatchAmount";
            this.BatchAmount.ReadOnly = true;
            this.BatchAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BatchAmount.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "UnitName";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle14;
            this.Column1.HeaderText = "单位";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Visible = false;
            this.Column1.Width = 40;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "0.00";
            dataGridViewCellStyle15.NullValue = "0.00";
            this.Column2.DefaultCellStyle = dataGridViewCellStyle15;
            this.Column2.HeaderText = "零售价";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 60;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "StockPrice";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Format = "0.00";
            dataGridViewCellStyle16.NullValue = "0.00";
            this.Column3.DefaultCellStyle = dataGridViewCellStyle16;
            this.Column3.HeaderText = "进货价";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 60;
            // 
            // 零售金额
            // 
            this.零售金额.DataPropertyName = "RetailFee";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.Format = "0.00";
            dataGridViewCellStyle17.NullValue = "0.00";
            this.零售金额.DefaultCellStyle = dataGridViewCellStyle17;
            this.零售金额.HeaderText = "零售金额";
            this.零售金额.Name = "零售金额";
            this.零售金额.ReadOnly = true;
            this.零售金额.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.零售金额.Width = 65;
            // 
            // 进货金额
            // 
            this.进货金额.DataPropertyName = "StockFee";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle18.Format = "0.00";
            dataGridViewCellStyle18.NullValue = "0.00";
            this.进货金额.DefaultCellStyle = dataGridViewCellStyle18;
            this.进货金额.HeaderText = "进货金额";
            this.进货金额.Name = "进货金额";
            this.进货金额.ReadOnly = true;
            this.进货金额.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.进货金额.Width = 65;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "FeeDifference";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle19.Format = "0.00";
            dataGridViewCellStyle19.NullValue = "0.00";
            this.Column4.DefaultCellStyle = dataGridViewCellStyle19;
            this.Column4.HeaderText = "进销差额";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 65;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnShowTypeTree);
            this.panelEx2.Controls.Add(this.txtType);
            this.panelEx2.Controls.Add(this.btn_Query);
            this.panelEx2.Controls.Add(this.chk_store);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.btn_Close);
            this.panelEx2.Controls.Add(this.btn_Print);
            this.panelEx2.Controls.Add(this.txt_Code);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.DeptRoom);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1192, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // btnShowTypeTree
            // 
            this.btnShowTypeTree.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnShowTypeTree.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnShowTypeTree.Location = new System.Drawing.Point(386, 10);
            this.btnShowTypeTree.Name = "btnShowTypeTree";
            this.btnShowTypeTree.Size = new System.Drawing.Size(27, 22);
            this.btnShowTypeTree.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnShowTypeTree.TabIndex = 104;
            this.btnShowTypeTree.Text = "...";
            this.btnShowTypeTree.Click += new System.EventHandler(this.btnShowTypeTree_Click);
            // 
            // txtType
            // 
            // 
            // 
            // 
            this.txtType.Border.Class = "TextBoxBorder";
            this.txtType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtType.Enabled = false;
            this.txtType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtType.Location = new System.Drawing.Point(227, 11);
            this.txtType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(153, 21);
            this.txtType.TabIndex = 103;
            this.txtType.WatermarkText = "请选择物资类型";
            // 
            // btn_Query
            // 
            this.btn_Query.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Query.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Query.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Query.Image = ((System.Drawing.Image)(resources.GetObject("btn_Query.Image")));
            this.btn_Query.Location = new System.Drawing.Point(855, 10);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(75, 22);
            this.btn_Query.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Query.TabIndex = 33;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // chk_store
            // 
            this.chk_store.AutoSize = true;
            // 
            // 
            // 
            this.chk_store.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_store.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk_store.Location = new System.Drawing.Point(701, 12);
            this.chk_store.Name = "chk_store";
            this.chk_store.Size = new System.Drawing.Size(132, 18);
            this.chk_store.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_store.TabIndex = 32;
            this.chk_store.Text = "过滤库存为0的药品";
            this.chk_store.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX5.Location = new System.Drawing.Point(439, 12);
            this.labelX5.Name = "labelX5";
            this.labelX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX5.Size = new System.Drawing.Size(56, 18);
            this.labelX5.TabIndex = 31;
            this.labelX5.Text = "查询代码";
            // 
            // btn_Close
            // 
            this.btn_Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Close.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Close.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.Location = new System.Drawing.Point(1013, 10);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 22);
            this.btn_Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Close.TabIndex = 30;
            this.btn_Close.Text = "关闭(&C)";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Print.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Print.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Print.Image = ((System.Drawing.Image)(resources.GetObject("btn_Print.Image")));
            this.btn_Print.Location = new System.Drawing.Point(934, 10);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 22);
            this.btn_Print.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Print.TabIndex = 29;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // txt_Code
            // 
            // 
            // 
            // 
            this.txt_Code.Border.Class = "TextBoxBorder";
            this.txt_Code.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Code.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Code.Location = new System.Drawing.Point(496, 11);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(183, 21);
            this.txt_Code.TabIndex = 34;
            this.txt_Code.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_Code_KeyDown);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(192, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(29, 23);
            this.labelX2.TabIndex = 27;
            this.labelX2.Text = "类型";
            // 
            // DeptRoom
            // 
            this.DeptRoom.DisplayMember = "Text";
            this.DeptRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DeptRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptRoom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeptRoom.FormattingEnabled = true;
            this.DeptRoom.ItemHeight = 15;
            this.DeptRoom.Location = new System.Drawing.Point(55, 11);
            this.DeptRoom.Name = "DeptRoom";
            this.DeptRoom.Size = new System.Drawing.Size(115, 21);
            this.DeptRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DeptRoom.TabIndex = 3;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(22, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(31, 18);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "库房";
            // 
            // fmCommon
            // 
            this.fmCommon.IsSkip = true;
            // 
            // FrmStoreQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 421);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmStoreQuery";
            this.Text = "物资库存";
            this.OpenWindowBefore += new System.EventHandler(this.FrmStoreQuery_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).EndInit();
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBatch)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx DeptRoom;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX btn_Query;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_store;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.ButtonX btn_Close;
        private DevComponents.DotNetBar.ButtonX btn_Print;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Code;
        private EfwControls.CustomControl.DataGrid dgStore;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private EfwControls.CustomControl.DataGrid dgBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 零售金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn 进货金额;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private EfwControls.CustomControl.frmForm fmCommon;
        private DevComponents.DotNetBar.ButtonX btnShowTypeTree;
        private DevComponents.DotNetBar.Controls.TextBoxX txtType;
    }
}