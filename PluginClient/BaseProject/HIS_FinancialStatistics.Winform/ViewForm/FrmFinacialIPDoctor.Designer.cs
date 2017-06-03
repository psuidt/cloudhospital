namespace HIS_FinancialStatistics.Winform.ViewForm
{
    partial class FrmFinacialIPDoctor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFinacialIPDoctor));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnStop = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.cbbTimeType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbbDocType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.dtTimer = new EfwControls.CustomControl.StatDateTime();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cbbWork = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.GridViewResult = new Axgregn6Lib.AxGRDisplayViewer();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnStop);
            this.panelEx1.Controls.Add(this.btnPrint);
            this.panelEx1.Controls.Add(this.btnQuery);
            this.panelEx1.Controls.Add(this.cbbTimeType);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.cbbDocType);
            this.panelEx1.Controls.Add(this.dtTimer);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.cbbWork);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1264, 39);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // btnStop
            // 
            this.btnStop.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStop.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(1179, 7);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(83, 23);
            this.btnStop.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "关闭（&C）";
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(1086, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 11;
            this.btnPrint.Text = "打印（&P）";
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(998, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(83, 23);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 10;
            this.btnQuery.Text = "查询（&Q）";
            this.btnQuery.Click += new System.EventHandler(this.BtnQuery_Click);
            // 
            // cbbTimeType
            // 
            this.cbbTimeType.DisplayMember = "Text";
            this.cbbTimeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTimeType.FormattingEnabled = true;
            this.cbbTimeType.ItemHeight = 15;
            this.cbbTimeType.Location = new System.Drawing.Point(861, 8);
            this.cbbTimeType.Name = "cbbTimeType";
            this.cbbTimeType.Size = new System.Drawing.Size(127, 21);
            this.cbbTimeType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbTimeType.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(799, 11);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(56, 18);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "时间类型";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(624, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(56, 18);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "医生类型";
            // 
            // cbbDocType
            // 
            this.cbbDocType.DisplayMember = "Text";
            this.cbbDocType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDocType.FormattingEnabled = true;
            this.cbbDocType.ItemHeight = 15;
            this.cbbDocType.Location = new System.Drawing.Point(686, 8);
            this.cbbDocType.Name = "cbbDocType";
            this.cbbDocType.Size = new System.Drawing.Size(103, 21);
            this.cbbDocType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbDocType.TabIndex = 5;
            // 
            // dtTimer
            // 
            this.dtTimer.BackColor = System.Drawing.Color.Transparent;
            this.dtTimer.DateFormat = "yyyy-MM-dd";
            this.dtTimer.DateWidth = 120;
            this.dtTimer.Font = new System.Drawing.Font("宋体", 9.5F);
            this.dtTimer.Location = new System.Drawing.Point(308, 8);
            this.dtTimer.Name = "dtTimer";
            this.dtTimer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.dtTimer.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.dtTimer.Size = new System.Drawing.Size(303, 22);
            this.dtTimer.TabIndex = 3;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(246, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "时间范围";
            // 
            // cbbWork
            // 
            this.cbbWork.DisplayMember = "Text";
            this.cbbWork.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbbWork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbWork.FormattingEnabled = true;
            this.cbbWork.ItemHeight = 15;
            this.cbbWork.Location = new System.Drawing.Point(71, 9);
            this.cbbWork.Name = "cbbWork";
            this.cbbWork.Size = new System.Drawing.Size(169, 21);
            this.cbbWork.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbbWork.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "医疗机构";
            // 
            // GridViewResult
            // 
            this.GridViewResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridViewResult.Enabled = true;
            this.GridViewResult.Location = new System.Drawing.Point(0, 39);
            this.GridViewResult.Name = "GridViewResult";
            this.GridViewResult.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("GridViewResult.OcxState")));
            this.GridViewResult.Size = new System.Drawing.Size(1264, 511);
            this.GridViewResult.TabIndex = 2;
            // 
            // FrmFinacialIPDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 550);
            this.Controls.Add(this.GridViewResult);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmFinacialIPDoctor";
            this.Text = "住院医生工作量统计";
            this.OpenWindowBefore += new System.EventHandler(this.FemFinacialIPDoctor_OpenWindowBefore);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmFinacialIPDoctor_FormClosed);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnStop;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbTimeType;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbDocType;
        private EfwControls.CustomControl.StatDateTime dtTimer;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbbWork;
        private DevComponents.DotNetBar.LabelX labelX1;
        private Axgregn6Lib.AxGRDisplayViewer GridViewResult;
    }
}