namespace HIS_OPDoctor.Winform.ViewForm
{
    partial class FrmDiagnosis
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDiagnosis));
            this.dgDisease = new EfwControls.CustomControl.DataGrid();
            this.DiagnosisName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiagnosisCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cdel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DiagnosisRecordID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txtDigShowCard = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.btnFeeAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtFeeDisease = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.barBillMgr = new DevComponents.DotNetBar.Bar();
            this.btnUp = new DevComponents.DotNetBar.ButtonItem();
            this.btnUpdateBill = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisease)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barBillMgr)).BeginInit();
            this.panelEx5.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgDisease
            // 
            this.dgDisease.AllowSortWhenClickColumnHeader = false;
            this.dgDisease.AllowUserToAddRows = false;
            this.dgDisease.AllowUserToDeleteRows = false;
            this.dgDisease.AllowUserToResizeColumns = false;
            this.dgDisease.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgDisease.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDisease.BackgroundColor = System.Drawing.Color.White;
            this.dgDisease.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDisease.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDisease.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiagnosisName,
            this.DiagnosisCode,
            this.cdel,
            this.DiagnosisRecordID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDisease.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDisease.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDisease.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDisease.HighlightSelectedColumnHeaders = false;
            this.dgDisease.Location = new System.Drawing.Point(0, 0);
            this.dgDisease.MultiSelect = false;
            this.dgDisease.Name = "dgDisease";
            this.dgDisease.RowHeadersWidth = 25;
            this.dgDisease.RowTemplate.Height = 23;
            this.dgDisease.SelectAllSignVisible = false;
            this.dgDisease.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDisease.SeqVisible = true;
            this.dgDisease.SetCustomStyle = false;
            this.dgDisease.Size = new System.Drawing.Size(424, 226);
            this.dgDisease.TabIndex = 101;
            this.dgDisease.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDisease_CellClick);
            this.dgDisease.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDisease_CellFormatting);
            // 
            // DiagnosisName
            // 
            this.DiagnosisName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DiagnosisName.DataPropertyName = "DiagnosisName";
            this.DiagnosisName.HeaderText = "诊断名称";
            this.DiagnosisName.MinimumWidth = 100;
            this.DiagnosisName.Name = "DiagnosisName";
            this.DiagnosisName.ReadOnly = true;
            this.DiagnosisName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DiagnosisCode
            // 
            this.DiagnosisCode.DataPropertyName = "DiagnosisCode";
            this.DiagnosisCode.HeaderText = "ICD编码";
            this.DiagnosisCode.Name = "DiagnosisCode";
            this.DiagnosisCode.ReadOnly = true;
            this.DiagnosisCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cdel
            // 
            this.cdel.HeaderText = "移除";
            this.cdel.Name = "cdel";
            // 
            // DiagnosisRecordID
            // 
            this.DiagnosisRecordID.DataPropertyName = "DiagnosisRecordID";
            this.DiagnosisRecordID.HeaderText = "DiagnosisRecordID";
            this.DiagnosisRecordID.Name = "DiagnosisRecordID";
            this.DiagnosisRecordID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiagnosisRecordID.Visible = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.txtDigShowCard);
            this.panelEx3.Controls.Add(this.btnFeeAdd);
            this.panelEx3.Controls.Add(this.labelX2);
            this.panelEx3.Controls.Add(this.btnAdd);
            this.panelEx3.Controls.Add(this.labelX1);
            this.panelEx3.Controls.Add(this.txtFeeDisease);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 253);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(424, 62);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // txtDigShowCard
            // 
            this.txtDigShowCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDigShowCard.Border.Class = "TextBoxBorder";
            this.txtDigShowCard.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDigShowCard.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("txtDigShowCard.ButtonCustom.Image")));
            this.txtDigShowCard.ButtonCustom.Visible = true;
            this.txtDigShowCard.CardColumn = null;
            this.txtDigShowCard.DisplayField = "";
            this.txtDigShowCard.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDigShowCard.IsEnterShowCard = true;
            this.txtDigShowCard.IsNumSelected = false;
            this.txtDigShowCard.IsPage = true;
            this.txtDigShowCard.IsShowLetter = false;
            this.txtDigShowCard.IsShowPage = false;
            this.txtDigShowCard.IsShowSeq = true;
            this.txtDigShowCard.Location = new System.Drawing.Point(110, 6);
            this.txtDigShowCard.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtDigShowCard.MemberField = "";
            this.txtDigShowCard.MemberValue = null;
            this.txtDigShowCard.Name = "txtDigShowCard";
            this.txtDigShowCard.QueryFields = new string[] {
        ""};
            this.txtDigShowCard.QueryFieldsString = "";
            this.txtDigShowCard.SelectedValue = null;
            this.txtDigShowCard.ShowCardColumns = null;
            this.txtDigShowCard.ShowCardDataSource = null;
            this.txtDigShowCard.ShowCardHeight = 100;
            this.txtDigShowCard.ShowCardWidth = 0;
            this.txtDigShowCard.Size = new System.Drawing.Size(224, 21);
            this.txtDigShowCard.TabIndex = 3;
            this.txtDigShowCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDigShowCard_KeyDown);
            // 
            // btnFeeAdd
            // 
            this.btnFeeAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFeeAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFeeAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFeeAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFeeAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnFeeAdd.Image")));
            this.btnFeeAdd.Location = new System.Drawing.Point(340, 34);
            this.btnFeeAdd.Name = "btnFeeAdd";
            this.btnFeeAdd.Size = new System.Drawing.Size(75, 22);
            this.btnFeeAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFeeAdd.TabIndex = 13;
            this.btnFeeAdd.Text = "添加";
            this.btnFeeAdd.Click += new System.EventHandler(this.btnFeeAdd_Click);
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX2.Location = new System.Drawing.Point(22, 36);
            this.labelX2.Name = "labelX2";
            this.labelX2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX2.Size = new System.Drawing.Size(81, 18);
            this.labelX2.TabIndex = 12;
            this.labelX2.Text = "自由录入诊断";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(340, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 22);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 9F);
            this.labelX1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(90)))), ((int)(((byte)(171)))));
            this.labelX1.Location = new System.Drawing.Point(10, 9);
            this.labelX1.Name = "labelX1";
            this.labelX1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelX1.Size = new System.Drawing.Size(93, 18);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "ICD-10标准诊断";
            // 
            // txtFeeDisease
            // 
            this.txtFeeDisease.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtFeeDisease.Border.Class = "TextBoxBorder";
            this.txtFeeDisease.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFeeDisease.Location = new System.Drawing.Point(110, 32);
            this.txtFeeDisease.MaxLength = 50;
            this.txtFeeDisease.Name = "txtFeeDisease";
            this.txtFeeDisease.Size = new System.Drawing.Size(224, 21);
            this.txtFeeDisease.TabIndex = 11;
            this.txtFeeDisease.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFeeDisease_KeyDown);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.barBillMgr);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(424, 27);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 4;
            this.panelEx2.Text = "panelEx2";
            // 
            // barBillMgr
            // 
            this.barBillMgr.AntiAlias = true;
            this.barBillMgr.Dock = System.Windows.Forms.DockStyle.Top;
            this.barBillMgr.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.barBillMgr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.barBillMgr.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnUp,
            this.btnUpdateBill,
            this.btnClose});
            this.barBillMgr.Location = new System.Drawing.Point(0, 0);
            this.barBillMgr.Name = "barBillMgr";
            this.barBillMgr.PaddingLeft = 22;
            this.barBillMgr.Size = new System.Drawing.Size(424, 25);
            this.barBillMgr.Stretch = true;
            this.barBillMgr.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.barBillMgr.TabIndex = 4;
            this.barBillMgr.TabStop = false;
            this.barBillMgr.Text = "bar1";
            // 
            // btnUp
            // 
            this.btnUp.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Name = "btnUp";
            this.btnUp.Text = "上移(U)";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnUpdateBill
            // 
            this.btnUpdateBill.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUpdateBill.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateBill.Image")));
            this.btnUpdateBill.Name = "btnUpdateBill";
            this.btnUpdateBill.Text = "下移(D)";
            this.btnUpdateBill.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx1);
            this.panelEx5.Controls.Add(this.panelEx3);
            this.panelEx5.Controls.Add(this.panelEx2);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 0);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(424, 315);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 5;
            this.panelEx5.Text = "panelEx5";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dgDisease);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(424, 226);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 102;
            this.panelEx1.Text = "panelEx1";
            // 
            // FrmDiagnosis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 315);
            this.Controls.Add(this.panelEx5);
            this.Name = "FrmDiagnosis";
            this.Text = "诊断选择";
            this.OpenWindowBefore += new System.EventHandler(this.FrmDiagnosis_OpenWindowBefore);
            this.Load += new System.EventHandler(this.FrmDiagnosis_Load);
            this.VisibleChanged += new System.EventHandler(this.FrmDiagnosis_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgDisease)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barBillMgr)).EndInit();
            this.panelEx5.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX labelX1;
        private EfwControls.CustomControl.TextBoxCard txtDigShowCard;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.ButtonX btnFeeAdd;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFeeDisease;
        private EfwControls.CustomControl.DataGrid dgDisease;
        private DevComponents.DotNetBar.Bar barBillMgr;
        private DevComponents.DotNetBar.ButtonItem btnUpdateBill;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonItem btnUp;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagnosisRecordID;
        private System.Windows.Forms.DataGridViewButtonColumn cdel;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagnosisCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagnosisName;
    }
}