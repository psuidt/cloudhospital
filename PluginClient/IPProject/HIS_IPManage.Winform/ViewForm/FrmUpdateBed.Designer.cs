namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmUpdateBed
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            EfwControls.CustomControl.DataGridViewSelectionCard dataGridViewSelectionCard1 = new EfwControls.CustomControl.DataGridViewSelectionCard();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.grdBedFreeList = new EfwControls.CustomControl.GridBoxCard();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnBedSave = new DevComponents.DotNetBar.ButtonX();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtBedNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.chkIsPlus = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtNurseID = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.txtDoctorID = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.txtWardNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.frmBed = new EfwControls.CustomControl.frmForm(this.components);
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBedFreeList)).BeginInit();
            this.panelEx3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.grdBedFreeList);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.panelEx2);
            this.panelEx1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(483, 522);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // grdBedFreeList
            // 
            this.grdBedFreeList.AllowSortWhenClickColumnHeader = false;
            this.grdBedFreeList.AllowUserToAddRows = false;
            this.grdBedFreeList.AllowUserToDeleteRows = false;
            this.grdBedFreeList.AllowUserToResizeColumns = false;
            this.grdBedFreeList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.grdBedFreeList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.grdBedFreeList.BackgroundColor = System.Drawing.Color.White;
            this.grdBedFreeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdBedFreeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdBedFreeList.ColumnHeadersHeight = 30;
            this.grdBedFreeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdBedFreeList.DefaultCellStyle = dataGridViewCellStyle5;
            this.grdBedFreeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBedFreeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdBedFreeList.HideSelectionCardWhenCustomInput = false;
            this.grdBedFreeList.HighlightSelectedColumnHeaders = false;
            this.grdBedFreeList.IsInputNumSelectedCard = false;
            this.grdBedFreeList.IsShowLetter = false;
            this.grdBedFreeList.IsShowPage = true;
            this.grdBedFreeList.Location = new System.Drawing.Point(0, 106);
            this.grdBedFreeList.Margin = new System.Windows.Forms.Padding(2);
            this.grdBedFreeList.Name = "grdBedFreeList";
            this.grdBedFreeList.ReadOnly = true;
            this.grdBedFreeList.RowHeadersWidth = 30;
            this.grdBedFreeList.SelectAllSignVisible = false;
            dataGridViewSelectionCard1.BindColumnIndex = 0;
            dataGridViewSelectionCard1.CardColumn = null;
            dataGridViewSelectionCard1.CardSize = new System.Drawing.Size(350, 276);
            dataGridViewSelectionCard1.DataSource = null;
            dataGridViewSelectionCard1.FilterResult = null;
            dataGridViewSelectionCard1.IsPage = true;
            dataGridViewSelectionCard1.Memo = null;
            dataGridViewSelectionCard1.PageTotalRecord = 0;
            dataGridViewSelectionCard1.QueryFields = new string[] {
        ""};
            dataGridViewSelectionCard1.QueryFieldsString = "";
            dataGridViewSelectionCard1.SelectCardFilterType = EfwControls.CustomControl.MatchModes.ByAnyString;
            dataGridViewSelectionCard1.ShowCardColumns = null;
            this.grdBedFreeList.SelectionCards = new EfwControls.CustomControl.DataGridViewSelectionCard[] {
        dataGridViewSelectionCard1};
            this.grdBedFreeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdBedFreeList.SelectionNumKeyBoards = null;
            this.grdBedFreeList.SeqVisible = true;
            this.grdBedFreeList.SetCustomStyle = false;
            this.grdBedFreeList.Size = new System.Drawing.Size(483, 381);
            this.grdBedFreeList.TabIndex = 11;
            this.grdBedFreeList.SelectCardRowSelected += new EfwControls.CustomControl.OnSelectCardRowSelectedHandle(this.grdBedFreeList_SelectCardRowSelected);
            this.grdBedFreeList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBedFreeList_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "ItemName";
            this.Column1.HeaderText = "床位费";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 220;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "ItemAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "UnitPrice";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N4";
            dataGridViewCellStyle4.NullValue = null;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "单价";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FeeType";
            this.Column2.FalseValue = "0";
            this.Column2.HeaderText = "包床";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.TrueValue = "1";
            this.Column2.Width = 50;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btnCancel);
            this.panelEx3.Controls.Add(this.btnBedSave);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx3.Location = new System.Drawing.Point(0, 487);
            this.panelEx3.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(483, 35);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 12;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(378, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 22);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "退出(&C)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnBedSave
            // 
            this.btnBedSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBedSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnBedSave.Location = new System.Drawing.Point(298, 6);
            this.btnBedSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnBedSave.Name = "btnBedSave";
            this.btnBedSave.Size = new System.Drawing.Size(75, 22);
            this.btnBedSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnBedSave.TabIndex = 6;
            this.btnBedSave.Text = "保存(&S)";
            this.btnBedSave.Click += new System.EventHandler(this.btnBedSave_Click);
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.Location = new System.Drawing.Point(0, 104);
            this.bar1.Margin = new System.Windows.Forms.Padding(2);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(483, 2);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 10;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtBedNo);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.chkIsPlus);
            this.panelEx2.Controls.Add(this.txtNurseID);
            this.panelEx2.Controls.Add(this.txtDoctorID);
            this.panelEx2.Controls.Add(this.txtWardNo);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(2);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(483, 104);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 9;
            // 
            // txtBedNo
            // 
            // 
            // 
            // 
            this.txtBedNo.Border.Class = "TextBoxBorder";
            this.txtBedNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBedNo.Location = new System.Drawing.Point(300, 28);
            this.txtBedNo.Name = "txtBedNo";
            this.txtBedNo.Size = new System.Drawing.Size(101, 21);
            this.txtBedNo.TabIndex = 3;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(240, 28);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(55, 22);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "床位号";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // chkIsPlus
            // 
            // 
            // 
            // 
            this.chkIsPlus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkIsPlus.Location = new System.Drawing.Point(407, 30);
            this.chkIsPlus.Name = "chkIsPlus";
            this.chkIsPlus.Size = new System.Drawing.Size(60, 18);
            this.chkIsPlus.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkIsPlus.TabIndex = 2;
            this.chkIsPlus.Text = "加床";
            // 
            // txtNurseID
            // 
            // 
            // 
            // 
            this.txtNurseID.Border.Class = "TextBoxBorder";
            this.txtNurseID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNurseID.ButtonCustom.Visible = true;
            this.txtNurseID.CardColumn = null;
            this.txtNurseID.DisplayField = "";
            this.txtNurseID.IsEnterShowCard = false;
            this.txtNurseID.IsNumSelected = true;
            this.txtNurseID.IsPage = true;
            this.txtNurseID.IsShowLetter = false;
            this.txtNurseID.IsShowPage = false;
            this.txtNurseID.IsShowSeq = true;
            this.txtNurseID.Location = new System.Drawing.Point(300, 54);
            this.txtNurseID.Margin = new System.Windows.Forms.Padding(2);
            this.txtNurseID.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtNurseID.MemberField = "";
            this.txtNurseID.MemberValue = null;
            this.txtNurseID.Name = "txtNurseID";
            this.txtNurseID.QueryFields = new string[] {
        ""};
            this.txtNurseID.QueryFieldsString = "";
            this.txtNurseID.SelectedValue = null;
            this.txtNurseID.ShowCardColumns = null;
            this.txtNurseID.ShowCardDataSource = null;
            this.txtNurseID.ShowCardHeight = 0;
            this.txtNurseID.ShowCardWidth = 0;
            this.txtNurseID.Size = new System.Drawing.Size(153, 21);
            this.txtNurseID.TabIndex = 5;
            this.txtNurseID.AfterSelectedRow += new EfwControls.CustomControl.AfterSelectedRowHandler(this.txtNurseID_AfterSelectedRow);
            // 
            // txtDoctorID
            // 
            // 
            // 
            // 
            this.txtDoctorID.Border.Class = "TextBoxBorder";
            this.txtDoctorID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDoctorID.ButtonCustom.Visible = true;
            this.txtDoctorID.CardColumn = null;
            this.txtDoctorID.DisplayField = "";
            this.txtDoctorID.IsEnterShowCard = false;
            this.txtDoctorID.IsNumSelected = true;
            this.txtDoctorID.IsPage = true;
            this.txtDoctorID.IsShowLetter = false;
            this.txtDoctorID.IsShowPage = false;
            this.txtDoctorID.IsShowSeq = true;
            this.txtDoctorID.Location = new System.Drawing.Point(88, 54);
            this.txtDoctorID.Margin = new System.Windows.Forms.Padding(2);
            this.txtDoctorID.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.txtDoctorID.MemberField = "";
            this.txtDoctorID.MemberValue = null;
            this.txtDoctorID.Name = "txtDoctorID";
            this.txtDoctorID.QueryFields = new string[] {
        ""};
            this.txtDoctorID.QueryFieldsString = "";
            this.txtDoctorID.SelectedValue = null;
            this.txtDoctorID.ShowCardColumns = null;
            this.txtDoctorID.ShowCardDataSource = null;
            this.txtDoctorID.ShowCardHeight = 0;
            this.txtDoctorID.ShowCardWidth = 0;
            this.txtDoctorID.Size = new System.Drawing.Size(130, 21);
            this.txtDoctorID.TabIndex = 4;
            // 
            // txtWardNo
            // 
            // 
            // 
            // 
            this.txtWardNo.Border.Class = "TextBoxBorder";
            this.txtWardNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWardNo.Location = new System.Drawing.Point(88, 28);
            this.txtWardNo.Name = "txtWardNo";
            this.txtWardNo.Size = new System.Drawing.Size(130, 21);
            this.txtWardNo.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(240, 54);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 22);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "责任护士";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(28, 54);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(55, 22);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "责任医生";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(28, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(55, 22);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "房间号";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // frmBed
            // 
            this.frmBed.IsSkip = true;
            // 
            // FrmUpdateBed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 522);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdateBed";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病床维护";
            this.Load += new System.EventHandler(this.FrmUpdateBed_Load);
            this.Shown += new System.EventHandler(this.FrmUpdateBed_Shown);
            this.VisibleChanged += new System.EventHandler(this.FrmUpdateBed_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUpdateBed_KeyDown);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBedFreeList)).EndInit();
            this.panelEx3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.frmForm frmBed;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBedNo;
        private DevComponents.DotNetBar.LabelX labelX5;
        private EfwControls.CustomControl.TextBoxCard txtNurseID;
        private EfwControls.CustomControl.TextBoxCard txtDoctorID;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkIsPlus;
        private DevComponents.DotNetBar.Controls.TextBoxX txtWardNo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Bar bar1;
        //private DevComponents.DotNetBar.ButtonItem btnAdd;
        //private DevComponents.DotNetBar.ButtonItem btnDel;
        private EfwControls.CustomControl.GridBoxCard grdBedFreeList;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnBedSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
    }
}