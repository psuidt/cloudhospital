namespace HIS_OPDoctor.Winform.ViewForm
{
    partial class FrmIPDocWorkQuery
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmIPDocWorkQuery));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.chartType = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx7 = new DevComponents.DotNetBar.PanelEx();
            this.dgDocWork = new EfwControls.CustomControl.DataGrid();
            this.Itemtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fees = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.lblFeeAvg = new DevComponents.DotNetBar.LabelX();
            this.lblDrugPer = new DevComponents.DotNetBar.LabelX();
            this.lblDrugSum = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.lblPersonCnt = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.radColumn = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radPie = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnQuery = new DevComponents.DotNetBar.ButtonX();
            this.sdtDate = new EfwControls.CustomControl.StatDateTime();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.panelEx5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartType)).BeginInit();
            this.panelEx4.SuspendLayout();
            this.panelEx7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDocWork)).BeginInit();
            this.panelEx6.SuspendLayout();
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
            this.panelEx1.Size = new System.Drawing.Size(902, 504);
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
            this.panelEx3.Controls.Add(this.panelEx5);
            this.panelEx3.Controls.Add(this.panelEx4);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 38);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(902, 466);
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
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.chartType);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(293, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(609, 466);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 1;
            // 
            // chartType
            // 
            chartArea1.Name = "ChartArea1";
            this.chartType.ChartAreas.Add(chartArea1);
            this.chartType.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartType.Legends.Add(legend1);
            this.chartType.Location = new System.Drawing.Point(0, 0);
            this.chartType.Name = "chartType";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartType.Series.Add(series1);
            this.chartType.Size = new System.Drawing.Size(609, 466);
            this.chartType.TabIndex = 0;
            this.chartType.Text = "chart1";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.panelEx7);
            this.panelEx4.Controls.Add(this.panelEx6);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(293, 466);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            this.panelEx4.Text = "panelEx4";
            // 
            // panelEx7
            // 
            this.panelEx7.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx7.Controls.Add(this.dgDocWork);
            this.panelEx7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx7.Location = new System.Drawing.Point(0, 63);
            this.panelEx7.Name = "panelEx7";
            this.panelEx7.Size = new System.Drawing.Size(293, 403);
            this.panelEx7.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx7.Style.GradientAngle = 90;
            this.panelEx7.TabIndex = 1;
            // 
            // dgDocWork
            // 
            this.dgDocWork.AllowSortWhenClickColumnHeader = false;
            this.dgDocWork.AllowUserToAddRows = false;
            this.dgDocWork.AllowUserToDeleteRows = false;
            this.dgDocWork.AllowUserToResizeColumns = false;
            this.dgDocWork.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgDocWork.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDocWork.BackgroundColor = System.Drawing.Color.White;
            this.dgDocWork.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDocWork.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDocWork.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Itemtype,
            this.Fees});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDocWork.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgDocWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDocWork.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDocWork.HighlightSelectedColumnHeaders = false;
            this.dgDocWork.Location = new System.Drawing.Point(0, 0);
            this.dgDocWork.Margin = new System.Windows.Forms.Padding(2);
            this.dgDocWork.MultiSelect = false;
            this.dgDocWork.Name = "dgDocWork";
            this.dgDocWork.ReadOnly = true;
            this.dgDocWork.RowHeadersWidth = 30;
            this.dgDocWork.RowTemplate.Height = 23;
            this.dgDocWork.SelectAllSignVisible = false;
            this.dgDocWork.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDocWork.SeqVisible = true;
            this.dgDocWork.SetCustomStyle = true;
            this.dgDocWork.Size = new System.Drawing.Size(293, 403);
            this.dgDocWork.TabIndex = 9;
            // 
            // Itemtype
            // 
            this.Itemtype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Itemtype.DataPropertyName = "Itemtype";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Itemtype.DefaultCellStyle = dataGridViewCellStyle3;
            this.Itemtype.HeaderText = "项目类型";
            this.Itemtype.MinimumWidth = 80;
            this.Itemtype.Name = "Itemtype";
            this.Itemtype.ReadOnly = true;
            this.Itemtype.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Fees
            // 
            this.Fees.DataPropertyName = "Fees";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "0.00";
            this.Fees.DefaultCellStyle = dataGridViewCellStyle4;
            this.Fees.HeaderText = "金额";
            this.Fees.Name = "Fees";
            this.Fees.ReadOnly = true;
            this.Fees.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.lblFeeAvg);
            this.panelEx6.Controls.Add(this.lblDrugPer);
            this.panelEx6.Controls.Add(this.lblDrugSum);
            this.panelEx6.Controls.Add(this.labelX5);
            this.panelEx6.Controls.Add(this.labelX4);
            this.panelEx6.Controls.Add(this.labelX3);
            this.panelEx6.Controls.Add(this.lblPersonCnt);
            this.panelEx6.Controls.Add(this.labelX2);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx6.Location = new System.Drawing.Point(0, 0);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(293, 63);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 0;
            // 
            // lblFeeAvg
            // 
            this.lblFeeAvg.AutoSize = true;
            // 
            // 
            // 
            this.lblFeeAvg.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFeeAvg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFeeAvg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.lblFeeAvg.Location = new System.Drawing.Point(96, 35);
            this.lblFeeAvg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblFeeAvg.Name = "lblFeeAvg";
            this.lblFeeAvg.Size = new System.Drawing.Size(31, 16);
            this.lblFeeAvg.TabIndex = 115;
            this.lblFeeAvg.Text = "0.00";
            // 
            // lblDrugPer
            // 
            this.lblDrugPer.AutoSize = true;
            // 
            // 
            // 
            this.lblDrugPer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDrugPer.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugPer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.lblDrugPer.Location = new System.Drawing.Point(220, 35);
            this.lblDrugPer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDrugPer.Name = "lblDrugPer";
            this.lblDrugPer.Size = new System.Drawing.Size(37, 16);
            this.lblDrugPer.TabIndex = 114;
            this.lblDrugPer.Text = "0.00%";
            // 
            // lblDrugSum
            // 
            this.lblDrugSum.AutoSize = true;
            // 
            // 
            // 
            this.lblDrugSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDrugSum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDrugSum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.lblDrugSum.Location = new System.Drawing.Point(220, 8);
            this.lblDrugSum.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblDrugSum.Name = "lblDrugSum";
            this.lblDrugSum.Size = new System.Drawing.Size(31, 16);
            this.lblDrugSum.TabIndex = 113;
            this.lblDrugSum.Text = "0.00";
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
            this.labelX5.Location = new System.Drawing.Point(6, 35);
            this.labelX5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(93, 18);
            this.labelX5.TabIndex = 112;
            this.labelX5.Text = "病人人均费用：";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX4.Location = new System.Drawing.Point(155, 35);
            this.labelX4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(68, 18);
            this.labelX4.TabIndex = 111;
            this.labelX4.Text = "药品占比：";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
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
            this.labelX3.Location = new System.Drawing.Point(155, 8);
            this.labelX3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(68, 18);
            this.labelX3.TabIndex = 110;
            this.labelX3.Text = "药品合计：";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblPersonCnt
            // 
            this.lblPersonCnt.AutoSize = true;
            // 
            // 
            // 
            this.lblPersonCnt.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPersonCnt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPersonCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.lblPersonCnt.Location = new System.Drawing.Point(96, 8);
            this.lblPersonCnt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblPersonCnt.Name = "lblPersonCnt";
            this.lblPersonCnt.Size = new System.Drawing.Size(25, 18);
            this.lblPersonCnt.TabIndex = 109;
            this.lblPersonCnt.Text = "0人";
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
            this.labelX2.Location = new System.Drawing.Point(6, 8);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(81, 18);
            this.labelX2.TabIndex = 108;
            this.labelX2.Text = "主管病人数：";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.radColumn);
            this.panelEx2.Controls.Add(this.radPie);
            this.panelEx2.Controls.Add(this.btnClose);
            this.panelEx2.Controls.Add(this.btnQuery);
            this.panelEx2.Controls.Add(this.sdtDate);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(902, 38);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // radColumn
            // 
            this.radColumn.AutoSize = true;
            // 
            // 
            // 
            this.radColumn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radColumn.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radColumn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radColumn.Location = new System.Drawing.Point(625, 11);
            this.radColumn.Name = "radColumn";
            this.radColumn.Size = new System.Drawing.Size(64, 18);
            this.radColumn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radColumn.TabIndex = 112;
            this.radColumn.Text = "柱状图";
            this.radColumn.CheckedChanged += new System.EventHandler(this.radColumn_CheckedChanged);
            // 
            // radPie
            // 
            this.radPie.AutoSize = true;
            // 
            // 
            // 
            this.radPie.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radPie.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radPie.Checked = true;
            this.radPie.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radPie.CheckValue = "Y";
            this.radPie.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radPie.Location = new System.Drawing.Point(564, 11);
            this.radPie.Name = "radPie";
            this.radPie.Size = new System.Drawing.Size(64, 18);
            this.radPie.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radPie.TabIndex = 111;
            this.radPie.Text = "饼状图";
            this.radPie.CheckedChanged += new System.EventHandler(this.radPie_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(462, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 110;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnQuery.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.Location = new System.Drawing.Point(381, 10);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 22);
            this.btnQuery.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnQuery.TabIndex = 109;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // sdtDate
            // 
            this.sdtDate.BackColor = System.Drawing.Color.Transparent;
            this.sdtDate.DateFormat = "yyyy-MM-dd";
            this.sdtDate.DateWidth = 120;
            this.sdtDate.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sdtDate.Location = new System.Drawing.Point(75, 9);
            this.sdtDate.Name = "sdtDate";
            this.sdtDate.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.sdtDate.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.sdtDate.Size = new System.Drawing.Size(290, 21);
            this.sdtDate.TabIndex = 108;
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
            this.labelX1.Location = new System.Drawing.Point(13, 11);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 107;
            this.labelX1.Text = "统计时间";
            // 
            // FrmIPDocWorkQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 504);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmIPDocWorkQuery";
            this.Text = "住院医生个人工作量统计";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDocWorkQuery_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartType)).EndInit();
            this.panelEx4.ResumeLayout(false);
            this.panelEx7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDocWork)).EndInit();
            this.panelEx6.ResumeLayout(false);
            this.panelEx6.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx7;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnQuery;
        private EfwControls.CustomControl.StatDateTime sdtDate;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX radColumn;
        private DevComponents.DotNetBar.Controls.CheckBoxX radPie;
        private DevComponents.DotNetBar.LabelX lblPersonCnt;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartType;
        private EfwControls.CustomControl.DataGrid dgDocWork;
        private DevComponents.DotNetBar.LabelX lblFeeAvg;
        private DevComponents.DotNetBar.LabelX lblDrugPer;
        private DevComponents.DotNetBar.LabelX lblDrugSum;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fees;
    }
}