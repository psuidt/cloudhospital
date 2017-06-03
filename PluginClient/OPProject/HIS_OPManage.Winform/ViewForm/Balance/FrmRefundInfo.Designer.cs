namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmRefundInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRefundInfo));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPayInfo = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnRefund = new DevComponents.DotNetBar.ButtonX();
            this.btnReadMediaCard = new DevComponents.DotNetBar.ButtonX();
            this.txtMediaInfo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtRefundCash = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txtTotalFee = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtInvoice = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.btnClose);
            this.panelEx1.Controls.Add(this.btnRefund);
            this.panelEx1.Controls.Add(this.btnReadMediaCard);
            this.panelEx1.Controls.Add(this.txtMediaInfo);
            this.panelEx1.Controls.Add(this.txtRefundCash);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.txtTotalFee);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txtInvoice);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(449, 284);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPayInfo);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBox1.Location = new System.Drawing.Point(0, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 82);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "票据支付信息";
            // 
            // lblPayInfo
            // 
            // 
            // 
            // 
            this.lblPayInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPayInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblPayInfo.Location = new System.Drawing.Point(6, 22);
            this.lblPayInfo.Name = "lblPayInfo";
            this.lblPayInfo.Size = new System.Drawing.Size(426, 52);
            this.lblPayInfo.TabIndex = 18;
            this.lblPayInfo.Text = "1";
            this.lblPayInfo.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX7.Location = new System.Drawing.Point(2, 217);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(440, 68);
            this.labelX7.TabIndex = 17;
            this.labelX7.Text = "提示：\r\n1:原医保记账金额退回医保记账，原POS退POS。\r\n2:系统按先全退，再补收原则退费。\r\n3:部分退的，优惠金额按原优惠金额全退，再补收重新算优惠金额" +
    "。";
            this.labelX7.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Font = new System.Drawing.Font("宋体", 11F);
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(367, 193);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 22);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRefund
            // 
            this.btnRefund.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefund.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefund.Font = new System.Drawing.Font("宋体", 11F);
            this.btnRefund.Image = ((System.Drawing.Image)(resources.GetObject("btnRefund.Image")));
            this.btnRefund.Location = new System.Drawing.Point(271, 193);
            this.btnRefund.Name = "btnRefund";
            this.btnRefund.Size = new System.Drawing.Size(90, 22);
            this.btnRefund.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefund.TabIndex = 15;
            this.btnRefund.Text = "确定退费";
            this.btnRefund.Click += new System.EventHandler(this.btnRefund_Click);
            // 
            // btnReadMediaCard
            // 
            this.btnReadMediaCard.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReadMediaCard.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReadMediaCard.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReadMediaCard.Image = ((System.Drawing.Image)(resources.GetObject("btnReadMediaCard.Image")));
            this.btnReadMediaCard.Location = new System.Drawing.Point(5, 131);
            this.btnReadMediaCard.Name = "btnReadMediaCard";
            this.btnReadMediaCard.Size = new System.Drawing.Size(91, 22);
            this.btnReadMediaCard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReadMediaCard.TabIndex = 14;
            this.btnReadMediaCard.Text = "医保读卡";
            this.btnReadMediaCard.Click += new System.EventHandler(this.btnReadMediaCard_Click);
            // 
            // txtMediaInfo
            // 
            // 
            // 
            // 
            this.txtMediaInfo.Border.Class = "TextBoxBorder";
            this.txtMediaInfo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMediaInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtMediaInfo.Location = new System.Drawing.Point(102, 131);
            this.txtMediaInfo.Name = "txtMediaInfo";
            this.txtMediaInfo.ReadOnly = true;
            this.txtMediaInfo.Size = new System.Drawing.Size(340, 24);
            this.txtMediaInfo.TabIndex = 13;
            // 
            // txtRefundCash
            // 
            // 
            // 
            // 
            this.txtRefundCash.Border.Class = "TextBoxBorder";
            this.txtRefundCash.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRefundCash.Font = new System.Drawing.Font("宋体", 11F);
            this.txtRefundCash.Location = new System.Drawing.Point(102, 159);
            this.txtRefundCash.Name = "txtRefundCash";
            this.txtRefundCash.ReadOnly = true;
            this.txtRefundCash.Size = new System.Drawing.Size(340, 24);
            this.txtRefundCash.TabIndex = 11;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX6.Location = new System.Drawing.Point(43, 161);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(53, 23);
            this.labelX6.TabIndex = 10;
            this.labelX6.Text = "退现金";
            // 
            // txtTotalFee
            // 
            // 
            // 
            // 
            this.txtTotalFee.Border.Class = "TextBoxBorder";
            this.txtTotalFee.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTotalFee.Font = new System.Drawing.Font("宋体", 11F);
            this.txtTotalFee.Location = new System.Drawing.Point(303, 9);
            this.txtTotalFee.Name = "txtTotalFee";
            this.txtTotalFee.ReadOnly = true;
            this.txtTotalFee.Size = new System.Drawing.Size(139, 24);
            this.txtTotalFee.TabIndex = 3;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX2.Location = new System.Drawing.Point(216, 10);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(81, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "结算总金额";
            // 
            // txtInvoice
            // 
            // 
            // 
            // 
            this.txtInvoice.Border.Class = "TextBoxBorder";
            this.txtInvoice.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInvoice.Font = new System.Drawing.Font("宋体", 11F);
            this.txtInvoice.Location = new System.Drawing.Point(61, 9);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.ReadOnly = true;
            this.txtInvoice.Size = new System.Drawing.Size(149, 24);
            this.txtInvoice.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX1.Location = new System.Drawing.Point(3, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(52, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "票据号";
            // 
            // FrmRefundInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 284);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRefundInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "退费提示";
            this.Load += new System.EventHandler(this.FrmRefundInfo_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmRefundInfo_KeyUp);
            this.panelEx1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.ButtonX btnRefund;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRefundCash;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTotalFee;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInvoice;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX lblPayInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnReadMediaCard;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMediaInfo;
    }
}