namespace HIS_OPDoctor.Winform.ViewForm
{
    partial class FrmBedInfo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBedInfo));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dgBed = new EfwControls.CustomControl.DataGrid();
            this.WardID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsUse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoUse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btnBedQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBed)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgBed);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(538, 391);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // dgBed
            // 
            this.dgBed.AllowSortWhenClickColumnHeader = false;
            this.dgBed.AllowUserToAddRows = false;
            this.dgBed.AllowUserToDeleteRows = false;
            this.dgBed.AllowUserToResizeColumns = false;
            this.dgBed.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgBed.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgBed.BackgroundColor = System.Drawing.Color.White;
            this.dgBed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgBed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgBed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WardID,
            this.WardName,
            this.Total,
            this.IsUse,
            this.NoUse});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBed.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgBed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBed.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgBed.HighlightSelectedColumnHeaders = false;
            this.dgBed.Location = new System.Drawing.Point(0, 25);
            this.dgBed.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dgBed.MultiSelect = false;
            this.dgBed.Name = "dgBed";
            this.dgBed.ReadOnly = true;
            this.dgBed.RowHeadersVisible = false;
            this.dgBed.RowHeadersWidth = 30;
            this.dgBed.RowTemplate.Height = 23;
            this.dgBed.SelectAllSignVisible = false;
            this.dgBed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgBed.SeqVisible = false;
            this.dgBed.SetCustomStyle = true;
            this.dgBed.Size = new System.Drawing.Size(538, 366);
            this.dgBed.TabIndex = 4;
            // 
            // WardID
            // 
            this.WardID.DataPropertyName = "WardID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.WardID.DefaultCellStyle = dataGridViewCellStyle3;
            this.WardID.HeaderText = "编码";
            this.WardID.Name = "WardID";
            this.WardID.ReadOnly = true;
            this.WardID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WardID.Width = 80;
            // 
            // WardName
            // 
            this.WardName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.WardName.DataPropertyName = "WardName";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.WardName.DefaultCellStyle = dataGridViewCellStyle4;
            this.WardName.HeaderText = "病区名称";
            this.WardName.MinimumWidth = 120;
            this.WardName.Name = "WardName";
            this.WardName.ReadOnly = true;
            this.WardName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Total
            // 
            this.Total.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Total.DataPropertyName = "Total";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Total.DefaultCellStyle = dataGridViewCellStyle5;
            this.Total.HeaderText = "病区总床位数";
            this.Total.MinimumWidth = 50;
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            this.Total.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsUse
            // 
            this.IsUse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.IsUse.DataPropertyName = "IsUse";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.IsUse.DefaultCellStyle = dataGridViewCellStyle6;
            this.IsUse.HeaderText = "已使用床位数";
            this.IsUse.MinimumWidth = 50;
            this.IsUse.Name = "IsUse";
            this.IsUse.ReadOnly = true;
            this.IsUse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsUse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NoUse
            // 
            this.NoUse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NoUse.DataPropertyName = "NoUse";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NoUse.DefaultCellStyle = dataGridViewCellStyle7;
            this.NoUse.HeaderText = "空床位数";
            this.NoUse.MinimumWidth = 50;
            this.NoUse.Name = "NoUse";
            this.NoUse.ReadOnly = true;
            this.NoUse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NoUse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.bar2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(538, 25);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // bar2
            // 
            this.bar2.AntiAlias = true;
            this.bar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnBedQuery,
            this.btnClose});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(538, 25);
            this.bar2.Stretch = true;
            this.bar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar2.TabIndex = 36;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btnBedQuery
            // 
            this.btnBedQuery.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnBedQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnBedQuery.Image")));
            this.btnBedQuery.Name = "btnBedQuery";
            this.btnBedQuery.Text = "查询(&Q)";
            this.btnBedQuery.Click += new System.EventHandler(this.btnBedQuery_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmBedInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 391);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmBedInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "床位情况查询";
            this.Load += new System.EventHandler(this.FrmBedInfo_Load);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBed)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btnBedQuery;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private EfwControls.CustomControl.DataGrid dgBed;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardID;
        private System.Windows.Forms.DataGridViewTextBoxColumn WardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUse;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoUse;
    }
}