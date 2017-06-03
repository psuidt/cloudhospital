namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmChargeDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.plDetail = new DevComponents.DotNetBar.PanelEx();
            this.spGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptositFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CashFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PosFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PromFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundingFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // plDetail
            // 
            this.plDetail.CanvasColor = System.Drawing.SystemColors.Control;
            this.plDetail.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.plDetail.Controls.Add(this.spGrid);
            this.plDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plDetail.Location = new System.Drawing.Point(0, 0);
            this.plDetail.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.plDetail.Name = "plDetail";
            this.plDetail.Size = new System.Drawing.Size(884, 490);
            this.plDetail.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.plDetail.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.plDetail.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.plDetail.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.plDetail.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.plDetail.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.plDetail.Style.GradientAngle = 90;
            this.plDetail.TabIndex = 0;
            // 
            // spGrid
            // 
            this.spGrid.AllowUserToAddRows = false;
            this.spGrid.AllowUserToDeleteRows = false;
            this.spGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.spGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.spGrid.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.spGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.spGrid.ColumnHeadersHeight = 30;
            this.spGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PatName,
            this.InvoiceNO,
            this.TotalFee,
            this.DeptositFee,
            this.CashFee,
            this.PosFee,
            this.PromFee,
            this.RoundingFee,
            this.CostDate});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.spGrid.DefaultCellStyle = dataGridViewCellStyle12;
            this.spGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.spGrid.HighlightSelectedColumnHeaders = false;
            this.spGrid.Location = new System.Drawing.Point(0, 0);
            this.spGrid.Margin = new System.Windows.Forms.Padding(2);
            this.spGrid.MultiSelect = false;
            this.spGrid.Name = "spGrid";
            this.spGrid.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.spGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.spGrid.RowHeadersVisible = false;
            this.spGrid.RowHeadersWidth = 24;
            this.spGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.spGrid.RowTemplate.Height = 25;
            this.spGrid.SelectAllSignVisible = false;
            this.spGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.spGrid.Size = new System.Drawing.Size(884, 490);
            this.spGrid.TabIndex = 103;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.PatName.DefaultCellStyle = dataGridViewCellStyle3;
            this.PatName.HeaderText = "姓名";
            this.PatName.MinimumWidth = 80;
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 80;
            // 
            // InvoiceNO
            // 
            this.InvoiceNO.DataPropertyName = "InvoiceNO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.InvoiceNO.DefaultCellStyle = dataGridViewCellStyle4;
            this.InvoiceNO.HeaderText = "发票号";
            this.InvoiceNO.MinimumWidth = 80;
            this.InvoiceNO.Name = "InvoiceNO";
            this.InvoiceNO.ReadOnly = true;
            this.InvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvoiceNO.Width = 80;
            // 
            // TotalFee
            // 
            this.TotalFee.DataPropertyName = "TotalFee";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = "0.00";
            this.TotalFee.DefaultCellStyle = dataGridViewCellStyle5;
            this.TotalFee.HeaderText = "总金额";
            this.TotalFee.MinimumWidth = 100;
            this.TotalFee.Name = "TotalFee";
            this.TotalFee.ReadOnly = true;
            this.TotalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DeptositFee
            // 
            this.DeptositFee.DataPropertyName = "DeptositFee";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = "0.00";
            this.DeptositFee.DefaultCellStyle = dataGridViewCellStyle6;
            this.DeptositFee.HeaderText = "抵预交金";
            this.DeptositFee.MinimumWidth = 100;
            this.DeptositFee.Name = "DeptositFee";
            this.DeptositFee.ReadOnly = true;
            this.DeptositFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CashFee
            // 
            this.CashFee.DataPropertyName = "CashFee";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = "0.00";
            this.CashFee.DefaultCellStyle = dataGridViewCellStyle7;
            this.CashFee.HeaderText = "现金";
            this.CashFee.Name = "CashFee";
            this.CashFee.ReadOnly = true;
            this.CashFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PosFee
            // 
            this.PosFee.DataPropertyName = "PosFee";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = "0.00";
            this.PosFee.DefaultCellStyle = dataGridViewCellStyle8;
            this.PosFee.HeaderText = "POS金额";
            this.PosFee.Name = "PosFee";
            this.PosFee.ReadOnly = true;
            this.PosFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PromFee
            // 
            this.PromFee.DataPropertyName = "PromFee";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = "0.00";
            this.PromFee.DefaultCellStyle = dataGridViewCellStyle9;
            this.PromFee.HeaderText = "优惠金额";
            this.PromFee.Name = "PromFee";
            this.PromFee.ReadOnly = true;
            this.PromFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RoundingFee
            // 
            this.RoundingFee.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = "0.00";
            this.RoundingFee.DefaultCellStyle = dataGridViewCellStyle10;
            this.RoundingFee.HeaderText = "凑整金额";
            this.RoundingFee.Name = "RoundingFee";
            this.RoundingFee.ReadOnly = true;
            this.RoundingFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CostDate
            // 
            this.CostDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CostDate.DataPropertyName = "CostDate";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle11.Format = "yyyy-MM-dd hh:mm:ss";
            dataGridViewCellStyle11.NullValue = null;
            this.CostDate.DefaultCellStyle = dataGridViewCellStyle11;
            this.CostDate.HeaderText = "收费时间";
            this.CostDate.Name = "CostDate";
            this.CostDate.ReadOnly = true;
            this.CostDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmChargeDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 490);
            this.Controls.Add(this.plDetail);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChargeDetail";
            this.ShowIcon = false;
            this.Text = "收费明细";
            this.Load += new System.EventHandler(this.FrmChargeDetail_Load);
            this.plDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spGrid)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx plDetail;
        protected DevComponents.DotNetBar.Controls.DataGridViewX spGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptositFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn CashFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn PosFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn PromFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoundingFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostDate;
    }
}