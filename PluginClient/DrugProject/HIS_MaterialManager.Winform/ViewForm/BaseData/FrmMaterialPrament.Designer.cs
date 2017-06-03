namespace HIS_MaterialManage.Winform.ViewForm
{
    partial class FrmMaterialPrament
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaterialPrament));
            this.iip_wzcl = new DevComponents.Editors.IntegerInput();
            this.label6 = new System.Windows.Forms.Label();
            this.chk_input_check = new System.Windows.Forms.CheckBox();
            this.chk_out_chexk = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.iip_wzfw = new DevComponents.Editors.IntegerInput();
            this.iip_banlance_day = new DevComponents.Editors.IntegerInput();
            this.chk_dz = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chk_qzpz = new System.Windows.Forms.CheckBox();
            this.chk_qzkc = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lstDrugRoom = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.lblCurrentDeptName = new System.Windows.Forms.Label();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btn_Save = new DevComponents.DotNetBar.ButtonX();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.iip_wzcl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iip_wzfw)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iip_banlance_day)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.expandablePanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // iip_wzcl
            // 
            // 
            // 
            // 
            this.iip_wzcl.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iip_wzcl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iip_wzcl.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iip_wzcl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iip_wzcl.Location = new System.Drawing.Point(116, 32);
            this.iip_wzcl.MaxValue = 100;
            this.iip_wzcl.MinValue = 0;
            this.iip_wzcl.Name = "iip_wzcl";
            this.iip_wzcl.ShowUpDown = true;
            this.iip_wzcl.Size = new System.Drawing.Size(69, 21);
            this.iip_wzcl.TabIndex = 19;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label6.Location = new System.Drawing.Point(188, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "%";
            // 
            // chk_input_check
            // 
            this.chk_input_check.AutoSize = true;
            this.chk_input_check.BackColor = System.Drawing.Color.Transparent;
            this.chk_input_check.Font = new System.Drawing.Font("宋体", 9F);
            this.chk_input_check.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chk_input_check.Location = new System.Drawing.Point(28, 100);
            this.chk_input_check.Name = "chk_input_check";
            this.chk_input_check.Size = new System.Drawing.Size(108, 16);
            this.chk_input_check.TabIndex = 10;
            this.chk_input_check.Text = "入库单自动审核";
            this.chk_input_check.UseVisualStyleBackColor = false;
            // 
            // chk_out_chexk
            // 
            this.chk_out_chexk.AutoSize = true;
            this.chk_out_chexk.BackColor = System.Drawing.Color.Transparent;
            this.chk_out_chexk.Font = new System.Drawing.Font("宋体", 9F);
            this.chk_out_chexk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chk_out_chexk.Location = new System.Drawing.Point(28, 67);
            this.chk_out_chexk.Name = "chk_out_chexk";
            this.chk_out_chexk.Size = new System.Drawing.Size(108, 16);
            this.chk_out_chexk.TabIndex = 9;
            this.chk_out_chexk.Text = "出库单自动审核";
            this.chk_out_chexk.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label4.Location = new System.Drawing.Point(12, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "物资利润百分比";
            // 
            // iip_wzfw
            // 
            // 
            // 
            // 
            this.iip_wzfw.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iip_wzfw.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iip_wzfw.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iip_wzfw.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iip_wzfw.Location = new System.Drawing.Point(123, 256);
            this.iip_wzfw.MaxValue = 100;
            this.iip_wzfw.MinValue = 1;
            this.iip_wzfw.Name = "iip_wzfw";
            this.iip_wzfw.ShowUpDown = true;
            this.iip_wzfw.Size = new System.Drawing.Size(96, 23);
            this.iip_wzfw.TabIndex = 18;
            this.iip_wzfw.Value = 1;
            // 
            // iip_banlance_day
            // 
            // 
            // 
            // 
            this.iip_banlance_day.BackgroundStyle.Class = "DateTimeInputBackground";
            this.iip_banlance_day.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.iip_banlance_day.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.iip_banlance_day.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.iip_banlance_day.Location = new System.Drawing.Point(123, 127);
            this.iip_banlance_day.MaxValue = 31;
            this.iip_banlance_day.MinValue = 1;
            this.iip_banlance_day.Name = "iip_banlance_day";
            this.iip_banlance_day.ShowUpDown = true;
            this.iip_banlance_day.Size = new System.Drawing.Size(96, 23);
            this.iip_banlance_day.TabIndex = 17;
            this.iip_banlance_day.Value = 1;
            // 
            // chk_dz
            // 
            this.chk_dz.AutoSize = true;
            this.chk_dz.BackColor = System.Drawing.Color.Transparent;
            this.chk_dz.Font = new System.Drawing.Font("宋体", 9F);
            this.chk_dz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chk_dz.Location = new System.Drawing.Point(28, 228);
            this.chk_dz.Name = "chk_dz";
            this.chk_dz.Size = new System.Drawing.Size(108, 16);
            this.chk_dz.TabIndex = 16;
            this.chk_dz.Text = "月结前必须对账";
            this.chk_dz.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label10.Location = new System.Drawing.Point(1120, 267);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 20);
            this.label10.TabIndex = 15;
            this.label10.Text = "角";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("宋体", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label9.Location = new System.Drawing.Point(28, 261);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "对账误差范围";
            // 
            // chk_qzpz
            // 
            this.chk_qzpz.AutoSize = true;
            this.chk_qzpz.BackColor = System.Drawing.Color.Transparent;
            this.chk_qzpz.Font = new System.Drawing.Font("宋体", 9F);
            this.chk_qzpz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chk_qzpz.Location = new System.Drawing.Point(28, 195);
            this.chk_qzpz.Name = "chk_qzpz";
            this.chk_qzpz.Size = new System.Drawing.Size(96, 16);
            this.chk_qzpz.TabIndex = 4;
            this.chk_qzpz.Text = "允许强制平账";
            this.chk_qzpz.UseVisualStyleBackColor = false;
            // 
            // chk_qzkc
            // 
            this.chk_qzkc.AutoSize = true;
            this.chk_qzkc.BackColor = System.Drawing.Color.Transparent;
            this.chk_qzkc.Font = new System.Drawing.Font("宋体", 9F);
            this.chk_qzkc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.chk_qzkc.Location = new System.Drawing.Point(28, 162);
            this.chk_qzkc.Name = "chk_qzkc";
            this.chk_qzkc.Size = new System.Drawing.Size(96, 16);
            this.chk_qzkc.TabIndex = 3;
            this.chk_qzkc.Text = "强制控制库存";
            this.chk_qzkc.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 9F);
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label8.Location = new System.Drawing.Point(28, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "月结时间为每月";
            // 
            // lstDrugRoom
            // 
            this.lstDrugRoom.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstDrugRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDrugRoom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstDrugRoom.HideSelection = false;
            this.lstDrugRoom.Location = new System.Drawing.Point(0, 26);
            this.lstDrugRoom.Name = "lstDrugRoom";
            this.lstDrugRoom.Size = new System.Drawing.Size(675, 537);
            this.lstDrugRoom.TabIndex = 0;
            this.lstDrugRoom.UseCompatibleStateImageBehavior = false;
            this.lstDrugRoom.View = System.Windows.Forms.View.Details;
            this.lstDrugRoom.SelectedIndexChanged += new System.EventHandler(this.lstDrugRoom_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "库房";
            this.columnHeader1.Width = 200;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel1);
            this.panelEx1.Controls.Add(this.expandablePanel2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(964, 627);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.lstDrugRoom);
            this.expandablePanel1.Controls.Add(this.panelEx2);
            this.expandablePanel1.Controls.Add(this.label14);
            this.expandablePanel1.Controls.Add(this.label13);
            this.expandablePanel1.Controls.Add(this.label10);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 64);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(964, 563);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 4;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "各库房参数";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.lblCurrentDeptName);
            this.panelEx2.Controls.Add(this.label9);
            this.panelEx2.Controls.Add(this.chk_qzpz);
            this.panelEx2.Controls.Add(this.chk_dz);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Controls.Add(this.chk_qzkc);
            this.panelEx2.Controls.Add(this.btn_Save);
            this.panelEx2.Controls.Add(this.iip_banlance_day);
            this.panelEx2.Controls.Add(this.chk_out_chexk);
            this.panelEx2.Controls.Add(this.label8);
            this.panelEx2.Controls.Add(this.iip_wzfw);
            this.panelEx2.Controls.Add(this.chk_input_check);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelEx2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx2.Location = new System.Drawing.Point(675, 26);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(289, 537);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 24;
            // 
            // lblCurrentDeptName
            // 
            this.lblCurrentDeptName.AutoSize = true;
            this.lblCurrentDeptName.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentDeptName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblCurrentDeptName.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentDeptName.Location = new System.Drawing.Point(28, 28);
            this.lblCurrentDeptName.Name = "lblCurrentDeptName";
            this.lblCurrentDeptName.Size = new System.Drawing.Size(107, 20);
            this.lblCurrentDeptName.TabIndex = 21;
            this.lblCurrentDeptName.Text = "当前选中库房：";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(145, 311);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Save.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Save.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Image = ((System.Drawing.Image)(resources.GetObject("btn_Save.Image")));
            this.btn_Save.Location = new System.Drawing.Point(60, 311);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 22);
            this.btn_Save.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Save.TabIndex = 19;
            this.btn_Save.Text = "保存(&S)";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label14.Location = new System.Drawing.Point(1145, 267);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 20);
            this.label14.TabIndex = 23;
            this.label14.Text = "角";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.label13.Location = new System.Drawing.Point(1145, 129);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 20);
            this.label13.TabIndex = 22;
            this.label13.Text = "日";
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.label4);
            this.expandablePanel2.Controls.Add(this.iip_wzcl);
            this.expandablePanel2.Controls.Add(this.label6);
            this.expandablePanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.expandablePanel2.ExpandButtonVisible = false;
            this.expandablePanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(964, 64);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 3;
            this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "公共参数";
            // 
            // FrmMaterialPrament
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 627);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmMaterialPrament";
            this.Text = "药品系统参数设置";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDrugPrament_OpenWindowBefore);
            this.Load += new System.EventHandler(this.FrmDrugPrament_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iip_wzcl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iip_wzfw)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iip_banlance_day)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            this.expandablePanel1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.expandablePanel2.ResumeLayout(false);
            this.expandablePanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.Editors.IntegerInput iip_wzcl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chk_input_check;
        private System.Windows.Forms.CheckBox chk_out_chexk;
        private System.Windows.Forms.Label label4;
        private DevComponents.Editors.IntegerInput iip_wzfw;
        private DevComponents.Editors.IntegerInput iip_banlance_day;
        private System.Windows.Forms.CheckBox chk_dz;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chk_qzpz;
        private System.Windows.Forms.CheckBox chk_qzkc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lstDrugRoom;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btn_Save;
        private System.Windows.Forms.Label lblCurrentDeptName;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private DevComponents.DotNetBar.PanelEx panelEx2;
    }
}