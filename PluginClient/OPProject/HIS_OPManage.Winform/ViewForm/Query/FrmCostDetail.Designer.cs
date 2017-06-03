namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmCostDetail
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgCostDetail = new EfwControls.CustomControl.DataGrid();
            this.FeeNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PatName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiniUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PresAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RetailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCostDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgCostDetail);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(986, 381);
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
            // dgCostDetail
            // 
            this.dgCostDetail.AllowSortWhenClickColumnHeader = false;
            this.dgCostDetail.AllowUserToAddRows = false;
            this.dgCostDetail.AllowUserToResizeColumns = false;
            this.dgCostDetail.AllowUserToResizeRows = false;
            this.dgCostDetail.BackgroundColor = System.Drawing.Color.White;
            this.dgCostDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCostDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCostDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCostDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FeeNO,
            this.InvoiceNO,
            this.PatName,
            this.ExecDeptName,
            this.ItemName,
            this.Spec,
            this.PackNum,
            this.PackUnit,
            this.MiniNum,
            this.MiniUnit,
            this.PresAmount,
            this.RetailPrice,
            this.Column8});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCostDetail.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgCostDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCostDetail.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCostDetail.HighlightSelectedColumnHeaders = false;
            this.dgCostDetail.Location = new System.Drawing.Point(0, 0);
            this.dgCostDetail.Name = "dgCostDetail";
            this.dgCostDetail.ReadOnly = true;
            this.dgCostDetail.RowHeadersWidth = 30;
            this.dgCostDetail.RowTemplate.Height = 23;
            this.dgCostDetail.SelectAllSignVisible = false;
            this.dgCostDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCostDetail.SeqVisible = true;
            this.dgCostDetail.SetCustomStyle = false;
            this.dgCostDetail.Size = new System.Drawing.Size(986, 381);
            this.dgCostDetail.TabIndex = 1;
            // 
            // FeeNO
            // 
            this.FeeNO.DataPropertyName = "FeeNO";
            this.FeeNO.HeaderText = "处方流水号";
            this.FeeNO.Name = "FeeNO";
            this.FeeNO.ReadOnly = true;
            this.FeeNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FeeNO.Visible = false;
            this.FeeNO.Width = 140;
            // 
            // InvoiceNO
            // 
            this.InvoiceNO.DataPropertyName = "InvoiceNO";
            this.InvoiceNO.HeaderText = "发票号";
            this.InvoiceNO.Name = "InvoiceNO";
            this.InvoiceNO.ReadOnly = true;
            this.InvoiceNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.InvoiceNO.Width = 80;
            // 
            // PatName
            // 
            this.PatName.DataPropertyName = "PatName";
            this.PatName.HeaderText = "病人姓名";
            this.PatName.Name = "PatName";
            this.PatName.ReadOnly = true;
            this.PatName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PatName.Width = 70;
            // 
            // ExecDeptName
            // 
            this.ExecDeptName.DataPropertyName = "ExecDeptName";
            this.ExecDeptName.HeaderText = "执行科室";
            this.ExecDeptName.Name = "ExecDeptName";
            this.ExecDeptName.ReadOnly = true;
            this.ExecDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ExecDeptName.Width = 80;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle2;
            this.ItemName.HeaderText = "项目名称";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Spec.DefaultCellStyle = dataGridViewCellStyle3;
            this.Spec.HeaderText = "规格";
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Spec.Width = 120;
            // 
            // PackNum
            // 
            this.PackNum.DataPropertyName = "PackNum";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.PackNum.DefaultCellStyle = dataGridViewCellStyle4;
            this.PackNum.HeaderText = "包装数量";
            this.PackNum.Name = "PackNum";
            this.PackNum.ReadOnly = true;
            this.PackNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackNum.Width = 60;
            // 
            // PackUnit
            // 
            this.PackUnit.DataPropertyName = "PackUnit";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PackUnit.DefaultCellStyle = dataGridViewCellStyle5;
            this.PackUnit.HeaderText = "包装单位";
            this.PackUnit.Name = "PackUnit";
            this.PackUnit.ReadOnly = true;
            this.PackUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PackUnit.Width = 60;
            // 
            // MiniNum
            // 
            this.MiniNum.DataPropertyName = "MiniNum";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            this.MiniNum.DefaultCellStyle = dataGridViewCellStyle6;
            this.MiniNum.HeaderText = "基本数量";
            this.MiniNum.Name = "MiniNum";
            this.MiniNum.ReadOnly = true;
            this.MiniNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniNum.Width = 60;
            // 
            // MiniUnit
            // 
            this.MiniUnit.DataPropertyName = "MiniUnit";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MiniUnit.DefaultCellStyle = dataGridViewCellStyle7;
            this.MiniUnit.HeaderText = "基本单位";
            this.MiniUnit.Name = "MiniUnit";
            this.MiniUnit.ReadOnly = true;
            this.MiniUnit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiniUnit.Width = 60;
            // 
            // PresAmount
            // 
            this.PresAmount.DataPropertyName = "PresAmount";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.PresAmount.DefaultCellStyle = dataGridViewCellStyle8;
            this.PresAmount.HeaderText = "付数";
            this.PresAmount.Name = "PresAmount";
            this.PresAmount.ReadOnly = true;
            this.PresAmount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PresAmount.Width = 35;
            // 
            // RetailPrice
            // 
            this.RetailPrice.DataPropertyName = "RetailPrice";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N4";
            dataGridViewCellStyle9.NullValue = null;
            this.RetailPrice.DefaultCellStyle = dataGridViewCellStyle9;
            this.RetailPrice.HeaderText = "零售价";
            this.RetailPrice.Name = "RetailPrice";
            this.RetailPrice.ReadOnly = true;
            this.RetailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.RetailPrice.Width = 80;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "TotalFee";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column8.HeaderText = "项目金额";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 80;
            // 
            // FrmCostDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 381);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCostDetail";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "详细信息";
            this.Shown += new System.EventHandler(this.FrmCostDetail_Shown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCostDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgCostDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PatName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiniUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PresAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn RetailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}