namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmRegList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.dgRegInfo = new EfwControls.CustomControl.DataGrid();
            this.visitno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cureDeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regdocName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.dgRegInfo);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(799, 374);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(617, 330);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "取 消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(509, 330);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "选 择";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dgRegInfo
            // 
            this.dgRegInfo.AllowSortWhenClickColumnHeader = false;
            this.dgRegInfo.AllowUserToAddRows = false;
            this.dgRegInfo.AllowUserToDeleteRows = false;
            this.dgRegInfo.AllowUserToResizeColumns = false;
            this.dgRegInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgRegInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgRegInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgRegInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgRegInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgRegInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.visitno,
            this.regdate,
            this.cureDeptName,
            this.regdocName,
            this.patname});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgRegInfo.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgRegInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgRegInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgRegInfo.HighlightSelectedColumnHeaders = false;
            this.dgRegInfo.Location = new System.Drawing.Point(0, 0);
            this.dgRegInfo.Margin = new System.Windows.Forms.Padding(4);
            this.dgRegInfo.MultiSelect = false;
            this.dgRegInfo.Name = "dgRegInfo";
            this.dgRegInfo.ReadOnly = true;
            this.dgRegInfo.RowHeadersWidth = 30;
            this.dgRegInfo.RowTemplate.Height = 23;
            this.dgRegInfo.SelectAllSignVisible = false;
            this.dgRegInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegInfo.SeqVisible = true;
            this.dgRegInfo.SetCustomStyle = true;
            this.dgRegInfo.Size = new System.Drawing.Size(799, 322);
            this.dgRegInfo.TabIndex = 0;
            this.dgRegInfo.DoubleClick += new System.EventHandler(this.dgRegInfo_DoubleClick);
            this.dgRegInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgRegInfo_KeyDown);
            // 
            // visitno
            // 
            this.visitno.DataPropertyName = "visitno";
            this.visitno.HeaderText = "就诊号";
            this.visitno.Name = "visitno";
            this.visitno.ReadOnly = true;
            this.visitno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.visitno.Width = 120;
            // 
            // regdate
            // 
            this.regdate.DataPropertyName = "regdate";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle3.Format = "yyyy-MM-dd HH:mm:ss";
            this.regdate.DefaultCellStyle = dataGridViewCellStyle3;
            this.regdate.HeaderText = "就诊日期";
            this.regdate.Name = "regdate";
            this.regdate.ReadOnly = true;
            this.regdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.regdate.Width = 200;
            // 
            // cureDeptName
            // 
            this.cureDeptName.DataPropertyName = "regDeptName";
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 11F);
            this.cureDeptName.DefaultCellStyle = dataGridViewCellStyle4;
            this.cureDeptName.HeaderText = "就诊科室";
            this.cureDeptName.Name = "cureDeptName";
            this.cureDeptName.ReadOnly = true;
            this.cureDeptName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.cureDeptName.Width = 120;
            // 
            // regdocName
            // 
            this.regdocName.DataPropertyName = "regdocName";
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 11F);
            this.regdocName.DefaultCellStyle = dataGridViewCellStyle5;
            this.regdocName.HeaderText = "就诊医生";
            this.regdocName.Name = "regdocName";
            this.regdocName.ReadOnly = true;
            this.regdocName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.regdocName.Width = 120;
            // 
            // patname
            // 
            this.patname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.patname.DataPropertyName = "patname";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 11F);
            this.patname.DefaultCellStyle = dataGridViewCellStyle6;
            this.patname.HeaderText = "病人姓名";
            this.patname.Name = "patname";
            this.patname.ReadOnly = true;
            this.patname.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmRegList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 374);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "就诊记录选择";
            this.Load += new System.EventHandler(this.FrmRegList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmRegList_KeyUp);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRegInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.DataGrid dgRegInfo;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn visitno;
        private System.Windows.Forms.DataGridViewTextBoxColumn regdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cureDeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn regdocName;
        private System.Windows.Forms.DataGridViewTextBoxColumn patname;
    }
}