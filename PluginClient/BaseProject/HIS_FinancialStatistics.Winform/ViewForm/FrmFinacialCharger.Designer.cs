namespace HIS_FinancialStatistics.Winform.ViewForm
{
    partial class FrmFinacialCharger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinacialCharger));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.cmbQueryType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.sdtDate = new EfwControls.CustomControl.StatDateTime();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmbWorker = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.GridViewResult = new Axgregn6Lib.AxGRDisplayViewer();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnPrint);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.cmbQueryType);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.sdtDate);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.cmbWorker);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 40);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(975, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 106;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(894, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 22);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 105;
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(813, 9);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 104;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // cmbQueryType
            // 
            this.cmbQueryType.DisplayMember = "Text";
            this.cmbQueryType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQueryType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQueryType.FormattingEnabled = true;
            this.cmbQueryType.ItemHeight = 15;
            this.cmbQueryType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cmbQueryType.Location = new System.Drawing.Point(687, 10);
            this.cmbQueryType.Name = "cmbQueryType";
            this.cmbQueryType.Size = new System.Drawing.Size(120, 21);
            this.cmbQueryType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbQueryType.TabIndex = 103;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "门诊统计";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "住院统计";
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
            this.labelX2.Location = new System.Drawing.Point(625, 13);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 102;
            this.labelX2.Text = "统计方式";
            // 
            // sdtDate
            // 
            this.sdtDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtDate.DateFormat = "yyyy-MM-dd";
            this.sdtDate.DateWidth = 120;
            this.sdtDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtDate.Location = new System.Drawing.Point(329, 11);
            this.sdtDate.Name = "sdtDate";
            this.sdtDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtDate.Size = new System.Drawing.Size(290, 21);
            this.sdtDate.TabIndex = 101;
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
            this.labelX1.Location = new System.Drawing.Point(267, 13);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 100;
            this.labelX1.Text = "时间范围";
            // 
            // cmbWorker
            // 
            this.cmbWorker.DisplayMember = "Text";
            this.cmbWorker.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWorker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorker.FormattingEnabled = true;
            this.cmbWorker.ItemHeight = 15;
            this.cmbWorker.Location = new System.Drawing.Point(66, 10);
            this.cmbWorker.Name = "cmbWorker";
            this.cmbWorker.Size = new System.Drawing.Size(195, 21);
            this.cmbWorker.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWorker.TabIndex = 99;
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
            this.labelX3.Location = new System.Drawing.Point(4, 13);
            this.labelX3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX3.Name = "labelX3";
            this.labelX3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 98;
            this.labelX3.Text = "医疗机构";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.GridViewResult);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 40);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1264, 689);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "panelEx2";
            // 
            // GridViewResult
            // 
            this.GridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewResult.Enabled = true;
            this.GridViewResult.Location = new System.Drawing.Point(0, 0);
            this.GridViewResult.Name = "GridViewResult";
            this.GridViewResult.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("GridViewResult.OcxState")));
            this.GridViewResult.Size = new System.Drawing.Size(1264, 689);
            this.GridViewResult.TabIndex = 0;
            // 
            // FrmFinacialCharger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmFinacialCharger";
            this.Text = "收费员工作量统计";
            this.OpenWindowBefore += new System.EventHandler(this.FrmFinacialCharger_OpenWindowBefore);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFinacialCharger_FormClosed);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private Axgregn6Lib.AxGRDisplayViewer GridViewResult;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbQueryType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.LabelX labelX2;
        private EfwControls.CustomControl.StatDateTime sdtDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWorker;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnQuery;
    }
}