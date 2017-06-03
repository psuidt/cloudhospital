namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmInvoiceDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgInvoiceDetail = new EfwControls.CustomControl.DataGrid();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeInvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndInvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CostDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoundingFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoiceDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgInvoiceDetail);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(873, 438);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dgInvoiceDetail
            // 
            this.dgInvoiceDetail.AllowSortWhenClickColumnHeader = false;
            this.dgInvoiceDetail.AllowUserToAddRows = false;
            this.dgInvoiceDetail.AllowUserToResizeColumns = false;
            this.dgInvoiceDetail.AllowUserToResizeRows = false;
            this.dgInvoiceDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgInvoiceDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgInvoiceDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgInvoiceDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgInvoiceDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.姓名,
            this.BeInvoiceNO,
            this.EndInvoiceNO,
            this.CostDate,
            this.TotalFee,
            this.RoundingFee});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInvoiceDetail.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgInvoiceDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoiceDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgInvoiceDetail.HighlightSelectedColumnHeaders = false;
            this.dgInvoiceDetail.Location = new System.Drawing.Point(0, 0);
            this.dgInvoiceDetail.Name = "dgInvoiceDetail";
            this.dgInvoiceDetail.ReadOnly = true;
            this.dgInvoiceDetail.RowHeadersWidth = 30;
            this.dgInvoiceDetail.RowTemplate.Height = 23;
            this.dgInvoiceDetail.SelectAllSignVisible = false;
            this.dgInvoiceDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgInvoiceDetail.SeqVisible = true;
            this.dgInvoiceDetail.SetCustomStyle = false;
            this.dgInvoiceDetail.Size = new System.Drawing.Size(873, 438);
            this.dgInvoiceDetail.TabIndex = 0;
            // 
            // 姓名
            // 
            this.姓名.DataPropertyName = "PatName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.姓名.DefaultCellStyle = dataGridViewCellStyle2;
            this.姓名.HeaderText = "姓名";
            this.姓名.Name = "姓名";
            this.姓名.ReadOnly = true;
            this.姓名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.姓名.Width = 70;
            // 
            // BeInvoiceNO
            // 
            this.BeInvoiceNO.DataPropertyName = "BeInvoiceNO";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.BeInvoiceNO.DefaultCellStyle = dataGridViewCellStyle3;
            this.BeInvoiceNO.HeaderText = "开始票号";
            this.BeInvoiceNO.Name = "BeInvoiceNO";
            this.BeInvoiceNO.ReadOnly = true;
            this.BeInvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BeInvoiceNO.Width = 80;
            // 
            // EndInvoiceNO
            // 
            this.EndInvoiceNO.DataPropertyName = "EndInvoiceNO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.EndInvoiceNO.DefaultCellStyle = dataGridViewCellStyle4;
            this.EndInvoiceNO.HeaderText = "结束票号";
            this.EndInvoiceNO.Name = "EndInvoiceNO";
            this.EndInvoiceNO.ReadOnly = true;
            this.EndInvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EndInvoiceNO.Width = 80;
            // 
            // CostDate
            // 
            this.CostDate.DataPropertyName = "CostDate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.Format = "yyyy-MM-dd HH:mm:ss";
            this.CostDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.CostDate.HeaderText = "收费时间";
            this.CostDate.Name = "CostDate";
            this.CostDate.ReadOnly = true;
            this.CostDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CostDate.Width = 140;
            // 
            // TotalFee
            // 
            this.TotalFee.DataPropertyName = "TotalFee";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.TotalFee.DefaultCellStyle = dataGridViewCellStyle6;
            this.TotalFee.HeaderText = "票据总额 ";
            this.TotalFee.Name = "TotalFee";
            this.TotalFee.ReadOnly = true;
            this.TotalFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // RoundingFee
            // 
            this.RoundingFee.DataPropertyName = "RoundingFee";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.RoundingFee.DefaultCellStyle = dataGridViewCellStyle7;
            this.RoundingFee.HeaderText = "凑整费";
            this.RoundingFee.Name = "RoundingFee";
            this.RoundingFee.ReadOnly = true;
            this.RoundingFee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RoundingFee.Width = 60;
            // 
            // FrmInvoiceDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 438);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInvoiceDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "票据明细";
            this.Load += new System.EventHandler(this.FrmInvoiceDetail_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoiceDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgInvoiceDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeInvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndInvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CostDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoundingFee;
    }
}