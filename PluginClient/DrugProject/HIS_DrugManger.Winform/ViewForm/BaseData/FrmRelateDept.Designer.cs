namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmRelateDept
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRelateDept));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.btn_Close = new DevComponents.DotNetBar.ButtonX();
            this.btn_del = new DevComponents.DotNetBar.ButtonX();
            this.btn_Save = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx8 = new DevComponents.DotNetBar.PanelEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.panelEx9 = new DevComponents.DotNetBar.PanelEx();
            this.dg_deptlist = new EfwControls.CustomControl.GridBoxCard();
            this.RelationDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationDeptType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrugDeptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationDeptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtc_ds = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.txtc_dept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx6.SuspendLayout();
            this.panelEx7.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx8.SuspendLayout();
            this.panelEx9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_deptlist)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(884, 311);
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
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx6);
            this.panelEx3.Controls.Add(this.panelEx7);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(228, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(656, 311);
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
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.dg_deptlist);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 40);
            this.panelEx6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(656, 271);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 4;
            this.panelEx6.Text = "panelEx6";
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.btn_Close);
            this.panelEx7.Controls.Add(this.btn_del);
            this.panelEx7.Controls.Add(this.btn_Save);
            this.panelEx7.Controls.Add(this.txtc_ds);
            this.panelEx7.Controls.Add(this.labelX2);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx7.Location = new System.Drawing.Point(0, 0);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(656, 40);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 3;
            // 
            // btn_Close
            // 
            this.btn_Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Close.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Close.Image = ((System.Drawing.Image)(resources.GetObject("btn_Close.Image")));
            this.btn_Close.Location = new System.Drawing.Point(504, 9);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 22);
            this.btn_Close.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Close.TabIndex = 6;
            this.btn_Close.Text = "关闭(&C)";
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_del
            // 
            this.btn_del.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_del.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_del.Image = ((System.Drawing.Image)(resources.GetObject("btn_del.Image")));
            this.btn_del.Location = new System.Drawing.Point(423, 9);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 22);
            this.btn_del.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_del.TabIndex = 5;
            this.btn_del.Text = "删除(&D)";
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Location = new System.Drawing.Point(342, 9);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 22);
            this.btn_Save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 10F);
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(22, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(62, 19);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "药品库房";
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandableControl = this.panelEx2;
            this.expandableSplitter1.ExpandActionClick = false;
            this.expandableSplitter1.ExpandActionDoubleClick = true;
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
            this.expandableSplitter1.Location = new System.Drawing.Point(222, 0);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(6, 311);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 2;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx5);
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(222, 311);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            this.panelEx2.Text = "panelEx2";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx9);
            this.panelEx5.Controls.Add(this.panelEx8);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 40);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(222, 271);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 2;
            this.panelEx5.Text = "panelEx5";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(222, 241);
            this.treeView1.TabIndex = 0;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.txtc_dept);
            this.panelEx4.Controls.Add(this.labelX1);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(222, 40);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10F);
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(14, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(62, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "定位查询";
            // 
            // panelEx8
            // 
            this.panelEx8.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx8.Controls.Add(this.labelX3);
            this.panelEx8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx8.Location = new System.Drawing.Point(0, 241);
            this.panelEx8.Name = "panelEx8";
            this.panelEx8.Size = new System.Drawing.Size(222, 30);
            this.panelEx8.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx8.Style.GradientAngle = 90;
            this.panelEx8.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.ForeColor = System.Drawing.Color.Red;
            this.labelX3.Location = new System.Drawing.Point(0, 0);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(222, 30);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "通过双击树节点来添加往来科室";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelEx9
            // 
            this.panelEx9.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx9.Controls.Add(this.treeView1);
            this.panelEx9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx9.Location = new System.Drawing.Point(0, 0);
            this.panelEx9.Name = "panelEx9";
            this.panelEx9.Size = new System.Drawing.Size(222, 241);
            this.panelEx9.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx9.Style.GradientAngle = 90;
            this.panelEx9.TabIndex = 2;
            this.panelEx9.Text = "panelEx9";
            // 
            // dg_deptlist
            // 
            this.dg_deptlist.AllowSortWhenClickColumnHeader = false;
            this.dg_deptlist.AllowUserToAddRows = false;
            this.dg_deptlist.AllowUserToDeleteRows = false;
            this.dg_deptlist.AllowUserToResizeColumns = false;
            this.dg_deptlist.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dg_deptlist.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_deptlist.BackgroundColor = System.Drawing.Color.White;
            this.dg_deptlist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_deptlist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_deptlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RelationDeptName,
            this.RelationDeptType,
            this.DrugDeptID,
            this.RelationDeptID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_deptlist.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_deptlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dg_deptlist.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dg_deptlist.HideSelectionCardWhenCustomInput = false;
            this.dg_deptlist.HighlightSelectedColumnHeaders = false;
            this.dg_deptlist.IsInputNumSelectedCard = true;
            this.dg_deptlist.IsShowLetter = false;
            this.dg_deptlist.IsShowPage = false;
            this.dg_deptlist.Location = new System.Drawing.Point(0, 0);
            this.dg_deptlist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dg_deptlist.MultiSelect = false;
            this.dg_deptlist.Name = "dg_deptlist";
            this.dg_deptlist.RowHeadersWidth = 30;
            this.dg_deptlist.RowTemplate.Height = 23;
            this.dg_deptlist.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 1;
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
            this.dg_deptlist.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.dg_deptlist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_deptlist.SelectionNumKeyBoards = null;
            this.dg_deptlist.SeqVisible = true;
            this.dg_deptlist.SetCustomStyle = true;
            this.dg_deptlist.Size = new System.Drawing.Size(656, 271);
            this.dg_deptlist.TabIndex = 2;
            this.dg_deptlist.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dg_deptlist_SelectCardRowSelected);
            this.dg_deptlist.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dg_deptlist_DataGridViewCellPressEnterKey);
            // 
            // RelationDeptName
            // 
            this.RelationDeptName.DataPropertyName = "RelationDeptName";
            this.RelationDeptName.HeaderText = "科室名称";
            this.RelationDeptName.Name = "RelationDeptName";
            this.RelationDeptName.ReadOnly = true;
            this.RelationDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RelationDeptName.Width = 500;
            // 
            // RelationDeptType
            // 
            this.RelationDeptType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RelationDeptType.DataPropertyName = "RelationDeptTypeName";
            this.RelationDeptType.HeaderText = "科室类别";
            this.RelationDeptType.Name = "RelationDeptType";
            this.RelationDeptType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DrugDeptID
            // 
            this.DrugDeptID.DataPropertyName = "DrugDeptID";
            this.DrugDeptID.HeaderText = "DrugDeptID";
            this.DrugDeptID.Name = "DrugDeptID";
            this.DrugDeptID.ReadOnly = true;
            this.DrugDeptID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugDeptID.Visible = false;
            // 
            // RelationDeptID
            // 
            this.RelationDeptID.DataPropertyName = "RelationDeptID";
            this.RelationDeptID.HeaderText = "RelationDeptID";
            this.RelationDeptID.Name = "RelationDeptID";
            this.RelationDeptID.ReadOnly = true;
            this.RelationDeptID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RelationDeptID.Visible = false;
            // 
            // txtc_ds
            // 
            // 
            // 
            // 
            this.txtc_ds.Border.Class = "TextBoxBorder";
            this.txtc_ds.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtc_ds.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtc_ds.ButtonCustom.Image")));
            this.txtc_ds.ButtonCustom.Visible = true;
            this.txtc_ds.CardColumn = "40";
            this.txtc_ds.DisplayField = "";
            this.txtc_ds.IsEnterShowCard = true;
            this.txtc_ds.IsNumSelected = false;
            this.txtc_ds.IsPage = true;
            this.txtc_ds.IsShowLetter = false;
            this.txtc_ds.IsShowPage = false;
            this.txtc_ds.IsShowSeq = true;
            this.txtc_ds.Location = new System.Drawing.Point(90, 10);
            this.txtc_ds.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtc_ds.MemberField = "";
            this.txtc_ds.MemberValue = null;
            this.txtc_ds.Name = "txtc_ds";
            this.txtc_ds.QueryFields = new string[] {
        ""};
            this.txtc_ds.QueryFieldsString = "";
            this.txtc_ds.SelectedValue = null;
            this.txtc_ds.ShowCardColumns = null;
            this.txtc_ds.ShowCardDataSource = null;
            this.txtc_ds.ShowCardHeight = 0;
            this.txtc_ds.ShowCardWidth = 0;
            this.txtc_ds.Size = new System.Drawing.Size(230, 21);
            this.txtc_ds.TabIndex = 3;
            this.txtc_ds.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtc_ds_AfterSelectedRow);
            // 
            // txtc_dept
            // 
            this.txtc_dept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtc_dept.Border.Class = "TextBoxBorder";
            this.txtc_dept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtc_dept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtc_dept.ButtonCustom.Image")));
            this.txtc_dept.ButtonCustom.Visible = true;
            this.txtc_dept.CardColumn = "40";
            this.txtc_dept.DisplayField = "";
            this.txtc_dept.IsEnterShowCard = true;
            this.txtc_dept.IsNumSelected = false;
            this.txtc_dept.IsPage = true;
            this.txtc_dept.IsShowLetter = false;
            this.txtc_dept.IsShowPage = false;
            this.txtc_dept.IsShowSeq = true;
            this.txtc_dept.Location = new System.Drawing.Point(78, 10);
            this.txtc_dept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtc_dept.MemberField = "";
            this.txtc_dept.MemberValue = null;
            this.txtc_dept.Name = "txtc_dept";
            this.txtc_dept.QueryFields = new string[] {
        ""};
            this.txtc_dept.QueryFieldsString = "";
            this.txtc_dept.SelectedValue = null;
            this.txtc_dept.ShowCardColumns = null;
            this.txtc_dept.ShowCardDataSource = null;
            this.txtc_dept.ShowCardHeight = 0;
            this.txtc_dept.ShowCardWidth = 0;
            this.txtc_dept.Size = new System.Drawing.Size(135, 21);
            this.txtc_dept.TabIndex = 1;
            this.txtc_dept.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtc_dept_AfterSelectedRow);
            // 
            // FrmRelateDept
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 311);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmRelateDept";
            this.Text = "往来科室配置";
            this.OpenWindowBefore += new System.EventHandler(this.FrmRelateDept_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            this.panelEx7.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.panelEx8.ResumeLayout(false);
            this.panelEx9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_deptlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.ButtonX btn_Save;
        private EfwControls.CustomControl.TextBoxCard txtc_ds;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private EfwControls.CustomControl.TextBoxCard txtc_dept;
        private DevComponents.DotNetBar.ButtonX btn_Close;
        private DevComponents.DotNetBar.ButtonX btn_del;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private System.Windows.Forms.TreeView treeView1;
        private EfwControls.CustomControl.GridBoxCard dg_deptlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationDeptType;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugDeptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationDeptID;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx9;
        private DevComponents.DotNetBar.PanelEx panelEx8;
        private DevComponents.DotNetBar.LabelX labelX3;
    }
}