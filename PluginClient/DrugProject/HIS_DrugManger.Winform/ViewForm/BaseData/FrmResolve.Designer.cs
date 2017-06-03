namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmResolve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmResolve));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmbDept = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnBack = new DevComponents.DotNetBar.ButtonX();
            this.chk_notResolve = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chk_isResolve = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnSet = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.chkAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txt_Code = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dgStore = new EfwControls.CustomControl.GridBoxCard();
            this.tbDrugType = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.ck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StorageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TradeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ResolveFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1266, 376);
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
            this.panelEx4.Controls.Add(this.chkAll);
            this.panelEx4.Controls.Add(this.dgStore);
            this.panelEx4.Controls.Add(this.buttonX6);
            this.panelEx4.Controls.Add(this.buttonX5);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 40);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1266, 336);
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
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX6.Location = new System.Drawing.Point(597, -22);
            this.buttonX6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(45, 15);
            this.buttonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX6.TabIndex = 32;
            this.buttonX6.Text = "退出";
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Location = new System.Drawing.Point(453, -22);
            this.buttonX5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(45, 15);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 29;
            this.buttonX5.Text = "查询";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txt_Code);
            this.panelEx2.Controls.Add(this.cmbDept);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.btnBack);
            this.panelEx2.Controls.Add(this.tbDrugType);
            this.panelEx2.Controls.Add(this.chk_notResolve);
            this.panelEx2.Controls.Add(this.chk_isResolve);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.btnCancel);
            this.panelEx2.Controls.Add(this.btnPrint);
            this.panelEx2.Controls.Add(this.btnSet);
            this.panelEx2.Controls.Add(this.btnSearch);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1266, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // cmbDept
            // 
            this.cmbDept.DisplayMember = "DeptName";
            this.cmbDept.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDept.FormattingEnabled = true;
            this.cmbDept.ItemHeight = 15;
            this.cmbDept.Location = new System.Drawing.Point(49, 11);
            this.cmbDept.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.cmbDept.Name = "cmbDept";
            this.cmbDept.Size = new System.Drawing.Size(84, 21);
            this.cmbDept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDept.TabIndex = 96;
            this.cmbDept.ValueMember = "DeptID";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(12, 12);
            this.labelX4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX4.Name = "labelX4";
            this.labelX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX4.Size = new System.Drawing.Size(31, 18);
            this.labelX4.TabIndex = 95;
            this.labelX4.Text = "库房";
            // 
            // btnBack
            // 
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(972, 10);
            this.btnBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(82, 22);
            this.btnBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBack.TabIndex = 34;
            this.btnBack.Text = "取消拆零";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chk_notResolve
            // 
            this.chk_notResolve.AutoSize = true;
            // 
            // 
            // 
            this.chk_notResolve.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_notResolve.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chk_notResolve.Checked = true;
            this.chk_notResolve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_notResolve.CheckValue = "Y";
            this.chk_notResolve.Location = new System.Drawing.Point(622, 12);
            this.chk_notResolve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chk_notResolve.Name = "chk_notResolve";
            this.chk_notResolve.Size = new System.Drawing.Size(88, 18);
            this.chk_notResolve.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_notResolve.TabIndex = 47;
            this.chk_notResolve.Text = "未拆零药品";
            this.chk_notResolve.CheckedChanged += new System.EventHandler(this.chk_notResolve_CheckedChanged);
            // 
            // chk_isResolve
            // 
            this.chk_isResolve.AutoSize = true;
            // 
            // 
            // 
            this.chk_isResolve.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_isResolve.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.chk_isResolve.Location = new System.Drawing.Point(716, 12);
            this.chk_isResolve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chk_isResolve.Name = "chk_isResolve";
            this.chk_isResolve.Size = new System.Drawing.Size(88, 18);
            this.chk_isResolve.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_isResolve.TabIndex = 46;
            this.chk_isResolve.Text = "已拆零药品";
            this.chk_isResolve.CheckValueChanged += new System.EventHandler(this.chk_isResolve_CheckValueChanged);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(288, 12);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 42;
            this.labelX2.Text = "查询代码";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(139, 12);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 41;
            this.labelX1.Text = "药品类型";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(1141, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 32;
            this.btnCancel.Text = "关闭(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(1060, 10);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 22);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 31;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSet
            // 
            this.btnSet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSet.Image")));
            this.btnSet.Location = new System.Drawing.Point(891, 10);
            this.btnSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 22);
            this.btnSet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSet.TabIndex = 30;
            this.btnSet.Text = "拆零";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(810, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 22);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 29;
            this.btnSearch.Text = "查询(&Q)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkAll
            // 
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.Location = new System.Drawing.Point(46, 4);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(21, 18);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 35;
            this.chkAll.Text = "checkBoxX1";
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // txt_Code
            // 
            // 
            // 
            // 
            this.txt_Code.Border.Class = "TextBoxBorder";
            this.txt_Code.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Code.Location = new System.Drawing.Point(351, 10);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(255, 21);
            this.txt_Code.TabIndex = 97;
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
            this.ck,
            this.StorageID,
            this.DrugID,
            this.TradeName,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.ResolveFlag,
            this.UnitAmount,
            this.PackAmount,
            this.MiniUnit});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgStore.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgStore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgStore.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgStore.HideSelectionCardWhenCustomInput = false;
            this.dgStore.HighlightSelectedColumnHeaders = false;
            this.dgStore.IsInputNumSelectedCard = true;
            this.dgStore.IsShowLetter = false;
            this.dgStore.IsShowPage = false;
            this.dgStore.Location = new System.Drawing.Point(0, 0);
            this.dgStore.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgStore.Name = "dgStore";
            this.dgStore.ReadOnly = true;
            this.dgStore.RowHeadersWidth = 35;
            this.dgStore.RowTemplate.Height = 30;
            this.dgStore.SelectAllSignVisible = false;
            this.dgStore.SelectionCards = null;
            this.dgStore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgStore.SelectionNumKeyBoards = null;
            this.dgStore.SeqVisible = true;
            this.dgStore.SetCustomStyle = true;
            this.dgStore.Size = new System.Drawing.Size(1266, 336);
            this.dgStore.TabIndex = 33;
            this.dgStore.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgStore_CellClick);
            // 
            // tbDrugType
            // 
            // 
            // 
            // 
            this.tbDrugType.Border.Class = "TextBoxBorder";
            this.tbDrugType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbDrugType.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("tbDrugType.ButtonCustom.Image")));
            this.tbDrugType.ButtonCustom.Visible = true;
            this.tbDrugType.CardColumn = "40";
            this.tbDrugType.DisplayField = "";
            this.tbDrugType.IsEnterShowCard = true;
            this.tbDrugType.IsNumSelected = false;
            this.tbDrugType.IsPage = true;
            this.tbDrugType.IsShowLetter = false;
            this.tbDrugType.IsShowPage = false;
            this.tbDrugType.IsShowSeq = true;
            this.tbDrugType.Location = new System.Drawing.Point(201, 11);
            this.tbDrugType.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.tbDrugType.MemberField = "";
            this.tbDrugType.MemberValue = null;
            this.tbDrugType.Name = "tbDrugType";
            this.tbDrugType.QueryFields = new string[] {
        ""};
            this.tbDrugType.QueryFieldsString = "";
            this.tbDrugType.SelectedValue = null;
            this.tbDrugType.ShowCardColumns = null;
            this.tbDrugType.ShowCardDataSource = null;
            this.tbDrugType.ShowCardHeight = 0;
            this.tbDrugType.ShowCardWidth = 0;
            this.tbDrugType.Size = new System.Drawing.Size(81, 21);
            this.tbDrugType.TabIndex = 34;
            this.tbDrugType.TextChanged += new System.EventHandler(this.tbDrugType_TextChanged);
            // 
            // ck
            // 
            this.ck.DataPropertyName = "ck";
            this.ck.HeaderText = "";
            this.ck.Name = "ck";
            this.ck.ReadOnly = true;
            this.ck.Width = 40;
            // 
            // StorageID
            // 
            this.StorageID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.StorageID.FillWeight = 5F;
            this.StorageID.HeaderText = "StorageID";
            this.StorageID.Name = "StorageID";
            this.StorageID.ReadOnly = true;
            this.StorageID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StorageID.Visible = false;
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DrugID.DefaultCellStyle = dataGridViewCellStyle3;
            this.DrugID.HeaderText = "编码";
            this.DrugID.Name = "DrugID";
            this.DrugID.ReadOnly = true;
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 55;
            // 
            // TradeName
            // 
            this.TradeName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TradeName.DataPropertyName = "TradeName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TradeName.DefaultCellStyle = dataGridViewCellStyle4;
            this.TradeName.HeaderText = "药品名称";
            this.TradeName.Name = "TradeName";
            this.TradeName.ReadOnly = true;
            this.TradeName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ChemName
            // 
            this.ChemName.DataPropertyName = "ChemName";
            this.ChemName.HeaderText = "化学名称";
            this.ChemName.Name = "ChemName";
            this.ChemName.ReadOnly = true;
            this.ChemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ChemName.Width = 200;
            // 
            // Spec
            // 
            this.Spec.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Spec.DataPropertyName = "Spec";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Spec.DefaultCellStyle = dataGridViewCellStyle5;
            this.Spec.HeaderText = "药品规格";
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
            // ResolveFlag
            // 
            this.ResolveFlag.DataPropertyName = "ResolveFlag";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ResolveFlag.DefaultCellStyle = dataGridViewCellStyle7;
            this.ResolveFlag.HeaderText = "拆零";
            this.ResolveFlag.Name = "ResolveFlag";
            this.ResolveFlag.ReadOnly = true;
            this.ResolveFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ResolveFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ResolveFlag.Width = 45;
            // 
            // UnitAmount
            // 
            this.UnitAmount.DataPropertyName = "UnitAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.UnitAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.UnitAmount.HeaderText = "拆零系数";
            this.UnitAmount.Name = "UnitAmount";
            this.UnitAmount.ReadOnly = true;
            this.UnitAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitAmount.Width = 65;
            // 
            // PackAmount
            // 
            this.PackAmount.DataPropertyName = "PackAmount";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PackAmount.DefaultCellStyle = dataGridViewCellStyle9;
            this.PackAmount.HeaderText = "库存数量";
            this.PackAmount.Name = "PackAmount";
            this.PackAmount.ReadOnly = true;
            this.PackAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackAmount.Width = 70;
            // 
            // MiniUnit
            // 
            this.MiniUnit.DataPropertyName = "MiniUnit";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MiniUnit.DefaultCellStyle = dataGridViewCellStyle10;
            this.MiniUnit.HeaderText = "拆零单位";
            this.MiniUnit.Name = "MiniUnit";
            this.MiniUnit.ReadOnly = true;
            this.MiniUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniUnit.Width = 65;
            // 
            // FrmResolve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 376);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmResolve";
            this.Text = "药品拆零";
            this.OpenWindowBefore += new System.EventHandler(this.FrmResolve_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgStore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnSet;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private EfwControls.CustomControl.GridBoxCard dgStore;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_notResolve;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_isResolve;
        private EfwControls.CustomControl.TextBoxCard tbDrugType;
        private DevComponents.DotNetBar.ButtonX btnBack;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbDept;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAll;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Code;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ck;
        private System.Windows.Forms.DataGridViewTextBoxColumn StorageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TradeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ResolveFlag;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniUnit;
    }
}