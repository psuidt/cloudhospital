namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmSinglePaymentManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSinglePaymentManage));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.TreePayInfoList = new DevComponents.AdvTree.AdvTree();
            this.UnUpload = new DevComponents.AdvTree.Node();
            this.Uploaded = new DevComponents.AdvTree.Node();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.btnQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btnPayMent = new DevComponents.DotNetBar.ButtonItem();
            this.btnPrint = new DevComponents.DotNetBar.ButtonItem();
            this.btnClose = new DevComponents.DotNetBar.ButtonItem();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.ucAccountTab1 = new HIS_IPManage.Winform.ViewForm.UCAccountTab();
            this.statDTime = new EfwControls.CustomControl.StatDateTime();
            this.tbcStaff = new EfwControls.CustomControl.TextBoxCard(this.components);
            this.controlContainerItem1 = new DevComponents.DotNetBar.ControlContainerItem();
            this.controlContainerItem2 = new DevComponents.DotNetBar.ControlContainerItem();
            this.panelEx1.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TreePayInfoList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.bar1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.ucAccountTab1);
            this.panelEx1.Controls.Add(this.expandableSplitter1);
            this.panelEx1.Controls.Add(this.panelEx3);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1170, 515);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // expandableSplitter1
            // 
            this.expandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.expandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.expandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.expandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.expandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.expandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.expandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.expandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.expandableSplitter1.Location = new System.Drawing.Point(173, 27);
            this.expandableSplitter1.Name = "expandableSplitter1";
            this.expandableSplitter1.Size = new System.Drawing.Size(4, 488);
            this.expandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.expandableSplitter1.TabIndex = 13;
            this.expandableSplitter1.TabStop = false;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.expandablePanel1);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx3.Location = new System.Drawing.Point(0, 27);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(173, 488);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 12;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.TreePayInfoList);
            this.expandablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.expandablePanel1.ExpandButtonVisible = false;
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(0, 0);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(173, 488);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 1;
            this.expandablePanel1.TitleHeight = 19;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "缴款记录";
            // 
            // TreePayInfoList
            // 
            this.TreePayInfoList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.TreePayInfoList.AllowDrop = true;
            this.TreePayInfoList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.TreePayInfoList.BackgroundStyle.Class = "TreeBorderKey";
            this.TreePayInfoList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.TreePayInfoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreePayInfoList.Location = new System.Drawing.Point(0, 19);
            this.TreePayInfoList.Margin = new System.Windows.Forms.Padding(2);
            this.TreePayInfoList.Name = "TreePayInfoList";
            this.TreePayInfoList.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.UnUpload,
            this.Uploaded});
            this.TreePayInfoList.NodesConnector = this.nodeConnector1;
            this.TreePayInfoList.NodeStyle = this.elementStyle1;
            this.TreePayInfoList.PathSeparator = ";";
            this.TreePayInfoList.Size = new System.Drawing.Size(173, 469);
            this.TreePayInfoList.Styles.Add(this.elementStyle1);
            this.TreePayInfoList.TabIndex = 1;
            this.TreePayInfoList.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.TreePayInfoList_NodeClick);
            // 
            // UnUpload
            // 
            this.UnUpload.Expanded = true;
            this.UnUpload.Name = "UnUpload";
            this.UnUpload.Text = "未缴款";
            // 
            // Uploaded
            // 
            this.Uploaded.Name = "Uploaded";
            this.Uploaded.Text = "已缴款";
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Controls.Add(this.statDTime);
            this.bar1.Controls.Add(this.tbcStaff);
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.controlContainerItem1,
            this.labelItem2,
            this.controlContainerItem2,
            this.btnQuery,
            this.btnPayMent,
            this.btnPrint,
            this.btnClose});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Margin = new System.Windows.Forms.Padding(2);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1170, 27);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 11;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "缴款时间";
            // 
            // labelItem2
            // 
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "收费员";
            // 
            // btnQuery
            // 
            this.btnQuery.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnQuery.FontBold = true;
            this.btnQuery.Image = global::HIS_IPManage.Winform.Properties.Resources.搜索;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnPayMent
            // 
            this.btnPayMent.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPayMent.FontBold = true;
            this.btnPayMent.Image = global::HIS_IPManage.Winform.Properties.Resources.收费;
            this.btnPayMent.Name = "btnPayMent";
            this.btnPayMent.Text = "缴款(&A)";
            this.btnPayMent.Click += new System.EventHandler(this.btnPayMent_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPrint.FontBold = true;
            this.btnPrint.Image = global::HIS_IPManage.Winform.Properties.Resources.打印;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Text = "打印(&P)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnClose.FontBold = true;
            this.btnClose.Image = global::HIS_IPManage.Winform.Properties.Resources.关闭;
            this.btnClose.Name = "btnClose";
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabItem2
            // 
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "结算缴款";
            // 
            // tabItem1
            // 
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "tabItem1";
            // 
            // tabItem3
            // 
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "预交金缴款";
            // 
            // ucAccountTab1
            // 
            this.ucAccountTab1.AccountId = 0;
            this.ucAccountTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAccountTab1.DTotalFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.DTotalPaymentFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.DTotalRoundFee = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ucAccountTab1.frmName = "UCAccountTab";
            this.ucAccountTab1.InvokeController = null;
            this.ucAccountTab1.Location = new System.Drawing.Point(177, 27);
            this.ucAccountTab1.Name = "ucAccountTab1";
            this.ucAccountTab1.Size = new System.Drawing.Size(993, 488);
            this.ucAccountTab1.TabIndex = 16;
            // 
            // statDTime
            // 
            this.statDTime.BackColor = System.Drawing.Color.Transparent;
            this.statDTime.DateFormat = "yyyy-MM-dd";
            this.statDTime.DateWidth = 120;
            this.statDTime.Font = new System.Drawing.Font("宋体", 9.5F);
            this.statDTime.Location = new System.Drawing.Point(59, 2);
            this.statDTime.Name = "statDTime";
            this.statDTime.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.statDTime.ShowStyle = EfwControls.CustomControl.showStyle.horizontal;
            this.statDTime.Size = new System.Drawing.Size(269, 22);
            this.statDTime.TabIndex = 4;
            // 
            // tbcStaff
            // 
            // 
            // 
            // 
            this.tbcStaff.Border.Class = "TextBoxBorder";
            this.tbcStaff.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbcStaff.ButtonCustom.Image = ((System.Drawing.Image)(resources.GetObject("tbcStaff.ButtonCustom.Image")));
            this.tbcStaff.ButtonCustom.Visible = true;
            this.tbcStaff.CardColumn = null;
            this.tbcStaff.DisplayField = "";
            this.tbcStaff.Enabled = false;
            this.tbcStaff.IsEnterShowCard = true;
            this.tbcStaff.IsNumSelected = false;
            this.tbcStaff.IsPage = true;
            this.tbcStaff.IsShowLetter = false;
            this.tbcStaff.IsShowPage = false;
            this.tbcStaff.IsShowSeq = true;
            this.tbcStaff.Location = new System.Drawing.Point(376, 3);
            this.tbcStaff.Margin = new System.Windows.Forms.Padding(2);
            this.tbcStaff.MatchMode = EfwControls.CustomControl.MatchModes.ByAnyString;
            this.tbcStaff.MemberField = "";
            this.tbcStaff.MemberValue = null;
            this.tbcStaff.Name = "tbcStaff";
            this.tbcStaff.QueryFields = new string[] {
        ""};
            this.tbcStaff.QueryFieldsString = "";
            this.tbcStaff.SelectedValue = null;
            this.tbcStaff.ShowCardColumns = null;
            this.tbcStaff.ShowCardDataSource = null;
            this.tbcStaff.ShowCardHeight = 0;
            this.tbcStaff.ShowCardWidth = 0;
            this.tbcStaff.Size = new System.Drawing.Size(141, 21);
            this.tbcStaff.TabIndex = 11;
            // 
            // controlContainerItem1
            // 
            this.controlContainerItem1.AllowItemResize = false;
            this.controlContainerItem1.Control = this.statDTime;
            this.controlContainerItem1.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem1.Name = "controlContainerItem1";
            // 
            // controlContainerItem2
            // 
            this.controlContainerItem2.AllowItemResize = false;
            this.controlContainerItem2.Control = this.tbcStaff;
            this.controlContainerItem2.MenuVisibility = DevComponents.DotNetBar.eMenuVisibility.VisibleAlways;
            this.controlContainerItem2.Name = "controlContainerItem2";
            // 
            // FrmSinglePaymentManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 515);
            this.ControlBox = false;
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSinglePaymentManage";
            this.Text = "交款单管理";
            this.OpenWindowBefore += new System.EventHandler(this.FrmSinglePaymentManage_OpenWindowBefore);
            this.panelEx1.ResumeLayout(false);
            this.panelEx3.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TreePayInfoList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.bar1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private EfwControls.CustomControl.StatDateTime statDTime;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private EfwControls.CustomControl.TextBoxCard tbcStaff;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.ButtonItem btnQuery;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem1;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.ButtonItem btnPayMent;
        private DevComponents.DotNetBar.ButtonItem btnPrint;
        private DevComponents.DotNetBar.ButtonItem btnClose;
        private DevComponents.DotNetBar.ControlContainerItem controlContainerItem2;
        private DevComponents.DotNetBar.ExpandableSplitter expandableSplitter1;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.AdvTree.AdvTree TreePayInfoList;
        private DevComponents.AdvTree.Node UnUpload;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.Node Uploaded;
        private UCAccountTab ucAccountTab1;
    }
}