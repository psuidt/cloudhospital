namespace HIS_MemberManage.Winform.ViewForm
{
    partial class FrmChangeCard
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.txtType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtMemberName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.txtName = new DevComponents.DotNetBar.PanelEx();
            this.btnChange = new DevComponents.DotNetBar.ButtonX();
            this.txtNewCard = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtOldCard = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbCash = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbPOS = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtAmount = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.dgChargeList = new EfwControls.CustomControl.DataGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewCardNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.txtName.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChargeList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.txtType);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.txtMemberName);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(715, 44);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // txtType
            // 
            // 
            // 
            // 
            this.txtType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtType.Location = new System.Drawing.Point(269, 12);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(169, 21);
            this.txtType.TabIndex = 4;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX4.Location = new System.Drawing.Point(206, 14);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(56, 18);
            this.labelX4.TabIndex = 3;
            this.labelX4.Text = "帐户类型";
            // 
            // txtMemberName
            // 
            // 
            // 
            // 
            this.txtMemberName.Border.Class = "TextBoxBorder";
            this.txtMemberName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMemberName.Location = new System.Drawing.Point(70, 11);
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.ReadOnly = true;
            this.txtMemberName.Size = new System.Drawing.Size(121, 21);
            this.txtMemberName.TabIndex = 2;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX3.Location = new System.Drawing.Point(14, 14);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(56, 18);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "会员姓名";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(444, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtName
            // 
            this.txtName.CanvasColor = System.Drawing.SystemColors.Control;
            this.txtName.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txtName.Controls.Add(this.expandablePanel1);
            this.txtName.Controls.Add(this.txtAmount);
            this.txtName.Controls.Add(this.labelX5);
            this.txtName.Controls.Add(this.btnChange);
            this.txtName.Controls.Add(this.txtNewCard);
            this.txtName.Controls.Add(this.txtOldCard);
            this.txtName.Controls.Add(this.labelX2);
            this.txtName.Controls.Add(this.labelX1);
            this.txtName.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtName.Location = new System.Drawing.Point(472, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(243, 332);
            this.txtName.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.txtName.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.txtName.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.txtName.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.txtName.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.txtName.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.txtName.Style.GradientAngle = 90;
            this.txtName.TabIndex = 1;
            // 
            // btnChange
            // 
            this.btnChange.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnChange.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnChange.Location = new System.Drawing.Point(158, 209);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnChange.TabIndex = 4;
            this.btnChange.Text = "换卡(&S)";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // txtNewCard
            // 
            // 
            // 
            // 
            this.txtNewCard.Border.Class = "TextBoxBorder";
            this.txtNewCard.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNewCard.Location = new System.Drawing.Point(56, 50);
            this.txtNewCard.Name = "txtNewCard";
            this.txtNewCard.Size = new System.Drawing.Size(177, 21);
            this.txtNewCard.TabIndex = 0;
            // 
            // txtOldCard
            // 
            // 
            // 
            // 
            this.txtOldCard.Border.Class = "TextBoxBorder";
            this.txtOldCard.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtOldCard.Location = new System.Drawing.Point(56, 13);
            this.txtOldCard.Name = "txtOldCard";
            this.txtOldCard.ReadOnly = true;
            this.txtOldCard.Size = new System.Drawing.Size(177, 21);
            this.txtOldCard.TabIndex = 2;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(6, 53);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(44, 18);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "新卡号";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(6, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(44, 18);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "旧卡号";
            // 
            // cbCash
            // 
            this.cbCash.AutoSize = true;
            this.cbCash.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbCash.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbCash.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbCash.Location = new System.Drawing.Point(23, 35);
            this.cbCash.Name = "cbCash";
            this.cbCash.Size = new System.Drawing.Size(51, 18);
            this.cbCash.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbCash.TabIndex = 6;
            this.cbCash.Text = "现金";
            // 
            // cbPOS
            // 
            this.cbPOS.AutoSize = true;
            this.cbPOS.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbPOS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbPOS.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbPOS.Location = new System.Drawing.Point(150, 35);
            this.cbPOS.Name = "cbPOS";
            this.cbPOS.Size = new System.Drawing.Size(45, 16);
            this.cbPOS.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPOS.TabIndex = 7;
            this.cbPOS.Text = "POS";
            // 
            // txtAmount
            // 
            // 
            // 
            // 
            this.txtAmount.Border.Class = "TextBoxBorder";
            this.txtAmount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAmount.Location = new System.Drawing.Point(56, 91);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(177, 21);
            this.txtAmount.TabIndex = 9;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX5.Location = new System.Drawing.Point(6, 94);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(44, 18);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "手续费";
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.cbCash);
            this.expandablePanel1.Controls.Add(this.cbPOS);
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(8, 128);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(225, 64);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 10;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "支付方式";
            // 
            // dgChargeList
            // 
            this.dgChargeList.AllowSortWhenClickColumnHeader = false;
            this.dgChargeList.AllowUserToAddRows = false;
            this.dgChargeList.AllowUserToDeleteRows = false;
            this.dgChargeList.AllowUserToResizeColumns = false;
            this.dgChargeList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dgChargeList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgChargeList.BackgroundColor = System.Drawing.Color.White;
            this.dgChargeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgChargeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgChargeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChargeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.NewCardNO});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgChargeList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgChargeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgChargeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgChargeList.HighlightSelectedColumnHeaders = false;
            this.dgChargeList.Location = new System.Drawing.Point(0, 44);
            this.dgChargeList.Name = "dgChargeList";
            this.dgChargeList.ReadOnly = true;
            this.dgChargeList.RowHeadersWidth = 30;
            this.dgChargeList.RowTemplate.Height = 28;
            this.dgChargeList.SelectAllSignVisible = false;
            this.dgChargeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChargeList.SeqVisible = true;
            this.dgChargeList.SetCustomStyle = false;
            this.dgChargeList.Size = new System.Drawing.Size(472, 332);
            this.dgChargeList.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "OperateDate";
            this.Column1.HeaderText = "换卡时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 120;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "OldCardNO";
            this.Column2.HeaderText = "旧卡号";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 160;
            // 
            // NewCardNO
            // 
            this.NewCardNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NewCardNO.DataPropertyName = "NewCardNO";
            this.NewCardNO.HeaderText = "新卡号";
            this.NewCardNO.Name = "NewCardNO";
            this.NewCardNO.ReadOnly = true;
            this.NewCardNO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FrmChangeCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 376);
            this.Controls.Add(this.dgChargeList);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmChangeCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "换卡";
            this.Load += new System.EventHandler(this.FrmChangeCard_Load);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.txtName.ResumeLayout(false);
            this.txtName.PerformLayout();
            this.expandablePanel1.ResumeLayout(false);
            this.expandablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChargeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx txtName;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtType;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMemberName;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtOldCard;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNewCard;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnChange;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAmount;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbPOS;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbCash;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private EfwControls.CustomControl.DataGrid dgChargeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewCardNO;
    }
}