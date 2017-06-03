namespace HIS_OPManage.Winform.ViewForm
{
    partial class FrmRegInvoiceInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegInvoiceInput));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtInvoiceNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lblPayInfo = new DevComponents.DotNetBar.LabelX();
            this.txtMedicareCardInfo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnReadMedicareCard = new DevComponents.DotNetBar.ButtonX();
            this.lblPatInfo = new DevComponents.DotNetBar.LabelX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 11F);
            this.labelX1.Location = new System.Drawing.Point(6, 3);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(239, 39);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "请输入要退的挂号收据号并按回车";
            // 
            // txtInvoiceNo
            // 
            // 
            // 
            // 
            this.txtInvoiceNo.Border.Class = "TextBoxBorder";
            this.txtInvoiceNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInvoiceNo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtInvoiceNo.Location = new System.Drawing.Point(253, 10);
            this.txtInvoiceNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.Size = new System.Drawing.Size(198, 24);
            this.txtInvoiceNo.TabIndex = 2;
            this.txtInvoiceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceNo_KeyDown);
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(234, 150);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96, 26);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确 定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 11F);
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(350, 150);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 26);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取 消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lblPatInfo);
            this.panelEx1.Controls.Add(this.lblPayInfo);
            this.panelEx1.Controls.Add(this.txtMedicareCardInfo);
            this.panelEx1.Controls.Add(this.btnReadMedicareCard);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Controls.Add(this.btnOK);
            this.panelEx1.Controls.Add(this.txtInvoiceNo);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(465, 181);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.None;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            // 
            // lblPayInfo
            // 
            // 
            // 
            // 
            this.lblPayInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPayInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblPayInfo.Location = new System.Drawing.Point(4, 76);
            this.lblPayInfo.Margin = new System.Windows.Forms.Padding(4);
            this.lblPayInfo.Name = "lblPayInfo";
            this.lblPayInfo.Size = new System.Drawing.Size(458, 31);
            this.lblPayInfo.TabIndex = 22;
            this.lblPayInfo.Text = "1";
            // 
            // txtMedicareCardInfo
            // 
            // 
            // 
            // 
            this.txtMedicareCardInfo.Border.Class = "TextBoxBorder";
            this.txtMedicareCardInfo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMedicareCardInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtMedicareCardInfo.Location = new System.Drawing.Point(107, 115);
            this.txtMedicareCardInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtMedicareCardInfo.Name = "txtMedicareCardInfo";
            this.txtMedicareCardInfo.Size = new System.Drawing.Size(355, 24);
            this.txtMedicareCardInfo.TabIndex = 21;
            this.txtMedicareCardInfo.Visible = false;
            // 
            // btnReadMedicareCard
            // 
            this.btnReadMedicareCard.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReadMedicareCard.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReadMedicareCard.Font = new System.Drawing.Font("宋体", 11F);
            this.btnReadMedicareCard.Location = new System.Drawing.Point(3, 115);
            this.btnReadMedicareCard.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadMedicareCard.Name = "btnReadMedicareCard";
            this.btnReadMedicareCard.Size = new System.Drawing.Size(96, 26);
            this.btnReadMedicareCard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReadMedicareCard.TabIndex = 20;
            this.btnReadMedicareCard.Text = "医保读卡";
            this.btnReadMedicareCard.Visible = false;
            this.btnReadMedicareCard.Click += new System.EventHandler(this.btnReadMedicareCard_Click);
            // 
            // lblPatInfo
            // 
            // 
            // 
            // 
            this.lblPatInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPatInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblPatInfo.Location = new System.Drawing.Point(4, 37);
            this.lblPatInfo.Margin = new System.Windows.Forms.Padding(4);
            this.lblPatInfo.Name = "lblPatInfo";
            this.lblPatInfo.Size = new System.Drawing.Size(458, 31);
            this.lblPatInfo.TabIndex = 23;
            // 
            // FrmRegInvoiceInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 181);
            this.Controls.Add(this.panelEx1);
            this.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegInvoiceInput";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "挂号退号";
            this.Load += new System.EventHandler(this.FrmRegInvoiceInput_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmRegInvoiceInput_KeyUp);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInvoiceNo;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMedicareCardInfo;
        private DevComponents.DotNetBar.ButtonX btnReadMedicareCard;
        private DevComponents.DotNetBar.LabelX lblPayInfo;
        private DevComponents.DotNetBar.LabelX lblPatInfo;
    }
}