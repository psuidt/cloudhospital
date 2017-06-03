using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HIS_OPDoctor.Winform.ViewForm.MedicalApply
{
    public partial class FrmExamItemDetail : Form
    {
        public FrmExamItemDetail(DataTable dtItemDetail)
        {
            InitializeComponent();
            BindDt(dtItemDetail);
        }

        private void BindDt(DataTable dtDetail)
        {
            dgItemDetail.DataSource = dtDetail;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
