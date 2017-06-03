namespace HIS_IPManage.Winform.ViewForm
{
    partial class FrmPayMentInfo
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
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.flpPayCtrl = new System.Windows.Forms.FlowLayoutPanel();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtBack = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtTheKnotComplement = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtPaymentTotal = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtAccountSum = new DevComponents.DotNetBar.LabelX();
            this.txtFavorableSum = new DevComponents.DotNetBar.LabelX();
            this.txtPersonalSum = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtAmount = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.lbSum = new DevComponents.DotNetBar.LabelX();
            this.btnGiveUp = new DevComponents.DotNetBar.ButtonX();
            this.btnConfirm = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupPanel2);
            this.panelEx1.Controls.Add(this.groupPanel1);
            this.panelEx1.Controls.Add(this.btnGiveUp);
            this.panelEx1.Controls.Add(this.btnConfirm);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(486, 237);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.flpPayCtrl);
            this.groupPanel2.DrawTitleBox = false;
            this.groupPanel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel2.Location = new System.Drawing.Point(5, 94);
            this.groupPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(475, 104);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 0;
            this.groupPanel2.Text = "支付明细";
            // 
            // flpPayCtrl
            // 
            this.flpPayCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPayCtrl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPayCtrl.Location = new System.Drawing.Point(0, 0);
            this.flpPayCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.flpPayCtrl.Name = "flpPayCtrl";
            this.flpPayCtrl.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.flpPayCtrl.Size = new System.Drawing.Size(469, 72);
            this.flpPayCtrl.TabIndex = 0;
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.txtBack);
            this.groupPanel1.Controls.Add(this.labelX8);
            this.groupPanel1.Controls.Add(this.txtTheKnotComplement);
            this.groupPanel1.Controls.Add(this.labelX6);
            this.groupPanel1.Controls.Add(this.txtPaymentTotal);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.txtAccountSum);
            this.groupPanel1.Controls.Add(this.txtFavorableSum);
            this.groupPanel1.Controls.Add(this.txtPersonalSum);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.txtAmount);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.lbSum);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(486, 92);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 3;
            // 
            // txtBack
            // 
            this.txtBack.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtBack.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtBack.BackgroundStyle.BorderBottomWidth = 1;
            this.txtBack.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtBack.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtBack.BackgroundStyle.BorderLeftWidth = 1;
            this.txtBack.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtBack.BackgroundStyle.BorderRightWidth = 1;
            this.txtBack.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtBack.BackgroundStyle.BorderTopWidth = 1;
            this.txtBack.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBack.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBack.ForeColor = System.Drawing.Color.Black;
            this.txtBack.Location = new System.Drawing.Point(371, 57);
            this.txtBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBack.Name = "txtBack";
            this.txtBack.Size = new System.Drawing.Size(78, 24);
            this.txtBack.TabIndex = 15;
            this.txtBack.Text = "0.00";
            this.txtBack.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX8.Location = new System.Drawing.Point(331, 61);
            this.labelX8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(36, 20);
            this.labelX8.TabIndex = 14;
            this.labelX8.Text = "结退";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtTheKnotComplement
            // 
            this.txtTheKnotComplement.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtTheKnotComplement.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtTheKnotComplement.BackgroundStyle.BorderBottomWidth = 1;
            this.txtTheKnotComplement.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtTheKnotComplement.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtTheKnotComplement.BackgroundStyle.BorderLeftWidth = 1;
            this.txtTheKnotComplement.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtTheKnotComplement.BackgroundStyle.BorderRightWidth = 1;
            this.txtTheKnotComplement.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtTheKnotComplement.BackgroundStyle.BorderTopWidth = 1;
            this.txtTheKnotComplement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTheKnotComplement.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTheKnotComplement.ForeColor = System.Drawing.Color.Black;
            this.txtTheKnotComplement.Location = new System.Drawing.Point(246, 57);
            this.txtTheKnotComplement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTheKnotComplement.Name = "txtTheKnotComplement";
            this.txtTheKnotComplement.Size = new System.Drawing.Size(77, 24);
            this.txtTheKnotComplement.TabIndex = 13;
            this.txtTheKnotComplement.Text = "0.00";
            this.txtTheKnotComplement.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX6.Location = new System.Drawing.Point(210, 60);
            this.labelX6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(36, 20);
            this.labelX6.TabIndex = 12;
            this.labelX6.Text = "结补";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtPaymentTotal
            // 
            this.txtPaymentTotal.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtPaymentTotal.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPaymentTotal.BackgroundStyle.BorderBottomWidth = 1;
            this.txtPaymentTotal.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtPaymentTotal.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPaymentTotal.BackgroundStyle.BorderLeftWidth = 1;
            this.txtPaymentTotal.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPaymentTotal.BackgroundStyle.BorderRightWidth = 1;
            this.txtPaymentTotal.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPaymentTotal.BackgroundStyle.BorderTopWidth = 1;
            this.txtPaymentTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPaymentTotal.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPaymentTotal.ForeColor = System.Drawing.Color.Black;
            this.txtPaymentTotal.Location = new System.Drawing.Point(109, 57);
            this.txtPaymentTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPaymentTotal.Name = "txtPaymentTotal";
            this.txtPaymentTotal.Size = new System.Drawing.Size(96, 24);
            this.txtPaymentTotal.TabIndex = 11;
            this.txtPaymentTotal.Text = "0.00";
            this.txtPaymentTotal.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(30, 60);
            this.labelX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(79, 20);
            this.labelX1.TabIndex = 10;
            this.labelX1.Text = "预交金总额";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtAccountSum
            // 
            this.txtAccountSum.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtAccountSum.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAccountSum.BackgroundStyle.BorderBottomWidth = 1;
            this.txtAccountSum.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtAccountSum.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAccountSum.BackgroundStyle.BorderLeftWidth = 1;
            this.txtAccountSum.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAccountSum.BackgroundStyle.BorderRightWidth = 1;
            this.txtAccountSum.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAccountSum.BackgroundStyle.BorderTopWidth = 1;
            this.txtAccountSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAccountSum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAccountSum.ForeColor = System.Drawing.Color.Black;
            this.txtAccountSum.Location = new System.Drawing.Point(353, 29);
            this.txtAccountSum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAccountSum.Name = "txtAccountSum";
            this.txtAccountSum.Size = new System.Drawing.Size(96, 24);
            this.txtAccountSum.TabIndex = 3;
            this.txtAccountSum.Text = "0.00";
            this.txtAccountSum.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtFavorableSum
            // 
            this.txtFavorableSum.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtFavorableSum.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtFavorableSum.BackgroundStyle.BorderBottomWidth = 1;
            this.txtFavorableSum.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtFavorableSum.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtFavorableSum.BackgroundStyle.BorderLeftWidth = 1;
            this.txtFavorableSum.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtFavorableSum.BackgroundStyle.BorderRightWidth = 1;
            this.txtFavorableSum.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtFavorableSum.BackgroundStyle.BorderTopWidth = 1;
            this.txtFavorableSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFavorableSum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFavorableSum.ForeColor = System.Drawing.Color.Black;
            this.txtFavorableSum.Location = new System.Drawing.Point(353, 1);
            this.txtFavorableSum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFavorableSum.Name = "txtFavorableSum";
            this.txtFavorableSum.Size = new System.Drawing.Size(96, 24);
            this.txtFavorableSum.TabIndex = 1;
            this.txtFavorableSum.Text = "0.00";
            this.txtFavorableSum.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtPersonalSum
            // 
            this.txtPersonalSum.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtPersonalSum.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPersonalSum.BackgroundStyle.BorderBottomWidth = 1;
            this.txtPersonalSum.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtPersonalSum.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPersonalSum.BackgroundStyle.BorderLeftWidth = 1;
            this.txtPersonalSum.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPersonalSum.BackgroundStyle.BorderRightWidth = 1;
            this.txtPersonalSum.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtPersonalSum.BackgroundStyle.BorderTopWidth = 1;
            this.txtPersonalSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPersonalSum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPersonalSum.ForeColor = System.Drawing.Color.Black;
            this.txtPersonalSum.Location = new System.Drawing.Point(109, 29);
            this.txtPersonalSum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPersonalSum.Name = "txtPersonalSum";
            this.txtPersonalSum.Size = new System.Drawing.Size(96, 24);
            this.txtPersonalSum.TabIndex = 2;
            this.txtPersonalSum.Text = "0.00";
            this.txtPersonalSum.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX4.Location = new System.Drawing.Point(277, 4);
            this.labelX4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(65, 20);
            this.labelX4.TabIndex = 9;
            this.labelX4.Text = "优惠金额";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX5.Location = new System.Drawing.Point(44, 32);
            this.labelX5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(65, 20);
            this.labelX5.TabIndex = 8;
            this.labelX5.Text = "自付金额";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.LemonChiffon;
            // 
            // 
            // 
            this.txtAmount.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAmount.BackgroundStyle.BorderBottomWidth = 1;
            this.txtAmount.BackgroundStyle.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtAmount.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAmount.BackgroundStyle.BorderLeftWidth = 1;
            this.txtAmount.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAmount.BackgroundStyle.BorderRightWidth = 1;
            this.txtAmount.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.txtAmount.BackgroundStyle.BorderTopWidth = 1;
            this.txtAmount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAmount.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAmount.ForeColor = System.Drawing.Color.Black;
            this.txtAmount.Location = new System.Drawing.Point(109, 1);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(96, 24);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.Text = "0.00";
            this.txtAmount.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX2.Location = new System.Drawing.Point(277, 32);
            this.labelX2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(65, 20);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "记账金额";
            // 
            // lbSum
            // 
            this.lbSum.AutoSize = true;
            this.lbSum.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbSum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbSum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSum.Location = new System.Drawing.Point(30, 4);
            this.lbSum.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbSum.Name = "lbSum";
            this.lbSum.Size = new System.Drawing.Size(79, 20);
            this.lbSum.TabIndex = 0;
            this.lbSum.Text = "住院总金额";
            this.lbSum.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnGiveUp
            // 
            this.btnGiveUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnGiveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGiveUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnGiveUp.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGiveUp.ImageTextSpacing = 6;
            this.btnGiveUp.Location = new System.Drawing.Point(402, 205);
            this.btnGiveUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGiveUp.Name = "btnGiveUp";
            this.btnGiveUp.Size = new System.Drawing.Size(75, 22);
            this.btnGiveUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnGiveUp.TabIndex = 2;
            this.btnGiveUp.Text = "取消(&C)";
            this.btnGiveUp.Click += new System.EventHandler(this.btnGiveUp_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfirm.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnConfirm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.ImageTextSpacing = 6;
            this.btnConfirm.Location = new System.Drawing.Point(313, 205);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 22);
            this.btnConfirm.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "结算(&S)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // FrmPayMentInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 237);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPayMentInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "结算面板";
            this.Load += new System.EventHandler(this.FrmPayMentInfo_Load);
            this.Shown += new System.EventHandler(this.FrmPayMentInfo_Shown);
            this.panelEx1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnConfirm;
        private DevComponents.DotNetBar.ButtonX btnGiveUp;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX lbSum;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private System.Windows.Forms.FlowLayoutPanel flpPayCtrl;
        private DevComponents.DotNetBar.LabelX txtAmount;
        private DevComponents.DotNetBar.LabelX txtAccountSum;
        private DevComponents.DotNetBar.LabelX txtFavorableSum;
        private DevComponents.DotNetBar.LabelX txtPersonalSum;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX txtPaymentTotal;
        private DevComponents.DotNetBar.LabelX txtTheKnotComplement;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX txtBack;
    }
}