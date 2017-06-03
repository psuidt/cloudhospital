namespace HIS_IPEMR.Winform.ViewForm
{
    partial class FrmEmrModel
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
            this.emrControl1 = new EfwControls.HISControl.Emr.Controls.EmrControl();
            this.SuspendLayout();
            // 
            // emrControl1
            // 
            this.emrControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emrControl1.EmrContrlType = EfwControls.HISControl.Emr.Controls.Entity.EmrContrlType.病历模板;
            this.emrControl1.Location = new System.Drawing.Point(0, 0);
            this.emrControl1.Name = "emrControl1";
            this.emrControl1.Size = new System.Drawing.Size(992, 532);
            this.emrControl1.TabIndex = 0;
            // 
            // FrmEmrModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 532);
            this.Controls.Add(this.emrControl1);
            this.Name = "FrmEmrModel";
            this.Text = "病历模板";
            this.ResumeLayout(false);

        }

        #endregion

        private EfwControls.HISControl.Emr.Controls.EmrControl emrControl1;
    }
}