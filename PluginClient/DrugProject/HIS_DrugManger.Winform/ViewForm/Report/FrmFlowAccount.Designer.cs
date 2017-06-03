namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmFlowAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFlowAccount));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.axGRDisplayViewer = new Axgregn6Lib.AxGRDisplayViewer();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmb_CType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmb_Type = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.LogicType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.cb_Balance = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.DeptRoom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.ckMonth = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckNomal = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtpBillDate = new EfwControls.CustomControl.StatDateTime();
            this.frmCommon = new EfwControls.CustomControl.frmForm(this.components);
            this.cmb_dept = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer)).BeginInit();
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
            this.panelEx1.Size = new System.Drawing.Size(1148, 471);
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
            this.panelEx3.Controls.Add(this.axGRDisplayViewer);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 80);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1148, 391);
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
            // axGRDisplayViewer
            // 
            this.axGRDisplayViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axGRDisplayViewer.Enabled = true;
            this.axGRDisplayViewer.Location = new System.Drawing.Point(0, 0);
            this.axGRDisplayViewer.Name = "axGRDisplayViewer";
            this.axGRDisplayViewer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGRDisplayViewer.OcxState")));
            this.axGRDisplayViewer.Size = new System.Drawing.Size(1148, 391);
            this.axGRDisplayViewer.TabIndex = 2;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cmb_dept);
            this.panelEx2.Controls.Add(this.cmb_CType);
            this.panelEx2.Controls.Add(this.cmb_Type);
            this.panelEx2.Controls.Add(this.LogicType);
            this.panelEx2.Controls.Add(this.cb_Balance);
            this.panelEx2.Controls.Add(this.labelX6);
            this.panelEx2.Controls.Add(this.DeptRoom);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.txtNo);
            this.panelEx2.Controls.Add(this.labelX8);
            this.panelEx2.Controls.Add(this.buttonX3);
            this.panelEx2.Controls.Add(this.buttonX2);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.txtName);
            this.panelEx2.Controls.Add(this.labelX7);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.ckMonth);
            this.panelEx2.Controls.Add(this.ckNomal);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.dtpBillDate);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1148, 80);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // cmb_CType
            // 
            this.cmb_CType.DisplayMember = "Text";
            this.cmb_CType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_CType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CType.FormattingEnabled = true;
            this.cmb_CType.ItemHeight = 15;
            this.cmb_CType.Location = new System.Drawing.Point(323, 42);
            this.cmb_CType.Name = "cmb_CType";
            this.cmb_CType.Size = new System.Drawing.Size(121, 21);
            this.cmb_CType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_CType.TabIndex = 26;
            // 
            // cmb_Type
            // 
            this.cmb_Type.DisplayMember = "Text";
            this.cmb_Type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Type.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Type.FormattingEnabled = true;
            this.cmb_Type.ItemHeight = 15;
            this.cmb_Type.Location = new System.Drawing.Point(165, 42);
            this.cmb_Type.Name = "cmb_Type";
            this.cmb_Type.Size = new System.Drawing.Size(102, 21);
            this.cmb_Type.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_Type.TabIndex = 25;
            this.cmb_Type.TextChanged += new System.EventHandler(this.cmb_Type_TextChanged);
            // 
            // LogicType
            // 
            this.LogicType.DisplayMember = "Text";
            this.LogicType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LogicType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LogicType.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LogicType.FormattingEnabled = true;
            this.LogicType.ItemHeight = 15;
            this.LogicType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.LogicType.Location = new System.Drawing.Point(695, 16);
            this.LogicType.Name = "LogicType";
            this.LogicType.Size = new System.Drawing.Size(170, 21);
            this.LogicType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.LogicType.TabIndex = 23;
            this.LogicType.TextChanged += new System.EventHandler(this.LogicType_TextChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "住院";
            this.comboItem1.Value = "0";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "门诊";
            this.comboItem2.Value = "1";
            // 
            // cb_Balance
            // 
            this.cb_Balance.DisplayMember = "Text";
            this.cb_Balance.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cb_Balance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cb_Balance.FormattingEnabled = true;
            this.cb_Balance.ItemHeight = 15;
            this.cb_Balance.Location = new System.Drawing.Point(165, 15);
            this.cb_Balance.Name = "cb_Balance";
            this.cb_Balance.Size = new System.Drawing.Size(279, 21);
            this.cb_Balance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cb_Balance.TabIndex = 3;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(276, 44);
            this.labelX6.Name = "labelX6";
            this.labelX6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX6.Size = new System.Drawing.Size(44, 18);
            this.labelX6.TabIndex = 13;
            this.labelX6.Text = "子类型";
            // 
            // DeptRoom
            // 
            this.DeptRoom.DisplayMember = "Text";
            this.DeptRoom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.DeptRoom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DeptRoom.FormattingEnabled = true;
            this.DeptRoom.ItemHeight = 15;
            this.DeptRoom.Location = new System.Drawing.Point(510, 16);
            this.DeptRoom.Name = "DeptRoom";
            this.DeptRoom.Size = new System.Drawing.Size(120, 21);
            this.DeptRoom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.DeptRoom.TabIndex = 22;
            this.DeptRoom.TextChanged += new System.EventHandler(this.DeptRoom_TextChanged);
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(106, 43);
            this.labelX5.Name = "labelX5";
            this.labelX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX5.Size = new System.Drawing.Size(56, 18);
            this.labelX5.TabIndex = 11;
            this.labelX5.Text = "药品类型";
            // 
            // txtNo
            // 
            // 
            // 
            // 
            this.txtNo.Border.Class = "TextBoxBorder";
            this.txtNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNo.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNo.Location = new System.Drawing.Point(510, 42);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(120, 21);
            this.txtNo.TabIndex = 21;
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(450, 44);
            this.labelX8.Name = "labelX8";
            this.labelX8.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX8.Size = new System.Drawing.Size(56, 18);
            this.labelX8.TabIndex = 20;
            this.labelX8.Text = "记账单号";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX3.Image = ((System.Drawing.Image)(resources.GetObject("buttonX3.Image")));
            this.buttonX3.Location = new System.Drawing.Point(1033, 43);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(75, 22);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 19;
            this.buttonX3.Text = "关闭(&C)";
            this.buttonX3.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX2.Image = ((System.Drawing.Image)(resources.GetObject("buttonX2.Image")));
            this.buttonX2.Location = new System.Drawing.Point(952, 43);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(75, 22);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 18;
            this.buttonX2.Text = "打印(&P)";
            this.buttonX2.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(871, 43);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 17;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(695, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(170, 21);
            this.txtName.TabIndex = 16;
            // 
            // labelX7
            // 
            this.labelX7.AutoSize = true;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(636, 44);
            this.labelX7.Name = "labelX7";
            this.labelX7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX7.Size = new System.Drawing.Size(56, 18);
            this.labelX7.TabIndex = 15;
            this.labelX7.Text = "药品查询";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(871, 19);
            this.labelX4.Name = "labelX4";
            this.labelX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX4.Size = new System.Drawing.Size(56, 18);
            this.labelX4.TabIndex = 9;
            this.labelX4.Text = "往来单位";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.Location = new System.Drawing.Point(636, 16);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "业务类型";
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(450, 16);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "药剂科室";
            // 
            // ckMonth
            // 
            this.ckMonth.AutoSize = true;
            // 
            // 
            // 
            this.ckMonth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckMonth.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckMonth.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckMonth.Location = new System.Drawing.Point(12, 40);
            this.ckMonth.Name = "ckMonth";
            this.ckMonth.Size = new System.Drawing.Size(76, 18);
            this.ckMonth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckMonth.TabIndex = 4;
            this.ckMonth.Text = "按会计月";
            // 
            // ckNomal
            // 
            this.ckNomal.AutoSize = true;
            // 
            // 
            // 
            this.ckNomal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckNomal.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckNomal.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckNomal.Location = new System.Drawing.Point(12, 16);
            this.ckNomal.Name = "ckNomal";
            this.ckNomal.Size = new System.Drawing.Size(88, 18);
            this.ckNomal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckNomal.TabIndex = 2;
            this.ckNomal.Text = "按自然时间";
            this.ckNomal.CheckedChanged += new System.EventHandler(this.ckNomal_CheckedChanged);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(106, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "统计时间";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.BackColor = System.Drawing.Color.Transparent;
            this.dtpBillDate.DateFormat = "yyyy-MM-dd";
            this.dtpBillDate.DateWidth = 120;
            this.dtpBillDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpBillDate.Location = new System.Drawing.Point(165, 15);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtpBillDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtpBillDate.Size = new System.Drawing.Size(279, 21);
            this.dtpBillDate.TabIndex = 0;
            // 
            // frmCommon
            // 
            this.frmCommon.IsSkip = true;
            // 
            // cmb_dept
            // 
            // 
            // 
            // 
            this.cmb_dept.Border.Class = "TextBoxBorder";
            this.cmb_dept.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cmb_dept.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("textBoxCard1.ButtonCustom.Image")));
            this.cmb_dept.ButtonCustom.Visible = true;
            this.cmb_dept.CardColumn = null;
            this.cmb_dept.DisplayField = "";
            this.cmb_dept.IsEnterShowCard = true;
            this.cmb_dept.IsNumSelected = false;
            this.cmb_dept.IsPage = true;
            this.cmb_dept.IsShowLetter = false;
            this.cmb_dept.IsShowPage = false;
            this.cmb_dept.IsShowSeq = true;
            this.cmb_dept.Location = new System.Drawing.Point(930, 16);
            this.cmb_dept.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.cmb_dept.MemberField = "";
            this.cmb_dept.MemberValue = null;
            this.cmb_dept.Name = "cmb_dept";
            this.cmb_dept.QueryFields = null;
            this.cmb_dept.QueryFieldsString = "";
            this.cmb_dept.SelectedValue = null;
            this.cmb_dept.ShowCardColumns = null;
            this.cmb_dept.ShowCardDataSource = null;
            this.cmb_dept.ShowCardHeight = 0;
            this.cmb_dept.ShowCardWidth = 0;
            this.cmb_dept.Size = new System.Drawing.Size(178, 21);
            this.cmb_dept.TabIndex = 27;
            // 
            // FrmFlowAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 471);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmFlowAccount";
            this.Text = "药品分类流水";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDrugSortBill_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axGRDisplayViewer)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckMonth;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckNomal;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.StatDateTime dtpBillDate;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNo;
        private DevComponents.DotNetBar.Controls.ComboBoxEx DeptRoom;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cb_Balance;
        private DevComponents.DotNetBar.Controls.ComboBoxEx LogicType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_Type;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_CType;
        private Axgregn6Lib.AxGRDisplayViewer axGRDisplayViewer;
        private EfwControls.CustomControl.frmForm frmCommon;
        private EfwControls.CustomControl.TextBoxCard cmb_dept;
    }
}