namespace HIS_OPDoctor.Winform.ViewForm
{
    partial class FrmOMRTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOMRTemplate));
            this.pParent = new DevComponents.DotNetBar.PanelEx();
            this.pContent = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.tvOMRTpl = new DevComponents.AdvTree.AdvTree();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.node4 = new DevComponents.AdvTree.Node();
            this.nodeConnector5 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle5 = new DevComponents.DotNetBar.ElementStyle();
            this.ptop = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.txtMouldName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.radOPerson = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radODept = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.radOAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.itemNewSub = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemMidNew = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMidNewSub = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEditMidDrug = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMidDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pParent.SuspendLayout();
            this.pContent.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvOMRTpl)).BeginInit();
            this.ptop.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.cmTree.SuspendLayout();
            this.MenuMid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pParent
            // 
            this.pParent.CanvasColor = System.Drawing.SystemColors.Control;
            this.pParent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pParent.Controls.Add(this.pContent);
            this.pParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pParent.Location = new System.Drawing.Point(0, 0);
            this.pParent.Name = "pParent";
            this.pParent.Size = new System.Drawing.Size(503, 370);
            this.pParent.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pParent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pParent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pParent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pParent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pParent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pParent.Style.GradientAngle = 90;
            this.pParent.TabIndex = 0;
            // 
            // pContent
            // 
            this.pContent.CanvasColor = System.Drawing.SystemColors.Control;
            this.pContent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pContent.Controls.Add(this.panelEx2);
            this.pContent.Controls.Add(this.ptop);
            this.pContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pContent.Location = new System.Drawing.Point(0, 0);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(503, 370);
            this.pContent.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pContent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pContent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.pContent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pContent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.pContent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pContent.Style.GradientAngle = 90;
            this.pContent.TabIndex = 1;
            this.pContent.Text = "panelEx2";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx4);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 68);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(503, 302);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "panelEx2";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.tvOMRTpl);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(503, 302);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 1;
            this.panelEx4.Text = "panelEx4";
            // 
            // tvOMRTpl
            // 
            this.tvOMRTpl.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.tvOMRTpl.AllowDrop = true;
            this.tvOMRTpl.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.tvOMRTpl.BackgroundStyle.Class = "TreeBorderKey";
            this.tvOMRTpl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tvOMRTpl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOMRTpl.DragDropEnabled = false;
            this.tvOMRTpl.DragDropNodeCopyEnabled = false;
            this.tvOMRTpl.ImageList = this.imgList;
            this.tvOMRTpl.Location = new System.Drawing.Point(0, 0);
            this.tvOMRTpl.Name = "tvOMRTpl";
            this.tvOMRTpl.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node4});
            this.tvOMRTpl.NodesConnector = this.nodeConnector5;
            this.tvOMRTpl.NodeStyle = this.elementStyle5;
            this.tvOMRTpl.PathSeparator = ";";
            this.tvOMRTpl.Size = new System.Drawing.Size(503, 302);
            this.tvOMRTpl.Styles.Add(this.elementStyle5);
            this.tvOMRTpl.TabIndex = 4;
            this.tvOMRTpl.Text = "advTree2";
            this.tvOMRTpl.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.tvOMRTpl_NodeClick);
            this.tvOMRTpl.Click += new System.EventHandler(this.tvOMRTpl_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "包类型.png");
            this.imgList.Images.SetKeyName(1, "Order.png");
            // 
            // node4
            // 
            this.node4.Name = "node4";
            // 
            // nodeConnector5
            // 
            this.nodeConnector5.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle5
            // 
            this.elementStyle5.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle5.Name = "elementStyle5";
            this.elementStyle5.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // ptop
            // 
            this.ptop.CanvasColor = System.Drawing.SystemColors.Control;
            this.ptop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ptop.Controls.Add(this.panelEx1);
            this.ptop.Controls.Add(this.panelEx5);
            this.ptop.Dock = System.Windows.Forms.DockStyle.Top;
            this.ptop.Location = new System.Drawing.Point(0, 0);
            this.ptop.Name = "ptop";
            this.ptop.Size = new System.Drawing.Size(503, 68);
            this.ptop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.ptop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.ptop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.ptop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.ptop.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ptop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.ptop.Style.GradientAngle = 90;
            this.ptop.TabIndex = 0;
            this.ptop.Text = "panelEx1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.txtMouldName);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(503, 33);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
            this.buttonX1.Location = new System.Drawing.Point(355, 6);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 22);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 43;
            this.buttonX1.Text = "关闭(&C)";
            this.buttonX1.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(275, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 22);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 42;
            this.btnOK.Text = "保存(&S)";
            this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtMouldName
            // 
            // 
            // 
            // 
            this.txtMouldName.Border.Class = "TextBoxBorder";
            this.txtMouldName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMouldName.Location = new System.Drawing.Point(67, 7);
            this.txtMouldName.Name = "txtMouldName";
            this.txtMouldName.Size = new System.Drawing.Size(195, 21);
            this.txtMouldName.TabIndex = 41;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(7, 9);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 18);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "模板名称";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.labelX1);
            this.panelEx5.Controls.Add(this.radOPerson);
            this.panelEx5.Controls.Add(this.radODept);
            this.panelEx5.Controls.Add(this.radOAll);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelEx5.Location = new System.Drawing.Point(0, 33);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(503, 35);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 3;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 18);
            this.labelX1.TabIndex = 30;
            this.labelX1.Text = "模板级别";
            // 
            // radOPerson
            // 
            this.radOPerson.AutoSize = true;
            // 
            // 
            // 
            this.radOPerson.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radOPerson.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radOPerson.Checked = true;
            this.radOPerson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radOPerson.CheckValue = "Y";
            this.radOPerson.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radOPerson.Location = new System.Drawing.Point(206, 9);
            this.radOPerson.Name = "radOPerson";
            this.radOPerson.Size = new System.Drawing.Size(51, 18);
            this.radOPerson.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radOPerson.TabIndex = 29;
            this.radOPerson.Text = "个人";
            this.radOPerson.CheckedChanged += new System.EventHandler(this.radOAll_CheckedChanged);
            // 
            // radODept
            // 
            this.radODept.AutoSize = true;
            // 
            // 
            // 
            this.radODept.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radODept.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radODept.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radODept.Location = new System.Drawing.Point(145, 9);
            this.radODept.Name = "radODept";
            this.radODept.Size = new System.Drawing.Size(51, 18);
            this.radODept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radODept.TabIndex = 28;
            this.radODept.Text = "科级";
            this.radODept.CheckedChanged += new System.EventHandler(this.radOAll_CheckedChanged);
            // 
            // radOAll
            // 
            this.radOAll.AutoSize = true;
            // 
            // 
            // 
            this.radOAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.radOAll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.radOAll.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radOAll.Location = new System.Drawing.Point(84, 9);
            this.radOAll.Name = "radOAll";
            this.radOAll.Size = new System.Drawing.Size(51, 18);
            this.radOAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.radOAll.TabIndex = 27;
            this.radOAll.Text = "院级";
            this.radOAll.CheckedChanged += new System.EventHandler(this.radOAll_CheckedChanged);
            // 
            // cmTree
            // 
            this.cmTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemNew,
            this.itemNewSub,
            this.itemEdit,
            this.itemDelete});
            this.cmTree.Name = "cmTree";
            this.cmTree.Size = new System.Drawing.Size(149, 92);
            this.cmTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmTree_Opening);
            // 
            // itemNew
            // 
            this.itemNew.Name = "itemNew";
            this.itemNew.Size = new System.Drawing.Size(148, 22);
            this.itemNew.Text = "新增同级节点";
            this.itemNew.Visible = false;
            this.itemNew.Click += new System.EventHandler(this.itemNew_Click);
            // 
            // itemNewSub
            // 
            this.itemNewSub.Name = "itemNewSub";
            this.itemNewSub.Size = new System.Drawing.Size(148, 22);
            this.itemNewSub.Text = "新增节点";
            this.itemNewSub.Click += new System.EventHandler(this.itemNewSub_Click);
            // 
            // itemEdit
            // 
            this.itemEdit.Name = "itemEdit";
            this.itemEdit.Size = new System.Drawing.Size(148, 22);
            this.itemEdit.Text = "修改节点";
            this.itemEdit.Click += new System.EventHandler(this.itemEdit_Click);
            // 
            // itemDelete
            // 
            this.itemDelete.Name = "itemDelete";
            this.itemDelete.Size = new System.Drawing.Size(148, 22);
            this.itemDelete.Text = "删除节点";
            this.itemDelete.Click += new System.EventHandler(this.itemDelete_Click);
            // 
            // MenuMid
            // 
            this.MenuMid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMidNew,
            this.itemMidNewSub,
            this.itemEditMidDrug,
            this.itemMidDelete});
            this.MenuMid.Name = "cmTree";
            this.MenuMid.Size = new System.Drawing.Size(149, 92);
            // 
            // itemMidNew
            // 
            this.itemMidNew.Name = "itemMidNew";
            this.itemMidNew.Size = new System.Drawing.Size(148, 22);
            this.itemMidNew.Text = "新增同级节点";
            this.itemMidNew.Visible = false;
            // 
            // itemMidNewSub
            // 
            this.itemMidNewSub.Name = "itemMidNewSub";
            this.itemMidNewSub.Size = new System.Drawing.Size(148, 22);
            this.itemMidNewSub.Text = "新增节点";
            // 
            // itemEditMidDrug
            // 
            this.itemEditMidDrug.Name = "itemEditMidDrug";
            this.itemEditMidDrug.Size = new System.Drawing.Size(148, 22);
            this.itemEditMidDrug.Text = "修改节点";
            // 
            // itemMidDelete
            // 
            this.itemMidDelete.Name = "itemMidDelete";
            this.itemMidDelete.Size = new System.Drawing.Size(148, 22);
            this.itemMidDelete.Text = "删除节点";
            // 
            // FrmOMRTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 370);
            this.Controls.Add(this.pParent);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmOMRTemplate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "将当前病历存为模板";
            this.CloseWindowAfter += new System.EventHandler(this.FrmOMRTemplate_CloseWindowAfter);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmOMRTemplate_FormClosed);
            this.Load += new System.EventHandler(this.FrmOMRTemplate_Load);
            this.pParent.ResumeLayout(false);
            this.pContent.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvOMRTpl)).EndInit();
            this.ptop.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx5.ResumeLayout(false);
            this.panelEx5.PerformLayout();
            this.cmTree.ResumeLayout(false);
            this.MenuMid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx pParent;
        private DevComponents.DotNetBar.PanelEx pContent;
        private DevComponents.DotNetBar.PanelEx ptop;
        private DevComponents.DotNetBar.Controls.CheckBoxX radOAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX radODept;
        private DevComponents.DotNetBar.Controls.CheckBoxX radOPerson;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private System.Windows.Forms.ToolStripMenuItem itemMidDelete;
        private System.Windows.Forms.ToolStripMenuItem itemEditMidDrug;
        private System.Windows.Forms.ToolStripMenuItem itemMidNewSub;
        private System.Windows.Forms.ToolStripMenuItem itemMidNew;
        private System.Windows.Forms.ContextMenuStrip MenuMid;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ToolStripMenuItem itemDelete;
        private System.Windows.Forms.ToolStripMenuItem itemEdit;
        private System.Windows.Forms.ToolStripMenuItem itemNewSub;
        private System.Windows.Forms.ToolStripMenuItem itemNew;
        private System.Windows.Forms.ContextMenuStrip cmTree;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ElementStyle elementStyle5;
        private DevComponents.AdvTree.NodeConnector nodeConnector5;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.AdvTree.AdvTree tvOMRTpl;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMouldName;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnOK;
    }
}