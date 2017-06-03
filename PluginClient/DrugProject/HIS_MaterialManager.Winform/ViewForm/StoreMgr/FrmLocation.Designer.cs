namespace HIS_MaterialManage.Winform.ViewForm
{
    partial class FrmLocation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocation));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dgStore = new EfwControls.CustomControl.DataGrid();
            this.chkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StorageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.LocationTree = new DevComponents.AdvTree.AdvTree();
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolMenuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuDel = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnShowTypeTree = new DevComponents.DotNetBar.ButtonX();
            this.txtType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.BtnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnSet = new DevComponents.DotNetBar.ButtonX();
            this.BtnCancel = new DevComponents.DotNetBar.ButtonX();
            this.txt_Code = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.chkIsLocation = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkNotLocation = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.DeptRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chk_Store = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.frmLocations = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocationTree)).BeginInit();
            this.treeMenu.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1224, 446);
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
            this.panelEx4.Controls.Add(this.dgStore);
            this.panelEx4.Controls.Add(this.expandableSplitter1);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(200, 36);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1024, 410);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 2;
            this.panelEx4.Text = "panelEx4";
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgStore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgStore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkSelect,
            this.StorageID,
            this.MaterialID,
            this.MatName,
            this.Spec,
            this.ProductName,
            this.Amount,
            this.UnitName,
            this.txtLocation});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStore.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStore.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgStore.HighlightSelectedColumnHeaders = false;
            this.dgStore.Location = new System.Drawing.Point(6, 0);
            this.dgStore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgStore.Name = "dgStore";
            this.dgStore.ReadOnly = true;
            this.dgStore.RowHeadersWidth = 30;
            this.dgStore.RowTemplate.Height = 23;
            this.dgStore.SelectAllSignVisible = false;
            this.dgStore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStore.SeqVisible = true;
            this.dgStore.SetCustomStyle = true;
            this.dgStore.Size = new System.Drawing.Size(1018, 410);
            this.dgStore.TabIndex = 2;
            this.dgStore.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStore_CellClick);
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chkSelect.DataPropertyName = "ck";
            this.chkSelect.FalseValue = "0";
            this.chkSelect.FillWeight = 18.00983F;
            this.chkSelect.HeaderText = "";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.ReadOnly = true;
            this.chkSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chkSelect.TrueValue = "1";
            // 
            // StorageID
            // 
            this.StorageID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StorageID.DataPropertyName = "StorageID";
            this.StorageID.HeaderText = "StorageID";
            this.StorageID.Name = "StorageID";
            this.StorageID.ReadOnly = true;
            this.StorageID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StorageID.Visible = false;
            // 
            // MaterialID
            // 
            this.MaterialID.DataPropertyName = "MaterialID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MaterialID.DefaultCellStyle = dataGridViewCellStyle3;
            this.MaterialID.FillWeight = 36.01966F;
            this.MaterialID.HeaderText = "物资编码";
            this.MaterialID.Name = "MaterialID";
            this.MaterialID.ReadOnly = true;
            this.MaterialID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaterialID.Width = 65;
            // 
            // MatName
            // 
            this.MatName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MatName.DataPropertyName = "MatName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MatName.DefaultCellStyle = dataGridViewCellStyle4;
            this.MatName.FillWeight = 59.4233F;
            this.MatName.HeaderText = "物资名称";
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
            this.Spec.FillWeight = 59.4233F;
            this.Spec.HeaderText = "物资规格";
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
            this.ProductName.FillWeight = 90.94046F;
            this.ProductName.HeaderText = "生产厂家";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            this.ProductName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle7;
            this.Amount.FillWeight = 45.91593F;
            this.Amount.HeaderText = "数量";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Amount.Width = 65;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.UnitName.DefaultCellStyle = dataGridViewCellStyle8;
            this.UnitName.FillWeight = 105.2906F;
            this.UnitName.HeaderText = "单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 45;
            // 
            // txtLocation
            // 
            this.txtLocation.DataPropertyName = "LocationID";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.txtLocation.DefaultCellStyle = dataGridViewCellStyle9;
            this.txtLocation.FillWeight = 45.91593F;
            this.txtLocation.HeaderText = "库位名称";
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.ReadOnly = true;
            this.txtLocation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.txtLocation.Width = 120;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(0, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 410);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 0;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.LocationTree);
            this.panelEx3.Controls.Add(this.panelEx5);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 36);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(200, 410);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 1;
            // 
            // LocationTree
            // 
            this.LocationTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.LocationTree.AllowDrop = true;
            this.LocationTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.LocationTree.BackgroundStyle.Class = "TreeBorderKey";
            this.LocationTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.LocationTree.ContextMenuStrip = this.treeMenu;
            this.LocationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LocationTree.Location = new System.Drawing.Point(0, 32);
            this.LocationTree.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LocationTree.Name = "LocationTree";
            this.LocationTree.NodesConnector = this.nodeConnector1;
            this.LocationTree.NodeStyle = this.elementStyle1;
            this.LocationTree.PathSeparator = ";";
            this.LocationTree.Size = new System.Drawing.Size(200, 378);
            this.LocationTree.Styles.Add(this.elementStyle1);
            this.LocationTree.TabIndex = 2;
            this.LocationTree.Text = "advTree1";
            // 
            // treeMenu
            // 
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuAdd,
            this.toolMenuEdit,
            this.toolMenuDel});
            this.treeMenu.Name = "contextMenuStrip1";
            this.treeMenu.Size = new System.Drawing.Size(101, 70);
            // 
            // toolMenuAdd
            // 
            this.toolMenuAdd.Name = "toolMenuAdd";
            this.toolMenuAdd.Size = new System.Drawing.Size(100, 22);
            this.toolMenuAdd.Text = "新增";
            this.toolMenuAdd.Click += new System.EventHandler(this.toolMenuAdd_Click);
            // 
            // toolMenuEdit
            // 
            this.toolMenuEdit.Name = "toolMenuEdit";
            this.toolMenuEdit.Size = new System.Drawing.Size(100, 22);
            this.toolMenuEdit.Text = "修改";
            this.toolMenuEdit.Click += new System.EventHandler(this.toolMenuEdit_Click);
            // 
            // toolMenuDel
            // 
            this.toolMenuDel.Name = "toolMenuDel";
            this.toolMenuDel.Size = new System.Drawing.Size(100, 22);
            this.toolMenuDel.Text = "删除";
            this.toolMenuDel.Click += new System.EventHandler(this.toolMenuDel_Click);
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.labelX4);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(200, 32);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 0;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(57, 4);
            this.labelX4.Name = "labelX4";
            this.labelX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX4.Size = new System.Drawing.Size(70, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "库位列表";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnShowTypeTree);
            this.panelEx2.Controls.Add(this.txtType);
            this.panelEx2.Controls.Add(this.BtnSearch);
            this.panelEx2.Controls.Add(this.btnSet);
            this.panelEx2.Controls.Add(this.BtnCancel);
            this.panelEx2.Controls.Add(this.txt_Code);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.chkIsLocation);
            this.panelEx2.Controls.Add(this.chkNotLocation);
            this.panelEx2.Controls.Add(this.DeptRoom);
            this.panelEx2.Controls.Add(this.chk_Store);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1224, 36);
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
            this.btnShowTypeTree.Location = new System.Drawing.Point(359, 7);
            this.btnShowTypeTree.Name = "btnShowTypeTree";
            this.btnShowTypeTree.Size = new System.Drawing.Size(27, 22);
            this.btnShowTypeTree.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnShowTypeTree.TabIndex = 102;
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
            this.txtType.Location = new System.Drawing.Point(200, 8);
            this.txtType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(153, 21);
            this.txtType.TabIndex = 101;
            this.txtType.WatermarkText = "请选择物资类型";
            // 
            // BtnSearch
            // 
            this.BtnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("BtnSearch.Image")));
            this.BtnSearch.Location = new System.Drawing.Point(940, 7);
            this.BtnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 22);
            this.BtnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnSearch.TabIndex = 25;
            this.BtnSearch.Text = "查询(&Q)";
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnSet
            // 
            this.btnSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSet.Image")));
            this.btnSet.Location = new System.Drawing.Point(859, 7);
            this.btnSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 22);
            this.btnSet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSet.TabIndex = 24;
            this.btnSet.Text = "归库(&F2)";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.BtnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.BtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(1020, 7);
            this.BtnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 22);
            this.BtnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.BtnCancel.TabIndex = 23;
            this.BtnCancel.Text = "关闭(&C)";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // txt_Code
            // 
            // 
            // 
            // 
            this.txt_Code.Border.Class = "TextBoxBorder";
            this.txt_Code.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Code.Location = new System.Drawing.Point(456, 7);
            this.txt_Code.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(104, 23);
            this.txt_Code.TabIndex = 22;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(411, 8);
            this.labelX5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX5.Name = "labelX5";
            this.labelX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX5.Size = new System.Drawing.Size(44, 20);
            this.labelX5.TabIndex = 21;
            this.labelX5.Text = "检索码";
            // 
            // chkIsLocation
            // 
            this.chkIsLocation.AutoSize = true;
            // 
            // 
            // 
            this.chkIsLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkIsLocation.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkIsLocation.Location = new System.Drawing.Point(643, 8);
            this.chkIsLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIsLocation.Name = "chkIsLocation";
            this.chkIsLocation.Size = new System.Drawing.Size(64, 20);
            this.chkIsLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkIsLocation.TabIndex = 15;
            this.chkIsLocation.Text = "已归库";
            // 
            // chkNotLocation
            // 
            this.chkNotLocation.AutoSize = true;
            // 
            // 
            // 
            this.chkNotLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkNotLocation.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chkNotLocation.Checked = true;
            this.chkNotLocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNotLocation.CheckValue = "Y";
            this.chkNotLocation.Location = new System.Drawing.Point(582, 8);
            this.chkNotLocation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkNotLocation.Name = "chkNotLocation";
            this.chkNotLocation.Size = new System.Drawing.Size(64, 20);
            this.chkNotLocation.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkNotLocation.TabIndex = 14;
            this.chkNotLocation.Text = "未归库";
            // 
            // DeptRoom
            // 
            this.DeptRoom.DisplayMember = "Text";
            this.DeptRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DeptRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeptRoom.FormattingEnabled = true;
            this.DeptRoom.ItemHeight = 17;
            this.DeptRoom.Location = new System.Drawing.Point(56, 7);
            this.DeptRoom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeptRoom.Name = "DeptRoom";
            this.DeptRoom.Size = new System.Drawing.Size(90, 23);
            this.DeptRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DeptRoom.TabIndex = 13;
            // 
            // chk_Store
            // 
            // 
            // 
            // 
            this.chk_Store.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_Store.Location = new System.Drawing.Point(729, 7);
            this.chk_Store.Name = "chk_Store";
            this.chk_Store.Size = new System.Drawing.Size(108, 23);
            this.chk_Store.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_Store.TabIndex = 11;
            this.chk_Store.Text = "过滤库存为0的";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(168, 7);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(29, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "类型";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(33, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "库房";
            // 
            // frmLocations
            // 
            this.frmLocations.IsSkip = true;
            // 
            // FrmLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 446);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmLocation";
            this.Text = "物资库位管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmLocation_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLocation_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LocationTree)).EndInit();
            this.treeMenu.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_Store;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx DeptRoom;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkNotLocation;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkIsLocation;
        private DevComponents.AdvTree.AdvTree LocationTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private EfwControls.CustomControl.DataGrid dgStore;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn StorageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtLocation;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Code;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.ContextMenuStrip treeMenu;
        private System.Windows.Forms.ToolStripMenuItem toolMenuAdd;
        private System.Windows.Forms.ToolStripMenuItem toolMenuEdit;
        private System.Windows.Forms.ToolStripMenuItem toolMenuDel;
        private EfwControls.CustomControl.frmForm frmLocations;
        private DevComponents.DotNetBar.ButtonX BtnCancel;
        private DevComponents.DotNetBar.ButtonX BtnSearch;
        private DevComponents.DotNetBar.ButtonX btnSet;
        private DevComponents.DotNetBar.ButtonX btnShowTypeTree;
        private DevComponents.DotNetBar.Controls.TextBoxX txtType;
    }
}