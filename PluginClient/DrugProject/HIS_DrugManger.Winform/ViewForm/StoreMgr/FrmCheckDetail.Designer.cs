namespace HIS_DrugManage.Winform.ViewForm
{
    partial class FrmCheckDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCheckDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.lblActSum = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.lblDiffSum = new DevComponents.DotNetBar.LabelX();
            this.lblFactSum = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.barDetail = new DevComponents.DotNetBar.Bar();
            this.btnNewDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnDelDetail = new DevComponents.DotNetBar.ButtonItem();
            this.BtnSetFactAmount = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrintTable = new DevComponents.DotNetBar.ButtonItem();
            this.btnGetData = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefreshDetail = new DevComponents.DotNetBar.ButtonItem();
            this.chkInput = new DevComponents.DotNetBar.CheckBoxItem();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtiBillDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtBillNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.barHead = new DevComponents.DotNetBar.Bar();
            this.btnSaveBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnNewBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnRefresth = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.dgDetails = new EfwControls.CustomControl.GridBoxCard();
            this.txtCode = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.frmFormInHead = new EfwControls.CustomControl.frmForm(this.components);
            this.DrugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Place = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValidityDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barDetail)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtiBillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.panelEx4);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1031, 354);
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
            this.panelEx4.Controls.Add(this.dgDetails);
            this.panelEx4.Controls.Add(this.panelEx6);
            this.panelEx4.Controls.Add(this.barDetail);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 65);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(1031, 289);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 4;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.labelX6);
            this.panelEx6.Controls.Add(this.lblActSum);
            this.panelEx6.Controls.Add(this.labelX8);
            this.panelEx6.Controls.Add(this.lblDiffSum);
            this.panelEx6.Controls.Add(this.lblFactSum);
            this.panelEx6.Controls.Add(this.labelX5);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx6.Location = new System.Drawing.Point(0, 264);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(1031, 25);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 8;
            // 
            // labelX6
            // 
            this.labelX6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(500, 3);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(62, 18);
            this.labelX6.TabIndex = 9;
            this.labelX6.Text = "盘存金额:";
            // 
            // lblActSum
            // 
            this.lblActSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActSum.AutoSize = true;
            // 
            // 
            // 
            this.lblActSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblActSum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblActSum.Location = new System.Drawing.Point(762, 4);
            this.lblActSum.Name = "lblActSum";
            this.lblActSum.Size = new System.Drawing.Size(32, 16);
            this.lblActSum.TabIndex = 6;
            this.lblActSum.Text = "0.00";
            // 
            // labelX8
            // 
            this.labelX8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(880, 3);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(62, 18);
            this.labelX8.TabIndex = 11;
            this.labelX8.Text = "盈亏金额:";
            // 
            // lblDiffSum
            // 
            this.lblDiffSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiffSum.AutoSize = true;
            // 
            // 
            // 
            this.lblDiffSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDiffSum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDiffSum.Location = new System.Drawing.Point(945, 4);
            this.lblDiffSum.Name = "lblDiffSum";
            this.lblDiffSum.Size = new System.Drawing.Size(32, 16);
            this.lblDiffSum.TabIndex = 10;
            this.lblDiffSum.Text = "0.00";
            // 
            // lblFactSum
            // 
            this.lblFactSum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFactSum.AutoSize = true;
            // 
            // 
            // 
            this.lblFactSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFactSum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFactSum.Location = new System.Drawing.Point(565, 4);
            this.lblFactSum.Name = "lblFactSum";
            this.lblFactSum.Size = new System.Drawing.Size(32, 16);
            this.lblFactSum.TabIndex = 8;
            this.lblFactSum.Text = "0.00";
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(697, 3);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(62, 18);
            this.labelX5.TabIndex = 7;
            this.labelX5.Text = "账存金额:";
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
            this.BtnSetFactAmount,
            this.btnPrintTable,
            this.btnGetData,
            this.btnRefreshDetail,
            this.chkInput});
            this.barDetail.Location = new System.Drawing.Point(0, 0);
            this.barDetail.Name = "barDetail";
            this.barDetail.PaddingLeft = 22;
            this.barDetail.Size = new System.Drawing.Size(1031, 25);
            this.barDetail.Stretch = true;
            this.barDetail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barDetail.TabIndex = 7;
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
            this.btnDelDetail.Text = "删除明细(&D)";
            this.btnDelDetail.Click += new System.EventHandler(this.btnDelDetail_Click);
            // 
            // BtnSetFactAmount
            // 
            this.BtnSetFactAmount.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.BtnSetFactAmount.Image = ((System.Drawing.Image)(resources.GetObject("BtnSetFactAmount.Image")));
            this.BtnSetFactAmount.Name = "BtnSetFactAmount";
            this.BtnSetFactAmount.Text = "盘存数默认账存数(&K)";
            this.BtnSetFactAmount.Click += new System.EventHandler(this.BtnSetFactAmount_Click);
            // 
            // btnPrintTable
            // 
            this.btnPrintTable.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPrintTable.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintTable.Image")));
            this.btnPrintTable.Name = "btnPrintTable";
            this.btnPrintTable.Text = "打印空表(&P)";
            this.btnPrintTable.Click += new System.EventHandler(this.btnPrintTable_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnGetData.Image = ((System.Drawing.Image)(resources.GetObject("btnGetData.Image")));
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Text = "提取数据(&T)";
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnRefreshDetail
            // 
            this.btnRefreshDetail.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRefreshDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshDetail.Image")));
            this.btnRefreshDetail.Name = "btnRefreshDetail";
            this.btnRefreshDetail.Text = "刷新网格(F5)";
            this.btnRefreshDetail.Click += new System.EventHandler(this.btnRefreshDetail_Click);
            // 
            // chkInput
            // 
            this.chkInput.Name = "chkInput";
            this.chkInput.Text = "仅录入盘存数";
            this.chkInput.CheckedChanged += new DevComponents.DotNetBar.CheckBoxChangeEventHandler(this.chkInput_CheckedChanged);
            this.chkInput.Click += new System.EventHandler(this.chkInput_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.panelEx5);
            this.panelEx3.Controls.Add(this.barHead);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(1031, 65);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 3;
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx2);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 25);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(1031, 40);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 11;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtCode);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.dtiBillDate);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.txtBillNo);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1031, 40);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 10;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(407, 11);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "定位查询";
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
            this.labelX1.Location = new System.Drawing.Point(192, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "单据时间";
            // 
            // dtiBillDate
            // 
            // 
            // 
            // 
            this.dtiBillDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiBillDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiBillDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiBillDate.ButtonDropDown.Visible = true;
            this.dtiBillDate.CustomFormat = "yyyy-MM-dd";
            this.dtiBillDate.Enabled = false;
            this.dtiBillDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtiBillDate.IsPopupCalendarOpen = false;
            this.dtiBillDate.Location = new System.Drawing.Point(251, 10);
            // 
            // 
            // 
            this.dtiBillDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiBillDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiBillDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiBillDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiBillDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiBillDate.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtiBillDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiBillDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiBillDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiBillDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiBillDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiBillDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiBillDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiBillDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiBillDate.Name = "dtiBillDate";
            this.dtiBillDate.Size = new System.Drawing.Size(135, 21);
            this.dtiBillDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiBillDate.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(22, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(44, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "单据号";
            // 
            // txtBillNo
            // 
            // 
            // 
            // 
            this.txtBillNo.Border.Class = "TextBoxBorder";
            this.txtBillNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBillNo.Location = new System.Drawing.Point(70, 10);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.ReadOnly = true;
            this.txtBillNo.Size = new System.Drawing.Size(100, 21);
            this.txtBillNo.TabIndex = 3;
            this.txtBillNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillNo_KeyPress);
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
            this.barHead.Size = new System.Drawing.Size(1031, 25);
            this.barHead.Stretch = true;
            this.barHead.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barHead.TabIndex = 85;
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
            // dgDetails
            // 
            this.dgDetails.AllowSortWhenClickColumnHeader = false;
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgDetails.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.BackgroundColor = System.Drawing.Color.White;
            this.dgDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DrugID,
            this.ChemName,
            this.Spec,
            this.ProductName,
            this.Place,
            this.BatchNO,
            this.ValidityDate,
            this.RetailPrice,
            this.StockPrice,
            this.ActAmount,
            this.UnitName,
            this.FactAmount});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetails.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDetails.HideSelectionCardWhenCustomInput = false;
            this.dgDetails.HighlightSelectedColumnHeaders = false;
            this.dgDetails.IsInputNumSelectedCard = false;
            this.dgDetails.IsShowLetter = false;
            this.dgDetails.IsShowPage = false;
            this.dgDetails.Location = new System.Drawing.Point(0, 25);
            this.dgDetails.MultiSelect = false;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.RowHeadersWidth = 30;
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
            this.dgDetails.SetCustomStyle = true;
            this.dgDetails.Size = new System.Drawing.Size(1031, 239);
            this.dgDetails.TabIndex = 0;
            this.dgDetails.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.dgDetails_SelectCardRowSelected);
            this.dgDetails.DataGridViewCellPressEnterKey += new EfwControls.CustomControl.OnDataGridViewCellPressEnterKeyHandle(this.dgDetails_DataGridViewCellPressEnterKey);
            this.dgDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridBoxCard1_CellContentClick);
            this.dgDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDetails_CellValueChanged);
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtCode.Border.Class = "TextBoxBorder";
            this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCode.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtCode.ButtonCustom.Image")));
            this.txtCode.ButtonCustom.Visible = true;
            this.txtCode.CardColumn = null;
            this.txtCode.DisplayField = "";
            this.txtCode.IsEnterShowCard = true;
            this.txtCode.IsNumSelected = false;
            this.txtCode.IsPage = true;
            this.txtCode.IsShowLetter = false;
            this.txtCode.IsShowPage = false;
            this.txtCode.IsShowSeq = true;
            this.txtCode.Location = new System.Drawing.Point(470, 9);
            this.txtCode.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtCode.MemberField = "";
            this.txtCode.MemberValue = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.QueryFields = new string[] {
        ""};
            this.txtCode.QueryFieldsString = "";
            this.txtCode.SelectedValue = null;
            this.txtCode.ShowCardColumns = new System.Windows.Forms.DataGridViewTextBoxColumn[0];
            this.txtCode.ShowCardDataSource = null;
            this.txtCode.ShowCardHeight = 0;
            this.txtCode.ShowCardWidth = 0;
            this.txtCode.Size = new System.Drawing.Size(180, 21);
            this.txtCode.TabIndex = 12;
            this.txtCode.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtCode_AfterSelectedRow);
            // 
            // frmFormInHead
            // 
            this.frmFormInHead.IsSkip = true;
            // 
            // DrugID
            // 
            this.DrugID.DataPropertyName = "DrugID";
            this.DrugID.HeaderText = "编码";
            this.DrugID.Name = "DrugID";
            this.DrugID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DrugID.Width = 60;
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
            // Place
            // 
            this.Place.DataPropertyName = "Place";
            this.Place.HeaderText = "库位编码";
            this.Place.Name = "Place";
            this.Place.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BatchNO
            // 
            this.BatchNO.DataPropertyName = "BatchNO";
            this.BatchNO.HeaderText = "批次";
            this.BatchNO.Name = "BatchNO";
            this.BatchNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValidityDate
            // 
            this.ValidityDate.DataPropertyName = "ValidityDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "yyyy-MM-dd";
            this.ValidityDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.ValidityDate.HeaderText = "有效日期";
            this.ValidityDate.Name = "ValidityDate";
            this.ValidityDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "0.00";
            dataGridViewCellStyle4.NullValue = "0.00";
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle4;
            this.RetailPrice.HeaderText = "零售价";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 60;
            // 
            // StockPrice
            // 
            this.StockPrice.DataPropertyName = "StockPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "0.00";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.StockPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.StockPrice.HeaderText = "进货价";
            this.StockPrice.Name = "StockPrice";
            this.StockPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.StockPrice.Width = 60;
            // 
            // ActAmount
            // 
            this.ActAmount.DataPropertyName = "ActAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "0";
            dataGridViewCellStyle6.NullValue = "0";
            this.ActAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.ActAmount.HeaderText = "账包装数";
            this.ActAmount.Name = "ActAmount";
            this.ActAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ActAmount.Width = 70;
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            this.UnitName.HeaderText = "包装单位";
            this.UnitName.Name = "UnitName";
            this.UnitName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UnitName.Width = 70;
            // 
            // FactAmount
            // 
            this.FactAmount.DataPropertyName = "FactAmount";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "0";
            dataGridViewCellStyle7.NullValue = "0";
            this.FactAmount.DefaultCellStyle = dataGridViewCellStyle7;
            this.FactAmount.HeaderText = "盘包装数";
            this.FactAmount.Name = "FactAmount";
            this.FactAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FactAmount.Width = 70;
            // 
            // FrmCheckDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 354);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FrmCheckDetail";
            this.Text = "盘点单";
            this.OpenWindowBefore += new System.EventHandler(this.FrmCheckDetail_OpenWindowBefore);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCheckDetail_KeyDown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            this.panelEx6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barDetail)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtiBillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barHead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiBillDate;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBillNo;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private EfwControls.CustomControl.GridBoxCard dgDetails;
        private DevComponents.DotNetBar.Bar barDetail;
        private DevComponents.DotNetBar.ButtonItem BtnSetFactAmount;
        private DevComponents.DotNetBar.ButtonItem btnNewDetail;
        private DevComponents.DotNetBar.ButtonItem btnDelDetail;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX lblDiffSum;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX lblActSum;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX lblFactSum;
        private DevComponents.DotNetBar.ButtonItem btnPrintTable;
        private DevComponents.DotNetBar.ButtonItem btnGetData;
        private DevComponents.DotNetBar.Bar barHead;
        private DevComponents.DotNetBar.ButtonItem btnSaveBill;
        private DevComponents.DotNetBar.ButtonItem btnNewBill;
        private DevComponents.DotNetBar.ButtonItem btnRefresth;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ButtonItem btnRefreshDetail;
        private EfwControls.CustomControl.frmForm frmFormInHead;
        private EfwControls.CustomControl.TextBoxCard txtCode;
        private DevComponents.DotNetBar.CheckBoxItem chkInput;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Place;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValidityDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactAmount;
    }
}